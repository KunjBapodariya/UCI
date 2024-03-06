using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Task_17_01
{
    public partial class Contact : Page
    {
        public string _StrCon_Window_Auth = System.Configuration.ConfigurationManager.ConnectionStrings["ConStrWin"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand cmd = new SqlCommand("showsearch", _Con);
            cmd.Parameters.Add("Name", SqlDbType.VarChar).Value = Convert.ToString(txtSearchStudentName.Text);

            cmd.CommandType = CommandType.StoredProcedure;

            if (txtSearchStudentName.Text == "")
            {
                txtSearchStudentName.Text = "";
            }

            SqlDataReader rd = null;

            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                rd = cmd.ExecuteReader();


                lblNameData.Text = "Name";
                lblGender.Text = "Gender";
                lblDob.Text = "Date";
                lblAddress1.Text = "Address1";
                lblPhone1.Text = "Phone1";
                lblEmail.Text = "Email";

                while (rd.Read())
                {
                    lblNameData.Text += "<br/>" + Convert.ToString(rd["Name"]);
                    lblGender.Text += "<br/>" + Convert.ToString(rd["Gender"]);
                    lblDob.Text += "<br/>" + Convert.ToString(rd["Date"]);
                    lblAddress1.Text += "<br/>" + Convert.ToString(rd["Address1"]);
                    lblPhone1.Text += "<br/>" + Convert.ToString(rd["Phone1"]);
                    lblEmail.Text += "<br/>" + Convert.ToString(rd["Email"]);
                    txtDataId.Text = Convert.ToString(rd["Id"]);
                    txtDataName.Text = Convert.ToString(rd["Name"]);
                }
                txtSearchStudentName.Text = "";
                txtSearchStudentName.Text = "";
            }
            catch (SqlException se)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    _Con.Close();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand cmd = new SqlCommand("updateqr", _Con);
            cmd.Parameters.AddWithValue("Id", Convert.ToInt32(txtDataId.Text));
            cmd.Parameters.AddWithValue("Name", Convert.ToString(txtUpdateStudentName.Text));
            cmd.Parameters.AddWithValue("Gender", Convert.ToString(txtUpdateStudentGender.Text));
            cmd.Parameters.AddWithValue("Date", Convert.ToDateTime(txtUpdateStudentDob.Text));
            cmd.Parameters.AddWithValue("Address1", Convert.ToString(txtUpdateStudentAddress1.Text));
            cmd.Parameters.AddWithValue("Address2", Convert.ToString(txtUpdateStudentAddress2.Text));
            cmd.Parameters.AddWithValue("Phone1", Convert.ToInt64(txtUpdateStudentPhone1.Text));
            cmd.Parameters.AddWithValue("Phone2", Convert.ToInt64(txtUpdateStudentPhone2.Text));
            cmd.Parameters.AddWithValue("Email", Convert.ToString(txtUpdateStudentEmail.Text));
            cmd.Parameters.AddWithValue("Password", Convert.ToString(txtUpdateStudentPassword.Text));

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                if (_Con.State == ConnectionState.Closed)
                {

                    _Con.Open();
                }
                Response.Write("Data updated");
                cmd.ExecuteNonQuery();
            }
            catch (SqlException se)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    _Con.Close();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand cmd = new SqlCommand("deleteStudentData", _Con);
            cmd.Parameters.AddWithValue("Id", Convert.ToInt64(txtDataId.Text));

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (SqlException se)
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