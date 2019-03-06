using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace CRUD
{
    public partial class Contact : System.Web.UI.Page
    {
        //string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CRUD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CRUD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDelete.Enabled = false;
                fillgridview();
            }
            
        }

        protected void btnSave_click(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("ContactCP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ContactID", (hfContactID.Value == "" ? 0 : Convert.ToInt32(hfContactID.Value)));
            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.ExecuteNonQuery();
            string contactID = hfContactID.Value;
            con.Close();
            Clear();
            if (contactID == "")
            {
                lblSuccess.Text = "Saved Successfully";
            }
            else
            {
                lblSuccess.Text = "Updated Successfully";
            }
        }

        protected void btnDelete_click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("DeleteSP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ContactID", Convert.ToInt32(hfContactID.Value));
            cmd.ExecuteNonQuery();
            con.Close();
            Clear();
            fillgridview();
            lblSuccess.Text = "Deleted Successfully";
        }

        protected void btnClear_click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            hfContactID.Value = "";
            txtName.Text = "";
            txtMobileNo.Text = "";
            txtAddress.Text = "";
            lblSuccess.Text = "";
            lblError.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
        }

        void fillgridview()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter sda = new SqlDataAdapter("SelectSP",con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            gvContact.DataSource = dt;
            gvContact.DataBind();
        }

        protected void lnk_OnClick(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter sda = new SqlDataAdapter("ContactViewSP", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@ContactID", contactID);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            hfContactID.Value = contactID.ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            btnSave.Text = "Update";
            btnDelete.Enabled = true;
        }
    }
}