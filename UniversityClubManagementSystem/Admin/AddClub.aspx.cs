using System;
using System.Web.UI;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class AddClub : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminId"] == null)
            {
                Response.Redirect("~/Admin/AdminLogin.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }

                CLUB clubModel = new CLUB();
                bool success = clubModel.Create(
                    txtClubName.Text.Trim(),
                    txtDescription.Text.Trim(),
                    txtCategory.Text.Trim(),
                    txtPresidentName.Text.Trim(),
                    chkIsActive.Checked
                );

                if (success)
                {
                    Response.Redirect("~/Admin/ManageClubs.aspx");
                }
                else
                {
                    ShowMessage("Unable to create club. Please try again.", false);
                }
            }
            catch (Exception)
            {
                ShowMessage("An error occurred while creating the club.", false);
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
