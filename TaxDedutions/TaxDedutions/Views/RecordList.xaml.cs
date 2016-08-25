using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxDedutions.ViewModels;
using Xamarin.Forms;

namespace TaxDedutions.Views
{
    public partial class RecordList : ContentPage
    {
        RecordListViewModel model;
        public RecordList()
        {
            InitializeComponent();
            model = new RecordListViewModel();
            model.Navigation = Navigation;
            this.BindingContext = model;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var record = e.SelectedItem as RecordItemList;

            var detail = new DetailRecord(record);
            await Navigation.PushAsync(detail);
        }

        protected override void OnAppearing()
        {
            if (model != null)
                model.GetRecords();
        }

    }
}
