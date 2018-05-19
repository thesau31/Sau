using Sau.Raylan.SR5.Contracts.Interfaces;
using System;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Services.Combat
{
    public class CombatTurnService
    {
        public List<ICharacter> Participants { get; private set; }

        #region c'tor
        public CombatTurnService(IEnumerable<ICharacter> participants)
        {
            if (participants == null) throw new ArgumentNullException();
        }
        #endregion
    }
}
