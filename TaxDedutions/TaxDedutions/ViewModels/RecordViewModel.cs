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
    public class RecordViewModel : BaseViewModel
    {

        #region Fields

        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private RecordDatabase db;
        private string name = string.Empty;
        private DateTime date = DateTime.Now;
        private string ImageBase64;
        private ImageSource image;
        private string amount;
        private string type;
        private string description = string.Empty;
        private string note = string.Empty;
        private bool hasImage;
        private int selectIndex;

        public Dictionary<string, Color> Deductions;
      
        #endregion

        #region Constructors

        public RecordViewModel()
        {
           Deductions = new Dictionary<string, Color>
        {
            { "Uncategorized", Color.Red },
            { "Medical and Dental Expenses", Color.Aqua },
            { "Deductible Taxes", Color.Blue },
            { "Home Mortgage Points", Color.Gray },
            { "Interest Expense", Color.Lime },
            { "Charitable Contributions", Color.Navy },
            { "Miscellaneous Expenses", Color.Purple },
            { "Business Use of Home", Color.Silver },
            { "Business Use of Car", Color.White },
            { "Business Travel Expenses", Color.Yellow },
            { "Business Entertainment Expenses", Color.Green },
            { "Work-Related Education Expenses", Color.Pink },
            { "Employee Business Expenses", Color.Silver },
            { "Casualty, Disaster, and Theft Losses", Color.Teal }
        };

            db = new RecordDatabase();
        }

        #endregion

        #region Properties

        public EntryRecord EntryRecordPage { get; set; }

        public INavigation Navigation { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        public bool HasImage
        {
            get
            {
                return hasImage;
            }
            set
            {
                hasImage = value;
                OnPropertyChanged("HasImage");
                OnPropertyChanged("IsVisible");
            }
        }
        public bool IsVisible
        {
            get
            {
                return !hasImage;
            }
        }
        public string Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                OnPropertyChanged("Note");
            }
        }
        public int SelectIndex
        {
            get { return selectIndex; }
            set
            {
                selectIndex = value;
                OnPropertyChanged("SelectIndex");
            }
        }

        #endregion

        #region Commands

        private Command selectPictureCommand;
        public Command SelectPictureCommand
        {
            get
            {
                return selectPictureCommand ?? (selectPictureCommand = new Command(async () => await SelectPicture()));
            }
        }
        private async Task SelectPicture()
        {
            IsBusy = true;


            ImageSource imageSource;
            IMediaPicker mediaPicker;
            //  Image img;

            // img = new Image() { HeightRequest = 300, WidthRequest = 300, BackgroundColor = Color.FromHex("#D6D6D2"), Aspect = Aspect.AspectFit };

            var device = Resolver.Resolve<IDevice>();
            mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;

            try
            {
                var mediaFile = await mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400
                });
                imageSource = ImageSource.FromStream(() => mediaFile.Source);
                Image = imageSource;
                HasImage = true;

                using (var memoryStream = new MemoryStream())
                {
                    mediaFile.Source.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    ImageBase64 = Convert.ToBase64String(imageBytes);

                }

                IsBusy = false;

               // Navigation.PopAsync();


            }
            catch (System.Exception ex)
            {
                // this.status = ex.Message;
            }



        }


        private Command takePictureCommand;
        public Command TakePictureCommand
        {
            get
            {
                return takePictureCommand ?? (takePictureCommand = new Command(async () => await TakePicture()));
            }
        }
        private async Task TakePicture()
        {
            IsBusy = true;


            ImageSource imageSource;
            IMediaPicker mediaPicker;
            //  Image img;

            // img = new Image() { HeightRequest = 300, WidthRequest = 300, BackgroundColor = Color.FromHex("#D6D6D2"), Aspect = Aspect.AspectFit };

            var device = Resolver.Resolve<IDevice>();
            mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;

            try
            {
                //var mediaFile = await mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions
                //{
                //    DefaultCamera = CameraDevice.Front,
                //    MaxPixelDimension = 400
                //});
                //imageSource = ImageSource.FromStream(() => mediaFile.Source);
                //Image = imageSource;


                //using (var memoryStream = new MemoryStream())
                //{
                //    mediaFile.Source.CopyTo(memoryStream);
                //    byte[] imageBytes = memoryStream.ToArray();

                //    imageBase64 = Convert.ToBase64String(imageBytes);

                //}

                IsBusy = false;

                //  Navigation.PopAsync();

                var media = mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400
                }).ContinueWith(t => {
                    if (t.IsFaulted)
                    {
                        //this.status = t.Exception.InnerException.ToString();
                    }
                    else if (t.IsCanceled)
                    {
                       // this.status = "Canceled";
                    }
                    else
                    {
                        var mediaFile = t.Result;
                        imageSource = ImageSource.FromStream(() => mediaFile.Source);
                        Image = imageSource;
                        HasImage = true;
                        //    return mediaFile;
                    }

                    //  return null;
                }, _scheduler);


            }
            catch (System.Exception ex)
            {
                // this.status = ex.Message;
            }



        }

        private Command saveCommand;
        public Command SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new Command(async () => await SaveRecord()));
            }
        }
        private async Task SaveRecord()
        {
      
            IsBusy = true;
            if (!Validations()) return;

            Record record = new Record();
            record.Amount = amount;
            record.Date = Date.ToString("s");
            record.DateCreate = DateTime.Now;
            record.Description = description;
            if (HasImage)
            {
                record.Image = ImageBase64;
                record.HasImage = true;
            }
            record.Name = Name;

            record.Type = Deductions.Keys.ElementAt(SelectIndex);

            db.AddRecord(record);

            EntryRecordPage.DisplayAlert("New Invoice", "Saved", "OK");

            await Navigation.PushModalAsync(new MainPage());

        }

        private bool Validations()
        {
            if (string.IsNullOrEmpty(Name))
            {
                EntryRecordPage.DisplayAlert("Validation", "The Name is required.", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(Description))
            {
                EntryRecordPage.DisplayAlert("Validation", "The Description is required.", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(Amount))
            {
                EntryRecordPage.DisplayAlert("Validation", "The Amount is required.", "OK");
                return false;
            }

            return true;
        }

        #endregion

    }
}
