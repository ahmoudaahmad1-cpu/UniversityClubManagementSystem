using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace UniversityClubManagementSystem.Models
{
    public class STUDENT
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string Major { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime CreatedDate { get; set; }

        private DBHelper db = new DBHelper();

        public bool Register(string fullName, string email, string username, string password, string phone, string major)
        {
            if (UsernameExists(username) || EmailExists(email))
            {
                return false;
            }

            string query = @"INSERT INTO Students (FullName, Email, Username, PasswordHash, Phone, Major, IsBlocked, CreatedDate)
                             VALUES (@FullName, @Email, @Username, @PasswordHash, @Phone, @Major, 0, GETDATE())";

            SqlParameter[] parameters = {
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Email", email),
                new SqlParameter("@Username", username),
                new SqlParameter("@PasswordHash", PasswordHelper.HashPassword(password)),
                new SqlParameter("@Phone", (object)phone ?? DBNull.Value),
                new SqlParameter("@Major", (object)major ?? DBNull.Value)
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public STUDENT Authenticate(string username, string password)
        {
            string query = "SELECT * FROM Students WHERE Username = @Username";
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

            if (Convert.ToBoolean(row["IsBlocked"]))
            {
                return null;
            }

            return MapStudent(row);
        }

        public STUDENT GetById(int studentId)
        {
            string query = "SELECT * FROM Students WHERE StudentId = @StudentId";
            SqlParameter[] parameters = { new SqlParameter("@StudentId", studentId) };
            DataTable dt = db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return MapStudent(dt.Rows[0]);
        }

        public bool UpdateProfile(int studentId, string fullName, string email, string phone, string major)
        {
            return UpdateProfile(studentId, fullName, email, phone, major, null);
        }

        public bool UpdateProfile(int studentId, string fullName, string email, string phone, string major, string username)
        {
            STUDENT current = GetById(studentId);
            if (current == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(username) &&
                !current.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                UsernameExists(username))
            {
                return false;
            }

            if (!current.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && EmailExists(email))
            {
                return false;
            }

            string query = @"UPDATE Students
                             SET FullName = @FullName, Email = @Email, Phone = @Phone, Major = @Major, Username = @Username
                             WHERE StudentId = @StudentId";

            SqlParameter[] parameters = {
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Email", email),
                new SqlParameter("@Phone", (object)phone ?? DBNull.Value),
                new SqlParameter("@Major", (object)major ?? DBNull.Value),
                new SqlParameter("@Username", string.IsNullOrWhiteSpace(username) ? current.Username : username),
                new SqlParameter("@StudentId", studentId)
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UsernameExists(string username)
        {
            string query = "SELECT COUNT(*) FROM Students WHERE Username = @Username";
            SqlParameter[] parameters = { new SqlParameter("@Username", username) };
            int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
            return count > 0;
        }

        public bool EmailExists(string email)
        {
            string query = "SELECT COUNT(*) FROM Students WHERE Email = @Email";
            SqlParameter[] parameters = { new SqlParameter("@Email", email) };
            int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
            return count > 0;
        }

        public List<STUDENT> GetAllStudents(string search)
        {
            string query = "SELECT * FROM Students WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query += " AND (FullName LIKE @Search OR Email LIKE @Search OR Username LIKE @Search)";
                parameters.Add(new SqlParameter("@Search", "%" + search + "%"));
            }

            query += " ORDER BY FullName";

            DataTable dt = db.ExecuteQuery(query, parameters.ToArray());
            List<STUDENT> students = new List<STUDENT>();

            foreach (DataRow row in dt.Rows)
            {
                students.Add(MapStudent(row));
            }

            return students;
        }

        public int GetTotalStudentsCount()
        {
            string query = "SELECT COUNT(*) FROM Students";
            return Convert.ToInt32(db.ExecuteScalar(query));
        }

        public bool ToggleBlockedStatus(int studentId)
        {
            string query = @"UPDATE Students
                             SET IsBlocked = CASE WHEN IsBlocked = 1 THEN 0 ELSE 1 END
                             WHERE StudentId = @StudentId";
            SqlParameter[] parameters = { new SqlParameter("@StudentId", studentId) };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        private STUDENT MapStudent(DataRow row)
        {
            return new STUDENT
            {
                StudentId = Convert.ToInt32(row["StudentId"]),
                FullName = row["FullName"].ToString(),
                Email = row["Email"].ToString(),
                Username = row["Username"].ToString(),
                PasswordHash = row["PasswordHash"].ToString(),
                Phone = row["Phone"] == DBNull.Value ? "" : row["Phone"].ToString(),
                Major = row["Major"] == DBNull.Value ? "" : row["Major"].ToString(),
                IsBlocked = Convert.ToBoolean(row["IsBlocked"]),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"])
            };
        }
    }
}
