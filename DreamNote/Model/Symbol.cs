using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNote.Model
{
    public class Symbol
    {
        private string name;
        public string Name
        {
            get => name;
            set => name = value.ToLower();
        }

        public int Count { get; set; }

        public Symbol()
        {
            Count = 1;
        }

        public Symbol(string name) : this()
        {
            Name = name;
        }

        public override bool Equals(object? obj)
        {
            return obj is Symbol symbol &&
                   name.Equals(symbol.name);
        }

    }
}
