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

using ASPNETWebApplication.Repositories;

namespace ASPNETWebApplication.WebShop
{
    /// <summary>
    /// Product search page.
    /// </summary>
    public partial class Search : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Meta data: helpful for SEO (search engine optimization)
            Page.Title = "Search Products";
            Page.MetaKeywords = "Search, Electronic Products";
            Page.MetaDescription = "Search for Electronic Products at Patterns in Action"; 

            if (!IsPostBack)
            {
                // Set the selected menu item in Master page.
                SelectedMenu = "search";

                // Set default button on page
                Form.DefaultButton = this.ButtonSearch.UniqueID;

                // Default sort order
                SortColumn = "ProductId";
                SortDirection = "ASC";
            }
        }

        /// <summary>
        /// Takes search criteria, sets datasource, and bind data to controls.
        /// </summary>
        private void Bind()
        {
            // Validate price range.
            int priceRangeId;
            if (!int.TryParse(DropDownListRange.SelectedValue, out priceRangeId))
                priceRangeId = 0;

            // Get product name entered.
            string productName = this.TextBoxProductName.Text.Trim();

            // Retrieve list of products.
            var repository = new ProductRepository();
            GridViewProducts.DataSource = repository.SearchProducts(productName, priceRangeId, SortExpression);
            GridViewProducts.DataBind();

            PanelSearchResults.Visible = true;
        }

        /// <summary>
        /// Sets sort order and re-binds page.
        /// </summary>
        protected void GridViewProducts_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            SortColumn = e.SortExpression;

            Bind();
        }

        /// <summary>
        /// Adds glyph to header according to current sort settings.
        /// </summary>
        protected void GridViewProducts_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                AddGlyph(this.GridViewProducts, e.Row);
            }
        }

        /// <summary>
        /// Databinds page with selected search criteria.
        /// </summary>
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}
