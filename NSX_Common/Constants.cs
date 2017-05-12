using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSX_Common
{
    public class Constants
    {
        public static string NOIMAGE = "noimage.png";
        public static string sanpham_folder = @"san-pham\";
        public static string tintuc_folder = @"tin-tuc\";
        public static string userlogo =@"user-logo\";
        public static string hethong = @"he-thong\";
    }

    public enum RightAdmin
    {
        System =1,
        WebConfig =2,
        News=3,
        Product=4,
        Order =5,
        Menu=6,
        Comment=7
    }

    public enum CategoryNew
    {
        New =0,
        Hot=1,
        Outstanding =2,
        MostViewed =3
    }
}
