using System;
using System.Collections.Generic;
using System.Text;

namespace RVS_App.Models
{
    public class Room
    {
        public int room_key { get; set; }
        public int? building_key { get; set; }
        public int? air_handler_key { get; set; }
        public int? room_type_key { get; set; }
        public int? terminal_unit_key { get; set; }
        public string room_number { get; set; }
        public string room_name { get; set; }
        public int area { get; set; }
        public int? population { get; set; }
        public int? supply_airflow { get; set; }
        public int? return_airflow { get; set; }
        public int? exhaust_airflow { get; set; }
        public double? ventilation_efficiency { get; set; } = 1;
        public bool critical { get; set; } = false;
        public int? oa_required { get; set; }
        public string building_level { get; set; }
        public int? height { get; set; }
        public int? fixture_qty { get; set; }
        public string notes { get; set; }
        public string site_name { get; set; }
        public string building_name { get; set; }
        public string room_type_name { get; set; }
        public string air_handler_name { get; set; }
        public string terminal_unit_name { get; set; }
        public int volume { get; set; }

    }

}



