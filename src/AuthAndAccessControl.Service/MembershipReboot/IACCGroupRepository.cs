using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot.Ef;

namespace AuthAndAccessControl.Service.MembershipReboot
{
    public class IACCGroupRepository : DbContextGroupRepository<IACCDatabase, IACCGroup>
    {
        public IACCGroupRepository(IACCDatabase ctx)
            : base(ctx)
        {

        }
    }
}
