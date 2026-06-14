using System;
using System.Web.UI;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class Dashboard : Page
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
                LoadStats();
            }
        }

        private void LoadStats()
        {
            try
            {
                STUDENT studentModel = new STUDENT();
                CLUB clubModel = new CLUB();
                ACTIVITY activityModel = new ACTIVITY();
                MEMBERSHIP membershipModel = new MEMBERSHIP();

                lblStudentCount.Text = studentModel.GetTotalStudentsCount().ToString();
                lblClubCount.Text = clubModel.GetTotalClubsCount().ToString();
                lblActivityCount.Text = activityModel.GetTotalActivitiesCount().ToString();
                lblPendingCount.Text = membershipModel.GetPendingCount().ToString();
            }
            catch (Exception)
            {
                lblMessage.Text = "Unable to load dashboard statistics. Please try again later.";
                lblMessage.Visible = true;
            }
        }
    }
}
