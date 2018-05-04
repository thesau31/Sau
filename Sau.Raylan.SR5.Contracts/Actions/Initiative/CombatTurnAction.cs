namespace Sau.Raylan.SR5.Contracts.Actions.Initiative
{
    public class CombatTurnAction : IInitiativeAction
    {
        InitiativeCost _initiativeCost;
        public InitiativeCost InitiativeCost { get { return _initiativeCost; } }

        public CombatTurnAction()
        {
            _initiativeCost = new InitiativeCost() { Cost = 10, IsCostRequired = false };
        }
    }
}
