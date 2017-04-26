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
    class RoleDetailService : IRoleDetailService
    {
        public void DeleteAccountGroup()
        {
            SqlDb_Ultis.ExeNonStored("Tbl_DetailRoleDelete");
        }

        public RoleDetailViewModel GetAccountGroupById(Guid Id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_DetailRoleSelectByID", "@Id", Id);
            RoleDetailViewModel accgr = new RoleDetailViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                accgr = Ultis.GetItem<RoleDetailViewModel>(item);
            }
            return accgr;
        }

        public List<RoleDetailViewModel> GetAllAccountGroup()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_DetailRoleSelectAll");
            var lst = Ultis.DataTableToList<RoleDetailViewModel>(dtb);
            return lst;
        }

        public void InsertAccountGroup(RoleDetailViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_DetailRoleInsert",
                "@Id",viewModel.Id,
                "@GroupAccId",viewModel.GroupAccId,
                "@Role",viewModel.Role);
        }

        public void UpdateAccountGroup(RoleDetailViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_DetailRoleUpDate",
                "@Id", viewModel.Id,
                "@GroupAccId", viewModel.GroupAccId,
                "@Role", viewModel.Role);
        }
    }
}
