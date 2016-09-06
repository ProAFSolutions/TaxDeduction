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
    public class TaxesDeductionsViewModel : BaseViewModel
    {

        #region Fields

        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private RecordDatabase db;
        private int selectIndex;

        public List<TaxList> deductions;
      
        #endregion

        #region Constructors

        public TaxesDeductionsViewModel()
        {
            //   Deductions = new Dictionary<string, Color>
            //{
            //    { "Uncategorized", Color.Red },
            //    { "Medical and Dental Expenses", Color.Aqua },
            //    { "Deductible Taxes", Color.Blue },
            //    { "Home Mortgage Points", Color.Gray },
            //    { "Interest Expense", Color.Lime },
            //    { "Charitable Contributions", Color.Navy },
            //    { "Miscellaneous Expenses", Color.Purple },
            //    { "Business Use of Home", Color.Silver },
            //    { "Business Use of Car", Color.White },
            //    { "Business Travel Expenses", Color.Yellow },
            //    { "Business Entertainment Expenses", Color.Green },
            //    { "Work-Related Education Expenses", Color.Pink },
            //    { "Employee Business Expenses", Color.Silver },
            //    { "Casualty, Disaster, and Theft Losses", Color.Teal }
            //};

            var list = Constants.DeductionsLinks.Where(y => y.Key != "Uncategorized").ToList().Select(x => new TaxList()
            {
                Name = x.Key,
                Image = ImageSource.FromFile("Link.png"),
               Link = x.Value

        }).ToList();
            //   Deductions.Remove("Uncategorized");
            Deductions = list;
            db = new RecordDatabase();
        }

        #endregion

        #region Properties

        public EntryRecord EntryRecordPage { get; set; }

        public int SelectIndex
        {
            get { return selectIndex; }
            set
            {
                selectIndex = value;
                OnPropertyChanged("SelectIndex");
            }
        }

        public List<TaxList> Deductions
        {
            get { return deductions; }
            set
            {
                deductions = value;
                OnPropertyChanged("Deductions");
            }
        }

        #endregion

        #region Commands

        #endregion

    }

    public class TaxList
    {
        public string Name { get; set; }
        public ImageSource Image {get;set;}
        public string Link { get; set; }
    }
}
