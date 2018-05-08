﻿using System.Collections.Generic;
using System.Linq;

namespace Sau.Raylan.SR5.Services
{
    public class InitiativePassService
    {
        public List<InitiativePassSlot> InitiativeOrder { get; private set; }

        #region c'tor
        public InitiativePassService(IEnumerable<InitiativePassSlot> initiativeOrder)
        {
            InitiativeOrder = initiativeOrder.ToList();
            InitiativeOrder.Sort();
        } 
        #endregion

        
    }
}