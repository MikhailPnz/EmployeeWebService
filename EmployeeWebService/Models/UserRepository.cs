using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Npgsql;

namespace EmployeeWebService.Models
{
    public interface IUserRepository
    {
        void Create(User user);
        void Delete(int id);
        User Get(int id);
        List<User> GetUsers();
        void Update(User user);
    }
    public class UserRepository : IUserRepository
    {
        string connectionString = null;
        public UserRepository(string conn)
        {
            connectionString = conn;
        }
        public List<User> GetUsers()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                //connection.Execute("Insert into Employee (first_name, last_name, address) values ('John', 'Smith', '123 Duane St');");
                return connection.Query<User>("SELECT * FROM Users").ToList();
                //Console.WriteLine(value.First());
            }


            //using (IDbConnection db = new SqlConnection(connectionString))
            //{
            //    return db.Query<User>("SELECT * FROM Users").ToList();
            //}
        }

        public User Get(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                //connection.Execute("Insert into Employee (first_name, last_name, address) values ('John', 'Smith', '123 Duane St');");
                return connection.Query<User>("SELECT * FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
                //Console.WriteLine(value.First());
            }
            //using (IDbConnection db = new SqlConnection(connectionString))
            //{
            //    return db.Query<User>("SELECT * FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
            //}
        }

        public void Create(User user)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                //connection.Execute("Insert into Employee (first_name, last_name, address) values ('John', 'Smith', '123 Duane St');");
                var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age)RETURNING id";
                int? userId = connection.Execute(sqlQuery, user);
                user.Id = userId.Value;
                //connection.Open();
                // если мы хотим получить id добавленного пользователя
                // INSERT INTO teams VALUES (...) RETURNING id INTO last_id;
                //var sqlQuery_ = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age)RETURNING id INTO Id";
                //int? userId = connection.Query<int>(sqlQuery_, user).FirstOrDefault();
                //user.Id = userId.Value;
                //Console.WriteLine(value.First());
            }

            //using (IDbConnection db = new SqlConnection(connectionString))
            //{
            //    var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age)";
            //    db.Execute(sqlQuery, user);

            //    // если мы хотим получить id добавленного пользователя
            //    //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
            //    //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
            //    //user.Id = userId.Value;
            //}
        }

        public void Update(User user)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                //connection.Execute("Insert into Employee (first_name, last_name, address) values ('John', 'Smith', '123 Duane St');");
                var sqlQuery = "UPDATE Users SET Name = @Name, Age = @Age WHERE Id = @Id";
                connection.Execute(sqlQuery, user);
                //Console.WriteLine(value.First());
            }



            //using (IDbConnection db = new SqlConnection(connectionString))
            //{
            //    var sqlQuery = "UPDATE Users SET Name = @Name, Age = @Age WHERE Id = @Id";
            //    db.Execute(sqlQuery, user);
            //}
        }

        public void Delete(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                //connection.Execute("Insert into Employee (first_name, last_name, address) values ('John', 'Smith', '123 Duane St');");
                var sqlQuery = "DELETE FROM Users WHERE Id = @id";
                connection.Execute(sqlQuery, new { id });
                //Console.WriteLine(value.First());
            }



            //using (IDbConnection db = new SqlConnection(connectionString))
            //{
            //    var sqlQuery = "DELETE FROM Users WHERE Id = @id";
            //    db.Execute(sqlQuery, new { id });
            //}
        }
    }
}
