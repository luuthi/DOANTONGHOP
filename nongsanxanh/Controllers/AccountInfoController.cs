using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;
using NSX_Common;

namespace nongsanxanh.Controllers
{
    public class AccountInfoController : Controller
    {
        private readonly IAccountService _iAccountService;
        string[] allowedExtensions = new[] { ".jpg", ".png", ".gif", ".jpeg" };
        public AccountInfoController(IAccountService iAccountService)
        {
            _iAccountService = iAccountService;
        }
        // GET: AccountInfo
        public ActionResult Index(string accountid)
        {
            Guid id = Guid.Parse(accountid);
            AccountViewModel viewModel = _iAccountService.GetAccountById(id);
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Edit(string accountid)
        {
            Guid id = Guid.Parse(accountid);
            AccountViewModel viewModel = _iAccountService.GetAccountById(id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(string accountid,AccountViewModel viewModel)
        {
            if (viewModel.FullName !="" && viewModel.Email !="")
            {
                AccountViewModel acc = _iAccountService.GetAccountById(Guid.Parse(accountid));
                viewModel.Id = Guid.Parse(accountid);
                viewModel.PhoneNumber = viewModel.PhoneNumber ?? String.Empty;
                viewModel.Address = viewModel.Address ?? String.Empty;
                string fileName = "";
                if (viewModel.filePosted != null)
                {
                    var checkextension = Path.GetExtension(viewModel.filePosted.FileName).ToLower();
                    if (viewModel.filePosted != null && viewModel.filePosted.ContentLength > 0)
                    {
                        if (allowedExtensions.Contains(checkextension))
                        {
                            fileName = MediaHelper.SaveImageFile(viewModel.filePosted, Constants.userlogo,
                                Extentions.ToUnsignLinkString(acc.UserName));
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
                    fileName = acc.Image;
                }
                viewModel.Image = fileName;
                _iAccountService.UpdateInfo(viewModel);
                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Change()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Check(string UserName, string Password)
        {
            var rs = _iAccountService.GetAccountByUserName(UserName, NSX_Common.Extentions.Md5Hash(Password));
            if (rs.UserName!=null)
            {
                return Json(new JsonClasses()
                {
                    Status = true,
                    Html = ""
                });
            }
            else
            {
                return Json(new JsonClasses()
                {
                    Status = false,
                    Html = ""
                });
            }
        }
    }
}