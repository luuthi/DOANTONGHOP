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
    public class MenuService : IMenuService
    {
        public void DeleteMenu(Guid id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_MenuDelete", "@Id", id);
        }

        public List<MenuViewModel> GetAllMenu()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_MenuSelectAll");
            var lst = Ultis.DataTableToList<MenuViewModel>(dtb);
            return lst;
        }

        public MenuViewModel GetMenuById(Guid id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_MenuSelectByID", "@Id", id);
            MenuViewModel role = new MenuViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<MenuViewModel>(item);
            }
            return role;
        }

        public List<MenuViewModel> GetMenuByPId(Guid id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_MenuSelectByPID","@Id",id);
            var lst = Ultis.DataTableToList<MenuViewModel>(dtb);
            return lst; 
        }

        public void InsertMenu(MenuViewModel viewModel)
        {
           SqlDb_Ultis.ExeNonStored("Tbl_MenuInsert",
               "@Title", viewModel.Title,
               "@Order", viewModel.Order,
               "@ParrentId", viewModel.ParrentId,
               "@Id", viewModel.Id,
               "@Status", viewModel.Status);
        }

        public void UpdateMenu(MenuViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_MenuUpDate",
               "@Title", viewModel.Title,
               "@Order", viewModel.Order,
               "@ParrentId", viewModel.ParrentId,
               "@Id", viewModel.Id,
               "@Status", viewModel.Status);
        }
    }
}
