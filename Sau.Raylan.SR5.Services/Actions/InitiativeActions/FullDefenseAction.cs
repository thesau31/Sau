namespace Sau.Raylan.SR5.Services.Actions.Initiative
{
    public class FullDefenseAction : IInitiativeAction
    {
        InitiativeCost _initiativeCost;
        public InitiativeCost InitiativeCost { get { return _initiativeCost; } }

        public FullDefenseAction()
        {
            _initiativeCost = new InitiativeCost() { Cost = 5, IsCostRequired = true };
        }
    }
}
