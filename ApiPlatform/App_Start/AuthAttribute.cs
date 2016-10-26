using Autofac.Integration.WebApi;
using Cache.Redis;
using Entity;
using Service;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ApiPlatform.App_Start
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext oHttpActionContext)
        {
            HttpContextBase oHttpContextBase = (HttpContextBase)oHttpActionContext.Request.Properties["MS_HttpContext"]; //获取传统context     
            string sToken = oHttpContextBase.Request.Form["token"]; //先从post里面查

            if (string.IsNullOrEmpty(sToken))
                sToken = oHttpContextBase.Request.QueryString["token"]; //再从get里面查

            string sopenid = oHttpContextBase.Request.Form["openid"]; //先从post里面查

            if (string.IsNullOrEmpty(sopenid))
                sopenid = oHttpContextBase.Request.QueryString["openid"]; //再从get里面查
            IRedisManager ir = new RedisManager();
            IOauthService oa = new OauthService(ir);
            if (!oa.CheckTokenAndOpenID(sToken, sopenid))
            {
                HttpResponseMessage oHttpResponseMessage = new HttpResponseMessage();
                oHttpResponseMessage.Content = new StringContent("{‘StatusCode‘:'" + StausCode.TokenOrOpenIDError + "',‘StatusMsg‘:‘" + StausCode.TokenOrOpenIDErrorMsg + "‘}", Encoding.UTF8, "application/json");

                throw new HttpResponseException(oHttpResponseMessage);
            }
        }
    }
}