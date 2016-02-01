using Logging;
using Logic.DataObjects;
using System.Collections.Generic;
using System.Linq;
using TagLib;

namespace Logic.Business
{
    public static class Id3Handler
    {
        private static readonly Logger Logger = new Logger(LoggingType.Business);

        private static readonly List<string> Properties = new List<string>
        {
            "Album",
            "Title",
            "Track",
            "Year",
            "Comment",
            "Performers",
            "Genres"
        };

        public static bool Save(File id3, File oldId3)
        {
            try
            {
                Logger.Info("---------------");
                Logger.Info($"Changing values for \"{id3.Name}\"");
                var newTags = id3.TagTypes != TagTypes.Id3v2 ? id3.Tag : id3.GetTag(TagTypes.Id3v2);
                var oldTags = oldId3.TagTypes != TagTypes.Id3v2 ? oldId3.Tag : oldId3.GetTag(TagTypes.Id3v2);
                foreach (var property in Properties)
                {
                    if (property.Equals("Performers") || property.Equals("Genres"))
                        LogDifferences(newTags.GetPropertyValue($"Joined{property}"),
                            oldTags.GetPropertyValue($"Joined{property}"), property, newTags.GetPropertyValue(property),
                            oldTags.GetPropertyValue(property));
                    else
                        LogDifferences(newTags.GetPropertyValue(property),
                            oldTags.GetPropertyValue(property), property);
                }
                if (id3.TagTypes == TagTypes.Id3v2)
                    LogDifferences(newTags.GetPopularimeterFrame().Rating.ToStars(), oldTags.GetPopularimeterFrame().Rating.ToStars(), "Rating");
                //TODO: sauberer
                id3.Tag.Comment = $" - {oldId3.Name}";

                id3.Save();
                Logger.Info("---------------");
                return true;
            }
            catch (CorruptFileException ex)
            {
                Logger.Error($"An exception occured while changing values for \"{id3.Name}\"", ex);
                return false;
            }
        }

        private static void LogDifferences(object newVal, object oldVal, string property)
        {
            if (!Extensions.Compare(newVal, oldVal))
            {
                Logger.Info($"{property} from \"{oldVal}\" to \"{newVal}\"");
            }
        }

        private static void LogDifferences(object newVal, object oldVal, string property, object newValArray,
            object oldValArray)
        {
            if (!Extensions.Compare(newVal, oldVal))
            {
                if (newValArray != null && oldValArray != null && newValArray.GetType() == typeof(string[]) &&
                    oldValArray.GetType() == typeof(string[]))
                {
                    Logger.Info($"{property} " +
                                $"from {((string[])oldValArray).ToSeperatedString()} " +
                                $"to {((string[])newValArray).ToSeperatedString()}");
                }
            }
        }

        public static Id3MultiEditHelp GetTagsAndIntersectionFields(List<string> fileInfos)
        {
            var id3MultiEditHelp = new Id3MultiEditHelp();
            const string multiValues = "(multiple Values)";
            foreach (var fileInfo in fileInfos)
            {
                var file = File.Create(fileInfo);
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