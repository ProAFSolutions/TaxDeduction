using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaxDedutions.Views
{
    public class RootPage : MasterDetailPage
    {
        MenuPage menuPage;

        public RootPage()
        {
            menuPage = new MenuPage();

            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

            Master = menuPage;
            NavigationPage navigation = new NavigationPage(new MainPage());
            navigation.BarBackgroundColor = Color.Transparent;
            Detail = navigation;
        }

        void NavigateTo(MenuItem menu)
        {
            if (menu == null)
                return;

            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            NavigationPage navigation = new NavigationPage(displayPage);
            navigation.BarBackgroundColor = Color.Transparent;
            Detail = navigation;

            menuPage.Menu.SelectedItem = null;
            IsPresented = false;
        }
    }
}
