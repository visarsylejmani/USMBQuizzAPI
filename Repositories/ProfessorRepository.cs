using Microsoft.Extensions.Configuration;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using USMBAPI.Models;

namespace USMBAPI.Repositories
{
    public class ProfessorRepository
    {
        private readonly IConfiguration _config;

        public ProfessorRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(_config.GetConnectionString("Default"));
        }
        public void Add(Professor professor)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"INSERT INTO `Professors`(`Firstname`, `Lastname`, `Email`, `Password`) VALUES (@Firstname,@Lastname,@Email,@Password)";
            dbConnection.Open();
            dbConnection.Execute(sQuery, professor);
        }

        internal bool Authenticate(Professor professor)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Professors` WHERE `Email` = @Email AND `Password` = @Password";
            dbConnection.Open();
            return dbConnection.Query(sQuery, professor).FirstOrDefault();
        }

        public IEnumerable<Professor> GetAll()
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Professors`";
            dbConnection.Open();
            return dbConnection.Query<Professor>(sQuery);
        }
        public Professor GetById(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"SELECT * FROM `Professors` WHERE `ProfessorID` = @ProfessorID";
            dbConnection.Open();
            return dbConnection.Query<Professor>(sQuery, new { ProfessorID = id }).FirstOrDefault();
        }

        public void Delete(int id)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"DELETE FROM `Professors` WHERE `ProfessorID` = @ProfessorID";
            dbConnection.Open();
            dbConnection.Execute(sQuery, new { ProfessorID = id });

        }
        public void Update(Professor professor)
        {
            using IDbConnection dbConnection = GetConnection();
            string sQuery = @"UPDATE `Professors` SET `Firstname`=@Firstname,`Lastname`=@Lastname,`Email`=@Email,`Password`=@Password WHERE `ProfessorID` = @ProfessorID";
            dbConnection.Open();
            dbConnection.Query(sQuery, professor);

        }
    }
}
