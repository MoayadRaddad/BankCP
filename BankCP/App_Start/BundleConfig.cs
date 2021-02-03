using System.Web;
using System.Web.Optimization;

namespace BankCP
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                   "~/Scripts/jquery-{version}.js",
                   "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
               "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
               "~/Scripts/sweetalert.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Site.css",
                "~/Content/main.css",
               "~/Content/bootstrap.css",
               "~/Content/sweetalert.css"));

            bundles.Add(new StyleBundle("~/Content/rtl/css").Include(
                "~/Content/SiteAr.css",
                "~/Content/mainAr.css",
                "~/Content/bootstrap.rtl.css",
               "~/Content/sweetalert.css"));

            bundles.Add(new StyleBundle("~/Content/css-main").Include(
                "~/Content/util.css",
                "~/Content/animate.css",
                "~/Content/select2.min.css",
                "~/Content/perfect-scrollbar.css"));

            bundles.Add(new StyleBundle("~/Content/drop-down-list").Include(
                "~/Content/jquery.dropdown.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/drop-down-list").Include(
               "~/Scripts/jquery.dropdown.min.js",
               "~/Scripts/mock.js"));
        }
    }
}