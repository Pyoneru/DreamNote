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
using System.Windows.Shapes;

namespace DreamNote
{
    /// <summary>
    /// Interaction logic for EntryPasswordWindow.xaml
    /// </summary>
    public partial class EntryPasswordWindow : Window
    {
        private Window parent;
        private Model.Entry refEntry;
        public EntryPasswordWindow(Window parent, Model.Entry entry)
        {
            InitializeComponent();
            this.parent = parent;
            this.refEntry = entry;
        }

        private void Button_Click_Open(object sender, RoutedEventArgs args)
        {
            string password = passwordBox.Password;

            EntryWindow entryWindow = new EntryWindow(refEntry, password);
            App.Current.MainWindow = entryWindow;
            entryWindow.Show();
            this.Close();
            parent.Close();

        }

        private void Button_Click_Close(object sender, RoutedEventArgs args)
        {
            this.Close();
        }
    }
}
