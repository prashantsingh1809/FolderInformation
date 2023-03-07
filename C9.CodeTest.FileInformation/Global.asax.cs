using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace C9.CodeTest.FileInformation
{
    public class Global : System.Web.HttpApplication
    {
        public static ILog logger;
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            logger = log4net.LogManager.GetLogger("ErrorLog");
        }
    }
}