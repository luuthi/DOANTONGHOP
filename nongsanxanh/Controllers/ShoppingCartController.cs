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

        public ShoppingCartController(IProductService iProductService)
        {
            _iProductService = iProductService;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProductToCart(string productid,int soluong)
        {
            Guid id = Guid.Parse(productid);
            var pro = _iProductService.GetProductById(id);
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel()
            {
                ProductId =  id,
                ProductName = pro.ProductName,
                Image = pro.Image,
                Price = pro.Price,
                Quantity = soluong,
                SubTotal =  soluong*pro.Price,
                Unit = pro.Unit
            };
            List<ShoppingCartViewModel> lst = new List<ShoppingCartViewModel>();
            if (Session["Cart"]!=null)
            {
                lst = Session["Cart"] as List<ShoppingCartViewModel>;
            }
            bool same = false;
            foreach (var item in lst)
            {
                if (item.ProductId == viewModel.ProductId)
                {
                    item.Quantity++;
                    same = true;
                    break;
                }
            }
            if (same==false)
            {
                lst.Add(viewModel);
            }
            Session["Cart"] = lst;
            return Json(new {listpro = JsonClasses.ToPartialView(this, "_parViewCart", lst) });
        }
    }
}