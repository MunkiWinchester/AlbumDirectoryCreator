namespace Logic.DataObjects
{
    public class Performer
    {
        // ReSharper disable once MemberCanBePrivate.Global
        // Changes here a bad, m'kay
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public string Name { get; set; }

        public Performer(string name)
        {
            Name = name;
        }

        // ReSharper disable once UnusedMember.Global
        // Don't delete this!!!
        public Performer()
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
}