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
    public class CommentService : ICommentService
    {
        public void DeletComment(Guid id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_CommentDelete", "@Id", id);
        }

        public List<CommentViewModel> GetAllComment()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_CommentSelectAll");
            var lst = Ultis.DataTableToList<CommentViewModel>(dtb);
            return lst;
        }

        public CommentViewModel GetCommentById(Guid id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_CommentSelectByID", "@Id", id);
            CommentViewModel role = new CommentViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<CommentViewModel>(item);
            }
            return role;
        }

        public List<CommentViewModel> GetCommentByNewsId(Guid id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_CommentSelectByNewsId","@Id",id);
            var lst = Ultis.DataTableToList<CommentViewModel>(dtb);
            return lst;
        }

        public List<CommentViewModel> GetCommentByPId(Guid id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_CommentSelectByPID","@Id",id);
            var lst = Ultis.DataTableToList<CommentViewModel>(dtb);
            return lst;
        }

        public List<CommentViewModel> GetCommentByProductId(Guid id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_CommentSelectByProductId", "@Id", id);
            var lst = Ultis.DataTableToList<CommentViewModel>(dtb);
            return lst;
        }

        public void InsertCooment(CommentViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_CommentInsert",
                "@Id", viewModel.Id,
                "@Contents", viewModel.Contents,
                "@Category",String.Empty,
                "@Status", viewModel.Status,
                "@ParrentId", viewModel.ParrentId,
                "@ProductId", viewModel.ProductId,
                "@NewsId",viewModel.NewsId,
                "@EmailComment", viewModel.EmailComment,
                "@CreatedDate", viewModel.CreatedDate,
                "@ModiyDate", viewModel.ModifyDate);
        }

        public void UpdateComment(CommentViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_CommentUpDate",
                "@Id", viewModel.Id,
                "@Contents", viewModel.Contents,
                "@Category", String.Empty,
                "@Status", viewModel.Status,
                "@ParrentId", viewModel.ParrentId,
                "@ProductId", viewModel.ProductId,
                "@NewsId", viewModel.NewsId,
                "@EmailComment", viewModel.EmailComment,
                "@ModiyDate", viewModel.ModifyDate);
        }
    }
}
