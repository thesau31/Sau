using System;

namespace Sau.Raylan.SR.Contracts.Actions
{
    public class CombatAction : ICharacterAction
    {
        public void Run(Character character)
        {
            character.Initiative.Use(10);
        }
    }
}
