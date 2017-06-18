using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;
using NSX_Common;
using BotDetect.Web.Mvc;

namespace nongsanxanh.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAccountService _iAccountService;
        private readonly IGroupAccountService _iGroupAccountService;
        string[] allowedExtensions = new[] { ".jpg", ".png", ".gif", ".jpeg" };
        public RegisterController(IAccountService iAccountService, IGroupAccountService iGroupAccountService)
        {
            _iGroupAccountService = iGroupAccountService;
            _iAccountService = iAccountService;
        }
        // GET: Register
        public ActionResult Index()
        {
            AccountViewModel viewModel = new AccountViewModel();
            viewModel.Birthday = DateTime.Now.Date;
            viewModel.Image = Constants.NOIMAGE;
            return View(viewModel);
        }

        public ActionResult Success()
        {
            return View();
        }

        private bool CheckUserName(string userName)
        {
            AccountViewModel acc=new AccountViewModel();
            acc = _iAccountService.GetAccountByUserName(userName);
            if (acc.UserName != String.Empty)
            {
                return true;
            }
            return false;
        }

        private bool CheckEmail(string email)
        {
            AccountViewModel acc = new AccountViewModel();
            acc = _iAccountService.GetAccountByEmail(email);
            if (acc.UserName != String.Empty)
            {
                return true;
            }
            return false;
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "exampleCaptcha", "Mã captcha chưa đúng")]
        public ActionResult Register(AccountViewModel viewModel)
        {
            ModelState.Remove("GroupAccId");
            if (ModelState.IsValid)
            {
                if (viewModel.Password == viewModel.RePassword)
                {
                    if (CheckUserName(viewModel.UserName))
                    {
                        if (CheckEmail(viewModel.Email))
                        {
                            viewModel.Email = viewModel.Email ?? String.Empty;
                            viewModel.PhoneNumber = viewModel.PhoneNumber ?? String.Empty;
                            viewModel.Address = viewModel.Address ?? String.Empty;
                            viewModel.Password = Extentions.Md5Hash(viewModel.Password);
                            viewModel.GroupAccId = "KHACHHANG";
                            viewModel.Status = false;
                            viewModel.TypeAccount = false;
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
                                        return View("Index", viewModel);
                                    }
                                }
                            }
                            else
                            {
                                fileName = Constants.NOIMAGE;
                            }

                            viewModel.Image = fileName;
                            _iAccountService.InsertAccount(viewModel);
                            return RedirectToAction("Success");
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "Email đã tồn tại");
                            return View("Index", viewModel);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "Tên đăng nhập đã được sử dụng");
                        return View("Index", viewModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("RePassword","Mật khẩu nhập lại chưa trùng khớp");
                    return View("Index", viewModel);
                }
            }
            else
            {
                return View("Index", viewModel);
            }
        }
    }
}