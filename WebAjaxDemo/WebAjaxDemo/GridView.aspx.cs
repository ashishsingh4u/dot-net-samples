using System;
using System.Data;
using System.Web.Services;

namespace WebAjaxDemo
{
    public partial class GridView : System.Web.UI.Page
    {
        protected int index = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.gv.DataSource = GetBooks(5);
                this.gv.DataBind();
            }
        }

        [WebMethod]
        public static string SelectBook(int id, string title, string author)
        {
            string msg = string.Format("You selected \"{0}\" by {1} ", title, author);
            return msg;
        }

        public static DataTable GetBooks(int bookCount)
        {
            DataTable table = new DataTable();
            table.Columns.AddRange(new[] { new DataColumn("ID",typeof(int)), new DataColumn("Title"), new DataColumn("Author"), new DataColumn("PublishDateShort",typeof(string)) });
            table.Rows.Add(new object[] { 1, "Ashu", "1", DateTime.Now.ToShortDateString() });
            table.Rows.Add(new object[] { 2, "Manish", "2", DateTime.Now.ToShortDateString() });
            table.Rows.Add(new object[] { 3, "Swetu", "3", DateTime.Now.ToShortDateString() });
            table.Rows.Add(new object[] { 4, "Sachin", "4", DateTime.Now.ToShortDateString() });
            return table;

        }

    }
}