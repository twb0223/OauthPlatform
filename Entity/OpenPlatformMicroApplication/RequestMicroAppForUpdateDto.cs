using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class RequestMicroAppForUpdateDto
    {
        public string AppID { get; set; }
        public string AppName { get; set; }
        /// <summary>
        /// logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduction { get; set; }

        public int IsOpen { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string AppUrl { get; set; }


        public string BackUrl { get; set; }

    }
}
