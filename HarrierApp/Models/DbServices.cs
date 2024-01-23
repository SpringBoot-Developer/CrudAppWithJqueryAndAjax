
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Data.SqlClient;
using HarrierApp.Models;
using System.Linq;

public class DbServices
{
    private readonly string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;



    public bool Add(User obj)
    {
        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Users (Name, Email, Mobile, Skills, Role, Status, Password, ConfirmPassword) \r\nVALUES (@Name, @Email, @Mobile, @Skills, @Role, @Status, @Password, @ConfirmPassword)";

                using(SqlCommand cmd = new SqlCommand(insertQuery , connection))
                {
                    cmd.Parameters.AddWithValue("@Name" , obj.Name);
                    cmd.Parameters.AddWithValue("@Email" , obj.Email);
                    cmd.Parameters.AddWithValue("@Mobile" , obj.Mobile);
                    cmd.Parameters.AddWithValue("@Skills" , string.Join("," , obj.Skills));
                    cmd.Parameters.AddWithValue("@Role" , obj.Role);
                    cmd.Parameters.AddWithValue("@Status" , obj.Status);
                    cmd.Parameters.AddWithValue("@Password" , obj.Password);
                    cmd.Parameters.AddWithValue("@ConfirmPassword" , obj.ConfirmPassword);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error adding user: {ex.Message}");
       
            return false;
        }
    }
    public List<User> GetAll()
    {
        List<User> users = new List<User>();

        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, Name, Email, Mobile, Skills, Role, Status FROM Users";

                using(SqlCommand cmd = new SqlCommand(selectQuery , connection))
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        User user = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")) ,
                            Name = reader.GetString(reader.GetOrdinal("Name")) ,
                            Email = reader.GetString(reader.GetOrdinal("Email")) ,
                            Mobile = reader.GetString(reader.GetOrdinal("Mobile")) ,
                            Skills = reader.GetString(reader.GetOrdinal("Skills")).Split(',').ToList() , 
                            Role = reader.GetString(reader.GetOrdinal("Role")) ,
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        };

                        users.Add(user);
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error getting users: {ex.Message}");
        }

        return users;
    }



    public bool Delete(User obj)
    {
        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Users WHERE Id = @Id";

                using(SqlCommand cmd = new SqlCommand(deleteQuery , connection))
                {
                    cmd.Parameters.AddWithValue("@Id" , obj.Id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error deleting user: {ex.Message}");
            return false;
        }
    }
    public bool Update(User obj)
    {
        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Users SET Name = @Name, Email = @Email, Mobile = @Mobile, Skills = @Skills, Role = @Role, Status = @Status WHERE Id = @Id";

                using(SqlCommand cmd = new SqlCommand(updateQuery , connection))
                {
                    cmd.Parameters.AddWithValue("@Id" , obj.Id);
                    cmd.Parameters.AddWithValue("@Name" , obj.Name);
                    cmd.Parameters.AddWithValue("@Email" , obj.Email);
                    cmd.Parameters.AddWithValue("@Mobile" , obj.Mobile);
                    cmd.Parameters.AddWithValue("@Skills" , string.Join("," , obj.Skills)); 
                    cmd.Parameters.AddWithValue("@Role" , obj.Role);
                    cmd.Parameters.AddWithValue("@Status" , obj.Status);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error updating user: {ex.Message}");
            return false;
        }
    }

}
