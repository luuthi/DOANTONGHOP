using Bussiness.Interface;
using Bussiness.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace nongsanxanh.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _iProductService;
        private readonly IProductGroupService _iProductGroupService;
        public ProductsController(IProductService iProductService, IProductGroupService iProductGroupService)
        {
            _iProductGroupService = iProductGroupService;
            _iProductService = iProductService;
        }
        // GET: Products
        public ActionResult Index()
        {
            var lstGroup = new List<ProductGroupViewModel>();

            lstGroup = _iProductGroupService.GetAllProductGroup();
            ProductAllViewModel viewModel = new ProductAllViewModel();
            viewModel.lstListProduct = new List<List<ProductViewModel>>();
            foreach (var item in lstGroup)
            {
                var rs = _iProductService.GetAllProduct();
                List<ProductViewModel> lstPro = new List<ProductViewModel>();
                lstPro = rs.Where(m => m.GroupId == item.Id).Take(4).ToList();
                if (lstPro.Count==0)
                {
                    ProductViewModel pro = new ProductViewModel();
                    pro.GroupName = item.GroupName;
                    lstPro.Add(pro);
                }
                viewModel.lstListProduct.Add(lstPro);
            }
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Group(string catid, int? page)
        {
            var lstGroup = new List<ProductGroupViewModel>();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            lstGroup = _iProductGroupService.GetAllProductGroup().Where(o=>o.Url==catid).ToList();
            List<ProductViewModel> lstPro = new List<ProductViewModel>();
            foreach (var item in lstGroup)
            {
                var rs = _iProductService.GetAllProduct();
                lstPro = rs.Where(m => m.GroupId == item.Id).ToList();
                if (lstPro.Count == 0)
                {
                    ProductViewModel pro = new ProductViewModel();
                    pro.GroupName = item.GroupName;
                    lstPro.Add(pro);
                }
            }
            return View(lstPro.ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult Details(string productid)
        {
            var detailProduct = _iProductService.GetProductById(Guid.Parse(productid));
            return View(detailProduct);
        }
    }
}