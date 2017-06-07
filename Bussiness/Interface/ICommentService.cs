using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;

namespace Bussiness.Interface
{
    public interface ICommentService
    {
        List<CommentViewModel> GetAllComment();
        CommentViewModel GetCommentById(Guid id);
        List<CommentViewModel> GetCommentByPId(Guid id);
        List<CommentViewModel>  GetCommentByProductId(Guid id);
        List<CommentViewModel> GetCommentByNewsId(Guid id);
        void InsertCooment(CommentViewModel viewModel);
        void UpdateComment(CommentViewModel viewModel);
        void DeletComment(Guid id);
    }
}
