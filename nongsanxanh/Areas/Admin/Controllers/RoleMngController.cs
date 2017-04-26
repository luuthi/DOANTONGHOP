using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;
using nongsanxanh.Models;
using NSX_Common;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class RoleMngController : Controller
    {
        private readonly IRoleService _iRoleService;

        public RoleMngController(IRoleService iRoleService)
        {
            _iRoleService = iRoleService;
        }
        // GET: Admin/RoleMng
        public ActionResult Index()
        {
            var viewModel = _iRoleService.GetAllRole();
            foreach (var item in viewModel)
            {
                item.Code = (RightAdmin) Enum.Parse(typeof(RightAdmin), item.Code.ToString());
            }
            return View(viewModel);
        }

        // GET: Admin/RoleMng/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/RoleMng/Create
        public ActionResult Create()
        {
            RoleViewModel viewModel =new RoleViewModel();
            return View(viewModel);
        }

        // POST: Admin/RoleMng/Create
        [HttpPost]
        public ActionResult Create(RoleViewModel viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _iRoleService.InsertRole(viewModel);
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

        // GET: Admin/RoleMng/Edit/5
        public ActionResult Edit(string roleid)
        {
            Guid g = Guid.Parse(roleid);
            RoleViewModel viewModel = _iRoleService.GetRoleById(g);
            return View(viewModel);
        }

        // POST: Admin/RoleMng/Edit/5
        [HttpPost]
        public ActionResult Edit(string roleid, RoleViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                Guid g = Guid.Parse(roleid);
                if (ModelState.IsValid)
                {
                    viewModel.Id = g;
                    _iRoleService.UpdateRole(viewModel);
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

        // GET: Admin/RoleMng/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/RoleMng/Delete/5
        [HttpPost]
        public ActionResult Delete(string roleid)
        {
            try
            {
                // TODO: Add delete logic here
                Guid g = Guid.Parse(roleid);
                _iRoleService.DeleteRole(g);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
