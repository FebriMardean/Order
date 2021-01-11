using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Model
{
    public class Promo
    {
        public string npromo { get; set; }
        public double diskon { get; set; }

        public double diskonInPercent { get; set; }

        public Promo(string npromo, double diskon = 0, double diskonInPercent = 0)
        {
            this.npromo = npromo;
            this.diskon = diskon;
            this.diskonInPercent = diskonInPercent;
        }
    }
}
