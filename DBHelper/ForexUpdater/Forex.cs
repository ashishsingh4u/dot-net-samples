using System;
using System.Windows.Forms;
using DBHelper;
using System.Data;
using System.Text.RegularExpressions;

namespace ForexUpdater
{
    public partial class Forex : Form
    {
        private Regex _regex;
        private readonly DataAccessRequirements _dataAccessRequirements;
        private const string ConnectionString = "Server=ASHISHKUMAR;Initial Catalog=Ashu;User ID=ashu;Password=ashu";
        public Forex()
        {
            InitializeComponent();
            _regex = new Regex(@"[-+]?[0-9]*\.?[0-9]*");
            _dataAccessRequirements = new DataAccessRequirements()
                                          {
                                              Application = "Test",
                                              ConnectionString = ConnectionString,
                                              Database = "Ashu",
                                              DatabaseType = DatabaseType.MsNativeClient,
                                              User = "ashu",
                                              Password = "ashu",
                                              SpPrefix = string.Empty
                                          };
        }

        private void ButtonUpdateClick(object sender, EventArgs e)
        {
            var dataSet = new DataSet();
            dataSet.ReadXml(@"C:\Projects\Exchange Rates For US Dollar.xml");
            var sqlTransRequest = new SqlTranRequest(_dataAccessRequirements.Database);
            foreach (DataRow row in dataSet.Tables["item"].Rows)
            {
                string[] pair = row["title"].ToString().Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                var sqlRequest = new SqlRequest(_dataAccessRequirements.Database, "ForexRCUD");
                sqlRequest.AddNamedParam("@Function",'C');
                sqlRequest.AddNamedParam("BaseCcy", pair[1]);
                sqlRequest.AddNamedParam("QuoteCcy", pair[0]);
                sqlRequest.AddNamedParam("Rate", double.Parse(_regex.Matches(row["description"].ToString())[14].Value));
                sqlRequest.AddNamedParam("Description", row["description"].ToString());
                sqlRequest.AddNamedParam("Category", row["category"].ToString());
                sqlTransRequest.AddRequest(sqlRequest);
            }
            _dataAccessRequirements.DataAccess.ExecuteSql(_dataAccessRequirements, sqlTransRequest);
        }
    }
}
