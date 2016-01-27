using AlbumDirectoryCreator.Properties;
using Logging;
using Logic.Business;
using Logic.DataObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;
using TagLib;
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
        private ConcurrentDictionary<string, BaseInfoTag> _hashSet = new ConcurrentDictionary<string, BaseInfoTag>();
        private readonly Logger _logger = new Logger(LoggingType.UI);

        public DirCreatorForm()
        {
            InitializeComponent();
        }

        private void EnumerateFiles()
        {
            _fileInfos.Clear();
            _fileInfos.AddRange(
                Directory.EnumerateFiles(_pathIn, "*.*", SearchOption.AllDirectories)
                    .Where(file => _extensions.Contains(file.Split('.').Last().ToLower()))
                    .ToList());

            ReadMetaDatesTpl();
        }

        private async void ReadMetaDatesTpl()
        {
            ClearBindingsEtc();
            if (!_hashSet.Any())
            {
                _logger.Info(
                    $"{_fileInfos.Count} files ({string.Join("; ", _extensions)}) being read in from \"{_pathIn}\"");
                var errorHappened = false;
                var withException = 0;
                progressBar.Maximum = _hashSet.Count;
                var transformBlock = new TransformBlock<string, BaseInfoTag>(s => { progressBar.PerformStep(); return Helper.ReadMetaDatas(s); },
                    new ExecutionDataflowBlockOptions
                    {
                        TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext(),
                        MaxDegreeOfParallelism = 20
                    });
                var readMetaDates = new ActionBlock<BaseInfoTag>(treeMp3 =>
                {
                    if (treeMp3 != null)
                        _hashSet.TryAdd(treeMp3.FileInfo, treeMp3);
                    else
                    {
                        withException++;
                        errorHappened = true;
                    }
                },
                    new ExecutionDataflowBlockOptions
                    {
                        TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()
                    });
                transformBlock.LinkTo(readMetaDates, new DataflowLinkOptions { PropagateCompletion = true });

                _stopwatch.Start();
                progressBar.Maximum = _fileInfos.Count;
                // Dateien durchgehen
                Parallel.ForEach(_fileInfos, fileInfo =>
                {
                    transformBlock.Post(fileInfo);
                });

                transformBlock.Complete();
                await transformBlock.Completion;
                _stopwatch.Stop();

                if (errorHappened)
                    MessageBox.Show(
                        $"{Resources.ErrorOccured}\r\n{withException} files caused an exception.\r\n(View Logfile to see which files)",
                        Resources.Error,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Logfile schreiben
                _logger.Info($"Analyzing of {_hashSet.Count} files done within {_stopwatch.Elapsed}");
                _stopwatch.Reset();
                _logger.Info($"{withException} files that triggered an exception and were ignored");
                var percentage = Helper.CalculatePercentage(_hashSet.Count, _fileInfos.Count, 0);
                _logger.Info($"{_hashSet.Count} files that are successfully read in ({percentage}%)");
                // Show the stuff on the UI
                BindAndSort();
                buttonCreate.Enabled = true;
            }
        }

        private void BindAndSort(string fileInfoOfEdited = "")
        {
            // Detach current changed event
            bindingSourceFiles.CurrentChanged -= bindingSourceFiles_CurrentChanged;

            // Sort the stuff and bind it to the binding sources
            bindingSourceFiles.DataSource = _hashSet.Values.ToDataTable();
            if (_hashSet.Values.Count != 0)
                bindingSourceFiles.Sort = $"{nameof(BaseInfoTag.FirstPerformer)}, {nameof(BaseInfoTag.Album)}";

            // Attach current changed event
            bindingSourceFiles.CurrentChanged += bindingSourceFiles_CurrentChanged;

            if (string.IsNullOrWhiteSpace(fileInfoOfEdited))
            {
                bindingSourceFiles.MoveNext();
                bindingSourceFiles.MoveFirst();
            }
            else
                bindingSourceFiles.Position = bindingSourceFiles.Find(nameof(BaseInfoTag.FileInfo), fileInfoOfEdited);
        }

        private async void CreateFolderEtc()
        {
            if (_hashSet.Any())
            {
                _logger.Info($"Transfering {_hashSet.Count} files ({string.Join("; ", _extensions)}) " +
                        $"from \"{_pathIn}\" to \"{_pathOut}\" with artist/album structure");

                var transformBlock = new TransformBlock<BaseInfoTag, bool>(t => { progressBar.PerformStep(); return Helper.MoveFile(t, _pathOut); },
                    new ExecutionDataflowBlockOptions
                    {
                        TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext(),
                        MaxDegreeOfParallelism = 20
                    });
                var successfully = 0;
                var withException = 0;
                var readMetaDates = new ActionBlock<bool>(x =>
                {
                    if (x)
                        successfully++;
                    else
                        withException++;
                },
                    new ExecutionDataflowBlockOptions
                    {
                        TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()
                    });
                transformBlock.LinkTo(readMetaDates, new DataflowLinkOptions { PropagateCompletion = true });

                _stopwatch.Start();
                progressBar.Maximum = _fileInfos.Count;
                // Dateien durchgehen
                Parallel.ForEach(_hashSet.Values, treeMp3 =>
                {
                    transformBlock.Post(treeMp3);
                });

                transformBlock.Complete();
                await transformBlock.Completion;
                ClearBindingsEtc();
                _stopwatch.Stop();

                _logger.Info($"{withException} files that triggered an exception and are ignored");
                var percentage = Helper.CalculatePercentage(_hashSet.Count, successfully, withException);
                _logger.Info($"{successfully} files that are successfully transfered within {_stopwatch.Elapsed} ({percentage}%)");
                _stopwatch.Reset();

                _logger.Info($"Deleting empty folders from the origin path \"{_pathIn}\"");
                Helper.DeleteEmptyFolders(_pathOut);
                buttonCreate.Enabled = false;
            }
        }

        private void ClearBindingsEtc()
        {
            bindingSourceFiles.Sort = "";
            bindingSourceFiles.DataSource = null;
            _hashSet = new ConcurrentDictionary<string, BaseInfoTag>();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var path = button == buttonSearch ? textBoxPathOrigins.Text : textBoxPathDestiny.Text;
                var pathExists = Directory.Exists(path);
                if (button == buttonSearch && pathExists)
                {
                    _pathIn = path;
                    Settings.Default.pathOrigin = _pathIn;
                    Settings.Default.Save();

                    EnumerateFiles();
                }
                else if (button == buttonCreate)
                {
                    if (Path.IsPathRooted(path) && !path.StartsWith("\\"))
                    {
                        if (!pathExists)
                            Directory.CreateDirectory(_pathOut);
                        _pathOut = path;
                        Settings.Default.pathDestiny = _pathOut;
                        Settings.Default.Save();

                        CreateFolderEtc();
                    }
                }
                else
                    MessageBox.Show($"Your given path (\"{path}\") is not valid!", Resources.Error,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSearchOriginPath_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            folderDialogOrigins.SelectedPath = _pathIn;
            var result = folderDialogOrigins.ShowDialog();
            if (result == DialogResult.OK && button != null)
            {
                if (button == buttonSearchOriginPath)
                    _pathIn = textBoxPathOrigins.Text = folderDialogOrigins.SelectedPath;
                else if (button == buttonSearchDestinyPath)
                    _pathOut = textBoxPathDestiny.Text = folderDialogDestiny.SelectedPath;
            }
        }

        private void textBoxPathOrigins_Enter(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            var toolTip = new ToolTip();
            if (textBox == textBoxPathOrigins)
                toolTip.Show(Resources.InputHint, textBox, 0, 15, 1500);
            else if (textBox == textBoxPathDestiny)
                toolTip.Show(Resources.OutputHint, textBox, 0, 15, 1500);
        }

        private void bindingSourceFiles_CurrentChanged(object sender, EventArgs e)
        {
            if (advancedDataGridView1.SelectedRows.Count > 1)
            {
                iD3Editor.SetValues((
                    from DataGridViewRow row in advancedDataGridView1.SelectedRows
                    select row.DataBoundItem as DataRowView
                    into current
                    select current?.Row[nameof(BaseInfoTag.FileInfo)]?.ToString()).ToList());
            }
            else
            {
                var current = bindingSourceFiles.Current as DataRowView;
                var path = current?.Row[nameof(BaseInfoTag.FileInfo)]?.ToString();
                SetEditorUp(!string.IsNullOrEmpty(path) ? path : string.Empty);
            }
        }

        private void SetEditorUp(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                iD3Editor.Enabled = true;
                iD3Editor.SetValues(path);
            }
            else
            {
                iD3Editor.Enabled = false;
                iD3Editor.Clear();
            }
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
                var tag = taglibFile.TagTypes != TagTypes.Id3v2 ? taglibFile.Tag : taglibFile.GetTag(TagTypes.Id3v2);
                var newTreeMp3 = new BaseInfoTag(tag.JoinedPerformers, tag.FirstPerformer, tag.Album, tag.Title,
                    previous.Key);
                _hashSet.AddOrUpdate(previous.Key, newTreeMp3, (s, mp3) => newTreeMp3);

                BindAndSort(previous.Key);
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

        private void DirCreatorForm_Load(object sender, EventArgs e)
        {
            _logger.Info("_________________________________________________________________");
            _logger.Info("AlbumDirectoryCreator is opened!");
            textBoxPathOrigins.Text = folderDialogOrigins.SelectedPath = _pathIn = Settings.Default.pathOrigin;
            textBoxPathDestiny.Text = folderDialogDestiny.SelectedPath = _pathOut = Settings.Default.pathDestiny;
        }
    }
}