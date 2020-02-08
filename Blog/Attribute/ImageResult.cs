using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Attribute
{
    public class ImageResult:ActionResult
    {
        private readonly Image _image;

        public ImageResult(Image image)
        {
            _image = image;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "image/jpeg";
            try
            {
                _image.Save(response.OutputStream, ImageFormat.Jpeg);
            }
            finally
            {
                _image.Dispose();
            }
        }
    }
}