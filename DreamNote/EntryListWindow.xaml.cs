using DreamNote.Controller;
using DreamNote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
    /// Interaction logic for EntryListWindow.xaml
    /// </summary>
    public partial class EntryListWindow : Window
    {
        private Container C;

        public EntryListWindow()
        {
            C = Container.GetInstance();
            InitializeComponent();
            ClearSearchBar();
            ClearEntries();
            InitializeEntries();
        }

        private void ClearSearchBar()
        {
            search_bar.Text = "";
        }

        private void ClearEntries()
        {
            if(listPanel != null)
                listPanel.Children.Clear();
        }

        private void InitializeEntries()
        {
            SetEntries(C.Entries);
        }

        private void SetEntries(List<Entry> entries)
        {
            foreach (var (entry, index) in entries.Select((value, i) => (value, i)))
            {
                Button button = new Button();
                button.Content = entry.Title;
                button.Tag = index;
                button.Click += OpenEntry;

                listPanel.Children.Add(button);
            }
        }

        private void OpenEntry(object sender, RoutedEventArgs e)
        {
            Entry entry = C.Entries[((int)(sender as Button).Tag)];

            EntryPasswordWindow window = new EntryPasswordWindow(this, entry);
            window.Show();
        }

        private void Button_Home(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            App.Current.MainWindow = mainWindow;
            mainWindow.Show();
            this.Close();
        }

        private void SearchEntries(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(search_bar.Text))
            {
                ClearEntries();
                List<Entry> entries = GetEntriesByText(C.Entries, search_bar.Text);
                SetEntries(entries);
            }
            else
            {
                ClearEntries();
                SetEntries(C.Entries);
            }
        }

        private List<Entry> GetEntriesByText(List<Entry> entries, string text)
        {
            List<Entry> list = new List<Entry>();

            foreach(Entry entry in entries)
                if(entry.Title.Contains(text, StringComparison.OrdinalIgnoreCase))
                    list.Add(entry);

            return list;
        }
    }
}
