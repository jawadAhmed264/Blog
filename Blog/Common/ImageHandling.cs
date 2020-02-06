using HtmlAgilityPack;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Blog.Common
{
    public class ImageHandling
    {
        //ImageResize
        public static void ImageResizeandSave(int ImageSize, string OriginalPath, string TargetPath)
        {
            // Loads original image from file
            Image imgOriginal = Image.FromFile(OriginalPath);
            // Finds height and width of original image
            float OriginalHeight = imgOriginal.Height;
            float OriginalWidth = imgOriginal.Width;
            // Finds height and width of resized image
            int ThumbnailWidth;
            int ThumbnailHeight;
            if (OriginalHeight > OriginalWidth)
            {
                ThumbnailHeight = ImageSize;
                ThumbnailWidth = (int)((OriginalWidth / OriginalHeight) * (float)ImageSize);
            }
            else
            {
                ThumbnailWidth = ImageSize;
                ThumbnailHeight = (int)((OriginalHeight / OriginalWidth) * (float)ImageSize);
            }
            // Create new bitmap that will be used for thumbnail
            Bitmap ThumbnailBitmap = new Bitmap(ThumbnailWidth, ThumbnailHeight);
            Graphics ResizedImage = Graphics.FromImage(ThumbnailBitmap);
            // Resized image will have best possible quality
            ResizedImage.InterpolationMode = InterpolationMode.HighQualityBicubic;
            ResizedImage.CompositingQuality = CompositingQuality.HighQuality;
            ResizedImage.SmoothingMode = SmoothingMode.HighQuality;
            // Draw resized image
            ResizedImage.DrawImage(imgOriginal, 0, 0, ThumbnailWidth, ThumbnailHeight);
            // Save thumbnail to file
            ThumbnailBitmap.Save(TargetPath);
        }
        public static List<string> getImageNames(List<string> imageSrc)
        {
            List<string> ImageNames = new List<string>();
            ImageNames.Clear();
            if (imageSrc != null)
            {
                ImageNames.Clear();
                foreach (string name in imageSrc)
                {
                    string[] splitArray = name.Split('/');
                    string ImageName = splitArray.Last();
                    ImageNames.Add(ImageName);
                }
                return ImageNames;
            }
            return ImageNames;
        }
        public static List<string> getImageSourceList(string html)
        {

            List<string> srcList = new List<string>();
            srcList.Clear();
            srcList.Clear();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(@"" + html);
            var ImageTags = doc.DocumentNode.SelectNodes("//img[@src]");
            if (ImageTags != null)
            {
                foreach (HtmlNode link in ImageTags)
                {
                    string src = link.GetAttributeValue("src", "").ToString();
                    srcList.Add(src);
                }
            }
            return srcList;
        }
    }
}