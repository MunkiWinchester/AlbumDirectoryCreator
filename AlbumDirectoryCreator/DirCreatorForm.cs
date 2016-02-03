using AlbumDirectoryCreator.Components;
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
using File = TagLib.File;
using TextBox = System.Windows.Forms.TextBox;

namespace AlbumDirectoryCreator
{
    public partial class DirCreatorForm : Form
    {
        private string _pathIn;
        private string _pathOut;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private List<string> _fileInfos = new List<string>();
        private readonly List<string> _extensions = new List<string> { "mp3", "wma" };
        private ConcurrentDictionary<string, BaseInfoTag> _hashSet = new ConcurrentDictionary<string, BaseInfoTag>();
        private readonly Logger _logger = new Logger();
        private float _stepValue;
        private float _currentPercentage;
        private static int _maxDegreeOfParallelism = 2;

        public DirCreatorForm()
        {
            InitializeComponent();
        }

        private void ClearBindingsEtc()
        {
            bindingSourceFiles.Sort = "";
            bindingSourceFiles.DataSource = null;
            _fileInfos.Clear();
            _hashSet = new ConcurrentDictionary<string, BaseInfoTag>();
        }

        private void BindAndSort(string fileInfoOfEdited = "")
        {
            // Detach current changed event
            bindingSourceFiles.CurrentChanged -= bindingSourceFiles_CurrentChanged;

            // Sort the stuff and bind it to the binding sources
            bindingSourceFiles.DataSource = _hashSet.Values.ToDataTable();
            if (_hashSet.Values.Count != 0)
                bindingSourceFiles.Sort =
                    $"{nameof(BaseInfoTag.FirstPerformer)}, {nameof(BaseInfoTag.Album)}, {nameof(BaseInfoTag.Title)}";

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

        private async void EnumerateFiles()
        {
            ClearBindingsEtc();
            _fileInfos = Helper.GetAllFiles(_pathIn, _extensions);
            _logger.Info(
                    $"{_fileInfos.Count} files ({string.Join("; ", _extensions)}) being read in from \"{_pathIn}\"");
            var errorHappened = false;
            var withException = 0;

            var actionBlock = new ActionBlock<string>(s =>
            {
                PerformStep();
                var baseInfoTags = Helper.ReadMetaDatas(s);
                if (baseInfoTags != null)
                    _hashSet.TryAdd(baseInfoTags.FileInfo, baseInfoTags);
                else
                {
                    withException++;
                    errorHappened = true;
                }
            }, new ExecutionDataflowBlockOptions
            {
                TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext(),
                MaxDegreeOfParallelism = _maxDegreeOfParallelism
            });

            StartOrStop(true, _fileInfos.Count);
            // Dateien durchgehen
            Parallel.ForEach(_fileInfos, fileInfo =>
            {
                actionBlock.Post(fileInfo);
            });
            actionBlock.Complete();
            await actionBlock.Completion;
            StartOrStop(false);

            if (errorHappened)
                MessageBox.Show(
                    $"{Resources.ErrorOccured}\r\n{withException} files caused an exception.\r\n(View Logfile to see which files)",
                    Resources.Error,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Logfile schreiben
            if (withException > 0)
                _logger.Info($"{withException} files that triggered an exception and were ignored");
            var percentage = Helper.CalculatePercentage(_hashSet.Count, _fileInfos.Count);
            _logger.Info($"{_hashSet.Count} files that are successfully read in within {_stopwatch.Elapsed} ({percentage}%)");
            // Show the stuff on the UI
            BindAndSort();

            buttonMove.Enabled = true;
        }

        private async void CreateFolderEtc()
        {
            if (_hashSet.Any())
            {
                _logger.Info($"Transfering {_hashSet.Count} files ({string.Join("; ", _extensions)}) " +
                        $"from \"{_pathIn}\" to \"{_pathOut}\" with artist/album structure");

                var successfully = 0;
                var withException = 0;
                var actionBlock = new ActionBlock<BaseInfoTag>(b =>
                {
                    PerformStep();
                    if (Helper.MoveFile(b, _pathOut))
                        successfully++;
                    else
                        withException++;
                }, new ExecutionDataflowBlockOptions
                {
                    TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext(),
                    MaxDegreeOfParallelism = _maxDegreeOfParallelism
                });

                StartOrStop(true, _hashSet.Count);
                // Dateien durchgehen
                Parallel.ForEach(_hashSet.Values, baseInfoTag =>
                {
                    actionBlock.Post(baseInfoTag);
                });
                actionBlock.Complete();
                await actionBlock.Completion;
                StartOrStop(false);

                if (withException > 0)
                    _logger.Info($"{withException} files that triggered an exception and are ignored");
                var percentage = Helper.CalculatePercentage(successfully, _hashSet.Count);
                _logger.Info($"{successfully} files that are successfully transfered within {_stopwatch.Elapsed} ({percentage}%)");
                _logger.Info($"Deleting empty folders from the origin path \"{_pathIn}\"");
                Helper.DeleteEmptyFolders(_pathIn);

                buttonMove.Enabled = false;
                ClearBindingsEtc();
            }
        }

        private void StartOrStop(bool start, int maximum = 0)
        {
            if (start)
            {
                _stepValue = (float)100 / maximum;
                progressBar.Value = 0;
                _currentPercentage = 0;
                labelPercentage.Text = "";
                _stopwatch.Reset();
                labelPercentage.Refresh();
                progressBar.Maximum = maximum;
                _stopwatch.Start();
                LoadingAnimation.Start(advancedDataGridView);
            }
            else
            {
                LoadingAnimation.End(advancedDataGridView);
                _stopwatch.Stop();
            }
        }

        private void PerformStep()
        {
            progressBar.PerformStep();
            _currentPercentage += _stepValue;
            labelPercentage.Text = $"{Convert.ToInt32(_currentPercentage)}%";
            labelPercentage.Refresh();
        }

        private bool SavePath(string path, bool isIn)
        {
            if (isIn)
            {
                if (Directory.Exists(path))
                {
                    _pathIn = path;
                    Settings.Default.pathOrigin = _pathIn;
                    Settings.Default.Save();
                    return true;
                }
                MessageBox.Show($"Your given path (\"{path}\") is not valid!", Resources.Error,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Path.IsPathRooted(path) && !path.StartsWith("\\"))
            {
                _pathOut = path;
                Settings.Default.pathDestiny = _pathOut;
                Settings.Default.Save();
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(_pathOut);
                return true;
            }
            MessageBox.Show($"Your given path (\"{path}\") is not valid!", Resources.Error,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private void DirCreatorForm_Load(object sender, EventArgs e)
        {
            _logger.Info("_________________________________________________________________");
            _logger.Info("AlbumDirectoryCreator is opened!");
            textBoxPathOrigins.Text = _pathIn = Settings.Default.pathOrigin;
            textBoxPathDestiny.Text = _pathOut = Settings.Default.pathDestiny;
            _maxDegreeOfParallelism = Environment.ProcessorCount * 2;
        }

        private void buttonAction_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var path = button == buttonEnumerate ? textBoxPathOrigins.Text : textBoxPathDestiny.Text;
                if (button == buttonEnumerate)
                {
                    if (SavePath(path, true))
                        EnumerateFiles();
                }
                else if (button == buttonMove)
                {
                    if (SavePath(path, false))
                        CreateFolderEtc();
                }
            }
        }

        private void buttonSearchPath_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var isOrigin = button == buttonSearchOriginPath;
                folderDialog.SelectedPath = isOrigin ? _pathIn : _pathOut;
                folderDialog.Description = isOrigin ? Resources.InputHint : Resources.OutputHint;
                var result = folderDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    SavePath(folderDialog.SelectedPath, isOrigin);
                }
            }
        }

        private void textBoxPath_Enter(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            var toolTip = new ToolTip();
            if (textBox == textBoxPathOrigins)
                toolTip.Show(Resources.InputHint, textBox, 0, 15, 1500);
            else if (textBox == textBoxPathDestiny)
                toolTip.Show(Resources.OutputHint, textBox, 0, 15, 1500);
        }

        private void textBoxPath_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && e.KeyCode == Keys.Enter)
            {
                SavePath(textBox == textBoxPathOrigins ? textBoxPathOrigins.Text : textBoxPathDestiny.Text,
                    textBox == textBoxPathOrigins);
            }
        }

        private void bindingSourceFiles_CurrentChanged(object sender, EventArgs e)
        {
            if (advancedDataGridView.SelectedRows.Count > 1)
            {
                iD3Editor.SetValues((
                    from DataGridViewRow row in advancedDataGridView.SelectedRows
                    select row.DataBoundItem as DataRowView
                    into current
                    select current?.Row[nameof(BaseInfoTag.FileInfo)]?.ToString()).ToList());
            }
            else
            {
                var current = bindingSourceFiles.Current as DataRowView;
                var path = current?.Row[nameof(BaseInfoTag.FileInfo)]?.ToString();
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
        }

        private void linkLabelLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var logfilePath =
                $@"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\logs\{DateTime.Today.ToString("yyyy-MM-dd")}.log";
            Process.Start(logfilePath);
        }

        private void advancedDataGridView_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSourceFiles.Filter = advancedDataGridView.FilterString;
        }

        private void advancedDataGridView_SortStringChanged(object sender, EventArgs e)
        {
            bindingSourceFiles.Sort = advancedDataGridView.SortString;
        }

        private void iD3Editor_ItemSaved(object sender, EventArgs e)
        {
            var kvp = sender as KeyValuePair<string, File>? ?? new KeyValuePair<string, File>();
            if (kvp.Key != null)
            {
                var taglibFile = kvp.Value;
                var previous = _hashSet.First(x => taglibFile != null && x.Key.Equals(kvp.Key));
                if (previous.Value != null)
                {
                    var tag = taglibFile.TagTypes != TagTypes.Id3v2 ? taglibFile.Tag : taglibFile.GetTag(TagTypes.Id3v2);
                    var baseInfoTag = new BaseInfoTag(tag.JoinedPerformers, tag.FirstPerformer, tag.Album, tag.Title,
                        taglibFile.Name);
                    _hashSet.TryAdd(baseInfoTag.FileInfo, baseInfoTag);
                    _hashSet.TryRemove(previous.Key, out baseInfoTag);

                    BindAndSort(baseInfoTag.FileInfo);
                }
            }
        }
    }
}