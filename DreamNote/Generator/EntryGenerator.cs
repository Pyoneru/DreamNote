using DreamNote.Controller;
using DreamNote.Model;
using DreamNote.Tool;
using System;
using System.IO;
using System.Windows;
using System.Xml;

namespace DreamNote.Generator
{
    public class EntryGenerator
    {

        private ITextEncoder textEncoder;
        private Container C;

        public EntryGenerator(ITextEncoder textEncoder)
        {
            this.textEncoder = textEncoder;
            C = Container.GetInstance();
        }

        public bool generate(Entry entry, string password)
        {
            XmlDocument xml = new XmlDocument();

            XmlDeclaration declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlElement root = xml.DocumentElement;
            xml.InsertBefore(declaration, root);

            var xmlEntry = xml.CreateElement("Entry");

            var xmlTitle = xml.CreateElement("Title");
            xmlTitle.InnerText = entry.Title;

            var xmlDate = xml.CreateElement("SaveDate");
            xmlDate.InnerText = DateFormater.Format(entry.Date);
  

            var xmlHasPassword = xml.CreateElement("HasPassword");
            xmlHasPassword.InnerText = C.Settings.HasPassowrd ? "true" : "false";

            var xmlContent = xml.CreateElement("Content");
            if (C.Settings.HasPassowrd)
                xmlContent.InnerText = textEncoder.Encode(entry.Content, password);
            else
                xmlContent.InnerText = entry.Content;

            var xmlSymbols = WrapSymbols(entry, xml, password);

            xmlEntry.AppendChild(xmlTitle);
            xmlEntry.AppendChild(xmlDate);
            xmlEntry.AppendChild(xmlHasPassword);
            xmlEntry.AppendChild(xmlContent);
            xmlEntry.AppendChild(xmlSymbols);

            xml.AppendChild(xmlEntry);

            string filename = Path.Combine(C.EntryPath, DateFormater.NoFormat(DateTime.Now) + ".xml");
            try
            {
                //MessageBox.Show(filename);
                xml.Save(filename);
            }catch(Exception e)
            {
                return false;
            }

            return true;
        }

        private XmlElement WrapSymbols(Entry entry, XmlDocument xml, string password)
        {
            var xmlSymbols = xml.CreateElement("Symbols");

            foreach (var symbol in entry.Symbols)
            {
                var xmlSymbol = xml.CreateElement("Symbol");
                xmlSymbol.InnerText = symbol.Name;

                xmlSymbols.AppendChild(xmlSymbol);
            }

            return xmlSymbols;
        }
    }
}
