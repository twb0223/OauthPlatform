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
        public bool CreateApp(OpenPlatformMicroApplication model)
        {
            //创建Appid，appsecret
            model.AppID = Tools.CreateAppID();
            model.AppSecret = Tools.CreateAppSecret();
            model.CreateTime = DateTime.Now.ToShortDateString();
            // model.PlatformUserID = Guid.NewGuid();
            string strsql = @"INSERT INTO MircoApp(AppName,Logo,Introduction,AppUrl,AppID,AppSecret,IsOpen,CreateUserID,CreateTime,PlatformUserID)VALUES(
    @AppName,@Logo,@Introduction,@AppUrl,@AppID,@AppSecret,@IsOpen,@CreateUserID,@CreateTime,@PlatformUserID)";
            try
            {
                int result = DapperHelper.Add(strsql, model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateApp(OpenPlatformMicroApplication model)
        {
            string strsql = @"Update MircoApp Set AppName=@AppName,Logo=@Logo,AppUrl=@AppUrl,Introduction=@Introduction,IsOpen=@IsOpen where ID=@ID";
            try
            {
                int result = DapperHelper.Update(strsql, model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
            var reqresult = Tools.PostWebRequest("http://10.0.5.43:9000/api/services/app/openPlatformMicro/ValidateOpenPlatformMicroApplication", parma, Encoding.UTF8);
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
            var reqresult = Tools.PostWebRequest("http://10.0.5.43:9000/api/services/app/openUserHeadsService/GetOpenId", parma, Encoding.UTF8);
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
        public string GetOpenID(string token, string code)
        {
            ///读取缓存得到Openid
            var OpenID = ir.GetStringKey(code);
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
            if (string.IsNullOrEmpty(token)||string.IsNullOrEmpty(OpenId))
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
    }
}
