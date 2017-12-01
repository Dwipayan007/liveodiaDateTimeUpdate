using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveOdiaFinal.Models
{
    public class HotNewsModel
    {
        public int HID { get; set; }
        public string TITLE { get; set; }
        public string Hsub { get; set; }

        public string HotNews { get; set; }
        public string HotImg { get; set; }
        public string newsDate { get; set; }
    }
}