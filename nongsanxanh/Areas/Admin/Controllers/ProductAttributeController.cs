using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class ProductAttributeController : Controller
    {
        private readonly IProductAttributeService _iProductAttributeService;

        public ProductAttributeController(IProductAttributeService iProductAttributeService)
        {
            _iProductAttributeService = iProductAttributeService;
        }
        // GET: Admin/ProductAttribute
        public ActionResult Index()
        {
            var viewModel = _iProductAttributeService.GetAllProducAttributet();
            return View(viewModel);
        }

        // GET: Admin/ProductAttribute/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/ProductAttribute/Create
        public ActionResult Create()
        {
            ProductAttributeViewModel viewModel = new ProductAttributeViewModel();
            return View(viewModel);
        }

        // POST: Admin/ProductAttribute/Create
        [HttpPost]
        public ActionResult Create(ProductAttributeViewModel viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    viewModel.AttributeType = String.Empty;
                    _iProductAttributeService.InsertProductAttribute(viewModel);
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

        // GET: Admin/ProductAttribute/Edit/5
        public ActionResult Edit(string proAttrId)
        {
            Guid g = Guid.Parse(proAttrId);
            var viewModel = _iProductAttributeService.GetProductAttributeById(g);
            return View(viewModel);
        }

        // POST: Admin/ProductAttribute/Edit/5
        [HttpPost]
        public ActionResult Edit(string proAttrId, ProductAttributeViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                Guid g = Guid.Parse(proAttrId);
                if (ModelState.IsValid)
                {
                    viewModel.Id = g;
                    viewModel.AttributeType = String.Empty;
                    _iProductAttributeService.UpdateProductAttribute(viewModel);
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

        // GET: Admin/ProductAttribute/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/ProductAttribute/Delete/5
        [HttpPost]
        public ActionResult Delete(string proAttrId)
        {
            try
            {
                // TODO: Add delete logic here
                Guid g = Guid.Parse(proAttrId);
                _iProductAttributeService.DeleteProductAttribute(g);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
