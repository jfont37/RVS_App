using RVS_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVS_App
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
                        ORDER by building.building_name";

            return _db.LoadData<Building, dynamic>(sql, new { });
        }

        public Task<List<Building>> GetSiteBuildings(int site_key)
        {
            var sql = @"SELECT building.*,
                        site.site_name,
                            state.abbreviation state 
                        FROM building 
                            left join state on state.state_key = building.state_key
                            left join site on site.site_key = building.site_key
                        WHERE building.site_key = " + site_key + @"
                        ORDER BY building.building_name";

            return _db.LoadData<Building, dynamic>(sql, new { });
        }

        public async Task<Building> GetBuilding(int building_key)
        {
            var sql = "select site.*, state.abbreviation state from site left join state on state.state_key = site.state_key where site_key = @site.site_key";
            var buildings = await _db.LoadData<Building, dynamic>(sql, new { });
            List<Building> buildings1 = buildings.ToList();
            return buildings1.FirstOrDefault();
        }

        public Task InsertBuilding(Building building)
        {
            var sql = @"INSERT INTO building (site_key, building_name, address, city, state_key, zip)
                        VALUES (@site_key, @building_name, @address, @city, @state_key, @zip);";

            return _db.SaveData(sql, building);
        }

        public Task UpdateBuilding(Building building)
        {
            var sql = @"UPDATE building SET building_name = @building_name
                            , site_key = @site_key
                            , address = @address
                            , city = @city
                            , state_key = @state_key
                            , zip = @zip
                            , area = @area
                        WHERE building_key = @building_key";

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
