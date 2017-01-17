using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Owin.Hosting;

namespace AuthAndAccessControl.Service
{
    public class AuthAndAccessControlRunnable
    {
        public static IWindsorContainer Container = new WindsorContainer();
        private IDisposable httpServer;
        public AuthAndAccessControlRunnable()
        {
            Container.Install(FromAssembly.This());
        }
        public void Start()
        {
            httpServer = WebApp.Start<OwinStartup>(url: ConfigurationManager.AppSettings["IdentityServerSecureUrl"]);
        }

        public void Stop()
        {
            
        }
    }
    
}
