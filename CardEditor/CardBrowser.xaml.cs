
using CardEditor.Data;
using CardEditor.Domain;
using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CardEditor
{
    /// <summary>
    /// Interaction logic for CardBrowser.xaml
    /// </summary>
    public partial class CardBrowser : Window
    {
        readonly EditorManager manager;
        Card selectedCard;
        public CardBrowser()
        {
            InitializeComponent();
            manager = (Application.Current.MainWindow as MainWindow).getManager();
            readAllDocuments();
        }

        public void readAllDocuments()
        {
            List<Card> list = manager.cardCollection.AsQueryable().ToList<Card>();
            dgCards.ItemsSource = list;
        }

        private void dgCards_MouseUp(object sender, MouseButtonEventArgs e)
        {
            selectedCard = (Card)dgCards.SelectedItem;
            showCard();
            
        }

        private void showCard()
        {
            if (selectedCard == null)
            {
                return;
            }

            string file = selectedCard.FilePath;
            var filePath = file;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(file);
            bitmap.EndInit();
            imgInner.Source = bitmap;
            lblAtk.Content = selectedCard.Attack;
            lblDef.Content = selectedCard.Defence;
            lblCost.Content = selectedCard.Cost;
            lblName.Content = selectedCard.Name;
            lblType.Content = selectedCard.Typing;

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            manager.cardCollection.DeleteOne(c => c.Id == selectedCard.Id);
            readAllDocuments();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            manager.Export(selectedCard, selectedCard.Name + "Card.json");
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json files (*.json)|*.json|All Files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                string file = openFileDialog.FileName;
                manager.Import(file);
                readAllDocuments();

            }
        }
    }
}
