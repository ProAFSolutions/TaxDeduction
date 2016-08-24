using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TaxDedutions.Services;
using Android.Graphics;
using System.IO;
using TaxDedutions.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ImageResizer_Android))]
namespace TaxDedutions.Droid
{
    public class ImageResizer_Android : IImageResizer
    {
        public ImageResizer_Android() { }
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);


            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);
                return ms.ToArray();
            }


        }

        public byte[] RotateImage(byte[] imageData, int degree)
        {
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Matrix matrix = new Matrix();
            matrix.PostRotate(degree);

            var rotatedImage = Bitmap.CreateBitmap(originalImage, 0, 0, originalImage.Width, originalImage.Height, matrix, true);
            originalImage.Recycle();

            using (MemoryStream ms = new MemoryStream())
            {
                rotatedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);
                return ms.ToArray();
            }

        }
    }
}