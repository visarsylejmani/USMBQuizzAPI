using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USMBQuizzAPI.Models;
using USMBQuizzAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace USMBQuizzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {

        private readonly ProfessorRepository professorRepository;
        private IConfiguration _config;

        public ProfessorController(IConfiguration config)
        {
            _config = config;
            professorRepository = new ProfessorRepository(_config);
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Professor> Get()
        {
            return professorRepository.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Professor Get(int id)
        {
            return professorRepository.GetById(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Professor professor)
        {
            if (ModelState.IsValid)
                professorRepository.Add(professor);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Professor professor)
        {
            professor.ProfessorID = id;
            if (ModelState.IsValid)
                professorRepository.Update(professor);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            professorRepository.Delete(id);
        }
    }
}
