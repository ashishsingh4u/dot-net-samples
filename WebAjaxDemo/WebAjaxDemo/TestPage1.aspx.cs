using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAjaxDemo
{
    public partial class TestPage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Hello"] = "Hello Sir";
            Application["Hello"] = "Hello Application";
        }

        protected void Button1Click(object sender, EventArgs e)
        {
            Response.Redirect("TestPage2.aspx?Hello=" + TextBox1.Text + "&Hi=" + TextBox2.Text);
        }

    }
}