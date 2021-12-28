using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class TerminalUnit
    {
        public int terminal_unit_key { get; set; }
        public int air_handler_key { get; set; }
        public string NAME { get; set; }
        public string area { get; set; }
        public int? oa_required { get; set; }
        public int? cooling_min_airflow { get; set; }
        public int? cooling_max_airflow { get; set; }
        public int? heating_min_airflow { get; set; }
        public int? heating_max_airflow { get; set; }
        public int? cooling_min_airflow_unocc { get; set; }
        public int? cooling_max_airflow_unocc { get; set; }
        public int? heating_min_airflow_unocc { get; set; }
        public int? heating_max_airflow_unocc { get; set; }
    }
}



