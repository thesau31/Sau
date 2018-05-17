using Sau.Raylan.SR5.Contracts.Interfaces;
using System;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Contracts
{
    public class AttributePool : IAttributePool
    {
        private Dictionary<AttributeType, Attribute> _attributes = new Dictionary<AttributeType, Attribute>()
        {
            { AttributeType.Body, new Attribute() { Abbreviation = "BOD" } },
            { AttributeType.Agility, new Attribute() { Abbreviation = "AGI" } },
            { AttributeType.Reaction, new Attribute() { Abbreviation = "REA" } },
            { AttributeType.Strength, new Attribute() { Abbreviation = "STR" } },
            { AttributeType.Willpower, new Attribute() { Abbreviation = "WIL" } },
            { AttributeType.Logic, new Attribute() { Abbreviation = "LOG" } },
            { AttributeType.Intuition, new Attribute() { Abbreviation = "INT" } },
            { AttributeType.Charisma, new Attribute() { Abbreviation = "CHA" } },
            { AttributeType.Edge, new Attribute() { Abbreviation = "EDG" } },
            { AttributeType.Essence, new Attribute() { Abbreviation = "ESS" } },
            { AttributeType.Magic, new Attribute() { Abbreviation = "MAG" } },
        };

        private Dictionary<LimitType, Limit> _limits = new Dictionary<LimitType, Limit>() { };

        public AttributePool()
        {
            _limits.Add(LimitType.Mental, new Limit()
            {
                Name = "Mental",
                Value = () => (int)Math.Ceiling((double)((_attributes[AttributeType.Logic].Value * 2) + _attributes[AttributeType.Intuition].Value + _attributes[AttributeType.Willpower].Value) / 3)
            });
            _limits.Add(LimitType.Physical, new Limit()
            {
                Name = "Physical",
                Value = () => (int)Math.Ceiling((double)((_attributes[AttributeType.Strength].Value * 2) + _attributes[AttributeType.Body].Value + _attributes[AttributeType.Reaction].Value) / 3)
            });
            _limits.Add(LimitType.Social, new Limit()
            {
                Name = "Social",
                Value = () => (int)Math.Ceiling((double)((_attributes[AttributeType.Charisma].Value * 2) + _attributes[AttributeType.Willpower].Value + _attributes[AttributeType.Essence].Value) / 3)
            });
        }

        public int this[AttributeType type]
        {
            get
            {
                return _attributes[type].Value;
            }
            set
            {
                _attributes[type].Value = value;
            }
        }

        public string Display(AttributeType type)
        {
            return _attributes[type].Display;
        }

        public int LimitValue(LimitType type)
        {
            return _limits[type].Value();
        }

        public string LimitDisplay(LimitType type)
        {
            return _limits[type].Display;
        }

        private class Attribute
        {
            public int Value { get; set; }
            public string Abbreviation { get; set; }
            public string Display { get { return string.Format("{0} ({1})", Abbreviation, Value); } }
        }

        private class Limit
        {
            public Func<int> Value { get; set; }
            public string Name { get; set; }
            public string Display { get { return string.Format("[{0} ({1})]", Name, Value()); } }
        }
    }
}
