using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;

namespace Bussiness.Interface
{
    public interface INewsService 
    {
        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        List<NewsViewModel> GetAllNews();
        /// <summary>
        /// Lấy tài khoản theo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        NewsViewModel GetNewsById(Guid Id);
        /// <summary>
        /// thêm mới tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void InsertNews(NewsViewModel viewModel);
        /// <summary>
        /// cập nhật tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void UpdateNews(NewsViewModel viewModel);
        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void DeleteNews(Guid Id);
        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void DeleteNews_Del(Guid Id);
    }
}
