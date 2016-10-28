using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using Newtonsoft.Json;
using Common;
using Newtonsoft.Json.Linq;

namespace Service
{
    public class ResourceService : IResourceService
    {
        public List<OpenPlatformMicroApplication> GetAllByCondition(GetOpenPlatformMicroApplicationByConditionInput input)
        {
            var parma = JsonConvert.SerializeObject(input);
            var reqresult = Tools.PostWebRequest("http://localhost:6234/api/services/app/openPlatformMicro/GetOpenPlatfromMicroApplicationByCondition", parma, Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var list = jo["result"]["openPlatformMicroApplications"].ToString();
            return JsonConvert.DeserializeObject<List<OpenPlatformMicroApplication>>(list);
        }

        /// <summary>
        /// 创建App
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpenPlatformMicroApplication CreateApp(OpenPlatformMicroApplication model)
        {
            //todo 调用添加地址 执行添加
            var parma = JsonConvert.SerializeObject(model);
            var reqresult = Tools.PostWebRequest(Tools.GetPlatformUrl() + "/api/services/app/openPlatformMicro/CreateNewApp", parma, Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var list = jo["result"].ToString();
            return JsonConvert.DeserializeObject<OpenPlatformMicroApplication>(list);
        }

        /// <summary>
        /// 更新app
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpenPlatformMicroApplication UpdateApp(UpdateAppInput model)
        {
            var parma = JsonConvert.SerializeObject(model);
            var reqresult = Tools.PostWebRequest(Tools.GetPlatformUrl() + "/api/services/app/openPlatformMicro/UpdateNewApp", parma, Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var list = jo["result"].ToString();
            return JsonConvert.DeserializeObject<OpenPlatformMicroApplication>(list);
        }

        public bool DeleteApp(Guid Id)
        {
            var reqresult = Tools.PostWebRequest(Tools.GetPlatformUrl() + "/api/services/app/openPlatformMicro/DeleteApp?Id=" + Id, "", Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var result = (bool)jo["success"];
            return result;
        }

        public GetAppInfoDto GetAppInfo(string AppId)
        {
            var reqresult = Tools.PostWebRequest(Tools.GetPlatformUrl() + "/api/services/app/openPlatformMicro/GetAppInfo?AppId=" + AppId, "", Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var result = jo["result"].ToString();
            return JsonConvert.DeserializeObject<GetAppInfoDto>(result);
        }

        public OpenPlatformUsers Register(OpenPlatformUsersRegisterDto model)
        {
            var parma = JsonConvert.SerializeObject(model);
            var reqresult = Tools.PostWebRequest(Tools.GetPlatformUrl() + "/api/services/app/openPlatformUsersService/Resister", parma, Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var result = jo["result"].ToString();
            return JsonConvert.DeserializeObject<OpenPlatformUsers>(result);
        }

        public bool Login(OpenPlatformUserLoginDto model)
        {
            var parma = JsonConvert.SerializeObject(model);
            var reqresult = Tools.PostWebRequest(Tools.GetPlatformUrl() + "/api/services/app/openPlatformUsersService/Login", parma, Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var result = (bool)jo["result"];
            return result;
        }

        public bool ChangePassword(OpenPlatformUserChangePasswordDto model)
        {
            var parma = JsonConvert.SerializeObject(model);
            var reqresult = Tools.PostWebRequest(Tools.GetPlatformUrl() + "/api/services/app/openPlatformUsersService/ChangePassword", parma, Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var result = (bool)jo["result"];
            return result;
        }

        public bool ExamineApp(ExamineAppInput model)
        {
            var parma = JsonConvert.SerializeObject(model);
            var reqresult = Tools.PostWebRequest(Tools.GetPlatformUrl() + "/api/services/app/openPlatformMicro/ExamineApp", parma, Encoding.UTF8);
            JObject jo = (JObject)JsonConvert.DeserializeObject(reqresult);
            var result = (bool)jo["result"];
            return result;
        }
    }
}
