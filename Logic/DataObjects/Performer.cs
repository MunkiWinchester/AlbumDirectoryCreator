namespace Logic.DataObjects
{
    public class Performer
    {
        // Don't delete this!!!
        // Don't delete this!!!
        // Don't delete this!!!
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public string Perfomer { get; set; }

        public Performer(string name)
        {
            Perfomer = name;
        }

        // Don't delete this!!!
        // Don't delete this!!!
        // Don't delete this!!!
        // ReSharper disable once UnusedMember.Global
        public Performer()
        {
        }

        public override string ToString()
        {
            return Perfomer;
        }
    }
}