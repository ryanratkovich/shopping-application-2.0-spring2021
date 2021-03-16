using Assignment3.ViewModels;
using Assignment3.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Assignment3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (File.Exists($"{AppDataPaths.GetDefault().LocalAppData}\\saveFile.txt"))
            {
                var text = File.ReadAllText($"{AppDataPaths.GetDefault().LocalAppData}\\saveFile.txt");
                DataContext = JsonConvert.DeserializeObject<MainViewModel>(text, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto    //Using this setting allows the DataContext to accurately read types of polymorphic collections (instead of defaulting all objects to Product)
                });
            }
            else
            {
                DataContext = new MainViewModel();
            }
        }

        private void Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as MainViewModel).AddToCart();
        }

        private void ShoppingCart_SelectionChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).RemoveFromCart();
        }

        private async void CheckOut_Click(object sender, RoutedEventArgs e)   //Displays receipt with line items and writes to disk
        {
            var diag = new CheckoutDialog(DataContext as MainViewModel);
            await diag.ShowAsync();
        }
    }
}
