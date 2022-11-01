using DreamNote.Controller;
using DreamNote.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DreamNote.Control
{
    /// <summary>
    /// Interaction logic for SymbolGroupElement.xaml
    /// </summary>
    public partial class SymbolGroupElement : UserControl
    {
        private Container C;
        private Model.Entry refEntry;
        private Window parent;

        public SymbolGroupElement()
        {
            C = Container.GetInstance();
            InitializeComponent();
        }

        public void Set(Model.Entry entry, Window window)
        {
            SetEntry(entry);
            this.refEntry = entry;
            this.parent = window;
        }

        public void SetEntry(Model.Entry entry)
        {
            entry_title.Text = entry.Title;

            List<Symbol> symbols = entry.Symbols;

            string hint = "";
            for(int i = 0; i < symbols.Count; i++)
            {
                Symbol symbol = symbols[i];
                if(i < symbols.Count - 1)
                {
                    hint += symbol.Name + ",";
                }
                else
                {
                    hint += symbol.Name;
                }
            }

            entry_hint.Text = hint;
        }

        private void Button_entry(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new EntryWindow(this.refEntry);
            App.Current.MainWindow = entryWindow;
            entryWindow.Show();
            parent.Close();
        }
    }
}
