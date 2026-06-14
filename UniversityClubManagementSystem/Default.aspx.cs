using System;
using System.Web.UI;

namespace UniversityClubManagementSystem
{
    public partial class DefaultPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["StudentId"] != null)
                {
                    Response.Redirect("~/Home.aspx");
                }

                if (Session["AdminId"] != null)
                {
                    Response.Redirect("~/Admin/Dashboard.aspx");
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
