using LiveOdiaFinal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiveOdiaFinal.Controllers {
    public class RelatedNewsController : ApiController {
        // GET: api/RelatedNews
        public DataTable Get() {
            return dbutility.getRelated();
        }

        // GET: api/RelatedNews/5
        public DataTable Get(int id) {
            return dbutility.getRelatedNewsById(id);
        }

        // POST: api/RelatedNews
        public bool Post(Related rdt) {
            return dbutility.addRelated(rdt);

        }

        // PUT: api/RelatedNews/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE: api/RelatedNews/5
        public void Delete(int id) {
        }
    }
}
