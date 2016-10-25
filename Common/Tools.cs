using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

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
    }
}