using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace GridviewManipulationsDemo
{
    public partial class DisconnectedModel_ManipulationsDemo : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet empDS, jobsDS;
        DataRow row;

        /// <summary>
        /// Creates a Connection with the Database
        /// </summary>
        /// <returns></returns>
        static SqlConnection GetConnection()
        {
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DB08H92Constr"].ConnectionString;
            try
            {
                if (!string.IsNullOrEmpty(conStr))
                {
                    return new SqlConnection(conStr);
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Gets the Unique jobs list from the database
        /// </summary>
        void LoadJobs()
        {
            cn = GetConnection();
            cmd = new SqlCommand("usp_GetJobs", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            jobsDS = new DataSet();
            da.Fill(jobsDS, "Jobs");
            ddlJob.Items.Clear();
            ddlJob.DataSource = jobsDS.Tables[0];
            ddlJob.DataTextField = "Designation";
            ddlJob.DataValueField = "Designation";
            ddlJob.DataBind();
            ddlJob.Items.Add("Others");          
        }

        void LoadEmpDetails()
        {
            cn = GetConnection();
            //select command
            da = new SqlDataAdapter("select * from tblEmployeeDetails", cn);
            empDS = new DataSet();
            da.Fill(empDS, "Employee");
            //add the primary key constraint
            empDS.Tables[0].Constraints.Add("pk_empno", empDS.Tables[0].Columns[0], true);
            /*  CommandBuilder object builds the commands for data adapter object based on the primary key of a table. 
                If the table is defined with a primary key, then all the commands will be generated, 
                else only InsertCommand will be generated.*/    
            SqlCommandBuilder cmb = new SqlCommandBuilder(da);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadJobs();

            LoadEmpDetails();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (empDS.Tables[0].Rows.Contains(txtEmpID.Text))
            {
                row=empDS.Tables[0].Rows.Find(txtEmpID.Text);
                txtName.Text=row[1].ToString();
                txtSalary.Text=row[2].ToString();
                ddlJob.SelectedValue=row[3].ToString();
            }
            else
                lblStatus.Text = "<font color='red'>No such employee existed</font>";
        }
        
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            row = empDS.Tables[0].NewRow();
            int lastRowIdx = empDS.Tables[0].Rows.Count - 1;
            int newEmpNo=((int)empDS.Tables[0].Rows[lastRowIdx][0] + 1);
            row[0] =  newEmpNo;
            row[1] = txtName.Text;
            row[2] = txtSalary.Text;
            if (ddlJob.SelectedValue == "Others")
                row[3] = txtJob.Text;
            else
                row[3] = ddlJob.SelectedValue;
            empDS.Tables[0].Rows.Add(row);
            txtJob.Visible = false;
            da.Update(empDS, "Employee");
            lblStatus.Text = "<font color='green'>Details are saved successfully with the ID: <b> " + newEmpNo.ToString() + "</b></font>";
            LoadJobs();
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            row = empDS.Tables[0].Rows.Find(txtEmpID.Text);
            row.BeginEdit();
            row[1] = txtName.Text;
            row[2] = txtSalary.Text;
            if (ddlJob.SelectedValue == "Others")
                row[3] = txtJob.Text;
            else
                row[3] = ddlJob.SelectedValue;
            row.EndEdit();
            txtJob.Visible = false;
            da.Update(empDS, "Employee");
            lblStatus.Text = "<font color='green'>Details are modified successfully for the employee: <b> " + txtEmpID.Text + "</b></font>"; 
            LoadJobs();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            empDS.Tables[0].Rows.Find(txtEmpID.Text).Delete();
            da.Update(empDS, "Employee");
            lblStatus.Text = "<font color='green'> " + txtEmpID.Text + " employee details are removed successfully" + "</b></font>"; 
            LoadJobs();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtEmpID.Text = txtName.Text = txtSalary.Text = txtJob.Text="";
            ddlJob.SelectedIndex = -1;
            lblStatus.Text = "<font color='Black'> <b> No need to give EmployeeID for adding the details </b> </font>";
        }

        protected void ddlJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlJob.SelectedValue == "Others")
            {
                txtJob.Visible = true;
                txtJob.Focus();
            }
            else
                txtJob.Visible = false;
        }

        
    }
}