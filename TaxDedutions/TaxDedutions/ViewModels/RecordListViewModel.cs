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
        private int selectIndex;
        private int selectIndexType;
        #endregion

        #region Constructors

        public RecordListViewModel()
        {
            IsBusy = true;
            GetAllRecords();
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

        public int SelectIndexYear
        {
            get { return selectIndex; }
            set
            {
                selectIndex = value;
                OnPropertyChanged("SelectIndexYear");
            }
        }

        public int SelectIndexType
        {
            get { return selectIndexType; }
            set
            {
                selectIndexType = value;
                OnPropertyChanged("SelectIndexType");
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

        public void GetAllRecords()
        {
            db = new RecordDatabase();
            Records = db.GetRecords().ToList().Select(
                x => new RecordItemList()
                {
                    Name = x.Name,
                    Concat = x.Name + " (Amount: $" + x.Amount.ToString() + ")",
                    Description = x.Description,
                    Image = String64toImage(x.Image),
                    ID = x.ID,
                    Date = x.Date,
                    Amount = x.Amount,
                    Type = x.Type,
                    HasImage = x.HasImage

                }
                ).ToList();

            for (int i = 0; i < Records.Count(); i++)
            {
                Records[i].Number = (i + 1).ToString();
                if (Records[i].Number.Length == 1)
                    Records[i].Number = Records[i].Number + " ";
            }

            IsBusy = false;

        }

        public void GetRecordsByQuery(bool byDate, string date, bool byType, string type)
        {
            db = new RecordDatabase();
            Records = db.GetRecords().ToList().Select(
                x => new RecordItemList()
                {
                    Name = x.Name,
                    Concat = x.Name + " (Amount: $" + x.Amount.ToString() + ")",
                    Description = x.Description,
                    Image = String64toImage(x.Image),
                    ID = x.ID,
                    Date = x.Date,
                    Amount = x.Amount,
                    Type = x.Type,
                    HasImage = x.HasImage

                }
                ).ToList();

            for (int i = 0; i < Records.Count(); i++)
            {
                Records[i].Number = (i + 1).ToString();
                if (Records[i].Number.Length == 1)
                    Records[i].Number = Records[i].Number + " ";
            }

            if(Records.Count()>0)
            {
                if (byDate)
                {
                    //Records = Records.Where(x=>x.)
                }

                if (byType)
                {
                    Records = Records.Where(x => x.Type == type).ToList();
                }
            }

            IsBusy = false;

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

        public string Concat { get; set; }

        public string Number { get; set; }
        public RecordItemList()
        {

        }
    }
}
