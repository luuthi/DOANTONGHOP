using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Management;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;
using System.Reflection;
using System.ComponentModel;
using NSX_Common;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class DecentralizationController : Controller
    {
        private readonly IGroupAccountService _iGroupAccountService;
        private readonly IRoleDetailService _iRoleDetailService;
        private readonly IRoleService _iRoleService;

        public DecentralizationController(IGroupAccountService iGroupAccountService,IRoleDetailService iRoleDetailService,IRoleService iRoleService)
        {
            _iGroupAccountService = iGroupAccountService;
            _iRoleDetailService = iRoleDetailService;
            _iRoleService = iRoleService;
        }
        // GET: Admin/Decentralization
        public ActionResult Index()
        {
            var viewModel = _iGroupAccountService.GetAllAccountGroup();
            var data = _iRoleDetailService.GetAllAccountGroup();
            foreach (var item in viewModel)
            {
                var roleArr = data.Where(m => m.GroupAccId == item.GroupCode).ToArray();
                string s;
                if (roleArr.Length>0)
                {
                    s = roleArr[0].Role;
                }
                else
                {
                    s = String.Empty;
                }
                LoadQuyenEnum(item.GroupCode,s);
            }
            return View(viewModel);
        }
        public static string GetDescriptionFromEnumValue(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
        private void LoadQuyenEnum(string group, string role)
        {
            var lstEnum = Enum.GetValues(typeof(RightAdmin)).Cast<RightAdmin>().ToList();
            var lst = new List<SelectListItem>();

            foreach (var item in lstEnum)
            {
                lst.Add(new SelectListItem
                {
                    Value = item.ToString(),
                    Text = GetDescriptionFromEnumValue((RightAdmin)item),
                    Selected = role.Contains(item.ToString())
                });
            }
            ViewData["RightAdmin" + group] = lst;
        }
        //private void LoadQuyen(string group, string role)
        //{
        //    var units = _iRoleService.GetAllRole();
        //    var lst = new List<SelectListItem>();
           
        //    units.ForEach(m =>
        //    {
        //        lst.Add(new SelectListItem
        //        {
        //            Value = m.Code.ToString(),
        //            Text = m.Role,
        //            Selected = role.Contains(m.Code.ToString())
        //        });
        //    });
        //    ViewData["RightAdmin"+group] = lst;
        //}
        [System.Web.Mvc.HttpPost]
        public JsonResult Delete()
        {
            _iRoleDetailService.DeleteAccountGroup();
            return Json(new { success = 1, msg = "OK" });
        }
        [System.Web.Mvc.HttpPost]
        public JsonResult Save(RoleDetailViewModel viewModel)
        {
            viewModel.Role = viewModel.Role =="null" ? String.Empty: viewModel.Role;
            _iRoleDetailService.InsertAccountGroup(viewModel);
            return Json(new { success = 1, msg = "OK" });
        }
        // GET: Admin/Decentralization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Decentralization/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Decentralization/Create
        [System.Web.Mvc.HttpPost]
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

        // GET: Admin/Decentralization/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Decentralization/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
