using Business.DataObjects;
using Logging;
using System;
using System.IO;
using System.Linq;

namespace Business.Business
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
                var counter = 1;
                var newFileInfo = new FileInfo(Path.Combine(path, oldFileInfo.Name));
                var nameWithoutExtension = Path.GetFileNameWithoutExtension(oldFileInfo.Name);
                while (newFileInfo.Exists)
                {
                    var tempFileName = $"{nameWithoutExtension} ({counter++})";
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
                var taglibFile = TagLib.File.Create(fileInfo);
                var tagInf = taglibFile.Tag;

                if (string.IsNullOrWhiteSpace(tagInf.FirstPerformer))
                {
                    return new TreeMp3(string.Empty, tagInf.Album, tagInf.Title, fileInfo);
                }
                return new TreeMp3(tagInf.Performers.ToNormalizedString(),
                    tagInf.Album, tagInf.Title, fileInfo);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message} -> \"{fileInfo}\"", ex);
                _withException++;
                _errorHappened = true;
                return false;
            }
        }
    }
}