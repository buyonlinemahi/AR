using System.Web;
using System.Web.Optimization;

namespace LMGEDIApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/alertify").Include(
                      "~/Scripts/alertify/alertify.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-1.11.0.js",
                       "~/Scripts/JQuery/jquery-ui-1.11.4.js",
                        "~/Scripts/JQuery/modernizr-2.6.2-respond-1.1.0.min.js",
                        "~/Scripts/knockout/knockout*",
                        "~/Scripts/JQuery/moment.js",
                        "~/Scripts/knockout/Custom/knockout.custom-binding-handlers.js"
                         ));
           

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/jquery.form.js",
                      "~/Scripts/bootstrap/bootstrap.js",
                      "~/Scripts/bootstrap/bootstrap-datetimepicker.js",
                      "~/Scripts/bootstrap/jqBootstrapValidation.js"
                      ));

            bundles.Add(new ScriptBundle("~/js/main").Include(
                  "~/Scripts/main.js"
                    ));

            //css
            bundles.Add(new StyleBundle("~/Css/Content/css").Include(
                "~/Content/css/main.css",
                "~/Content/css/bootstrap-datetimepicker.min.css",
                "~/Content/bootstrap/jquery-ui-1.11.4.css"
                ));
            bundles.Add(new StyleBundle("~/Content/themes/alertify").Include(
                      "~/Content/alertify/alertify.default.css",
                      "~/Content/alertify/alertify.core.css"));
            bundles.Add(new StyleBundle("~/Css/bundles/bootstrap").Include("~/Content/css/bootstrap.min.css",
                                                                        "~/Content/css/bootstrap-theme.min.css"
                                                                        ));
            //Here is Basic JavaScript
            bundles.Add(new ScriptBundle("~/bundles/LMGEDIScripts").Include(

                "~/Scripts/AjaxUtil.js"

               ));
        }
    }
}