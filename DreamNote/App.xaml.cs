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
    }
}
