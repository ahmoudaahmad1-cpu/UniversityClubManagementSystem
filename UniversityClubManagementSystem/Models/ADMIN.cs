using System;
using System.Data;
using System.Data.SqlClient;

namespace UniversityClubManagementSystem.Models
{
    public class ADMIN
    {
        public int AdminId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        private DBHelper db = new DBHelper();

        public ADMIN Authenticate(string username, string password)
        {
            string query = "SELECT * FROM Admins WHERE Username = @Username";
            SqlParameter[] parameters = { new SqlParameter("@Username", username) };
            DataTable dt = db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dt.Rows[0];
            string storedHash = row["PasswordHash"].ToString();

            if (!PasswordHelper.VerifyPassword(password, storedHash))
            {
                return null;
            }

            return new ADMIN
            {
                AdminId = Convert.ToInt32(row["AdminId"]),
                Username = row["Username"].ToString(),
                PasswordHash = storedHash
            };
        }
    }
}
