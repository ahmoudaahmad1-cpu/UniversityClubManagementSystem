using System;
using System.Web.UI;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class StudentProfile : Page
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
                    LoadProfile();
                }
            }
            catch (Exception)
            {
                ShowError("Unable to load your profile. Please try again.");
            }
        }

        private void LoadProfile()
        {
            int studentId = Convert.ToInt32(Session["StudentId"]);
            STUDENT studentModel = new STUDENT();
            STUDENT student = studentModel.GetById(studentId);

            if (student == null)
            {
                ShowError("Student profile not found.");
                return;
            }

            txtFullName.Text = student.FullName;
            txtEmail.Text = student.Email;
            txtUsername.Text = student.Username;
            txtPhone.Text = student.Phone;
            txtMajor.Text = student.Major;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }

                int studentId = Convert.ToInt32(Session["StudentId"]);
                STUDENT studentModel = new STUDENT();

                bool success = studentModel.UpdateProfile(
                    studentId,
                    txtFullName.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtPhone.Text.Trim(),
                    txtMajor.Text.Trim(),
                    txtUsername.Text.Trim());

                if (!success)
                {
                    ShowError("Update failed. Username or email may already be in use.");
                    return;
                }

                Session["StudentName"] = txtFullName.Text.Trim();
                lblSuccess.Text = "Profile updated successfully.";
                lblSuccess.Visible = true;
                lblMessage.Visible = false;
            }
            catch (Exception)
            {
                ShowError("Unable to update profile. Please try again.");
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
