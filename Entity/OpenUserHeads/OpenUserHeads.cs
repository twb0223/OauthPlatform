using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class OpenUserHeads
    {
        public Guid ID { get; set; }
        public string OpenId { get; set; }

        public Guid UserId { get; set; }

        public Guid OpenPlatformMicroApplicationId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}