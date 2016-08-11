using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxDedutions.DB;
using TaxDedutions.Models;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace TaxDedutions.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        #region Fields

        private int totalInvoices;

        private string totalAmount;

        public Dictionary<string, Color> Deductions;

        private RecordDatabase db;

        #endregion

        #region Constructors

        public MainViewModel()
        {
          //  totalAmount = "$0";
            db = new RecordDatabase();
            var invoices = db.GetRecords().ToList();
            TotalInvoices = invoices.Count();
            totalAmount = "$"+invoices.Sum(x => decimal.Parse(x.Amount)).ToString();
        }

        #endregion

        #region Properties

        public int TotalInvoices
        {
            get { return totalInvoices; }
            set
            {
                totalInvoices = value;
                OnPropertyChanged("TotalInvoices");
            }
        }

        public string TotalAmount
        {
            get { return totalAmount; }
            set
            {
                totalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }

        public INavigation Navigation { get; set; }

        #endregion


        #region Commands

        private Command addCommand;
        public Command AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new Command(async () => await AddRecord()));
            }
        }
        private async Task AddRecord()
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new EntryRecord());
           
        }

        private Command listCommand;
        public Command ListCommand
        {
            get
            {
                return listCommand ?? (listCommand = new Command(async () => await RecordList()));
            }
        }
        private async Task RecordList()
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new RecordList());

        }

        #endregion

    }
}
