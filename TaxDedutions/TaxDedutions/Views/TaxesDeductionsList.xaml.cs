using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxDedutions.ViewModels;
using Xamarin.Forms;

namespace TaxDedutions.Views
{
    public partial class TaxesDeductionsList : ContentPage
    {
        TaxesDeductionsViewModel model;
        public TaxesDeductionsList()
        {
            InitializeComponent();
            model = new TaxesDeductionsViewModel();
            model.Navigation = Navigation;
            this.BindingContext = model;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            /* var record = e.SelectedItem as RecordItemList;

             var detail = new DetailRecord(record);
             await Navigation.PushAsync(detail);*/

            if (((ListView)sender).SelectedItem == null) return;

            var tax = e.SelectedItem as TaxList;
            ((ListView)sender).SelectedItem = null;

            Uri uri = new Uri(tax.Link);
            Device.OpenUri(uri);

        }

        protected override void OnAppearing()
        {
           
        }

    }
}
