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
    public class NewsController : Controller
    {
        private readonly INewsService _iNewsService;
        private readonly INewsGroupService _iNewsGroupService;
        private readonly ICategoryService _iCategoryService;
        string[] allowedExtensions = new[] { ".jpg", ".png", ".gif", ".jpeg" };
        public NewsController(INewsGroupService iNewsGroupService,INewsService iNewsService,ICategoryService iCategoryService)
        {
            _iNewsGroupService = iNewsGroupService;
            _iNewsService = iNewsService;
            _iCategoryService = iCategoryService;
        }
        private void LoadNewsGroup()
        {
            var units = _iNewsGroupService.GetAllNewsGroup();
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
                    Text = m.NameGroup
                });
            });
            ViewData["GroupId"] = lst;
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
        // GET: Admin/News
        public ActionResult Index()
        {
            var viewModel = _iNewsService.GetAllNews().Where(m=>m.Status==true).ToList();
            return View(viewModel);
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            NewsViewModel viewModel =new NewsViewModel();
            LoadNewsGroup();
            LoadCategory("");
            viewModel.Status = true;
            viewModel.CanComment = true;
            viewModel.Image = Constants.NOIMAGE;
            return View(viewModel);
        }

        // POST: Admin/News/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NewsViewModel viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    AccountViewModel account = Session["Account"] as AccountViewModel;
                    viewModel.Content = viewModel.Content ?? String.Empty;
                    string fileName = "";
                    if (viewModel.filePosted != null)
                    {
                        var checkextension = Path.GetExtension(viewModel.filePosted.FileName).ToLower();
                        if (viewModel.filePosted != null && viewModel.filePosted.ContentLength > 0)
                        {
                            if (allowedExtensions.Contains(checkextension))
                            {
                                fileName = MediaHelper.SaveImageFile(viewModel.filePosted, Constants.tintuc_folder, Extentions.ToUnsignLinkString(viewModel.Title.Length >10 ? viewModel.Title.Substring(0,10): viewModel.Title));
                            }
                            else
                            {
                                ModelState.AddModelError("filePosted", "File không hợp lệ");
                                return View(viewModel);
                            }
                        }
                    }
                    else
                    {
                        fileName = Constants.NOIMAGE;
                    }

                    viewModel.Image = fileName;
                    viewModel.PostAccount = account.UserName;
                    viewModel.PostedDate =DateTime.Now;
                    viewModel.ModifyDate = DateTime.Now;
                    viewModel.Modifier = account.UserName;
                    viewModel.Category = string.Join(",", viewModel.EnumCategory);
                    _iNewsService.InsertNews(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    LoadNewsGroup();
                    return View(viewModel);
                }
            }
            catch (Exception exception)
            {
                LoadNewsGroup();
                LoadCategory("");
                return View();
            }
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(string newsid)
        {
            Guid g = Guid.Parse(newsid);
            var viewModel = _iNewsService.GetNewsById(g);
            LoadNewsGroup();
            LoadCategory(viewModel.Category);
            return View(viewModel);
        }

        // POST: Admin/News/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(string newsid, NewsViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                Guid g = Guid.Parse(newsid);
                if (ModelState.IsValid)
                {
                    viewModel.Id = g;
                    AccountViewModel account = Session["Account"] as AccountViewModel;
                    viewModel.Content = viewModel.Content ?? String.Empty;
                    string fileName = "";
                    if (viewModel.filePosted != null)
                    {
                        var checkextension = Path.GetExtension(viewModel.filePosted.FileName).ToLower();
                        if (viewModel.filePosted != null && viewModel.filePosted.ContentLength > 0)
                        {
                            if (allowedExtensions.Contains(checkextension))
                            {
                                fileName = MediaHelper.SaveImageFile(viewModel.filePosted, Constants.tintuc_folder, Extentions.ToUnsignLinkString(viewModel.Title));
                            }
                            else
                            {
                                ModelState.AddModelError("filePosted", "File không hợp lệ");
                                return View(viewModel);
                            }
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
                    _iNewsService.UpdateNews(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    LoadNewsGroup();
                    return View(viewModel);
                }
            }
            catch
            {
                LoadNewsGroup();
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult Delete(string newsid)
        {
            try
            {
                // TODO: Add delete logic here
                Guid g = Guid.Parse(newsid);
                _iNewsService.DeleteNews(g);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Delete_Del(string newsid)
        {
           
                // TODO: Add delete logic here
                Guid g = Guid.Parse(newsid);
                _iNewsService.DeleteNews_Del(g);
                var lst = _iNewsService.GetAllNews().Where(m => m.Status == false).ToList();
                return PartialView("_parDisabledNews", lst);
           
        }

        [HttpPost]
        public ActionResult LoadDisabledNews()
        {
            var viewModel = _iNewsService.GetAllNews().Where(m=>m.Status==false).ToList();
            return PartialView("_parDisabledNews", viewModel);
        }

        [HttpPost]
        public ActionResult Restore(string newsid)
        {
            Guid g = Guid.Parse(newsid);
            var viewModel = _iNewsService.GetNewsById(g);
            viewModel.Status = true;
            _iNewsService.UpdateNews(viewModel);
            var lst = _iNewsService.GetAllNews().Where(m => m.Status == false).ToList();
            return PartialView("_parDisabledNews", lst);
        }
    }
}
