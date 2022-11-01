using DreamNote.Generator;
using DreamNote.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DreamNote.Controller
{
    public class Container
    {
        private static Container instance;
        public List<Entry> Entries { get; }

        public List<Symbol> Symbols { get; }

        public ITextEncoder TextEncoder { get; set; }

        public Settings Settings { get; set; }

        public string Path { get; }

        public string EntryPath => Path + "\\Entry";

        public string SuffixFilenamePattern => "*.entry.xml";

        public string SettingsFilename => "settings.json";

        private Container()
        {
            Entries = new List<Entry>();
            Symbols = new List<Symbol>();
            Path = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
        }

        public static Container GetInstance()
        {
            if (instance == null)
                instance = new Container();
            return instance;
        }

        public List<Entry> GetEntiresBySymbol(Symbol symbol)
        {
            return Entries.FindAll(item => item.Symbols.Contains(symbol));
        }

        public void AddEntry(Entry entry)
        {
            Entries.Add(entry);

            foreach(var symbol in entry.Symbols)
            {
                if(!Symbols.Contains(symbol))
                    Symbols.Add(symbol);
                //else 
                //    Symbols.Find(s => s.Equals(symbol)).Count++;     
            }
        }
    }
}
