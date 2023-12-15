using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ClassLibPaste
{
    public class DBL
    {
        public void SaveToDatabase(string title, string content, string qs, bool burnAfterReading)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string insertQuery = "INSERT INTO Pastes (Title, Content, QS, BurnAfterReading, ViewCount) VALUES (@Title, @Content, @Query, @BAR, @ViewCount)";

                using (SqlCommand command = new SqlCommand(insertQuery, conn))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Content", content);
                    command.Parameters.AddWithValue("@Query", qs);
                    command.Parameters.AddWithValue("@BAR", burnAfterReading);
                    command.Parameters.AddWithValue("@ViewCount", 0); // Initial view count

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
