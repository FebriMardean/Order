using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Model
{
    class Pembayaran
    {
        private double promo = 0;
        private double balance = 0;


        private OnPembayaranChangedListener paymentCallback;

        public Pembayaran(OnPembayaranChangedListener paymentCallback)
        {
            this.paymentCallback = paymentCallback;
        }

        public void setBalance(double balance)
        {
            this.balance = balance;
        }

        public double getBalance()
        {
            return this.balance;
        }

        public double getPromo()
        {
            return this.promo;
        }

        public void setPromo(double promo)
        {
            this.promo = promo;
        }

        public void updateTotal(double subtotal, double potongan)
        {

            double total = subtotal + potongan;
            this.paymentCallback.onPriceUpdated(subtotal, total, potongan);
        }
    }

    interface OnPembayaranChangedListener
    {
        void onPriceUpdated(double subtotal, double grantTotal, double potongan);
    }
}

