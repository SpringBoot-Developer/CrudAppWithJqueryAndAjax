
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Data.SqlClient;
using HarrierApp.Models;
using System.Linq;

public class DbServices
{
    private readonly string connectionString = ConfigurationManager.ConnectionStrings["dbconfig"].ConnectionString;



    // UserAuth
    public bool AddUserAuth(UserAuth obj)
    {
        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO UserAuth (UserName, Email, Password, IsValid) VALUES (@UserName, @Email, @Password, @IsValid)";

                using(SqlCommand cmd = new SqlCommand(insertQuery , connection))
                {
                    cmd.Parameters.AddWithValue("@UserName" , obj.UserName);
                    cmd.Parameters.AddWithValue("@Email" , obj.Email);
                    cmd.Parameters.AddWithValue("@Password" , obj.Password);
                    cmd.Parameters.AddWithValue("@IsValid" , false); ;

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

  public UserAuth GetUserById(int userId)
{
    UserAuth user = null;

    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string selectQuery = "SELECT Id, UserName, Email, Password, IsValid FROM UserAuth WHERE Id = @UserId";

            using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserAuth
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            IsValid = reader.GetBoolean(reader.GetOrdinal("IsValid"))
                        };
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting user: {ex.Message}");
    }

    return user;
}

    public List<UserAuth> GetAllUserAuth()
    {
        List<UserAuth> users = new List<UserAuth>();

        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, UserName, Email, Password, IsValid FROM UserAuth";

                using(SqlCommand cmd = new SqlCommand(selectQuery , connection))
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        UserAuth user = new UserAuth
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")) ,
                            UserName = reader.GetString(reader.GetOrdinal("UserName")) ,
                            Email = reader.GetString(reader.GetOrdinal("Email")) ,
                            Password = reader.GetString(reader.GetOrdinal("Password")) ,
                            IsValid = reader.GetBoolean(reader.GetOrdinal("IsValid"))
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

    //---------Login----------

    public bool AddUser(UserModel obj)
    {
        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO LoginUser (Name, Email, Password) \r\nVALUES (@Name, @Email,@Password)";

                using(SqlCommand cmd = new SqlCommand(insertQuery , connection))
                {
                    cmd.Parameters.AddWithValue("@Name" , obj.Name);
                    cmd.Parameters.AddWithValue("@Email" , obj.Email);
                    cmd.Parameters.AddWithValue("@Password" , obj.Password);

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


    public List<UserModel> GetUser()
    {
        List<UserModel> users = new List<UserModel>();

        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM LoginUser";

                using(SqlCommand cmd = new SqlCommand(selectQuery , connection))
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        UserModel user = new UserModel
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")) ,
                            Name = reader.GetString(reader.GetOrdinal("Name")) ,
                            Email = reader.GetString(reader.GetOrdinal("Email")) ,
                            Password = reader.GetString(reader.GetOrdinal("Password")) ,
                       
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


    public List<UserModel> GetUserByEmail(string email)
    {
        List<UserModel> users = new List<UserModel>();

        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM LoginUser WHERE Email=@Email";

                using(SqlCommand cmd = new SqlCommand(selectQuery , connection))
                {
                    // Use SqlParameter to avoid SQL injection
                    cmd.Parameters.AddWithValue("@Email" , email);

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            UserModel user = new UserModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")) ,
                                Name = reader.GetString(reader.GetOrdinal("Name")) ,
                                Email = reader.GetString(reader.GetOrdinal("Email")) ,
                                Password = reader.GetString(reader.GetOrdinal("Password"))
                            };

                            users.Add(user);
                        }
                    }
                }
            }
        }
        catch(Exception ex)
        {
            // Handle exceptions here
            Console.WriteLine("Error: " + ex.Message);
        }

        return users;
    }

    public bool UpdateUser(UserModel obj)
    {
        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE LoginUser SET Name = @Name, Email = @Email, Password = @Password WHERE Id = @Id";

                using(SqlCommand cmd = new SqlCommand(updateQuery , connection))
                {
                    cmd.Parameters.AddWithValue("@Id" , obj.Id);
                    cmd.Parameters.AddWithValue("@Name" , obj.Name);
                    cmd.Parameters.AddWithValue("@Email" , obj.Email);
                    cmd.Parameters.AddWithValue("@Password" , obj.Password);

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


    //------------------------------------------

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


    //--------------------------------


    public bool AddUserDetail(UserDetail obj)
    {
        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO UserDetails (Name, Gender, Role) \r\nVALUES (@Name, @Gender, @Role)";

                using(SqlCommand cmd = new SqlCommand(insertQuery , connection))
                {
                    cmd.Parameters.AddWithValue("@Name" , obj.Name);
                    cmd.Parameters.AddWithValue("@Gender" , obj.Gender);
                    cmd.Parameters.AddWithValue("@Role" , obj.Role);
           
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error adding userDetail: {ex.Message}");

            return false;
        }
    }
    public List<UserDetail> GetAllUserDetail()
    {
        List<UserDetail> users = new List<UserDetail>();

        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, Name, Gender, Role FROM UserDetails";

                using(SqlCommand cmd = new SqlCommand(selectQuery , connection))
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        UserDetail user = new UserDetail
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")) ,
                            Name = reader.GetString(reader.GetOrdinal("Name")) ,
                            Gender = reader.GetString(reader.GetOrdinal("Gender")) ,
                            Role = reader.GetString(reader.GetOrdinal("Role")) 
                        };

                        users.Add(user);
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error getting usersDetail {ex.Message}");
        }

        return users;
    }

    public bool DeleteUserDetail(UserDetail obj)
    {
        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM UserDetails WHERE Id = @Id";

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
            Console.WriteLine($"Error deleting userDetail: {ex.Message}");
            return false;
        }
    }
    public bool UpdateUserDetail(UserDetail obj)
    {
        try
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE UserDetails SET Name = @Name, Gender = @Gender, Role = @Role WHERE Id = @Id";

                using(SqlCommand cmd = new SqlCommand(updateQuery , connection))
                {
                    cmd.Parameters.AddWithValue("@Id" , obj.Id);
                    cmd.Parameters.AddWithValue("@Name" , obj.Name);
                    cmd.Parameters.AddWithValue("@Gender" , obj.Gender);
                    cmd.Parameters.AddWithValue("@Role" , obj.Role);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error updating userDetail: {ex.Message}");
            return false;
        }
    }
}
