using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Services.Actions.Initiative;
using System;

namespace Sau.Raylan.SR5.Services
{
    public class InitiativePassSlot : IComparable<InitiativePassSlot>
    {
        public int CurrentInitiative { get; set; }
        public bool HasActed { get; set; }
        public Character Participant { get; set; }

        public void PerformAction(IInitiativeAction action, IDiceBag diceBag)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (diceBag == null) throw new ArgumentNullException("bag");

            if (CurrentInitiative <= 0)
                throw new InvalidOperationException("You may not perform an action with 0 initiative.");
            if (action.InitiativeCost.Cost > CurrentInitiative && action.InitiativeCost.IsCostRequired)
                throw new InvalidOperationException("You must have enough initiative remaining to call PerformAction() when the cost is required.");

            if (CurrentInitiative >= action.InitiativeCost.Cost)
                CurrentInitiative -= action.InitiativeCost.Cost;
            else
                CurrentInitiative = 0;

            action.Do(diceBag, Participant);
        }

        #region IComparable<T>
        public int CompareTo(InitiativePassSlot other)
        {
            if (other == null) throw new ArgumentNullException("other");

            if (HasActed == other.HasActed)
            {
                if (CurrentInitiative > other.CurrentInitiative) return -1;
                if (CurrentInitiative < other.CurrentInitiative) return 1;
                return 0;
            }
            else
                return HasActed.CompareTo(other.HasActed);
        } 
        #endregion
    }
}