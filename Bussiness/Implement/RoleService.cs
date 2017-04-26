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
    public class RoleService : IRoleService
    {
        public void DeleteRole(Guid Id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_RoleDelete","@Id",Id);
        }

        public RoleViewModel GetRoleById(Guid Id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_RoleSelectByID", "@Id", Id);
            RoleViewModel role = new RoleViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<RoleViewModel>(item);
            }
            return role;
        }

        public List<RoleViewModel> GetAllRole()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_RoleSelectAll");
            var lst = Ultis.DataTableToList<RoleViewModel>(dtb);
            return lst;
        }

        public void InsertRole(RoleViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_RoleInsert",
                "@Id",viewModel.Id,
                "@Code",viewModel.Code,
                "@Role",viewModel.Role);
        }

        public void UpdateRole(RoleViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_RoleUpDate",
                "@Id", viewModel.Id,
                "@Code", viewModel.Code,
                "@Role", viewModel.Role);
        }
    }
}
