using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Room
    {
        public int room_key { get; set; }
        public int? building_key { get; set; } = null;
        public int? air_handler_key { get; set; } = null;
        public int? room_type_key { get; set; } = null;
        public int? terminal_unit_key { get; set; } = null;
        public string room_number { get; set; }
        public string room_name { get; set; }
        public int area { get; set; } = 1;
        public int? population { get; set; } = 1;
        public int? supply_airflow { get; set; } = 1;
        public double? ventilation_efficiency { get; set; } = .9;
        public bool critical { get; set; } = false;
        public int? oa_required { get; set; } = 1;
        public string building_level { get; set; } = "1";
        public int? height { get; set; } = 1;
        public int? fixture_qty { get; set; } = 0;
        public string notes { get; set; }
        public string site_name { get; set; }
        public string building_name { get; set; }
    }

}



