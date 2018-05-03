namespace Sau.Raylan.SR5.Contracts.Actions
{
    public class CombatAction : ICharacterAction
    {
        public void Run(Character character)
        {
            character.Initiative.Use(10);
        }
    }
}
