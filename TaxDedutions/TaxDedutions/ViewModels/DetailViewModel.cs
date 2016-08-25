using ExifLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxDedutions.DB;
using TaxDedutions.Models;
using TaxDedutions.Services;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace TaxDedutions.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {

        #region Fields

        private RecordItemList record;

        #endregion

        #region Constructors

        public DetailViewModel(RecordItemList _record)
        {
            Record = _record;
        }

        #endregion

        #region Properties


        public RecordItemList Record
        {
            get { return record; }
            set
            {
                record = value;
                OnPropertyChanged("Record");
            }
        }

        #endregion

        #region Commands

        private Command removeCommand;
        public Command RemoveCommand
        {
            get
            {
                return removeCommand ?? (removeCommand = new Command(async () => await Remove()));
            }
        }
        private async Task Remove()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await Page.DisplayAlert("Confirmation", "Do you want to remove this record?", "Yes", "No");
                if (result)
                {
                    //Remove invoice
                    RecordDatabase db = new RecordDatabase();
                    db.DeleteRecord(Record.ID);
                    await this.Navigation.PopAsync();
                }
            });

        }

        #endregion

    }
}
