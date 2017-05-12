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
    public class CompanyInfoService : ICompanyInfoService
    {
        public void DeleteAccount()
        {
            SqlDb_Ultis.ExeNonStored("Tbl_InforCompanyDelete");
        }

        public CompanyInfoViewModel GetCompanyInfo()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_InforCompanySelectAll");
            CompanyInfoViewModel role = new CompanyInfoViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<CompanyInfoViewModel>(item);
            }
            return role;
        }

        public void InsertInfo(CompanyInfoViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_InforCompanyInsert",
                "@CompanyName", viewModel.CompanyName,
                "@Email", viewModel.Email,
                "@Address", viewModel.Address,
                "@Phone", viewModel.Phone,
                "@Facebook", viewModel.Facebook,
                "@Fax", viewModel.Fax,
                "@Mobile", viewModel.Mobile,
                "@Website", viewModel.Website,
                "@Logo", viewModel.Logo);
        }
    }
}
