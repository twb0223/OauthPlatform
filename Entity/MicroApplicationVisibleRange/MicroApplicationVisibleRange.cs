using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class MicroApplicationVisibleRange
    {
        public Guid Id { get; set; }
        public Guid OpenPlatformMicroApplication_Id { get; set; }

        public Guid Company_Id { get; set; }

        public Guid AdminUserId { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
