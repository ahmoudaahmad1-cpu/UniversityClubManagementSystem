using System;
using System.Web.UI;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class EditClub : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminId"] == null)
            {
                Response.Redirect("~/Admin/AdminLogin.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadClub();
            }
        }

        private void LoadClub()
        {
            try
            {
                if (!int.TryParse(Request.QueryString["id"], out int clubId))
                {
                    ShowMessage("Invalid club ID.", false);
                    return;
                }

                CLUB clubModel = new CLUB();
                CLUB club = clubModel.GetById(clubId);

                if (club == null)
                {
                    ShowMessage("Club not found.", false);
                    return;
                }

                hfClubId.Value = club.ClubId.ToString();
                txtClubName.Text = club.ClubName;
                txtDescription.Text = club.Description;
                txtCategory.Text = club.Category;
                txtPresidentName.Text = club.PresidentName;
                chkIsActive.Checked = club.IsActive;
            }
            catch (Exception)
            {
                ShowMessage("Unable to load club details.", false);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }

                if (!int.TryParse(hfClubId.Value, out int clubId))
                {
                    ShowMessage("Invalid club ID.", false);
                    return;
                }

                CLUB clubModel = new CLUB();
                bool success = clubModel.Update(
                    clubId,
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
                    ShowMessage("Unable to update club. Please try again.", false);
                }
            }
            catch (Exception)
            {
                ShowMessage("An error occurred while updating the club.", false);
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
