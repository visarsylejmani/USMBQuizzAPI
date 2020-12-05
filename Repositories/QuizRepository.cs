using Microsoft.Extensions.Configuration;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using USMBAPI.Models;

namespace USMBAPI.Repositories
{
    public class QuizRepository
    {
        private readonly IConfiguration _config;

        public QuizRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(_config.GetConnectionString("Default"));
        }
        public void Add(Quiz quiz)
        {
            using IDbConnection dbConnection = GetConnection();
            Console.WriteLine(quiz.ClassID.GetType());
            Console.WriteLine(quiz.Title.GetType());
            string sQuery = @"INSERT INTO `Quizzes`(`Title`, `ClassID`) VALUES (@Title,@ClassID)";
            dbConnection.Open();
            dbConnection.Execute(sQuery, quiz);
        }
        public IEnumerable<Quiz> GetAll()
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Quizzes`";
            dbConnection.Open();
            return dbConnection.Query<Quiz>(sQuery);
        }

        public Quiz GetById(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Quizzes` WHERE `QuizID` = @QuizID";
            dbConnection.Open();
            return dbConnection.Query<Quiz>(sQuery, new { QuizID = id }).FirstOrDefault();
        }

        public IEnumerable<Quiz> GetByClassID(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Quizzes` WHERE `ClassID` = @ClassID";
            dbConnection.Open();
            return dbConnection.Query<Quiz>(sQuery, new { ClassID = id });
        }

        public void Delete(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"DELETE FROM `Quizzes` WHERE `QuizID` = @QuizID";
            dbConnection.Open();
            dbConnection.Execute(sQuery, new { QuizID = id });

        }

        public void Update(Quiz quiz)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"UPDATE `Quizzes` SET `Title`=@Title,`ClassID`=@ClassID WHERE QuizID=@QuizID";
            dbConnection.Open();
            dbConnection.Query(sQuery, quiz);

        }
    }
}
