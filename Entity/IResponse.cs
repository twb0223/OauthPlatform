using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public  interface IResponse
    {
        /// <summary>
        /// 代码
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        string Message { get; set; }
    }
}
