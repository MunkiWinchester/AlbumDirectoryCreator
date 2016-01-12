using AlbumDirectoryCreator.Properties;
using Business;
using DataObjects;
using Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;
using File = System.IO.File;
using TextBox = System.Windows.Forms.TextBox;

namespace AlbumDirectoryCreator
{
    public partial class DirCreatorForm : Form
    {
        private string _pathIn;
        private string _pathOut;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly List<string> _fileInfos = new List<string>();
        private HashSet<TreeMp3> _treeViewHash = new HashSet<TreeMp3>();
        private readonly List<string> _extensions = new List<string> { "mp3", "wma" };
        private readonly List<KeyValuePair<string, string>> _oldAndNewKvPs = new List<KeyValuePair<string, string>>();
        private readonly Logger _logger = new Logger(LoggingType.UI);
        private Form _form;

        public DirCreatorForm()
        {
            InitializeComponent();
            InitializeDataTable();
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
                    .Where(file => _extensions.Contains(file.Split('.').Last()))
                    .ToList());

            Invoke((MethodInvoker)ReadMetaDatesTpl);
        }

        private void ReadMetaDatesTpl()
        {
            ClearBindingsEtc();

            // Variablen erstellen
            var artistList = new ConcurrentDictionary<int, string>();
            var albumList = new ConcurrentDictionary<int, string>();
            var idForTree = 2;
            var withoutInfo = 0;
            var withException = 0;
            var errorHappened = false;
            var path = string.Empty;

            _treeViewHash.Add(new TreeMp3(1, 0, "Without Artist", string.Empty, string.Empty, string.Empty));

            var readMetaDates = new ActionBlock<string>(fileInfo =>
            {
                try
                {
                    // Auslesen
                    var taglibFile = TagLib.File.Create(fileInfo);
                    var tagInf = taglibFile.Tag;

                    // Artist Hash
                    var performerHash = tagInf.FirstPerformer?.GetHashCode() ?? 0;
                    // Album Hash
                    var performerAlbumHash = taglibFile.GetPerformerAlbumHashKvP();

                    if (performerHash == 0 && performerAlbumHash.Key == 0)
                    {
                        artistList.TryAdd(performerHash, string.Empty);
                        albumList.TryAdd(performerAlbumHash.Key, string.Empty);
                        _treeViewHash.Add(new TreeMp3(idForTree, 1, tagInf.FirstPerformer, tagInf.Album, tagInf.Title, fileInfo));
                        withoutInfo++;
                        path = "00Music without Artist and Album\\";
                    }
                    else
                    {
                        // Artist hinzufügen
                        if (!artistList.ContainsKey(performerHash))
                        {
                            artistList.TryAdd(performerHash, tagInf.FirstPerformer);
                            _treeViewHash.Add(new TreeMp3(performerHash, 0, tagInf.FirstPerformer,
                                string.Empty, string.Empty, string.Empty));
                        }
                        // Album des Artist hinzufügen
                        if (artistList.ContainsKey(performerHash) && !albumList.ContainsKey(performerAlbumHash.Key))
                        {
                            var artistKvP = artistList.First(a => a.Value.Equals(tagInf.FirstPerformer));
                            albumList.TryAdd(performerAlbumHash.Key, performerAlbumHash.Value);
                            _treeViewHash.Add(new TreeMp3(performerAlbumHash.Key, artistKvP.Key, tagInf.FirstPerformer,
                                tagInf.Album, string.Empty, string.Empty));
                        }
                        if (artistList.ContainsKey(performerHash) && albumList.ContainsKey(performerAlbumHash.Key))
                        {
                            var hash = (string.IsNullOrWhiteSpace(tagInf.Album))
                                ? artistList.First(a => a.Value.Equals(tagInf.FirstPerformer))
                                : albumList.First(a => a.Key.Equals(performerAlbumHash.Key));
                            _treeViewHash.Add(new TreeMp3(idForTree, hash.Key, tagInf.Performers.ToNormalizedString(),
                                tagInf.Album, tagInf.Title, fileInfo));
                        }
                        if (!artistList.ContainsKey(performerHash) && !albumList.ContainsKey(performerAlbumHash.Key))
                        {
                            artistList.TryAdd(performerHash, tagInf.FirstPerformer);
                            var artistKvP = artistList.First(a => a.Value.Equals(tagInf.FirstPerformer));
                            albumList.TryAdd(tagInf.Album.GetHashCode(), tagInf.Album);
                            _treeViewHash.Add(new TreeMp3(performerHash, artistKvP.Key, tagInf.FirstPerformer,
                                tagInf.Album, string.Empty, string.Empty));
                            _treeViewHash.Add(new TreeMp3(idForTree, performerHash,
                                tagInf.Performers.ToNormalizedString(),
                                tagInf.Album, tagInf.Title, fileInfo));
                        }

                        // Pfad erstellen
                        if (!string.IsNullOrWhiteSpace(tagInf.FirstPerformer) &&
                            !string.IsNullOrWhiteSpace(tagInf.Album))
                        {
                            path = string.Format("{0}\\{1}\\",
                                tagInf.FirstPerformer.RemoveInvalidPathCharsAndToTitleCase(),
                                tagInf.Album.RemoveInvalidPathCharsAndToTitleCase());
                        }
                        else if (!string.IsNullOrWhiteSpace(tagInf.FirstPerformer))
                        {
                            path = string.Format("{0}\\", tagInf.FirstPerformer.RemoveInvalidPathCharsAndToTitleCase());
                        }
                        idForTree++;
                    }
                    // Pfad für die Datei und Ursprungspfad der Datei
                    _oldAndNewKvPs.Add(new KeyValuePair<string, string>(path, fileInfo));
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
                    MaxDegreeOfParallelism = 1
                });

            // Dateien durchgehen
            Parallel.ForEach(_fileInfos, fileInfo =>
            {
                readMetaDates.Post(fileInfo);
            });

            _stopwatch.Start();
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
                    _logger.Info($"{withoutInfo} files that don't have tags / couldn't be read in");
                    _logger.Info($"{withException} files that triggered an exception and are ignored");
                    _logger.Info($"{_oldAndNewKvPs.Count} files that are successfully read in");
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
            bindingSourceTree.CurrentChanged -= bindingSourceTree_CurrentChanged;

            // Sort the stuff and bind it to the binding sources
            bindingSourceFiles.DataSource = _treeViewHash.Where(t => t != null)
                .Where(a => !string.IsNullOrWhiteSpace(a.Path)).OrderBy(x => x.Artist).ThenBy(x => x.Album).ToList();
            // todo: Fight against Stackoverflow in TreeView
            bindingSourceTree.DataSource =
                _treeViewHash.Where(t => t != null).GroupBy(t => t.Id).Select(g => g.First()).ToList();
            dataTreeListView.Sort(olvColumnArtist);

            // Attach current changed event
            bindingSourceFiles.CurrentChanged += bindingSourceFiles_CurrentChanged;
            bindingSourceTree.CurrentChanged += bindingSourceTree_CurrentChanged;
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
                _logger.Info($"Transfering {_oldAndNewKvPs.Count} files to \"{_pathOut}\" with artist/album structure");
                foreach (var keyValuePair in _oldAndNewKvPs)
                {
                    var path = $"{_pathOut}\\{keyValuePair.Key}";
                    Directory.CreateDirectory(path);
                    var file = keyValuePair.Value.Split('\\').Last();

                    // falls die Datei schon existiert
                    var counter = 1;
                    var fileNameOnly = Path.GetFileNameWithoutExtension(file);
                    var fileExtension = Path.GetExtension(file);
                    file = Path.Combine(path, $"{fileNameOnly}{fileExtension}");
                    while (File.Exists(file))
                    {
                        var tempFileName = $"{fileNameOnly} ({counter++})";
                        file = Path.Combine(path, $"{tempFileName}{fileExtension}");
                    }

                    // Datei in neue Struktur kopieren
                    File.Move(keyValuePair.Value, file);
                }
                _logger.Info($"Deleting empty folders from the origin path \"{_pathIn}\"");
                Helper.DeleteEmptyFolders(_pathOut);
            }
        }

        private void ClearBindingsEtc()
        {
            bindingSourceFiles.DataSource = null;
            bindingSourceTree.DataSource = null;
            _treeViewHash = new HashSet<TreeMp3>();
            _oldAndNewKvPs.Clear();
        }

        private void InitializeDataTable()
        {
            dataTreeListView.KeyAspectName = "Id";
            dataTreeListView.ParentKeyAspectName = "ParentId";
            dataTreeListView.RootKeyValue = 0;
            dataTreeListView.AutoSizeColumns();
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
            _stopwatch.Start();
            CreateFolderEtc();
        }

        private void backgroundWorkerCreate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Invoke((MethodInvoker)_form.Close);
            _stopwatch.Stop();
            _logger.Info($"Transfering completed within: {_stopwatch.Elapsed}");
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxPathOrigins.Text))
            {
                _pathIn = textBoxPathOrigins.Text;
                Settings.Default.pathOrigin = _pathIn;
                Settings.Default.Save();
                backgroundWorkerGetFiles.RunWorkerAsync();
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            backgroundWorkerCreate.RunWorkerAsync();
        }

        private void buttonSearchOriginPath_Click(object sender, EventArgs e)
        {
            var result = folderDialogOrigins.ShowDialog();
            if (result == DialogResult.OK)
            {
                _pathIn = textBoxPathOrigins.Text = folderDialogOrigins.SelectedPath;
            }
        }

        private void buttonSearchDestinyPath_Click(object sender, EventArgs e)
        {
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

        private void bindingSourceTree_CurrentChanged(object sender, EventArgs e)
        {
            var current = bindingSourceTree.Current as TreeMp3;
            if (!string.IsNullOrEmpty(current?.Path))
                EnableEditorAndSetValues(current.Path);
            else
                DisableEditorAndClear();
        }

        private void bindingSourceFiles_CurrentChanged(object sender, EventArgs e)
        {
            var current = bindingSourceFiles.Current as TreeMp3;
            if (!string.IsNullOrEmpty(current?.Path))
                EnableEditorAndSetValues(current.Path);
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
            var previous = _treeViewHash.First(x => taglibFile != null && x.Path.Equals(taglibFile.Name));
            if (taglibFile != null && previous != null)
            {
                _treeViewHash.Remove(previous);
                _treeViewHash.Add(new TreeMp3(previous.Id, previous.ParentId,
                    taglibFile.Tag.Performers.ToNormalizedString(), taglibFile.Tag.Album, taglibFile.Tag.Title,
                    previous.Path));

                BindAndSort();
            }
        }
    }
}