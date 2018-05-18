using Sau.Raylan.SR5.Contracts.Interfaces;

namespace Sau.Raylan.SR5.Services.Actions.Initiative
{
    public class InitiativeAction : Action, IInitiativeAction
    {
        public InitiativeCost InitiativeCost { get; private set; }
  
        internal InitiativeAction(InitiativeActionInput input)
            : base(input)
        {
            InitiativeCost = new InitiativeCost() { Cost = input.Cost, IsCostRequired = input.IsCostRequired };
        }
    }
}
