using RVS_App.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using OfficeOpenXml;
using System.IO;
using System.Linq;

namespace RVS_App
{
    public class RoomService
    {
        private readonly SqlDataAccess _db;
        private readonly AirHandlerService _airHandlerSerrvice;
        private readonly TerminalUnitService _terminalUnitService;

        public RoomService(SqlDataAccess db, AirHandlerService ah, TerminalUnitService tu)
        {
            _db = db;
            _airHandlerSerrvice = ah;
            _terminalUnitService = tu;  
        }

        public Task<List<Room>> GetBuildingRooms(int building_key)
        {

        var sql = @"SELECT room.*
                        ,building.building_name
                        ,site.site_name
                        ,room_type.room_type_name
                        ,air_handler.air_handler_name
                        ,terminal_unit.terminal_unit_name
                FROM room 
                    left join building on building.building_key = room.building_key
                    left join site on site.site_key = building.site_key
                    left join room_type on room_type.room_type_key = room.room_type_key
                    left join air_handler on air_handler.air_handler_key = room.air_handler_key
                    left join terminal_unit on terminal_unit.terminal_unit_key = room.terminal_unit_key
                WHERE room.building_key = " + building_key + 
              " ORDER BY room.room_number";

            return _db.LoadData<Room, int>(sql, building_key);
        }

        public Task<List<Room>> GetRoom(int room_key)
        {
            var sql = @"SELECT room.*
                        FROM room
                        WHERE room_key = " + room_key;

            return _db.LoadData<Room, dynamic>(sql, new { });
        }

        public Task InsertRoom(Room room)
        {
            var volume = room.area * room.height;
            var sql = @"INSERT INTO public.room(
	                    building_key, air_handler_key, room_type_key, terminal_unit_key, room_number, room_name, area, population, supply_airflow, return_airflow, exhaust_airflow, ventilation_efficiency, critical, oa_required, building_level, height, fixture_qty, notes, volume)
	                VALUES (@building_key, @air_handler_key, @room_type_key, @terminal_unit_key, @room_number, @room_name, @area, @population, @supply_airflow, @return_airflow, @exhaust_airflow, @ventilation_efficiency, @critical, @oa_required, @building_level, @height, @fixture_qty, @notes, " + volume + ")";
            
            return _db.SaveData(sql, room);
        }

        public Task UpdateRoom(Room room)
        {
            var volume = room.area * room.height;
            var sql = @"UPDATE room SET building_key = @building_key
                            , air_handler_key = @air_handler_key
                            , terminal_unit_key = @terminal_unit_key
                            , room_type_key = @room_type_key
                            , room_number = @room_number
                            , room_name = @room_name 
                            , area = @area 
                            , population = @population 
                            , supply_airflow = @supply_airflow
                            , return_airflow = @return_airflow
                            , exhaust_airflow = @exhaust_airflow
                            , ventilation_efficiency = @ventilation_efficiency
                            , critical = @critical
                            , oa_required = @oa_required
                            , building_level = @building_level
                            , height = @height
                            , fixture_qty = @fixture_qty
                            , notes = @notes
                            , volume = " + volume +
                        " WHERE room_key = @room_key";

            return _db.SaveData(sql, room);
        }

        //use this one once we can pass fileinfo in with method call
        //public async Task <List<Room>> ImportRoomsFromExcel(FileInfo file, int bldgkey)
        public async Task<List<Room>> ImportRoomsFromExcel(String file1, int bldgkey)
        {
            //this part is just for testing until i figure out how to pass fileinfo in with method call from front end.
            var file2 = new FileInfo(file1);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var rooms = new List<Room>();
            using var package = new ExcelPackage(file2);
            await package.LoadAsync(file2);

            var worksheet = package.Workbook.Worksheets[0];

            //use these to change which row and column the importer starts on
            int row = 1;
            int col = 1;    

            while (string.IsNullOrWhiteSpace(worksheet.Cells[row,col].Value?.ToString()) ==false)
            {
                Room r = new Room();
                r.building_key = bldgkey;   
                r.room_number = worksheet.Cells[row,col].Value.ToString();
                r.room_name = worksheet.Cells[row, col + 1].Value.ToString();
                r.area = int.Parse(worksheet.Cells[row, col + 2].Value.ToString());
                r.height = int.Parse(worksheet.Cells[row, col + 4].Value.ToString());

                //check if terminal unit already in database


                
                List<TerminalUnit> tus = new List<TerminalUnit>();
                var tu = new TerminalUnit();
                tu.building_key = bldgkey;
                tu.terminal_unit_name = worksheet.Cells[row, col + 3].Value.ToString();

                //check if terminal unit already in database if not insert tu name from spreadsheet and bldg key
                tus = await _terminalUnitService.GetImportedTerminalUnit(bldgkey, tu.terminal_unit_name);
                var tu3 = tus.FirstOrDefault();
                if(tu3 != null)
                {
                    r.terminal_unit_key = tu3.terminal_unit_key;
                }

                if (tu3 == null)
                {
                    await _terminalUnitService.InsertTerminalUnit(tu);
                    tus = await _terminalUnitService.GetImportedTerminalUnit(bldgkey, tu.terminal_unit_name);
                    var tu4 = tus.FirstOrDefault();
                    r.terminal_unit_key = tu4.terminal_unit_key;
                }
                

                await InsertRoom(r);    
                //rooms.Add(r);
                row++;  
            }

            return rooms;
        }

        public Task DeleteRoom(Room room)
        {
            var roomKey = room.room_key;
            var sql = @"delete from room where room_key = " + roomKey;

            return _db.SaveData(sql, roomKey);
        }
    }
}
