using System.Collections.Generic;

namespace Sau.Raylan.SR5.Contracts
{
    public class Character
    {
        public AttributePool Attributes { get; set; }

        public int InitiativeDicePool { get { return 1; } }
    }
}
