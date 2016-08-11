using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxDedutions.Models;
using TaxDedutions.ViewModels;
using Xamarin.Forms;

namespace TaxDedutions
{
    public partial class RecordList : ContentPage
    {
        RecordListViewModel model;

        public RecordList()
        {
            InitializeComponent();
            model = new RecordListViewModel();
            this.BindingContext = model;

        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var customer = e.SelectedItem as Record;
            //DisplayAlert("Click", "Click on customer: " + customer.Name, "cancel");
        }
    }
}
