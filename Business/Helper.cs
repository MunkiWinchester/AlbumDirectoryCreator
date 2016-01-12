using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Business
{
    public static class Helper
    {
        public static void DeleteEmptyFolders(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                DeleteEmptyFolders(directory);
                var files = Directory.EnumerateFileSystemEntries(directory).ToList();
                var desktopIni = $"{directory}\\desktop.ini";
                var result = files.Contains(desktopIni) && files.Count == 1;

                if (!files.Any() || result)
                {
                    File.Delete(desktopIni);
                    Directory.Delete(directory, false);
                }
            }
        }
    }
}