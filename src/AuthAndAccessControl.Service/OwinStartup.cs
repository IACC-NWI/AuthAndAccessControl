using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AuthAndAccessControl.Service.MembershipReboot;
using AuthAndAccessControl.Service.Services;
using IdentityAdmin.Configuration;
using IdentityAdmin.Core;
using IdentityManager;
using IdentityManager.Configuration;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.EntityFramework;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OpenIdConnect;
using Newtonsoft.Json;
using Owin;
using WindsorWebApiDependency;


namespace AuthAndAccessControl.Service
{
    public class OwinStartup
    {
        private readonly string _issuerUri;
        private readonly string _publicOriginUri;
        private readonly string _identityManagerAdminRoleName;
        private readonly string _identityStoreConnection;
        private readonly string _identityServerSecureUrl;
        private bool requireSSL { get; set; } = false;
        public OwinStartup()
        {
            _issuerUri = ConfigurationManager.AppSettings["IssuerUri"];
            _publicOriginUri = ConfigurationManager.AppSettings["PublicOrigin"];
            _identityStoreConnection = ConfigurationManager.ConnectionStrings["IdentityStoreDatabase"].ConnectionString;
            _identityServerSecureUrl = ConfigurationManager.AppSettings["IdentityServerSecureUrl"];
            _identityManagerAdminRoleName = ConfigurationManager.AppSettings["IdentityManager/AdminRoleName"];
        }

        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            log4net.Config.XmlConfigurator.Configure();
            app.UseCors(CorsOptions.AllowAll);
            

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                CookieName = "NWI-IACC-IdentityServer"
            });
            app.Map("/identity", identity =>
            {
                var svcOptions = new EntityFrameworkServiceOptions { ConnectionString = _identityStoreConnection };
                var factory = new IdentityServerServiceFactory();
                factory.UserService = new IdentityServer3.Core.Configuration.Registration<IUserService, IACCUserService>();
                factory.Register(new IdentityServer3.Core.Configuration.Registration<IACCUserAccountService>());
                factory.Register(new IdentityServer3.Core.Configuration.Registration<IACCConfig>(IACCConfig.Config));
                factory.Register(new IdentityServer3.Core.Configuration.Registration<IACCUserRepository>());
                factory.Register(
                    new IdentityServer3.Core.Configuration.Registration<IACCDatabase>(resolver => new IACCDatabase(_identityStoreConnection)));
                factory.RegisterOperationalServices(svcOptions);

                //Use client and scope stores from database
                factory.RegisterConfigurationServices(svcOptions);
                
                identity.UseIdentityServer(new IdentityServerOptions
                {
                    IssuerUri = _issuerUri,
                    SiteName = "NWI IACC Single Sign-On Server",
                    //SigningCertificate = LoadCertificate(),
                    RequireSsl = requireSSL,
                    Factory = factory,
                    AuthenticationOptions = new AuthenticationOptions()
                    {
                        //IdentityProviders = ConfigureAdditionalIdProviders,
                        EnableSignOutPrompt = true,
                        EnablePostSignOutAutoRedirect = true,
                        PostSignOutAutoRedirectDelay = 0
                    },
                    //CspOptions = new CspOptions() { },
                    LoggingOptions = new LoggingOptions() { EnableHttpLogging = true, EnableKatanaLogging = true },
                    PublicOrigin = _publicOriginUri


                });
            });
            app.Map("/localclientadmin", adminApp =>
            {
                var factory = new IdentityAdminServiceFactory();
                factory.IdentityAdminService = new IdentityAdmin.Configuration.Registration<IIdentityAdminService>(resolver => new IACCAdminManagerService(_identityStoreConnection));
                adminApp.UseIdentityAdmin(new IdentityAdminOptions()
                {
                    Factory = factory,
                    AdminSecurityConfiguration = new IdentityAdmin.Configuration.LocalhostSecurityConfiguration()
                    {
                        RequireSsl = requireSSL,
                        HostAuthenticationType = IdentityManager.Constants.LocalAuthenticationType,
                        ShowLoginButton = true
                    }
                });

            });
            app.Map("/clientadmin", adminApp =>
            {
                adminApp.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                {
                    Authority = _identityServerSecureUrl + "identity",
                    ClientId = "idAdmin",
                    RedirectUri = _identityServerSecureUrl + "clientadmin/#/",
                    ResponseType = "id_token",
                    Scope = "openid nuveenEmployee",
                    SignInAsAuthenticationType = IdentityManager.Constants.CookieAuthenticationType
                });

                var factory = new IdentityAdminServiceFactory();
                factory.IdentityAdminService = new IdentityAdmin.Configuration.Registration<IIdentityAdminService>(resolver => new IACCAdminManagerService(_identityStoreConnection));
                adminApp.UseIdentityAdmin(new IdentityAdminOptions()
                {
                    Factory = factory,
                    AdminSecurityConfiguration = new AdminHostSecurityConfiguration()
                    {
                        RequireSsl = requireSSL,
                        AdminRoleName = _identityManagerAdminRoleName,
                        HostAuthenticationType = IdentityManager.Constants.CookieAuthenticationType,
                        ShowLoginButton = true,
                        RoleClaimType = "role",
                        NameClaimType = "name"
                    }
                });

            });
            app.Map("/idMgrAdmin", idmgr =>
            {
                idmgr.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                {
                    //    AuthenticationType = "oidc",
                    Authority = _identityServerSecureUrl + "identity",
                    ClientId = IdentityManager.Constants.IdMgrClientId,
                    RedirectUri = _identityServerSecureUrl + "identitymanagerAdmin/#/",
                    ResponseType = "id_token",
                    Scope = "openid nuveenEmployee",
                    SignInAsAuthenticationType = IdentityManager.Constants.CookieAuthenticationType
                });
                var factory = new IdentityManagerServiceFactory();
                factory.IdentityManagerService = new IdentityManager.Configuration.Registration<IIdentityManagerService, IACCIdentityManagerService>();
                factory.Register(new IdentityManager.Configuration.Registration<IACCUserAccountService>());
                factory.Register(new IdentityManager.Configuration.Registration<IACCGroupService>());
                factory.Register(new IdentityManager.Configuration.Registration<IACCUserRepository>());
                factory.Register(new IdentityManager.Configuration.Registration<IACCGroupRepository>());
                factory.Register(new IdentityManager.Configuration.Registration<IACCDatabase>(resolver => new IACCDatabase(_identityStoreConnection)));
                factory.Register(new IdentityManager.Configuration.Registration<IACCConfig>(IACCConfig.Config));

                idmgr.UseIdentityManager(new IdentityManagerOptions()
                {
                    Factory = factory,
                    SecurityConfiguration = new HostSecurityConfiguration()
                    {
                        RequireSsl = requireSSL,
                        AdminRoleName = _identityManagerAdminRoleName,
                        HostAuthenticationType = Constants.CookieAuthenticationType,
                        ShowLoginButton = true,
                        RoleClaimType = "role",
                        NameClaimType = "name"
                    },

                });
            });
            app.Map("/localIdMgrAdmin", adminApp =>
            {
                var factory = new IdentityManagerServiceFactory();
                factory.IdentityManagerService = new IdentityManager.Configuration.Registration<IIdentityManagerService, IACCIdentityManagerService>();
                factory.Register(new IdentityManager.Configuration.Registration<IACCUserAccountService>());
                factory.Register(new IdentityManager.Configuration.Registration<IACCGroupService>());
                factory.Register(new IdentityManager.Configuration.Registration<IACCUserRepository>());
                factory.Register(new IdentityManager.Configuration.Registration<IACCGroupRepository>());
                factory.Register(new IdentityManager.Configuration.Registration<IACCDatabase>(resolver => new IACCDatabase(_identityStoreConnection)));
                factory.Register(new IdentityManager.Configuration.Registration<IACCConfig>(IACCConfig.Config));

                adminApp.UseIdentityManager(new IdentityManagerOptions()
                {
                    Factory = factory,
                    SecurityConfiguration = new IdentityManager.Configuration.LocalhostSecurityConfiguration()
                    {
                        RequireSsl = requireSSL,
                        HostAuthenticationType = IdentityManager.Constants.LocalAuthenticationType,
                        ShowLoginButton = true
                    }
                });
            });

            var config = new HttpConfiguration();
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.DependencyResolver = new WindsorDependencyResolver(AuthAndAccessControlRunnable.Container);
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            app.UseWebApi(config);
        }
    }
}
