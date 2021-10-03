using CardEditor.Data;
using CardEditor.Domain;
using Microsoft.Win32;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        string typeText;
        string filePath;
        EditorManager manager;

        public MainWindow()
        {
            InitializeComponent();
            manager = new EditorManager();
            manager.Init();
            populateComboBox();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(btnUpload))
            {
                imageUpload();
            }
            else if (sender.Equals(btnAddType))
            {
                var type = new Types { 
                    Name = tbxAddType.Text, 
                    Attack = int.Parse(tbxTypeAtk.Text), 
                    Defence = int.Parse(tbxTypeDef.Text),
                    Cost = int.Parse(tbxTypeCost.Text)
                };
                manager.storeData(type);
                populateComboBox();

            }
            else if (sender.Equals(btnAddCard))
            {
                var card = new Card { 
                    Name = tbxName.Text,
                    Typing = cboType.Text,
                    Attack = int.Parse(tbxCardAtk.Text),
                    Defence = int.Parse(tbxCardDef.Text),
                    Cost = int.Parse(tbxCardCost.Text),
                    FilePath = filePath
                };
                manager.StoreData(card);
                clearAll();
            }
            else if (sender.Equals(btnBrowse))
            {
                CardBrowser cardBrowser = new CardBrowser();
                cardBrowser.Show();
            }
        }

        public void imageUpload()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                string file = openFileDialog.FileName;
                tbcPath.Text = "Path: " + file;
                filePath = file;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(file);
                bitmap.EndInit();
                image.Source = bitmap;

            }
        }

        public EditorManager getManager()
        {
            return manager;
        }

        public static IEnumerable<T> FindChildren<T>(DependencyObject dObj) where T : DependencyObject
        {

            for(int i = 0; i < VisualTreeHelper.GetChildrenCount(dObj); i++)
            {
                var child = VisualTreeHelper.GetChild(dObj, i);

                if(child != null && child is T)
                {
                    yield return (T)child;
                }

                foreach(T childOfChild in FindChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
            
        }

        public void clearAll()
        {
            foreach(var tbx in FindChildren<TextBox>(this))
            {
                tbx.Clear();
                image.Source = null;
            }

        }



        public void populateComboBox()
        {
            cboType.Items.Clear();
            List<Types> list = manager.GetTypeList();
            
            foreach(Types types in list)
            {
                cboType.Items.Add(types.Name);
            }
        }
    }
}
