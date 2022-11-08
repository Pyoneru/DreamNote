using DreamNote.Controller;
using DreamNote.Generator;
using DreamNote.Model;
using DreamNote.Reader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DreamNote
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container container = Container.GetInstance();
            container.TextEncoder = new TextEncoder();
            container.Settings = new Settings();
            container.Settings.HasPassowrd = true;

            //List<Entry> entries = new List<Entry>();
            //string password = "dupa";
            //Random random = new Random();
            //for(int i = 0; i < 20; i++)
            //{
            //    Entry entry = new Entry();
            //    entry.Title = RandomText(random.Next(10) + 5);
            //    entry.Content = RandomText(random.Next(100) + 100);

            //    int cSymbols = random.Next(5) + 1;
            //    List<Symbol> symbols = new List<Symbol>();
            //    for(int c = 0; c < cSymbols; c++)
            //    {
            //        symbols.Add(new Symbol(RandomText(random.Next(5) + 5)));
            //    }
            //    entry.Symbols = symbols;

            //    entries.Add(entry);
            //}
            //EntryGenerator generator = new EntryGenerator(new TextEncoder());

            //foreach (var entry in entries)
            //{
            //    generator.generate(entry, password);
            //    Thread.Sleep(1000);
            //}

            if(!Directory.Exists(container.EntryPath))
                Directory.CreateDirectory(container.EntryPath);
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(container.EntryPath);

                var files = directoryInfo.GetFiles(container.SuffixFilenamePattern);

                IEntryReader reader = new NoContentEntryReader();

                foreach(var file in files)
                {
                    Entry? entry = reader.Read(file.FullName, "");
                    if(entry != null)
                        container.AddEntry(entry);
                }
            }

            //if (!File.Exists($"{container.Path}\\{container.SettingsFilename}"))
            //{
            //    // ToDo: Create settings
            //    Settings settings = new Settings();
            //    settings.Encoder = Settings.EncoderType.ENTRY;
            //    container.Settings = settings;
            //}
            //else
            //{
            //    // ToDo: Read settings
            //}

        }

        private string RandomText(int size)
        {
            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string word = "";
            for (int i = 0; i < size; i++)
                word += chars[random.Next(chars.Length)];
            return word;
        }
    }
}
