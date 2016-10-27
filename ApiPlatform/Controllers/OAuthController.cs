using System;
using System.Web.Http;
using Service;
using Entity;
using ApiPlatform.App_Start;

namespace ApiPlatform.Controllers
{

    public class OAuthController : ApiController
    {
        IOauthService oa;
        public OAuthController(IOauthService oa)
        {
            this.oa = oa;
        }
        [Route("api/Oauth/Get")]
        public string Get()
        {

            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;

            OpenPlatformMicroApplication m = new OpenPlatformMicroApplication
            {
                Name = "123123",
                AppUrl = "asdasxcsds",
                BackUrl = "dsasdasd",
                CreatorId = Guid.Parse("0257662E-C746-4FB4-94F7-0E1DBD6CCFBF"),
                Introduction = "asdasdasd",
                logo = "asdasd",
                ExamineUserId = Guid.NewGuid(),
                ExamineTime = DateTime.Now
            };

            OpenPlatformMicroApplication entity = null;
            try
            {
                entity = oa.CreateApp(m);
            }
            catch (Exception ex)
            {
                code = StausCode.DataCreteException;
                msg = StausCode.DataCreateExceptionMsg;
            }
            return "sss";
        }

        [Route("api/Oauth/CreateMicroApp")]
        [HttpPost]
        public OpenPlatformMicroApplicationDto CreateMicroApp(RequestMicroAppDto model)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            OpenPlatformMicroApplication m = new OpenPlatformMicroApplication
            {
                Name = model.AppName,
                AppUrl = model.AppUrl,
                BackUrl = model.BackUrl,
                CreatorId = model.CreateUserId,
                Introduction = model.Introduction,
                logo = model.Logo
            };
            OpenPlatformMicroApplication entity = null;
            try
            {
                entity = oa.CreateApp(m);

            }
            catch (Exception)
            {
                code = StausCode.DataCreteException;
                msg = StausCode.DataCreateExceptionMsg;
            }
            OpenPlatformMicroApplicationDto result = new OpenPlatformMicroApplicationDto
            {
                StatusCode = code,
                StatusMsg = msg,
                OpenPlatformMicroApplication = entity
            };
            return result;
        }
        [Route("api/Oauth/UpdateMicroApp")]
        [HttpPost]
        public OpenPlatformMicroApplicationDto UpdteMicroApp(OpenPlatformMicroApplication model)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            OpenPlatformMicroApplication entity = null;
            try
            {
                entity = oa.UpdateApp(model);
            }
            catch (Exception)
            {
                code = StausCode.DataCreteException;
                msg = StausCode.DataCreateExceptionMsg;
            }
            OpenPlatformMicroApplicationDto result = new OpenPlatformMicroApplicationDto
            {
                StatusCode = code,
                StatusMsg = msg,
                OpenPlatformMicroApplication = entity
            };
            return result;
        }
        [Route("api/Oauth/GetToken")]
        [HttpPost]
        public ResponeTokenDto GetToken(RequestTokenDto model)
        {
            var token = "";
            double ExpiresIn = 0;
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            //1.验证,检查数据库中是否有这个Appid 和appsecret的应用
            try
            {
                if (oa.CheckApp(model.AppId, model.AppSecret))
                {
                    //2.如果数据库中有这个应用，则接着在缓存中找token,如果缓存中有token，则直接返回Token，否则先生成token
                    token = oa.GetToken(model.AppId);
                    if (string.IsNullOrEmpty(token))
                    {
                        token = oa.CreateToken(model.AppId);//创建token
                    }
                }
                else
                {
                    code = StausCode.AppIDSecretErr; msg = StausCode.AppIDSecretErrMsg;
                }
                var ts = oa.KeyTimeToLive(model.AppId);
                if (ts != null)
                {
                    ExpiresIn = ((TimeSpan)ts).TotalSeconds;
                }

            }
            catch (Exception)
            {
                code = StausCode.Exception;
                msg = StausCode.ExceptionMsg;
            }
            ResponeTokenDto result = new ResponeTokenDto
            {
                StatusCode = code,
                StatusMsg = msg,
                Token = token,
                ExpiresIn = ExpiresIn
            };
            return result;
        }
        /// <summary>
        /// 获取OpenID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("api/Oauth/GetOpenID")]
        [HttpPost]
        public ResponeOpenIDDto GetOpenID(RequestOpenIDDto model)
        {
            var openid = oa.GetOpenID(model.AppId, model.Token, model.Code);
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;

            if (string.IsNullOrEmpty(openid))
            {
                code = StausCode.TokenOrOpenIDError;
                msg = StausCode.TokenOrOpenIDErrorMsg;
            }
            ResponeOpenIDDto result = new ResponeOpenIDDto
            {
                StatusCode = code,
                StatusMsg = msg,
                OpenID = openid
            };
            return result;
        }
    }
}
