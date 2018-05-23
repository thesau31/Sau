using Sau.Raylan.SR5.Contracts.Interfaces;
using System;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Services.Combat
{
    public class InitiativePassFactory
    {
        readonly IDiceBag _diceBag;
        readonly List<ICharacter> _participants;

        public InitiativePassFactory(IDiceBag diceBag, List<ICharacter> participants)
        {
            _diceBag = diceBag ?? throw new ArgumentNullException("diceBag");
            _participants = participants ?? throw new ArgumentNullException("participants");
        }

        public T Create<T>() where T : IInitiativePass, new()
        {
            var pass = new T();
            pass.Setup(_diceBag, _participants);
            return pass;
        }
    }
}
