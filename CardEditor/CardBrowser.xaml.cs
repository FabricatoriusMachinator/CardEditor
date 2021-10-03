
using CardEditor.Data;
using CardEditor.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
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

namespace CardEditor
{
    /// <summary>
    /// Interaction logic for CardBrowser.xaml
    /// </summary>
    public partial class CardBrowser : Window
    {
        EditorManager manager;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(btnDelete))
            {
                manager.cardCollection.DeleteOne(c => c.Id == selectedCard.Id);
                readAllDocuments();
            }
        }

        private void dgCards_MouseUp(object sender, MouseButtonEventArgs e)
        {
            selectedCard = (Card)dgCards.SelectedItem;
            showCard();
            
        }

        private void showCard()
        {
            string file = selectedCard.filePath;
            var filePath = file;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(file);
            bitmap.EndInit();
            imgInner.Source = bitmap;
            lblAtk.Content = selectedCard.Attack;
            lblDef.Content = selectedCard.Defence;
            lblCost.Content = selectedCard.Cost;

        }
    }
}
