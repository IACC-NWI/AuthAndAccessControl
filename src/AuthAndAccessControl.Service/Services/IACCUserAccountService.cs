using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthAndAccessControl.Service.MembershipReboot;
using BrockAllen.MembershipReboot;

namespace AuthAndAccessControl.Service.Services
{
    public class IACCUserAccountService : UserAccountService<IACCUser>
    {
        public IACCUserAccountService(IACCConfig config, IACCUserRepository repo)
            : base(config, repo)
        {

        }
    }
}
