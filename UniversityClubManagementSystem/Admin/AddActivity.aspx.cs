using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class AddActivity : Page
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
                LoadClubs();
            }
        }

        private void LoadClubs()
        {
            try
            {
                CLUB clubModel = new CLUB();
                ddlClub.DataSource = clubModel.GetActiveClubs();
                ddlClub.DataTextField = "ClubName";
                ddlClub.DataValueField = "ClubId";
                ddlClub.DataBind();
                ddlClub.Items.Insert(0, new ListItem("-- Select Club --", ""));
            }
            catch (Exception)
            {
                ShowMessage("Unable to load clubs.", false);
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

                if (!DateTime.TryParse(txtActivityDate.Text, out DateTime activityDate))
                {
                    ShowMessage("Please enter a valid activity date and time.", false);
                    return;
                }

                ACTIVITY activityModel = new ACTIVITY();
                bool success = activityModel.Create(
                    Convert.ToInt32(ddlClub.SelectedValue),
                    txtTitle.Text.Trim(),
                    txtDescription.Text.Trim(),
                    activityDate,
                    txtLocation.Text.Trim()
                );

                if (success)
                {
                    Response.Redirect("~/Admin/ManageActivities.aspx");
                }
                else
                {
                    ShowMessage("Unable to create activity. Please try again.", false);
                }
            }
            catch (Exception)
            {
                ShowMessage("An error occurred while creating the activity.", false);
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
