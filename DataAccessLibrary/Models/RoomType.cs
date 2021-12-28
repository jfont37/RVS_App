using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class RoomType
    {

        public int room_type_key { get; set; }
        public int room_type_category_key { get; set; }
        public string NAME { get; set; }
        public int? oa_rate_people { get; set; }
        public int? oa_rate_area { get; set; }
        public int? default_occupancy_density { get; set; }
        public int? air_class { get; set; }
        public bool unoccupied_airflow { get; set; }
        public int? exhaust_rate_bycount { get; set; }
        public int? exhaust_rate_byarea { get; set; }
    }

}



