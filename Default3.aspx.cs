using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{
    SqlCommand cmd;
    SqlConnection con;
    SqlDataAdapter sda;
    DataSet ds;
    SqlCommandBuilder scb;
    private object row;
    string conString;

    protected void Page_Load(object sender, EventArgs e)
    {      
        conString = @"Data Source=inchnilpdb02\mssqlserver1;Initial Catalog=CHN12_MMS73_TEST;Integrated Security=False;User ID=mms73user;Password=mms73user;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        con = new SqlConnection(conString);
        sda = new SqlDataAdapter();
        ds = new DataSet();
    }

    protected void myButton_Click(object sender, EventArgs e)
    {
        string selectCmd = "select * from employeee where id = '"+emp_textbox.Text+"'";
        cmd = new SqlCommand(selectCmd, con);
        sda.SelectCommand = cmd;
        sda.Fill(ds, "emp");
        myGrid.DataSource = ds.Tables[0];
        myGrid.DataBind();

    }

    protected void myGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string selectCmd = "select * from employeee where id = '"+emp_textbox.Text+"'";
        cmd = new SqlCommand(selectCmd, con);
        sda.SelectCommand = cmd;
        sda.Fill(ds, "emp");
        myGrid.DataBind();
        string deleteCmd = "delete from employeee where id = '"+emp_textbox.Text+"'";
        cmd = new SqlCommand(deleteCmd, con);
        scb = new SqlCommandBuilder(sda);
        foreach ( DataRow row in ds.Tables[0].Rows)
        {
            if(row[0].ToString().Equals(emp_textbox.Text))
            {
                row.Delete();
            }
        } 

        sda.Update(ds, "emp");
          myGrid.DataBind();


    }

    protected void myGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
        System.Diagnostics.Debug.WriteLine("SomeText 1 ");
        string selectCmd = "select * from employeee where id = '" + emp_textbox.Text + "'";
        cmd = new SqlCommand(selectCmd, con);
        sda.SelectCommand = cmd;
        sda.Fill(ds, "emp");
        scb = new SqlCommandBuilder(sda);

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            if (row["id"].ToString() == emp_textbox.Text)
            {
                row["username"] = ((TextBox)myGrid.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
                row["password"] = ((TextBox)myGrid.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
                row["email"] = ((TextBox)myGrid.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            }
        }
        sda.Update(ds, "emp");
        myGrid.DataBind();
        
    }

    protected void myGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        
        string selectCmd = "select * from employeee where id = '" + emp_textbox.Text + "'";
        cmd = new SqlCommand(selectCmd, con);
        sda.SelectCommand = cmd;
        sda.Fill(ds, "emp");        
        myGrid.DataBind();
        

    }

    protected void myGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        myGrid.DataBind();
    }
}