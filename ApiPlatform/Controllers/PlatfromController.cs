using Entity;
using Newtonsoft.Json;
using Service;
using System.Web.Http;

namespace ApiPlatform.Controllers
{
    public class PlatfromController : ApiController
    {
        IOauthService oa;
        public PlatfromController(IOauthService oa)
        {
            this.oa = oa;
        }
        /// <summary>
        /// 获取OpenID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("api/Platfrom/GetOpenID")]
        [HttpPost]
        public ResponeOpenIDDto GetOpenID(RequestOpenIDDto model)
        {
            var openid = oa.GetOpenID(model.Token, model.Code);
            ResponeOpenIDDto result = new ResponeOpenIDDto
            {
                StatusCode = StausCode.Ok,
                StatusMsg = StausCode.OkMsg,
                OpenID = openid
            };
            return result;
        }

        /// <summary>
        /// 获取Code
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("api/Platfrom/GetCode")]
        [HttpPost]
        public ResponeCodeDto GetCode(RequstCodeDto model)
        {
            string userCode = "";
            userCode = oa.GetUserCode(model.AppID, model.UserID);
            if (string.IsNullOrEmpty(userCode))
            {
                userCode = oa.CreateUserCode(model.AppID, model.UserID);//创建UserCode 
            }
            ResponeCodeDto result = new ResponeCodeDto
            {
                StatusCode = StausCode.Ok,
                StatusMsg = StausCode.OkMsg,
                Code = userCode
            };
            return result;
        }
    }
}
