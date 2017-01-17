using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Admin.EntityFramework;
using IdentityServer3.Admin.EntityFramework.Entities;

namespace AuthAndAccessControl.Service.Services
{
    public class IACCAdminManagerService : IdentityAdminCoreManager<IdentityClient, int, IdentityScope, int>
    {
        public IACCAdminManagerService(string connectionString) : base(connectionString)
        {

        }
    }
}
