using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot.Relational;

namespace AuthAndAccessControl.Service.MembershipReboot
{
    public class IACCUser : RelationalUserAccount
    {
        [DisplayName("First Name")]
        public virtual string FirstName { get; set; }
        [DisplayName("Last Name")]
        public virtual string LastName { get; set; }
        [DisplayName("Membership Number")]
        public virtual string MembershipNumber { get; set; }
        
        public virtual string Subject { get; set; }

    }
}
