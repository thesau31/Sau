using Sau.Raylan.SR5.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sau.Raylan.SR5.Services.Combat
{
    public class CombatService
    {
        private readonly ICombatTurnService _combatTurnService;
        private readonly IDiceBag _diceBag;

        public CombatService(ICombatTurnService combatTurnService, IDiceBag diceBag, List<ICharacter> participants)
        {
            _combatTurnService = combatTurnService ?? throw new ArgumentNullException("combatTurnService");
            _diceBag = diceBag ?? throw new ArgumentNullException("diceBag");
        }

        public void StartCombat()
        {

        }
    }
}
