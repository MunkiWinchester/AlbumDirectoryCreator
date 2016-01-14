using System.IO;
using System.Linq;

namespace Business
{
    public static class Helper
    {
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
    }
}