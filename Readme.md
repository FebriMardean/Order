Nama : Ananda Febriansyah Mardean

NIM : 19.11.2856

# Studi Kasus Promo
Aplikasi ini bertujuan seperti simulasi pembelian makanan dan minuman dengan menggunakan promo/voucher.

# Scope and Functionalities
- User dapat melihat daftar makanan dan minuman yang tersedia
- User dapat menggunakan voucher
- User dapat melihat potongan harga setelah menggunakan salah satu voucher
- User dapat melihat voucher yang di tawarkan

# How Does It Works?
Dalam kasus yang diberikan saya menggunakan 4 buah model dan 3 buah controller yang bertujuan untuk menjalankan program tersebut.

Dimulai dari `Penawaran.xaml.cs` yg bertujuan untuk menawarkan sebuah list makanan yang bisa menggunakan sebuah voucher untuk di tampilkan di listbox.

```csharp
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
```

Kemudian dilanjutkan pada `PromoM.xaml.cs`, yang bertujuan untuk mengsinkronisasi voucher dan model penawaran, yang akan di tampilkan pada listbox juga.

```csharp
 private void generateContentPromo()
        {
            Promo promo1 = new Promo("Promo Awal tahun Diskon 25 % ", diskonInPercent : 25);
            Promo promo2 = new Promo("Promo Tebus Murah Diskon 30 % ", diskonInPercent : 30);
            Promo promo3 = new Promo("Promo Natal", diskonInPercent : 10);

            controller.addPromo(promo1);
            controller.addPromo(promo2);
            controller.addPromo(promo3);

            listPromo.Items.Refresh();
        }
```


Lalu pada `MainWindow.xaml.cs` kita menginisiasi object dari `Penawaran.xaml.cs` dan juga `PromoM.xaml.cs`, untuk di masukan dalam sebuah list Keranjang. Dan tampilan Total dari semua belanja akan di tampilkan pada ListBox.

```csharp
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

        private void onButtonAddItemClicked(object sender, RoutedEventArgs e)
        {
            Penawaran penawaranWindow = new Penawaran();
            penawaranWindow.SetOnItemSelectedListener(this);
            penawaranWindow.Show();
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