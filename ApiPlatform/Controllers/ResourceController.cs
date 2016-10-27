using Entity;
using System.Web.Http;
using Service;
using ApiPlatform.App_Start;
using System;

namespace ApiPlatform.Controllers
{
    /// <summary>
    /// 资源api接口
    /// </summary>
  
    public class ResourceController : ApiController
    {
        IResourceService irs;
        public ResourceController(IResourceService _irs)
        {
            this.irs = _irs;
        }
        //[Route("api/Resource/Get")]
        //public GetOpenPlatformMicroApplicationByConditionOutput Get()
        //{
        //    var input = new GetOpenPlatformMicroApplicationByConditionInput
        //    {
        //        CompanyID = Guid.Parse("eeeeeeee-c746-4fb4-94f7-0e1dbd6ccfbf"),
        //        ShopID = null,
        //        DepartmentID = null,
        //        UserID = null

        //    };
        //    var code = StausCode.Ok;
        //    var msg = StausCode.OkMsg;
        //    var list = irs.GetAllByCondition(input);
        //    GetOpenPlatformMicroApplicationByConditionOutput result = new GetOpenPlatformMicroApplicationByConditionOutput
        //    {
        //        StatusCode = code,
        //        StatusMsg = msg,
        //        OpenPlatformMicroApplicationlist = list
        //    };
        //    return result;
        //}


        [HttpPost]
        [Route("api/Resource/GetAppListByCondition")]
        public GetOpenPlatformMicroApplicationByConditionOutput GetAllApp(GetOpenPlatformMicroApplicationByConditionInput model)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            var list = irs.GetAllByCondition(model);

            GetOpenPlatformMicroApplicationByConditionOutput result = new GetOpenPlatformMicroApplicationByConditionOutput
            {
                StatusCode = code,
                StatusMsg = msg,
                OpenPlatformMicroApplicationlist = list
            };
            return result;
        }

    }
}
