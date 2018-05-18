using System;
using System.Collections.Generic;
using System.Text;
using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Contracts.Interfaces;

namespace Sau.Raylan.SR5.Services.Actions
{
    public class Action : IAction
    {
        public string Name { get; private set; }
        public List<AttributeType> AttributesUsed { get; private set; }
        public List<SkillType> SkillsUsed { get; private set; }
        public LimitType Limit { get; private set; }

        #region c'tor
        protected Action()
        {
            AttributesUsed = new List<AttributeType>();
            SkillsUsed = new List<SkillType>();
        }

        internal Action(ActionInput input)
            : this()
        {
            Name = input.Name;
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

            // attribute(s)
            for (int i = 0; i < AttributesUsed.Count; i++)
            {
                if (i > 0)
                    str.Append(" + ");
                str.Append(source.Attributes.Display(AttributesUsed[i]));
            }

            // skill(s)
            for (int i = 0; i < SkillsUsed.Count; i++)
            {
                if (str.Length > 0)
                    str.Append(" + ");
                str.Append(source.Skills.Display(SkillsUsed[i]));
            }

            // limit
            if (Limit != LimitType.None) str.Append(" " + source.Attributes.LimitDisplay(Limit));

            // todo: roll notation - threshold
            // threshold

            return str.ToString();
        }
    }
}
