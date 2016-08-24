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
    public class RecordListViewModel : BaseViewModel
    {

        #region Fields

        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private RecordDatabase db;
        private List<RecordItemList> records;

        #endregion

        #region Constructors

        public RecordListViewModel()
        {
            GetRecords();
        }

        #endregion

        #region Properties

        public List<RecordItemList> Records
        {
            get
            {
                return records;
            }
            set
            {
                records = value;
                OnPropertyChanged("Records");
            }
        }

        #endregion

        #region Commands

        public ImageSource String64toImage(string value)
        {
            if (string.IsNullOrEmpty(value))
                return ImageSource.FromFile("Invoice.png");
            return ImageSource.FromStream(
              () => new MemoryStream(Convert.FromBase64String(value)));
        }

        public void GetRecords()
        {
            db = new RecordDatabase();
            Records = db.GetRecords().ToList().Select(
                x => new RecordItemList()
                {
                    Name = x.Name + " (Amount: $" + x.Amount.ToString() + ")",
                    Description = x.Description,
                    Image = String64toImage(x.Image),
                    ID = x.ID

                }
                ).ToList();
        }
        #endregion

    }

    public class RecordItemList
        {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public ImageSource Image { get; set; }
        public bool HasImage { get; set; }
        public string Amount { get; set; }
        public string Type { get; set; }

        public string Description { get; set; }
        public RecordItemList()
        {

        }
    }
}
