using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLibrary
{
    public class RoomTypeService
    {
        private readonly SqlDataAccess _db;

        public RoomTypeService(SqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<RoomType>> GetRoomTypes()
        {
            var sql = @"SELECT room_type.* 
                        FROM room_type 
                            left join room_type_category on room_type_category.room_type_category_key = room_type.room_type_category_key
                        ORDER BY room_type.""NAME""";

            return _db.LoadData<RoomType, dynamic>(sql, new { });
        }

        public Task InsertRoomType(RoomType room_type)
        {
            var room_type_category_key      = room_type.room_type_category_key;            
            var room_name                   = room_type.NAME;
            var oa_rate_people              = room_type.oa_rate_people;
            var oa_rate_area                = room_type.oa_rate_area;
            var default_occupancy_density   = room_type.default_occupancy_density;
            var air_class                   = room_type.air_class;
            var unoccupied_airflow          = room_type.unoccupied_airflow;
            var exhaust_rate_bycount        = room_type.exhaust_rate_bycount;
            var exhaust_rate_byarea         = room_type.exhaust_rate_byarea;

            var sql = @"INSERT INTO public.room_type(
	                  room_type_key, room_type_category_key, ""NAME"", oa_rate_people, oa_rate_area, default_occupancy_density, air_class, unoccupied_airflow, exhaust_rate_bycount, exhaust_rate_byarea)
                      VALUES ('"+ room_type_category_key + "', '"+ room_name + "', '"+ oa_rate_people + "', '"+ oa_rate_area + "', '"+ default_occupancy_density + "', '"+ air_class + "', " + unoccupied_airflow + "', " 
                      + exhaust_rate_bycount + "', " + exhaust_rate_byarea +");";

            return _db.SaveData(sql, room_type);
        }

        public Task UpdateRoomType(RoomType room_type)
        {
            var room_type_key = room_type.room_type_key;
            var room_type_category_key = room_type.room_type_category_key;
            var NAME = room_type.NAME;
            var oa_rate_people = room_type.oa_rate_people;
            var oa_rate_area = room_type.oa_rate_area;
            var default_occupancy_density = room_type.default_occupancy_density;
            var air_class = room_type.air_class;
            var unoccupied_airflow = room_type.unoccupied_airflow;
            var exhaust_rate_bycount = room_type.exhaust_rate_bycount;
            var exhaust_rate_byarea = room_type.exhaust_rate_byarea;

            var sql = @"update room_type SET room_type_category_key = '" + room_type_category_key +
                "', NAME = '" + NAME +
                "', oa_rate_people = '" + oa_rate_people +
                "', oa_rate_area = '" + oa_rate_area +
                "', default_occupancy_density = '" + default_occupancy_density +
                "', air_class = '" + air_class + "'" +
                "', unoccupied_airflow = '" + unoccupied_airflow + "'" +
                "', exhaust_rate_bycount = '" + exhaust_rate_bycount + "'" +
                "', exhaust_rate_byarea = '" + exhaust_rate_byarea + "'" +
                " where room_type_key = " + room_type_key
                ;

            return _db.SaveData(sql, room_type);
        }

        public Task DeleteRoom(RoomType room_type)
        {
            var roomTypeKey = room_type.room_type_key;
            var sql = @"delete from room_type where room_type.key = " + roomTypeKey;

            return _db.SaveData(sql, roomTypeKey);
        }
    }
}
