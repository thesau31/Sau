using System;

namespace Sau.Raylan.SR5.Services.Actions
{
    public class ActionResult
    {
        public string RollNotation { get; private set; }
        public DicePoolResults DiceResults { get; private set; }

        public ActionResult(string rollNotation, DicePoolResults diceResults)
        {
            if (rollNotation == null) throw new ArgumentNullException("rollNotation");
            if (diceResults == null) throw new ArgumentNullException("diceResults");

            RollNotation = rollNotation;
            DiceResults = diceResults;
        }
    }
}
