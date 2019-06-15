using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetMeALibrary.Model;
using GetMeALibrary.Sql;
using Microsoft.AspNetCore.Mvc;

namespace GetMeALifeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Database context = GetConnectionToDB();
            var users = context.Query<User>("SELECT * FROM USER");
            string[] values = new string[users.Count];
            for (int i = 0; i < users.Count; i++)
                values[i] = users[i].ToString();

            return values;
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private Database GetConnectionToDB()
        {
            return new Database("server=35.238.128.54;port=3306;database=team20_db;user=team20_user;password=donuts are tasty;");
        }
    }
}
