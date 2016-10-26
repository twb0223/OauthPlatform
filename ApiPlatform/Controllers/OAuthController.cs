using System;
using System.Web.Http;
using Service;
using Entity;

namespace ApiPlatform.Controllers
{

    public class OAuthController : ApiController
    {
        IOauthService oa;
        public OAuthController(IOauthService oa)
        {
            this.oa = oa;
        }

        [Route("api/Oauth/CreateMicroApp")]
        [HttpPost]
        public OpenPlatformMicroApplicationDto CreateMicroApp(OpenPlatformMicroApplication model)
        {
            oa.CreateApp(model);
            OpenPlatformMicroApplicationDto result = new OpenPlatformMicroApplicationDto
            {
                StatusCode = StausCode.Ok,
                StatusMsg = StausCode.OkMsg,
                OpenPlatformMicroApplication = model
            };
            return result;
        }

        [Route("api/Oauth/UpdateMicroApp")]
        [HttpPost]
        public OpenPlatformMicroApplicationDto UpdteMicroApp(OpenPlatformMicroApplication model)
        {
            oa.UpdateApp(model);
            OpenPlatformMicroApplicationDto result = new OpenPlatformMicroApplicationDto
            {
                StatusCode = StausCode.Ok,
                StatusMsg = StausCode.OkMsg,
                OpenPlatformMicroApplication = model
            };
            return result;
        }

        [Route("api/Oauth/GetToken")]
        [HttpPost]
        public ResponeTokenDto GetToken(RequestTokenDto model)
        {
            string token = "";
            string code = StausCode.Ok;
            string msg = StausCode.OkMsg;
            //1.验证,检查数据库中是否有这个Appid 和appsecret的应用
            if (oa.CheckApp(model.AppId, model.AppSecret))
            {
                //2.如果数据库中有这个应用，则接着在缓存中找token,如果缓存中有token，则直接返回Token，否则先生成token
                token = oa.GetToken(model.AppId);
                if (String.IsNullOrEmpty(token))
                {
                    token = oa.CreateToken(model.AppId);//创建token
                }
            }
            else
            {
                code = StausCode.AppIDSecretErr; msg = StausCode.AppIDSecretErrMsg;
            }
            ResponeTokenDto result = new ResponeTokenDto
            {
                StatusCode = code,
                StatusMsg = msg,
                Token = token
            };
            return result;
        }

    }
}
