using RVS_App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVS_App
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
            var sql = @"SELECT room_type.*,
                            room_type_category.room_type_category_name
                        FROM room_type 
                            left join room_type_category on room_type_category.room_type_category_key = room_type.room_type_category_key
                        ORDER BY room_type_category.room_type_category_name, room_type.room_type_name";

            return _db.LoadData<RoomType, dynamic>(sql, new { });
        }

        public Task InsertRoomType(RoomType room_type)
        {
            var sql = @"INSERT INTO public.room_type (room_type_key, room_type_category_key, room_type_name, oa_rate_people, oa_rate_area, default_occupancy_density, air_class, unoccupied_airflow, exhaust_rate_bycount, exhaust_rate_byarea)
                      VALUES (@room_type_key, @room_type_category_key, @room_type_name, @oa_rate_people, @oa_rate_area, @default_occupancy_density, @air_class, @unoccupied_airflow, @exhaust_rate_bycount, @exhaust_rate_byarea)";

            return _db.SaveData(sql, room_type);
        }

        public Task UpdateRoomType(RoomType room_type)
        {
            var sql = @"update room_type SET room_type_category_key = @room_type_category_key,
                        ,room_type_name = @room_type_name
                        ,oa_rate_people = @oa_rate_people
                        ,oa_rate_area = @oa_rate_area
                        ,default_occupancy_density = @default_occupancy_density
                        ,air_class = @air_class
                        ,unoccupied_airflow = @unoccupied_airflow
                        ,exhaust_rate_bycount = @exhaust_rate_bycount
                        ,exhaust_rate_byarea = @exhaust_rate_byarea
                        where room_type_key = @room_type_key";

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
