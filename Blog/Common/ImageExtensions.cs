using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace Blog.Common
{
    public static class ImageExtensions
    {
        public static Image BestFit(this Image image, int height, int width)
        {
            return image.Height > image.Width
                ? image.ConstrainProportions(height, Dimensions.Height)
                : image.ConstrainProportions(width, Dimensions.Width);
        }
        public static Image ConstrainProportions(this Image imgPhoto, int size, Dimensions dimension)
        {
            var sourceWidth = imgPhoto.Width;
            var sourceHeight = imgPhoto.Height;
            var sourceX = 0;
            var sourceY = 0;
            var destX = 0;
            var destY = 0;
            float nPercent;

            switch (dimension)
            {
                case Dimensions.Width:
                    nPercent = (float)size / (float)sourceWidth;
                    break;
                default:
                    nPercent = (float)size / (float)sourceHeight;
                    break;
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.CompositingMode = CompositingMode.SourceCopy;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.SmoothingMode = SmoothingMode.HighQuality;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

            try
            {
                grPhoto.DrawImage(imgPhoto,
                    new Rectangle(destX, destY, destWidth, destHeight),
                    new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                    GraphicsUnit.Pixel
                );
            }
            finally
            {
                grPhoto.Dispose();
            }
            return bmPhoto;
        }
    }
    public enum Dimensions
    {
        Width,
        Height
    }
}