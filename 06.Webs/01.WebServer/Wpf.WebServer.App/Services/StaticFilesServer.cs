#region Using

using System;
using System.IO;
using System.Reflection;

using System.Web.Http;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;

using NLib;
using NLib.Reflection;
using NLib.Services.RestApi;
using Owin;
using static NLib.Services.RestApi.NRestServerStartUp;

#endregion

namespace NLib.Services.RestApi
{
    /// <summary>
    /// Web Server StartUp class.
    /// </summary>
    public class StartUp : NRestServerStartUp
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public StartUp() : base()
        {
            this.AuthenticationValidator = (string userName, string password) =>
            {
                return true;
            };
            this.EnableSwagger = true;
            this.ApiName = "Web Server Application";
            this.ApiVersion = "v1";
        }

        #endregion

        internal static class MapControllers
        {
            internal static class Files
            {
                internal static void MapRoutes(HttpConfiguration config)
                {
                    config.Routes.MapHttpRoute(
                        name: "DefaultApi",
                        routeTemplate: "api/{controller}/{id}",
                        defaults: new { id = RouteParameter.Optional });
                }
            }
        }

        #region Override Methods

        public override void Configuration(IAppBuilder app)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            base.Configuration(app);

            // Set File Root
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            //string rootPath = Path.Combine(appPath, "www");
            string rootPath = Path.Combine(appPath, "flutter");
            try
            {
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                var physicalFileSystem = new PhysicalFileSystem(rootPath);
                var options = new Microsoft.Owin.StaticFiles.FileServerOptions
                {
                    EnableDefaultFiles = true,
                    FileSystem = physicalFileSystem
                };
                options.StaticFileOptions.FileSystem = physicalFileSystem;
                options.StaticFileOptions.ServeUnknownFileTypes = true;
                options.DefaultFilesOptions.DefaultFileNames = new[]
                {
                    "index.html"
                };

                app.UseFileServer(options);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        /// <summary>
        /// Init Map Routes.
        /// </summary>
        /// <param name="config">The HttpConfiguration instance.</param>
        protected override void InitMapRoutes(HttpConfiguration config)
        {
            // Handle route by specificed controller (Route Order is important).
            MapControllers.Files.MapRoutes(config);

            #region Default Route (do not used)

            // If comment below line the auto map default controllers will not load and cannot access.
            //InitDefaultMapRoute(config);

            #endregion
        }

        #endregion
    }

    /// <summary>
    /// Static Files Web Server (Self Host).
    /// </summary>
    public class StaticFilesServer
    {
        #region Internal Variables

        private IDisposable server = null;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public StaticFilesServer() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~StaticFilesServer()
        {
            Shutdown();
        }

        #endregion

        #region Private Methods

        private string BaseAddress
        {
            get
            {
                string result = string.Format(@"{0}://{1}:{2}", "http", "+", 9000); ;
                return result;
            }
        }

        private void InitOwinFirewall()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            string portNum = "9000";
            string appName = "Static Files Web Server (REST)";
            var nash = new CommandLine();
            nash.Run("http add urlacl url=http://+:" + portNum + "/ user=Everyone");
            nash.Run("advfirewall firewall add rule dir=in action=allow protocol=TCP localport=" + portNum + " name=\"" + appName + "\" enable=yes profile=Any");
        }

        private void ReleaseOwinFirewall()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            string portNum = "9000";
            string appName = "Static Files Web Server";
            var nash = new CommandLine();
            nash.Run("http delete urlacl url=http://+:" + portNum + "/");
            nash.Run("advfirewall firewall delete rule name=\"" + appName + "\"");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start service.
        /// </summary>
        public void Start()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (null == server)
            {
                InitOwinFirewall();
                server = WebApp.Start<StartUp>(url: BaseAddress);
                med.Info("Static Files Web Server started.");
            }
            else
            {
                med.Info("Static Files Web Server starting failed.");
            }
        }

        /// <summary>
        /// Shutdown service.
        /// </summary>
        public void Shutdown()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (null != server)
            {
                server.Dispose();
            }
            server = null;
            ReleaseOwinFirewall();
            med.Info("Static Files Web Server shutdown.");
        }

        #endregion
    }
}
