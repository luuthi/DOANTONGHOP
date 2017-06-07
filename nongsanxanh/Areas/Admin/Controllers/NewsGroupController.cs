using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;
using NSX_Common;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class NewsGroupController : Controller
    {
        private readonly INewsGroupService _iNewsGroupService;

        public NewsGroupController(INewsGroupService iNewsGroupService)
        {
            _iNewsGroupService = iNewsGroupService;
        }
        // GET: Admin/NewsGroup
        public ActionResult Index()
        {
            var viewModel = _iNewsGroupService.GetAllNewsGroup();
            return View(viewModel);
        }

        // GET: Admin/NewsGroup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/NewsGroup/Create
        public ActionResult Create()
        {
            NewsGroupViewModel viewModel = new NewsGroupViewModel();
            viewModel.Status = true;
            return View(viewModel);
        }

        // POST: Admin/NewsGroup/Create
        [HttpPost]
        public ActionResult Create(NewsGroupViewModel viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    AccountViewModel account =  Session["Account"] as AccountViewModel;
                    viewModel.Description = viewModel.Description ?? String.Empty;
                    viewModel.Creator = account.UserName;
                    viewModel.CreatedDate = DateTime.Now;
                    viewModel.Modifier = account.UserName;
                    viewModel.ModifyDate = DateTime.Now;
                    viewModel.Url = Extentions.ToUnsignLinkString(viewModel.NameGroup);
                    _iNewsGroupService.InsertNewsGroup(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/NewsGroup/Edit/5
        public ActionResult Edit(string newsgroupid)
        {
            Guid g = Guid.Parse(newsgroupid);
            NewsGroupViewModel viewModel = _iNewsGroupService.GetNewsGroupById(g);
            return View(viewModel);
        }

        // POST: Admin/NewsGroup/Edit/5
        [HttpPost]
        public ActionResult Edit(string newsgroupid, NewsGroupViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                Guid g = Guid.Parse(newsgroupid);
                if (ModelState.IsValid)
                {
                    AccountViewModel account = Session["Account"] as AccountViewModel;
                    viewModel.Id = g;
                    viewModel.Description = viewModel.Description ?? String.Empty;
                    viewModel.ModifyDate =DateTime.Now;
                    viewModel.Modifier = account.UserName;
                    viewModel.Url = Extentions.ToUnsignLinkString(viewModel.NameGroup);
                    _iNewsGroupService.UpdateNewsGroup(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch
            {
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult Delete(string newsgroupid)
        {
            try
            {
                // TODO: Add delete logic here
                Guid g = Guid.Parse(newsgroupid);
                _iNewsGroupService.DeleteNewsGroup(g);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
