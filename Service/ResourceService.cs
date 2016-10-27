using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
