using Business.Business;
using Business.DataObjects;
using HundredMilesSoftware.UltraID3Lib;
using Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using TagLib;
using TextBox = System.Windows.Forms.TextBox;

namespace AlbumDirectoryCreator
{
    public partial class Id3Editor : UserControl
    {
        [Browsable(true)]
        public event EventHandler ItemSaved;

        private static readonly Logger Logger = new Logger(LoggingType.UI);
        private File _file;

        public Id3Editor()
        {
            InitializeComponent();
            var genres = new UltraID3().GenreInfos;
            foreach (GenreInfo genre in genres)
            {
                checkedListBoxGenre.Items.Add(genre.Name);
            }
            dataGridViewPerformers.AutoGenerateColumns = true;
            bindingSourcePerformers.AllowNew = true;
        }

        private void textBoxYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        public void SetValues(string fileInfo)
        {
            if (System.IO.File.Exists(fileInfo))
            {
                _file = File.Create(fileInfo);
                SetValues();
            }
        }

        private void SetValues()
        {
            try
            {
                var fileTags = _file.Tag;

                var performers = (from object item in fileTags.Performers select new Performer(item.ToString())).ToList();
                bindingSourcePerformers.DataSource = performers;
                textBoxAlbum.Text = fileTags.Album;
                textBoxTitle.Text = fileTags.Title;
                textBoxTitleNr.Text = fileTags.Track.ToString();
                textBoxYear.Text = fileTags.Year.ToString();
                textBoxComment.Text = fileTags.Comment;
                textBoxPath.Text = _file.Name;

                foreach (var genre in fileTags.Genres)
                {
                    var index = checkedListBoxGenre.Items.IndexOf(genre);
                    if (index == -1)
                    {
                        checkedListBoxGenre.Items.Add(genre);
                        checkedListBoxGenre.SetItemChecked(checkedListBoxGenre.Items.IndexOf(genre), true);
                    }
                    else
                        checkedListBoxGenre.SetItemChecked(index, true);
                }
            }
            catch (CorruptFileException ex)
            {
                Logger.Error($"{_file.Name} is corrupted! Reasons: \"{_file.CorruptionReasons}\"",
                    ex);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var file = File.Create(_file.Name);
            var performers = (List<Performer>)bindingSourcePerformers.DataSource;
            if (performers != null)
                file.Tag.Performers = performers.Select(performer => performer.ToString()).Where(p => p != null).ToArray();
            file.Tag.Album = textBoxAlbum.Text;
            file.Tag.Title = textBoxTitle.Text;
            file.Tag.Track = string.IsNullOrWhiteSpace(textBoxTitleNr.Text) ? 0 : uint.Parse(textBoxTitleNr.Text);
            file.Tag.Year = string.IsNullOrWhiteSpace(textBoxYear.Text) ? 1900 : uint.Parse(textBoxYear.Text);
            file.Tag.Comment = textBoxComment.Text;
            file.Tag.Genres =
              (from object checkedItem in checkedListBoxGenre.CheckedItems select checkedItem.ToString()).ToArray();

            if (Id3Handler.Save(file, _file))
                ItemSaved?.Invoke(file, EventArgs.Empty);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SetValues();
        }

        public void Clear()
        {
            bindingSourcePerformers.Clear();
            foreach (Control ctrl in groupBoxEditor.Controls)
            {
                var box = ctrl as TextBox;
                if (box != null)
                    box.Text = string.Empty;

                var checkedListBox = ctrl as CheckedListBox;
                if (checkedListBox != null)
                {
                    foreach (int index in checkedListBox.CheckedIndices)
                    {
                        checkedListBox.SetItemCheckState(index, CheckState.Unchecked);
                    }
                }
            }
        }

        private void dataGridViewPerformers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                bindingSourcePerformers.Remove(bindingSourcePerformers.Current);
            }
        }
    }
}