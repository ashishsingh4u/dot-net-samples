using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace ASPNETWebApplication
{
    /// <summary>
    /// Home page.
    /// </summary>
    public partial class Default : PageBase
    {
        /// <summary>
        /// Override. Returns breadcrumb node for this page. 
        /// </summary>
        /// <returns></returns>
        public override SiteMapNode SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            return new SiteMapNode(e.Provider, "Home", "~/", "home", "");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Meta data: helpful for SEO (search engine optimization)
            Page.Title = "Patterns in Action Web Forms Application";
            Page.MetaKeywords = "Shopping, Electronic Products, Patterns in Action, ASP.NET Web Forms";
            Page.MetaDescription = "Patterns in Action Web Forms Application"; 
          
            if (!IsPostBack)
            {
                // Set the selected menu item in Master page.
                SelectedMenu = "home";

                // Register javascript that opens popup windows.
                RegisterOpenWindowJavaScript();
            }
        }
    }
}
