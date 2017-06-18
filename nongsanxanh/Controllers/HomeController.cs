using Bussiness.Interface;
using Bussiness.ViewModel;
using NSX_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nongsanxanh.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _iProductService;
        private readonly INewsService _iNewsService;
        private readonly IAccountService _iAccountService;
        private bool typeError = false;
        public HomeController(IProductService iProductService, INewsService iNewService, IAccountService iAccountService)
        {
            _iNewsService = iNewService;
            _iAccountService = iAccountService;
            _iProductService = iProductService;
        }
        public ActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.lstNewProduct = loadNewProduct();
            viewModel.lstNews = loadNews();
            viewModel.lstOutstandingProduct = loadOutstandingProduct();
            viewModel.lstTopSellProduct = loadTopSellProduct();
            return View(viewModel);
        }
        private List<ProductViewModel> loadNewProduct()
        {
            List<ProductViewModel> lst = new List<ProductViewModel>();
            var data = _iProductService.GetAllProduct().Where(m => m.Category.Contains(CategoryNew.New.ToString())).ToList();
            foreach (var item in data)
            {
                ProductViewModel pro = new ProductViewModel();
                pro.Id = item.Id;
                pro.GroupName = Extentions.ToUnsignLinkString(item.GroupName);
                pro.Image = item.Image;
                pro.ProductName = item.ProductName;
                lst.Add(pro);
            }
            return lst;
        }
        private List<ProductViewModel> loadTopSellProduct()
        {
            List<ProductViewModel> lst = new List<ProductViewModel>();
            var data = _iProductService.GetAllProduct().Where(m => m.Category.Contains(CategoryNew.Hot.ToString())).ToList();
            foreach (var item in data)
            {
                ProductViewModel pro = new ProductViewModel();
                pro.Id = item.Id;
                pro.GroupName = Extentions.ToUnsignLinkString(item.GroupName);
                pro.Image = item.Image;
                pro.ProductName = item.ProductName;
                lst.Add(pro);
            }
            return lst;
        }
        private List<ProductViewModel> loadOutstandingProduct()
        {
            List<ProductViewModel> lst = new List<ProductViewModel>();
            var data = _iProductService.GetAllProduct().Where(m => m.Category.Contains(CategoryNew.Outstanding.ToString())).ToList();
            foreach (var item in data)
            {
                ProductViewModel pro = new ProductViewModel();
                pro.Id = item.Id;
                pro.GroupName = Extentions.ToUnsignLinkString(item.GroupName);
                pro.Image = item.Image;
                pro.ProductName = item.ProductName;
                lst.Add(pro);
            }
            return lst;
        }
        private List<NewsViewModel> loadNews()
        {
            List<NewsViewModel> lst = new List<NewsViewModel>();
            var data = _iNewsService.GetAllNews().Take(3);
            foreach (var item in data)
            {
                NewsViewModel news = new NewsViewModel();
                news.Title = item.Title;
                news.Image = item.Image;
                news.Description = item.Description;
                news.Id = item.Id;
                news.NameGroup = item.NameGroup;
                news.PostedDate = item.PostedDate;
                lst.Add(news);
            }
            return lst;
        }

        [HttpPost]
        public ActionResult LoadFormLogin()
        {
            LoginViewModel viewModel = new LoginViewModel();
            return PartialView("_parLogin", viewModel);
        }

        [HttpPost]
        public JsonResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (doLogin(viewModel.UserName,viewModel.Password))
                {
                    Session["isLoginClient"] = true;
                    JsonClasses cls = new JsonClasses()
                    {
                        Status = true,
                        Html = JsonClasses.ToPartialView(this, "_parLogin", null)
                        //return Json(new { });//PartialView("_modal_repo", viewModel);
                    };
                    return Json(cls);
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
                    return Json(new JsonClasses()
                    {
                        Status = false,
                        Html = JsonClasses.ToPartialView(this, "_parLogin", viewModel)
                    });
                }

            }
            else
            {
                return Json(new JsonClasses()
                {
                    Status = false,
                    Html = JsonClasses.ToPartialView(this, "_parLogin", viewModel)
                });
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
                    typeError = true;
                    return false;
                }
            }
            else
            {
                typeError = false;
                return false;
            }
        }
        public ActionResult doLogout()
        {
            Session["AccountClient"] = null;
            Session["isLoginClient"] = false;
            return RedirectToAction("Index", "Home");
        }
    }
}