using System;
using System.Collections.Generic;
using System.Web.UI;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class ClubDetails : Page
    {
        private int ClubId
        {
            get
            {
                int clubId;
                int.TryParse(Request.QueryString["id"], out clubId);
                return clubId;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadClubDetails();
                }
            }
            catch (Exception)
            {
                ShowError("Unable to load club details. Please try again.");
            }
        }

        protected void btnJoin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["StudentId"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                int studentId = Convert.ToInt32(Session["StudentId"]);
                MEMBERSHIP membershipModel = new MEMBERSHIP();

                bool success = membershipModel.RequestJoin(studentId, ClubId);
                if (!success)
                {
                    ShowError("Unable to submit join request. You may already have a pending or approved membership.");
                    return;
                }

                lblSuccess.Text = "Join request submitted successfully. Status: Pending.";
                lblSuccess.Visible = true;
                lblMessage.Visible = false;
                LoadMembershipStatus(studentId);
            }
            catch (Exception)
            {
                ShowError("Unable to submit join request. Please try again.");
            }
        }

        private void LoadClubDetails()
        {
            if (ClubId <= 0)
            {
                pnlClubInfo.Visible = false;
                pnlNotFound.Visible = true;
                return;
            }

            CLUB clubModel = new CLUB();
            ACTIVITY activityModel = new ACTIVITY();

            CLUB club = clubModel.GetById(ClubId);
            if (club == null || !club.IsActive)
            {
                pnlClubInfo.Visible = false;
                pnlNotFound.Visible = true;
                return;
            }

            List<CLUB> activeClubs = clubModel.GetActiveClubs();
            foreach (CLUB activeClub in activeClubs)
            {
                if (activeClub.ClubId == ClubId)
                {
                    club.MemberCount = activeClub.MemberCount;
                    break;
                }
            }

            pnlClubInfo.Visible = true;
            pnlNotFound.Visible = false;

            lblClubName.Text = club.ClubName;
            lblCategory.Text = club.Category;
            lblPresident.Text = club.PresidentName;
            lblDescription.Text = club.Description;
            lblMemberCount.Text = club.MemberCount.ToString();

            List<ACTIVITY> activities = activityModel.GetActivitiesByClub(ClubId);
            gvActivities.DataSource = activities;
            gvActivities.DataBind();

            if (Session["StudentId"] != null)
            {
                int studentId = Convert.ToInt32(Session["StudentId"]);
                LoadMembershipStatus(studentId);
            }
            else
            {
                btnJoin.Visible = true;
            }
        }

        private void LoadMembershipStatus(int studentId)
        {
            MEMBERSHIP membershipModel = new MEMBERSHIP();
            string status = membershipModel.GetStudentMembershipStatus(studentId, ClubId);

            if (string.IsNullOrEmpty(status))
            {
                lblMembershipStatus.Visible = false;
                btnJoin.Visible = true;
                return;
            }

            lblMembershipStatus.Text = status;
            lblMembershipStatus.Visible = true;
            btnJoin.Visible = false;

            if (status == "Approved")
            {
                lblMembershipStatus.CssClass = "badge bg-success fs-6";
            }
            else if (status == "Pending")
            {
                lblMembershipStatus.CssClass = "badge bg-warning text-dark fs-6";
            }
            else
            {
                lblMembershipStatus.CssClass = "badge bg-danger fs-6";
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
