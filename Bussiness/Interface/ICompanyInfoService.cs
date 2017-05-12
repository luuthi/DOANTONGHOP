using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;

namespace Bussiness.Interface
{
    public interface ICompanyInfoService
    {
        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        CompanyInfoViewModel GetCompanyInfo();
        /// <summary>
        /// thêm mới tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void InsertInfo(CompanyInfoViewModel viewModel);
        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void DeleteAccount();
    }
}
