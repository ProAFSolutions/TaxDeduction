﻿using System;
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
            BackgroundColor = Color.White;

            Menu = new MenuListView();

           /* var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.FromHex("AAAAAA"),
                    Text = "MENU",
                }
            };*/

            var layout = new StackLayout
            {
                Spacing = 5,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, 26, 0, 5)
            };
          //  layout.Children.Add(menuLabel);
            layout.Children.Add(Menu);

            Content = layout;
        }
    }
}
