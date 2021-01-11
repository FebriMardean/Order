using Order.Controller;
using Order.Model;
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

namespace Order
{
    /// <summary>
    /// Interaction logic for Penawaran.xaml
    /// </summary>
    public partial class Penawaran : Window
    {
        PenawaranController controller;
        OnPenawaranChangedListener listener;

        public Penawaran()
        {
            InitializeComponent();

            controller = new PenawaranController();
            listPenawaran.ItemsSource = controller.getItems();

            generateContentPenawaran();

        }

        public void SetOnItemSelectedListener(OnPenawaranChangedListener listener)
        {
            this.listener = listener;
        }

        private void generateContentPenawaran()
        {
            Item drink1 = new Item("Coffe Late", 30000);
            Item drink2 = new Item("Black Tea", 20000);
            Item food1 = new Item("Pizza", 75000);
            Item drink3 = new Item("Milk Shake", 15000);
            Item food2 = new Item("Fried Frice Special", 14000);
            Item food4 = new Item("Watermelon Juice", 25000);
            Item drink4 = new Item("Lemon Squash", 30000);

            controller.addItem(drink1);
            controller.addItem(drink2);
            controller.addItem(food1);
            controller.addItem(drink3);
            controller.addItem(food2);
            controller.addItem(food4);
            controller.addItem(drink4);

            listPenawaran.Items.Refresh();
        }

        private void listPenawaran_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listbox = sender as ListBox;
            Item item = listbox.SelectedItem as Item;

            this.listener.onPenawaranSelected(item);
        }
    }   

    public interface OnPenawaranChangedListener
    {
        void onPenawaranSelected(Item item);
    }
}

