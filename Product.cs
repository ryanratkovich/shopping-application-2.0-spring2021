using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.Models
{
    public class Product: INotifyPropertyChanged
    {
        public virtual double Price { get; set; }   //Overridable property
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public Product(double price, string name, string des, int id)
        {
            Price = price;
            Name = name;
            Description = des;
            Id = id;
        }

        public virtual string ShoppingCartDisplay   //Overridable property for displaying object in ShoppingCart
        {
            get
            {
                return $"{Name} - Price: ${Price}";
            }
        }

        public virtual string ReceiptDisplay    //Overridable property for displaying object in Receipt
        {
            get
            {
                return $"{Name} - Price: ${Price}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")   //Method has protected modifier so that derived classes have access
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
