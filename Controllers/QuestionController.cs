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

    public class QuestionController : ControllerBase
    {

        private readonly QuestionRepository questionRepository;
        private readonly IConfiguration _config;

        public QuestionController(IConfiguration config)
        {
            _config = config;
            questionRepository = new QuestionRepository(_config);
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return questionRepository.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Question Get(int id)
        {
            return questionRepository.GetById(id);
        }

        [HttpGet]
        [Route("GetByQuizID/{id}")]
        public IEnumerable<Question> GetByQuizID(int id)
        {
            return questionRepository.GetByQuizID(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Question question)
        {
            if (ModelState.IsValid)
                questionRepository.Add(question);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Question question)
        {
            question.QuestionID = id;
            if (ModelState.IsValid)
                questionRepository.Update(question);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            questionRepository.Delete(id);
        }
    }
}
