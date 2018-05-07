using System;
using System.Linq;

namespace Sau.Raylan.SR5
{
    public class DicePool
    {
        DiceBag _diceBag;
        int _numberOfDice;

        #region c'tor
        public DicePool(DiceBag diceBag, int numberOfDice)
        {
            if (diceBag == null)
                throw new ArgumentNullException("diceBag");
            if (numberOfDice <= 0)
                throw new ArgumentOutOfRangeException("numberOfDice", "[numberOfDice] must be greater than 0 to create a DicePool.");

            _diceBag = diceBag;
            _numberOfDice = numberOfDice;
        } 
        #endregion

        public DicePoolResults Roll()
        {   
            return new DicePoolResults(_diceBag.d6(_numberOfDice).ToList());
        }
    }
}
