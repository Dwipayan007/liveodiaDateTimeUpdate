using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LiveOdiaFinal.Controllers {
    public class EditNewsController : ApiController {
        // GET: api/EditNews
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET: api/EditNews/5
        public string Get(int id) {
            return "value";
        }

        // POST: api/EditNews
        public async Task<HttpResponseMessage> Post() {
            // const string StoragePath = "~/UploadedImage";
            bool res = false;
            Dictionary<string, string> myData = new Dictionary<string, string>();
            string StoragePath = HttpContext.Current.Server.MapPath("~/UploadedImage/");
            if (Request.Content.IsMimeMultipartContent()) {
                try {
                    string fname = "";
                    var streamProvider = new MultipartFormDataStreamProvider(StoragePath);
                    await Request.Content.ReadAsMultipartAsync(streamProvider);
                    foreach (MultipartFileData fileData in streamProvider.FileData) {
                        if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName)) {
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                        }
                        string fileName = fileData.Headers.ContentDisposition.FileName;

                        if (fileName.StartsWith("\"") && fileName.EndsWith("\"")) {
                            fileName = fileName.Trim('"');
                            fname = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + "_" + fileName;
                            myData.Add("img", fname);
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\")) {
                            fname = Path.GetFileName(fileName);

                        }
                        File.Copy(fileData.LocalFileName, Path.Combine(StoragePath, fname));
                    }
                    foreach (var key in streamProvider.FormData.AllKeys) {
                        foreach (var val in streamProvider.FormData.GetValues(key)) {
                            myData.Add(key, val);
                        }
                    }
                    if (streamProvider.FileData.Count == 0) {
                        myData.Add("img", "Default");
                    }

                    myData.Add("updatedon", DateTime.Now.ToString());

                    dbutility.updateNewsData(myData);
                } catch (Exception e) {
                    Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            } else {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
            }
        }

        // PUT: api/EditNews/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE: api/EditNews/5
        public void Delete(int id) {
        }
    }
}
