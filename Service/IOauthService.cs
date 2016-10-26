﻿using Entity;

namespace Service
{
    public interface IOauthService
    {
        bool CheckApp(string Appid, string AppSecret);
        bool CheckTokenAndOpenID(string token, string OpenId);
        bool CreateApp(OpenPlatformMicroApplication model);
        string CreateToken(string AppID);
        string CreateUserCode(string AppID, string UserID);
        string GetOpenID(string token, string code);
        string GetToken(string AppID);
        string GetUserCode(string AppID, string UserID);
        bool UpdateApp(OpenPlatformMicroApplication model);
    }
}