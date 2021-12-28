using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataAccessLibrary
{
    public class RoomService
    {
        private readonly SqlDataAccess _db;

        public RoomService(SqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<Room>> GetBuildingRooms(int building_key)
        {

        var sql = @"SELECT room.*
                        ,building.""NAME"" building_name
                        ,site.""NAME"" site_name
                FROM room 
                    left join building on building.building_key = room.building_key
                    left join site on site.site_key = building.site_key
                WHERE room.building_key = " + building_key + 
              " ORDER BY room.room_number";

            return _db.LoadData<Room, int>(sql, building_key);
        }

        public Task<List<Room>> GetRoom()
        {
            var sql = @"SELECT room.*
                        FROM room
                        WHERE room_key = @room.room_key";

            return _db.LoadData<Room, dynamic>(sql, new { });
        }

        public Task InsertRoom(Room room)
        {
            var sql = @"INSERT INTO public.room(
	                    building_key, room_type_key, room_number, room_name, area, population, supply_airflow, ventilation_efficiency, critical, oa_required, building_level, height, fixture_qty)
	                VALUES (@building_key, @room_type_key, @room_number, @room_name, @area, @population, @supply_airflow, @ventilation_efficiency, @critical, @oa_required, @building_level, @height, @fixture_qty);";

            return _db.SaveData(sql, room);
        }

        public Task UpdateRoom(Room room)
        {
            var sql = @"UPDATE room SET building_key = @building_key
                            , air_handler_key = @air_handler_key
                            , terminal_unit_key = @terminal_unit_key
                            , room_type_key = @room_type_key
                            , room_number = @room_number
                            , room_name = @room_name 
                            , area = @area 
                            , population = @population 
                            , supply_airflow = @supply_airflow
                            , ventilation_efficiency = @ventilation_efficiency
                            , critical = @critical
                            , oa_required = @oa_required
                            , building_level = @building_level
                            , height = @height
                            , fixture_qty = @fixture_qty
                        WHERE room_key = @room_key";

            return _db.SaveData(sql, room);
        }

        public Task DeleteRoom(Room room)
        {
            var roomKey = room.room_key;
            var sql = @"delete from room where room_key = " + roomKey;

            return _db.SaveData(sql, roomKey);
        }
    }
}
