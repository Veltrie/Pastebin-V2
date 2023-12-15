using ClassLibPaste;
using System;
using System.Data.SqlClient;

namespace VelBin
{
    public partial class SubmitPaste : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string title = Request.Form["title"];
                string content = Request.Form["content"];
                bool burnAfterReading = Request.Form["burnAfterReading"] == "on";

                string qs = Guid.NewGuid().ToString();

                DBL dBL = new DBL();
                dBL.SaveToDatabase(title, content,qs, burnAfterReading);

                Response.Redirect("ViewPaste.aspx?uniquecode=" + qs);
            }
        }

        //private void SaveToDatabase(string title, string content, string qs, bool burnAfterReading)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        string insertQuery = "INSERT INTO Pastes (Title, Content, QS, BurnAfterReading, ViewCount) VALUES (@Title, @Content, @Query, @BAR, @ViewCount)";

        //        using (SqlCommand command = new SqlCommand(insertQuery, conn))
        //        {
        //            command.Parameters.AddWithValue("@Title", title);
        //            command.Parameters.AddWithValue("@Content", content);
        //            command.Parameters.AddWithValue("@Query", qs);
        //            command.Parameters.AddWithValue("@BAR", burnAfterReading);
        //            command.Parameters.AddWithValue("@ViewCount", 0); // Initial view count

        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

    }
}
