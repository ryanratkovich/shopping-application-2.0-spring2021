using Assignment3.Models;
using Assignment3.ViewModels;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment3.Dialogs
{
    public sealed partial class CheckoutDialog : ContentDialog
    {
        public CheckoutDialog(MainViewModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as MainViewModel).ShoppingCart.Clear();    //Clear ShoppingCart prior to exiting program
            if (!File.Exists($"{AppDataPaths.GetDefault().LocalAppData}\\saveFile.txt"))    //If file doesn't exist, create file and save to disk
            {
                File.WriteAllText($"{AppDataPaths.GetDefault().LocalAppData}\\saveFile.txt", JsonConvert.SerializeObject(DataContext, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All //Using this setting allows the items in polymorphic collections in MainViewModel to be saved to disk as their true type(s)
                }));
            }
            Environment.Exit(-1);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
