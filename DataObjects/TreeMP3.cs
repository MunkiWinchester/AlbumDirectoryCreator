namespace DataObjects
{
    public class TreeMp3
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }

        public TreeMp3(int id, int parentId, string artist, string album, string title, string path)
        {
            Id = id;
            ParentId = parentId;
            Artist = artist;
            Album = album;
            Title = title;
            Path = path;
        }
    }
}