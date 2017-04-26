using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class AccountGroupController : Controller
    {
        private readonly IGroupAccountService _iGroupAccountService;

        public AccountGroupController(IGroupAccountService iGroupAccountService)
        {
            _iGroupAccountService = iGroupAccountService;
        }
        // GET: Admin/AccountGroup
        public ActionResult Index()
        {
            var viewModel = _iGroupAccountService.GetAllAccountGroup();
            return View(viewModel);
        }

        // GET: Admin/AccountGroup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/AccountGroup/Create
        public ActionResult Create()
        {
            GroupAccountViewModel viewModel = new GroupAccountViewModel();
            return View(viewModel);
        }

        // POST: Admin/AccountGroup/Create
        [HttpPost]
        public ActionResult Create(GroupAccountViewModel viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _iGroupAccountService.InsertAccountGroup(viewModel);
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

        // GET: Admin/AccountGroup/Edit/5
        public ActionResult Edit(string groupid)
        {
            Guid g;
            if (Guid.TryParse(groupid, out g))
            {
                GroupAccountViewModel viewModel = _iGroupAccountService.GetAccountGroupById(g);
                return View(viewModel);
            }
            return View();
        }

        // POST: Admin/AccountGroup/Edit/5
        [HttpPost]
        public ActionResult Edit(string groupid, GroupAccountViewModel viewModel)
        {
            try
            {
                Guid g = Guid.Parse(groupid);
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    viewModel.Id = g;
                    _iGroupAccountService.UpdateAccountGroup(viewModel);
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
        // GET: Admin/AccountGroup/Delete/5
        public ActionResult Delete(string groupid)
        {
            Guid g = Guid.Parse(groupid);
            _iGroupAccountService.DeleteAccountGroup(g);
            return View();
        }
        
       
    }
}
