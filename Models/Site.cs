using System;
using System.Collections.Generic;
using System.Text;

namespace RVS_App.Models
{
    public class Site
    {
        public int site_key { get; set; }
        public string site_name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int? state_key { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
    }
}
