using StnwServiceWeb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class samplew02 : System.Web.UI.Page
{
    public DataSet dst;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Bind the GridView with data from the DataSet
            dst = GetData();
            ViewState["UserData"] = dst; // Store the dataset in ViewState
            GridView1.DataSource = dst.Tables["Users"];
            GridView1.DataBind();

            RadioButton1.Checked = true;

            ViewState["SortDirection"] = "ASC";
            ViewState["SortColumn"] = "Id"; 
        }
        else
        {
            // Retrieve the dataset from ViewState on postback
            dst = (DataSet)ViewState["UserData"];
        }

        lblColumn.Text = "Column: Id";
        lblDirection.Text = "Direction: ASC";

    }

    private DataSet GetData()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add(new DataTable("Users"));
        DataTable dt = ds.Tables["Users"];
        dt.Columns.Add("Id", typeof(string));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Town", typeof(string));
        dt.Columns.Add("Country", typeof(string));
        dt.PrimaryKey = new DataColumn[] { dt.Columns["Id"] };

        dt.Rows.Add("ABDEN", "Maria Weiss", "Berlin", "Germany");
        dt.Rows.Add("AXEIS", "Pedro Alvarez", "México D.F.", "Mexico");
        dt.Rows.Add("BENOI", "Anna Tóth", "Szeged", "Hungary");
        dt.Rows.Add("CAZLE", "Jan Eriksson", "Mannheim", "Sweden");
        dt.Rows.Add("DRFOS", "Johan Hofmann", "Lübeck", "Germany");
        return ds;
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // Retrieve the dataset from ViewState
        DataSet dst = (DataSet)ViewState["UserData"];

        // Set the row to edit mode
        GridView1.EditIndex = e.NewEditIndex;
        // Rebind the GridView with data
        GridView1.DataSource = dst.Tables["Users"];
        GridView1.DataBind();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Retrieve the dataset from ViewState
        DataSet dst = (DataSet)ViewState["UserData"];

        // Get the row being edited
        GridViewRow row = GridView1.Rows[e.RowIndex];

        // Find the TextBox controls in the row (assuming the BoundFields have TextBox controls in edit mode)
        TextBox IdTextBox = (TextBox)row.FindControl("TextBox_Id");
        TextBox NameTextBox = (TextBox)row.FindControl("TextBox_Name");
        TextBox townTextBox = (TextBox)row.FindControl("TextBox_Town");
        TextBox CountryTextBox = (TextBox)row.FindControl("TextBox_Country");

        // Check if the TextBox controls are found
        if (NameTextBox != null && townTextBox != null)
        {
            // Get new values from the TextBox controls
            string Id = IdTextBox.Text;
            string Name = NameTextBox.Text;
            string towm = townTextBox.Text;
            string Country = CountryTextBox.Text;

            // Update your DataSet (assuming you update the dataset directly)
            dst.Tables["Users"].Rows[e.RowIndex]["Id"] = Id;
            dst.Tables["Users"].Rows[e.RowIndex]["Name"] = Name;
            dst.Tables["Users"].Rows[e.RowIndex]["Town"] = towm;
            dst.Tables["Users"].Rows[e.RowIndex]["Country"] = Country;
        }

        // Update ViewState after modifying the dataset
        ViewState["UserData"] = dst;

        // Set the GridView back to normal mode
        GridView1.EditIndex = -1;

        // Rebind the updated data
        GridView1.DataSource = dst.Tables["Users"];
        GridView1.DataBind();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        // Cancel edit mode and rebind data
        GridView1.EditIndex = -1;
        GridView1.DataSource = dst.Tables["Users"];
        GridView1.DataBind();
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {

        clsStnwClassWeb tsi = new clsStnwClassWeb();
        tsi.dsRPT = dst;

        tsi.preslAccountCode = "DEMO1";
        tsi.preslUserCode = "0000";

        string binPath = HttpContext.Current.Server.MapPath("~/bin");
        tsi.ReportFullName = System.IO.Path.Combine(binPath, "CustomerReport1.rpt");

        tsi.RPTSortTableIme = "Users";
        if (RadioButton1.Checked)
        {
            tsi.RPTDEST = 0;
        }
        else
        {
            tsi.RPTDEST = 1;
        }

        string sortedColumn = ViewState["SortColumn"] as string;
        string sortedDirection = ViewState["SortDirection"] as string;

        string SORTORD = "1";
        if (sortedDirection == "ASC")
        {
            SORTORD = "1";
        }
        else
        {
            SORTORD = "2";
        }

        tsi.RPTSortField1 = sortedColumn; // Use the sorted column for your report
        tsi.RPTSortDirection = SORTORD; // Store or use the sort direction as needed

        if (CheckBox1.Checked == true)
        {
            tsi.ReportFormula = "{Users.Country} = 'Germany'";
        }
        else
        {
            tsi.ReportFormula = "";
        }

        tsi.ShowWindow(this, HttpContext.Current);
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        // Retrieve the dataset from ViewState
        DataSet dst = (DataSet)ViewState["UserData"];
        DataTable dt = dst.Tables["Users"];

        // Get the current sort expression and direction
        string sortExpression = e.SortExpression;
        string sortDirection = ViewState["SortDirection"] as string;

        // Toggle sort direction
        if (sortDirection == "ASC")
        {
            sortDirection = "DESC";
        }
        else
        {
            sortDirection = "ASC";
        }

        // Sort the DataTable based on the sort expression and direction
        dt.DefaultView.Sort = sortExpression + " " + sortDirection;

        // Rebind the sorted data to the GridView
        GridView1.DataSource = dt;
        GridView1.DataBind();

        // Save the sort direction and column in ViewState
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortColumn"] = sortExpression; // Store the sort column

        // Update labels to show the sorted column and direction
        lblColumn.Text = "Column: " + sortExpression;
        lblDirection.Text = "Direction: " + sortDirection;
    }

    //The simplest example for displaying a Crystal Report from a web page.
}