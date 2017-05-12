using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.Interface;
using Bussiness.ViewModel;
using DataAccess;
using NSX_Common;

namespace Bussiness.Implement
{
    public class CategoryService : ICategoryService
    {
        public List<Category> GetlAllCategories()
        {
            DataTable dtb = SqlDb_Ultis.ExeSQLToDataTable("Select * from tbl_Category");
            var lst = Ultis.DataTableToList<Category>(dtb);
            return lst;
        }
    }
}
