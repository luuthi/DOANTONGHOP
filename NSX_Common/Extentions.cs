using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using ClosedXML.Excel;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace NSX_Common
{
    public static class Extentions
    {

        
        public static string FormatNumber(decimal str)
        {
            string number = "";
            double db = Convert.ToDouble(str);
            number = string.Format("{0:0,0}", db);
            if (number == "00")
                number = "0";
            return number;
        }

        public static string AutoID(string Ten, long i)
        {
            string ID = "";
            if (i < 10)
            {
                ID = Ten.ToUpper() + "000000" + Convert.ToString(i);
            }
            else if (i < 100)
            {
                ID = Ten.ToUpper() + "00000" + Convert.ToString(i);
            }
            else if (i < 1000)
            {
                ID = Ten.ToUpper() + "0000" + Convert.ToString(i);
            }
            else if (i < 10000)
            {
                ID = Ten.ToUpper() + "000" + Convert.ToString(i);
            }
            else if (i < 100000)
            {
                ID = Ten.ToUpper() + "00" + Convert.ToString(i);
            }
            else if (i < 1000000)
            {
                ID = Ten.ToUpper() + "0" + Convert.ToString(i);
            }
            else
            {
                ID = Ten.ToUpper() + Convert.ToString(i);
            }
            return ID;
        }
        public static bool IsGenericType(this Type type)
        {
            return type.IsGenericType;
        }

        public static string PadLeft(this int value, char paddingChar, int length)
        {
            var valueString = value.ToString();
            return valueString.PadLeft(length, paddingChar);
        }

        /// <summary>
        /// Try to get value from Dictionary type = "string, string" by given key
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string TryGetValueOrDefault(this Dictionary<string, string> dict, string key)
        {
            if (!string.IsNullOrEmpty(key) && dict.ContainsKey(key))
            {
                return dict[key];
            }

            return key;
        }

        public static Guid ToGuid(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return Guid.Parse(value);
            }

            return Guid.Empty;
        }

        /// <summary>
        ///     Return the user id using the UserIdClaimType
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetUserId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                string str = claimsIdentity.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (str != null)
                {
                    return (string)Convert.ChangeType(str, typeof(string), CultureInfo.InvariantCulture);
                }
            }
            return default(string);
        }

        /// <summary>
        ///     Return the user name using the UserNameClaimType
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetUserName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                return null;
            }
            return claimsIdentity.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
        }

        /// <summary>
        ///     Return the claim value for the first claim with the specified type if it exists, null otherwise
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            Claim claim = identity.FindFirst(claimType);
            if (claim == null)
            {
                return null;
            }
            return claim.Value;
        }



        /// <summary>
        /// Compare 2 strings is equal
        /// </summary>
        /// <param name="self"></param>
        /// <param name="destString"></param>
        /// <returns></returns>
        public static bool Similar(this string self, string destString)
        {
            return self.ToLower() == destString.ToLower();
        }

        /// <summary>
        /// To lower the first character of string: ConcreteString -> concreteString
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToLowerFirst(this string self)
        {
            if (string.IsNullOrWhiteSpace(self)) return self;
            return self.Substring(0, 1).ToLower() + self.Substring(1);
        }

        /// <summary>
        /// Create MD5 string
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string Md5Hash(this string self)
        {
            if (string.IsNullOrWhiteSpace(self)) return self;

            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(self));

            //get hash result after compute it
            byte[] result = md5.Hash;

            var strBuilder = new StringBuilder();
            foreach (byte t in result)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(t.ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public static List<E> MixList<E>(List<E> inputList)
        {
            if (inputList == null || inputList.Count == 0) return new List<E>();
            if (inputList.Count == 1) return inputList;

            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            if (inputList.Count == 2)
            {
                randomList.Add(inputList[1]);
                randomList.Add(inputList[0]);
            }
            else
            {
                while (inputList.Count > 0)
                {
                    randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                    randomList.Add(inputList[randomIndex]); //add it to the new, random list
                    inputList.RemoveAt(randomIndex); //remove to avoid duplicates
                }
            }

            return randomList; //return the new random list
        }

        public static object GetValue(this Dictionary<string, object> value, string key)
        {
            return value.ContainsKey(key) ? value[key] : "";
        }

        public static DateTime GetFirstDayOfWeek(this DateTime sourceDateTime)
        {
            var daysAhead = (DayOfWeek.Sunday - (int)sourceDateTime.DayOfWeek);

            sourceDateTime = sourceDateTime.AddDays((int)daysAhead);

            return sourceDateTime;
        }

        public static DateTime GetLastDayOfWeek(this DateTime sourceDateTime)
        {
            var daysAhead = DayOfWeek.Saturday - (int)sourceDateTime.DayOfWeek;

            sourceDateTime = sourceDateTime.AddDays((int)daysAhead);

            return sourceDateTime;
        }

        /// <summary>
        /// Reformat string to remove all duplicate spaces in string. eg: "the   name" => "the name"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Formatting(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;
            string[] strDestination = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string pStr in strDestination)
            {
                value += pStr + " ";
            }
            return value.Trim();
        }

        public static string ToLongString(this string value)
        {
            var str = value.ToLower();
            var longVal = 0;
            foreach (var character in str)
            {
                longVal += (int)character;
            }

            return longVal.ToString();
        }

        public static string ToCurrencyString(this int value)
        {
            return String.Format("{0:N}", value).Replace(".00", "");
        }

        public static string RemoveSign4VietnameseString(string str)
        {
            //Replace sign of character in the string
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                {
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
                }
            }

            return str;
        }

        public static string RemoveUnexpectedCharacters(string str)
        {
            const string stringSource = "~!@#$%^&*()_+{}:\"<>?,./;'[]\\|-=`";
            const string dest = "";
            //Replace characters in the string
            for (int i = 1; i < stringSource.Length; i++)
            {
                str = str.Replace(stringSource[i].ToString(), dest);
            }

            return str;
        }

        public static string ToUnsignLinkString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            value = RemoveSign4VietnameseString(value.ToLower());
            value = RemoveUnexpectedCharacters(value.ToLower());
            return value.Replace(" ", "-").Trim('-');
        }

        public static string ToJsonString(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string ToDob(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy");
        }
        public static string ToNewsPublishedDate(this DateTime value)
        {
            return value.ToString("dd MMM yyyy");
        }

        /// <summary>
        /// Formats a string to an invariant culture
        /// </summary>
        /// <param name="formatString">The format string.</param>
        /// <param name="objects">The objects.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string FormatInvariant(this string format, params object[] objects)
        {
            return string.Format(CultureInfo.InvariantCulture, format, objects);
        }

        /// <summary>
        /// Formats a string to the current culture.
        /// </summary>
        /// <param name="formatString">The format string.</param>
        /// <param name="objects">The objects.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string FormatCurrent(this string format, params object[] objects)
        {
            return string.Format(CultureInfo.CurrentCulture, format, objects);
        }

        /// <summary>
        /// Formats a string to the current UI culture.
        /// </summary>
        /// <param name="formatString">The format string.</param>
        /// <param name="objects">The objects.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string FormatCurrentUI(this string format, params object[] objects)
        {
            return string.Format(CultureInfo.CurrentUICulture, format, objects);
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string format, params object[] args)
        {
            return FormatWith(format, CultureInfo.CurrentCulture, args);
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            return string.Format(provider, format, args);
        }

        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"

        };

        public static void Export2Excel(string Title, XLColor HeaderBackgroundColor, XLColor HeaderForeColor, int HeaderFont, DataTable gv, XLColor ColumnBackgroundColor, XLColor ColumnForeColor, string SheetName, string FileName, List<string> headerTitle,HttpResponseBase response)
        {
            DataTable table = gv;
            //creating a new Workbook
            var wb = new XLWorkbook();
            // adding a new sheet in workbook
            var ws = wb.Worksheets.Add(SheetName);
            //adding content
            //Title
            ws.Cell("A1").Value = Title;
            ws.Cell("A2").Value = "Ngày tạo thống kê: " + DateTime.Now.ToShortDateString();
            //add columns
            string[] cols = new string[table.Columns.Count];
            for (int c = 0; c < table.Columns.Count; c++)
            {
                var a = table.Columns[c].ToString();
                cols[c] = table.Columns[c].ToString().Replace('_', ' ');
            }

            char StartCharCols = 'A';
            int StartIndexCols = 3;
            #region CreatingColumnHeaders

            int count = 1;
            foreach (string header in headerTitle)
            {
                if (count == headerTitle.Count)
                {
                    string DataCell = StartCharCols.ToString() + StartIndexCols.ToString();
                    ws.Cell(DataCell).Value = header;
                    ws.Cell(DataCell).WorksheetColumn().Width = header.ToString().Length + 10;
                    ws.Cell(DataCell).Style.Font.Bold = true;
                    ws.Cell(DataCell).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(DataCell).Style.Fill.BackgroundColor = ColumnBackgroundColor;
                    ws.Cell(DataCell).Style.Font.FontColor = ColumnForeColor;
                }
                else
                {
                    string DataCell = StartCharCols.ToString() + StartIndexCols.ToString();
                    ws.Cell(DataCell).Value = header;
                    ws.Cell(DataCell).WorksheetColumn().Width = header.ToString().Length + 10;
                    ws.Cell(DataCell).Style.Font.Bold = true;
                    ws.Cell(DataCell).Style.Fill.BackgroundColor = ColumnBackgroundColor;
                    ws.Cell(DataCell).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(DataCell).Style.Font.FontColor = ColumnForeColor;
                    StartCharCols++;
                }
                count++;
            }
            #endregion

            //Merging Header

            string Range = "A1:" + StartCharCols.ToString() + "1";

            ws.Range(Range).Merge();
            ws.Range(Range).Style.Font.FontSize = HeaderFont;
            ws.Range(Range).Style.Font.Bold = true;

            ws.Range(Range).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            ws.Range(Range).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            if (HeaderBackgroundColor != null && HeaderForeColor != null)
            {
                ws.Range(Range).Style.Fill.BackgroundColor = HeaderBackgroundColor;
                ws.Range(Range).Style.Font.FontColor = HeaderForeColor;
            }

            //Style definitions for Date range
            Range = "A2:" + StartCharCols.ToString() + "2";

            ws.Range(Range).Merge();
            ws.Range(Range).Style.Font.FontSize = 10;
            ws.Range(Range).Style.Font.Bold = true;
            ws.Range(Range).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Bottom);
            ws.Range(Range).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

            //border definitions for Columns
            Range = "A3:" + StartCharCols.ToString() + "3";
            ws.Range(Range).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Range(Range).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            ws.Range(Range).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            ws.Range(Range).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

            char StartCharData = 'A';
            int StartIndexData = 4;

            char StartCharDataCol = char.MinValue;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {

                    string DataCell = StartCharData.ToString() + StartIndexData;
                    var a = table.Rows[i][j].ToString();
                    a = a.Replace("&nbsp;", " ");
                    a = a.Replace("&amp;", "&");
                    //check if value is of integer type
                    int val = 0;
                    decimal currency = 0;
                    DateTime dt = DateTime.Now;
                    if (int.TryParse(a, out val))
                    {
                        ws.Cell(DataCell).Style.NumberFormat.NumberFormatId = 3;
                        ws.Cell(DataCell).Value = val;
                    }
                    if (decimal.TryParse(a, out currency))
                    {
                        ws.Cell(DataCell).Style.NumberFormat.NumberFormatId = 3;
                        ws.Cell(DataCell).Value = currency;
                    }
                    //check if datetime type
                    else if (DateTime.TryParse(a, out dt))
                    {

                        ws.Cell(DataCell).Value = dt.ToShortDateString();
                    }
                    else
                    {
                        ws.Cell(DataCell).SetValue(a);
                    }

                    StartCharData++;
                }
                StartCharData = 'A';
                StartIndexData++;
            }

            char LastChar = Convert.ToChar(StartCharData + table.Columns.Count - 1);
            int TotalRows = table.Rows.Count + 3;
            Range = "A4:" + LastChar + TotalRows;
            ws.Range(Range).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Range(Range).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            ws.Range(Range).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            ws.Range(Range).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            //Code to save the file
            HttpResponseBase res = response;
            response.Clear();
            response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            response.AddHeader("content-disposition", "attachment;filename=" + FileName + DateTime.Now.Day+"_"+DateTime.Now.Month+"_"+DateTime.Now.Year + ".xlsx");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(response.OutputStream);
                memoryStream.Close();
            }

            response.End();

        }
    }
}
