using Order.Controller;
using Order.Model;
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
using static Order.Model.Keranjang;

namespace Order
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, OnKeranjangChangedListener, OnPembayaranChangedListener, OnPenawaranChangedListener, OnPromoChangedListener
    {
        OnPembayaranChangedListener
        OnPromoChangedListener;
        

        MainWindowController controller;
        Pembayaran pembayaran;
        Promo promo;

        public MainWindow()
        {
            InitializeComponent();

            pembayaran = new Pembayaran(this);
            pembayaran.setBalance(500000);
            pembayaran.setPromo(0);

            Keranjang keranjang = new Keranjang(pembayaran, this);

            controller = new MainWindowController(keranjang);

            listBoxPesanan.ItemsSource = controller.getSelectedItems();
            listBoxPakaiPromo.ItemsSource = controller.getSelectedPromos();

            initializeView();
        }

        private void initializeView()
        {
            labelSubtotal.Content = 0;
            labelGrantTotal.Content = 0;
            labelPromoFee.Content = (pembayaran.getPromo() > 0) ? - pembayaran.getPromo() : 0;
        }

        public void onPenawaranSelected(Item item)
        {
            controller.addItem(item);
        }

        public void onPromoSelected(Promo promo)
        {
            controller.addPromo(promo);
        }

        private void onButtonAddItemClicked(object sender, RoutedEventArgs e)
        {
            Penawaran penawaranWindow = new Penawaran();
            penawaranWindow.SetOnItemSelectedListener(this);
            penawaranWindow.Show();
        }

        private void onButtonAddPromoClicked(object sender, RoutedEventArgs e)
        {
            PromoM promomWindow = new PromoM();
            promomWindow.SetOnItemSelectedListener(this);
            promomWindow.Show();
        }

        private void listBoxPesanan_ItemClicked(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Kamu ingin menghapus item ini?",
                    "Konfirmasi", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ListBox listBox = sender as ListBox;
                Item item = listBox.SelectedItem as Item;
                controller.deleteSelectedItem(item);
            }
        }

        private void listBoxPakaiPromo_ItemClicked(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Kamu ingin membatalkan promo ini?",
                   "Konfirmasi", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ListBox listBox = sender as ListBox;
                Promo promo = listBox.SelectedItem as Promo;
                controller.deleteSelectedPromo(promo);
            }
        }


        public void onSelectedPenawaranDeleted()
        {
            listBoxPesanan.Items.Refresh();
        }

        public void onSelectedPenawaranAdded()
        {
            listBoxPesanan.Items.Refresh();
        }

        public void onPriceUpdated(double subtotal, double grantTotal, double potongan )
        {
            labelSubtotal.Content = "Rp " + subtotal;
            labelGrantTotal.Content = "Rp " + grantTotal;
            labelPromoFee.Content = "Rp" + potongan;

        }

        public void removeItemSucceed()
        {
            listBoxPesanan.Items.Refresh();
        }

        public void addItemSucceed()
        {
            listBoxPesanan.Items.Refresh();
        }

        public void removePromoSucceed()
        {
            listBoxPakaiPromo.Items.Refresh();
        }

        public void addPromoSucceed()
        {
            listBoxPakaiPromo.Items.Refresh();
        }


        public void OnPakaiPromoChangedListener(Promo promo)
        {
            controller.addPromo(promo);
        }
    }
}
