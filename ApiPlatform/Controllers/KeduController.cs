using ApiPlatform.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiPlatform.Controllers
{
    /// <summary>
    /// 拦截器所有调用该action的请求 都会拦截验证
    /// </summary>
    [Auth]
    public class KeduController : Controller
    {
        // GET: Kedu
        public JsonResult Index()
        {
            var person = new
            {

            };

            return Json(person, JsonRequestBehavior.AllowGet);
        }
    }
}