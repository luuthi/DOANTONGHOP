using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        private readonly IAccountService _iAccountService;

        public LoginAdminController(IAccountService iAccountService)
        {
            _iAccountService = iAccountService;
        }
        // GET: Admin/LoginAdmin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (doLogin(viewModel.UserName, viewModel.Password))
                {
                    Session["isLogin"] = true;
                    return RedirectToAction("Index", "AdminHome");
                }
                else
                {
                    Session["isLogin"] = false;
                    return View("Index", viewModel);
                }
            }
            else
            {
                return View("Index",viewModel);
            }
        }
        public bool doLogin(string UserName, string Password)
        {
            AccountViewModel acc = _iAccountService.GetAccountByUserName(UserName,NSX_Common.Extentions.Md5Hash(Password));
            if (acc.UserName!=null)
            {
                if (acc.Status)
                {
                    if (acc.TypeAccount)
                    {
                        Session["Account"] = acc;
                        Session["Role"] = _iAccountService.GetRoleByGroup(acc.GroupAccId);
                        return true;
                    }
                    else
                    {
                        TempData["Error"] = "Không phải tài khoản Admin";
                        return false;
                    }
                }
                else
                {
                    TempData["Error"] = "Tài khoản chưa được kích hoạt hoặc đã bị khóa";
                    return false;
                }
            }
            else
            {
                TempData["Error"] = "Tên tài khoản hoặc mật khẩu sai    ";
                return false;
            }
        }
        public ActionResult doLogout()
        {
            Session["Account"] = null;
            Session["isLogin"] = false;
            return RedirectToAction("Index", "LoginAdmin");
        }
    }
}