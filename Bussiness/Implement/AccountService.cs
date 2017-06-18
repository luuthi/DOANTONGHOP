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
    public class AccountService : IAccountService
    {
        public void DeleteAccount(Guid Id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_AccountDelete","@Id",Id);
        }

        public AccountViewModel GetAccountByEmail(string email)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_AccountSelectByEmail",
                "@Email", email);
            AccountViewModel acc = new AccountViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                acc = Ultis.GetItem<AccountViewModel>(item);
            }
            return acc;
        }

        public AccountViewModel GetAccountById(Guid Id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_AccountSelectByID",
                "@Id", Id);
            AccountViewModel acc = new AccountViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                acc = Ultis.GetItem<AccountViewModel>(item);
            }
            return acc;
        }

        public AccountViewModel GetAccountByUserName(string UserName, string Password)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_AccountSelectByUserName",
                "@UserName", UserName, "@Password", Password);
            AccountViewModel acc =new AccountViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                acc = Ultis.GetItem<AccountViewModel>(item);
            }
            return acc;
        }

        public AccountViewModel GetAccountByUserName(string userName)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_AccountSelectByUserName1",
                "@UserName", userName);
            AccountViewModel acc = new AccountViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                acc = Ultis.GetItem<AccountViewModel>(item);
            }
            return acc;
        }

        public List<AccountViewModel> GetAllAccount()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_AccountSelectAll");
            var lst = Ultis.DataTableToList<AccountViewModel>(dtb);
            return lst;
        }

        public string GetRoleByGroup(string group)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("GetRoleByGroup","@group",group);
            string rs;
            if (dtb.Rows.Count>0)
            {

                rs= dtb.Rows[0][0].ToString() ?? String.Empty;
            }
            else
            {
                rs = "";
            }
            return rs;
        }

        public void InsertAccount(AccountViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_AccountInsert",
                "@Id", viewModel.Id,
                "@UserName", viewModel.UserName,
                "@Passwod", viewModel.Password,
                "@GroupId",viewModel.GroupAccId,
                "@FullName", viewModel.FullName,
                "@Gender", viewModel.Gender,
                "@Birthday", viewModel.Birthday,
                "@Email", viewModel.Email,
                "@PhoneNumber", viewModel.PhoneNumber,
                "@Image", viewModel.Image,
                "@Address", viewModel.Address,
                "@TypeAccount", viewModel.TypeAccount,
                "@Status", viewModel.Status);
        }

        public void UpdateAccount(AccountViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_AccountUpDate",
                 "@Id", viewModel.Id,
                 "@UserName", viewModel.UserName,
                 "@Passwod", viewModel.Password,
                "@GroupId", viewModel.GroupAccId,
                 "@FullName", viewModel.FullName,
                 "@Gender", viewModel.Gender,
                 "@Birthday", viewModel.Birthday,
                 "@Email", viewModel.Email,
                 "@PhoneNumber", viewModel.PhoneNumber,
                 "@Image", viewModel.Image,
                 "@Address", viewModel.Address,
                 "@TypeAccount", viewModel.TypeAccount,
                 "@Status", viewModel.Status);  
        }

        public void UpdateInfo(AccountViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_UpdateInfor",
                "@Id", viewModel.Id,
                "@FullName", viewModel.FullName,
                "@Gender", viewModel.Gender,
                "@Birthday", viewModel.Birthday,
                "@Email", viewModel.Email,
                "@PhoneNumber", viewModel.PhoneNumber,
                "@Address", viewModel.Address);
        }

        public void UpdateStt(bool stt, Guid id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_AccountUpDate_stt",
                "@Id",id,
                "@Status", stt);
        }
    }
}
