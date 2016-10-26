﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class RequestMicroAppDto
    {
        public string AppName { get; set; }
        /// <summary>
        /// logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduction { get; set; }


        public string AppUrl { get; set; }

        public string BackUrl { get; set; }

        public Guid CreateUserId { get; set; }
    }
}
