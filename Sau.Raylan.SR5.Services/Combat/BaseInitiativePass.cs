using System;
using System.Collections.Generic;
using System.Linq;
using Sau.Raylan.SR5.Contracts.Interfaces;

namespace Sau.Raylan.SR5.Services.Combat
{
    public abstract class BaseInitiativePass : IInitiativePass
    {
        #region Implementations
        protected readonly Func<InitiativePassSlot, bool> _leftToAct = (x => !x.HasActed && x.CurrentInitiative > 0);

        public List<InitiativePassSlot> InitiativeOrder { get; protected set; }

        public bool IsComplete { get { return InitiativeOrder.Count(_leftToAct) == 0; } }

        public bool NeedsAnotherPass { get { return InitiativeOrder.Any(x => x.CurrentInitiative > 0); } }

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
        #endregion

        #region Abstracts
        public abstract InitiativePassSlot Next();

        public abstract void Reset();
        #endregion
    }
}
