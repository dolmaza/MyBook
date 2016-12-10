using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using StackExchange.Profiling.Mvc;
using StackExchange.Profiling.SqlFormatters;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyBook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Miniprofiler Settings //
            GlobalFilters.Filters.Add(new ProfilingActionFilter());
            MiniProfilerEF6.Initialize();
            MiniProfiler.Settings.SqlFormatter = new SqlServerFormatter();
            // To Layout body 
            //@StackExchange.Profiling.MiniProfiler.RenderIncludes()


            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}
