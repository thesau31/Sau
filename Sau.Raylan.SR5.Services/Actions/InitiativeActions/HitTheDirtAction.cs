namespace Sau.Raylan.SR5.Services.Actions.Initiative
{
    public class HitTheDirtAction : IInitiativeAction
    {
        InitiativeCost _initiativeCost;
        public InitiativeCost InitiativeCost { get { return _initiativeCost; } }

        public HitTheDirtAction()
        {
            _initiativeCost = new InitiativeCost() { Cost = 5, IsCostRequired = true };
        }
    }
}
