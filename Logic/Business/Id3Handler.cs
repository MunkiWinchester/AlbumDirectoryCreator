using Logging;
using Logic.DataObjects;
using System.Collections.Generic;
using System.Linq;
using TagLib;
using File = TagLib.File;

namespace Logic.Business
{
    public static class Id3Handler
    {
        private static readonly Logger Logger = new Logger();

        /// <summary>
        /// List of the properties which are important metadata
        /// </summary>
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

        /// <summary>
        /// Saves the new/changed values to the file
        /// </summary>
        /// <param name="id3">New values to be saved</param>
        /// <param name="oldId3">Old values (needed for log file)</param>
        /// <returns>Success or not</returns>
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

        /// <summary>
        /// Logs the differences of a given property
        /// </summary>
        /// <param name="newVal">New value for the property</param>
        /// <param name="oldVal">Old value for the property</param>
        /// <param name="property">Property which is changed</param>
        private static void LogDifferences(object newVal, object oldVal, string property)
        {
            if (!Extensions.Compare(newVal, oldVal))
            {
                Logger.Info($"{property} from \"{oldVal}\" to \"{newVal}\"");
            }
        }

        /// <summary>
        /// Logs the differences of a given property
        /// </summary>
        /// <param name="newVal">New value for the property</param>
        /// <param name="oldVal">Old value for the property</param>
        /// <param name="property">Property which is changed</param>
        /// <param name="newValArray">For multi fields</param>
        /// <param name="oldValArray">For multi fields</param>
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

        /// <summary>
        /// Returns the tags and the intersection of multiple files
        /// </summary>
        /// <param name="fileInfos">List of the fileinfos</param>
        /// <returns>The multi edit help tags</returns>
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
                performers.Count == 0
                    ? new List<Performer> { new Performer(multiValues) }
                    : performers.Max(k => k.Key) == fileInfos.Count
                        ? (from y in performers where y.Key.Equals(fileInfos.Count) select new Performer(y.Value))
                        .ToList()
                        : new List<Performer> { new Performer(multiValues) };
            id3MultiEditHelp.Albums =
                albums.Count == 0
                    ? new[] { multiValues }
                    : albums.Max(k => k.Key) == fileInfos.Count
                        ? (from y in albums where y.Key.Equals(fileInfos.Count) select y.Value).ToArray()
                        : new[] { multiValues };
            id3MultiEditHelp.Genres =
                genres.Count == 0
                    ? new string[0]
                    : genres.Max(k => k.Key) == fileInfos.Count
                        ? (from y in genres where y.Key.Equals(fileInfos.Count) select y.Value).ToArray()
                        : new string[0];
            id3MultiEditHelp.Rating = stars.First(y => y.Key.Equals(fileInfos.Count)).Value;
            id3MultiEditHelp.Year = years.FirstOrDefault(y => y.Key.Equals(fileInfos.Count)).Value;
            id3MultiEditHelp.Comment = comments.FirstOrDefault(x => x.Key == fileInfos.Count).Value ?? multiValues;

            return id3MultiEditHelp;
        }
    }
}