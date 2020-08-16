using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public IActionResult Get()
        {
            return Ok(UserRestService.Models.User.GetAll());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(UserRestService.Models.User.GetUser(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JObject jsonResult)
        {
            //UserRestService.Models.User user = JsonConvert.DeserializeObject<UserRestService.Models.User>(jsonResult.ToString());
            int res = await UserRestService.Models.User.Create(jsonResult);
            return Ok(res);
        }
        [HttpPost("check-password")]
        public IActionResult CheckPassword([FromBody] JObject jsonResult)
        {
            PasswordObj vals = JsonConvert.DeserializeObject<PasswordObj>(jsonResult.ToString());
            return Ok(UserRestService.Models.User.CheckPassword(vals.Password, vals.UserEmail));
        }
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] JObject jObject, string id)
        {
            //UserRestService.Models.User user = JsonConvert.DeserializeObject<UserRestService.Models.User>(jObject.ToString());
            if (int.TryParse(id, out int intId))
            {
                //user.UserID = intId;
                UserRestService.Models.User.Update(jObject, intId);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
