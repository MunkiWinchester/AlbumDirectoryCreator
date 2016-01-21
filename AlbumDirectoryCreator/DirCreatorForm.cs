﻿using AlbumDirectoryCreator.Properties;
using Business.Business;
using Business.DataObjects;
using Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
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
        private int _withException;
        private bool _errorHappened;

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

            ReadMetaDatesTpl();
        }

        private async void ReadMetaDatesTpl()
        {
            ClearBindingsEtc();
            if (_hashSet.Any())
            {
                progressBar.Maximum = _hashSet.Count;
                var transformBlock = new TransformBlock<string, bool>(s => ReadMetaDatas(s),
                    new ExecutionDataflowBlockOptions
                    {
                        TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext(),
                        MaxDegreeOfParallelism = 20
                    });

                var readMetaDates = new ActionBlock<bool>(x =>
                {
                    PerformStep();
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
                //_form.Close();

                var result = DialogResult.Cancel;
                if (_errorHappened)
                    result =
                        MessageBox.Show(
                            $"{Resources.ErrorOccured}\r\n{_withException} files caused an exception.\r\n(Retry could be helping)",
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
                        _logger.Info($"{_withException} files that triggered an exception and are ignored");
                        var percentage = (float)_hashSet.Count / (_fileInfos.Count - _withException) * 100;
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

        private async void CreateFolderEtc()
        {
            var pathOut = textBoxPathDestiny.Text;
            var result = Path.IsPathRooted(pathOut);
            if (result && !pathOut.StartsWith(@"\") && _hashSet.Any())
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
                _logger.Info($"Transfering {_hashSet.Count} files from \"{_pathIn}\" to \"{_pathOut}\" with artist/album structure");

                var successfully = 0;
                var withException = 0;

                var transformBlock = new TransformBlock<TreeMp3, bool>(t => Helper.MoveFile(t, _pathOut),
                    new ExecutionDataflowBlockOptions
                    {
                        TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext(),
                        MaxDegreeOfParallelism = 20
                    });

                var readMetaDates = new ActionBlock<bool>(x =>
                {
                    if (x)
                        successfully++;
                    else
                        withException++;

                    PerformStep();
                },
                    new ExecutionDataflowBlockOptions
                    {
                        TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()
                    });

                transformBlock.LinkTo(readMetaDates, new DataflowLinkOptions { PropagateCompletion = true });

                progressBar.Maximum = _fileInfos.Count;
                // Dateien durchgehen
                Parallel.ForEach(_hashSet.Values, treeMp3 =>
                {
                    transformBlock.Post(treeMp3);
                });

                transformBlock.Complete();
                await transformBlock.Completion;
                _stopwatch.Stop();

                _logger.Info($"{withException} files that triggered an exception and are ignored");
                var percentage = (float)successfully / (_hashSet.Count - withException) * 100;
                _logger.Info($"{successfully} files that are successfully transfered within {_stopwatch.Elapsed} ({percentage}%)");
                _stopwatch.Reset();

                _logger.Info($"Deleting empty folders from the origin path \"{_pathIn}\"");
                Helper.DeleteEmptyFolders(_pathOut);
            }
        }

        private void PerformStep()
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(0.2));
            progressBar.PerformStep();
        }

        private void ClearBindingsEtc()
        {
            _errorHappened = false;
            _withException = 0;
            bindingSourceFiles.Sort = "";
            bindingSourceFiles.DataSource = null;
            _hashSet = new ConcurrentDictionary<string, TreeMp3>();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxPathOrigins.Text))
            {
                _pathIn = textBoxPathOrigins.Text;
                Settings.Default.pathOrigin = _pathIn;
                Settings.Default.Save();

                //ShowLoadingAnimation();
                EnumerateFiles();
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            CreateFolderEtc();
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