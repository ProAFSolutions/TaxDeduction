using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxDedutions.ViewModels;
using Xamarin.Forms;

namespace TaxDedutions
{
    public partial class DetailRecord : ContentPage
    {
        DetailViewModel model;

        

        public DetailRecord(RecordItemList record)
        {
            InitializeComponent();
            model = new DetailViewModel(record);
            model.Navigation = Navigation;
            model.Page = this;
            this.BindingContext = model;

        }
    }
}
