﻿using Microsoft.Extensions.Configuration;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using USMBAPI.Models;

namespace USMBAPI.Repositories
{
    public class ClassRepository
    {
        private readonly IConfiguration _config;

        public ClassRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(_config.GetConnectionString("Default"));
        }
        public void Add(Class _class)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"INSERT INTO `Classes`(`Name`, `Passkey`, `ProfessorID`) VALUES (@Name,@Passkey,@ProfessorID)";
            dbConnection.Open();
            dbConnection.Execute(sQuery, _class);
        }
        public IEnumerable<Class> GetAll()
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Classes`";
            dbConnection.Open();
            return dbConnection.Query<Class>(sQuery);
        }
        public IEnumerable<Quiz> GetQuizzes(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Quizzes` WERE `ClassID`=@ClassID";
            dbConnection.Open();
            return dbConnection.Query<Quiz>(sQuery, new { ClassID = id });
        }

        public Class GetById(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Classes` WHERE `ClassID` = @ClassID";
            dbConnection.Open();
            return dbConnection.Query<Class>(sQuery, new { ClassID = id }).FirstOrDefault();
        }

        public void Delete(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"DELETE FROM `Classes` WHERE `ClassID` = @ClassID";
            dbConnection.Open();
            dbConnection.Execute(sQuery, new { ClassID = id });

        }
        public void Update(Class _class)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"UPDATE `Classes` SET `Name`=@Name,`Passkey`=@Passkey,`ProfessorID`=@ProfessorID WHERE `ClassID`= @ClassID";
            dbConnection.Open();
            dbConnection.Query(sQuery, _class);

        }
    }
}
