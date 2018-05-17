using Sau.Raylan.SR5.Contracts.Interfaces;
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
            { AttributeType.Will, new Attribute() { Abbreviation = "WIL" } },
            { AttributeType.Logic, new Attribute() { Abbreviation = "LOG" } },
            { AttributeType.Intuition, new Attribute() { Abbreviation = "INT" } },
            { AttributeType.Charisma, new Attribute() { Abbreviation = "CHA" } },
            { AttributeType.Edge, new Attribute() { Abbreviation = "EDG" } },
            { AttributeType.Essence, new Attribute() { Abbreviation = "ESS" } },
            { AttributeType.Magic, new Attribute() { Abbreviation = "MAG" } },
        };

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

        private class Attribute
        {
            public int Value { get; set; }
            public string Abbreviation { get; set; }
            public string Display { get { return string.Format("{0} ({1})", Abbreviation, Value); } }
        }
    }
}
