using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNote.Model
{
    public class Entry
    {

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public List<Symbol> Symbols { get; set; }
        public string Path { get; set; }

        public Entry()
        {
            Symbols = new List<Symbol>();
        }

        public override bool Equals(object? obj)
        {
            return obj is Entry entry &&
                   Title == entry.Title &&
                   Content == entry.Content &&
                   Date == entry.Date &&
                   EqualityComparer<List<Symbol>>.Default.Equals(Symbols, entry.Symbols);
        }
    }
}
