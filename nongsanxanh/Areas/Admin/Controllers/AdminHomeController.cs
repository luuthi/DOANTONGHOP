using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using nongsanxanh.Areas.Admin.Models;

namespace nongsanxanh.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
      
        public ActionResult Index()
        {
            return View();
        }
    }
}