using System;
using System.Collections.Generic;
using System.Text;

namespace RVS_App.Models
{
    public class ReportRow
    {
        public string site_name { get; set; }
        public string building_name { get; set; }
        public string terminal_unit_name { get; set; }
        public int? area { get; set; }
        public string air_handler_name { get; set; }
        public int? oa_required { get; set; }
        public string cooling_max { get; set; }
        public string cooling_min { get; set; }
        public string heating_max { get; set; }
        public string heating_min { get; set; }
        public string cooling_max_unocc { get; set; }
        public string cooling_min_unocc { get; set; }
        public string heating_max_unocc { get; set; }
        public string heating_min_unocc { get; set; }
    }

}


