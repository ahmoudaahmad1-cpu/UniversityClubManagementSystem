using System;
using System.Collections.Generic;
using System.Web.UI;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["StudentId"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                if (!IsPostBack)
                {
                    LoadDashboardStats();
                }
            }
            catch (Exception)
            {
                ShowError("Unable to load your dashboard. Please try again.");
            }
        }

        private void LoadDashboardStats()
        {
            int studentId = Convert.ToInt32(Session["StudentId"]);
            STUDENT studentModel = new STUDENT();
            MEMBERSHIP membershipModel = new MEMBERSHIP();
            ACTIVITY activityModel = new ACTIVITY();

            STUDENT student = studentModel.GetById(studentId);
            if (student != null)
            {
                lblStudentName.Text = student.FullName;
            }
            else
            {
                lblStudentName.Text = "Student";
            }

            List<MEMBERSHIP> memberships = membershipModel.GetMembershipsByStudent(studentId);
            lblMembershipCount.Text = memberships.Count.ToString();

            int approvedCount = 0;
            foreach (MEMBERSHIP membership in memberships)
            {
                if (membership.Status == "Approved")
                {
                    approvedCount++;
                }
            }
            lblApprovedCount.Text = approvedCount.ToString();

            List<ACTIVITY> activities = activityModel.GetUpcomingActivities();
            lblActivityCount.Text = activities.Count.ToString();
        }

        private void ShowError(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }
    }
}
