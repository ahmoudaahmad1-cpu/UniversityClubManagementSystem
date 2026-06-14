using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class MyMemberships : Page
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
                    LoadMemberships();
                }
            }
            catch (Exception)
            {
                ShowError("Unable to load memberships. Please try again.");
            }
        }

        protected void gvMemberships_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "CancelRequest")
                {
                    int membershipId = Convert.ToInt32(e.CommandArgument);
                    int studentId = Convert.ToInt32(Session["StudentId"]);

                    MEMBERSHIP membershipModel = new MEMBERSHIP();
                    bool success = membershipModel.CancelRequest(membershipId, studentId);

                    if (!success)
                    {
                        ShowError("Unable to cancel request. It may no longer be pending.");
                        return;
                    }

                    lblSuccess.Text = "Pending request cancelled successfully.";
                    lblSuccess.Visible = true;
                    lblMessage.Visible = false;
                    LoadMemberships();
                }
            }
            catch (Exception)
            {
                ShowError("Unable to cancel request. Please try again.");
            }
        }

        protected string GetStatusBadgeClass(string status)
        {
            if (status == "Approved")
            {
                return "badge bg-success";
            }
            if (status == "Pending")
            {
                return "badge bg-warning text-dark";
            }
            return "badge bg-danger";
        }

        private void LoadMemberships()
        {
            int studentId = Convert.ToInt32(Session["StudentId"]);
            MEMBERSHIP membershipModel = new MEMBERSHIP();
            List<MEMBERSHIP> memberships = membershipModel.GetMembershipsByStudent(studentId);

            gvMemberships.DataSource = memberships;
            gvMemberships.DataBind();
        }

        private void ShowError(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
            lblSuccess.Visible = false;
        }
    }
}
