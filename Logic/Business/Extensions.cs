using Logic.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TagLib;
using TagLib.Id3v2;
using Tag = TagLib.Tag;

namespace Logic.Business
{
    public static class Extensions
    {
        private static readonly char[] InvalidChars = Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).ToArray();

        /// <summary>
        /// Removes invalid chars from a string and puts it in title case
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveInvalidPathCharsAndToTitleCase(this string s)
        {
            var myTi = new CultureInfo("en-US", false).TextInfo;
            if (string.IsNullOrWhiteSpace(s))
                return s;
            s = System.Text.RegularExpressions.Regex.Replace(s, @"\s+", " ");
            if (s.IndexOfAny(InvalidChars) != -1)
            {
                var temp = s.Split(InvalidChars, StringSplitOptions.RemoveEmptyEntries);
                var temp1 = new string[temp.Length];
                for (var index = 0; index < temp.Length; index++)
                {
                    var s1 = temp[index];
                    if (index != temp.Length - 1)
                        temp1[index] = $"{s1.Trim()} ";
                    else
                        temp1[index] = s1.Trim();
                }
                var sNew =
                    System.Text.RegularExpressions.Regex.Replace((string.Join(" ", temp1)), @"\s+", " ");
                return
                    myTi.ToTitleCase(sNew);
            }
            return myTi.ToTitleCase(s);
        }

        /// <summary>
        /// Returns the property from a property by searching with its name
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj);
        }

        /// <summary>
        /// Compares two values with each other
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newVal">First value</param>
        /// <param name="oldVal">Second value</param>
        /// <returns>Bool if they're equal</returns>
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
                    return newList.All(item => oldList.All(item.Equals));
                }
            }
            return EqualityComparer<T>.Default.Equals(newVal, oldVal);
        }

        /// <summary>
        /// Returns a string with all values of a collection of type IEnumerable
        /// Seperates the single values with a comma and puts them in quotation marks
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>"Value 1", "Value 2", ...</returns>
        public static string ToSeperatedString(this IEnumerable<string> collection)
        {
            var list = collection.ToList();
            if (!list.Any())
                return "\"\"";

            var returnString = new StringBuilder();
            foreach (var item in list)
            {
                returnString.Append($"\"{item}\", ");
            }
            return returnString.ToString().TrimEnd(',', ' ');
        }

        /// <summary>
        /// Converts a collection of type IEnumerable to a dataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            var dataTable = new DataTable();

            var sourceItems = collection as IList<T> ?? collection.ToList();

            if (sourceItems.Count > 0)
            {
                foreach (var property in sourceItems.First().GetType().GetProperties())
                {
                    dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                }

                foreach (var sourceItem in sourceItems)
                {
                    var newDataTableRow = dataTable.NewRow();
                    foreach (var propertyInfo in sourceItem.GetType().GetProperties())
                    {
                        var value = sourceItem.GetType().GetProperty(propertyInfo.Name).GetValue(sourceItem, null);
                        if (value == null)
                            newDataTableRow[propertyInfo.Name] = DBNull.Value;
                        else
                        {
                            newDataTableRow[propertyInfo.Name] = value;
                        }
                    }
                    dataTable.Rows.Add(newDataTableRow);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Gets the Rating ("stars") of a given tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static PopularimeterFrame GetPopularimeterFrame(this Tag tag)
        {
            if (tag.TagTypes != TagTypes.Id3v2)
                return null;

            TagLib.Id3v2.Tag.DefaultVersion = 3;
            TagLib.Id3v2.Tag.ForceDefaultVersion = true;

            return PopularimeterFrame.Get((TagLib.Id3v2.Tag)tag,
                "Windows Media Player 9 Series",
                true);
        }

        /// <summary>
        /// Changes a byte value into the amount of stars (enum)
        /// </summary>
        /// <param name="value">Byte value of the "stars"</param>
        /// <returns>The amount of stars (enum)</returns>
        public static Stars ToStars(this byte value)
        {
            switch (value)
            {
                case (byte)Stars.Zero:
                    return Stars.Zero;

                case (byte)Stars.One:
                    return Stars.One;

                case (byte)Stars.Two:
                    return Stars.Two;

                case (byte)Stars.Three:
                    return Stars.Three;

                case (byte)Stars.Four:
                    return Stars.Four;

                case (byte)Stars.Five:
                    return Stars.Five;
            }
            return Stars.Zero;
        }
    }
}