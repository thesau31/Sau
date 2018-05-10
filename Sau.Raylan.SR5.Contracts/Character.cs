namespace Sau.Raylan.SR5.Contracts
{
    public class Character
    {
        //public int Body { get; set; }
        //public int Agility { get; set; }
        public int Reaction { get; set; }
        //public int Strength { get; set; }
        //public int Will { get; set; }
        //public int Logic { get; set; }
        public int Intuition { get; set; }
        //public int Charisma { get; set; }

        public int InitiativeDicePool { get { return 1; } }
    }
}
