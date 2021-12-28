﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Building
    {
        public int building_key { get; set; }
        public int site_key { get; set; }
        public string NAME { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int? state_key { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public int? area { get; set; }
        public string site_name { get; set; }
    }

}


