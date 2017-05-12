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
        List<CommentViewModel> GetCommentByPId(Guid id);
        CommentViewModel GetCommentById(Guid id);
        void InsertCooment(CommentViewModel viewModel);
        void UpdateComment(CommentViewModel viewModel);
        void DeletComment(Guid id);
    }
}
