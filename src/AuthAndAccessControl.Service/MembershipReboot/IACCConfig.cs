using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrockAllen.MembershipReboot;

namespace AuthAndAccessControl.Service.MembershipReboot
{
    public class IACCConfig : MembershipRebootConfiguration<IACCUser>
    {
        public static readonly IACCConfig Config;

        static IACCConfig()
        {
            Config = new IACCConfig
            {
                PasswordHashingIterationCount = 10000,
                RequireAccountVerification = false
            };
        }
    }
}
