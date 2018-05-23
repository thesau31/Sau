using Sau.Raylan.SR5.Contracts.Interfaces;
using System;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Services.Combat
{
    public class CombatTurnService<T>
        where T: IInitiativePass, new()
    {
        public List<ICharacter> Participants { get; private set; }
        public IInitiativePass CurrentInitiativePass { get; private set; }

        #region c'tor
        public CombatTurnService(IDiceBag diceBag, List<ICharacter> participants)
        {
            if (diceBag == null) throw new ArgumentNullException();
            Participants = participants ?? throw new ArgumentNullException();

            CurrentInitiativePass = new InitiativePassFactory(diceBag, participants).Create<T>();
        }
        #endregion

        public ICharacter Next()
        {
            throw new NotImplementedException();
        }
    }
}
