﻿using System;

namespace Sau.Raylan.SR5
{
    public class DicePool
    {
        IDiceBag _diceBag;
        int _numberOfDice;

        #region c'tor
        public DicePool(IDiceBag diceBag, int numberOfDice)
        {
            if (numberOfDice <= 0)
                throw new ArgumentOutOfRangeException("numberOfDice", "[numberOfDice] must be greater than 0 to create a DicePool.");

            _diceBag = diceBag ?? throw new ArgumentNullException("diceBag");
            _numberOfDice = numberOfDice;
        }
        #endregion

        public DicePoolResults Roll()
        {
            return Roll(-1);
        }

        public DicePoolResults Roll(int limitToImpose)
        {
            return new DicePoolResults(_diceBag.d6(_numberOfDice), limitToImpose);
        }
    }
}
