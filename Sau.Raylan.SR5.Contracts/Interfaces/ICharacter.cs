namespace Sau.Raylan.SR5.Contracts.Interfaces
{
    public interface ICharacter : IHasAttributes, IHasSkills
    {
        int InitiativeDicePool { get; }
    }
}