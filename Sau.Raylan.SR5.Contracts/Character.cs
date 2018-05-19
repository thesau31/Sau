using Sau.Raylan.SR5.Contracts.Interfaces;

namespace Sau.Raylan.SR5.Contracts
{
    public class Character : ICharacter
    {
        public IAttributePool Attributes { get; set; }
        public ISkillPool Skills { get; set; }
        public int InitiativeDicePool { get { return 1; } }
    }
}
