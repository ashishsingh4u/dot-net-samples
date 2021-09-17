using System;
using System.Web.Services;

namespace WebAjaxDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        [WebMethod]
        public static string GetDate()
        {
            return "abc";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}