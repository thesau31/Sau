using Sau.Raylan.SR5.Contracts.Interfaces;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Services.Combat
{
    public interface IInitiativePass
    {
        List<InitiativePassSlot> InitiativeOrder { get; }

        bool IsComplete { get; }

        void Setup(IDiceBag diceBag, IEnumerable<ICharacter> participants);

        void Reset();

        InitiativePassSlot Next();
    }
}