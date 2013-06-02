using System.Web;
using System.Web.Optimization;

namespace Sunshine
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.UseCdn = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery", "http://lib.sinaapp.com/js/jquery/1.7/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable", "http://ajax.aspnetcdn.com/ajax/jquery.dataTables/1.8.2/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui", "http://code.jquery.com/ui/1.8.20/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
        //http://ajax.aspnetcdn.com/ajax/jquery.dataTables/1.8.2/jquery.dataTables.min.js
        //http://ajax.aspnetcdn.com/ajax/jquery.dataTables/1.8.2/css/jquery.dataTables.css
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Styles/css").Include("~/Styles/site.css",
                "~/Styles/base_old.css"));

            bundles.Add(new StyleBundle("~/Styles/site").Include("~/Styles/site.css"));

            bundles.Add(new StyleBundle("~/Styles/base").Include("~/Styles/base.css"));

            bundles.Add(new StyleBundle("~/Styles/search/css").Include("~/Styles/search/search.css"));

            bundles.Add(new StyleBundle("~/Styles/themes/base/css").Include(
                        "~/Styles/themes/base/jquery.ui.core.css",
                        "~/Styles/themes/base/jquery.ui.resizable.css",
                        "~/Styles/themes/base/jquery.ui.selectable.css",
                        "~/Styles/themes/base/jquery.ui.accordion.css",
                        "~/Styles/themes/base/jquery.ui.autocomplete.css",
                        "~/Styles/themes/base/jquery.ui.button.css",
                        "~/Styles/themes/base/jquery.ui.dialog.css",
                        "~/Styles/themes/base/jquery.ui.slider.css",
                        "~/Styles/themes/base/jquery.ui.tabs.css",
                        "~/Styles/themes/base/jquery.ui.datepicker.css",
                        "~/Styles/themes/base/jquery.ui.progressbar.css",
                        "~/Styles/themes/base/jquery.ui.theme.css"));
        }
    }
}