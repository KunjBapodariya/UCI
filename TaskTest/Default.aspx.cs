using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.Common;
using System.Linq.Expressions;

namespace Task_17_01
{

    public partial class _Default : Page
    {
        public string _StrCon_Window_Auth = System.Configuration.ConfigurationManager.ConnectionStrings["ConStrWin"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Name"] != null)
                {
                    lblSessionName.Text = "Name:  " + Session["Name"].ToString();
                }
                if (Session["Gender"] != null)
                {
                    lblSessionGender.Text = " Gender: " + Session["Gender"].ToString();
                }
                if (Session["Email"] != null)
                {
                    lblSessionEmail.Text = " Email: " + Session["Email"].ToString();
                }
                BindGridView();

                /*try
                {
                    if (Request.Cookies["Name"].Value != null)
                    {
                        lblSessionName.Text = "Name: " + Request.Cookies["Name"].Value;
                    }
                    if (Request.Cookies["Email"].Value != null)
                    { 
                        lblSessionEmail.Text = " Email:" + Request.Cookies["Email"].Value;
                    }
                    if (Request.Cookies["Gender"].Value != null)
                    {
                        lblSessionName.Text = "Gender: " + Request.Cookies["Gender"].Value;
                    }
                }
                catch (NullReferenceException ex)
                {
                    Response.Write("Cookies Not Set !");
                }*/
            }
            
        }

        protected void BindGridView()
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlDataAdapter adapter = new SqlDataAdapter("showinfo", _Con);
            DataTable dataTable = new DataTable();

            SqlDataReader rd = null;
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                adapter.Fill(dataTable);

                gvInfo.DataSource = dataTable;
                gvInfo.DataBind();
            }
            catch (SqlException Ex)
            {
                Response.Write(Ex.Message);
            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    _Con.Close();
            }
        }

        protected void gvInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvInfo.EditIndex = -1;
            BindGridView();
        }

        protected void gvInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = int.Parse(gvInfo.DataKeys[e.RowIndex].Value.ToString());
            deleteStudentData(id);
            BindGridView();
        }

        protected void gvInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvInfo.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void gvInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = int.Parse(gvInfo.DataKeys[e.RowIndex].Value.ToString());
            TextBox txtName = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtName");
            TextBox txtGender = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtGender");
            TextBox txtDob = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtDob");
            TextBox txtAddress1 = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtAddress1");
            TextBox txtPhone1 = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtPhone1");
            TextBox txtEmail = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtEmail");

            updateStudentData(id, txtName.Text, txtGender.Text, txtDob.Text, txtAddress1.Text, txtPhone1.Text, txtEmail.Text);
            gvInfo.EditIndex = -1;
            BindGridView();
        }

        protected void updateStudentData(int id, string name, string gender, string date, string address1, string phone1, string email)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand _cmd = new SqlCommand("updatewithoutpass", _Con);
            _cmd.Parameters.AddWithValue("Name", name);
            _cmd.Parameters.AddWithValue("Gender", gender);
            _cmd.Parameters.AddWithValue("Date", date);
            _cmd.Parameters.AddWithValue("Address1", address1);
            _cmd.Parameters.AddWithValue("Phone1", phone1);
            _cmd.Parameters.AddWithValue("Email", email);
            _cmd.Parameters.AddWithValue("Id", id);
            _cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                _cmd.ExecuteNonQuery();
            }
            catch (SqlException Ex)
            {
            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    _Con.Close();
            }
        }

        protected void deleteStudentData(int id)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand _cmd = new SqlCommand("deleteStudentData", _Con);
            _cmd.Parameters.AddWithValue("Id", id);
            _cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                _cmd.ExecuteNonQuery();
            }
            catch (SqlException Ex)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    _Con.Close();
            }
        }
    }
}
