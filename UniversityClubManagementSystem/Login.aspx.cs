using System;
using System.Web.UI;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack && Session["StudentId"] != null)
                {
                    Response.Redirect("~/Home.aspx");
                }
            }
            catch (Exception)
            {
                ShowError("Unable to load the login page. Please try again.");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }

                STUDENT studentModel = new STUDENT();
                STUDENT student = studentModel.Authenticate(txtUsername.Text.Trim(), txtPassword.Text);

                if (student == null)
                {
                    ShowError("Invalid username or password, or your account may be blocked.");
                    return;
                }

                Session["StudentId"] = student.StudentId;
                Session["StudentName"] = student.FullName;
                Response.Redirect("~/Home.aspx");
            }
            catch (Exception)
            {
                ShowError("Login failed. Please check your connection and try again.");
            }
        }

        private void ShowError(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }
    }
}
