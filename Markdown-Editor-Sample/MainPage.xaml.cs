using MarkdownEditBox.Models;
using Richasy.Helper.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Markdown_Editor_Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        private Instance _instance = new Instance("MarkdownSample");
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void MyNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;
            string tag = item.Tag.ToString();
            switch (tag)
            {
                case "1":
                    MainFrame.Navigate(typeof(Pages.Scenario1));
                    break;
                case "2":
                    MainFrame.Navigate(typeof(Pages.Scenario2));
                    break;
                case "3":
                    MainFrame.Navigate(typeof(Pages.Scenario3));
                    break;
                case "4":
                    MainFrame.Navigate(typeof(Pages.Scenario4));
                    break;
                default:
                    break;
            }
        }
    }
}
