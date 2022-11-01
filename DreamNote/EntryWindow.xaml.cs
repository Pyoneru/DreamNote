using DreamNote.Control;
using DreamNote.Controller;
using DreamNote.Generator;
using DreamNote.Model;
using DreamNote.Reader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DreamNote
{
    /// <summary>
    /// Interaction logic for EntryWindow.xaml
    /// </summary>
    public partial class EntryWindow : Window
    {
        private Container C;
        private Model.Entry entry;
        private string password;
        private int symbolId = 0;

        public EntryWindow(Model.Entry entry, string password)
        {
            C = Container.GetInstance();
            InitializeComponent();
            this.password = password;

            ClearEntry();
            this.entry = ReadEntry(entry, password);
            InitializeEntry(this.entry);

        }

        private Model.Entry ReadEntry(Model.Entry entry, string password)
        {
            if (!string.IsNullOrEmpty(entry.Path))
            {
                IEntryReader reader = new EntryReader(new TextEncoder());
                Model.Entry? readEntry = reader.Read(entry.Path, password);

                if (readEntry != null)
                    return readEntry;
                return new Model.Entry();
            }
            return entry;
        }

        private void ClearEntry()
        {
            entry_title.Text = "";
            entry_content.Text = "";
            entry_symbols.Children.Clear();
        }

        private void InitializeEntry(Model.Entry entry)
        {
            SetTitle(entry.Title);
            SetContent(entry.Content);

            //foreach (Model.Symbol symbol in entry.Symbols)
            //    AddSymbol(symbol);
        }

        public void SetTitle(string title)
        {
            this.entry_title.Text = title;
        }

        public void SetContent(string content)
        {
            this.entry_content.Text = content;
        }
    }
}
