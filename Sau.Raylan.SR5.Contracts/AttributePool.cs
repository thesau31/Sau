using System;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Contracts
{
    public class AttributePool
    {
        private Dictionary<AttributeType, Attribute> _attributes = new Dictionary<AttributeType, Attribute>()
        {
            { AttributeType.Reaction, new Attribute() { Abbreviation = "REA" } }
        };

        public int this[AttributeType type]
        {
            get
            {
                // todo: index error checking
                return _attributes[type].Value;
            }
            set
            {
                // todo: index error checking
                _attributes[type].Value = value;
            }
        }

        public string Display(AttributeType type)
        {
            throw new NotImplementedException();
        }

        private class Attribute
        {
            public int Value { get; set; }
            public string Abbreviation { get; set; }
            public string Display { get { return string.Format("[{0}] ({1})", Abbreviation, Value); } }
        }
    }



    public enum AttributeType
    {
        Body,
        Agility,
        Reaction,
        Strength,
        Will,
        Logic,
        Intuition,
        Charisma,
        Edge,
        Essence,
        Magic
    }
}
