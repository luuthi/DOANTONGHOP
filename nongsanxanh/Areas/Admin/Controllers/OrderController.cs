using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _iOrderService;
        private readonly IOrderDetailService _iOrderDetailService;
        private readonly IAccountService _iAccountService;
        private readonly IProductService _iProductService;
        public OrderController(IOrderDetailService iOrderDetailService,IOrderService iOrderService,IAccountService iAccountService,IProductService iProductService)
        {
            _iAccountService = iAccountService;
            _iOrderDetailService = iOrderDetailService;
            _iOrderService = iOrderService;
            _iProductService = iProductService;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");
        }
        private List<SelectListItem> LoadStatus()
        {
            var lst = new List<SelectListItem>();
            lst.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Chưa xử lý đơn hàng"
            });
            lst.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Đã xử lý đơn hàng"
            });
            lst.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Đang giao hàng"
            });
            lst.Add(new SelectListItem()
            {
                Value = "3",
                Text = "Đã giao hàng"
            });
            lst.Add(new SelectListItem()
            {
                Value = "4",
                Text = "Đơn hàng đã hủy"
            });
            return lst;
        }
        // GET: Admin/Order
        public ActionResult Index()
        {
            var viewModel = _iOrderService.GetAllOrder().Where(o=>o.Status!=4).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult LoadDisabled()
        {
            var viewModel = _iOrderService.GetAllOrder().Where(o => o.Status == 4).ToList();
            return PartialView("_parDisabledOrder", viewModel);
        }
        // GET: Admin/Order/Details/5
        [HttpPost]
        public ActionResult Details(string orderid)
        {
            Guid g = Guid.Parse(orderid);
            OrderViewModel viewModel = _iOrderService.GetOrderById(g);
            viewModel.Id = g;
            viewModel.lstStatus = LoadStatus();
            return PartialView("_parStatus",viewModel);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Order/Create
        [HttpPost]
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

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(string orderid)
        {
            Guid g = Guid.Parse(orderid);
            OrderViewModel viewModel = _iOrderService.GetOrderById(g);
            viewModel.lstDetail = _iOrderDetailService.GetOrderDetailByOrder(g);
            return View(viewModel);
        }

        // POST: Admin/Order/Edit/5
        [HttpPost]
        public ActionResult Edit(OrderViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                _iOrderService.UpdateOrder(viewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Restore(OrderViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                _iOrderService.UpdateOrder(viewModel);
                var lst = _iOrderService.GetAllOrder().Where(o => o.Status == 4).ToList();
                return PartialView("_parDisabledOrder", lst);
            }
            catch
            {
                return View();
            }
        }
        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Order/Delete/5
        [HttpPost]
        public ActionResult Delete(string orderid)
        {
            try
            {
                // TODO: Add delete logic here
                Guid g = Guid.Parse(orderid);
                _iOrderService.DeleteOrder(g);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
