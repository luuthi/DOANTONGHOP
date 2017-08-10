using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;
using PagedList;

namespace nongsanxanh.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductService _iProductService;
        public SearchController(IProductService iProductService)
        {
            _iProductService = iProductService;
        }
        // GET: Search
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var strArr = searchString.Split(' ');
            List<ProductViewModel> lst =new List<ProductViewModel>();
            var rs = _iProductService.GetAllProduct();
            foreach (string str in strArr)
            {
                var result=  rs.Where(m => m.ProductName.ToLower().Contains(str) && m.Status).ToList();
                lst.AddRange(result);
            }
            var viewModel =  lst.GroupBy(m => m.Id).Select(o => o.FirstOrDefault()).ToList();
            return View(viewModel.ToPagedList(pageNumber, pageSize));
        }
    }
}