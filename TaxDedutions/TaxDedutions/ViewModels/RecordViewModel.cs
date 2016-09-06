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

            Deductions = Constants.Deductions;

            db = new RecordDatabase();
        }

        #endregion

        #region Properties

        public EntryRecord EntryRecordPage { get; set; }

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


                //Convert Base 64
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

            var device = Resolver.Resolve<IDevice>();
            mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;

            try
            {

                IsBusy = false;

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
                        //imageSource = ImageSource.FromStream(() => mediaFile.Source);
                        //Image = imageSource;
                        // HasImage = true;

                        //    return mediaFile;
                        //Compress
                        using (var memoryStream = new MemoryStream())
                        {
                            mediaFile.Source.CopyTo(memoryStream);
                            memoryStream.Position = 0;
                            var ImageBytes = memoryStream.ToArray();

                            var picInfo = ExifReader.ReadJpeg(memoryStream);
                            var resize = DependencyService.Get<IImageResizer>();
                            if (picInfo.Width > 600 && picInfo.Height > 600)
                            {
                                var values = MaxResizeImage(picInfo.Width, picInfo.Height, 600, 700);
                                ImageBytes = resize.ResizeImage(ImageBytes, values[0], values[1]);
                            }

                            //Rotate
                            if (Device.OS == TargetPlatform.Android)
                            {
                                using (Stream streamPic = mediaFile.Source)
                                {
                                    // var picInfo = ExifReader.ReadJpeg(streamPic);
                                    ExifOrientation orientation = picInfo.Orientation;

                                    switch (orientation)
                                    {
                                        case ExifOrientation.TopRight:
                                            // ImageVisual.RotateTo(90.0);
                                            ImageBytes = resize.RotateImage(ImageBytes, 90);
                                            break;
                                        case ExifOrientation.BottomRight:
                                            // ImageVisual.RotateTo(180.0);
                                            ImageBytes = resize.RotateImage(ImageBytes, 180);
                                            break;
                                        case ExifOrientation.BottomLeft:
                                            // ImageVisual.RotateTo(270.0);
                                            ImageBytes = resize.RotateImage(ImageBytes, 270);
                                            break;
                                    }

                                }
                            }

                            imageSource = ImageSource.FromStream(() => new MemoryStream(ImageBytes));
                            Image = imageSource;
                            HasImage = true;
                            //Convert Base 64
                            ImageBase64 = Convert.ToBase64String(ImageBytes);
                        }
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

            await EntryRecordPage.DisplayAlert("Invoice", "The new invoice was saved.", "OK");

            //Clean
            Amount = null;
            Description = "";
            HasImage = false;
            ImageBase64 = null;
            Name = null;
            Type = null;
            Image = null;
            SelectIndex = 0;
            OnPropertyChanged("IsVisible");


            await Navigation.PopAsync();
            IsBusy = false;
            return;

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

        public static List<float> MaxResizeImage(float Width, float Height, float maxWidth, float maxHeight)
        {
            List<float> result = new List<float>();
            result.Add(Width);
            result.Add(Height);

            var maxResizeFactor = Math.Min(maxWidth / Width, maxHeight / Height);
            if (maxResizeFactor > 1) return result;
            var width = maxResizeFactor * Width;
            var height = maxResizeFactor * Height;

            result[0] = width;
            result[1] = height;

            return result;
        }

        #endregion

    }
}
