﻿using Microsoft.Extensions.Configuration;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using USMBQuizzAPI.Models;

namespace USMBQuizzAPI.Repositories
{
    public class QuizRepository
    {
        private IConfiguration _config;

        public QuizRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection getConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(_config.GetConnectionString("Default"));
        }
        public void Add(Quiz quiz)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"INSERT INTO `Quizzes`(`Title`, `ClassID`) VALUES (@Title,@ClassID))";
            dbConnection.Open();
            dbConnection.Execute(sQuery, quiz);
        }
        public IEnumerable<Quiz> GetAll()
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"SELECT * FROM `Quizzes`";
            dbConnection.Open();
            return dbConnection.Query<Quiz>(sQuery);
        }
        public Quiz GetById(int QuizID)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"SELECT * FROM `Quizzes` WHERE `QuizID` = @QuizID";
            dbConnection.Open();
            return dbConnection.Query<Quiz>(sQuery, new { QuizID = QuizID }).FirstOrDefault();
        }

        public void Delete(int QuizID)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"DELETE FROM `Quizzes` WHERE `QuizID` = @QuizID";
            dbConnection.Open();
            dbConnection.Execute(sQuery, new { QuizID = QuizID });

        }
        public void Update(Quiz quiz)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"UPDATE `Quizzes` SET `Title`=@Title,`ClassID`=@ClassID WHERE QuizID=@QuizID";
            dbConnection.Open();
            dbConnection.Query(sQuery, quiz);

        }
    }
}
