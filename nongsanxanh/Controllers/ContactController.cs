using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;

namespace nongsanxanh.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICompanyInfoService _companyInfoService;

        public ContactController(ICompanyInfoService iCompanyInfoService)
        {
            _companyInfoService = iCompanyInfoService;
        }
        // GET: Contact
        public ActionResult Index()
        {
            var data = _companyInfoService.GetCompanyInfo();
            return View(data);
        }
    }
}