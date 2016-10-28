using Entity;
using System;

namespace Service
{
    public interface IOauthService
    {
        bool CheckApp(string Appid, string AppSecret);
        bool CheckTokenAndOpenID(string token, string OpenId); 
        string CreateToken(string AppID);
        string CreateUserCode(string AppID, string UserID);
        string GetOpenID(string Appid, string token, string code);
        string GetToken(string AppID);
        string GetUserCode(string AppID, string UserID);
        TimeSpan? KeyTimeToLive(string key);
    }
}