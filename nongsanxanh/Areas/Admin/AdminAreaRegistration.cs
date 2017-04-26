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