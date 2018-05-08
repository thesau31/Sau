namespace Sau.Raylan.SR5.Services.Actions.Initiative
{
    public interface IInitiativeAction : IAction
    {
        InitiativeCost InitiativeCost { get; }
    }
}
