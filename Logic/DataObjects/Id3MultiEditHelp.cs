using System.Collections.Generic;
using TagLib;

namespace Logic.DataObjects
{
    public class Id3MultiEditHelp
    {
        public List<Performer> Performers { get; set; }
        public string[] Albums { get; set; }
        public uint? Year { get; set; }
        public string Comment { get; set; }
        public Stars? Rating { get; set; }
        public string[] Genres { get; set; }

        // ReSharper disable once CollectionNeverQueried.Global
        public Dictionary<string, Tag> TagList { get; private set; }

        public Id3MultiEditHelp()
        {
            TagList = new Dictionary<string, Tag>();
        }
    }
}