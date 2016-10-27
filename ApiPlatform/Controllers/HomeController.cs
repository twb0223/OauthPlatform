using Service;
using System.Web.Mvc;
using Entity;
using System;
using Common;
using System.Text;

namespace ApiPlatform.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            // MircoApp m = new MircoApp();
            // m.AppName = "wwwww";
            // m.Logo = "http://static.oschina.net/uploads/user/457/914061_100.jpg?t=1444110975000";
            // m.IsOpen = 1;
            // m.AppUrl = "http://static.oschina.net/";
            // m.Introduction = "666666";
            //// m.CreateUserId = Guid.NewGuid();
            // //m.PlatformUserID = Guid.NewGuid();
            // m.ID = Guid.Parse("2799582C-795B-4BC4-8563-CB7FCE7912DD");

            //string str = "{'openPlatformMicroApplications': [{'id': '0257662E-C746-4FB4-94F7-0E1DBD6CCFBF'}]}";

            //string str = "{'isValidate': true,'openPlatformMicroApplication': {'id': '0257662E-C746-4FB4-94F7-0E1DBD6CCFBF'} }";


            //var s = Tools.PostWebRequest("http://10.0.5.43:9000//api/services/app/openPlatformMicro/ValidateOpenPlatformMicroApplication", str, Encoding.UTF8);
            // os.CheckApp("123123123123", "dfdfdfdf");
            // os.CreateUserCode("0257662E-C746-4FB4-94F7-0E1DBD6CCFBF", "0257662E-C746-4FB4-94F7-0E1DBD6CCFBB");
            //var input = new GetOpenPlatformMicroApplicationByConditionInput
            //{
            //    CompanyID = Guid.Parse("1234567E-C746-4FB4-94F7-0E1DBD6FFFFF"),
            //    ShopID = null,
            //    DepartmentID = null,
            //    UserID = null

            //};
            //var s = irs.GetAllByCondition(input);

            return View();
        }
    }
}
