using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class ManageStudents : Page
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

        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ToggleBlock")
            {
                try
                {
                    int studentId = Convert.ToInt32(e.CommandArgument);
                    STUDENT studentModel = new STUDENT();

                    if (studentModel.ToggleBlockedStatus(studentId))
                    {
                        ShowMessage("Student status updated successfully.", true);
                    }
                    else
                    {
                        ShowMessage("Unable to update student status.", false);
                    }

                    BindGrid();
                }
                catch (Exception)
                {
                    ShowMessage("An error occurred while updating student status.", false);
                }
            }
        }

        private void BindGrid()
        {
            try
            {
                STUDENT studentModel = new STUDENT();
                gvStudents.DataSource = studentModel.GetAllStudents(txtSearch.Text.Trim());
                gvStudents.DataBind();
            }
            catch (Exception)
            {
                ShowMessage("Unable to load students. Please try again later.", false);
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
