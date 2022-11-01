using DreamNote.Model;
using DreamNote.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DreamNote.Reader
{
    public class NoContentEntryReader : IEntryReader
    {
        public Entry? Read(string filename, string password)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(filename);

                var xmlTitle = xml.SelectSingleNode("/Entry/Title");
                var xmlDate = xml.SelectSingleNode("/Entry/SaveDate");
                var xmlSymbols = xml.SelectNodes("/Entry/Symbols/Symbol");

                Entry entry = new Entry();
                entry.Path = filename;
                entry.Title = xmlTitle.InnerText;
                entry.Date = DateFormater.ConvertFormat(xmlDate.InnerText);

                List<Symbol> symbols = new List<Symbol>();
                foreach (XmlNode xmlSymbol in xmlSymbols)
                {
                    string symbol = "";
                    symbol = xmlSymbol.InnerText;

                    symbols.Add(new Symbol(symbol));
                }
                entry.Symbols = symbols;

                return entry;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
