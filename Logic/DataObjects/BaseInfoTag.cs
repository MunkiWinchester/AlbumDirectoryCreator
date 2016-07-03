using Logic.Business;

namespace Logic.DataObjects
{
    public class BaseInfoTag
    {
        public string JoinedPerformers { get; }
        public string FirstPerformer { get; }
        public string Album { get; }
        public string Title { get; }
        public string FileInfo { get; }
        public string NewBasePath { get; }

        public BaseInfoTag(string joinedPerformers, string firstPerformer, string album, string title, string fileInfo)
        {
            JoinedPerformers = joinedPerformers;
            FirstPerformer = firstPerformer;
            Album = album;
            Title = title;
            FileInfo = fileInfo;

            var path = "00Without Artist\\";
            var perf = firstPerformer.RemoveInvalidPathCharsAndToTitleCase();
            var alb = album.RemoveInvalidPathCharsAndToTitleCase();
            // Pfad erstellen
            if (!string.IsNullOrWhiteSpace(perf) &&
                !string.IsNullOrWhiteSpace(alb))
            {
                path =
                    $"{perf}\\{alb}\\";
            }
            else if (!string.IsNullOrWhiteSpace(perf))
            {
                path = $"{perf}\\";
            }
            NewBasePath = path;
        }

        public BaseInfoTag()
        {
        }
    }
}