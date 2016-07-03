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

namespace AlbumDirectoryCreator.Components
{
    public partial class Id3Editor : UserControl
    {
        [Browsable(true)]
        public event EventHandler ItemSaved;

        [Browsable(true)]
        public event EventHandler MultiItemSaved;

        private static readonly Logger Logger = new Logger();
        private File _file;
        private bool _isMulti;
        private Id3MultiEditHelp _id3MultiEditHelp;

        public Id3Editor()
        {
            InitializeComponent();
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

                var picBox = ctrl as PictureBox;
                picBox?.Hide();

                var checkBox = ctrl as CheckBox;
                if (checkBox != null)
                    checkBox.Checked = false;
            }
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
            _id3MultiEditHelp = Id3Handler.GetTagsAndIntersectionFields(fileInfoList);
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

                if (genres == null) return;
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

        private void Id3Editor_Load(object sender, EventArgs e)
        {
            var genres = new UltraID3().GenreInfos;
            genres.RemoveAt(0);
            foreach (GenreInfo genre in genres)
            {
                checkedListBoxGenre.Items.Add(genre.Name);
            }
            dataGridViewPerformers.AutoGenerateColumns = true;
            bindingSourcePerformers.AllowNew = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var stars = (byte)starsBoxRating.GetStars();
            var performers = (List<Performer>)bindingSourcePerformers.DataSource;
            var album = textBoxAlbum.Text;
            uint trackNo = 0;
            if (!string.IsNullOrWhiteSpace(textBoxTitleNr.Text))
            {
                uint.TryParse(textBoxTitleNr.Text, out trackNo);
            }
            uint year = 0;
            if (!string.IsNullOrWhiteSpace(textBoxYear.Text))
            {
                uint.TryParse(textBoxYear.Text, out year);
            }
            var comment = textBoxComment.Text;
            var genres =
                    (from object checkedItem in checkedListBoxGenre.CheckedItems select checkedItem.ToString()).ToArray();

            if (!_isMulti)
            {
                var savedFile = SaveToFile(_file.Name, stars, performers, album, trackNo, year, comment, genres);
                if (savedFile != null)
                {
                    ItemSaved?.Invoke(new KeyValuePair<string, File>(_file.Name, savedFile), new EventArgs());
                    pictureBox.Show();
                }
            }
            else
            {
                var returnList = new List<KeyValuePair<string, File>>();
                foreach (var filePath in _id3MultiEditHelp.TagList.Keys)
                {
                    _file = File.Create(filePath);
                    var savedFile = SaveToFile(filePath, stars, performers, album, trackNo, year, comment, genres);
                    if (savedFile != null)
                    {
                        returnList.Add(new KeyValuePair<string, File>(filePath, savedFile));
                    }
                }
                MultiItemSaved?.Invoke(returnList, new EventArgs());
                pictureBox.Show();
            }
        }

        private File SaveToFile(string filePath, byte stars, List<Performer> performers, string album, uint trackNo, uint year,
            string comment, string[] genres)
        {
            var file = File.Create(filePath);
            var tag = file.TagTypes != TagTypes.Id3v2 ? file.Tag : file.GetTag(TagTypes.Id3v2);

            var rating = tag.GetPopularimeterFrame();
            if (rating != null)
                rating.Rating = stars;
            if (performers != null)
                tag.Performers =
                    performers.Select(performer => performer.ToString()).Where(p => p != null).ToArray();
            tag.Album = album;
            if (trackNo != 0)
                tag.Track = trackNo;
            if (year != 0)
                tag.Year = year;
            tag.Comment = comment;
            tag.Genres = genres;

            return SaveToFile(file, tag);
        }

        private File SaveToFile(File file, Tag tag)
        {
            if (Id3Handler.Save(file, _file))
            {
                if (checkBoxRename.Checked)
                {
                    var filename =
                        Helper.RenameFile(new BaseInfoTag(tag.JoinedPerformers, tag.FirstPerformer, tag.Album,
                            tag.Title,
                            _file.Name));
                    if (filename != Actiontype.Exception.ToString() &&
                        filename != Actiontype.Already.ToString())
                        return File.Create(filename);
                }
                return file;
            }
            return null;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SetValues();
        }

        private void dataGridViewPerformers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                bindingSourcePerformers.Remove(bindingSourcePerformers.Current);
            }
        }

        private void textBoxYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}