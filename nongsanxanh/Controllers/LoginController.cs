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
    public class LoginController : Controller
    {
        private readonly IAccountService _iAccountService;
        private bool typeError = false;
        public LoginController(IAccountService iAccountService)
        {
            _iAccountService = iAccountService;
        }
        // GET: Login
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
                    Session["isLoginClient"] = true;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (typeError)
                    {
                        ModelState.AddModelError("UserName", "Tên đăng nhập hoặc mật khẩu chưa chính xác");
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "Tài khoản chưa được kích hoạt");
                    }
                    return View("Index", viewModel);
                }
            }
            else
            {
                return View("Index", viewModel);
            }
        }
        
        public bool doLogin(string UserName, string Password)
        {
            AccountViewModel acc = _iAccountService.GetAccountByUserName(UserName, NSX_Common.Extentions.Md5Hash(Password));
            if (acc.UserName != null)
            {
                if (acc.Status)
                {
                    Session["AccountClient"] = acc;
                    Session["Role"] = _iAccountService.GetRoleByGroup(acc.GroupAccId);
                    return true;
                }
                else
                {
                    typeError = false;
                    return false;
                }
            }
            else
            {
                typeError = true;
                return false;
            }
        }
    }
}