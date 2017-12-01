using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using LiveOdiaFinal.Models;

namespace LiveOdiaFinal
{
    public class autoUpdate : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            string dt = DateTime.Now.ToString("dd-MM-yyyy");
            AdminModel ad = null;
            ad.updatedon = dt;
            //dbutility.updateNewsDate(ad);

            dbutility.createPdfName("a", ad);
        }
    }
}