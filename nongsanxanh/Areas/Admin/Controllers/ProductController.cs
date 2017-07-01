using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;
using NSX_Common;
using System.ComponentModel;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductGroupService _iProductGroupService;
        private readonly IProductService _iProductService;
        private readonly ICategoryService _iCategoryService;
        private readonly IProductAttributeService _iProductAttributeService;
        private readonly IProductAttributeValueService _iProductAttributeValueService;
        private readonly IOrderDetailService _iOderDetailService;
        string[] allowedExtensions = new[] { ".jpg", ".png", ".gif", ".jpeg" };

        public ProductController(IProductGroupService iProductGroupService, IProductService iProductService, ICategoryService iCategoryService, IProductAttributeService iProductAttributeService, IProductAttributeValueService iProductAttributeValueService, IOrderDetailService iOderDetailService)
        {
            _iProductGroupService = iProductGroupService;
            _iProductAttributeService = iProductAttributeService;
            _iProductAttributeValueService = iProductAttributeValueService;
            _iCategoryService = iCategoryService;
            _iProductService = iProductService;
            _iOderDetailService = iOderDetailService;
        }
        #region Load data combo
        private List<SelectListItem> LoadProGroup()
        {
            var units = _iProductGroupService.GetAllProductGroup();
            var lst = new List<SelectListItem>();
            lst.Add(new SelectListItem()
            {
                Value = "null",
                Text = "Chọn nhóm............."
            });
            units.ForEach(m =>
            {
                lst.Add(new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.GroupName
                });
            });
            return lst;
        }
        private List<SelectListItem> LoadAttribute()
        {
            var units = _iProductAttributeService.GetAllProducAttributet();
            var lst = new List<SelectListItem>();
            lst.Add(new SelectListItem()
            {
                Value = "null",
                Text = "Chọn thuộc tính............."
            });
            units.ForEach(m =>
            {
                lst.Add(new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.AttributeName
                });
            });
            return lst;
        }
        public static string GetDescriptionFromEnumValue(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
        private void LoadCategory(string selected)
        {
            var lstEnum = Enum.GetValues(typeof(CategoryNew)).Cast<CategoryNew>().ToList();
            var lst = new List<SelectListItem>();

            foreach (var item in lstEnum)
            {
                lst.Add(new SelectListItem
                {
                    Value = item.ToString(),
                    Text = GetDescriptionFromEnumValue((CategoryNew)item),
                    Selected = selected.Contains(item.ToString())
                });
            }
            ViewData["EnumCategory"] = lst;
        }

        [HttpPost]
        public ActionResult LoadAtrr(string masanpham)
        {
            Guid g = Guid.Parse(masanpham);
            var viewModel = _iProductAttributeService.GetAllProducAttributebySP(g);
            return PartialView("_parAttrValPro", viewModel);
        }
        #endregion

        // GET: Admin/Product
        public ActionResult Index()
        {
            var viewModel = _iProductService.GetAllProduct().Where(m => m.Status == true).ToList();
            return View(viewModel);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            var viewModel = new ProductViewModel();
            viewModel.CanComment = true;
            viewModel.Status = true;
            viewModel.Image = Constants.NOIMAGE;
            viewModel.lstGroup = LoadProGroup();
            LoadCategory("");
            return View(viewModel);
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductViewModel viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    AccountViewModel account = Session["Account"] as AccountViewModel;
                    viewModel.Description = viewModel.Description ?? String.Empty;
                    viewModel.DetailInfo = viewModel.DetailInfo ?? String.Empty;
                    viewModel.Vendor = viewModel.Vendor ?? String.Empty;
                    viewModel.Gallery = viewModel.Gallery ?? String.Empty;
                    string fileName = "";
                    if (viewModel.filePosted != null)
                    {
                        var checkextension = Path.GetExtension(viewModel.filePosted.FileName).ToLower();
                        if (viewModel.filePosted != null && viewModel.filePosted.ContentLength > 0)
                        {
                            if (allowedExtensions.Contains(checkextension))
                            {
                                fileName = MediaHelper.SaveImageFile(viewModel.filePosted, Constants.sanpham_folder, Extentions.ToUnsignLinkString(viewModel.ProductName.Length > 10 ? viewModel.ProductName.Substring(0, 10) : viewModel.ProductName));
                            }
                            else
                            {
                                ModelState.AddModelError("filePosted", "File không hợp lệ");
                                return View(viewModel);
                            }
                        }
                        else
                        {
                            viewModel.lstGroup = LoadProGroup();
                            LoadCategory(viewModel.Category);
                            return View(viewModel);
                        }
                    }
                    else
                    {
                        fileName = Constants.NOIMAGE;
                    }

                    viewModel.Image = fileName;
                    viewModel.Creator = account.UserName;
                    viewModel.CreatedDate = DateTime.Now;
                    viewModel.ModifyDate = DateTime.Now;
                    viewModel.Modifier = account.UserName;
                    if (viewModel.EnumCategory != null)
                    {
                        viewModel.Category = string.Join(",", viewModel.EnumCategory);
                    }
                    else
                    {
                        viewModel.Category = String.Empty;
                    }
                    _iProductService.InsertProduct(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    viewModel.lstGroup = LoadProGroup();
                    LoadCategory(viewModel.Category??String.Empty);
                    return View(viewModel);
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(string proid)
        {
            Guid g = Guid.Parse(proid);
            var viewModel = _iProductService.GetProductById(g);
            LoadCategory(viewModel.Category);
            viewModel.lstGroup = LoadProGroup();
            return View(viewModel);
        }

        // POST: Admin/Product/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(string proid, ProductViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                Guid g = Guid.Parse(proid);
                if (ModelState.IsValid)
                {
                    viewModel.Id = g;
                    AccountViewModel account = Session["Account"] as AccountViewModel;
                    viewModel.Description = viewModel.Description ?? String.Empty;
                    viewModel.DetailInfo = viewModel.DetailInfo ?? String.Empty;
                    viewModel.Gallery = viewModel.Gallery ?? String.Empty;
                    viewModel.Vendor = viewModel.Vendor ?? String.Empty;
                    string fileName = "";
                    if (viewModel.filePosted != null)
                    {
                        var checkextension = Path.GetExtension(viewModel.filePosted.FileName).ToLower();
                        if (viewModel.filePosted != null && viewModel.filePosted.ContentLength > 0)
                        {
                            if (allowedExtensions.Contains(checkextension))
                            {
                                fileName = MediaHelper.SaveImageFile(viewModel.filePosted, Constants.sanpham_folder, Extentions.ToUnsignLinkString(viewModel.ProductName.Length > 10 ? viewModel.ProductName.Substring(0, 10) : viewModel.ProductName));
                            }
                            else
                            {
                                ModelState.AddModelError("filePosted", "File không hợp lệ");
                                return View(viewModel);
                            }
                        }
                        else
                        {
                            viewModel.lstGroup = LoadProGroup();
                            LoadCategory(viewModel.Category);
                            return View(viewModel);
                        }
                    }
                    else
                    {
                        fileName = viewModel.Image;
                    }

                    viewModel.Image = fileName;
                    viewModel.ModifyDate = DateTime.Now;
                    viewModel.Modifier = account.UserName;
                    if (viewModel.EnumCategory!=null)
                    {
                        viewModel.Category = string.Join(",", viewModel.EnumCategory);
                    }
                    else
                    {
                        viewModel.Category = String.Empty;
                    }
                    _iProductService.UpdateProduct(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    viewModel.lstGroup = LoadProGroup();
                    LoadCategory(viewModel.Category ?? String.Empty);
                    return View(viewModel);
                }
            }
            catch
            {
                return View();
            }
        }

       
        // POST: Admin/Product/Delete/5
        [HttpPost]
        public ActionResult Delete(string proid)
        {
            try
            {
                // TODO: Add delete logic here
                Guid g = Guid.Parse(proid);
                _iProductService.DeleteProduct(g);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Delete_Del(string proid)
        {

            // TODO: Add delete logic here
            Guid g = Guid.Parse(proid);
            if (_iOderDetailService.GetOrderDetailByProduct(g)>0)
            {
                JsonClasses cls = new JsonClasses()
                {
                    Status = false,
                    Html = "Sản phẩm đã được bán.Không xóa được!"
                };
                return Json(cls);
            }
            else
            {
                _iProductService.DeleteProduct_del(g);
                var lst = _iProductService.GetAllProduct().Where(m => m.Status == false).ToList();
                JsonClasses cls = new JsonClasses()
                {
                    Status = true,
                    Html = JsonClasses.ToPartialView(this, "_parDisabledProduct", lst)
                };
                return Json(cls);
            }
            

        }
        [HttpPost]
        public ActionResult LoadDisabledPro()
        {
            var lst = _iProductService.GetAllProduct().Where(m => m.Status == false).ToList();
            return PartialView("_parDisabledProduct", lst);
        }
        [HttpPost]
        public ActionResult Restore(string proid)
        {
            Guid g = Guid.Parse(proid);
            var viewModel = _iProductService.GetProductById(g);
            viewModel.Status = true;
            _iProductService.UpdateProduct(viewModel);
            var lst = _iProductService.GetAllProduct().Where(m => m.Status == false).ToList();
            return PartialView("_parDisabledProduct", lst);
        }

        [HttpPost]
        public ActionResult DeleteByPro(string masanpham)
        {
            Guid g = Guid.Parse(masanpham);
            _iProductAttributeValueService.DeleteProductAttributeValue(g);
            return View();
        }

        [HttpPost]
        public ActionResult SaveAttr(ProductAttributeValueViewModel viewModel)
        {
            _iProductAttributeValueService.InsertProductAttributeValue(viewModel);
            return Json(new { success = 1, msg = "OK" });
        }
    }
}
