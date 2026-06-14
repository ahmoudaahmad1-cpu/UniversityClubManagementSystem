using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class EditActivity : Page
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
                LoadActivity();
            }
        }

        private void LoadClubs()
        {
            try
            {
                CLUB clubModel = new CLUB();
                ddlClub.DataSource = clubModel.GetAllClubs();
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

        private void LoadActivity()
        {
            try
            {
                if (!int.TryParse(Request.QueryString["id"], out int activityId))
                {
                    ShowMessage("Invalid activity ID.", false);
                    return;
                }

                ACTIVITY activityModel = new ACTIVITY();
                ACTIVITY activity = activityModel.GetById(activityId);

                if (activity == null)
                {
                    ShowMessage("Activity not found.", false);
                    return;
                }

                hfActivityId.Value = activity.ActivityId.ToString();
                ddlClub.SelectedValue = activity.ClubId.ToString();
                txtTitle.Text = activity.ActivityTitle;
                txtDescription.Text = activity.Description;
                txtActivityDate.Text = activity.ActivityDate.ToString("yyyy-MM-ddTHH:mm");
                txtLocation.Text = activity.Location;
            }
            catch (Exception)
            {
                ShowMessage("Unable to load activity details.", false);
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

                if (!int.TryParse(hfActivityId.Value, out int activityId))
                {
                    ShowMessage("Invalid activity ID.", false);
                    return;
                }

                if (!DateTime.TryParse(txtActivityDate.Text, out DateTime activityDate))
                {
                    ShowMessage("Please enter a valid activity date and time.", false);
                    return;
                }

                ACTIVITY activityModel = new ACTIVITY();
                bool success = activityModel.Update(
                    activityId,
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
                    ShowMessage("Unable to update activity. Please try again.", false);
                }
            }
            catch (Exception)
            {
                ShowMessage("An error occurred while updating the activity.", false);
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
