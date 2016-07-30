using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRGetHelpBot
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Assembly web = Assembly.GetExecutingAssembly();
            AssemblyName webName = web.GetName();

            lblVersion.Text = webName.Version.ToString();

        }
    }
}