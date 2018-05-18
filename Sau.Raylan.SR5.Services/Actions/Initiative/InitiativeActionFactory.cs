using Sau.Raylan.SR5.Contracts;
using System;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Services.Actions.Initiative
{
    public class InitiativeActionFactory
    {
        private Dictionary<InitiativeActionType, InitiativeActionInput> Inputs = new Dictionary<InitiativeActionType, InitiativeActionInput>()
        {
            { InitiativeActionType.Block, new InitiativeActionInput() {
                Name = "Block",
                Cost = 5,
                IsCostRequired = true,
                AttributesUsed = new List<AttributeType>() { AttributeType.Reaction, AttributeType.Intuition },
                SkillsUsed = new List<SkillType>() { SkillType.UnarmedCombat },
                Limit = LimitType.Physical } }
        };

        public InitiativeAction Create(InitiativeActionType actionType)
        {
            if (Inputs.ContainsKey(actionType))
                return new InitiativeAction(Inputs[actionType]);

            throw new NotImplementedException();
        }
    }

    public enum InitiativeActionType
    {
        None,
        Block,
    }
}
