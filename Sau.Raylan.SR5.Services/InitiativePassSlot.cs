using Sau.Raylan.SR5.Contracts;
using System;

namespace Sau.Raylan.SR5.Services
{
    public class InitiativePassSlot : IComparable<InitiativePassSlot>
    {
        public int CurrentInitiative { get; set; }
        public bool HasActed { get; set; }
        public Character Participant { get; set; }

        public int CompareTo(InitiativePassSlot other)
        {
            if (other == null) throw new ArgumentNullException();

            if (HasActed == other.HasActed)
            {
                if (CurrentInitiative > other.CurrentInitiative) return -1;
                if (CurrentInitiative < other.CurrentInitiative) return 1;
                return 0;
            }
            else
                return HasActed.CompareTo(other.HasActed);
        }
    }
}