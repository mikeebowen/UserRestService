using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UserRepository.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private class PasswordObj
        {
            public string Password { get; set; }
            public string UserEmail { get; set; }
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<UserRestService.Models.User> Get()
        {
            return UserRestService.Models.User.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<int> Post([FromBody] JObject jsonResult)
        {
            UserDTO userDTO = JsonConvert.DeserializeObject<UserDTO>(jsonResult.ToString());
            int res = await UserDTO.Create(userDTO);
            return res;
        }
        [HttpPost("check-password")]
        public bool CheckPassword([FromBody] JObject jsonResult)
        {
            PasswordObj vals = JsonConvert.DeserializeObject<PasswordObj>(jsonResult.ToString());
            return UserDTO.CheckPassword(vals.Password, vals.UserEmail);
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
        }
    }
}
