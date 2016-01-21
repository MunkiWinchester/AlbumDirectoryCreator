using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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

                var compareValuesAndLog = new ActionBlock<string>(property =>
                {
                    LogDifferences(id3.Tag.GetPropertyValue(property),
                        oldId3.Tag.GetPropertyValue(property), property);
                },
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                });

                Parallel.ForEach(Properties, property =>
                {
                    compareValuesAndLog.Post(property);
                });

                compareValuesAndLog.Complete();
                compareValuesAndLog.Completion.Wait();

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
                if (newVal != null && oldVal != null && newVal.GetType() == typeof(string[]))
                {
                    Logger.Info($"{property} " +
                                $"from {(oldVal as string[])?.ToList().ToSeperatedString()} " +
                                $"to {(newVal as string[])?.ToList().ToSeperatedString()}");
                }
                else
                {
                    Logger.Info($"{property} from \"{oldVal}\" to \"{newVal}\"");
                }
            }
        }
    }
}