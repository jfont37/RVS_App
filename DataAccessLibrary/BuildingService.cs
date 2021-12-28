using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLibrary
{
    public class BuildingService
    {
        private readonly SqlDataAccess _db;

        public BuildingService(SqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<Building>> GetBuildings()
        {
            var sql = @"SELECT building.*,
                            state.abbreviation state 
                        FROM building 
                            left join state on state.state_key = building.state_key
                            left join site on site.site_key = building.site_key
                        ORDER by building.""NAME""";

            return _db.LoadData<Building, dynamic>(sql, new { });
        }

        public Task<List<Building>> GetSiteBuildings(int site_key)
        {
            var sql = @"SELECT building.*,
                        site.""NAME"" site_name,
                            state.abbreviation state 
                        FROM building 
                            left join state on state.state_key = building.state_key
                            left join site on site.site_key = building.site_key
                        WHERE building.site_key = " + site_key + @"
                        ORDER BY building.""NAME""";

            return _db.LoadData<Building, dynamic>(sql, new { });
        }

        public async Task<Building> GetBuilding(int building_key)
        {
            var sql = "select site.*, state.abbreviation state from site left join state on state.state_key = site.state_key where site_key = @site.site_key";
            var buildings = await _db.LoadData<Building, dynamic>(sql, new { });
            List<Building> buildings1 = buildings.ToList();
            return buildings1.FirstOrDefault();
        }

        //public Task InsertBuilding(Building building)
        //{
        //    var buildingName        = building.NAME;
        //    var buildingSite_key    = building.site_key;
        //    var buildingAddress     = building.address;
        //    var buildingCity        = building.city;
        //    var buildingState_key   = building.state_key;
        //    var buildingZip         = building.zip;
        //    //var buildingArea        = building.area;

        //    var sql = @"INSERT INTO building (site_key, ""NAME"", address, city, state_key, zip)
        //                VALUES ('" + buildingSite_key + "', '" + buildingName + "', '" + buildingAddress + "', '" + buildingCity 
        //                + "', '" + buildingState_key + "', '" + buildingZip + "');";

        //    return _db.SaveData(sql, building);
        //}

        public Task InsertBuilding(Building building)
        {
            var sql = @"INSERT INTO building (site_key, ""NAME"", address, city, state_key, zip)
                        VALUES (@site_key, @NAME, @address, @city, @state_key, @zip);";

            return _db.SaveData(sql, building);
        }

        public Task UpdateBuilding(Building building)
        {
            var buildingBuilding_key    = building.building_key;
            var buildingName            = building.NAME;
            var buildingSite_key        = building.site_key;
            var buildingAddress         = building.address;
            var buildingCity            = building.city;
            var buildingState_key       = building.state_key;
            var buildingZip             = building.zip;
            //var buildingArea            = building.area;

            var sql = @"update building SET ""NAME"" = '" + buildingName +
                "', site_key = '" + buildingSite_key +
                "', address = '" + buildingAddress +
                "', city = '" + buildingCity +
                "', state_key = '" + buildingState_key +
                "', zip = '" + buildingZip + "'" +
                //"', area = '" + buildingArea + "'" +
                " where building_key = " + buildingBuilding_key
                ;

            return _db.SaveData(sql, building);
        }

        public Task DeleteBuilding(Building building)
        {
            var buildingKey = building.building_key;
            var sql = @"delete from building where building_key = " + buildingKey;

            return _db.SaveData(sql, buildingKey);
        }


    }
}
