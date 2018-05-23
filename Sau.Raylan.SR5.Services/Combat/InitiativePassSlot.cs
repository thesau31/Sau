using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Contracts.Interfaces;
using Sau.Raylan.SR5.Services.Actions.Initiative;
using System;

namespace Sau.Raylan.SR5.Services.Combat
{
    public class InitiativePassSlot : IComparable<InitiativePassSlot>
    {
        public int CurrentInitiative { get; set; }
        public bool HasActed { get; set; }
        public ICharacter Participant { get; set; }

        public void PerformAction(IDiceBag diceBag, IInitiativeAction action)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (diceBag == null) throw new ArgumentNullException("diceBag");

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

        public int RollInitiative(IDiceBag diceBag)
        {
            if (diceBag == null) throw new ArgumentNullException("diceBag");

            return
                Participant.Attributes[AttributeType.Intuition] +
                Participant.Attributes[AttributeType.Reaction] +
                new DicePool(diceBag, Participant.InitiativeDicePool).Roll().Total;
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