using System.Collections.Generic;
using Sau.Raylan.SR5.Contracts;

namespace Sau.Raylan.SR5.Services.Actions.Initiative
{
    public class InitiativeAction : IInitiativeAction
    {
        public string Name { get; private set; }
        public InitiativeCost InitiativeCost { get; private set; }
        public List<AttributeType> AttributesUsed { get; private set; }

        #region c'tor
        private InitiativeAction()
        {
            AttributesUsed = new List<AttributeType>();
        }

        internal InitiativeAction(InitiativeActionInput input)
            : this()
        {
            Name = input.Name;
            InitiativeCost = new InitiativeCost() { Cost = input.Cost, IsCostRequired = input.IsCostRequired };
            if (input.AttributesUsed != null)
                AttributesUsed = input.AttributesUsed;
        }
        #endregion

        public ActionResult Do(Character source, IDiceBag bag)
        {
            var times = 0;
            AttributesUsed.ForEach(x => times += source.Attributes[x]);

            // todo: skill(s)

            var pool = new DicePool(bag, times);
            var results = pool.Roll(); // todo: limits
            var rollNotation = ""; // todo: roll notation

            return new ActionResult(rollNotation, results);
        }
    }
}
