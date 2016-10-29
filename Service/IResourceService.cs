using System;
using System.Collections.Generic;
using Entity;


namespace Service
{
    public interface IResourceService
    {
        List<OpenPlatformMicroApplication> GetAllByCondition(GetOpenPlatformMicroApplicationByConditionInput input);

        OpenPlatformMicroApplication CreateApp(OpenPlatformMicroApplication model);
        OpenPlatformMicroApplication UpdateApp(UpdateAppInput model);

        bool DeleteApp(Guid Id);

        GetAppInfoDto GetAppInfo(string AppId);

        OpenPlatformUsers Register(OpenPlatformUsersRegisterDto model);

        bool Login(OpenPlatformUserLoginDto model);

        bool ChangePassword(OpenPlatformUserChangePasswordDto model);

        bool ExamineApp(ExamineAppInput model);

        MicroApplicationAuthorization AddAuthorization(MicroApplicationAuthorizationDto model);
        MicroApplicationAuthorization UpdateAuthorization(MicroApplicationAuthorizationDto model);
        bool DeleteAuthorization(Guid Id);


        MicroApplicationVisibleRange AddMicroApplicationVisibleRange(MicroApplicationVisibleRangeDto model);
        MicroApplicationVisibleRange UpdateMicroApplicationVisibleRange(MicroApplicationVisibleRangeDto model);
        bool DeleteMicroApplicationVisibleRange(Guid Id);
    }
}
