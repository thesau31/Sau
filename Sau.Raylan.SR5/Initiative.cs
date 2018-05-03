using System;

namespace Sau.Raylan.SR5
{
    public class Initiative
    {
        private int _value = 0;

        public int Get() { return _value; }

        public void Use(int value)
        {
            this.Use(value, false);
        }

        public void Use(int value, bool isAmountRequired)
        {
            if (isAmountRequired && value > _value)
                throw new InvalidOperationException("You may not use more Initiative than you have when isAmountRequired is true");

            if (value > _value)
                _value = 0;
            else
                _value -= value;
        }
    }
}
