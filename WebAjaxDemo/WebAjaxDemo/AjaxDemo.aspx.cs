using System;

namespace WebAjaxDemo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void PageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label1.Text = DateTime.Now.ToString();
                Label2.Text = DateTime.Now.ToString();
            }
            else
            {
                Label1.Text = string.Empty;
                Label2.Text = string.Empty;
            }
        }

        protected void Timer1Tick(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToString();
            Label2.Text = DateTime.Now.ToString();
        }
    }
}