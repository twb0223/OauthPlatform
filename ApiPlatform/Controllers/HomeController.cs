using Service;
using System.Web.Mvc;
using Entity;
using System;

namespace ApiPlatform.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            OauthService os = new OauthService();
            MircoApp m = new MircoApp();
            m.AppName = "wwwww";
            m.Logo = "http://static.oschina.net/uploads/user/457/914061_100.jpg?t=1444110975000";
            m.IsOpen = 1;
            m.AppUrl = "http://static.oschina.net/";
            m.Introduction = "666666";
           // m.CreateUserId = Guid.NewGuid();
            //m.PlatformUserID = Guid.NewGuid();
            m.ID = Guid.Parse("2799582C-795B-4BC4-8563-CB7FCE7912DD");

            os.UpdateApp(m);

            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
