using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class StausCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        public const string Ok = "0";
        public const string OkMsg = "操作成功";

        /// <summary>
        /// 异常
        /// </summary>
        public const string Exception = "-999";
        public const string ExceptionMsg = "服务异常";

        /// <summary>
        /// 参数错误
        /// </summary>
        public const string InvalidParam = "3";
        public const string InvalidParamMsg = "操作无效";


        /// <summary>
        /// Appid或密码不正确
        /// </summary>
        public const string LoginNamePwdErr = "1001";
        public const string LoginNamePwdErrMsg = "用户名或密码不正确";


        /// <summary>
        /// Appid或密码不正确
        /// </summary>
        public const string AppIDSecretErr = "1002";
        public const string AppIDSecretErrMsg = "AppID或AppSecret不正确";

        /// <summary>
        /// Token过期
        /// </summary>
        public const string TokenOrOpenIDError = "2001";
        public const string TokenOrOpenIDErrorMsg = "Token或OpenId无效";
    };
}