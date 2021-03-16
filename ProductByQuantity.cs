using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.Models
{
    class ProductByQuantity : Product
    {
        public double UnitPrice { get; set; }

        private int units;
        public int Units
        {
            get
            {
                return units;
            }
            set
            {
                units = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("ShoppingCartDisplay");
                NotifyPropertyChanged("ReceiptDisplay");
            }
        }

        public override double Price => UnitPrice * Units;  //Unique implementation of Price via overriding
        
        public ProductByQuantity(double price, string name, string des, int id, double unit_price, int units) : base(price, name, des, id)
        {
            UnitPrice = unit_price;
            Units = units;
        }

        public override string ShoppingCartDisplay
        {
            get
            {
                return $"[{Name}] - UnitPrice: ${UnitPrice} - Units: {Units}";
            }
        }

        public override string ReceiptDisplay
        {
            get
            {
                return $"[{Name}] - UnitPrice: ${UnitPrice} - Units: {Units} - Item Total: ${UnitPrice * Units}";
            }
        }
    }
}
