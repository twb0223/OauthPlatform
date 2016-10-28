using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 更新app时传入model
    /// </summary>
    public class UpdateAppInput
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// logo
        /// </summary>
        public string logo { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduction { get; set; }


        public string AppUrl { get; set; }

        public string BackUrl { get; set; }
    }
}
