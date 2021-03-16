using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Inventory { get; set; }    //Contains all products
        public Product SelectedProduct { get; set; }    //Represents product that the user has highlighted
        public ObservableCollection<Product> ShoppingCart { get; set; } //Stores products that user is purchasing

        public string SubTotal => $"SubTotal: {ShoppingCart.Sum(i => i.Price):C}";

        public string SalesTax => $"Sales Tax (7%): {ShoppingCart.Sum(i => i.Price) * 0.07:C}";

        public string Total => $"Total: {(ShoppingCart.Sum(i => i.Price) + ShoppingCart.Sum(i => i.Price)*0.07):C}";

        public MainViewModel()
        {
            Inventory = new ObservableCollection<Product>();
            ShoppingCart = new ObservableCollection<Product>();

            
            //Add 10 unique ProductByQuantity objects to Inventory
            Inventory.Add(new ProductByQuantity(0.25, "ProductByQuantity1", "A", 0, 0.25, 1));
            Inventory.Add(new ProductByQuantity(0.50, "ProductByQuantity2", "B", 1, 0.5, 1));
            Inventory.Add(new ProductByQuantity(1.00, "ProductByQuantity3", "C", 2, 1, 1));
            Inventory.Add(new ProductByQuantity(1.50, "ProductByQuantity4", "D", 3, 1.5, 1));
            Inventory.Add(new ProductByQuantity(2.00, "ProductByQuantity5", "E", 4, 2, 1));
            Inventory.Add(new ProductByQuantity(2.50, "ProductByQuantity6", "F", 5, 2.5, 1));
            Inventory.Add(new ProductByQuantity(3.00, "ProductByQuantity7", "G", 6, 3, 1));
            Inventory.Add(new ProductByQuantity(3.50, "ProductByQuantity8", "H", 7, 3.5, 1));
            Inventory.Add(new ProductByQuantity(4.00, "ProductByQuantity9", "I", 8, 4, 1));
            Inventory.Add(new ProductByQuantity(4.50, "ProductByQuantity10", "J", 9, 4.5, 1));

            //Add 10 unique ProductByWeight objects to Inventory
            Inventory.Add(new ProductByWeight(5.00, "ProductByWeight1", "K", 10, 5.00, 1));
            Inventory.Add(new ProductByWeight(5.50, "ProductByWeight2", "L", 11, 5.50, 1));
            Inventory.Add(new ProductByWeight(6.00, "ProductByWeight3", "M", 12, 6.00, 1));
            Inventory.Add(new ProductByWeight(6.50, "ProductByWeight4", "N", 13, 6.50, 1));
            Inventory.Add(new ProductByWeight(7.00, "ProductByWeight5", "O", 14, 7.00, 1));
            Inventory.Add(new ProductByWeight(7.50, "ProductByWeight6", "P", 15, 7.50, 1));
            Inventory.Add(new ProductByWeight(8.00, "ProductByWeight7", "Q", 16, 8.00, 1));
            Inventory.Add(new ProductByWeight(8.50, "ProductByWeight8", "R", 17, 8.50, 1));
            Inventory.Add(new ProductByWeight(9.00, "ProductByWeight9", "S", 18, 9.00, 1));
            Inventory.Add(new ProductByWeight(9.50, "ProductByWeight10", "T", 19, 9.50, 1));
            
        }

        public void AddToCart() //Adds product to ShoppingCart
        {
            if (SelectedProduct == null)
            {
                return;
            }
            if (SelectedProduct is ProductByQuantity)
            {
                if (ShoppingCart.Any(i => i.Id.Equals(SelectedProduct.Id))) //If product is in cart, increment amount
                {
                    (ShoppingCart.FirstOrDefault(i => i.Id.Equals(SelectedProduct.Id)) as ProductByQuantity).Units++;
                }
                else //Else add to cart
                {
                    ShoppingCart.Add(new ProductByQuantity(SelectedProduct.Price, SelectedProduct.Name, SelectedProduct.Description, SelectedProduct.Id, (SelectedProduct as ProductByQuantity).UnitPrice, (SelectedProduct as ProductByQuantity).Units));
                }
            } 
            else if (SelectedProduct is ProductByWeight)
            {
                if (ShoppingCart.Any(i => i.Id.Equals(SelectedProduct.Id)))
                {
                    (ShoppingCart.FirstOrDefault(i => i.Id.Equals(SelectedProduct.Id)) as ProductByWeight).Ounces++;
                }
                else
                {
                    ShoppingCart.Add(new ProductByWeight(SelectedProduct.Price, SelectedProduct.Name, SelectedProduct.Description, SelectedProduct.Id, (SelectedProduct as ProductByWeight).PricePerOunce, (SelectedProduct as ProductByWeight).Ounces));
                }
            }
            SelectedProduct = null;
            NotifyPropertyChanged("SelectedProduct");
            NotifyPropertyChanged("SubTotal");
        }

        public void RemoveFromCart() //Removes product from ShoppingCart
        {
            if (SelectedProduct == null)
            {
                return;
            }
            if (SelectedProduct is ProductByQuantity)
            {
                if (ShoppingCart.Any(i => i.Id.Equals(SelectedProduct.Id))) //If product is in cart, decrement amount
                {
                    if ((ShoppingCart.FirstOrDefault(i => i.Id.Equals(SelectedProduct.Id)) as ProductByQuantity).Units > 1)
                    {
                        (ShoppingCart.FirstOrDefault(i => i.Id.Equals(SelectedProduct.Id)) as ProductByQuantity).Units--;
                    }
                    else //Else remove the product entirely
                    {
                        ShoppingCart.Remove(SelectedProduct);
                    }
                }
            }
            else if (SelectedProduct is ProductByWeight)
            {
                if (ShoppingCart.Any(i => i.Id.Equals(SelectedProduct.Id)))
                {
                    if ((ShoppingCart.FirstOrDefault(i => i.Id.Equals(SelectedProduct.Id)) as ProductByWeight).Ounces > 1)
                    {
                        (ShoppingCart.FirstOrDefault(i => i.Id.Equals(SelectedProduct.Id)) as ProductByWeight).Ounces--;
                    }
                    else
                    {
                        ShoppingCart.Remove(SelectedProduct);
                    }
                }
            }
            SelectedProduct = null;
            NotifyPropertyChanged("SelectedProduct");
            NotifyPropertyChanged("SubTotal");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
