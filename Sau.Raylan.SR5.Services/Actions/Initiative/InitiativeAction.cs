using System;
using System.Collections.Generic;
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
        public List<SkillType> SkillsUsed { get; private set; }
        public LimitType Limit { get; private set; }

        #region c'tor
        private InitiativeAction()
        {
            AttributesUsed = new List<AttributeType>();
            SkillsUsed = new List<SkillType>();
        }

        internal InitiativeAction(InitiativeActionInput input)
            : this()
        {
            Name = input.Name;
            InitiativeCost = new InitiativeCost() { Cost = input.Cost, IsCostRequired = input.IsCostRequired };
            if (input.AttributesUsed != null)
                AttributesUsed.AddRange(input.AttributesUsed);
            if (input.SkillsUsed != null)
                SkillsUsed.AddRange(input.SkillsUsed);
            Limit = input.Limit;
        }
        #endregion

        public ActionResult Do(IDiceBag bag, ICharacter source)
        {
            if (bag == null) throw new ArgumentNullException("bag");
            if (source == null) throw new ArgumentNullException("source");

            var times = 0;
            AttributesUsed.ForEach(x => times += source.Attributes[x]);
            SkillsUsed.ForEach(x => times += source.Skills[x]);

            var pool = new DicePool(bag, times);
            var results = (Limit == LimitType.None)
                ? pool.Roll()
                : pool.Roll(source.Attributes.LimitValue(Limit));
            var rollNotation = buildNotation(source);

            return new ActionResult(rollNotation, results);
        }

        private string buildNotation(ICharacter source)
        {
            var str = new StringBuilder();

            for (int i = 0; i < AttributesUsed.Count; i++)
            {
                if (i > 0)
                    str.Append(" + ");
                str.Append(source.Attributes.Display(AttributesUsed[i]));
            }

            for (int i = 0; i < SkillsUsed.Count; i++)
            {
                if (str.Length > 0)
                    str.Append(" + ");
                str.Append(source.Skills.Display(SkillsUsed[i]));
            }

            // todo: roll notation - limits

            // todo: roll notation - threshold

            return str.ToString();
        }
    }
}
