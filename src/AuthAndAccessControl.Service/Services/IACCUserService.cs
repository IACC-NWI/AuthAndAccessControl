using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthAndAccessControl.Service.MembershipReboot;
using IdentityServer3.Core.Models;
using IdentityServer3.MembershipReboot;

namespace AuthAndAccessControl.Service.Services
{
    public class IACCUserService : MembershipRebootUserService<IACCUser>
    {
        public IACCUserService(IACCUserAccountService userSvc) :
            base(userSvc)
        {

        }
        public override Task AuthenticateExternalAsync(ExternalAuthenticationContext ctx)
        {
            var defaultClaims = ctx.ExternalIdentity.Claims as List<Claim>;
            var emailClaim = defaultClaims.First(t => t.Type.Equals("email")).Value;
            ctx.ExternalIdentity.Claims = defaultClaims;


            return base.AuthenticateExternalAsync(ctx);
        }
    }
}
