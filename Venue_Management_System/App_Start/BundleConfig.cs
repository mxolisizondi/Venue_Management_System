using System.Web;
using System.Web.Optimization;

namespace Venue_Management_System
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

 
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Admin_Assets/assets/js/vendor.min.js",
                "~/Admin_Assets/assets/js/app.min.js",
                "~/Admin_Assets/assets/js/vendor/apexcharts.min.js",
                "~/Admin_Assets/assets/js/vendor/jquery-jvectormap-1.2.2.min.js",
                "~/Admin_Assets/assets/js/vendor/jquery-jvectormap-world-mill-en.js",
                "~/Admin_Assets/assets/js/pages/demo.dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Admin_Assets/assets/css").Include(
                      "~/Admin_Assets/assets/css/vendor/jquery-jvectormap-1.2.2.css",
                      "~/Admin_Assets/assets/css/icons.min.css",
                      "~/Admin_Assets/assets/css/app.min.css"));
            //I removed darkmode css
        }
    }
}
