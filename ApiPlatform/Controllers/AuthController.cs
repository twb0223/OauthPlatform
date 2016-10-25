
//1.用户登陆开放平台
//2.注册一个应用，平台返回appid+appSecret，作为接入开放平台的 标识，（应用信息存储到sql 数据库）
//3.应用开启时，先将appid+appSecret，发送到开放平台，开放平台 验证是否存在该应用，存在则返回Token，同时token缓存在redis中。
//4.应用收到token后，以后的每次访问webapi时都带着这个token，
//5.嘟嘟用户登陆微应用后，传递应用的 appid+appSecret
//redis 需要缓存的数据有，<Appid Token>,<Openid Token>,<Code,OpenID>

using Entity;
using Service;
using System;
using System.Web.Mvc;

namespace ApiPlatform.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        public JsonResult CreateMicroApp(MircoApp model)
        {
            JsonResult result = new JsonResult();
            //写入Sql数据库
            OauthService os = new OauthService();
            os.CreateApp(model);
            //构造返回对象
            result.Data = new { StausCode = StausCode.Ok, StatusMsg = StausCode.OkMsg, AppModel = model };
            return result;
        }

        [HttpPost]
        public JsonResult UpdteMicroApp(MircoApp model)
        {
            JsonResult result = new JsonResult();
            //写入Sql数据库
            OauthService os = new OauthService();
            os.UpdateApp(model);
            //构造返回对象
            result.Data = new { StausCode = StausCode.Ok, StatusMsg = StausCode.OkMsg, AppModel = model };
            return result;
        }


        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetToken(OAuthModel model)
        {
            JsonResult result = new JsonResult();
            OauthService os = new OauthService();
            string token = "";
            string userCode = "";
            string code = StausCode.Ok;
            string msg = StausCode.OkMsg;
            //1.验证,检查数据库中是否有这个Appid 和appsecret的应用
            if (os.CheckApp(model.AppId, model.AppSecret))
            {
                //2.如果数据库中有这个应用，则接着在缓存中找token,如果缓存中有token，则直接返回Token，否则先生成token
                token = os.GetToken(model.AppId);
                if (String.IsNullOrEmpty(token))
                {
                    token = os.CreateToken(model.AppId);//创建token
                }
                ////判断Usercode是否存在，不存在说明过期了。则重新生成。
                //userCode = os.GetUserCode(model.AppId, model.UserID);
                //if (String.IsNullOrEmpty(userCode))
                //{
                //    userCode = os.CreateUserCode(model.AppId, model.UserID);//创建UserCode 
                //}
            }
            else
            {
                //3.如果不存在则直接返回appid不合法
                code = StausCode.AppIDSecretErr; msg = StausCode.AppIDSecretErrMsg;
            }
            result.Data = new { StausCode = code, StatusMsg = msg, Token = token, UserCode = userCode };
            return result;
        }

        /// <summary>
        /// 获取Code
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult GetCode(string AppID,string UserID)
        {
            JsonResult result = new JsonResult();
            OauthService os = new OauthService();
            string token = "";
            string userCode = "";
            string code = StausCode.Ok;
            string msg = StausCode.OkMsg;

            userCode = os.GetUserCode(AppID, UserID);
            if (string.IsNullOrEmpty(userCode))
            {
                userCode = os.CreateUserCode(AppID, UserID);//创建UserCode 
            }
         
            result.Data = new { StausCode = code, StatusMsg = msg, Token = token, UserCode = userCode };
            return result;
        }

        /// <summary>
        /// 获取OpenID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOpenID(RequestOpenIDModel model)
        {
            JsonResult result = new JsonResult();
            //根据回传的Token+UserCode 获取用户Openid。
            OauthService os = new OauthService();
            var openid = os.GetOpenID(model.Token, model.UserCode);
            result.Data = new { StausCode = StausCode.Ok, StatusMsg = StausCode.OkMsg, OpenId = openid };
            return result;
        }
    }
}

