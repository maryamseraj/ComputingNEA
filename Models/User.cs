using System;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;
namespace COMPUTINGNEA.Models
{
    public class User 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Userpassword { get; set; }
        public int Userid { get; set; }

        // checks if email address entered is already in database
        public int CheckEmailExists()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "connectiontestnea.database.windows.net";
            builder.UserID = "maryamseraj";
            builder.Password = "Thenextstep22!";
            builder.InitialCatalog = "connection";

            // Connect to SQL database on Azure
            SqlConnection con = new SqlConnection(builder.ConnectionString);

            // Check if email already exists
            string checkEmail = "SELECT COUNT(*) FROM [dbo].[User] WHERE Email = " + "'" + Email + "'";
            SqlCommand cmd = new SqlCommand(checkEmail, con);
            con.Open();
            int EmailExist = (int)cmd.ExecuteScalar();
            
            return EmailExist;
        }

        // checks if username entered is already in database
        public int CheckUsernameExists()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "connectiontestnea.database.windows.net";
            builder.UserID = "maryamseraj";
            builder.Password = "Thenextstep22!";
            builder.InitialCatalog = "connection";

            // Connect to SQL database on Azure
            SqlConnection con = new SqlConnection(builder.ConnectionString);

            // Check if username already exists
            string checkUsername = "SELECT COUNT(*) FROM [dbo].[User] WHERE Username = " + "'" + Username + "'";
            SqlCommand cmd = new SqlCommand(checkUsername, con);
            con.Open();
            int UserExist = (int)cmd.ExecuteScalar();
            
            return UserExist;
        }

        // Saves user details into database upon successful registration
        public virtual int SaveDetails()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "connectiontestnea.database.windows.net";
            builder.UserID = "maryamseraj";
            builder.Password = "Thenextstep22!";
            builder.InitialCatalog = "connection";

            // Connect to SQL database on Azure
            SqlConnection con = new SqlConnection(builder.ConnectionString);

            // Hash the password
            var hashcode = GetHashCode(Userpassword);

            // Insert user details into User table
            string insertDetails = "INSERT INTO [dbo].[User](FirstName, LastName, Email, Username, Userpassword) values ('" + FirstName + "','" + LastName + "','" + Email + "','" + Username + "','" + hashcode + "')";
            SqlCommand cmd = new SqlCommand(insertDetails, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            
            return i;
        }

        // Hashes the password upon registration
        public string GetHashCode(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(Userpassword, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        // De-hases the hashed password
        public int GetPassword()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "connectiontestnea.database.windows.net";
            builder.UserID = "maryamseraj";
            builder.Password = "Thenextstep22!";
            builder.InitialCatalog = "connection";

            // Connect to SQL database on Azure
            SqlConnection con = new SqlConnection(builder.ConnectionString);

            // fetch stored value
            string savedPasswordHash = "SELECT Userpassword FROM [dbo].[User] WHERE Username = " + "'" + Username + "'";
            SqlCommand cmd = new SqlCommand(savedPasswordHash, con);
            con.Open();
            string passwordHash = (string)cmd.ExecuteScalar();
            

            //  Extract the bytes 
            byte[] hashBytes = Convert.FromBase64String(passwordHash);
            // Get the salt 
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            // Compute the hash on the password the user entered 
            var pbkdf2 = new Rfc2898DeriveBytes(Userpassword, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            // Compare the results 
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return 0; // prevents unauthorised access
                }
            }
            return 1; // access granted
        }

        // not used
        public int GetUserID()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "connectiontestnea.database.windows.net";
            builder.UserID = "maryamseraj";
            builder.Password = "Thenextstep22!";
            builder.InitialCatalog = "connection";

            // Connect to SQL database on Azure
            SqlConnection con = new SqlConnection(builder.ConnectionString);

            // Check if username and email already exist
            Username = "bobsmith";
            string checkUserID = "SELECT UserID FROM [dbo].[User] WHERE Username = " + "'" + Username + "'";
            SqlCommand cmd = new SqlCommand(checkUserID, con);
            con.Open();
            int UserID = (int)cmd.ExecuteScalar();
            
            return UserID;
        }

        public int SessionID()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "connectiontestnea.database.windows.net";
            builder.UserID = "maryamseraj";
            builder.Password = "Thenextstep22!";
            builder.InitialCatalog = "connection";

            // Connect to SQL database on Azure
            SqlConnection con = new SqlConnection(builder.ConnectionString);

            // Check if username and email already exist
            string checkEmail = "SELECT COUNT(*) FROM [dbo].[User] WHERE UserID = " + "'" + Userid + "'";
            SqlCommand cmd = new SqlCommand(checkEmail, con);
            con.Open();
            int EmailExist = (int)cmd.ExecuteScalar();
            con.Close();
            return EmailExist;
        }
    }
}



/*
 CREATE TABLE Investment (
InvestmentID int NOT NULL PRIMARY KEY,
UserID int FOREIGN KEY REFERENCES [dbo].[User](UserID)

CREATE TABLE User(
    UserID int NOT NULL PRIMARY KEY,
    ...
)
);
*/
