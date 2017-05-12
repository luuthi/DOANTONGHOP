using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NSX_Common
{
    public static class MediaHelper
    {
        /// <summary>
        /// Get folder name where stored media files
        /// </summary>
        public static string Folder
        {
            get
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains("MediaFolder"))
                {
                    throw new NullReferenceException("MediaFolder is not defined in web.config");
                }
                var mapFolder = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["MediaFolder"]);

                if (!Directory.Exists(mapFolder))
                {
                    Directory.CreateDirectory(mapFolder);
                }

                return mapFolder;
            }
        }
        /// <summary>
        /// Get folder name where stored media files
        /// </summary>
        public static string PdfFolder
        {
            get
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains("PdfFolder"))
                {
                    throw new NullReferenceException("PdfFolder is not defined in web.config");
                }
                var mapFolder = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PdfFolder"]);

                if (!Directory.Exists(mapFolder))
                {
                    Directory.CreateDirectory(mapFolder);
                }

                return mapFolder;
            }
        }
        public static string ImgFolder
        {
            get
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains("ImgFolder"))
                {
                    throw new NullReferenceException("ImgFolder is not defined in web.config");
                }
                var mapFolder = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ImgFolder"]);

                if (!Directory.Exists(mapFolder))
                {
                    Directory.CreateDirectory(mapFolder);
                }

                return mapFolder;
            }
        }
        public static string DocFolder
        {
            get
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains("DocFolder"))
                {
                    throw new NullReferenceException("DocFolder is not defined in web.config");
                }
                var mapFolder = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["DocFolder"]);

                if (!Directory.Exists(mapFolder))
                {
                    Directory.CreateDirectory(mapFolder);
                }

                return mapFolder;
            }
        }
        public static string ExcelFolder
        {
            get
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains("ExcelFolder"))
                {
                    throw new NullReferenceException("ExcelFolder is not defined in web.config");
                }
                var mapFolder = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ExcelFolder"]);

                if (!Directory.Exists(mapFolder))
                {
                    Directory.CreateDirectory(mapFolder);
                }

                return mapFolder;
            }
        }
        public static Size DefaultImageSize
        {
            get
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains("ImageDefaultSize"))
                {
                    throw new NullReferenceException("ImageDefaultSize is not defined in web.config");
                }
                var size = ConfigurationManager.AppSettings["MediaFolder"];
                var sizes = size.Split(',');
                if (sizes.Length != 2)
                {
                    return new Size(353, 204);
                }

                var w = sizes[0].Trim();
                var h = sizes[1].Trim();

                return new Size(int.Parse(w), int.Parse(h));
            }
        }

        /// <summary>
        /// Get new file name based on given file name. New file name is created to save unique name.
        /// </summary>
        /// <param name="imageFileFileName"></param>
        /// <returns></returns>
        public static string GetNewFileName(string imageFileFileName)
        {
            return  imageFileFileName.ToLower();
        }

        /// <summary>
        /// Get full file path with given file name
        /// </summary>
        /// <param name="imageFileFileName"></param>
        /// <returns></returns>
        public static string GetNewFilePath(string imageFileFileName)
        {
            var filename = GetNewFileName(imageFileFileName);
            return Path.Combine(Folder, filename);
        }

        /// <summary>
        /// Save image file from posted file content. Return new filename
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string SaveImageFile(HttpPostedFileBase postedFile, string subfolder, string fileName = null)
        {
            string Folder = "";
            fileName = fileName ?? postedFile.FileName;

            if (postedFile.FileName.ToLower().EndsWith(".jpg") || postedFile.FileName.ToLower().EndsWith(".png") || postedFile.FileName.ToLower().EndsWith(".gif") || postedFile.FileName.ToLower().EndsWith(".jpeg"))
            {
                fileName += ".jpg";
            }

            var filename = GetNewFileName(fileName);
            Folder = ImgFolder + subfolder;
            var filePath = Path.Combine(Folder, filename);
            var thumbFilePath = Path.Combine(Folder, "thumb_" + filename);
            //Save original file
            postedFile.SaveAs(filePath);

            //Create thumb file
            //var imageProcessing = new ImageProcessingTools();
            //var thumb = imageProcessing.ScaleToSize(Bitmap.FromFile(filePath), DefaultImageSize);
            //thumb.Save(thumbFilePath);
            //thumb.Dispose();
            ////Revise the original file
            //thumb = imageProcessing.ZomeByWidth(Bitmap.FromFile(filePath), 599);
            //thumb.Save(filePath + "tmp");
            //thumb.Dispose();
            //File.Delete(filePath);
            //File.Move(filePath + "tmp", filePath);
            return filename;
        }


        /// <summary>
        /// Save image file from posted file content. Return new filename
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string SaveDocumentFile(HttpPostedFileBase postedFile, string fileName = null)
        {
            //string[] arr;
            string Folder = "";
            //arr = postedFile.FileName.Split('.');
            fileName = fileName ?? postedFile.FileName;
            
            if (postedFile.FileName.EndsWith(".xls") || postedFile.FileName.EndsWith(".xlsx"))
            {
                fileName += ".xls";
                Folder = ExcelFolder;
            }
            else if (postedFile.FileName.EndsWith(".pdf"))
            {
                fileName += ".pdf";
                Folder = PdfFolder;
            }
            else if (postedFile.FileName.EndsWith(".doc") || postedFile.FileName.EndsWith(".docx"))
            {
                fileName += ".doc";
                Folder = DocFolder;
            }
            else if (postedFile.FileName.ToLower().EndsWith(".jpg") || postedFile.FileName.ToLower().EndsWith(".png") || postedFile.FileName.ToLower().EndsWith(".gif") || postedFile.FileName.ToLower().EndsWith(".jpeg"))
            {
                if (postedFile.FileName.ToLower().EndsWith(".jpg"))
                {
                    fileName +=".jpg";
                }
                if (postedFile.FileName.ToLower().EndsWith(".png"))
                {
                    fileName += ".png";
                }
                if (postedFile.FileName.ToLower().EndsWith(".gif"))
                {
                    fileName += ".gif";
                }
                if (postedFile.FileName.ToLower().EndsWith(".jpeg"))
                {
                    fileName += ".jpeg";
                }
                Folder = ImgFolder;
            }
            fileName = fileName ?? postedFile.FileName;
            var filename = GetNewFileName(fileName);
            var filePath = Path.Combine(Folder, filename);
            //Save original file
            postedFile.SaveAs(filePath);
            
            return filename;
        }
        /// <summary>
        /// Save image file from posted file content. Return new filename
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool RemoveDocumentFile(string fileName)
        {
            //string[] arr;
            string Folder = "";
            if (fileName.EndsWith(".xls") || fileName.EndsWith(".xlsx"))
            {
                Folder = ExcelFolder;
            }
            else if (fileName.EndsWith(".pdf"))
            {
                Folder = PdfFolder;
            }
            else if (fileName.EndsWith(".doc") || fileName.EndsWith(".docx"))
            {
                Folder = DocFolder;
            }
            var filePath = Path.Combine(Folder, fileName);
            //Delete original file
            File.Delete(filePath);
            return true;
        }
        public static byte[] GetFileContent(string fileSystemFileName, out string mimeType)
        {
            string Folder = "";
            if (fileSystemFileName.EndsWith(".xls") || fileSystemFileName.EndsWith(".xlsx"))
            {
                Folder = ExcelFolder;
            }
            else if (fileSystemFileName.EndsWith(".pdf"))
            {
                Folder = PdfFolder;
            }
            else if (fileSystemFileName.EndsWith(".doc") || fileSystemFileName.EndsWith(".docx"))
            {
                Folder = DocFolder;
            }
            var mapFolder = Folder;
            mimeType = "unknown";

            var filePath = Path.Combine(mapFolder, fileSystemFileName);
            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }

            mimeType = MimeMapping.GetMimeMapping(filePath);

            return System.IO.File.ReadAllBytes(filePath);
        }
    }
}
