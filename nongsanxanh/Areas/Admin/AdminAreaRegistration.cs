using System.Web.Http.Routing.Constraints;
using System.Web.Mvc;

namespace nongsanxanh.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
            name: "Comment",
            url: "Admin/Comment/{action}/{commentid}",
            defaults: new { controller = "Comment", action = "Index", commentid = new GuidRouteConstraint() }
            );
            context.MapRoute(
             name: "Menu",
             url: "Admin/Menu/{action}/{menuid}",
             defaults: new { controller = "Menu", action = "Index", menuid = new GuidRouteConstraint() }
             );
            context.MapRoute(
              name: "Order",
              url: "Admin/Order/{action}/{orderid}",
              defaults: new { controller = "Order", action = "Index", orderid = new GuidRouteConstraint() }
              );
            context.MapRoute(
               name: "ProductAttribute",
               url: "Admin/ProductAttribute/{action}/{proAttrId}",
               defaults: new { controller = "ProductAttribute", action = "Index", proAttrId = new GuidRouteConstraint() }
               );
            context.MapRoute(
               name: "Product",
               url: "Admin/Product/{action}/{proid}",
               defaults: new { controller = "Product", action = "Index", proid = new GuidRouteConstraint() }
               );
            context.MapRoute(
                name: "ProductGroup",
                url: "Admin/ProductGroup/{action}/{progroupid}",
                defaults: new {controller = "ProductGroup", action = "Index", progroupid = new GuidRouteConstraint()}
                );
           context.MapRoute(
            name: "News",
            url: "Admin/News/{action}/{newsid}",
            defaults: new { controller = "News", action = "Index", newsid = new GuidRouteConstraint() }
          );
            context.MapRoute(
             name: "NewsGroup",
             url: "Admin/NewsGroup/{action}/{newsgroupid}",
             defaults: new { controller = "NewsGroup", action = "Index", newsgroupid = new GuidRouteConstraint() }
           );
            context.MapRoute(
             name: "AccountGroup",
             url: "Admin/AccountGroup/{action}/{groupid}",
             defaults: new { controller = "AccountGroup", action = "Index", groupid = new GuidRouteConstraint() }
           );
            context.MapRoute(
             name: "Decentralization",
             url: "Admin/Decentralization/{action}/{groupid}",
             defaults: new { controller = "Decentralization", action = "Index", groupid = new GuidRouteConstraint() }
           );
            context.MapRoute(
            name: "RoleMng",
            url: "Admin/RoleMng/{action}/{roleid}",
            defaults: new { controller = "RoleMng", action = "Index", roleid = new GuidRouteConstraint() }
          );
            context.MapRoute(
              name: "LoginAdmin",
              url: "Admin/LoginAdmin/{action}/{accountid}",
              defaults: new { controller = "LoginAdmin", action = "Index", accountid = new GuidRouteConstraint() }
            );
            context.MapRoute(
               name: "AccountMng",
               url: "Admin/AccountMng/{action}/{accountid}",
               defaults: new { controller = "AccountMng", action = "Index", accountid = new GuidRouteConstraint() }
             );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}