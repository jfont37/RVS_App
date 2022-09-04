using System;
using System.Collections.Generic;
using System.Text;

namespace RVS_App.Models
{
    public class RoomType
    {

        public int room_type_key { get; set; }
        public int room_type_category_key { get; set; }
        public string room_type_name { get; set; }
        public int? oa_rate_people { get; set; }
        public double? oa_rate_area { get; set; }
        public int? default_occupancy_density { get; set; }
        public int? air_class { get; set; }
        public bool unoccupied_airflow { get; set; }
        public int? exhaust_rate_bycount { get; set; }
        public double? exhaust_rate_byarea { get; set; }
        public string room_type_category_name { get; set; }
        public bool critical { get; set; }
        public int air_changes_per_hour { get; set; }
        public int oa_air_changes_per_hour { get; set; }
        public string pressure_relationship { get; set; }
        public bool exhaust_all { get; set; }
    }

}



