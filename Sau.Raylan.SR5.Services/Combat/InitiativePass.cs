using Sau.Raylan.SR5.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sau.Raylan.SR5.Services.Combat
{
    public class InitiativePass
    {
        public List<InitiativePassSlot> InitiativeOrder { get; private set; }

        public InitiativePass(IDiceBag diceBag, IEnumerable<ICharacter> participants)
        {
            if (diceBag == null) throw new ArgumentNullException("diceBag");
            if (participants == null) throw new ArgumentNullException("participants");

            InitiativeOrder = participants.Select(x =>
                new InitiativePassSlot()
                {
                    Participant = x,
                    HasActed = false
                }).ToList();

            InitiativeOrder.ForEach(x => x.CurrentInitiative = x.RollInitiative(diceBag));

            InitiativeOrder.Sort();
        }

        public InitiativePassSlot Next()
        {
            InitiativeOrder.Sort();

            if (InitiativeOrder[0].HasActed || InitiativeOrder[0].CurrentInitiative == 0)
                return null;

            return InitiativeOrder[0];
        }
    }
}
