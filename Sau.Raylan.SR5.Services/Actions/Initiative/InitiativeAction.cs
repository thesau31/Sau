using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Contracts.Interfaces;

namespace Sau.Raylan.SR5.Services.Actions.Initiative
{
    // todo: Create base [Action : IAction] class, and move Do() to base class

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
                AttributesUsed.AddRange(input.AttributesUsed);
        }
        #endregion

        public ActionResult Do(IDiceBag bag, IHasAttributes source)
        {
            var times = 0;
            AttributesUsed.ForEach(x => times += source.Attributes[x]);

            // todo: skill(s)

            var pool = new DicePool(bag, times);
            var results = pool.Roll(); // todo: limits
            var rollNotation = buildNotation(source);

            return new ActionResult(rollNotation, results);
        }

        private string buildNotation(IHasAttributes source)
        {
            var str = new StringBuilder();

            for (int i = 0; i < AttributesUsed.Count; i++)
            {
                if (i > 0)
                    str.Append(" + ");
                str.Append(source.Attributes.Display(AttributesUsed[i]));
            }

            // todo: roll notation - skills

            // todo: roll notation - limits

            // todo: roll notation - threshold

            return str.ToString();
        }
    }
}
