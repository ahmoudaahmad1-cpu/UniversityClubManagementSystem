using System;
using System.Web.UI;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class AdminLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["AdminId"] != null)
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
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

                ADMIN adminModel = new ADMIN();
                ADMIN admin = adminModel.Authenticate(txtUsername.Text.Trim(), txtPassword.Text);

                if (admin == null)
                {
                    ShowMessage("Invalid username or password. Please try again.", false);
                    return;
                }

                Session["AdminId"] = admin.AdminId;
                Session["AdminUsername"] = admin.Username;
                Response.Redirect("~/Admin/Dashboard.aspx");
            }
            catch (Exception)
            {
                ShowMessage("An error occurred during login. Please try again later.", false);
            }
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = isSuccess ? "alert alert-success d-block" : "alert alert-danger d-block";
            lblMessage.Visible = true;
        }
    }
}
