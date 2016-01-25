using Logging;
using Logic.DataObjects;
using System;
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
                    var canBeDeleted = true;
                    foreach (var file in files)
                    {
                        var attributes = File.GetAttributes(file);
                        if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                        {
                            canBeDeleted = false;
                            break;
                        }
                    }

                    if (canBeDeleted)
                    {
                        Directory.Delete(directory, true);
                    }
                }
            }
        }

        public static bool MoveFile(TreeMp3 treeMp3, string basePath)
        {
            try
            {
                var path = $"{basePath}\\{treeMp3.NewPath}";
                Directory.CreateDirectory(path);
                var oldFileInfo = new FileInfo(treeMp3.FileInfo);

                // falls die Datei schon existiert
                var newFileName = $"{treeMp3.JoinedPerformers} - {treeMp3.Title}";
                var newFileInfo = new FileInfo(Path.Combine(path, $"{newFileName}{oldFileInfo.Extension}"));
                var counter = 1;
                while (newFileInfo.Exists)
                {
                    var tempFileName = $"{newFileName} ({counter++})";
                    newFileInfo = new FileInfo(Path.Combine(path, $"{tempFileName}{oldFileInfo.Extension}"));
                }
                // Datei in neue Struktur kopieren
                oldFileInfo.MoveTo(newFileInfo.FullName);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message} -> \"{treeMp3.FileInfo}\"", ex);
                return false;
            }
        }

        public static TreeMp3 ReadMetaDatas(string fileInfo)
        {
            try
            {
                // Auslesen
                var file = TagLib.File.Create(fileInfo);
                var tag = file.TagTypes != TagTypes.Id3v2 ? file.Tag : file.GetTag(TagTypes.Id3v2);

                return new TreeMp3(tag.JoinedPerformers, tag.FirstPerformer,
                    tag.Album, tag.Title, fileInfo);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message} -> \"{fileInfo}\"", ex);
                return null;
            }
        }
    }
}