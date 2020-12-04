using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USMBAPI.Models;
using USMBAPI.Repositories;


namespace USMBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {

        private readonly ClassRepository classRepository;
        private readonly IConfiguration _config;

        public ClassController(IConfiguration config)
        {
            _config = config;
            classRepository = new ClassRepository(_config);
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Class> Get()
        {
            return classRepository.GetAll();
        }
        [HttpGet("{id}/getquizzes")]
        public IEnumerable<Quiz> GetQuizzes(int id)
        {
            return classRepository.GetQuizzes(id);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Class Get(int id)
        {
            return classRepository.GetById(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Class _class)
        {
            if (ModelState.IsValid)
                classRepository.Add(_class);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Class _class)
        {
            _class.ClassID = id;
            if (ModelState.IsValid)
                classRepository.Update(_class);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            classRepository.Delete(id);
        }
    }
}
