namespace Sau.Raylan.SR5.Contracts.Actions.Initiative
{
    public interface IInitiativeAction : IAction
    {
        InitiativeCost InitiativeCost { get; }
    }
}
