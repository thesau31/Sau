using Sau.Raylan.SR5.Contracts;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Services.Actions.Initiative
{
    internal class InitiativeActionInput
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public bool IsCostRequired { get; set; }
        public List<AttributeType> AttributesUsed { get; set; }
        public List<SkillType> SkillsUsed { get; set; }
    }
}
