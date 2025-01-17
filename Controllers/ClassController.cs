﻿using Microsoft.AspNetCore.Authorization;
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

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Class Get(int id)
        {
            return classRepository.GetById(id);
        }
        [HttpGet]
        [Route("GetByProfessorID/{id}")]
        public IEnumerable<Class> GetByProfessorID(int id)
        {
            return classRepository.GetByProfessorID(id);
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
