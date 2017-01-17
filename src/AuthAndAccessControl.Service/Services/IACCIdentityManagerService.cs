using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthAndAccessControl.Service.MembershipReboot;
using IdentityManager.MembershipReboot;

namespace AuthAndAccessControl.Service.Services
{
    public class IACCIdentityManagerService : MembershipRebootIdentityManagerService<IACCUser, IACCGroup>
    {
        public IACCIdentityManagerService(IACCUserAccountService userSvc, IACCGroupService groupSvc)
            : base(userSvc, groupSvc)
        {

        }
    }
}
