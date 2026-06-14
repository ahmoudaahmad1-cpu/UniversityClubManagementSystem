using System;
using System.Web.UI;

namespace UniversityClubManagementSystem
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Default.aspx");
            }
            catch (Exception)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}
