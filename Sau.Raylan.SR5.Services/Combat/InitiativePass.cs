using System;
using System.Collections.Generic;
using System.Linq;

namespace Sau.Raylan.SR5.Services.Combat
{
    public class InitiativePass
    {
        public List<InitiativePassSlot> InitiativeOrder { get; private set; }


        // todo: adjust to take an enumerable of characters and generate the initiative pass.
        public InitiativePass(IEnumerable<InitiativePassSlot> initiativeOrder)
        {
            if (initiativeOrder == null) throw new ArgumentNullException("initiativeOrder");

            InitiativeOrder = initiativeOrder.ToList();
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
