using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetMeALibrary.Model;
using GetMeALibrary.Sql;
using GetMeALifeLibrary.GraphQL.GraphQLSchema;
using Microsoft.AspNetCore.Mvc;

namespace GetMeALifeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetMeALifeController : ControllerBase
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
        }

        private Database GetConnectionToDB()
        {
            return new Database("server=35.238.128.54;port=3306;database=team20_db;user=team20_user;password=donuts are tasty;");
        }
    }
}
