using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using USMBAPI.Models;
using USMBAPI.Repositories;


namespace USMBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class StudentController : ControllerBase
    {

        private readonly StudentRepository studentRepository;
        private readonly IConfiguration _config;

        public StudentController(IConfiguration config)
        {
            _config = config;
            studentRepository = new StudentRepository(_config);
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return studentRepository.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return studentRepository.GetById(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Student student)
        {
            if (ModelState.IsValid)
                studentRepository.Add(student);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Student student)
        {
            student.StudentID = id;
            if (ModelState.IsValid)
                studentRepository.Update(student);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            studentRepository.Delete(id);
        }
    }
}
