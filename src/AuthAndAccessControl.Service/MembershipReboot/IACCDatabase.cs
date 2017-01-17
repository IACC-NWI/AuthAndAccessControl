using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot.Ef;

namespace AuthAndAccessControl.Service.MembershipReboot
{
    public class IACCDatabase : MembershipRebootDbContext<IACCUser, IACCGroup>
    {
        public IACCDatabase(string name)
            : base(name)
        {

        }

        public IACCDatabase()
            : base("IdentityStoreDatabase")
        {

        }
    }
}
