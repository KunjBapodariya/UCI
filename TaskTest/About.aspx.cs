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
    public partial class About : Page
    {
        List<string> names = new List<string>();
        List<string> emails = new List<string>();
        public string _StrCon_Window_Auth = System.Configuration.ConfigurationManager.ConnectionStrings["ConStrWin"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cascadingDropdown();
            }
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand cmd = new SqlCommand("showinfo", _Con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader rd = null;

            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                rd = cmd.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                    names.Add(Convert.ToString(rd["Name"]));
                    emails.Add(Convert.ToString(rd["Email"]));
                }
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand _cmd = new SqlCommand("insertinto", _Con);
            SqlCommand _cmd_add = new SqlCommand("insertaddress", _Con);
            SqlCommand _cmd_ph = new SqlCommand("insertphone", _Con);
            _cmd.Parameters.AddWithValue("Name", Convert.ToString(txtStudentName.Text));
            _cmd.Parameters.AddWithValue("Gender", Convert.ToString(txtStudentGender.Text));
            _cmd.Parameters.AddWithValue("Date", Convert.ToString(txtStudentDob.Text));
            _cmd.Parameters.AddWithValue("Address1", Convert.ToString(txtStudentAddress1.Text));
            _cmd.Parameters.AddWithValue("Phone1", Convert.ToString(txtStudentPhone1.Text));
            _cmd.Parameters.AddWithValue("Email", Convert.ToString(txtStudentEmail.Text));
            _cmd.Parameters.AddWithValue("Password", Convert.ToString(txtStudentPassword.Text));
            Session["Name"] = Convert.ToString(txtStudentName.Text);
            Session["Gender"] = Convert.ToString(txtStudentGender.Text);
            Session["Email"] = Convert.ToString(txtStudentEmail.Text);
            Response.Cookies["Name"].Value = Convert.ToString(txtStudentName.Text);
            Response.Cookies["Email"].Value = Convert.ToString(txtStudentEmail.Text);
            Response.Cookies["Gender"].Value = Convert.ToString(txtStudentGender.Text);
            _cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }

                bool flag = true;
                for (int i = 0; i < names.Count; i++)
                {
                    if (names[i] == Convert.ToString(txtStudentName.Text) || emails[i] == Convert.ToString(txtStudentEmail.Text))
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    int id_for_table = (int)_cmd.ExecuteScalar();

                    _cmd_add.Parameters.Add("Id", SqlDbType.Int).Value = id_for_table;
                    _cmd_add.Parameters.Add("Address", SqlDbType.VarChar).Value = Convert.ToString(txtStudentAddress2.Text);

                    _cmd_ph.Parameters.Add("Id", SqlDbType.Int).Value = id_for_table;
                    _cmd_ph.Parameters.Add("Phone", SqlDbType.VarChar).Value = Convert.ToString(txtStudentPhone2.Text);

                    _cmd_add.CommandType = CommandType.StoredProcedure;
                    _cmd_ph.CommandType = CommandType.StoredProcedure;

                    _cmd_add.ExecuteNonQuery();
                    _cmd_ph.ExecuteNonQuery();
                    Response.Write($"{id_for_table} Records Inserted Successfully");
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    Response.Write("Name or Email already taken!");
                }
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

        protected void cascadingDropdown()
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from countries", _Con);
                ddlCountries.DataSource = cmd.ExecuteReader();
                ddlCountries.DataTextField = "CountryName";
                ddlCountries.DataValueField = "CId";
                ddlCountries.DataBind();
                ddlCountries.Items.Insert(0, new ListItem("---Select Country---", "0"));

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                {
                    _Con.Close();
                }
            }
        }

        protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            int countryId = Convert.ToInt32(ddlCountries.SelectedValue);
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from states where cid = " + countryId, _Con);
                ddlStates.DataSource = cmd.ExecuteReader();
                ddlStates.DataTextField = "StateName";
                ddlStates.DataValueField = "SId";
                ddlStates.DataBind();
                ddlStates.Items.Insert(0, new ListItem("---Select State  ---", "0"));

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                {
                    _Con.Close();
                }
            }
        }

        protected void ddlStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            int stateId = Convert.ToInt32(ddlStates.SelectedValue);
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from city where sid = " + stateId, _Con);
                ddlDistricts.DataSource = cmd.ExecuteReader();
                ddlDistricts.DataTextField = "CityName";
                ddlDistricts.DataValueField = "CyId";
                ddlDistricts.DataBind();
                ddlDistricts.Items.Insert(0, new ListItem("---Select City  ---", "0"));

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                {
                    _Con.Close();
                }
            }
        }

        protected void RepeatInformation_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            if (e.CommandName == "update")
            {
                Response.Write("Update call");
            }
            if (e.CommandName == "delete")
            {
                Response.Write("Delete call");
            }
        }

    }
}