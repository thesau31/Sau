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

        public string Display(SkillType type)
        {
            return _skills[type].Display;
        }

        private class Skill
        {
            public int Value { get; set; }
            public string Display { get; set; }
            // todo: associated attribute
        }
    }
}
