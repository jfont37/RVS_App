using System;
using System.Collections.Generic;
using System.Text;

namespace RVS_App.Models
{
    public class AirHandler
    {
        public int air_handler_key { get; set; }
        public int? building_key { get; set; }
        public string air_handler_name { get; set; }
        public int? area { get; set; }
        public int? population { get; set; }
        public double? ventilation_efficiency { get; set; }
        public int? supply_airflow { get; set; }
        public bool doas { get; set;}
        public double? oa_fraction { get; set; }
        public string site_name { get; set; }
        public string building_name { get; set; }
    }
}



