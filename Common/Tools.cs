﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.Configuration;

namespace Common
{
    public class Tools
    {
        public static string CreateAppID()
        {
            string str = Guid.NewGuid().ToString("N");
            return str;
        }
        public static string CreateAppSecret()
        {
            string str = Guid.NewGuid().ToString("N");
            byte[] result = Encoding.Default.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            str = BitConverter.ToString(output).Replace("-", "");
            return str;
        }

        public static string CreateToken()
        {
            Random rnd = new Random();
            int seed = 0;
            var rndData = new byte[64];
            rnd.NextBytes(rndData);
            var seedValue = Interlocked.Add(ref seed, 1);
            var seedData = BitConverter.GetBytes(seedValue);
            var tokenData = rndData.Concat(seedData).OrderBy(_ => rnd.Next());
            return Convert.ToBase64String(tokenData.ToArray()).TrimEnd('=');
        }

        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        public static string MD5Encrypt(string strText)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string result = BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText)), 4, 8);
            result = result.Replace("-", "");
            return result;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="paramData"></param>
        /// <param name="dataEncode"></param>
        /// <returns></returns>
        public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/json";
                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), dataEncode);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="Url">URL</param>
        /// <param name="postDataStr">请求参数</param>
        /// <param name="type">Contenttype</param>
        /// <returns></returns>
        public static string GetWebRequest(string Url, string postDataStr, string type, Encoding dataEncode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            if (type == "json")
            {
                request.ContentType = "application/json;charset=UTF-8";
            }
            else
            {
                request.ContentType = "text/html;charset=UTF-8";
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, dataEncode);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;

        }

        /// <summary>
        /// 获取平台域名地址
        /// </summary>
        /// <returns></returns>
        public static string GetPlatformUrl()
        {
            return ConfigurationManager.AppSettings["PlatfromUrl"].ToString();
        }
    }
}