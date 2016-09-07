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

            int year = 2016;
            int currentyear = DateTime.Now.Year;
            do
            {
                this.PickerYear.Items.Add(year.ToString());
                year++;
            } while (year < currentyear);

            model.SelectIndexYear = 0;

            for (int i = 0; i < Constants.Deductions.Count(); i++)
            {
                this.PickerType.Items.Add(Constants.Deductions.ElementAt(i).Key);
            }
            model.SelectIndexType = 0;


        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var record = e.SelectedItem as RecordItemList;

            var detail = new DetailRecord(record);
            await Navigation.PushAsync(detail);
        }

        protected override void OnAppearing()
        {
            GetRecords();
        }

        private void btnSearch_Clicked(object sender, EventArgs e)
        {
            GetRecords();
        }

        private void GetRecords()
        {
            if (model != null)
                if (!checkDate.Checked && !checkType.Checked)
                    model.GetAllRecords();
                else
                {
                    model.GetRecordsByQuery(checkDate.Checked, this.PickerYear.Items[model.SelectIndexYear],
                        checkType.Checked, this.PickerType.Items[model.SelectIndexType]);
                }
        }

    }
}
