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
    public class NewsGroupService : INewsGroupService
    {
        public void DeleteNewsGroup(Guid Id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_NewsGroupDelete","@Id",Id);
        }

        public List<NewsGroupViewModel> GetAllNewsGroup()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_NewsGroupSelectAll");
            var lst = Ultis.DataTableToList<NewsGroupViewModel>(dtb);
            return lst;
        }

        public NewsGroupViewModel GetNewsGroupById(Guid Id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_NewsGroupSelectByID", "@Id", Id);
            NewsGroupViewModel role = new NewsGroupViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<NewsGroupViewModel>(item);
            }
            return role;
        }

        public void InsertNewsGroup(NewsGroupViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_NewsGroupInsert",
                "@Id", viewModel.Id,
                "@NameGroup", viewModel.NameGroup,
                "@Description", viewModel.Description,
                "@Status", viewModel.Status,
                "@Creator", viewModel.Creator,
                "@CreatedDate", viewModel.CreatedDate,
                "@Modifier", viewModel.Modifier,
                "@ModifyDate", viewModel.ModifyDate);
        }

        public void UpdateNewsGroup(NewsGroupViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_NewsGroupUpDate",
                 "@Id", viewModel.Id,
                 "@NameGroup", viewModel.NameGroup,
                 "@Description", viewModel.Description,
                 "@Status", viewModel.Status,
                 "@Modifier", viewModel.Modifier,
                 "@ModifyDate", viewModel.ModifyDate);
        }
    }
}
