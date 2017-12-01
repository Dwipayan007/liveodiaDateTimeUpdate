using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LiveOdiaFinal.Controllers
{
    public class NewStoryController : ApiController
    {
        // GET: api/NewStory
        public DataTable Get()
        {
            return dbutility.getAllNewStory();
        }

        // GET: api/NewStory/5
        public DataTable Get(int id)
        {
            return dbutility.getNewstoryById(id);
        }

        // POST: api/NewStory
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/NewStory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/NewStory/5
        public bool Delete(int id)
        {
            bool res = false;
            DataTable dt = new DataTable();
            dt = dbutility.getNewstoryById(id);
            res = dbutility.deleteNewsStory(id);
            if (res)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string file = row.Field<string>("nimage");
                    deleteFileFromDisk(file);
                }
            }
            return res;
        }

        private bool deleteFileFromDisk(string dataColumn)
        {
            bool res = false;
            int count = 0;

            string path = HttpContext.Current.Server.MapPath("~/UploadedImage/");
            DirectoryInfo di = new DirectoryInfo(path);
            if (Directory.Exists(Path.GetDirectoryName(path+dataColumn)))
            {
                File.Delete(dataColumn);
            }
            if (count >= 0)
                res = true;
            return res;
        }
    }
}
