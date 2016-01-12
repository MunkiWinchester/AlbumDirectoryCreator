using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Business
{
    public static class Extensions
    {
        private static readonly char[] InvalidChars = Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).ToArray();

        public static string RemoveInvalidPathCharsAndToTitleCase(this string s)
        {
            var temp = s.Split(InvalidChars, StringSplitOptions.RemoveEmptyEntries);
            var temp1 = new string[temp.Length];
            for (var index = 0; index < temp.Length; index++)
            {
                var s1 = temp[index];
                if (index != temp.Length - 1)
                    temp1[index] = string.Format("{0} ", s1.Trim());
                else
                    temp1[index] = s1.Trim();
            }
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Join("", temp1));
        }

        public static KeyValuePair<int, string> GetPerformerAlbumHashKvP(this TagLib.File taglibFile)
        {
            if (taglibFile?.Tag?.FirstPerformer != null && taglibFile.Tag.Album != null)
            {
                var name = string.Format("{0} / {1}", taglibFile.Tag.FirstPerformer, taglibFile.Tag.Album);
                var hash = name.GetHashCode();
                return new KeyValuePair<int, string>(hash, name);
            }
            return new KeyValuePair<int, string>(0, string.Empty);
        }

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj);
        }

        public static bool Compare<T>(T newVal, T oldVal)
        {
            if (typeof(T) == typeof(string[]))
            {
                var newList = (newVal as string[])?.ToList();
                var oldList = (oldVal as string[])?.ToList();

                if (newList != null && oldList != null)
                {
                    if (newList.Count > oldList.Count)
                    {
                        return false;
                    }
                    if (newList.Count < oldList.Count)
                    {
                        return false;
                    }
                    return newList.All(item => oldList.Any(item.Equals));
                }
            }
            return EqualityComparer<T>.Default.Equals(newVal, oldVal);
        }

        public static string ToSeperatedString(this IEnumerable<string> iEnumerable)
        {
            var returnString = new StringBuilder();
            foreach (var item in iEnumerable)
            {
                returnString.Append($"\"{item}\", ");
            }
            return returnString.ToString().TrimEnd(',', ' ');
        }

        public static string ToNormalizedString(this string[] stringArray)
        {
            var returnString = new StringBuilder();
            foreach (var item in stringArray)
            {
                returnString.Append($"{item}, ");
            }
            return returnString.ToString().TrimEnd(',', ' ');
        }
    }
}