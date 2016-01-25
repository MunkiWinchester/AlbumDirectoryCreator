namespace Logic.DataObjects
{
    public class Performer
    {
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public string Perfomer { get; set; }

        public Performer(string name)
        {
            Perfomer = name;
        }

        // ReSharper disable once UnusedMember.Global
        // Don't delete this!!!
        public Performer()
        {
        }

        public override string ToString()
        {
            return Perfomer;
        }
    }
}