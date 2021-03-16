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
    class ProductByWeight : Product
    {
        public double PricePerOunce { get; set; }

        private double ounces;
        public double Ounces
        {
            get
            {
                return ounces;
            }
            set
            {
                ounces = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("ShoppingCartDisplay");
                NotifyPropertyChanged("ReceiptDisplay");
            }
        }

        public override double Price => PricePerOunce * Ounces; //Unique implementation of Price via overriding

        public ProductByWeight(double price, string name, string des, int id, double price_per_ounce, double ounces) : base(price, name, des, id)
        {
            PricePerOunce = price_per_ounce;
            Ounces = ounces;
        }

        public override string ShoppingCartDisplay
        {
            get
            {
                return $"[{Name}] - PricePerOunce: ${PricePerOunce} - Ounces: {Ounces}";
            }
        }

        public override string ReceiptDisplay
        {
            get
            {
                return $"[{Name}] - PricePerOunce: ${PricePerOunce} - Ounces: {Ounces} - Item Total: ${PricePerOunce * Ounces}";
            }
        }
    }
}
