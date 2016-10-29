using Entity;
using System.Web.Http;
using Service;
using ApiPlatform.App_Start;
using System;
using Common;
using Newtonsoft.Json;

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
        //public ResponeBaseModel Get()
        //{
        //    //OpenPlatformUsersRegisterDto model = new OpenPlatformUsersRegisterDto();
        //    //model.UserName = "Test";
        //    //model.Email = "xxx@ccc.com";
        //    //model.Mobile = "1231215";
        //    //model.Password = "123456";
        //    //model.CreateTime = DateTime.Now;
        //    //model.Password = Tools.MD5Encrypt(Tools.MD5Encrypt(model.Password));
        //    //var code = StausCode.Ok;
        //    //var msg = StausCode.OkMsg;
        //    //OpenPlatformUsers entity = null;
        //    //try
        //    //{
        //    //    entity = irs.Register(model);

        //    //}
        //    //catch (Exception)
        //    //{
        //    //    code = StausCode.Exception;
        //    //    msg = StausCode.ExceptionMsg;
        //    //}
        //    //return new OpenPlatformUsersRegisterOutput
        //    //{
        //    //    OpenPlatformUser = new OpenPlatformUsersRegisterOutputDto
        //    //    {
        //    //        Id = entity.Id,
        //    //        UserName = entity.UserName,
        //    //        Email = entity.Email,
        //    //        Mobile = entity.Mobile,
        //    //        CreateTime = entity.CreateTime
        //    //    },
        //    //    StatusCode = code,
        //    //    StatusMsg = msg
        //    //};

        //    //两次md5 加密后再传递到后台进行比较
        //    //var model = new OpenPlatformUserLoginDto();
        //    //model.UserName = "Test";
        //    //model.Password = "123456";
        //    //model.Password = Tools.MD5Encrypt(Tools.MD5Encrypt(model.Password));
        //    //var code = StausCode.Ok;
        //    //var msg = StausCode.OkMsg;
        //    //if (!irs.Login(model))
        //    //{
        //    //    code = StausCode.LoginNamePwdErr;
        //    //    msg = StausCode.LoginNamePwdErrMsg;
        //    //}
        //    //return new ResponeBaseModel { StatusCode = code, StatusMsg = msg };

        //    var model = new OpenPlatformUserChangePasswordDto();
        //    model.UserName = "Test";
        //    model.OldPassword = "123456";
        //    model.NewPassword = "qqqqqq";
        //    model.NewPassword = Tools.MD5Encrypt(Tools.MD5Encrypt(model.NewPassword));
        //    model.OldPassword = Tools.MD5Encrypt(Tools.MD5Encrypt(model.OldPassword));

        //    var code = StausCode.Ok;
        //    var msg = StausCode.OkMsg;
        //    if (!irs.ChangePassword(model))
        //    {
        //        code = StausCode.DataUpdateException;
        //        msg = StausCode.DataUpdateExceptionMsg;
        //    }
        //    return new ResponeBaseModel { StatusCode = code, StatusMsg = msg };

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

        [Route("api/Resource/CreateMicroApp")]
        [HttpPost]
        public OpenPlatformMicroApplicationDto CreateMicroApp(CreateMicroAppInput model)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            OpenPlatformMicroApplication m = new OpenPlatformMicroApplication
            {
                Id = Guid.NewGuid(),
                AppID = Tools.CreateAppID(),
                AppSecret = Tools.CreateAppSecret(),
                CreateTime = DateTime.Now,
                IsExamine = 1,
                IsOpen = 0,
                Name = model.Name,
                AppUrl = model.AppUrl,
                BackUrl = model.BackUrl,
                CreatorId = model.CreateUserId,
                Introduction = model.Introduction,
                logo = model.logo
            };
            OpenPlatformMicroApplication entity = null;
            try
            {
                entity = irs.CreateApp(m);

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

        [Route("api/Resource/UpdateMicroApp")]
        [HttpPost]
        public OpenPlatformMicroApplicationDto UpdteMicroApp(UpdateAppInput model)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            OpenPlatformMicroApplication entity = null;
            try
            {
                entity = irs.UpdateApp(model);
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

        [Route("api/Resource/DeleteApp")]
        [HttpGet]
        public ResponeBaseModel DeleteApp(string Id)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            try
            {
                var result = irs.DeleteApp(Guid.Parse(Id));
                if (!result)
                {
                    code = StausCode.DataUpdateException;
                    msg = StausCode.DataUpdateExceptionMsg;
                }
            }
            catch (Exception)
            {
                code = StausCode.Exception;
                msg = StausCode.ExceptionMsg;
            }
            return new ResponeBaseModel
            {
                StatusCode = code,
                StatusMsg = msg
            };
        }

        [Route("api/Resource/GetAppInfo")]
        [HttpGet]
        public GetAppInfoOutput GetAppInfo(string AppId)
        {
            var result = irs.GetAppInfo(AppId);
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            if (result == null)
            {
                code = StausCode.DataNotFound;
                msg = StausCode.DataNotFoundMsg;
            }
            return new GetAppInfoOutput
            {
                StatusCode = code,
                StatusMsg = msg,
                AppInfo = result
            };
        }

        [Route("api/Resource/Register")]
        [HttpPost]
        public OpenPlatformUsersRegisterOutput Register(OpenPlatformUsersRegisterDto model)
        {
            //密码两次md加密存储
            model.Password = Tools.MD5Encrypt(Tools.MD5Encrypt(model.Password));
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            OpenPlatformUsers entity = null;
            try
            {
                entity = irs.Register(model);

            }
            catch (Exception)
            {
                code = StausCode.Exception;
                msg = StausCode.ExceptionMsg;
            }
            return new OpenPlatformUsersRegisterOutput
            {
                OpenPlatformUser = new OpenPlatformUsersRegisterOutputDto
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    Email = entity.Email,
                    Mobile = entity.Mobile,
                    CreateTime = entity.CreateTime
                },
                StatusCode = code,
                StatusMsg = msg
            };
        }

        [Route("api/Resource/Login")]
        [HttpPost]
        public ResponeBaseModel Login(OpenPlatformUserLoginDto model)
        {
            //两次md5 加密后再传递到后台进行比较
            model.Password = Tools.MD5Encrypt(Tools.MD5Encrypt(model.Password));
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            if (!irs.Login(model))
            {
                code = StausCode.LoginNamePwdErr;
                msg = StausCode.LoginNamePwdErrMsg;
            }
            return new ResponeBaseModel { StatusCode = code, StatusMsg = msg };
        }

        [Route("api/Resource/ChangePassowrd")]
        [HttpPost]
        public ResponeBaseModel ChangePassowrd(OpenPlatformUserChangePasswordDto model)
        {
            model.NewPassword = Tools.MD5Encrypt(Tools.MD5Encrypt(model.NewPassword));
            model.OldPassword = Tools.MD5Encrypt(Tools.MD5Encrypt(model.OldPassword));

            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            if (!irs.ChangePassword(model))
            {
                code = StausCode.DataUpdateException;
                msg = StausCode.DataUpdateExceptionMsg;
            }
            return new ResponeBaseModel { StatusCode = code, StatusMsg = msg };
        }

        [Route("api/Resource/ExamineApp")]
        [HttpPost]
        public ResponeBaseModel ExamineApp(ExamineAppInput model)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            if (!irs.ExamineApp(model))
            {
                code = StausCode.DataUpdateException;
                msg = StausCode.DataUpdateExceptionMsg;
            }
            return new ResponeBaseModel { StatusCode = code, StatusMsg = msg };
        }

        [HttpPost]
        [Route("api/Resource/AddAuthorization")]
        public MicroApplicationAuthorizationOutput AddAuthorization(MicroApplicationAuthorizationDto model)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            MicroApplicationAuthorization entity = null;
            try
            {
                entity = irs.AddAuthorization(model);
                if (entity == null)
                {
                    code = StausCode.DataCreteException;
                    msg = StausCode.DataCreateExceptionMsg;
                }
            }
            catch (Exception)
            {
                code = StausCode.Exception;
                msg = StausCode.ExceptionMsg;
            }
            return new MicroApplicationAuthorizationOutput
            {
                StatusCode = code,
                StatusMsg = msg,
                microApplicationAuthorization = entity
            };


        }

        [HttpPost]
        [Route("api/Resource/UpdateAuthorization")]
        public MicroApplicationAuthorizationOutput UpdateAuthorization(MicroApplicationAuthorizationDto model)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            MicroApplicationAuthorization entity = null;
            try
            {
                entity = irs.UpdateAuthorization(model);
                if (entity == null)
                {
                    code = StausCode.DataUpdateException;
                    msg = StausCode.DataUpdateExceptionMsg;
                }
            }
            catch (Exception)
            {
                code = StausCode.Exception;
                msg = StausCode.ExceptionMsg;
            }
            return new MicroApplicationAuthorizationOutput
            {
                StatusCode = code,
                StatusMsg = msg,
                microApplicationAuthorization = entity
            };
        }

        [Route("api/Resource/DeleteAuthorization")]
        [HttpGet]
        public ResponeBaseModel DeleteAuthorization(string Id)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            try
            {
                var result = irs.DeleteAuthorization(Guid.Parse(Id));
                if (!result)
                {
                    code = StausCode.DataUpdateException;
                    msg = StausCode.DataUpdateExceptionMsg;
                }
            }
            catch (Exception)
            {
                code = StausCode.Exception;
                msg = StausCode.ExceptionMsg;
            }
            return new ResponeBaseModel
            {
                StatusCode = code,
                StatusMsg = msg
            };
        }



        [HttpPost]
        [Route("api/Resource/AddMicroApplicationVisibleRange")]
        public MicroApplicationVisibleRangeOutput AddMicroApplicationVisibleRange(MicroApplicationVisibleRangeDto model)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            MicroApplicationVisibleRange entity = null;
            try
            {
                entity = irs.AddMicroApplicationVisibleRange(model);
                if (entity == null)
                {
                    code = StausCode.DataCreteException;
                    msg = StausCode.DataCreateExceptionMsg;
                }
            }
            catch (Exception)
            {
                code = StausCode.Exception;
                msg = StausCode.ExceptionMsg;
            }
            return new MicroApplicationVisibleRangeOutput
            {
                StatusCode = code,
                StatusMsg = msg,
                microApplicationVisibleRange = entity
            };


        }

        [HttpPost]
        [Route("api/Resource/UpdateMicroApplicationVisibleRange")]
        public MicroApplicationVisibleRangeOutput UpdateMicroApplicationVisibleRange(MicroApplicationVisibleRangeDto model)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            MicroApplicationVisibleRange entity = null;
            try
            {
                entity = irs.UpdateMicroApplicationVisibleRange(model);
                if (entity == null)
                {
                    code = StausCode.DataUpdateException;
                    msg = StausCode.DataUpdateExceptionMsg;
                }
            }
            catch (Exception)
            {
                code = StausCode.Exception;
                msg = StausCode.ExceptionMsg;
            }
            return new MicroApplicationVisibleRangeOutput
            {
                StatusCode = code,
                StatusMsg = msg,
                microApplicationVisibleRange = entity
            };
        }

        [Route("api/Resource/DeleteMicroApplicationVisibleRange")]
        [HttpGet]
        public ResponeBaseModel DeleteMicroApplicationVisibleRange(string Id)
        {
            var code = StausCode.Ok;
            var msg = StausCode.OkMsg;
            try
            {
                var result = irs.DeleteMicroApplicationVisibleRange(Guid.Parse(Id));
                if (!result)
                {
                    code = StausCode.DataUpdateException;
                    msg = StausCode.DataUpdateExceptionMsg;
                }
            }
            catch (Exception)
            {
                code = StausCode.Exception;
                msg = StausCode.ExceptionMsg;
            }
            return new ResponeBaseModel
            {
                StatusCode = code,
                StatusMsg = msg
            };
        }

    }
}
