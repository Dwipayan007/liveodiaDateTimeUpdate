using LiveOdiaFinal.Models;
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
    public class ImpNewsController : ApiController
    {
        // GET: api/ImpNews
        public DataTable Get()
        {
            return dbutility.GetImpNews();
        }

        // GET: api/ImpNews/5
        public DataTable Get(int id)
        {
            return dbutility.getImpNewsById(id);
        }

        // POST: api/ImpNews
        public void Post(List<ImpNews> impnews)
        {
            dbutility.ReorderImpNews(impnews);
        }

        // PUT: api/ImpNews/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ImpNews/5
        public bool Delete(int id)
        {
            bool res = false;
            DataTable dt = new DataTable();
            dt = dbutility.getImpNewsById(id);
            res = dbutility.deleteImpNews(id);
            if (res)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string file = row.Field<string>("iimage");
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
            if (Directory.Exists(Path.GetDirectoryName(path + dataColumn)))
            {
                File.Delete(dataColumn);
            }
            if (count >= 0)
                res = true;
            return res;
        }
    }
}
