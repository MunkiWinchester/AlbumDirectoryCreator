using Logic.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TagLib;
using TagLib.Asf;
using TagLib.Id3v2;
using Tag = TagLib.Tag;

namespace Logic.Business
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
                    return newList.All(item => oldList.All(item.Equals));
                }
            }
            return EqualityComparer<T>.Default.Equals(newVal, oldVal);
        }

        public static string ToSeperatedString(this IEnumerable<string> iEnumerable)
        {
            var list = iEnumerable.ToList();
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
        /// Verwurstet eine Collection vom Typ IEnumerable (also Liste, IEnumerable, ArrayList what ever... zu einer DataTable
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

        // ReSharper disable once UnusedParameter.Global
        public static byte SetRating(this Tag tag, Stars stars)
        {
            return (byte)stars;
        }

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