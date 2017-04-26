using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSX_Common
{
    public enum RoundCornersEnum
    {
        All, TopLeft, TopRight, BottomLeft, BottomRight, TLeft_TRight, TRight_BRight, BRight_BLeft, BLeft_TLeft
    }

    public class ImageProcessingTools
    {
        public Image Combine(Image imageToMerge, Image backgroundImage, string filePathToSave)
        {
            var bitmap = new Bitmap(1280, 768);
            if (backgroundImage != null)
            {
                bitmap = new Bitmap(backgroundImage);
            }

            if (imageToMerge.Width > bitmap.Width)
            {
                imageToMerge = ZomeByWidth(imageToMerge, bitmap.Width);
            }

            if (imageToMerge.Height > bitmap.Height)
            {
                imageToMerge = ZomeByHeight(imageToMerge, bitmap.Height);
            }

            bitmap.SetResolution(imageToMerge.VerticalResolution, imageToMerge.HorizontalResolution);

            Graphics g = Graphics.FromImage(bitmap);
            if (backgroundImage == null)
            {
                g.Clear(Color.White);
            }
            Point drawPoint = new Point(0, 0);
            drawPoint.X = (bitmap.Width - imageToMerge.Width) / 2;
            drawPoint.Y = (bitmap.Height - imageToMerge.Height) / 2;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imageToMerge, drawPoint);

            if (!string.IsNullOrEmpty(filePathToSave))
                bitmap.Save(filePathToSave, ImageFormat.Jpeg);
            g.Dispose();
            return bitmap;
        }

        public Image RoundCorners(Image startImage, int cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(startImage.Width, startImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(backgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(startImage);
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            gp.AddArc(0 + RoundedImage.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            gp.AddArc(0 + RoundedImage.Width - cornerRadius, 0 + RoundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            gp.AddArc(0, 0 + RoundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            g.FillPath(brush, gp);
            return RoundedImage;
        }

        public Image RoundCorners(Image startImage, int cornerRadius, Color backgroundColor, RoundCornersEnum round, int zoom)
        {
            if (round == RoundCornersEnum.All)
            {
                return Zoom(RoundCorners(startImage, cornerRadius, backgroundColor), zoom);
            }

            if (round == RoundCornersEnum.TopLeft)
            {
                return Zoom(RoundTopLeft(startImage, cornerRadius, backgroundColor), zoom);
            }

            if (round == RoundCornersEnum.TopRight)
            {
                return Zoom(RoundTopRight(startImage, cornerRadius, backgroundColor), zoom);
            }

            if (round == RoundCornersEnum.BottomRight)
            {
                return Zoom(RoundBottomRight(startImage, cornerRadius, backgroundColor), zoom);
            }

            if (round == RoundCornersEnum.BottomLeft)
            {
                return Zoom(RoundBottomLeft(startImage, cornerRadius, backgroundColor), zoom);
            }

            if (round == RoundCornersEnum.TLeft_TRight)
            {
                return Zoom(RoundTopLeftvsTopRight(startImage, cornerRadius, backgroundColor), zoom);
            }

            if (round == RoundCornersEnum.TRight_BRight)
            {
                return Zoom(RoundTopRightvsBottomRight(startImage, cornerRadius, backgroundColor), zoom);
            }

            if (round == RoundCornersEnum.BRight_BLeft)
            {
                return Zoom(RoundBottomRightvsBottomLeft(startImage, cornerRadius, backgroundColor), zoom);
            }

            if (round == RoundCornersEnum.BLeft_TLeft)
            {
                return Zoom(RoundBottomLeftvsTopLeft(startImage, cornerRadius, backgroundColor), zoom);
            }
            return startImage;
        }

        public Image FitToSize(Image startImage, Size newSize)
        {

            int destWidth = newSize.Width;
            int destHeight = newSize.Height;

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(startImage, 0, 0, destWidth, destHeight);
            g.Dispose();
            startImage.Dispose();
            return (Image)b;
        }

        /// <summary>
        /// Scale to size, if height is smaller then scale buy with or crop it
        /// </summary>
        /// <param name="startImage"></param>
        /// <param name="newSize"></param>
        /// <param name="autoCropWidth"></param>
        /// <returns></returns>
        public Image ScaleToSize(Image startImage, Size newSize, bool autoCropWidth = true)
        {
            //Try by height:
            if (autoCropWidth)
            {
                var newImg = ZomeByHeight(startImage, newSize.Height);
                if (newImg.Width < newSize.Width)
                {
                    //Width smaller than expected, then try by width
                    newImg = ZomeByWidth(startImage, newSize.Width);
                    if (newImg.Height < newSize.Height)
                    {
                        return FitToSizeNoneCrop(startImage, newSize);
                    }
                    else //Height greater then expected then crop it
                    {
                        newImg = CropHeight(newImg, newSize.Height);
                        return newImg;
                    }
                }
                else //Width greater than expected then crop it
                {
                    newImg = CropWidth(newImg, newSize.Width);
                    return newImg;
                }
            }

            return FitToSizeNoneCrop(startImage, newSize);

        }

        private static Image FitToSizeNoneCrop(Image startImage, Size newSize)
        {
            //FIT it to size
            int destWidth = newSize.Width;
            int destHeight = newSize.Height;

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(startImage, 0, 0, destWidth, destHeight);
            g.Dispose();
            startImage.Dispose();
            return (Image)b;
        }

        private Image CropHeight(Image newImg, int newSizeHeight)
        {
            Bitmap bmpImage = new Bitmap(newImg);
            return bmpImage.Clone(new Rectangle(0, 0, newImg.Width, newSizeHeight), bmpImage.PixelFormat);
        }

        private Image CropWidth(Image newImg, int newSizeWidth)
        {
            Bitmap bmpImage = new Bitmap(newImg);
            return bmpImage.Clone(new Rectangle(0, 0, newSizeWidth, newImg.Height), bmpImage.PixelFormat);
        }

        public Image Zoom(Image startImage, int zoom)
        {
            int sourceWidth = startImage.Width;
            int sourceHeight = startImage.Height;

            if (zoom == 0) zoom = 100;
            Size size = new Size((int)(sourceWidth * ((float)zoom / 100)), (int)(sourceHeight * ((float)zoom / 100)));

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(startImage, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        public Image ZomeByWidth(Image startImage, int newWidth)
        {
            int sourceWidth = startImage.Width;
            int sourceHeight = startImage.Height;

            Size size = new Size(newWidth, sourceHeight);

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercent = nPercentW;
            //nPercentH = ((float)size.Height / (float)sourceHeight);

            //if (nPercentH < nPercentW)
            //  nPercent = nPercentH;
            //else
            //  nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(startImage, 0, 0, destWidth, destHeight);
            g.Dispose();
            startImage.Dispose();
            return (Image)b;
        }

        public Image ZomeByHeight(Image startImage, int newHeight)
        {
            int sourceWidth = startImage.Width;
            int sourceHeight = startImage.Height;

            Size size = new Size(sourceWidth, newHeight);

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            //nPercentW = ((float)size.Width / (float)sourceWidth);
            //nPercent = nPercentW;
            nPercentH = ((float)size.Height / (float)sourceHeight);
            nPercent = nPercentH;
            //if (nPercentH < nPercentW)
            //  nPercent = nPercentH;
            //else
            //  nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(startImage, 0, 0, destWidth, destHeight);
            g.Dispose();
            startImage.Dispose();
            return (Image)b;
        }

        #region public common funcs
        public string GetRandomFileName()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        public ImageFormat GetImageFileFormat(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            switch (ext)
            {
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".gif":
                    return ImageFormat.Gif;
                case ".png":
                    return ImageFormat.Png;
                case ".tiff":
                    return ImageFormat.Tiff;
            }

            return ImageFormat.Jpeg;
        }

        public string GetPngFileName(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName) + ".png";
        }
        #endregion

        #region Private methods

        private Image RoundBottomLeftvsTopLeft(Image startImage, int cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(startImage.Width, startImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(backgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(startImage);
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            gp.AddLine(cornerRadius, 0, RoundedImage.Width, 0);
            gp.AddLine(0 + RoundedImage.Width, 0, 0 + RoundedImage.Width, RoundedImage.Height);
            gp.AddLine(0 + RoundedImage.Width, RoundedImage.Height, cornerRadius, RoundedImage.Height);
            gp.AddArc(0, 0 + RoundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            g.FillPath(brush, gp);
            return RoundedImage;
        }

        private Image RoundBottomRightvsBottomLeft(Image startImage, int cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(startImage.Width, startImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(backgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(startImage);
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(0, 0, RoundedImage.Width, 0);
            gp.AddLine(RoundedImage.Width, 0, RoundedImage.Width, RoundedImage.Height - cornerRadius);
            gp.AddArc(0 + RoundedImage.Width - cornerRadius, 0 + RoundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            gp.AddArc(0, 0 + RoundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            gp.CloseFigure();
            g.FillPath(brush, gp);
            return RoundedImage;
        }

        private Image RoundTopRightvsBottomRight(Image startImage, int cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(startImage.Width, startImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(backgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(startImage);
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(0, 0, RoundedImage.Width - cornerRadius, 0);
            gp.AddArc(0 + RoundedImage.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            gp.AddArc(0 + RoundedImage.Width - cornerRadius, 0 + RoundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            gp.AddLine(RoundedImage.Width - cornerRadius, RoundedImage.Height, 0, RoundedImage.Height);
            gp.CloseFigure();
            g.FillPath(brush, gp);
            return RoundedImage;
        }

        private Image RoundTopLeftvsTopRight(Image startImage, int cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(startImage.Width, startImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(backgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(startImage);
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            gp.AddArc(0 + RoundedImage.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            gp.AddLine(RoundedImage.Width, cornerRadius, RoundedImage.Width, RoundedImage.Height);
            gp.AddLine(RoundedImage.Width, RoundedImage.Height, 0, RoundedImage.Height);
            gp.CloseFigure();
            g.FillPath(brush, gp);
            return RoundedImage;
        }

        private Image RoundBottomLeft(Image startImage, int cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(startImage.Width, startImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(backgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(startImage);
            GraphicsPath gp = new GraphicsPath();

            gp.AddLine(0, 0, RoundedImage.Width, 0);
            gp.AddLine(RoundedImage.Width, 0, RoundedImage.Width, RoundedImage.Height);
            gp.AddLine(RoundedImage.Width, RoundedImage.Height, cornerRadius, RoundedImage.Height);
            gp.AddArc(0, 0 + RoundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            gp.AddLine(0, 0, 0, RoundedImage.Height - cornerRadius);
            gp.CloseFigure();
            g.FillPath(brush, gp);
            return RoundedImage;
        }

        private Image RoundTopLeft(Image startImage, int cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(startImage.Width, startImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(backgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(startImage);

            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            gp.AddLine(cornerRadius, 0, RoundedImage.Width, 0);
            gp.AddLine(0 + RoundedImage.Width, 0, 0 + RoundedImage.Width, RoundedImage.Height);
            gp.AddLine(0 + RoundedImage.Width, RoundedImage.Height, 0, RoundedImage.Height);
            gp.CloseFigure();
            g.FillPath(brush, gp);
            return RoundedImage;
        }

        private Image RoundTopRight(Image startImage, int cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(startImage.Width, startImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(backgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(startImage);

            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(0, 0, RoundedImage.Width - cornerRadius, 0);
            gp.AddArc(0 + RoundedImage.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            gp.AddLine(RoundedImage.Width, cornerRadius, RoundedImage.Width, RoundedImage.Height);
            gp.AddLine(RoundedImage.Width, RoundedImage.Height, 0, RoundedImage.Height);
            //gp.AddArc(0, 0 + RoundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            gp.CloseFigure();
            g.FillPath(brush, gp);
            return RoundedImage;
        }

        private Image RoundBottomRight(Image startImage, int cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(startImage.Width, startImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(backgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(startImage);

            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(0, 0, RoundedImage.Width, 0);
            gp.AddLine(RoundedImage.Width, 0, RoundedImage.Width, RoundedImage.Height - cornerRadius);
            gp.AddArc(0 + RoundedImage.Width - cornerRadius, 0 + RoundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            gp.AddLine(RoundedImage.Width - cornerRadius, RoundedImage.Height, 0, RoundedImage.Height);
            gp.CloseFigure();
            g.FillPath(brush, gp);
            return RoundedImage;
        }

        #endregion
    }
}
