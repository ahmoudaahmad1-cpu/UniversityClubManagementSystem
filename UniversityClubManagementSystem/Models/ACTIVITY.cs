using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace UniversityClubManagementSystem.Models
{
    public class ACTIVITY
    {
        public int ActivityId { get; set; }
        public int ClubId { get; set; }
        public string ActivityTitle { get; set; }
        public string Description { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ClubName { get; set; }

        private DBHelper db = new DBHelper();

        public bool Create(int clubId, string activityTitle, string description, DateTime activityDate, string location)
        {
            string query = @"INSERT INTO Activities (ClubId, ActivityTitle, Description, ActivityDate, Location, CreatedDate)
                             VALUES (@ClubId, @ActivityTitle, @Description, @ActivityDate, @Location, GETDATE())";

            SqlParameter[] parameters = {
                new SqlParameter("@ClubId", clubId),
                new SqlParameter("@ActivityTitle", activityTitle),
                new SqlParameter("@Description", (object)description ?? DBNull.Value),
                new SqlParameter("@ActivityDate", activityDate),
                new SqlParameter("@Location", location)
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public ACTIVITY GetById(int activityId)
        {
            string query = @"SELECT a.*, c.ClubName
                             FROM Activities a
                             INNER JOIN Clubs c ON a.ClubId = c.ClubId
                             WHERE a.ActivityId = @ActivityId";
            SqlParameter[] parameters = { new SqlParameter("@ActivityId", activityId) };
            DataTable dt = db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return MapActivity(dt.Rows[0]);
        }

        public List<ACTIVITY> GetAllActivities()
        {
            string query = @"SELECT a.*, c.ClubName
                             FROM Activities a
                             INNER JOIN Clubs c ON a.ClubId = c.ClubId
                             ORDER BY a.ActivityDate DESC";

            DataTable dt = db.ExecuteQuery(query);
            return MapActivityList(dt);
        }

        public List<ACTIVITY> GetUpcomingActivities()
        {
            string query = @"SELECT a.*, c.ClubName
                             FROM Activities a
                             INNER JOIN Clubs c ON a.ClubId = c.ClubId
                             WHERE a.ActivityDate >= GETDATE()
                             ORDER BY a.ActivityDate ASC";

            DataTable dt = db.ExecuteQuery(query);
            return MapActivityList(dt);
        }

        public List<ACTIVITY> GetActivitiesByClub(int clubId)
        {
            string query = @"SELECT a.*, c.ClubName
                             FROM Activities a
                             INNER JOIN Clubs c ON a.ClubId = c.ClubId
                             WHERE a.ClubId = @ClubId
                             ORDER BY a.ActivityDate DESC";
            SqlParameter[] parameters = { new SqlParameter("@ClubId", clubId) };
            DataTable dt = db.ExecuteQuery(query, parameters);
            return MapActivityList(dt);
        }

        public int GetTotalActivitiesCount()
        {
            string query = "SELECT COUNT(*) FROM Activities";
            return Convert.ToInt32(db.ExecuteScalar(query));
        }

        public bool Update(int activityId, int clubId, string activityTitle, string description, DateTime activityDate, string location)
        {
            string query = @"UPDATE Activities
                             SET ClubId = @ClubId, ActivityTitle = @ActivityTitle, Description = @Description,
                                 ActivityDate = @ActivityDate, Location = @Location
                             WHERE ActivityId = @ActivityId";

            SqlParameter[] parameters = {
                new SqlParameter("@ClubId", clubId),
                new SqlParameter("@ActivityTitle", activityTitle),
                new SqlParameter("@Description", (object)description ?? DBNull.Value),
                new SqlParameter("@ActivityDate", activityDate),
                new SqlParameter("@Location", location),
                new SqlParameter("@ActivityId", activityId)
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Delete(int activityId)
        {
            string query = "DELETE FROM Activities WHERE ActivityId = @ActivityId";
            SqlParameter[] parameters = { new SqlParameter("@ActivityId", activityId) };
            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        private List<ACTIVITY> MapActivityList(DataTable dt)
        {
            List<ACTIVITY> activities = new List<ACTIVITY>();
            foreach (DataRow row in dt.Rows)
            {
                activities.Add(MapActivity(row));
            }
            return activities;
        }

        private ACTIVITY MapActivity(DataRow row)
        {
            ACTIVITY activity = new ACTIVITY
            {
                ActivityId = Convert.ToInt32(row["ActivityId"]),
                ClubId = Convert.ToInt32(row["ClubId"]),
                ActivityTitle = row["ActivityTitle"].ToString(),
                Description = row["Description"] == DBNull.Value ? "" : row["Description"].ToString(),
                ActivityDate = Convert.ToDateTime(row["ActivityDate"]),
                Location = row["Location"].ToString(),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"])
            };

            if (row.Table.Columns.Contains("ClubName"))
            {
                activity.ClubName = row["ClubName"].ToString();
            }

            return activity;
        }
    }
}
