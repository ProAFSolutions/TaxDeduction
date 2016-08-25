using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaxDedutions.Views
{
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }

        public MenuPage()
        {
           // Icon = "settings.png";
            Title = "menu"; // The Title property must be set.
            Icon = "sideMenu.png";
            BackgroundColor = Color.White;

            Menu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.FromHex("#68217A"),
                    Text = "LOGO",
                    FontSize=24,
                }
            };

            var layout = new StackLayout
            {
                Spacing = 5,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, 26, 0, 5)
            };
            layout.Children.Add(menuLabel);
            layout.Children.Add(Menu);



            var layoutCompany = new StackLayout
            {
                Spacing = 5,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Orientation = StackOrientation.Horizontal
            };

           // Image imgUser = new Image();
           // imgUser.Source = ImageSource.FromFile("UserMenu.png");
           // layoutUser.Children.Add(imgUser);

            var userLabel = new ContentView
            {
                Content = new Label
                {
                    TextColor = Color.FromHex("BFBFBF"),
                    //Text = "test11@jlanmobile.com",
                    Text = "ProAFSolutions",
                    FontSize = 20
                }
            };

            layoutCompany.Children.Add(userLabel);
            layout.Children.Add(layoutCompany);

            Content = layout;
        }
    }
}
