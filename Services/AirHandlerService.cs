using RVS_App.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using NpgsqlTypes;

namespace RVS_App
{
    public class AirHandlerService
    {
        private readonly SqlDataAccess _db;
        private readonly IConfiguration _config;
        public string ConnectionStringName { get; set; } = "MyConnection";

        public AirHandlerService(SqlDataAccess db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public Task<List<AirHandler>> GetBuildingAirHandlers(int building_key)
        {
            var sql = @"SELECT air_handler.*                        
                        ,building.building_name
                        ,site.site_name
                        FROM air_handler
                        left join building on building.building_key = air_handler.building_key
                        left join site on site.site_key = building.site_key
                        WHERE building.building_key = " + building_key +
                        " ORDER by air_handler.air_handler_name";

            return _db.LoadData<AirHandler, dynamic>(sql, new { });
        }

        public Task InsertAirHandler(AirHandler airHandler)
        {
            var sql = @"INSERT INTO air_handler (building_key, air_handler_name, area, population, ventilation_efficiency, supply_airflow, doas )
                        VALUES (@building_key, @air_handler_name, @area, @population, @ventilation_efficiency, @supply_airflow, @doas);";

            return _db.SaveData(sql, airHandler);
        }

        public Task UpdateAirHandler(AirHandler airHandler)
        {
            var sql = @"UPDATE air_handler SET building_key = @building_key
                            , air_handler_name = @air_handler_name
                            , area = @area
                            , population = @population
                            , ventilation_efficiency = @ventilation_efficiency
                            , supply_airflow = @supply_airflow
                            , doas = @doas
                        WHERE air_handler_key = @air_handler_key";

            return _db.SaveData(sql, airHandler);
        }

        public Task DeleteAirHandler(AirHandler airHandler)
        {
            var airHandlerKey = airHandler.air_handler_key;
            var sql = @"delete from air_handler where air_handler_key =" + airHandlerKey + "";

            return _db.SaveData(sql, airHandlerKey);
        }

    }
}
