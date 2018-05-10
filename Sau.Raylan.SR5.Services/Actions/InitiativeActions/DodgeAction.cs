using System;

namespace Sau.Raylan.SR5.Services.Actions.Initiative
{
    public class DodgeAction : IInitiativeAction
    {
        InitiativeCost _initiativeCost;
        public InitiativeCost InitiativeCost { get { return _initiativeCost; } }

        public DodgeAction()
        {
            _initiativeCost = new InitiativeCost() { Cost = 5, IsCostRequired = true };
        }

        public void Do()
        {
            throw new NotImplementedException();
        }
    }
}
