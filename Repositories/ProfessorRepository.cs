using Microsoft.Extensions.Configuration;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using USMBQuizzAPI.Models;

namespace USMBQuizzAPI.Repositories
{
    public class ProfessorRepository
    {
        private IConfiguration _config;

        public ProfessorRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection getConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(_config.GetConnectionString("Default"));
        }
        public void Add(Professor professor)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"INSERT INTO `Professors`(`Firstname`, `Lastname`, `Email`, `Password`) VALUES (@Firstname,@Lastname,@Email,@Password)";
            dbConnection.Open();
            dbConnection.Execute(sQuery, professor);
        }
        public IEnumerable<Professor> GetAll()
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"SELECT * FROM `Professors`";
            dbConnection.Open();
            return dbConnection.Query<Professor>(sQuery);
        }
        public Professor GetById(int ProfessorID)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"SELECT * FROM `Professors` WHERE `ProfessorID` = @ProfessorID";
            dbConnection.Open();
            return dbConnection.Query<Professor>(sQuery, new { ProfessorID = ProfessorID }).FirstOrDefault();
        }

        public void Delete(int ProfessorID)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"DELETE FROM `Professors` WHERE `ProfessorID` = @ProfessorID";
            dbConnection.Open();
            dbConnection.Execute(sQuery, new { ProfessorID = ProfessorID });

        }
        public void Update(Professor professor)
        {
            using IDbConnection dbConnection = getConnection();
            string sQuery = @"UPDATE `Professors` SET `Firstname`=@Firstname,`Lastname`=@Lastname,`Email`=@Email,`Password`=@Password WHERE `ProfessorID` = @ProfessorID";
            dbConnection.Open();
            dbConnection.Query(sQuery, professor);

        }
    }
}
