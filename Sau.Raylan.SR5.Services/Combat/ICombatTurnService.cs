using Sau.Raylan.SR5.Contracts.Interfaces;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Services.Combat
{
    public interface ICombatTurnService
    {
        void Setup(List<ICharacter> participants);

        InitiativePassSlot Next();
    }
}