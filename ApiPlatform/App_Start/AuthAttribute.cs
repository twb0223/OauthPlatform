using Entity;
using Service;
using System;
using System.Web.Mvc;

namespace ApiPlatform.App_Start
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //从请求head 中获取token，openid
            //var head = ((HttpRequestWrapper)((HttpContextWrapper)filterContext.HttpContext).Request).Headers;
            //var token = head["token"];
            //var openid = head["openid"];

            //访问具体webapi时进行拦截
            var token = filterContext.Controller.ValueProvider.GetValue("token").AttemptedValue;
            var openid = filterContext.Controller.ValueProvider.GetValue("openid").AttemptedValue;

            OauthService os = new OauthService();
            //如果token,openid 在缓存中有，则返回正常
            if (os.CheckTokenAndOpenID(token, openid))
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                JsonResult result = new JsonResult
                {
                    Data = new { StausCode = StausCode.TokenIsOutTime, StatusMsg = StausCode.TokenIsOutTimeMsg }
                };
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = result;
            }
        }
    }
}