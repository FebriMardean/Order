using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Order.Model
{
    class Keranjang
    {
        List<Item> itemBelanja;
        List<Promo> itemPromo;
        int capacity = 1;
        Pembayaran pembayaran;
        OnKeranjangChangedListener callback;

        public Keranjang (Pembayaran pembayaran, OnKeranjangChangedListener callback)
        {
            this.pembayaran = pembayaran;
            this.itemBelanja = new List<Item>();
            this.itemPromo = new List<Promo>();
            this.callback = callback;
        }

        public List<Item> getItems()
        {
            return this.itemBelanja;
        }

        public List<Promo> getPromos()
        {
            return this.itemPromo;
        }

        public void addItem(Item item)
        {
            this.itemBelanja.Add(item);
            this.callback.addItemSucceed();
            calculateSubTotal();
        }

        public void addPromo(Promo promo)
        {
            if (capacity == 1)
            {
                this.itemPromo.Add(promo);
                this.callback.addPromoSucceed();
                calculateSubTotal();
            }

            else
            {
                MessageBox.Show("Oopss! Kamu hanya dapat menggunakan salah satu voucher aja", "Ok", MessageBoxButton.OK);
            }
        }

        public void removeItem(Item item)
        {
            this.itemBelanja.Remove(item);
            this.callback.removeItemSucceed();
            capacity = 0;
            calculateSubTotal();
        }

        public void removePromo(Promo promo)
        {
            this.itemPromo.Remove(promo);
            this.callback.removePromoSucceed();
            capacity = 1;
            calculateSubTotal();
        }

        private void calculateSubTotal()
        {
            double subtotal = 0;
            double potongan = 0;
            foreach (Item item in itemBelanja)
            {
                subtotal += item.price;
            }

            foreach (Promo promo in itemPromo)
            {
                if (promo.diskonInPercent != 0)
                {

                    if (promo.diskonInPercent == 30)
                    {
                        if (subtotal >= 100000)
                        {
                            potongan -= 30000;
                        }
                        else
                        {
                            potongan -= subtotal * (promo.diskonInPercent / 100);
                        }
                    }
                    else
                    {
                        potongan -= subtotal * (promo.diskonInPercent / 100);
                    }
                }

                if (promo.diskon != 0)
                {
                    potongan -= promo.diskon;
                }
            }
            pembayaran.updateTotal(subtotal, potongan);

        }
        public interface OnKeranjangChangedListener
        {
            void removeItemSucceed();
            void addItemSucceed();

            void removePromoSucceed();
            void addPromoSucceed();
        }
    }
}
