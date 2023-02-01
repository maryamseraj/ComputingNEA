using System;
using Microsoft.Data.SqlClient;

namespace COMPUTINGNEA.Models
{
    public class sqldatabase
    {
        public void ConnectToDatabase()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "connectiontestnea.database.windows.net";
            builder.UserID = "maryamseraj";
            builder.Password = "Thenextstep22!";
            builder.InitialCatalog = "connection";
            SqlConnection con = new SqlConnection(builder.ConnectionString);

        }
    }
}
