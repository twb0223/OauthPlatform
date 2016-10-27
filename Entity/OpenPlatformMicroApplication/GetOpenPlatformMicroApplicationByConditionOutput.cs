using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class GetOpenPlatformMicroApplicationByConditionOutput : ResponeBaseModel
    {
        public List<OpenPlatformMicroApplication> OpenPlatformMicroApplicationlist { get; set; }
    }
}
