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
    public class AccountMngController : Controller
    {
        private readonly IAccountService _iAccountService;
        private readonly IGroupAccountService _iGroupAccountService;
        string[] allowedExtensions = new[] { ".jpg", ".png", ".gif", ".jpeg" };
        public AccountMngController(IAccountService iAccountService, IGroupAccountService iGroupAccountService)
        {
            _iGroupAccountService = iGroupAccountService;
            _iAccountService = iAccountService;
        }
        private void LoadGroupAcc()
        {
            var units = _iGroupAccountService.GetAllAccountGroup();
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
                    Value = m.GroupCode.ToString(),
                    Text = m.GroupName
                });
            });
            ViewData["GroupAccId"] = lst;
        }
        // GET: Admin/AccountMng
        public ActionResult Index()
        {
            var viewModel = _iAccountService.GetAllAccount();
            return View(viewModel);
        }

        // GET: Admin/AccountMng/Details/5
        [HttpPost]
        public ActionResult Details(string accountid)
        {
            Guid g = Guid.Parse(accountid);
            AccountViewModel viewModel = _iAccountService.GetAccountById(g);
            return PartialView("_parInforPopup", viewModel);
        }

        // GET: Admin/AccountMng/Create
        public ActionResult Create()
        {
            AccountViewModel viewModel = new AccountViewModel();
            viewModel.Birthday = DateTime.Now.Date;
            viewModel.Image = Constants.NOIMAGE;
            LoadGroupAcc();
            return View(viewModel);
        }

        // POST: Admin/AccountMng/Create
        [HttpPost]
        public ActionResult Create(AccountViewModel viewModel)
        {
            try
            {
                ModelState.Remove("RePassword");
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    viewModel.Email = viewModel.Email ?? String.Empty;
                    viewModel.PhoneNumber = viewModel.PhoneNumber ?? String.Empty;
                    viewModel.Address = viewModel.Address ?? String.Empty;
                    viewModel.Password = Extentions.Md5Hash(viewModel.Password);
                    string fileName = "";
                    if (viewModel.filePosted != null)
                    {
                        var checkextension = Path.GetExtension(viewModel.filePosted.FileName).ToLower();
                        if (viewModel.filePosted != null && viewModel.filePosted.ContentLength > 0)
                        {
                            if (allowedExtensions.Contains(checkextension))
                            {
                                fileName = MediaHelper.SaveImageFile(viewModel.filePosted, Constants.userlogo, Extentions.ToUnsignLinkString(viewModel.UserName));
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
                    _iAccountService.InsertAccount(viewModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    LoadGroupAcc();
                    return View(viewModel);
                }
            }
            catch(Exception e)
            {
                LoadGroupAcc();
                return View();
            }
        }

        // GET: Admin/AccountMng/Edit/5
        public ActionResult Edit(string accountid)
        {
            Guid g;
            if (Guid.TryParse(accountid, out g))
            {
                var viewModel = _iAccountService.GetAccountById(g);
                LoadGroupAcc();
                return View(viewModel);
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult ChangeStatus(string status,string accountid)
        {
            bool stt = Convert.ToBoolean(status);
            Guid g = Guid.Parse(accountid);
            _iAccountService.UpdateStt(stt,g);
            return View("Index");
        }
        // POST: Admin/AccountMng/Edit/5
        [HttpPost]
        public ActionResult Edit(string accountid, AccountViewModel viewModel)
        {
            try
            {
                Guid g = Guid.Parse(accountid);
                ModelState.Remove("RePassword");
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    AccountViewModel acc = _iAccountService.GetAccountByUserName(viewModel.UserName, NSX_Common.Extentions.Md5Hash(viewModel.Password));
                    if (acc.UserName != null)
                    {
                        viewModel.Id = g;
                        viewModel.Email = viewModel.Email ?? String.Empty;
                        viewModel.PhoneNumber = viewModel.PhoneNumber ?? String.Empty;
                        viewModel.Address = viewModel.Address ?? String.Empty;
                        viewModel.Password = Extentions.Md5Hash(viewModel.Password);
                        string fileName = "";
                        if (viewModel.filePosted != null)
                        {
                            var checkextension = Path.GetExtension(viewModel.filePosted.FileName).ToLower();
                            if (viewModel.filePosted != null && viewModel.filePosted.ContentLength > 0)
                            {
                                if (allowedExtensions.Contains(checkextension))
                                {
                                    fileName = MediaHelper.SaveImageFile(viewModel.filePosted, Constants.userlogo,
                                        Extentions.ToUnsignLinkString(viewModel.UserName));
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
                        _iAccountService.UpdateAccount(viewModel);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Sai mật khẩu");
                        LoadGroupAcc();
                        return View(viewModel);
                    }
                }
                else
                {
                    LoadGroupAcc();
                    return View(viewModel);
                }
            }
            catch
            {
                LoadGroupAcc();
                return View();
            }
        }
        [HttpPost]
        public ActionResult UpdatePopUp(AccountViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                viewModel.Email = viewModel.Email ?? String.Empty;
                viewModel.PhoneNumber = viewModel.PhoneNumber ?? String.Empty;
                viewModel.Address = viewModel.Address ?? String.Empty;
                _iAccountService.UpdateInfo(viewModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Admin/AccountMng/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/AccountMng/Delete/5
        [HttpPost]
        public ActionResult Delete(string accountid)
        {
            try
            {
                // TODO: Add delete logic here
                Guid g = Guid.Parse(accountid);
                _iAccountService.DeleteAccount(g);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
