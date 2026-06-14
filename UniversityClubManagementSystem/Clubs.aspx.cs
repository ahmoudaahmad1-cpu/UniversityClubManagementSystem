using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityClubManagementSystem.Models;

namespace UniversityClubManagementSystem
{
    public partial class Clubs : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadCategories();
                    LoadClubs();
                }
            }
            catch (Exception)
            {
                ShowError("Unable to load clubs. Please try again.");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadClubs();
            }
            catch (Exception)
            {
                ShowError("Search failed. Please try again.");
            }
        }

        private void LoadCategories()
        {
            CLUB clubModel = new CLUB();
            List<string> categories = clubModel.GetCategories();

            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(new ListItem("All Categories", ""));

            foreach (string category in categories)
            {
                ddlCategory.Items.Add(new ListItem(category, category));
            }
        }

        private void LoadClubs()
        {
            CLUB clubModel = new CLUB();
            List<CLUB> clubs = clubModel.GetActiveClubs();
            List<CLUB> filteredClubs = new List<CLUB>();

            string search = txtSearch.Text.Trim().ToLower();
            string category = ddlCategory.SelectedValue;

            foreach (CLUB club in clubs)
            {
                bool matchesCategory = string.IsNullOrEmpty(category) || club.Category == category;
                bool matchesSearch = string.IsNullOrEmpty(search) ||
                    club.ClubName.ToLower().Contains(search) ||
                    club.Description.ToLower().Contains(search) ||
                    club.Category.ToLower().Contains(search);

                if (matchesCategory && matchesSearch)
                {
                    filteredClubs.Add(club);
                }
            }

            gvClubs.DataSource = filteredClubs;
            gvClubs.DataBind();
        }

        private void ShowError(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }
    }
}
