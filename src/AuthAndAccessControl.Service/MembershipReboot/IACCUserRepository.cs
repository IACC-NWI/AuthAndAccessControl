using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot.Ef;

namespace AuthAndAccessControl.Service.MembershipReboot
{
    public class IACCUserRepository : DbContextUserAccountRepository<IACCDatabase, IACCUser>
    {
        public IACCUserRepository(IACCDatabase ctx) : base(ctx)
        {

        }

    }
}
