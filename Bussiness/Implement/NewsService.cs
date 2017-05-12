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
    public class NewsService : INewsService
    {
        public void DeleteNews(Guid Id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_NewsDelete","@Id",Id);
        }
        public void DeleteNews_Del(Guid Id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_NewsDelete_del", "@Id", Id);
        }
        public List<NewsViewModel> GetAllNews()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_NewsSelectAll");
            var lst = Ultis.DataTableToList<NewsViewModel>(dtb);
            return lst;
        }

        public NewsViewModel GetNewsById(Guid Id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_NewsSelectByID", "@Id", Id);
            NewsViewModel role = new NewsViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<NewsViewModel>(item);
            }
            return role;
        }

        public void InsertNews(NewsViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_NewsInsert",
                "@Id", viewModel.Id,
                "@Title", viewModel.Title,
                "@Description", viewModel.Description,
                "@Content", viewModel.Content,
                "@Image", viewModel.Image,
                "@Order", viewModel.Order,
                "@CanComment", viewModel.CanComment,
                "@Category", viewModel.Category,
                "@Status", viewModel.Status,
                "@GroupId", viewModel.GroupId,
                "@PostAccount", viewModel.PostAccount,
                "@PostedDate", viewModel.PostedDate,
                "@Modifier", viewModel.Modifier,
                "@ModifyDate", viewModel.ModifyDate);
        }

        public void UpdateNews(NewsViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_NewsUpDate",
               "@Id", viewModel.Id,
               "@Title", viewModel.Title,
               "@Description", viewModel.Description,
               "@Content", viewModel.Content,
               "@Image", viewModel.Image,
               "@Order", viewModel.Order,
               "@CanComment", viewModel.CanComment,
               "@Category", viewModel.Category,
               "@Status", viewModel.Status,
               "@GroupId", viewModel.GroupId,
               "@Modifier", viewModel.Modifier,
               "@ModifyDate", viewModel.ModifyDate);
        }
    }
}
