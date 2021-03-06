﻿using System.Linq;

namespace Sau.Raylan.SR5.Services.Combat
{
    public class InitiativePass : BaseInitiativePass
    {
        public override InitiativePassSlot Next()
        {
            InitiativeOrder.Sort();
            return InitiativeOrder.FirstOrDefault(_leftToAct);
        }

        public override void Reset()
        {
            InitiativeOrder.ForEach(x => x.CurrentInitiative -= 10);
            InitiativeOrder.ForEach(x => x.HasActed = false);
            InitiativeOrder = InitiativeOrder.Where(_leftToAct).ToList();
        }
    }
}
