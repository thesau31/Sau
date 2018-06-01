using Sau.Raylan.SR5.Contracts.Interfaces;
using System;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Services.Combat
{
    public class CombatTurnService<T> : ICombatTurnService
        where T : IInitiativePass, new()
    {
        private readonly IDiceBag _diceBag;

        public T CurrentInitiativePass { get; private set; }

        #region c'tor
        public CombatTurnService(IDiceBag diceBag)
        {
            _diceBag = diceBag ?? throw new ArgumentNullException("diceBag");
        }
        #endregion

        public void Setup(List<ICharacter> participants)
        {
            if (participants == null) throw new ArgumentNullException("participants");

            CurrentInitiativePass = new InitiativePassFactory(_diceBag, participants).Create<T>();
        }

        public InitiativePassSlot Next()
        {
            if (CurrentInitiativePass == null) throw new InvalidOperationException("You must run Setup before calling Next()");

            if (CurrentInitiativePass.IsComplete)
            {
                if (CurrentInitiativePass.NeedsAnotherPass)
                    CurrentInitiativePass.Reset();
                else
                    return null;
            }

            return CurrentInitiativePass.Next();
        }
    }
}
