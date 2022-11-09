using DreamNote.Control;
using DreamNote.Generator;
using DreamNote.Model;
using DreamNote.Reader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Container = DreamNote.Controller.Container;

namespace DreamNote
{
    /// <summary>
    /// Interaction logic for EntryWindow.xaml
    /// </summary>
    public partial class EntryWindow : Window
    {
        private Container C;
        private EntryGenerator generator;
        private Model.Entry? entry;
        private string password;


        public EntryWindow(Model.Entry entry, string password)
        {
            C = Container.GetInstance();
            generator = new EntryGenerator(C.TextEncoder);
            InitializeComponent();
            this.password = password;

            ClearEntry();
            this.entry = ReadEntry(entry, password);
            if(this.entry != null)
                InitializeEntry(this.entry);

        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if(entry == null)
            {
                MainWindow window = new MainWindow();
                App.Current.MainWindow = window;
                window.Show();
                Close();
            }
        }

        private Model.Entry? ReadEntry(Model.Entry entry, string password)
        {
            if (!string.IsNullOrEmpty(entry.Path))
            {
                IEntryReader reader = new EntryReader(new TextEncoder());
                Model.Entry? readEntry = reader.Read(entry.Path, password);

                if (readEntry != null)
                    return readEntry;
                return null;
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
            SetSymbols(entry.Symbols);
        }

        public void SetTitle(string title)
        {
            this.entry_title.Text = title;
        }

        public void SetContent(string content)
        {
            this.entry_content.Text = content;
        }

        public void SetSymbols(List<Symbol> symbols)
        {
            entry_symbols.Children.Clear();
            for (int i = 0; i < symbols.Count; i++)
            {
                Symbol symbol = symbols[i];
                SymbolListItem symbolItem = new SymbolListItem();
                symbolItem.Set(symbol, i, this);
                entry_symbols.Children.Add(symbolItem);
            }
        }

        public void RemoveSymbol(int idx)
        {
            entry.Symbols.RemoveAt(idx);
            SetSymbols(entry.Symbols);
        }

        private void Button_Save_Entry(object sender, RoutedEventArgs e)
        {
            SaveEntry();
        }

        private void Button_Home(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Content", "Title", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
                SaveEntry();

            MainWindow window = new MainWindow();
            App.Current.MainWindow = window;
            window.Show();
            Close();
        }

        private void Button_Add_Symbol(object sender, RoutedEventArgs e)
        {
            string symbolName = tb_symbol.Text;

            bool symbolExists = false;
            if(string.IsNullOrEmpty(symbolName))
            {
                MessageBox.Show("Symbol cannot be empty.", "Empty symbol!", MessageBoxButton.OK, MessageBoxImage.Warning);
                symbolExists = true;
            }


            if(!symbolExists){
                foreach (var symbol in entry.Symbols)
                {
                    if(symbol.Name.Equals(symbolName)){
                        MessageBox.Show("Symbol '" + symbolName + "' exists.", "Symbol Exists!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        symbolExists = true;
                    }
                }
            }

            if(!symbolExists)
            {
                entry.Symbols.Add(new Symbol(symbolName));
                SetSymbols(entry.Symbols);
            }
        }

//        protected override void OnClosed(EventArgs e)
//        {
//            MessageBoxResult result = MessageBox.Show("Would you like to save the entry?", "Save entry?", MessageBoxButton.YesNo, MessageBoxImage.Question);
//            if(result == MessageBoxResult.Yes)
//                SaveEntry();
//        }

        private void SaveEntry()
        {
            entry.Title = entry_title.Text;
            entry.Content = entry_content.Text;
            string path = generator.generate(entry, password);
            if(!string.IsNullOrEmpty(entry.Path)){
                C.Entries.Find(entry => entry.Path.Equals(path)).Title = entry.Title;
            }else
            {
                entry.Path = path;
                C.Entries.Add(entry);
            }
        }
    }
}
