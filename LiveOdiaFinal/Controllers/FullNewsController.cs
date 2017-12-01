using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiveOdiaFinal.Controllers
{
    public class FullNewsController : ApiController
    {
        // GET: api/FullNews
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FullNews/5
        public DataTable Get(int id)
        {
            return dbutility.getFullRnews(id);
        }

        // POST: api/FullNews
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/FullNews/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FullNews/5
        public void Delete(int id)
        {
        }
    }
}
