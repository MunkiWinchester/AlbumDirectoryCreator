using HundredMilesSoftware.UltraID3Lib;
using Logging;
using Logic.Business;
using Logic.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using TagLib;
using File = TagLib.File;
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
            genres.RemoveAt(0);
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
            Clear();
            try
            {
                var tag = _file.TagTypes != TagTypes.Id3v2 ? _file.Tag : _file.GetTag(TagTypes.Id3v2);

                var performers = (from object item in tag.Performers select new Performer(item.ToString())).ToList();
                bindingSourcePerformers.DataSource = performers;
                textBoxAlbum.Text = tag.Album;
                textBoxTitle.Text = tag.Title;
                textBoxTitleNr.Text = tag.Track.ToString();
                textBoxYear.Text = tag.Year.ToString();
                textBoxComment.Text = tag.Comment;
                textBoxPath.Text = _file.Name;
                starsBox1.SetStars(tag.GetPopularimeterFrame()?.Rating.ToStars());

                foreach (var genre in tag.Genres)
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

            var tag = file.TagTypes != TagTypes.Id3v2 ? file.Tag : file.GetTag(TagTypes.Id3v2);
            var rating = tag.GetPopularimeterFrame();
            if (rating != null)
                rating.Rating = tag.SetRating(starsBox1.GetStars());

            var performers = (List<Performer>)bindingSourcePerformers.DataSource;
            if (performers != null)
                tag.Performers = performers.Select(performer => performer.ToString()).Where(p => p != null).ToArray();
            tag.Album = textBoxAlbum.Text;
            tag.Title = textBoxTitle.Text;
            tag.Track = string.IsNullOrWhiteSpace(textBoxTitleNr.Text) ? 0 : uint.Parse(textBoxTitleNr.Text);
            tag.Year = string.IsNullOrWhiteSpace(textBoxYear.Text) ? 1900 : uint.Parse(textBoxYear.Text);
            tag.Comment = textBoxComment.Text;
            tag.Genres =
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
                var starBox = ctrl as StarsBox;
                starBox?.SetStars(Stars.Zero);
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