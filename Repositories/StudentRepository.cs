using Microsoft.Extensions.Configuration;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using USMBAPI.Models;

namespace USMBAPI.Repositories
{
    public class StudentRepository
    {
        private readonly IConfiguration _config;

        public StudentRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(_config.GetConnectionString("Default"));
        }
        public void Add(Student student)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"INSERT INTO `Students`(`Firstname`, `Lastname`, `Email`, `Password`) VALUES (@Firstname,@Lastname,@Email,@Password)";
            dbConnection.Open();
            dbConnection.Execute(sQuery, student);
        }
        public IEnumerable<Student> GetAll()
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Students`";
            dbConnection.Open();
            return dbConnection.Query<Student>(sQuery);
        }
        public Student GetById(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Students` WHERE `StudentID` = @StudentID";
            dbConnection.Open();
            return dbConnection.Query<Student>(sQuery, new { StudentID = id }).FirstOrDefault();
        }

        public void Delete(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"DELETE FROM `Students` WHERE `StudentID` = @StudentID";
            dbConnection.Open();
            dbConnection.Execute(sQuery, new { StudentID = id });

        }
        public void Update(Student student)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"UPDATE `Students` SET `Firstname`=@Firstname,`Lastname`=@Lastname,`Email`=@Email,`Password`=@Password WHERE `StudentID` = @StudentID";
            dbConnection.Open();
            dbConnection.Query(sQuery, student);

        }

        public Student Authenticate(Student student)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Students` WHERE `Email` = @Email AND `Password` = @Password";
            dbConnection.Open();
            return dbConnection.Query<Student>(sQuery, student).FirstOrDefault();
        }

        public Student GetByEmail(string email)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Students` WHERE `Email` = @Email";
            dbConnection.Open();
            return dbConnection.Query<Student>(sQuery, new { Email = email }).FirstOrDefault();
        }
    }
}
