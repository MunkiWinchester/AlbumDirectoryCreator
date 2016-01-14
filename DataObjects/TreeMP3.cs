using Business;
using System.Linq;

namespace DataObjects
{
    public class TreeMp3
    {
        public string Artist { get; }
        public string Album { get; }
        public string Title { get; }
        public string FileInfo { get; }

        public string NewPath { get; }

        public TreeMp3()
        {
        }

        public TreeMp3(string artist, string album, string title, string fileInfo)
        {
            Artist = artist;
            Album = album;
            Title = title;
            FileInfo = fileInfo;

            var path = "00Without Artist\\";
            // Pfad erstellen
            if (!string.IsNullOrWhiteSpace(artist) &&
                !string.IsNullOrWhiteSpace(album))
            {
                path =
                    $"{artist.RemoveInvalidPathCharsAndToTitleCase()}\\{album.RemoveInvalidPathCharsAndToTitleCase()}\\";
            }
            else if (!string.IsNullOrWhiteSpace(artist))
            {
                path = $"{artist.RemoveInvalidPathCharsAndToTitleCase()}\\";
            }
            NewPath = path;
        }
    }
}