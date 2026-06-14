using System;
using System.Collections.Generic;
using System.Web.UI;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class Activities : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadActivities();
                }
            }
            catch (Exception)
            {
                ShowError("Unable to load activities. Please try again.");
            }
        }

        private void LoadActivities()
        {
            ACTIVITY activityModel = new ACTIVITY();
            List<ACTIVITY> activities = activityModel.GetUpcomingActivities();

            gvActivities.DataSource = activities;
            gvActivities.DataBind();
        }

        private void ShowError(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }
    }
}
