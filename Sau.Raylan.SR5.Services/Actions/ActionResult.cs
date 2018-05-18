using System;

namespace Sau.Raylan.SR5.Services.Actions
{
    public class ActionResult
    {
        public string RollNotation { get; private set; }
        public DicePoolResults DiceResults { get; private set; }

        public ActionResult(string rollNotation, DicePoolResults diceResults)
        {
            RollNotation = rollNotation ?? throw new ArgumentNullException("rollNotation");
            DiceResults = diceResults ?? throw new ArgumentNullException("diceResults");
        }
    }
}
