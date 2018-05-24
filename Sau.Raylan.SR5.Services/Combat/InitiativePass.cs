using Sau.Raylan.SR5.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sau.Raylan.SR5.Services.Combat
{
    public class InitiativePass : IInitiativePass
    {
        public List<InitiativePassSlot> InitiativeOrder { get; private set; }

        private readonly Func<InitiativePassSlot, bool> _leftToAct = (x => !x.HasActed && x.CurrentInitiative > 0);
        public bool IsComplete { get { return InitiativeOrder.Count(_leftToAct) == 0; } }

        public void Setup(IDiceBag diceBag, IEnumerable<ICharacter> participants)
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

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public InitiativePassSlot Next()
        {
            InitiativeOrder.Sort();
            return InitiativeOrder.FirstOrDefault(_leftToAct);
        }
    }
}
