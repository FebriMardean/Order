using Order.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Controller
{
    class PromoController
    {
        private List<Promo> promos;

        public PromoController()
        {
            promos = new List<Promo>();
        }

        public void addPromo(Promo item)
        {
            this.promos.Add(item);
        }

        public List<Promo> getPromos()
        {
            return this.promos;
        }
    }
}
