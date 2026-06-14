using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace UniversityClubManagementSystem.Models
{
    public class CLUB
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string PresidentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int MemberCount { get; set; }

        private DBHelper db = new DBHelper();

        public bool Create(string clubName, string description, string category, string presidentName, bool isActive = true)
        {
            string query = @"INSERT INTO Clubs (ClubName, Description, Category, PresidentName, CreatedDate, IsActive)
                             VALUES (@ClubName, @Description, @Category, @PresidentName, GETDATE(), @IsActive)";

            SqlParameter[] parameters = {
                new SqlParameter("@ClubName", clubName),
                new SqlParameter("@Description", (object)description ?? DBNull.Value),
                new SqlParameter("@Category", category),
                new SqlParameter("@PresidentName", presidentName),
                new SqlParameter("@IsActive", isActive)
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public CLUB GetById(int clubId)
        {
            string query = "SELECT * FROM Clubs WHERE ClubId = @ClubId";
            SqlParameter[] parameters = { new SqlParameter("@ClubId", clubId) };
            DataTable dt = db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return MapClub(dt.Rows[0]);
        }

        public List<CLUB> GetAllClubs()
        {
            string query = @"SELECT c.*,
                                    (SELECT COUNT(*) FROM Memberships m
                                     WHERE m.ClubId = c.ClubId AND m.Status = 'Approved') AS MemberCount
                             FROM Clubs c
                             ORDER BY c.ClubName";

            DataTable dt = db.ExecuteQuery(query);
            return MapClubList(dt);
        }

        public List<CLUB> GetActiveClubs()
        {
            string query = @"SELECT c.*,
                                    (SELECT COUNT(*) FROM Memberships m
                                     WHERE m.ClubId = c.ClubId AND m.Status = 'Approved') AS MemberCount
                             FROM Clubs c
                             WHERE c.IsActive = 1
                             ORDER BY c.ClubName";

            DataTable dt = db.ExecuteQuery(query);
            return MapClubList(dt);
        }

        public List<string> GetCategories()
        {
            string query = "SELECT DISTINCT Category FROM Clubs ORDER BY Category";
            DataTable dt = db.ExecuteQuery(query);
            List<string> categories = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                categories.Add(row["Category"].ToString());
            }

            return categories;
        }

        public int GetTotalClubsCount()
        {
            string query = "SELECT COUNT(*) FROM Clubs";
            return Convert.ToInt32(db.ExecuteScalar(query));
        }

        public bool Update(int clubId, string clubName, string description, string category, string presidentName, bool isActive)
        {
            string query = @"UPDATE Clubs
                             SET ClubName = @ClubName, Description = @Description, Category = @Category,
                                 PresidentName = @PresidentName, IsActive = @IsActive
                             WHERE ClubId = @ClubId";

            SqlParameter[] parameters = {
                new SqlParameter("@ClubName", clubName),
                new SqlParameter("@Description", (object)description ?? DBNull.Value),
                new SqlParameter("@Category", category),
                new SqlParameter("@PresidentName", presidentName),
                new SqlParameter("@IsActive", isActive),
                new SqlParameter("@ClubId", clubId)
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool HasRelatedRecords(int clubId)
        {
            string query = @"SELECT
                                (SELECT COUNT(*) FROM Activities WHERE ClubId = @ClubId) +
                                (SELECT COUNT(*) FROM Memberships WHERE ClubId = @ClubId) AS TotalCount";
            SqlParameter[] parameters = { new SqlParameter("@ClubId", clubId) };
            int count = Convert.ToInt32(db.ExecuteScalar(query, parameters));
            return count > 0;
        }

        public bool Delete(int clubId)
        {
            if (HasRelatedRecords(clubId))
            {
                return false;
            }

            string query = "DELETE FROM Clubs WHERE ClubId = @ClubId";
            SqlParameter[] parameters = { new SqlParameter("@ClubId", clubId) };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        private List<CLUB> MapClubList(DataTable dt)
        {
            List<CLUB> clubs = new List<CLUB>();
            foreach (DataRow row in dt.Rows)
            {
                clubs.Add(MapClub(row));
            }
            return clubs;
        }

        private CLUB MapClub(DataRow row)
        {
            CLUB club = new CLUB
            {
                ClubId = Convert.ToInt32(row["ClubId"]),
                ClubName = row["ClubName"].ToString(),
                Description = row["Description"] == DBNull.Value ? "" : row["Description"].ToString(),
                Category = row["Category"].ToString(),
                PresidentName = row["PresidentName"].ToString(),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                IsActive = Convert.ToBoolean(row["IsActive"])
            };

            if (row.Table.Columns.Contains("MemberCount"))
            {
                club.MemberCount = Convert.ToInt32(row["MemberCount"]);
            }

            return club;
        }
    }
}
