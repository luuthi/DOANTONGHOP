using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class ProductGroupController : Controller
    {
        private readonly IProductGroupService _iProductGroupService;

        public ProductGroupController(IProductGroupService iProductGroupService)
        {
            _iProductGroupService = iProductGroupService;
        }
        // GET: Admin/ProductGroup
        public ActionResult Index()
        {
            var viewModel= _iProductGroupService.GetAllProductGroup();
            return View(viewModel);
        }

        // GET: Admin/ProductGroup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/ProductGroup/Create
        public ActionResult Create()
        {
            var viewModel = new ProductGroupViewModel();
            viewModel.Status = true;
            return View(viewModel);
        }

        // POST: Admin/ProductGroup/Create
        [HttpPost]
        public ActionResult Create(ProductGroupViewModel viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    AccountViewModel account = Session["Account"] as AccountViewModel;
                    viewModel.Description = viewModel.Description ?? String.Empty;
                    viewModel.CreatedDate = DateTime.Now;
                    viewModel.Creator = account.UserName;
                    viewModel.ModifyDate = DateTime.Now;
                    viewModel.Modifier = account.UserName;
                    _iProductGroupService.InsertProductGroup(viewModel);
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

        // GET: Admin/ProductGroup/Edit/5
        public ActionResult Edit(string progroupid)
        {
            Guid g = Guid.Parse(progroupid);
            var viewModel = _iProductGroupService.GetProductGroupById(g);
            return View(viewModel);
        }

        // POST: Admin/ProductGroup/Edit/5
        [HttpPost]
        public ActionResult Edit(string progroupid, ProductGroupViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                Guid g = Guid.Parse(progroupid);
                if (ModelState.IsValid)
                {
                    AccountViewModel account = Session["Account"] as AccountViewModel;
                    viewModel.Id = g;
                    viewModel.Description = viewModel.Description ?? String.Empty;
                    viewModel.ModifyDate = DateTime.Now;
                    viewModel.Modifier = account.UserName;
                    _iProductGroupService.UpdateProductGroup(viewModel);
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

        // GET: Admin/ProductGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/ProductGroup/Delete/5
        [HttpPost]
        public ActionResult Delete(string progroupid)
        {
            try
            {
                // TODO: Add delete logic here
                Guid g = Guid.Parse(progroupid);
                _iProductGroupService.DeleteProductGroup(g);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
