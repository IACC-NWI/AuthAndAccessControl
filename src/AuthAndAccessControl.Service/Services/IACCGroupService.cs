using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthAndAccessControl.Service.MembershipReboot;
using BrockAllen.MembershipReboot;

namespace AuthAndAccessControl.Service.Services
{
    public class IACCGroupService : GroupService<IACCGroup>
    {
        public IACCGroupService(IACCGroupRepository repo, IACCConfig config)
            : base(config.DefaultTenant, repo)
        {

        }
    }
}
