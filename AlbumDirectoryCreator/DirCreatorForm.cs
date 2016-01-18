using AlbumDirectoryCreator.Properties;
using Business;
using DataObjects;
using Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;

namespace AlbumDirectoryCreator
{
    public partial class DirCreatorForm : Form
    {
        private string _pathIn;
        private string _pathOut;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly List<string> _fileInfos = new List<string>();
        private readonly List<string> _extensions = new List<string> { "mp3", "wma" };
        private ConcurrentDictionary<string, TreeMp3> _hashSet = new ConcurrentDictionary<string, TreeMp3>();
        private readonly Logger _logger = new Logger(LoggingType.UI);
        private Form _form;

        public DirCreatorForm()
        {
            InitializeComponent();
            _logger.Info("_________________________________________________________________");
            _logger.Info("AlbumDirectoryCreator is opened!");
            textBoxPathOrigins.Text = folderDialogOrigins.SelectedPath = _pathIn = Settings.Default.pathOrigin;
            textBoxPathDestiny.Text = folderDialogDestiny.SelectedPath = _pathOut = Settings.Default.pathDestiny;
        }

        private void EnumerateFiles()
        {
            _fileInfos.Clear();
            _fileInfos.AddRange(
                Directory.EnumerateFiles(_pathIn, "*.*", SearchOption.AllDirectories)
                    .Where(file => _extensions.Contains(file.Split('.').Last().ToLower()))
                    .ToList());

            Invoke((MethodInvoker)ReadMetaDatesTpl);
        }

        private void ReadMetaDatesTpl()
        {
            ClearBindingsEtc();

            // Variablen erstellen
            var withoutInfo = 0;
            var withException = 0;
            var errorHappened = false;
            progressBar.Maximum = _hashSet.Count;

            var readMetaDates = new ActionBlock<string>(fileInfo =>
            {
                try
                {
                    progressBar.PerformStep();
                    // Auslesen
                    var taglibFile = TagLib.File.Create(fileInfo);
                    var tagInf = taglibFile.Tag;

                    if (string.IsNullOrWhiteSpace(tagInf.FirstPerformer))
                    {
                        _hashSet.TryAdd(fileInfo, new TreeMp3(string.Empty, tagInf.Album, tagInf.Title, fileInfo));
                        withoutInfo++;
                    }
                    else
                    {
                        _hashSet.TryAdd(fileInfo, new TreeMp3(tagInf.Performers.ToNormalizedString(),
                            tagInf.Album, tagInf.Title, fileInfo));
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"{ex.Message} -> \"{fileInfo}\"", ex);
                    withException++;
                    errorHappened = true;
                }
            },
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 1,
                    TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()
                });

            _stopwatch.Start();
            // Dateien durchgehen
            Parallel.ForEach(_fileInfos, fileInfo =>
            {
                readMetaDates.Post(fileInfo);
            });

            readMetaDates.Complete();
            readMetaDates.Completion.Wait(TimeSpan.FromMinutes(1));
            _stopwatch.Stop();

            var result = DialogResult.Cancel;
            if (errorHappened)
                result =
                    MessageBox.Show(
                        $"{Resources.ErrorOccured}\r\n{withException} files caused an exception.\r\n(Retry could be helping)",
                        Resources.Error,
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            switch (result)
            {
                case DialogResult.Cancel:
                    // Logfile schreiben
                    _logger.Info(
                        $"{_fileInfos.Count} files ({string.Join("; ", _extensions)}) read recursiveliv in from \"{_pathIn}\"");
                    _logger.Info($"{_fileInfos.Count} files analyzed within {_stopwatch.Elapsed}");
                    _stopwatch.Reset();
                    _logger.Info($"{withoutInfo} files that don't have an artist");
                    _logger.Info($"{withException} files that triggered an exception and are ignored");
                    var percentage = (float)_hashSet.Count / (_fileInfos.Count - withException) * 100;
                    _logger.Info($"{_hashSet.Count} files that are successfully read in ({percentage}%)");
                    // Show the stuff on the UI
                    BindAndSort();
                    buttonCreate.Enabled = true;
                    break;

                case DialogResult.Retry:
                    ReadMetaDatesTpl();
                    break;
            }
        }

        private void BindAndSort()
        {
            // Detach current changed event
            bindingSourceFiles.CurrentChanged -= bindingSourceFiles_CurrentChanged;

            // Sort the stuff and bind it to the binding sources
            bindingSourceFiles.DataSource = _hashSet.Values.ToDataTable();
            if (_hashSet.Values.Count != 0)
                bindingSourceFiles.Sort = $"{nameof(TreeMp3.Artist)}, {nameof(TreeMp3.Album)}";

            // Attach current changed event
            bindingSourceFiles.CurrentChanged += bindingSourceFiles_CurrentChanged;

            bindingSourceFiles.MoveNext();
            bindingSourceFiles.MoveFirst();
        }

        private void CreateFolderEtc()
        {
            var pathOut = textBoxPathDestiny.Text;
            var result = Path.IsPathRooted(pathOut);
            if (result && !pathOut.StartsWith(@"\"))
            {
                if (Directory.Exists(pathOut))
                    _pathOut = pathOut;
                else
                {
                    Directory.CreateDirectory(pathOut);
                    _pathOut = pathOut;
                }
                Settings.Default.pathDestiny = _pathOut;
                Settings.Default.Save();
                _logger.Info($"Transfering {_hashSet.Count} files to \"{_pathOut}\" with artist/album structure");

                var successfully = 0;
                var withException = 0;

                var moveFiles = new ActionBlock<TreeMp3>(treeMp3 =>
                {
                    try
                    {
                        var path = $"{_pathOut}\\{treeMp3.NewPath}";
                        Directory.CreateDirectory(path);
                        var oldFileInfo = new FileInfo(treeMp3.FileInfo);

                        // falls die Datei schon existiert
                        var counter = 1;
                        var newFileInfo = new FileInfo(Path.Combine(path, oldFileInfo.Name));
                        var nameWithoutExtension = Path.GetFileNameWithoutExtension(oldFileInfo.Name);
                        while (newFileInfo.Exists)
                        {
                            var tempFileName = $"{nameWithoutExtension} ({counter++})";
                            newFileInfo = new FileInfo(Path.Combine(path, $"{tempFileName}{oldFileInfo.Extension}"));
                        }
                        // Datei in neue Struktur kopieren
                        oldFileInfo.MoveTo(newFileInfo.FullName);
                        successfully++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"{ex.Message} -> \"{treeMp3.FileInfo}\"", ex);
                        withException++;
                    }
                },
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 1,
                    TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()
                });

                _stopwatch.Start();
                // Dateien durchgehen
                foreach (var treeMp3 in _hashSet.Values)
                {
                    moveFiles.Post(treeMp3);
                }

                moveFiles.Complete();
                moveFiles.Completion.Wait(TimeSpan.FromMinutes(1));
                _stopwatch.Stop();

                _logger.Info($"{withException} files that triggered an exception and are ignored");
                var percentage = (float)successfully / (_hashSet.Count - withException) * 100;
                _logger.Info($"{successfully} files that are successfully transfered within {_stopwatch.Elapsed} ({percentage}%)");
                _stopwatch.Reset();

                _logger.Info($"Deleting empty folders from the origin path \"{_pathIn}\"");
                Helper.DeleteEmptyFolders(_pathOut);
            }
        }

        private void ClearBindingsEtc()
        {
            bindingSourceFiles.Sort = "";
            bindingSourceFiles.DataSource = null;
            _hashSet = new ConcurrentDictionary<string, TreeMp3>();
        }

        private void backgroundWorkerGetFiles_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Invoke((MethodInvoker)ShowLoadingAnimation);
            EnumerateFiles();
        }

        private void backgroundWorkerGetFiles_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Invoke((MethodInvoker)_form.Close);
        }

        private void backgroundWorkerCreate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Invoke((MethodInvoker)ShowLoadingAnimation);
            CreateFolderEtc();
        }

        private void backgroundWorkerCreate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Invoke((MethodInvoker)_form.Close);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxPathOrigins.Text))
            {
                _pathIn = textBoxPathOrigins.Text;
                Settings.Default.pathOrigin = _pathIn;
                Settings.Default.Save();

                ShowLoadingAnimation();
                EnumerateFiles();
                //backgroundWorkerGetFiles.RunWorkerAsync();
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            backgroundWorkerCreate.RunWorkerAsync();
        }

        private void buttonSearchOriginPath_Click(object sender, EventArgs e)
        {
            folderDialogOrigins.SelectedPath = _pathIn;
            var result = folderDialogOrigins.ShowDialog();
            if (result == DialogResult.OK)
            {
                _pathIn = textBoxPathOrigins.Text = folderDialogOrigins.SelectedPath;
            }
        }

        private void buttonSearchDestinyPath_Click(object sender, EventArgs e)
        {
            folderDialogOrigins.SelectedPath = _pathOut;
            var result = folderDialogDestiny.ShowDialog();
            if (result == DialogResult.OK)
            {
                _pathOut = textBoxPathDestiny.Text = folderDialogDestiny.SelectedPath;
            }
        }

        private void textBoxPathOrigins_Enter(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            var toolTip = new ToolTip();
            toolTip.Show(Resources.InputHint, textBox, 0, 15, 1500);
        }

        private void textBoxPathDestiny_Enter(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            var toolTip = new ToolTip();
            toolTip.Show(Resources.OutputHint, textBox, 0, 15, 1500);
        }

        private void bindingSourceFiles_CurrentChanged(object sender, EventArgs e)
        {
            var current = bindingSourceFiles.Current as DataRowView;
            var path = current?.Row[nameof(TreeMp3.FileInfo)]?.ToString();
            if (!string.IsNullOrEmpty(path))
                EnableEditorAndSetValues(path);
            else
                DisableEditorAndClear();
        }

        private void EnableEditorAndSetValues(string path)
        {
            iD3Editor.Enabled = true;
            iD3Editor.SetValues(path);
        }

        private void DisableEditorAndClear()
        {
            iD3Editor.Enabled = false;
            iD3Editor.Clear();
        }

        private void ShowLoadingAnimation()
        {
            const int height = 75;
            const int width = 75;
            _form = new Form
            {
                Size = new Size(width, height),
                FormBorderStyle = FormBorderStyle.None,
                MinimizeBox = false,
                MaximizeBox = false,
                StartPosition = FormStartPosition.Manual,
                Location = new Point(Location.X + (Width - width) / 2, Location.Y + (Height - height) / 2),
                BackColor = Color.Turquoise,
                TransparencyKey = Color.Turquoise
            };
            var im = Resources.LoadAnimation;
            var pb = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = im,
                Location = new Point(0, 0)
            };
            _form.Controls.Add(pb);
            _form.Show();
        }

        private void linkLabelLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var date = DateTime.Today.ToString("yyyy-MM-dd");
            var logfilePath =
                $@"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\logs\{date}.log";

            try
            {
                var p = new Process
                {
                    StartInfo =
                            {
                                FileName = "notepad++.exe",
                                Arguments = logfilePath
                            }
                };
                p.Start();
            }
            catch
            {
                MessageBox.Show(Resources.CantFindNotepadPlus, Resources.Error,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iD3Editor_ItemSaved(object sender, EventArgs e)
        {
            var taglibFile = sender as TagLib.File;
            var previous = _hashSet.First(x => taglibFile != null && x.Key.Equals(taglibFile.Name));
            if (taglibFile != null && previous.Value != null)
            {
                var newTreeMp3 = new TreeMp3(
                    taglibFile.Tag.Performers.ToNormalizedString(), taglibFile.Tag.Album, taglibFile.Tag.Title,
                    previous.Key);
                _hashSet.AddOrUpdate(previous.Key, new TreeMp3(
                    taglibFile.Tag.Performers.ToNormalizedString(), taglibFile.Tag.Album, taglibFile.Tag.Title,
                    previous.Key), (s, mp3) => newTreeMp3);

                BindAndSort();
            }
        }

        private void advancedDataGridView1_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSourceFiles.Filter = advancedDataGridView1.FilterString;
        }

        private void advancedDataGridView1_SortStringChanged(object sender, EventArgs e)
        {
            bindingSourceFiles.Sort = advancedDataGridView1.SortString;
        }
    }
}