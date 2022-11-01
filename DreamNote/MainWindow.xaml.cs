using DreamNote.Controller;
using DreamNote.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DreamNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Container C;
        private List<TextBlock> tbSymbols;
        public MainWindow()
        {
            InitializeComponent();

            C = Container.GetInstance();

            tbSymbols = ListOfSymbolTextBlocks();

            ClearEntries();
            ClearSymbols();

            InitializeEntries();
            InitializeSymbols();

        }

        private List<TextBlock> ListOfSymbolTextBlocks()
        {
            List<TextBlock> symbols = new List<TextBlock>();
            symbols.Add(s00);
            symbols.Add(s01);
            symbols.Add(s02);
            symbols.Add(s10);
            symbols.Add(s11);
            symbols.Add(s12);
            return symbols;
        }

        private void ClearEntries()
        {
            panelEntries.Children.Clear();
        }

        private void ClearSymbols()
        {
            foreach(var tbSymbol in tbSymbols){
                tbSymbol.Text = String.Empty;
            }
        }

        private void InitializeEntries()
        {
            if (C != null)
            {
                List<Entry> entries = C.Entries;
                entries.Sort((e1, e2) => DateTime.Compare(e1.Date, e2.Date));

                int max = entries.Count > 10 ? 10 : entries.Count;

                for (int i = 0; i < max; i++)
                {
                    Control.Entry entry = new Control.Entry();
                    entry.SetEntry(entries[i], this);
                    panelEntries.Children.Add(entry);
                }
            }
        }

        private void InitializeSymbols()
        {
            if (C != null)
            {
                List<Symbol> symbols = C.Symbols;
                symbols.Sort((s1, s2) => s1.Count.CompareTo(s2.Count));

                int max = symbols.Count > 6 ? 6 : max = symbols.Count;

                for (int i = 0; i < max; i++)
                {
                    Symbol symbol = symbols[i];
                    string text = symbol.Name.Length > 12 ? symbol.Name.Substring(0, 9) + "..." : symbol.Name;
                    tbSymbols[i].Text = text;
                }
            }
        }

        private void HorizontalScroll(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollviewer.LineLeft();
            else
                scrollviewer.LineRight();
            e.Handled = true;
        }

        private void Button_Click_more_entries(object sender, RoutedEventArgs e)
        {
            EntryListWindow entryListWindow = new EntryListWindow();
            App.Current.MainWindow = entryListWindow;
            entryListWindow.Show();
            this.Close();
        }

        private void Button_Click_more_symbols(object sender, RoutedEventArgs e)
        {
            SymbolListWindow symbolListWindow = new SymbolListWindow();
            App.Current.MainWindow = symbolListWindow;
            symbolListWindow.Show();
            this.Close();
        }
    }
}
