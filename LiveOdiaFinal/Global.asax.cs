using LiveOdiaFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace LiveOdiaFinal
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Application["NoOfVisitors"] = +1;
            //string dateTime = DateTime.Now.ToLocalTime().ToString();
            //string dt = "10 / 24 / 2016 2:37:21 PM";

            //string inputText = dt.Substring(dt.Length - 10);

            DateTime t1 = DateTime.Now;

            DateTime t2 = Convert.ToDateTime("2:59:00 PM");

            int i = DateTime.Compare(t1, t2);
            string dt = t1.ToString("dd-MM-yyyy");
            AdminModel ad = new AdminModel();
            ad.updatedon = dt;
            if (i == 1)
            {
               // dbutility.updateNewsDate(ad);
            }

            dbutility.saveuserVisit(Application["NoOfVisitors"]);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        protected void Session_Start()
        {
            Application.Lock();
            Application["NoOfVisitors"] = (int)Application["NoOfVisitors"] + 1;
            Application.UnLock();
        }
    }
}
