using System;
using System.Collections.Generic;
using System.Text;

namespace RVS_App.Models
{
    public class TerminalUnit
    {
        public int terminal_unit_key { get; set; }
        public int? building_key { get; set; }
        public int? air_handler_key { get; set; }
        public string terminal_unit_name { get; set; }
        public int? area { get; set; }
        public int? oa_required { get; set; }
        public int? cooling_min_airflow { get; set; }
        public int? cooling_max_airflow { get; set; }
        public int? heating_min_airflow { get; set; }
        public int? heating_max_airflow { get; set; }
        public int? cooling_min_airflow_unocc { get; set; }
        public int? cooling_max_airflow_unocc { get; set; }
        public int? heating_min_airflow_unocc { get; set; }
        public int? heating_max_airflow_unocc { get; set; }
        public string air_handler_name { get; set; }
        public string site_name { get; set; }
        public string building_name { get; set; }
    }
}



