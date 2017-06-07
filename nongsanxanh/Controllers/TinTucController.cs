using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using Bussiness.ViewModel;
using NSX_Common;
using PagedList.Mvc;
using PagedList;


namespace nongsanxanh.Controllers
{
    public class TinTucController : Controller
    {
        private readonly INewsService _iNewsService;
        private readonly INewsGroupService _iNewsGroupService;

        public TinTucController(INewsService iNewsService,INewsGroupService iNewsGroupService)
        {
            _iNewsService = iNewsService;
            _iNewsGroupService = iNewsGroupService;
        }
        // GET: TinTuc
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var data = _iNewsService.GetAllNews().Where(m=>m.Category.Contains(CategoryNew.Outstanding.ToString())).ToList();
            List<NewsViewModel> lstNews =new List<NewsViewModel>();
            foreach (var item in data)
            {
                NewsViewModel news =new NewsViewModel();
                news.Title = item.Title;
                news.Image = item.Image;
                news.Description = item.Description;
                news.NameGroup = Extentions.ToUnsignLinkString(item.NameGroup);
                lstNews.Add(news);
            }
            return View(lstNews.ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult Group(string catid, int? page)
        {
            var lstGroup = new List<NewsGroupViewModel>();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            lstGroup = _iNewsGroupService.GetAllNewsGroup().Where(o => o.Url == catid).ToList();
            List<NewsViewModel> lstNews = new List<NewsViewModel>();
            foreach (var item in lstGroup)
            {
                var rs = _iNewsService.GetAllNews();
                lstNews = rs.Where(m => m.GroupId == item.Id).ToList();
                foreach (var i in lstNews)
                {
                    i.NameGroup = Extentions.ToUnsignLinkString(i.NameGroup);
                }
                //if (lstNews.Count == 0)
                //{
                //    NewsViewModel news = new NewsViewModel();
                //    news.NameGroup = item.NameGroup;
                //    lstNews.Add(news);
                //}
            }
            return View(lstNews.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Details(string catid, string newsid)
        {
            var data = _iNewsService.GetNewsById(Guid.Parse(newsid));
            return View(data);
        }
    }
}