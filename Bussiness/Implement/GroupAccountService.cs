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
    public class GroupAccountService : IGroupAccountService
    {
        public void DeleteAccountGroup(Guid Id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_GroupAccountDelete","@Id",Id);
        }

        public GroupAccountViewModel GetAccountGroupById(Guid Id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_GroupAccountSelectByID", "@Id", Id);
            GroupAccountViewModel accgr = new GroupAccountViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                accgr = Ultis.GetItem<GroupAccountViewModel>(item);
            }
            return accgr;
        }

        public List<GroupAccountViewModel> GetAllAccountGroup()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_GroupAccountSelectAll");
            var lst = Ultis.DataTableToList<GroupAccountViewModel>(dtb);
            return lst;
        }

        public void InsertAccountGroup(GroupAccountViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_GroupAccountInsert",
                "@Id",viewModel.Id,
                "@GroupCode",viewModel.GroupCode,
                "@GroupName",viewModel.GroupName);
        }

        public void UpdateAccountGroup(GroupAccountViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_GroupAccountUpdate",
               "@Id", viewModel.Id,
               "@GroupCode", viewModel.GroupCode,
               "@GroupName", viewModel.GroupName);
        }
    }
}
