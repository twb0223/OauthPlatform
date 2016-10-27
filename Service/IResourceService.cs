using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Service
{
    public interface IResourceService
    {
        List<OpenPlatformMicroApplication> GetAllByCondition(GetOpenPlatformMicroApplicationByConditionInput input);
    }
}
