using Logic.Business;

namespace Logic.DataObjects
{
    public class TreeMp3
    {
        public string JoinedPerformers { get; }
        public string FirstPerformer { get; }
        public string Album { get; }
        public string Title { get; }
        public string FileInfo { get; }

        public string NewPath { get; }

        public TreeMp3()
        {
        }

        public TreeMp3(string joinedPerformers, string firstPerformer, string album, string title, string fileInfo)
        {
            JoinedPerformers = joinedPerformers;
            FirstPerformer = firstPerformer;
            Album = album;
            Title = title;
            FileInfo = fileInfo;

            var path = "00Without Artist\\";
            // Pfad erstellen
            if (!string.IsNullOrWhiteSpace(firstPerformer) &&
                !string.IsNullOrWhiteSpace(album))
            {
                path =
                    $"{firstPerformer.RemoveInvalidPathCharsAndToTitleCase()}\\{album.RemoveInvalidPathCharsAndToTitleCase()}\\";
            }
            else if (!string.IsNullOrWhiteSpace(firstPerformer))
            {
                path = $"{firstPerformer.RemoveInvalidPathCharsAndToTitleCase()}\\";
            }
            NewPath = path;
        }
    }
}