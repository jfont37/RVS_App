using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class AirHandler
    {
        public int air_handler_key { get; set; }
        public string NAME { get; set; }
        public string area { get; set; }
        public int? occupant_qty { get; set; }
        public int? supply_airflow { get; set; }
        public int? site_key { get; set; }
        public int? ventilation_efficiency { get; set; }
    }
}



