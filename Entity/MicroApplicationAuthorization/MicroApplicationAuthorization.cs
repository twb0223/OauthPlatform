using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 应用授权
    /// </summary>
    public class MicroApplicationAuthorization
    {
        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }

        public Guid OpenPlatformMicroApplicationId { get; set; }

        public Guid CompanyId { get; set; }

        public int RangeType { get; set; }

        public Guid UserId { get; set; }


        public Guid? RangeId { get; set; }
    }
}
