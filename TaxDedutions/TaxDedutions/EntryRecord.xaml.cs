using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxDedutions.ViewModels;
using Xamarin.Forms;

namespace TaxDedutions
{
    public partial class EntryRecord : ContentPage
    {
        RecordViewModel model;

        

        public EntryRecord()
        {
            InitializeComponent();
            model = new RecordViewModel();
            model.EntryRecordPage = this;
            model.Navigation = Navigation;
            this.BindingContext = model;

            foreach (string item in model.Deductions.Keys)
            {
                this.PickerType.Items.Add(item);
            }
            model.SelectIndex = 0;

            this.Amount.SetBinding(Entry.TextProperty, "Amount");
            this.Date.SetBinding(DatePicker.DateProperty, "Date");
            this.PickerType.SetBinding(Picker.TitleProperty, "Type");
        }
    }
}
