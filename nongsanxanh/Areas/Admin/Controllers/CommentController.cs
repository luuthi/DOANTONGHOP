using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _iCommentService;

        public CommentController(ICommentService iCommentService)
        {
            _iCommentService = iCommentService;
        }
        private void LoadStatus()
        {
            var lst = new List<SelectListItem>();
            lst.Add(new SelectListItem()
            {
                Value = "-1",
                Text = "Chọn 1 trong các lựa chọn"
            });
            lst.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Bị hủy"
            });
            lst.Add(new SelectListItem()
            {
                Value ="1",
                Text = "Hiện hoạt động"
            });
            lst.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Đang chờ duyệt"
            });
            ViewData["Status"] = lst;
        }
        // GET: Admin/Comment
        
        public ActionResult Index()
        {
            LoadStatus();
            var viewModel = _iCommentService.GetAllComment();
            return View(viewModel);
        }
        
        [HttpPost]
        public ActionResult Index(string Status)
        {
            LoadStatus();
            int status = int.Parse(Status);
            List<CommentViewModel> viewModel;
            if (status == -1)
            {
                viewModel = _iCommentService.GetAllComment();
            }
            else
            {
                viewModel = _iCommentService.GetAllComment().Where(o => o.Status == status).ToList();
            }

            return View(viewModel);
        }
        // GET: Admin/Comment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Comment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Duyet(string commentid)
        {
            try
            {
                // TODO: Add update logic here
                Guid g = Guid.Parse(commentid);
                CommentViewModel viewModel = _iCommentService.GetCommentById(g);
                viewModel.Status = 1;
                _iCommentService.UpdateComment(viewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Comment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Comment/Delete/5
        [HttpPost]
        public ActionResult Delete(string commentid)
        {
            try
            {
                // TODO: Add delete logic here
                Guid g = Guid.Parse(commentid);
                CommentViewModel viewModel = _iCommentService.GetCommentById(g);
                viewModel.Status = 0;
                _iCommentService.UpdateComment(viewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
