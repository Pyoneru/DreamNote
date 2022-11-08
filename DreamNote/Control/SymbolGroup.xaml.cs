

using DreamNote.Controller;
using DreamNote.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DreamNote.Control
{
    /// <summary>
    /// Interaction logic for SymbolGroup.xaml
    /// </summary>
    public partial class SymbolGroup : UserControl
    {
        private Container C;
        private Window parent;

        public SymbolGroup()
        {
            C = Container.GetInstance();
            InitializeComponent();
            
            
        }

        public void Set(Symbol symbol, Window window)
        {
            this.parent = window;
            SetSymbol(symbol);
        }


        private void ClearEntries()
        {
            listPanel.Children.Clear();
        }

        public void SetSymbol(Symbol symbol)
        {
            List<Model.Entry> entries = C.GetEntiresBySymbol(symbol);

            symbol_title.Text = symbol.Name;
            symbol_count.Text = entries.Count.ToString();

            ClearEntries();
            foreach(Model.Entry entry in entries)
            {
                SymbolGroupElement element = new SymbolGroupElement();
                element.Set(entry, parent);
                listPanel.Children.Add(element);
            }
        }
    }
}
