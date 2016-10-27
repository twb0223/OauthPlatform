using System;
using Cache.Redis;
using Common;
using Entity;
using System.Configuration;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Service
{
    public class OauthService : IOauthService
    {
        private readonly string tokenExpiry = ConfigurationManager.AppSettings["TokenExpiry"].ToString();
        private readonly string codeExpiry = ConfigurationManager.AppSettings["CodeExpiry"].ToString();

        IRedisManager ir;
        public OauthService(IRedisManager ir)
        {
            this.ir = ir;
        }
        /// <summary>
        /// 创建App
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpenPlatformMicroApplication CreateApp(OpenPlatformMicroApplication model)
        {
            //创建Appid，appsecret
            model.AppID = Tools.CreateAppID();
            model.AppSecret = Tools.CreateAppSecret();
            model.CreateTime = DateTime.Now;
            model.Id = Guid.NewGuid();
            model.IsExamine = 1;
            model.IsOpen = 0;

            //todo 调用添加地址 执行添加
            var parma = JsonConvert.SerializeObject(model);
            var reqresult = Tools.PostWebRequest("http://localhost:6234/api/services/app/openPlatformMicro/CreateNewApp", parma, Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var list = jo["result"]["openPlatformMicroApplications"].ToString();
            return JsonConvert.DeserializeObject<OpenPlatformMicroApplication>(list);
        }

        /// <summary>
        /// 更新app
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpenPlatformMicroApplication UpdateApp(OpenPlatformMicroApplication model)
        {
            var parma = JsonConvert.SerializeObject(model);
            var reqresult = Tools.PostWebRequest("http://localhost:6234/api/services/app/openPlatformMicro/UpdateNewApp", parma, Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var list = jo["result"]["openPlatformMicroApplications"].ToString();
            return JsonConvert.DeserializeObject<OpenPlatformMicroApplication>(list);
        }

        /// <summary>
        /// 检查app是否存在
        /// </summary>
        /// <param name="Appid"></param>
        /// <param name="AppSecret"></param>
        /// <returns></returns>
        public bool CheckApp(string Appid, string AppSecret)
        {
            //读取数据库来严重app是否存在
            //返回json
            // {
            //   "result": {
            //     "isValidate": true,
            //    "openPlatformMicroApplication": {
            //       id": "0257662e-c746-4fb4-94f7-0e1dbd6ccfbf"
            //    }
            //                },
            //  "targetUrl": null,
            //  "success": true,
            //  "error": null,
            //  "unAuthorizedRequest": false,
            //  "__abp": true
            //}
            string parma = "{'appId':'" + Appid + "','appSecret': '" + AppSecret + "'}";
            var reqresult = Tools.PostWebRequest(Tools.GetPlatformUrl() + "/api/services/app/openPlatformMicro/ValidateOpenPlatformMicroApplication", parma, Encoding.UTF8);



            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var result = bool.Parse(jo["result"]["isValidate"].ToString());
            return result;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="AppID"></param>
        /// <returns></returns>
        public string GetToken(string AppID)
        {
            //找到该appid token
            var token = ir.GetStringKey(AppID).ToString();
            return token;
        }

        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="AppID"></param>
        /// <returns></returns>
        public string CreateToken(string AppID)
        {
            var token = Tools.CreateToken();

            var arr = tokenExpiry.Split(',');
            int day = 0;
            int hour = 0;
            int min = 0;
            int ms = 0;
            int.TryParse(arr[0], out day);
            int.TryParse(arr[1], out hour);
            int.TryParse(arr[2], out min);
            int.TryParse(arr[3], out ms);
            TimeSpan ts = new TimeSpan(day, hour, min, ms);
            //存储Appid Token关系
            ir.SetStringKey(AppID, token, ts);
            return token;
        }
        /// <summary>
        /// 获取UserCode
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GetUserCode(string AppID, string UserID)
        {
            var code = ir.GetStringKey(AppID + "_" + UserID);
            return code;
        }
        /// <summary>
        /// 创建Code
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string CreateUserCode(string AppID, string UserID)
        {
            var code = Guid.NewGuid().ToString("N");

            var arr = codeExpiry.Split(',');
            int day = 0;
            int hour = 0;
            int min = 0;
            int ms = 0;
            int.TryParse(arr[0], out day);
            int.TryParse(arr[1], out hour);
            int.TryParse(arr[2], out min);
            int.TryParse(arr[3], out ms);
            TimeSpan ts = new TimeSpan(day, hour, min, ms);
            //存储code
            ir.SetStringKey(AppID + "_" + UserID, code, ts);
            //获取OpenID
            //返回json
            //{
            //"result": {
            //    "openId": "d196f877e0734e049870272df9b5c315"
            //},
            //"targetUrl": null,
            //"success": true,
            //"error": null,
            //"unAuthorizedRequest": false,
            //"__abp": true
            //}
            string parma = "{'userId': '" + UserID + "','openPlatformMicroApplicationId': '" + AppID + "'}";
            var reqresult = Tools.PostWebRequest(Tools.GetPlatformUrl() + "/api/services/app/openUserHeadsService/GetOpenId", parma, Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var OpenID = jo["result"]["openId"].ToString();

            //将Code与OpenID的对应关系写入缓存。
            ir.SetStringKey("Code_" + code, OpenID, ts);

            return code;
        }
        /// <summary>
        /// 获取openid
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetOpenID(string Appid, string token, string code)
        {
            ///读取缓存得到Openid
            //验证token是否过期
            var otoken = ir.GetStringKey(Appid).ToString();
            string OpenID = "";
            if (otoken == token)//没有过期
            {
                OpenID = ir.GetStringKey("Code_" + code);
            }
            return OpenID;
        }
        /// <summary>
        /// 检查Token和Openid
        /// </summary>
        /// <param name="token"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public bool CheckTokenAndOpenID(string token, string OpenId)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(OpenId))
            {
                return false;
            }
            bool flag = true;
            var cachetoken = ir.GetStringKey(OpenId).ToString();
            if (cachetoken != token)
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 获取指定key的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TimeSpan? KeyTimeToLive(string key)
        {
            return ir.KeyTimeToLive(key);
        }
    }
}
