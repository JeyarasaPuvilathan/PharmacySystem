using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AdminLTE_Template1.App_Start;
using System.Configuration;
using System.Data.SqlClient;
using System;

namespace AdminLTE_Template1
{
    public class MvcApplication : HttpApplication
    {
        string con = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SqlDependency.Start(con);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            NotificationComponent NC = new NotificationComponent();
            //var currentTime = DateTime.Now;
            //HttpContext.Current.Session["LastUpdated"] = currentTime;
            NC.RegisterNotification(2);
        }


        protected void Application_End()
        {
            //here we will stop Sql Dependency
            SqlDependency.Stop(con);
        }
    }
}