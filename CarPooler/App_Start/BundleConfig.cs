using System.Web;
using System.Web.Optimization;

namespace CarPooler
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/StyleSheets/bootstrap.css",
                      "~/Content/StyleSheets/site.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.min.js",
                      "~/Scripts/angular-route.min.js",
                      "~/Scripts/scrollMonitor-master/scrollMonitor",
                      "~/Scripts/ui-bootstrap.min.js"
                ));
     
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/App/Scripts/app.js",
                      "~/App/Scripts/Services/LoginFactory.js",
                      "~/App/Scripts/Services/RegisterFactory.js",
                      "~/App/Scripts/Services/IndexFactory.js",
                      "~/App/Scripts/Services/UserTripsFactory.js",
                      "~/App/Scripts/Services/AllTripsFactory.js",
                      "~/App/Scripts/Services/UserProfileFactory.js",
                      "~/App/Scripts/Services/AddUserProfileFactory.js",          
                      "~/App/Scripts/Controllers/NavBarController.js",
                      "~/App/Scripts/Controllers/IndexController.js",
                      "~/App/Scripts/Controllers/UserTripsController.js",
                      "~/App/Scripts/Controllers/AllTripsController.js",
                      "~/App/Scripts/Controllers/UserProfileController.js",
                      "~/App/Scripts/Controllers/AddUserProfileController.js",  
                    "~/App/Scripts/Controllers/SingleTripController.js",
                      "~/App/Scripts/Services/SingleTripFactory.js"
                     
                ));

#if DEBUG
            BundleTable.EnableOptimizations=false;
#else
            BundleTable.EnableOptimizations=true;
#endif

        }
    }
}
