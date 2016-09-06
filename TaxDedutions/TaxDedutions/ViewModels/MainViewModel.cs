using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxDedutions.DB;
using TaxDedutions.Models;
using TaxDedutions.Views;
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

        private List<DeductionsExpensesListViewModel> graph1;

        private bool isVisibleGraph1 = true;

        #endregion

        #region Constructors

        public MainViewModel()
        {
          //  totalAmount = "$0";
            db = new RecordDatabase();
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

        public List<DeductionsExpensesListViewModel> Graph1
        {
            get
            {
                return graph1;
            }
            set
            {
                graph1 = value;
                OnPropertyChanged("Graph1");
            }
        }

        public bool IsVisibleGraph1
        {
            get
            {
                return isVisibleGraph1;
            }
            set
            {
                isVisibleGraph1 = value;
                OnPropertyChanged("IsVisibleGraph1");
            }
        }

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

            EntryRecord record = new EntryRecord();
            //navigation.BarBackgroundColor = Color.Transparent;
            //Detail = navigation;

            await Navigation.PushAsync(record);
           
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
            await Navigation.PushAsync(new RecordList());

        }

        #endregion

        #region Aux

        public void LoadData()
        {
            var invoices = db.GetRecords().ToList();
            TotalInvoices = invoices.Count();
            TotalAmount = "$" + invoices.Sum(x => decimal.Parse(x.Amount)).ToString();
            LoadGraph();
        }

        private void LoadGraph()
        {
            var records = db.GetRecords().ToList().GroupBy(x => x.Type).Select(cl => new DeductionsExpensesListViewModel
            {
                Name = cl.First().Type,
                Expense = "$"+cl.Sum(c=>float.Parse(c.Amount)).ToString()
            }).ToList();

            Graph1 = records;
            IsVisibleGraph1 = (records.Count > 0) ? true : false;
        }


        #endregion

    }
}
