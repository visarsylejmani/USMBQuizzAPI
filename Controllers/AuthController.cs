using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USMBAPI.Models;
using USMBQuizzAPI.Authentication;

namespace USMBQuizzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthentication;

        public AuthController(IJwtAuthenticationManager jwtAuthentication)
        {
            this.jwtAuthentication = jwtAuthentication;
        }
        [Route("professor/login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] Professor professor)
        {
            string token = null;
            if (ModelState.IsValid)
                token = jwtAuthentication.Authenticate(professor);
            if (token == null)
                return Unauthorized(new { Toast = "Mot de passe ou Email erroné" });

            return Ok(new { Token = token, Toast = "Connection réussie" });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("student/login")]
        public IActionResult Authenticate([FromBody] Student student)
        {
            string token = null;
            if (ModelState.IsValid)
                token = jwtAuthentication.Authenticate(student);
            if (token == null)
                return Unauthorized(new { Toast = "Mot de passe ou Email erroné" });

            return Ok(new { Token = token, Toast = "Connection réussie" });
        }
    }
}
