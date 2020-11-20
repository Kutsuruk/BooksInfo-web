using BooksInfo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

namespace BooksInfoWeb_KP
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            StaticDataContext.Directory = HostingEnvironment.MapPath("~/files");

            StaticDataContext.Load();
            if (StaticDataContext.BookGivings.Count == 0)
            {
				StaticDataContext.CreateTestingData();
                StaticDataContext.Save();
            }
        }
    }
}
