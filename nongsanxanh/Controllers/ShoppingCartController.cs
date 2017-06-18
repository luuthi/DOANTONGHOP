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
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _iProductService;
        private readonly IAccountService _iAccountService;
        private readonly IOrderService _iOrderService;
        private readonly IOrderDetailService _iOrderDetailService;
        private bool typeError=false;
        public ShoppingCartController(IProductService iProductService, IAccountService iAccountService, IOrderService iOrderService,IOrderDetailService iOrderDetailService)
        {
            _iProductService = iProductService;
            _iAccountService = iAccountService;
            _iOrderDetailService = iOrderDetailService;
            _iOrderService = iOrderService;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
            
        }

        public ActionResult ProcessPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProductToCart(string productid, int soluong)
        {
            Guid id = Guid.Parse(productid);
            var pro = _iProductService.GetProductById(id);
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel()
            {
                ProductId = id,
                ProductName = pro.ProductName,
                Image = pro.Image,
                Price = pro.Price,
                Quantity = soluong,
                SubTotal = soluong * pro.Price,
                Unit = pro.Unit
            };
            List<ShoppingCartViewModel> lst = new List<ShoppingCartViewModel>();
            if (Session["Cart"] != null)
            {
                lst = Session["Cart"] as List<ShoppingCartViewModel>;
            }
            bool same = false;
            foreach (var item in lst)
            {
                if (item.ProductId == viewModel.ProductId)
                {
                    item.Quantity = item.Quantity + soluong;
                    item.SubTotal = item.Quantity * item.Price;
                    same = true;
                    break;
                }
            }
            if (same == false)
            {
                lst.Add(viewModel);
            }
            Session["Cart"] = lst;
            return Json(new { listpro = JsonClasses.ToPartialView(this, "_parViewCart", lst) });
        }
        [HttpPost]
        public ActionResult EditProductToCart(string productid, int soluong)
        {

            Guid id = Guid.Parse(productid);
            List<ShoppingCartViewModel> lst = new List<ShoppingCartViewModel>();
            if (Session["Cart"] != null)
            {
                lst = Session["Cart"] as List<ShoppingCartViewModel>;
                var item = lst.Where(o => o.ProductId == id).SingleOrDefault();
                item.Quantity = soluong;
                item.SubTotal = item.Quantity * item.Price;
            }

            Session["Cart"] = lst;
            return Json(new { listpro = JsonClasses.ToPartialView(this, "_parViewCart", lst) });
        }
        [HttpPost]
        public ActionResult RemoveItemCart(string productid)
        {

            Guid id = Guid.Parse(productid);
            List<ShoppingCartViewModel> lst = new List<ShoppingCartViewModel>();
            if (Session["Cart"] != null)
            {
                lst = Session["Cart"] as List<ShoppingCartViewModel>;
                var item = lst.Where(o => o.ProductId == id).SingleOrDefault();
                lst.Remove(item);
            }
            Session["Cart"] = lst;
            return Json(new { listpro = JsonClasses.ToPartialView(this, "_parViewCart", lst) });
        }
        [HttpPost]
        public ActionResult LoginAjax(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (doLogin(viewModel.UserName, viewModel.Password))
                {
                    Session["isLoginClient"] = true;
                    AccountViewModel acc =
                        _iAccountService.GetAccountByUserName(viewModel.UserName);
                    JsonClasses cls = new JsonClasses()
                    {
                        Status = true,
                        Html = JsonClasses.ToPartialView(this, "_parPayment1", acc)
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
                        Html = JsonClasses.ToPartialView(this, "_parPayment", viewModel)
                    });
                }
            }
            else
            {
                return Json(new JsonClasses()
                {
                    Status = false,
                    Html = JsonClasses.ToPartialView(this, "_parPayment", viewModel)
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

        [HttpPost]
        public ActionResult SaveInfo(OrderViewModel viewModel)
        {
            AccountViewModel acc = Session["AccountClient"] as AccountViewModel;
            viewModel.Email = acc.Email;
            viewModel.FullName = acc.FullName;
            viewModel.OrderAccount = acc.UserName;
            viewModel.OrderDate = DateTime.Now;
            viewModel.PhoneNumber = acc.PhoneNumber;
            Session["InfoOrder"] = viewModel;
            JsonClasses cls = new JsonClasses()
            {
                Status = true,
                Html = JsonClasses.ToPartialView(this, "_parPayment2",null)
            };
            return Json(cls);

        }
        [HttpPost]
        public ActionResult SaveInfo1(string note, string expectedDate)
        {
            OrderViewModel viewModel = new OrderViewModel();
            AccountViewModel acc = Session["AccountClient"] as AccountViewModel;
            viewModel.Email = acc.Email;
            viewModel.FullName = acc.FullName;
            viewModel.Notes = note;
            viewModel.OrderAccount = acc.UserName;
            viewModel.OrderDate = DateTime.Now;
            viewModel.ExpectedDate =Convert.ToDateTime(expectedDate);
            viewModel.PhoneNumber = acc.PhoneNumber;
            viewModel.PhoneReceiver = acc.PhoneNumber;
            viewModel.Receiver = acc.FullName;
            viewModel.Place = acc.Address;
            Session["InfoOrder"] = viewModel;
            JsonClasses cls = new JsonClasses()
            {
                Status = true,
                Html = JsonClasses.ToPartialView(this, "_parPayment2", null)
            };
            return Json(cls);
        }

        [HttpPost]
        public ActionResult ShowOverview()
        {
            JsonClasses cls = new JsonClasses()
            {
                Status = true,
                Html = JsonClasses.ToPartialView(this, "_parOverviewOrder", null)
            };
            return Json(cls);
        }

        [HttpPost]
        public ActionResult SaveOrder()
        {
            int numberofOrder = _iOrderService.CountOrderOnDay()+1;
            List<ShoppingCartViewModel> lst = Session["Cart"] as List<ShoppingCartViewModel>;
            decimal total = 0;
            foreach (ShoppingCartViewModel item in lst)
            {
                total += item.SubTotal;
            }
            OrderViewModel viewModel = new OrderViewModel();
            viewModel = Session["InfoOrder"] as OrderViewModel;
            DateTime d = DateTime.Today;
            viewModel.OrderCode = Extentions.AutoID("DH-" + d.Year + d.Month + d.Day, numberofOrder);
            viewModel.Status = 0;
            viewModel.TypePayment = 1;
            viewModel.TotalMoney = total;
            _iOrderService.InsertOrder(viewModel);
            SaveDetailOrder(lst,viewModel.Id);
            Session.Remove("Cart");
            Session.Remove("InfoOrder");
            JsonClasses cls = new JsonClasses()
            {
                Status = true
            };
            return Json(cls);
        }

        private void SaveDetailOrder(List<ShoppingCartViewModel> lst,Guid id)
        {
            foreach (ShoppingCartViewModel item in lst)
            {
                OrderDetailViewModel detail = new OrderDetailViewModel();
                detail.ProductId = item.ProductId;
                detail.Amount = item.Quantity;
                detail.Image = item.Image;
                detail.OrderId = id;
                detail.Price = item.Price;
                detail.ProductName = item.ProductName;
                detail.Total = item.SubTotal;
                _iOrderDetailService.InsertOrderDetail(detail);
            }
        }
        
        public ActionResult Success()
        {
            return View();
        }

        public ActionResult HistoryShopping(string accountId)
        {
            List<OrderViewModel> lst = new List<OrderViewModel>();
            lst = _iOrderService.GetOrderByAccount(accountId);
            return View(lst);
        }
        public ActionResult DetailShopping(string orderid)
        {
            Guid g = Guid.Parse(orderid);
            OrderViewModel viewModel = _iOrderService.GetOrderById(g);
            viewModel.lstDetail = _iOrderDetailService.GetOrderDetailByOrder(g);
            return View(viewModel);
        }
    }
}