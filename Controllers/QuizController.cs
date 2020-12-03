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
    public class QuizController : ControllerBase
    {

        private readonly QuizRepository quizRepository;
        private IConfiguration _config;

        public QuizController(IConfiguration config)
        {
            _config = config;
            quizRepository = new QuizRepository(_config);
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Quiz> Get()
        {
            return quizRepository.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Quiz Get(int id)
        {
            return quizRepository.GetById(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Quiz quiz)
        {
            if (ModelState.IsValid)
                quizRepository.Add(quiz);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Quiz quiz)
        {
            quiz.QuizID = id;
            if (ModelState.IsValid)
                quizRepository.Update(quiz);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            quizRepository.Delete(id);
        }
    }
}
