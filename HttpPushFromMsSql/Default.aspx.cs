// Copyright (c) 2010 Daniel Wojciechowski 
// E-mail: at gmail.com daniel.wojciechowski

using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace HttpPushFromMsSql
{
    public partial class _Default : Page
    {

        protected void PageLoad(object sender, EventArgs e)
        {
            if(!IsPostBack) // "First" page load
            {
                // 1. Generate javascript code

                // 1.1 Generate RefreshData function (JavaScript)
                RegisterFunctionToPostBack("RefreshData", UpdatePanel1);
                
                // 1.2 Write content of LongPolling.js
                string script = File.ReadAllText(Server.MapPath("Script/LongPolling.js"));
                // This script is sticked in html in case when you will need
                // to dynamically change this script (for example script.Replace(...,...))
                // Otherwise you can just include this script using ScriptManager.
                //script = script.Replace("TEMPLATE('lastRecIdValue')", GetLastRecId().ToString());
                Page.ClientScript.RegisterStartupScript(typeof (Page), "LongPoller", script, true);
                // look at http://syedgakbar.wordpress.com/2008/03/01/registerstartupscript-in-ajax-pages/
                // to see how to use ScriptManager1 to register script when it is dynamically hanged on AJAX requests

                // 2. Refresh data in GridView
                RefreshData();
            } 

            if(ScriptManager1.IsInAsyncPostBack) // AJAX partial update
            {
                RefreshData();
            }
        }

        private void RefreshData()
        {
            DateTime refreshStart = DateTime.Now;
            DebugLabel.Text = "Refresh requested: " +
                Tools.ToTimeStringWithMilisecond(DateTime.Now);

            int lastRecId;
            DateTime lastRecTime;
            List<string> data = MessageDal.GetMessageData(out lastRecId, out lastRecTime);
            DebugLabel.Text +=
                "<br>Time measured: <b>" +
                (refreshStart - lastRecTime).TotalSeconds + "s</b>. " +
                "<br>Time measured starting at SQL INSERT, through query " +
                "notification of ASP.NET, Comet notification of browser " + 
                "and finishing at refresh call (by browser) on the server side.";

            Session["LastRecId"] = lastRecId;
            GridView1.DataSource = data;
            GridView1.DataBind();

            //DebugLabel.Text += "<br>Refresh end: " + Tools.ToTimeStringWithMilisecond(DateTime.Now);
            if (!IsPostBack)
                DebugLabel.Text = "";
            if(data.Count==0)
                DebugLabel.Text = "No data";
        }

        // I decided to generate javascript Refresh() function, but you can
        // write it by yourself and include in "LongPolling.js"
        //
        // Thanks to:
        // http://geekswithblogs.net/mnf/articles/102574.aspx
        // http://www.xefteri.com/articles/show.cfm?id=18 How postback works in ASP.NET
        // and thanks to Dave Ward hint for calling __doPostback("UpdatePanel1","") ,
        public bool RegisterFunctionToPostBack(string sFunctionName, Control ctrl)
        { 
            //call the postback function with the right ID __doPostBack('" + UniqueIDWithDollars(ctrl) + @"','');
            string js = "    function " + sFunctionName + @"()
                    {
                    " + ctrl.Page.ClientScript.GetPostBackEventReference(ctrl, "") + @"
                    }";
            ctrl.Page.ClientScript.RegisterStartupScript(GetType(), sFunctionName, js, true);
            return true;
        }

        
    }
}
