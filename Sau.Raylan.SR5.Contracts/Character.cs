namespace Sau.Raylan.SR5.Contracts
{
    public class Character
    {
        public int Body { get; set; }
        public int Agility { get; set; }
        public int Reaction { get; set; }
        public int Strength { get; set; }
        public int Will { get; set; }
        public int Logic { get; set; }
        public int Intuition { get; set; }
        public int Charisma { get; set; }

        public int InitiativeDicePool { get { return 1; } }
        public int CurrentInitiative { get; set; }

        //public void RollInitiative()
        //{
        //    CurrentInitiative = Reaction + Intuition + InitiativeDicePool.Roll().Total;
        //}

        //public void PerformAction(IInitiativeAction action)
        //{
        //    if (CurrentInitiative <= 0)
        //        throw new InvalidOperationException("You may not perform an action with 0 initiative.");
        //    if (action.InitiativeCost.Cost > CurrentInitiative && action.InitiativeCost.IsCostRequired)
        //        throw new InvalidOperationException("You must have enough initiative remaining to call PerformAction() when the cost is required.");

        //    if (CurrentInitiative >= action.InitiativeCost.Cost)
        //        CurrentInitiative -= action.InitiativeCost.Cost;
        //    else
        //        CurrentInitiative = 0;

        //    // do the action
        //}
    }
}
