using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BooksInfoWeb_KP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                                //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                                //defaults: new { controller = "Books", action = "ObjectsInfo", id = UrlParameter.Optional }
                                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }

            );

            routes.MapRoute(
                name: null,
                url: "Підручники",
                defaults: new
                {
                    controller = "Books",
                    action = "InfoWithPaging",
                    pageKey = "..",
                    pageNumber = 0
                }
            );

            routes.MapRoute(
                name: null,
                url: "Підручники_{pageKey}",
                defaults: new
                {
                    controller = "Books",
                    action = "InfoWithPaging",
                    pageNumber = 0
                },
                constraints: new { pageKey = @"[А-ЯЄ]" }
            );

            routes.MapRoute(
                name: null,
                url: "Підручники_{pageNumber}",
                defaults: new
                {
                    controller = "Books",
                    action = "InfoWithPaging",
                    pageKey = ".."
                },
                constraints: new { pageNumber = @"\d+" }
            );

            routes.MapRoute(
                name: null,
                url: "Підручники_{pageKey}_{pageNumber}",
                defaults: new
                {
                    controller = "Books",
                    action = "InfoWithPaging"
                },
                constraints: new { pageKey = @"[А-ЯЄ]", pageNumber = @"\d+" }
            );

            routes.MapRoute(
                name: "ASyncForm",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Forms", action = "Selection", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AJAXForm",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Forms", action = "BrowseByLetters", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CRUDForm",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "BooksCrud", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
