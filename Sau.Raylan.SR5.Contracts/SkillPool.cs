using Sau.Raylan.SR5.Contracts.Interfaces;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Contracts
{
    public class SkillPool : ISkillPool
    {
        private Dictionary<SkillType, Skill> _skills = new Dictionary<SkillType, Skill>()
        {
            { SkillType.UnarmedCombat, new Skill() }
        };

        public int this[SkillType type]
        {
            get
            {
                return _skills[type].Value;
            }
            set
            {
                _skills[type].Value = value;
            }
        }

        private class Skill
        {
            public int Value { get; set; }
            // todo: notation, associated attribute
        }
    }
}
