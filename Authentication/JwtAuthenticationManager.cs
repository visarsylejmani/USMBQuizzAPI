using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using USMBAPI.Models;
using USMBAPI.Repositories;

namespace USMBQuizzAPI.Authentication
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly ProfessorRepository professorRepository;
        private readonly StudentRepository studentRepository;
        private readonly IConfiguration _config;
        private readonly string _key= "js3sRyc5Ir6RIU1wYiwhpNmeVpQbPysx";
        public JwtAuthenticationManager(IConfiguration config)
        {
            _config = config;
            professorRepository = new ProfessorRepository(_config);
            studentRepository = new StudentRepository(_config);
        }
        public string Authenticate(Professor professor)
        {
            if (professorRepository.Authenticate(professor) != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(_key);
                var tokenDecriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Email, professor.Email)
                    }),
                    Expires = DateTime.UtcNow.AddYears(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(tokenDecriptor);
                return tokenHandler.WriteToken(token);
            }
            return null;
            

        }

        public string Authenticate(Student student)
        {
            if (studentRepository.Authenticate(student) != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(_key);
                var tokenDecriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Email, student.Email)
                    }),
                    Expires = DateTime.UtcNow.AddYears(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(tokenDecriptor);
                return tokenHandler.WriteToken(token);
            }
            return null;
        }
    }
}
