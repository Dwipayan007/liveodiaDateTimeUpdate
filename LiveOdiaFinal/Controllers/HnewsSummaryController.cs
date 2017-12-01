using LiveOdiaFinal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiveOdiaFinal.Controllers
{
    public class HnewsSummaryController : ApiController
    {
        // GET: api/HnewsSummary
        public DataTable Get()
        {
            return dbutility.getAllTopStory();
        }

        // GET: api/HnewsSummary/5
        public DataTable Get(int id)
        {
            return dbutility.getHotNewsSummaryDetail(id);
        }

        // POST: api/HnewsSummary
        //public bool Post(AdminModel newsDate)
        //{
        //    bool res = false;
        //    return res = dbutility.updateNewsDate(newsDate);

        //}

        // PUT: api/HnewsSummary/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/HnewsSummary/5
        public void Delete(int id)
        {
        }
    }
}
