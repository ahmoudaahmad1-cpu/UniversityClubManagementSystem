using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace UniversityClubManagementSystem.Models
{
    public class MEMBERSHIP
    {
        public int MembershipId { get; set; }
        public int StudentId { get; set; }
        public int ClubId { get; set; }
        public DateTime JoinDate { get; set; }
        public string Status { get; set; }
        public string StudentName { get; set; }
        public string ClubName { get; set; }

        private DBHelper db = new DBHelper();

        public bool RequestJoin(int studentId, int clubId)
        {
            if (HasExistingRequest(studentId, clubId))
            {
                return false;
            }

            string query = @"INSERT INTO Memberships (StudentId, ClubId, JoinDate, Status)
                             VALUES (@StudentId, @ClubId, GETDATE(), 'Pending')";

            SqlParameter[] parameters = {
                new SqlParameter("@StudentId", studentId),
                new SqlParameter("@ClubId", clubId)
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool HasExistingRequest(int studentId, int clubId)
        {
            string query = @"SELECT COUNT(*) FROM Memberships
                             WHERE StudentId = @StudentId AND ClubId = @ClubId
                             AND Status IN ('Pending', 'Approved')";
            SqlParameter[] parameters = {
                new SqlParameter("@StudentId", studentId),
                new SqlParameter("@ClubId", clubId)
            };
            int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
            return count > 0;
        }

        public List<MEMBERSHIP> GetMembershipsByStudent(int studentId)
        {
            string query = @"SELECT m.*, c.ClubName
                             FROM Memberships m
                             INNER JOIN Clubs c ON m.ClubId = c.ClubId
                             WHERE m.StudentId = @StudentId
                             ORDER BY m.JoinDate DESC";
            SqlParameter[] parameters = { new SqlParameter("@StudentId", studentId) };
            DataTable dt = db.ExecuteQuery(query, parameters);
            return MapMembershipList(dt);
        }

        public List<MEMBERSHIP> GetAllMemberships()
        {
            string query = @"SELECT m.*, s.FullName AS StudentName, c.ClubName
                             FROM Memberships m
                             INNER JOIN Students s ON m.StudentId = s.StudentId
                             INNER JOIN Clubs c ON m.ClubId = c.ClubId
                             ORDER BY m.JoinDate DESC";

            DataTable dt = db.ExecuteQuery(query);
            return MapMembershipList(dt);
        }

        public int GetPendingCount()
        {
            string query = "SELECT COUNT(*) FROM Memberships WHERE Status = 'Pending'";
            return Convert.ToInt32(db.ExecuteScalar(query));
        }

        public bool UpdateStatus(int membershipId, string status)
        {
            string query = "UPDATE Memberships SET Status = @Status WHERE MembershipId = @MembershipId";
            SqlParameter[] parameters = {
                new SqlParameter("@Status", status),
                new SqlParameter("@MembershipId", membershipId)
            };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool CancelRequest(int membershipId, int studentId)
        {
            string query = @"DELETE FROM Memberships
                             WHERE MembershipId = @MembershipId AND StudentId = @StudentId AND Status = 'Pending'";
            SqlParameter[] parameters = {
                new SqlParameter("@MembershipId", membershipId),
                new SqlParameter("@StudentId", studentId)
            };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public string GetStudentMembershipStatus(int studentId, int clubId)
        {
            string query = @"SELECT Status FROM Memberships
                             WHERE StudentId = @StudentId AND ClubId = @ClubId";
            SqlParameter[] parameters = {
                new SqlParameter("@StudentId", studentId),
                new SqlParameter("@ClubId", clubId)
            };
            object result = db.ExecuteScalar(query, parameters);

            if (result == null || result == DBNull.Value)
            {
                return "";
            }

            return result.ToString();
        }

        private List<MEMBERSHIP> MapMembershipList(DataTable dt)
        {
            List<MEMBERSHIP> memberships = new List<MEMBERSHIP>();
            foreach (DataRow row in dt.Rows)
            {
                memberships.Add(MapMembership(row));
            }
            return memberships;
        }

        private MEMBERSHIP MapMembership(DataRow row)
        {
            MEMBERSHIP membership = new MEMBERSHIP
            {
                MembershipId = Convert.ToInt32(row["MembershipId"]),
                StudentId = Convert.ToInt32(row["StudentId"]),
                ClubId = Convert.ToInt32(row["ClubId"]),
                JoinDate = Convert.ToDateTime(row["JoinDate"]),
                Status = row["Status"].ToString()
            };

            if (row.Table.Columns.Contains("StudentName"))
            {
                membership.StudentName = row["StudentName"].ToString();
            }

            if (row.Table.Columns.Contains("ClubName"))
            {
                membership.ClubName = row["ClubName"].ToString();
            }

            return membership;
        }
    }
}
