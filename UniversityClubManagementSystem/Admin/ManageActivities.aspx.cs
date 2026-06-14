using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class ManageActivities : Page
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
                BindGrid();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            BindGrid();
        }

        protected void gvActivities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteActivity")
            {
                try
                {
                    int activityId = Convert.ToInt32(e.CommandArgument);
                    ACTIVITY activityModel = new ACTIVITY();

                    if (activityModel.Delete(activityId))
                    {
                        ShowMessage("Activity deleted successfully.", true);
                    }
                    else
                    {
                        ShowMessage("Unable to delete activity.", false);
                    }

                    BindGrid();
                }
                catch (Exception)
                {
                    ShowMessage("An error occurred while deleting the activity.", false);
                }
            }
        }

        private void BindGrid()
        {
            try
            {
                ACTIVITY activityModel = new ACTIVITY();
                var activities = activityModel.GetAllActivities();
                string search = txtSearch.Text.Trim();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    activities = activities.Where(a =>
                        a.ActivityTitle.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (a.ClubName != null && a.ClubName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        a.Location.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                    ).ToList();
                }

                gvActivities.DataSource = activities;
                gvActivities.DataBind();
            }
            catch (Exception)
            {
                ShowMessage("Unable to load activities. Please try again later.", false);
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
