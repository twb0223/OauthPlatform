using System;
using Cache.Redis;
using Common;
using Entity;
using System.Configuration;

namespace Service
{
    public class OauthService
    {
        private readonly string tokenExpiry = ConfigurationManager.AppSettings["TokenExpiry"].ToString();
        private readonly string codeExpiry = ConfigurationManager.AppSettings["CodeExpiry"].ToString();
        public bool CreateApp(MircoApp model)
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

        public bool UpdateApp(MircoApp model)
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


            return true;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="AppID"></param>
        /// <returns></returns>
        public string GetToken(string AppID)
        {
            //找到该appid token
            var token = RedisManager.GetStringKey(AppID).ToString();
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
            int.TryParse(arr[3], out min);
            TimeSpan ts = new TimeSpan(day,hour,min,ms);//默认是一周过期时间

            RedisManager.SetStringKey(AppID, token, ts);
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
            var code = RedisManager.GetStringKey(AppID + "_" + UserID);
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
            int.TryParse(arr[3], out min);
            TimeSpan ts = new TimeSpan(day, hour, min, ms);//默认是一周过期时间

            RedisManager.SetStringKey(AppID + "_" + UserID, code,ts);
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

            return "";
        }
        /// <summary>
        /// 检查Token和Openid
        /// </summary>
        /// <param name="token"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public bool CheckTokenAndOpenID(string token, string OpenId)
        {
            bool flag = true;
            var cachetoken = RedisManager.GetStringKey(OpenId).ToString();
            if (cachetoken != token)
            {
                flag = false;
            }
            return flag;
        }
    }
}
