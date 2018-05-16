namespace Sau.Raylan.SR5.Contracts.Interfaces
{
    public interface ICharacter : IHasAttributes
    {
        int InitiativeDicePool { get; }
    }
}