using Sau.Raylan.SR5.Contracts;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Services.Actions
{
    internal class ActionInput
    {
        public string Name { get; set; }
        public List<AttributeType> AttributesUsed { get; set; }
        public List<SkillType> SkillsUsed { get; set; }
        public LimitType Limit { get; set; }
    }
}
