using System;
using System.Collections.Generic;

namespace Sau.Raylan
{
    public class DiceBag
    {
        private Random _random;

        #region c'tor
        public DiceBag()
        {
            _random = new Random();
        }

        public DiceBag(int seed)
        {
            _random = new Random(seed);
        }
        #endregion

        #region d6
        public virtual int d6()
        {
            return roll(6);
        }

        public virtual IEnumerable<int> d6(int times)
        {
            for (int i = 0; i < times; i++)
                yield return roll(6);
        } 
        #endregion

        private int roll(int sides)
        {
            return _random.Next(1, sides);
        }
    }
}
