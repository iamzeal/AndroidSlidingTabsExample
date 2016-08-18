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
    public partial class GridViewBindingDemo : System.Web.UI.Page
    {

        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet jobsDS, empDS;

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

        void BindEmpData()
        {
            cn = GetConnection();
            da = new SqlDataAdapter("select * from tblEmployeeDetails", cn);
            empDS = new DataSet();
            da.Fill(empDS);
            gvEmpDetails.DataSource = empDS.Tables[0];
            gvEmpDetails.DataBind();
        }


        DataTable LoadJobs()
        {
            cn = GetConnection();
            cmd = new SqlCommand("usp_GetJobs", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            jobsDS = new DataSet();
            da.Fill(jobsDS, "Jobs");
            return jobsDS.Tables[0];
        }


        /// <summary>
        /// Modifies the employee details
        /// </summary>
        /// <param name="empNo"></param>
        /// <param name="name"></param>
        /// <param name="sal"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        bool ModifyEmployee(int empNo, string name, double sal, string job)
        {
            cn = GetConnection();
            if (cn.State == System.Data.ConnectionState.Closed)
            {
                cn.Open();
            }

            cmd = new SqlCommand("usp_ModifyEmployee", cn); cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", empNo));
            cmd.Parameters.Add(new SqlParameter("@name", name));
            cmd.Parameters.Add(new SqlParameter("@sal", sal));
            cmd.Parameters.Add(new SqlParameter("@job", job));
            SqlParameter returnParam = cmd.Parameters.Add("ReturnValue", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            cn.Close();
            int result = (int)returnParam.Value;
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes employee details
        /// </summary>
        /// <param name="empNo"></param>
        /// <returns></returns>
        bool RemoveEmployee(int empNo)
        {
            cn = GetConnection();
            if (cn.State == System.Data.ConnectionState.Closed)
            {
                cn.Open();
            }
            cmd = new SqlCommand("usp_RemoveEmployee", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", empNo));
            SqlParameter returnParam = cmd.Parameters.Add("ReturnValue", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            cn.Close();
            int result = (int)returnParam.Value;
            if (result > 0)
            {
                return true;
            }
            return false;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                BindEmpData();
        }

        protected void gvEmpDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvr = gvEmpDetails.SelectedRow;
            Label lblName = (Label)gvr.FindControl("lbl_IR_Name");
            ClientScript.RegisterStartupScript(this.GetType(), "ShowMsg", "alert('Selected Employee is:" + lblName.Text + "')", true);
        }

        protected void gvEmpDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmpDetails.EditIndex = e.NewEditIndex;
            BindEmpData();
        }

        protected void gvEmpDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmpDetails.EditIndex = -1;
            BindEmpData();
        }

        protected void gvEmpDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&  gvEmpDetails.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlJobs = (DropDownList)e.Row.FindControl("ddl_ER_Job");
                ddlJobs.DataSource = LoadJobs();
                ddlJobs.DataTextField = "Designation";
                ddlJobs.DataValueField = "Designation";
                ddlJobs.DataBind();
                ddlJobs.Items.FindByValue((e.Row.FindControl("lbl_ER_Job") as Label).Text).Selected = true;
            }
        }

        protected void gvEmpDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = gvEmpDetails.Rows[e.RowIndex];
            int empNo;
            string name, job;
            double sal;
            empNo = Int32.Parse(((Label)gvr.FindControl("lbl_ER_Empno")).Text);
            name = ((TextBox)gvr.FindControl("txt_ER_Name")).Text;
            sal = Double.Parse(((TextBox)gvr.FindControl("txt_ER_Salary")).Text);
            job = ((DropDownList)gvr.FindControl("ddl_ER_Job")).SelectedValue;
            bool result = ModifyEmployee(empNo, name, sal, job);
            if (result)
                ClientScript.RegisterClientScriptBlock(this.GetType(), "SuccessMsg", "alert('Details are modified successfully')", true);
            else
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ErrMsg", "alert('Please recheck the details and submit them again')", true);
            gvEmpDetails.EditIndex = -1;
            BindEmpData();
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvEmpDetails.Rows)
            {
                CheckBox chk=(CheckBox)gvr.FindControl("CheckBox1");
                chk.Checked = ((CheckBox)gvEmpDetails.HeaderRow.FindControl("CheckBox2")).Checked;
            }
        }

        protected void gvEmpDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = gvEmpDetails.Rows[e.RowIndex];
            int eno = Int32.Parse(((Label)gvr.FindControl("lbl_IR_Empno")).Text);
            bool s = RemoveEmployee(eno);
            if (s)
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Details are removed successfully')", true);
            else
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Please recheck the details and submit them again')", true);
            BindEmpData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvEmpDetails.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    int eno = Int32.Parse(((Label)gvr.FindControl("lbl_IR_Empno")).Text);
                    bool s = RemoveEmployee(eno);
                }
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('All selected records are removed successfully')", true);
            BindEmpData();
        }
    }
}