using BlazorToDoList.Model;
using System.Data.SQLite;

namespace BlazorToDoList.Service
{
    public class LoginService
    {
        private readonly string _connectionString = @"Data Source=C:\Users\gldot\Downloads\Rebuilt_TasksDB.sqlite;";
        // Get user details (On Login)
        public User GetUser(string username, string password)
        {
            string sql = $"SELECT * FROM Users WHERE lower(Username) = '{username.ToLower()}' AND Password = '{password}'";
            User user = new User();
            user.UserId = -1;
            user.UserName = "Not logged in";
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.UserId = int.Parse(reader["UserId"].ToString());
                            user.UserName = reader["Username"].ToString();
                        }
                    }
                }
            }
            return user;
        }
        // Register - check if exists 
        public bool UserExists(string username)
        {
            // Compare lower case
            string sql = $"SELECT * FROM Users WHERE lower(Username) = '{username.ToLower()}'";
            User user = new User();
            user.UserId = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.UserId = int.Parse(reader["UserId"].ToString());
                        }
                    }
                }
            }
            if (user.UserId>0)
                return true;
            else
                return false;

        }
        // Register new user
        public int AddUser(string username, string password, bool isAdmin)
        {
            int admin = isAdmin ? 1 : 0;
            string insertSql = $"INSERT INTO Users (Username,Password,IsAdmin) " +
            $"VALUES ('{username}', " + $"'{password}'," + $" '{admin}')";
            int result = ExecuteSQL(insertSql);
            return result;
        }
        private int ExecuteSQL(string sql)
        {
            // Connect to DB
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            // Run the Command and read data
            var command = new SQLiteCommand(sql, connection);
            // Execute Update/Insert/Delete
            int result = command.ExecuteNonQuery();
            return result;
        }
    }
}
