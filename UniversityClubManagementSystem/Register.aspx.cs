using System;
using System.Web.UI;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class Register : Page
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
                ShowError("Unable to load the registration page. Please try again.");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }

                STUDENT studentModel = new STUDENT();
                bool success = studentModel.Register(
                    txtFullName.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtUsername.Text.Trim(),
                    txtPassword.Text,
                    txtPhone.Text.Trim(),
                    txtMajor.Text.Trim());

                if (!success)
                {
                    ShowError("Registration failed. Username or email may already be in use.");
                    return;
                }

                lblSuccess.Text = "Registration successful! You can now log in.";
                lblSuccess.Visible = true;
                lblMessage.Visible = false;
                btnRegister.Enabled = false;
            }
            catch (Exception)
            {
                ShowError("Registration failed. Please try again later.");
            }
        }

        private void ShowError(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
            lblSuccess.Visible = false;
        }
    }
}
