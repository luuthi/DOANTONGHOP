using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;
using NSX_Common;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductGroupService _iProductGroupService;
        private readonly IProductService _iProductService;
        private readonly ICategoryService _iCategoryService;
        private readonly IProductAttributeService _iProductAttributeService;
        private readonly ProductAttributeValueService _iProductAttributeValueService;
        string[] allowedExtensions = new[] { ".jpg", ".png", ".gif", ".jpeg" };

        public ProductController(IProductGroupService iProductGroupService, IProductService iProductService, ICategoryService iCategoryService, IProductAttributeService iProductAttributeService, ProductAttributeValueService iProductAttributeValueService)
        {
            _iProductGroupService = iProductGroupService;
            _iProductAttributeService = iProductAttributeService;
            _iProductAttributeValueService = iProductAttributeValueService;
            _iCategoryService = iCategoryService;
            _iProductService = iProductService;
        }
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
        private void LoadCategory(string selected)
        {
            var cate = _iCategoryService.GetlAllCategories();
            var lst = new List<SelectListItem>();
            cate.ForEach(m =>
            {
                lst.Add(new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CategoryName,
                    Selected = selected.Contains(m.Id.ToString())
                });
            });
            ViewData["EnumCategory"] = lst;
        }
        // GET: Admin/Product
        public ActionResult Index()
        {
            var viewModel = _iProductService.GetAllProduct();
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
                    viewModel.Category = string.Join(",", viewModel.EnumCategory);
                    _iProductService.InsertProduct(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    viewModel.lstGroup = LoadProGroup();
                    LoadCategory(viewModel.Category);
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
                    viewModel.Category = string.Join(",", viewModel.EnumCategory);
                    _iProductService.UpdateProduct(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    viewModel.lstGroup = LoadProGroup();
                    LoadCategory(viewModel.Category);
                    return View(viewModel);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult AddRow()
        {
            var viewModel = new ProductAttributeValueViewModel();
            viewModel.lstAttribute = LoadAttribute();
            return PartialView("_parAttrValPro", viewModel);
        }

        [HttpPost]
        public ActionResult AddAttr()
        {
            var viewModel = new ProductAttributeViewModel();
            return PartialView("_parAddNewAttr", viewModel);
        }

        [HttpPost]
        public ActionResult LoadAttrByProId(string proid)
        {
            Guid g = Guid.Parse(proid);
            var viewModel = _iProductAttributeValueService.GetProductAttributeValueByProId(g);
            return PartialView("_parModalDetail", viewModel);
        }

        [HttpPost]
        public ActionResult SaveAttr(ProductAttributeViewModel viewModel)
        {
            viewModel.AttributeType = viewModel.AttributeType ?? String.Empty;
            _iProductAttributeService.InsertProductAttribute(viewModel);
            return View();
        }
    }
}
