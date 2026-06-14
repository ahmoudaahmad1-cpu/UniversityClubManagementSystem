using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class ManageMemberships : Page
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

        protected void gvMemberships_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int membershipId = Convert.ToInt32(e.CommandArgument);
                MEMBERSHIP membershipModel = new MEMBERSHIP();

                if (e.CommandName == "Approve")
                {
                    if (membershipModel.UpdateStatus(membershipId, "Approved"))
                    {
                        ShowMessage("Membership approved successfully.", true);
                    }
                    else
                    {
                        ShowMessage("Unable to approve membership.", false);
                    }
                }
                else if (e.CommandName == "Reject")
                {
                    if (membershipModel.UpdateStatus(membershipId, "Rejected"))
                    {
                        ShowMessage("Membership rejected successfully.", true);
                    }
                    else
                    {
                        ShowMessage("Unable to reject membership.", false);
                    }
                }

                BindGrid();
            }
            catch (Exception)
            {
                ShowMessage("An error occurred while updating membership status.", false);
            }
        }

        protected string GetStatusBadgeClass(string status)
        {
            switch (status)
            {
                case "Approved":
                    return "badge bg-success";
                case "Rejected":
                    return "badge bg-danger";
                case "Pending":
                    return "badge bg-warning text-dark";
                default:
                    return "badge bg-secondary";
            }
        }

        private void BindGrid()
        {
            try
            {
                MEMBERSHIP membershipModel = new MEMBERSHIP();
                var memberships = membershipModel.GetAllMemberships();
                string search = txtSearch.Text.Trim();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    memberships = memberships.Where(m =>
                        (m.StudentName != null && m.StudentName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (m.ClubName != null && m.ClubName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (m.Status != null && m.Status.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    ).ToList();
                }

                gvMemberships.DataSource = memberships;
                gvMemberships.DataBind();
            }
            catch (Exception)
            {
                ShowMessage("Unable to load memberships. Please try again later.", false);
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
