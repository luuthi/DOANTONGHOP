using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;

namespace Bussiness.Interface
{
    public interface IAccountService
    {
        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        List<AccountViewModel> GetAllAccount();
        /// <summary>
        /// Lấy tài khoản theo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        AccountViewModel GetAccountById(Guid Id);
        /// <summary>
        /// Lấy tài khoản theo tên đăng nhập
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        AccountViewModel GetAccountByUserName(string UserName, string Password);
        /// <summary>
        /// thêm mới tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void InsertAccount(AccountViewModel viewModel);

        /// <summary>
        /// cập nhật tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void UpdateAccount(AccountViewModel viewModel);
        void UpdateStt(bool stt, Guid id);
        void UpdateInfo(AccountViewModel viewModel);
        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void DeleteAccount(Guid Id);

        string GetRoleByGroup(string group);
        AccountViewModel GetAccountByUserName(string userName);
        AccountViewModel GetAccountByEmail(string email);
    }
}
