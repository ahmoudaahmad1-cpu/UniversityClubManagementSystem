using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class ManageClubs : Page
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

        protected void gvClubs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteClub")
            {
                try
                {
                    int clubId = Convert.ToInt32(e.CommandArgument);
                    CLUB clubModel = new CLUB();

                    if (clubModel.HasRelatedRecords(clubId))
                    {
                        ShowMessage("Cannot delete this club because it has related activities or memberships.", false);
                        return;
                    }

                    if (clubModel.Delete(clubId))
                    {
                        ShowMessage("Club deleted successfully.", true);
                    }
                    else
                    {
                        ShowMessage("Unable to delete club.", false);
                    }

                    BindGrid();
                }
                catch (Exception)
                {
                    ShowMessage("An error occurred while deleting the club.", false);
                }
            }
        }

        private void BindGrid()
        {
            try
            {
                CLUB clubModel = new CLUB();
                var clubs = clubModel.GetAllClubs();
                string search = txtSearch.Text.Trim();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    clubs = clubs.Where(c =>
                        c.ClubName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        c.Category.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        c.PresidentName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                    ).ToList();
                }

                gvClubs.DataSource = clubs;
                gvClubs.DataBind();
            }
            catch (Exception)
            {
                ShowMessage("Unable to load clubs. Please try again later.", false);
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
