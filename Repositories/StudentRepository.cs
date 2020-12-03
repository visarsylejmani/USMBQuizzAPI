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
    public class StudentRepository
    {
        private IConfiguration _config;

        public StudentRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection getConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(_config.GetConnectionString("Default"));
        }
        public void Add(Student student)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"INSERT INTO `Students`(`Firstname`, `Lastname`, `Email`, `Password`) VALUES (@Firstname,@Lastname,@Email,@Password)";
            dbConnection.Open();
            dbConnection.Execute(sQuery, student);
        }
        public IEnumerable<Student> GetAll()
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"SELECT * FROM `Students`";
            dbConnection.Open();
            return dbConnection.Query<Student>(sQuery);
        }
        public Student GetById(int StudentID)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"SELECT * FROM `Students` WHERE `StudentID` = @StudentID";
            dbConnection.Open();
            return dbConnection.Query<Student>(sQuery, new { StudentID = StudentID }).FirstOrDefault();
        }

        public void Delete(int StudentID)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"DELETE FROM `Students` WHERE `StudentID` = @StudentID";
            dbConnection.Open();
            dbConnection.Execute(sQuery, new { StudentID = StudentID });

        }
        public void Update(Student student)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"UPDATE `Students` SET `Firstname`=@Firstname,`Lastname`=@Lastname,`Email`=@Email,`Password`=@Password WHERE `StudentID` = @StudentID";
            dbConnection.Open();
            dbConnection.Query(sQuery, student);

        }
    }
}