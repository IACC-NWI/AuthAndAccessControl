using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot;

namespace AuthAndAccessControl.Service.MembershipReboot
{
    public class IACCGroup : RelationalGroup
    {
        public virtual string Description { get; set; }
    }
}
