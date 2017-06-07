using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("Quản lý hệ thống")]
        System =1,
        [Description("Quản lý cấu hình website")]
        WebConfig =2,
        [Description("Quản lý tin tức")]
        News=3,
        [Description("Quản lý sản phẩm")]
        Product=4,
        [Description("Quản lý đơn hàng")]
        Order = 5,
        //[Description("Quản lý menu")]
        //Menu=6,
        [Description("Quản lý bình luận ")]
        Comment=6
    }

    public enum CategoryNew
    {
        [Description("Mới nhất")]
        New =0,
        [Description("Nóng")]
        Hot=1,
        [Description("Nổi bật")]
        Outstanding =2,
        [Description("Xem nhiều nhất")]
        MostViewed =3
    }
}
