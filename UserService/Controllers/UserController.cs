﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Models;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using System.Text;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public User User { get; set; }

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _db.Users.ToListAsync();
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<User>>> GetV1()
        {
            return await _db.Users.ToListAsync();
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<IEnumerable<User>>> GetV2()
        {
            return await _db.Users.ToListAsync();
        }



        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _db.Users.Find(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult CreateUser(IEnumerable<User> user)
        {
            try
            {
                foreach (var u in user)
                {
                    var usr = new User
                    {
                        Name = u.Name,
                        Address = u.Address,
                        Contact = u.Contact
                    };
                    _db.Users.Add(usr);
                    _db.SaveChanges();
                }
                return StatusCode(
                    StatusCodes.Status201Created,
                    user);
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    e);
            }

            //try
            //{
            //    var usr = new User
            //    {
            //        Name = user.Name,
            //        Address = user.Address,
            //        Contact = user.Contact
            //    };
            //    _db.Users.Add(usr);
            //    _db.SaveChanges();
            //    return StatusCode(
            //        StatusCodes.Status201Created,
            //        user);
            //}
            //catch (Exception e)
            //{
            //    return StatusCode(
            //        StatusCodes.Status500InternalServerError,
            //        e);
            //}
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var usr = _db.Users.Find(id);
            _db.Users.Remove(usr);
            _db.SaveChanges();
        }
    }
}
