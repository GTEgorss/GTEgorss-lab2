using System;

namespace JavaServerParser
{
    public class Variable
    {
        public Variable(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get; }
        public string Name { get; }

        public override string ToString()
        {
            return Type + " " + Name;
        }
    }
}