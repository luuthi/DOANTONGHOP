using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;
using NSX_Common;

namespace nongsanxanh.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _iCommentService;

        public CommentsController(ICommentService iCommentService)
        {
            _iCommentService = iCommentService;
        }
        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddComment(CommentViewModel viewModel)
        {
            viewModel.CreatedDate = DateTime.Now;
            viewModel.ModifyDate =DateTime.Now;
            viewModel.Status = 2;
            _iCommentService.InsertCooment(viewModel);
            return Json(new { status =true,msg="Vui lòng đợi quản trị viên kiểm duyệt bình luận!" });

        }
    }
}