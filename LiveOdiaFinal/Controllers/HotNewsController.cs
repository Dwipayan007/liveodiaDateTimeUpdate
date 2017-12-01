using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiveOdiaFinal.Controllers
{
    public class HotNewsController : ApiController
    {
        // GET: api/HotNews
        public DataTable Get()
        {
            return dbutility.GetHotNewsTitle();
        }

        // GET: api/HotNews/5
        public DataTable Get(int id)
        {
            return dbutility.getHotNewsSummary(id);
        }

        // POST: api/HotNews
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/HotNews/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/HotNews/5
        public bool Delete(int id)
        {

            bool res = false;
            res = dbutility.deleteHotNews(id);
            return res;
        }
    }
}
