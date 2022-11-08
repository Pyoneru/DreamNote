using DreamNote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DreamNote.Control
{
    /// <summary>
    /// Interaction logic for SymbolListItem.xaml
    /// </summary>
    public partial class SymbolListItem : UserControl
    {
        public Symbol Symbol { get; set; }
        public int Idx { get; set; }

        private EntryWindow entryWindow;

        public SymbolListItem()
        {
            InitializeComponent();
        }

        public void Set(Symbol symbol, int idx, EntryWindow window)
        {
            this.Symbol = symbol;
            symbol_title.Text = symbol.Name;
            Idx = idx;
            entryWindow = window;
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            entryWindow.RemoveSymbol(Idx);
        }
    }
}
