using Logging;
using System.Collections.Generic;
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
    }
}