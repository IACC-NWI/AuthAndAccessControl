using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace AuthAndAccessControl.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.UseLog4Net("logging.config");
                x.Service<AuthAndAccessControlRunnable>(s =>
                {
                    s.ConstructUsing(name => new AuthAndAccessControlRunnable());
                    s.WhenStarted(t => t.Start());
                    s.WhenStopped(t => t.Stop());
                });
                x.SetDisplayName("Auth And Access Control Service");
                x.RunAsLocalService();
            });
        }
    }
}
