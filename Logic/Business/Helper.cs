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
        private static readonly Logger Logger = new Logger();

        /// <summary>
        /// Read metadata from a file info
        /// </summary>
        /// <param name="fileInfo">File info of the file, of which the metadata should be read</param>
        /// <returns>The baseinfotag</returns>
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

        /// <summary>
        /// Moves the file and renames it to the new structure
        /// </summary>
        /// <param name="baseInfoTag">Metadatas of the file</param>
        /// <param name="basePath">Path of the "music" folder</param>
        /// <returns>Success or not</returns>
        public static bool MoveFile(BaseInfoTag baseInfoTag, string basePath)
        {
            try
            {
                var path = $"{basePath}\\{baseInfoTag.NewBasePath}";
                Directory.CreateDirectory(path);
                var oldFileInfo = new FileInfo(baseInfoTag.FileInfo);

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
                Logger.Info(
                    $"Moving \"{oldFileInfo.FullName}\"\r\n                                  to \"{newFileInfo.FullName}\"");
                oldFileInfo.MoveTo(newFileInfo.FullName);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message} -> \"{baseInfoTag.FileInfo}\"", ex);
                return false;
            }
        }

        /// <summary>
        /// Renames the file to the new structure
        /// </summary>
        /// <param name="baseInfoTag">Metadatas of the file</param>
        /// <returns>The new full path of the file</returns>
        public static string RenameFile(BaseInfoTag baseInfoTag)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(baseInfoTag.JoinedPerformers) &&
                    !string.IsNullOrWhiteSpace(baseInfoTag.Title))
                {
                    var oldFileInfo = new FileInfo(baseInfoTag.FileInfo);
                    var path = oldFileInfo.DirectoryName;
                    if (path != null)
                    {
                        var newFileName = $"{baseInfoTag.JoinedPerformers} - {baseInfoTag.Title}";
                        newFileName = newFileName.RemoveInvalidPathCharsAndToTitleCase().Trim();

                        var newFileInfo =
                            new FileInfo(Path.Combine(path,
                                $"{newFileName}{oldFileInfo.Extension.ToLower()}"));

                        if (!newFileInfo.FullName.ToLower().Equals(oldFileInfo.FullName.ToLower()))
                        {
                            var counter = 1;
                            var isTheSame = false;
                            while (newFileInfo.Exists)
                            {
                                isTheSame = newFileInfo.FullName.ToLower().Equals(oldFileInfo.FullName.ToLower());
                                if (isTheSame)
                                    break;

                                var tempFileName = $"{newFileName} ({counter++})";
                                newFileInfo =
                                    new FileInfo(Path.Combine(path,
                                        $"{tempFileName}{oldFileInfo.Extension.ToLower()}"));
                            }
                            if (!isTheSame)
                            {
                                // Datei umbenennen
                                Logger.Info(
                                    $"Renaming \"{oldFileInfo.FullName}\"\r\n                                    to \"{newFileInfo.FullName}\"");
                                File.Move(baseInfoTag.FileInfo, newFileInfo.FullName);
                                return newFileInfo.FullName;
                            }
                        }
                    }
                }
                return Actiontype.Already.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message} -> \"{baseInfoTag.FileInfo}\"", ex);
                return Actiontype.Exception.ToString();
            }
        }

        /// <summary>
        /// Get all files of the given folder which match the extensions
        /// </summary>
        /// <param name="folder">Folder where the files should be read in (recursiv)</param>
        /// <param name="extensions">Extension of files which should be read in</param>
        /// <returns>List with the paths of the files</returns>
        public static List<string> GetAllFiles(string folder, List<string> extensions)
        {
            var files = (from file in Directory.GetFiles(folder)
                         let attributes = File.GetAttributes(file)
                         where
                             (attributes & FileAttributes.Hidden) != FileAttributes.Hidden &&
                             extensions.Contains(file.Split('.').Last().ToLower())
                         select file).ToList();

            try
            {
                foreach (var subDir in Directory.GetDirectories(folder))
                {
                    files.AddRange(GetAllFiles(subDir, extensions));
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message} -> \"{folder}\"", ex);
            }
            return files;
        }

        /// <summary>
        /// Deletes empty folders (recursiv)
        /// </summary>
        /// <param name="startLocation">Folder in which the sub folders are checked</param>
        public static void DeleteEmptyFolders(string startLocation)
        {
            try
            {
                var directories = Directory.GetDirectories(startLocation);
                foreach (var directory in directories)
                {
                    DeleteEmptyFolders(directory);
                }
                if (!Directory.GetDirectories(startLocation).Any())
                    DeleteFolderIfEmpty(startLocation);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message} -> \"{startLocation}\"", ex);
            }
        }

        /// <summary>
        /// Checks if the folder is empty (just hidden files equal empty) and deletes them if empty
        /// </summary>
        /// <param name="directory">Folder which should be checked</param>
        private static void DeleteFolderIfEmpty(string directory)
        {
            var files = Directory.GetFiles(directory).ToList();
            var canBeDeleted =
                files.Select(File.GetAttributes)
                    .All(attributes => (attributes & FileAttributes.Hidden) == FileAttributes.Hidden);

            if (canBeDeleted)
            {
                try
                {
                    Directory.Delete(directory, true);
                }
                catch (Exception ex)
                {
                    Logger.Error($"{ex.Message} -> \"{directory}\"", ex);
                }
            }
        }

        /// <summary>
        /// Calculates how many percent where successfull
        /// </summary>
        /// <param name="successfully">Count of successfull items</param>
        /// <param name="total">Total count of items</param>
        /// <returns>Success percentage</returns>
        public static float CalculatePercentage(int successfully, int total)
        {
            var number = (float)successfully / total;
            return number * 100;
        }
    }
}