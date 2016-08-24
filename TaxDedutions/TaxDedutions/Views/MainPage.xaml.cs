using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxDedutions.ViewModels;
using Xamarin.Forms;

namespace TaxDedutions
{
    public partial class MainPage : ContentPage
    {
        MainViewModel model;
        public MainPage()
        {
            InitializeComponent();
            model = new MainViewModel();
            model.Navigation = Navigation;
            this.BindingContext = model;
        }
    }
}
