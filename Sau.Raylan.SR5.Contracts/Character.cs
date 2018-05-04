using Sau.Raylan.SR5.Contracts.Actions.Initiative;
using System;

namespace Sau.Raylan.SR5.Contracts
{
    public class Character
    {
        public int CurrentInitiative { get; set; }

        public void PerformAction(IInitiativeAction action)
        {
            if (CurrentInitiative <= 0)
                throw new InvalidOperationException("You may not perform an action with 0 initiative.");
            if (action.InitiativeCost.Cost > CurrentInitiative && action.InitiativeCost.IsCostRequired)
                throw new InvalidOperationException("You must have enough initiative remaining to call PerformAction() when the cost is required.");

            if (CurrentInitiative >= action.InitiativeCost.Cost)
                CurrentInitiative -= action.InitiativeCost.Cost;
            else
                CurrentInitiative = 0;

            // do the action
        }
    }
}
