using DreamNote.Control;
using DreamNote.Controller;
using DreamNote.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DreamNote
{
    /// <summary>
    /// Interaction logic for SymbolListWindow.xaml
    /// </summary>
    public partial class SymbolListWindow : Window
    {
        private Container C;
        public SymbolListWindow()
        {
            C = Container.GetInstance();
            InitializeComponent();
            ClearSearchBar();
            ClearSymbols();
            InitializeSymbols();
        }

        private void ClearSearchBar()
        {
            search_bar.Text = "";
        }

        private void ClearSymbols()
        {
            if(panelEntryList != null)
                panelEntryList.Children.Clear();
        }

        private void InitializeSymbols()
        {
            SetSymbols(C.Symbols);
        }

        private void SetSymbols(List<Symbol> symbols)
        {
            foreach(Symbol symbol in symbols)
            {
                SymbolGroup symbolGroup = new SymbolGroup();
                symbolGroup.Set(symbol, this);
                panelEntryList.Children.Add(symbolGroup);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            App.Current.MainWindow = mainWindow;
            mainWindow.Show();
            this.Close();
        }

        private void SearchSymbols(object sender, TextChangedEventArgs e)
        {
            SearchSymbolByName();
        }

        public void SearchSymbolByName()
        {
            ClearSymbols();
            if (!string.IsNullOrEmpty(search_bar.Text))
            {
                List<Symbol> symbols = new List<Symbol>();
                foreach (Symbol symbol in C.Symbols)
                {
                    if (symbol.Name.Contains(search_bar.Text))
                        symbols.Add(symbol);
                }
                SetSymbols(symbols);
            }
            else
            {
                SetSymbols(C.Symbols);
            }
        }
    }
}
