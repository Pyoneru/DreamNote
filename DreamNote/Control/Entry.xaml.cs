using DreamNote.Model;
using DreamNote.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DreamNote.Control
{
    /// <summary>
    /// Interaction logic for Entry.xaml
    /// </summary>
    public partial class Entry : UserControl
    {
        private Model.Entry refEntry;

        private Window parent;

        public Entry()
        {
            InitializeComponent();
        }

        public void SetEntry(Model.Entry entry, Window window)
        {
            this.refEntry = entry;
            this.parent = window;

            entry_date.Text = DateFormater.Format(entry.Date);
            entry_title.Text = entry.Title;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EntryPasswordWindow window = new EntryPasswordWindow(parent, refEntry);
            window.Show();
        }
    }
}
