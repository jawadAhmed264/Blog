using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Blog.Common
{
    public class FileHandler
    {
        //Method for Saving Image
        public static void SaveImage(HttpPostedFileBase bannerImage,int size,int chkHeight, int chkWidth, string ImagePath, out string error, out string url)
        {
            if (bannerImage != null)
            {
                HttpPostedFileBase file = bannerImage;
                string extension = Path.GetExtension(file.FileName);
                string fileid = Guid.NewGuid().ToString();
                fileid = Path.ChangeExtension(fileid, extension);

                Image img = Image.FromStream(bannerImage.InputStream, true, true);
                int width = img.Width;
                int height = img.Height;

                if (file != null && file.ContentLength > 0)
                {
                    const int megabyte = 1024 * 1024;

                    if (!file.ContentType.StartsWith("image/"))
                    {
                        error = "Invalid MIME content type";
                        url = null;
                        return;
                    }

                    string[] extensions = {".jpg", ".png" };
                    if (!extensions.Contains(extension))
                    {
                        error = "Invalid file extension";
                        url = null;
                        return;
                    }

                    if (file.ContentLength > (size * megabyte))
                    {
                        error = string.Format("File size limit exceeded, must be less then {0}MB",size);
                        url = null;
                        return;
                    }

                    if (width != chkWidth && height != chkHeight)
                    {
                        error = string.Format("Background dimension must be {0}*{1}", chkWidth, chkHeight);
                        url = null;
                        return;
                    }

                    string savePath = ImagePath + fileid;
                    file.SaveAs(savePath);
                    error = null;
                    url = fileid;
                    return;
                }
                error = null;
                url = null;
                return;
            }
            error = null;
            url = null;
            return;
        }

        public static int DeleteImage(string path, string filename)
        {
            int count = 0;
            if (Directory.Exists(path))
            {
                if (File.Exists(path + filename))
                {
                    File.Delete(path + filename);
                    count++;
                }
            }
            return count;
        }
    }
}