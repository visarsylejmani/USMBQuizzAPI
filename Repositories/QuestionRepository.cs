using Microsoft.Extensions.Configuration;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using USMBAPI.Models;

namespace USMBAPI.Repositories
{
    public class QuestionRepository
    {
        private readonly IConfiguration _config;

        public QuestionRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(_config.GetConnectionString("Default"));
        }
        public void Add(Question question)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"INSERT INTO `Questions`(`Description`, `QuizID`) VALUES (@Description,@QuizID)";
            dbConnection.Open();
            dbConnection.Execute(sQuery, question);
        }
        public IEnumerable<Question> GetAll()
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Questions`";
            dbConnection.Open();
            return dbConnection.Query<Question>(sQuery);
        }

        public Question GetById(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Questions` WHERE `QuestionID` = @QuestionID";
            dbConnection.Open();
            return dbConnection.Query<Question>(sQuery, new { QuestionID = id }).FirstOrDefault();
        }

        public IEnumerable<Question> GetByQuizID(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Questions` WHERE `QuizID` = @QuizID";
            dbConnection.Open();
            return dbConnection.Query<Question>(sQuery, new { QuizID = id });
        }

        public void Delete(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"DELETE FROM `Questions` WHERE `QuestionID` = @QuestionID";
            dbConnection.Open();
            dbConnection.Execute(sQuery, new { QuestionID = id });

        }

        public void Update(Question question)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"UPDATE `Questions` SET `Description`=@Description,`QuizID`=@QuizID WHERE QuestionID=@QuestionID";
            dbConnection.Open();
            dbConnection.Query(sQuery, question);

        }
    }
}
