using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _iMenuService;

        public MenuController(IMenuService iMenuService)
        {
            _iMenuService = iMenuService;
        }
        private List<SelectListItem> LoadParent()
        {
            var units = _iMenuService.GetAllMenu();
            var lst = new List<SelectListItem>();
            lst.Add(new SelectListItem()
            {
                Value = "null",
                Text = "Chọn menu cha............."
            });
            lst.Add(new SelectListItem()
            {
                Value = Guid.Empty.ToString(),
                Text = "Chọn làm menu gốc............."
            });
            units.ForEach(m =>
            {
                lst.Add(new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Title
                });
            });
            return lst;
        }
        // GET: Admin/Menu
        public ActionResult Index()
        {
            var viewModel = _iMenuService.GetAllMenu();
            return View(viewModel);
        }

        // GET: Admin/Menu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Menu/Create
        public ActionResult Create()
        {
            MenuViewModel viewModel = new MenuViewModel();
            viewModel.lstParentMenu = LoadParent();
            viewModel.Status = true;
            return View(viewModel);
        }

        // POST: Admin/Menu/Create
        [HttpPost]
        public ActionResult Create(MenuViewModel viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _iMenuService.InsertMenu(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    viewModel.lstParentMenu = LoadParent();
                    return View(viewModel);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(string menuid)
        {
            Guid g = Guid.Parse(menuid);
            MenuViewModel viewModel = _iMenuService.GetMenuById(g);
            viewModel.lstParentMenu = LoadParent();
            return View(viewModel);
        }

        // POST: Admin/Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(string menuid, MenuViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                Guid g = Guid.Parse(menuid);
                if (ModelState.IsValid)
                {
                    viewModel.Id = g;
                    _iMenuService.UpdateMenu(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    viewModel.lstParentMenu = LoadParent();
                    return View(viewModel);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost]
        public ActionResult Delete(string menuid)
        {
            try
            {
                // TODO: Add delete logic here
                Guid g = Guid.Parse(menuid);
                _iMenuService.DeleteMenu(g);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
