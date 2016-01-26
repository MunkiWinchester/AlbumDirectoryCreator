using AlbumDirectoryCreator.Properties;
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
        private bool _isMulti;
        private Id3MultiEditHelp _id3MultiEditHelp;

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
                _isMulti = false;
                _file = File.Create(fileInfo);
                SetValues();
            }
        }

        public void SetValues(List<string> fileInfoList)
        {
            _isMulti = true;
            _id3MultiEditHelp = Helper.GetTagsAndIntersectionFields(fileInfoList);
            SetValues();
        }

        private void SetValues()
        {
            Clear();
            try
            {
                List<string> genres;
                List<Performer> performers;
                string album;
                var year = string.Empty;
                string comment;
                Stars? rating;
                string title;
                var titleNr = string.Empty;
                string path;

                if (!_isMulti)
                {
                    var tag = _file.TagTypes != TagTypes.Id3v2 ? _file.Tag : _file.GetTag(TagTypes.Id3v2);

                    performers = (from object item in tag.Performers select new Performer(item.ToString())).ToList();
                    album = tag.Album;
                    title = tag.Title;
                    if (tag.Track != 0)
                        titleNr = tag.Track.ToString();
                    if (tag.Year != 0)
                        year = tag.Year.ToString();
                    comment = tag.Comment;
                    path = _file.Name;
                    rating = tag.GetPopularimeterFrame()?.Rating.ToStars();
                    genres = tag.Genres.ToList();
                }
                else
                {
                    genres = _id3MultiEditHelp.Genres?.ToList();
                    performers = _id3MultiEditHelp.Performers;
                    year = _id3MultiEditHelp.Year?.ToString();
                    album = string.Join("; ", _id3MultiEditHelp.Albums);
                    comment = _id3MultiEditHelp.Comment;
                    rating = _id3MultiEditHelp.Rating;
                    title = Resources.multiValue;
                    titleNr = Resources.multiValue;
                    path = Resources.multiValue;
                }

                bindingSourcePerformers.DataSource = performers;
                textBoxAlbum.Text = album;
                textBoxTitle.Text = title;
                textBoxYear.Text = year;
                textBoxComment.Text = comment;
                starsBoxRating.SetStars(rating);
                textBoxTitleNr.Text = titleNr;
                textBoxPath.Text = path;
                if (genres != null)
                    foreach (var genre in genres)
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
                rating.Rating = Helper.SetRating(starsBoxRating.GetStars());

            var performers = (List<Performer>)bindingSourcePerformers.DataSource;
            if (performers != null)
                tag.Performers = performers.Select(performer => performer.ToString()).Where(p => p != null).ToArray();
            tag.Album = textBoxAlbum.Text;
            tag.Title = textBoxTitle.Text;
            if (!string.IsNullOrWhiteSpace(textBoxTitleNr.Text))
                tag.Track = uint.Parse(textBoxTitleNr.Text);
            if (!string.IsNullOrWhiteSpace(textBoxYear.Text))
                tag.Year = uint.Parse(textBoxYear.Text);
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
            foreach (Control ctrl in tableLayoutPanel.Controls)
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