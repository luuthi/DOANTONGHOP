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
    public class CompanyInfoController : Controller
    {
        private readonly ICompanyInfoService _iCompanyInfoService;
        string[] allowedExtensions = new[] { ".jpg", ".png", ".gif", ".jpeg" };

        public CompanyInfoController(ICompanyInfoService iCompanyInfoService)
        {
            _iCompanyInfoService = iCompanyInfoService;
        }
        // GET: Admin/CompanyInfo
        public ActionResult Index()
        {
            var viewModel = _iCompanyInfoService.GetCompanyInfo();
            viewModel.Logo = viewModel.Logo?? Constants.NOIMAGE;
            return View(viewModel);
        }

        // GET: Admin/CompanyInfo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CompanyInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CompanyInfo/Create
        [HttpPost]
        public ActionResult Create(CompanyInfoViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    viewModel.Fax = viewModel.Fax ?? String.Empty;
                    viewModel.Mobile = viewModel.Mobile ?? String.Empty;
                    viewModel.Website = viewModel.Website ?? String.Empty;
                    viewModel.Logo = viewModel.Logo ?? String.Empty;
                    viewModel.Facebook = viewModel.Facebook ?? String.Empty;
                    string fileName = "";
                    if (viewModel.filePosted != null)
                    {
                        var checkextension = Path.GetExtension(viewModel.filePosted.FileName).ToLower();
                        if (viewModel.filePosted != null && viewModel.filePosted.ContentLength > 0)
                        {
                            if (allowedExtensions.Contains(checkextension))
                            {
                                fileName = MediaHelper.SaveImageFile(viewModel.filePosted,Constants.hethong, Extentions.ToUnsignLinkString(viewModel.CompanyName));
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

                    viewModel.Logo = fileName;
                    _iCompanyInfoService.DeleteAccount();
                    _iCompanyInfoService.InsertInfo(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Index",viewModel);
                }
            }
            catch
            {
                return View("Index", viewModel);
            }
        }

        // GET: Admin/CompanyInfo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/CompanyInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/CompanyInfo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/CompanyInfo/Delete/5
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
    }
}
