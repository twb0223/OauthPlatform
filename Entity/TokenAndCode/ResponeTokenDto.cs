﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ResponeTokenDto:ResponeBaseModel
    {
        public string Token { get; set; }

        public double ExpiresIn { get; set; }
    }
}
