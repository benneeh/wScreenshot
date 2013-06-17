using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace wScreenshot.Helper
{
    public static class BitmapHelper
    {
        public static void Save(Stream imageStream, BitmapMetadata metaData, Stream targetStream)
        {
            BitmapDecoder imgDecoder = BitmapDecoder.Create(imageStream, BitmapCreateOptions.None,
                BitmapCacheOption.Default);
            BitmapFrame img = imgDecoder.Frames[0];
            var bmpEnc = new BmpBitmapEncoder();
            if (metaData != null)
            {
                bmpEnc.Metadata = metaData;
            }
            else
            {
                bmpEnc.Metadata = imgDecoder.Metadata;
            }
            bmpEnc.Frames.Add(img);
            bmpEnc.Save(targetStream);

            GifBitmapEncoder gifEnc;
            JpegBitmapEncoder jpgEnc;
            PngBitmapEncoder pngEnc;
            TiffBitmapEncoder tiffEnc;
            WmpBitmapEncoder wmpEnc;
        }

        public static BitmapImage Load(string path)
        {
            var bm = new BitmapImage(new Uri(path));
            bm.Freeze();

            BitmapDecoder imgDecoder = BitmapDecoder.Create(new Uri(path), BitmapCreateOptions.None,
                BitmapCacheOption.Default);

            //imgDecoder.
            return null;
        }
    }
}