namespace Sau.Raylan.SR5.Contracts.Actions.Initiative
{
    public class ParryAction : IInitiativeAction
    {
        InitiativeCost _initiativeCost;
        public InitiativeCost InitiativeCost { get { return _initiativeCost; } }

        public ParryAction()
        {
            _initiativeCost = new InitiativeCost() { Cost = 5, IsCostRequired = true };
        }
    }
}
