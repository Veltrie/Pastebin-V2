using System;
using System.Data.SqlClient;

namespace VelBin
{
    public partial class ViewPaste : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
        const int ViewCountThreshold = 2; // Set your desired view count threshold

        protected void Page_Load(object sender, EventArgs e)
        {
            int pasteID = 0;

            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    string idValue = Request.QueryString["uniquecode"];

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Increment the view count
                        string updateViewCountQuery = "UPDATE Pastes SET ViewCount = ViewCount + 1 WHERE QS = @ID";
                        using (SqlCommand updateCommand = new SqlCommand(updateViewCountQuery, conn))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", idValue);
                            updateCommand.ExecuteNonQuery();
                        }

                        // Retrieve paste information
                        string selectQuery = "SELECT Title, Content, ViewCount, BurnAfterReading FROM Pastes WHERE QS = @ID";
                        using (SqlCommand command = new SqlCommand(selectQuery, conn))
                        {
                            command.Parameters.AddWithValue("@ID", idValue);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    lblTitle.Text = reader["Title"].ToString();
                                    lblContent.Text = reader["Content"].ToString();
                                    int viewCount = Convert.ToInt32(reader["ViewCount"]);
                                    bool burnAfterReading = Convert.ToBoolean(reader["BurnAfterReading"]);

                                    // Display Burn After Reading status
                                    lblBurnStatus.Text = GetBurnStatus(viewCount, burnAfterReading, idValue);
                                }
                                else
                                {
                                    lblTitle.Text = "Paste not found";
                                }
                            }
                        }
                    }
                }
            }
        }

        private string GetBurnStatus(int viewCount, bool burnAfterReading, string idValue)
        {
            if (viewCount >= ViewCountThreshold)
            {
                if (burnAfterReading)
                {
                    MarkPasteAsBurned(idValue); // Mark as burned after reading

                    // Check if the paste is marked as burned and delete it
                    if (IsPasteBurned(idValue))
                    {
                        DeletePaste(idValue);
                        return "Burned After Reading (Paste Deleted)";
                    }
                    else
                    {
                        return "Burned After Reading";
                    }
                }
            }

            return "Not Burned";
        }

        private bool IsPasteBurned(string idValue)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if the paste is marked as burned
                string checkBurnStatusQuery = "SELECT BurnAfterReading FROM Pastes WHERE QS = @ID";
                using (SqlCommand checkCommand = new SqlCommand(checkBurnStatusQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@ID", idValue);
                    return Convert.ToBoolean(checkCommand.ExecuteScalar());
                }
            }
        }

        private void MarkPasteAsBurned(string idValue)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Update the BurnAfterReading column to mark the paste as burned
                string updateBurnStatusQuery = "UPDATE Pastes SET BurnAfterReading = 1 WHERE QS = @ID";
                using (SqlCommand updateCommand = new SqlCommand(updateBurnStatusQuery, conn))
                {
                    updateCommand.Parameters.AddWithValue("@ID", idValue);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        private void DeletePaste(string idValue)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Delete the paste from the database
                string deletePasteQuery = "DELETE FROM Pastes WHERE QS = @ID";
                using (SqlCommand deleteCommand = new SqlCommand(deletePasteQuery, conn))
                {
                    deleteCommand.Parameters.AddWithValue("@ID", idValue);
                    deleteCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
