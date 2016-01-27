using Logging;
using Logic.DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagLib;
using File = System.IO.File;

namespace Logic.Business
{
    public static class Helper
    {
        private static readonly Logger Logger = new Logger(LoggingType.Business);

        public static void DeleteEmptyFolders(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                DeleteEmptyFolders(directory);
                var files = Directory.GetFiles(directory).ToList();
                var hasSubdirectories = Directory.GetDirectories(directory).Any();

                if (!hasSubdirectories)
                {
                    var canBeDeleted =
                        files.Select(File.GetAttributes)
                            .All(attributes => (attributes & FileAttributes.Hidden) == FileAttributes.Hidden);

                    if (canBeDeleted)
                    {
                        Directory.Delete(directory, true);
                    }
                }
            }
        }

        public static byte SetRating(Stars stars)
        {
            return (byte)stars;
        }

        public static bool MoveFile(BaseInfoTag baseInfoTag, string basePath)
        {
            try
            {
                var path = $"{basePath}\\{baseInfoTag.NewBasePath}";
                Directory.CreateDirectory(path);
                var oldFileInfo = new FileInfo(baseInfoTag.FileInfo);

                // falls die Datei schon existiert
                var newFileName = $"{baseInfoTag.JoinedPerformers} - {baseInfoTag.Title}";
                var newFileInfo = new FileInfo(Path.Combine(path, $"{newFileName}{oldFileInfo.Extension}"));
                if (newFileInfo != oldFileInfo)
                {
                    var counter = 1;
                    while (newFileInfo.Exists)
                    {
                        var tempFileName = $"{newFileName} ({counter++})";
                        newFileInfo = new FileInfo(Path.Combine(path, $"{tempFileName}{oldFileInfo.Extension}"));
                    }
                    // Datei in neue Struktur kopieren
                    oldFileInfo.MoveTo(newFileInfo.FullName);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message} -> \"{baseInfoTag.FileInfo}\"", ex);
                return false;
            }
        }

        public static BaseInfoTag ReadMetaDatas(string fileInfo)
        {
            try
            {
                // Auslesen
                var file = TagLib.File.Create(fileInfo);
                var tag = file.TagTypes != TagTypes.Id3v2 ? file.Tag : file.GetTag(TagTypes.Id3v2);

                return new BaseInfoTag(tag.JoinedPerformers, tag.FirstPerformer,
                    tag.Album, tag.Title, fileInfo);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message} -> \"{fileInfo}\"", ex);
                return null;
            }
        }

        public static float CalculatePercentage(int total, int successfully, int unsuccessfully)
        {
            return (float)successfully / (total - unsuccessfully) * 100;
        }

        public static Id3MultiEditHelp GetTagsAndIntersectionFields(List<string> fileInfos)
        {
            var id3MultiEditHelp = new Id3MultiEditHelp();
            var multiValues = "(multiple Values)";
            foreach (var fileInfo in fileInfos)
            {
                var file = TagLib.File.Create(fileInfo);
                var tag = file.TagTypes != TagTypes.Id3v2 ? file.Tag : file.GetTag(TagTypes.Id3v2);
                id3MultiEditHelp.TagList.Add(fileInfo, tag);
            }
            var performers =
                id3MultiEditHelp.TagList.Values.SelectMany(tag => tag.Performers)
                    .ToList()
                    .GroupBy(x => x)
                    .Select(g => new KeyValuePair<int, string>(g.Count(), g.Key))
                    .ToList();
            var albums =
                id3MultiEditHelp.TagList.Values.GroupBy(i => i.Album)
                    .Select(g => new KeyValuePair<int, string>(g.Count(), g.First().Album))
                    .ToList();
            var years =
                id3MultiEditHelp.TagList.Values.GroupBy(i => i.Year)
                    .Select(g => new KeyValuePair<int, uint?>(g.Count(), g.First().Year))
                    .ToList();
            var genres =
                id3MultiEditHelp.TagList.Values.GroupBy(i => i.JoinedGenres)
                    .Select(g => new KeyValuePair<int, string>(g.Count(), g.First().JoinedGenres))
                    .ToList();
            var comments =
                id3MultiEditHelp.TagList.Values.GroupBy(i => i.Comment)
                    .Select(g => new KeyValuePair<int, string>(g.Count(), g.First().Comment))
                    .ToList();
            var stars =
                id3MultiEditHelp.TagList.Values.GroupBy(i => i.GetPopularimeterFrame()?.Rating.ToStars())
                    .Select(
                        g =>
                            new KeyValuePair<int, Stars?>(g.Count(), g.First().GetPopularimeterFrame()?.Rating.ToStars()))
                    .ToList();

            id3MultiEditHelp.Performers =
                performers.Max(k => k.Key) == fileInfos.Count
                    ? (from y in performers where y.Key.Equals(performers.Max(x => x.Key)) select new Performer(y.Value))
                        .ToList()
                    : new List<Performer> { new Performer(multiValues) };
            id3MultiEditHelp.Albums =
                albums.Max(k => k.Key) == fileInfos.Count
                    ? (from y in albums where y.Key.Equals(albums.Max(x => x.Key)) select y.Value).ToArray()
                    : new[] { multiValues };
            if (genres.Max(k => k.Key) == fileInfos.Count)
                id3MultiEditHelp.Genres = (from y in genres where y.Key.Equals(genres.Max(x => x.Key)) select y.Value).ToArray();
            id3MultiEditHelp.Year = years.First(y => y.Key.Equals(years.Max(x => x.Key))).Value;
            id3MultiEditHelp.Comment = comments.First(y => y.Key.Equals(comments.Max(x => x.Key))).Value;
            id3MultiEditHelp.Rating = stars.First(y => y.Key.Equals(stars.Max(x => x.Key))).Value;
            return id3MultiEditHelp;
        }
    }
}