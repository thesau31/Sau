using Sau.Raylan.SR5.Contracts.Interfaces;
using Sau.Raylan.SR5.Services.Combat;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sau.Raylan.SR5.Services.Tests.Combat
{
    [ExcludeFromCodeCoverage]
    public class MockInitiativePass : IInitiativePass
    {
        public bool WasNextCalled { get; set; }
        public bool WasSetupCalled { get; set; }
        public bool ShouldReturnNext { get; set; }

        public List<InitiativePassSlot> InitiativeOrder { get; set; }

        public InitiativePassSlot Next()
        {
            WasNextCalled = true;
            if (ShouldReturnNext)
                return new InitiativePassSlot();
            return null;
        }

        public void Setup(IDiceBag diceBag, IEnumerable<ICharacter> participants)
        {
            WasSetupCalled = true;
        }
    }
}
