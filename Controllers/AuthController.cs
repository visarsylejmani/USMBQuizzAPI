using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;
using USMBAPI.Models;
using USMBAPI.Repositories;
using USMBQuizzAPI.Authentication;
using USMBQuizzAPI.Helpers;

namespace USMBQuizzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthentication;
        private readonly StudentRepository studentRepository;
        private readonly ProfessorRepository professorRepository;
        private readonly IConfiguration _config;
        private readonly string salt = "JeVaisTEncrypterToiMotDePasseVisibleJeVaisTEncrypterToiMotDePasseVisibleJeVaisTEncrypterToiMotDePasseVisible";

        public AuthController(IJwtAuthenticationManager jwtAuthentication,IConfiguration config)
        {
            _config = config;
            this.jwtAuthentication = jwtAuthentication;
            studentRepository = new StudentRepository(_config);
            professorRepository = new ProfessorRepository(_config);

        }
        [Route("professor/login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] Professor professor)
        {
            string token = null;
            if (ModelState.IsValid)
            {
                Professor professorFromBDD = professorRepository.GetByEmail(professor.Email);
                if (professorFromBDD != null)
                {
                    if (PasswordHashAndVerify.VerifyHash(professor.Password, "SHA512", professorFromBDD.Password))
                    {
                        professor.Password = professorFromBDD.Password;
                        token = jwtAuthentication.Authenticate(professor);
                    }
                    else return Ok(new { Toast = "Mot de passe erroné" });
                }
                else return Ok(new { Toast = "Email erroné" });

            }

            return Ok(new { Token = token, User = professorRepository.GetByEmail(professor.Email), Toast = "Connection réussie" });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("student/login")]
        public IActionResult Authenticate([FromBody] Student student)
        {
            string token = null;
            if (ModelState.IsValid)
            {
                Student studentFromBDD = studentRepository.GetByEmail(student.Email);
                if (studentFromBDD != null)
                {
                    if (PasswordHashAndVerify.VerifyHash(student.Password, "SHA512", studentFromBDD.Password))
                    {
                        student.Password = studentFromBDD.Password;
                        token = jwtAuthentication.Authenticate(student);
                    }
                    else return Ok(new { Toast = "Mot de passe erroné" });
                }
                else return Ok(new { Toast = "Email erroné" });

            }

            return Ok(new { Token = token,User = studentRepository.GetByEmail(student.Email), Toast = "Connection réussie" });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("professor/register")]
        public IActionResult Register([FromBody] Professor professor)
        {
            string token = null;
            if (ModelState.IsValid && professorRepository.GetByEmail(professor.Email) == null)
            {
                string pass = PasswordHashAndVerify.ComputeHash(professor.Password, "SHA512", Encoding.UTF8.GetBytes(salt));
                professor.Password = pass;
                professorRepository.Add(professor);
                token = jwtAuthentication.Authenticate(professor);
            }
            if (token == null)
                return Ok( new { Toast = "Email déja utilisé" });

            return Ok(new { Token = token, User = studentRepository.GetByEmail(professor.Email), Toast = "Création de compte réussie" });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("student/register")]
        public IActionResult Register([FromBody] Student student)
        {
            string token = null;
            if (ModelState.IsValid && studentRepository.GetByEmail(student.Email) == null)
            {
                var pass = PasswordHashAndVerify.ComputeHash(student.Password, "SHA512", Encoding.ASCII.GetBytes(salt));
                student.Password = pass;
                studentRepository.Add(student);
                token = jwtAuthentication.Authenticate(student);
            }
            if (token == null)
                return Ok(new { Toast = "Email déja utilisé" });

            return Ok(new { Token = token,User=studentRepository.GetByEmail(student.Email), Toast = "Création de compte réussie" });
        }
    }
}
