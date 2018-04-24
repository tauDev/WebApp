using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApp.Models
{
    public class UserDataAccessLayer
    {
        public static IConfiguration Configuration { get; set; }

        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder() 
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string connectionString = Configuration["ConnectionStrings:myConString"];

            return connectionString;
        }
        string connectionString = GetConnectionString();

        //To Register a new user
        public string RegisterUser(UserDetails user)
        {
            string result;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO () VALUES ()", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Username", user.UserName);
                cmd.Parameters.AddWithValue("@Emplid", user.Emplid);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                con.Close();
            }
                return result;
        }
        public string ValidateLogin(UserDetails user)
        {
            string result;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM db_EMPL WHERE EMPLID = @loginid AND " +
                    "PASS = @loginpassword", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@loginid", user.Emplid);
                cmd.Parameters.AddWithValue("@loginpassword", user.Password);
                con.Open();
                SqlDataReader dtr = cmd.ExecuteReader();
                if (dtr.HasRows)
                {
                    result = "Success";
                }
                else
                {
                    result = "Fail";
                }
                con.Close();
                return result;
            }
        }
    }
}
