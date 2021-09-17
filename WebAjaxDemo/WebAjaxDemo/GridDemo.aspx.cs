using System;
using System.Data;

namespace WebAjaxDemo
{
    public partial class GridDemo : System.Web.UI.Page
    {
        protected void PageLoad(object sender, EventArgs e)
        {

        }

        protected void AddButtonClick(object sender, EventArgs e)
        {
            var table = new DataTable();
            table.Columns.AddRange(new[] {new DataColumn("Name"), new DataColumn("Roll"), new DataColumn("City")});
            table.Rows.Add(new[] {"Ashu", "1", "Delhi"});
            table.Rows.Add(new[] { "Manish", "2", "Noida" });
            table.Rows.Add(new[] { "Swetu", "3", "Bangalore" });
            table.Rows.Add(new[] { "Sachin", "4", "Chennai" });
            _gridView.DataSource = table;
            _gridView.DataBind();
        }
    }
}