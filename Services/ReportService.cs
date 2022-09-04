using RVS_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVS_App
{
    public class ReportService
    {
        private readonly SqlDataAccess _db;

        public ReportService(SqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<ReportRow>> GetReportRows(int site_key)
        {
            var sql = @"
                        SELECT s.site_name as site_name
                            ,b.building_name as building_name
                            ,tu.terminal_unit_name as terminal_unit_name
                            ,sum(r.area) area
							,sum(r.volume) as volume
                            ,ah.air_handler_name
							,sum(CASE WHEN rt.critical = true THEN round((r.volume * rt.oa_air_changes_per_hour)/60)
								 ELSE round((((r.population * rt.oa_rate_people) + (r.area * rt.oa_rate_area))/ r.ventilation_efficiency))								
			         		 		END) as OA_Required
                            ,sum(r.supply_airflow) as Cooling_Max
                            ,round(sum((((r.population * rt.oa_rate_people) + (r.area * rt.oa_rate_area))/r.ventilation_efficiency))/(1+ah.oa_fraction-ah.ventilation_efficiency)) as Cooling_Min
                            ,sum(r.supply_airflow) as Heating_Max
                            ,round(sum((((r.population * rt.oa_rate_people) + (r.area * rt.oa_rate_area))/r.ventilation_efficiency))/(1+ah.oa_fraction-ah.ventilation_efficiency)) as Heating_Min
                            ,sum(r.supply_airflow) as Cooling_Max_Unocc
                            ,round(sum((((r.population * rt.oa_rate_people) + (r.area * rt.oa_rate_area))/r.ventilation_efficiency))/(1+ah.oa_fraction-ah.ventilation_efficiency)) as Cooling_Min_Unocc
                            ,sum(r.supply_airflow) as Heating_Max_Unocc
                            ,round(sum((((r.population * rt.oa_rate_people) + (r.area * rt.oa_rate_area))/r.ventilation_efficiency))/(1+ah.oa_fraction-ah.ventilation_efficiency)) as Heating_Min_Unocc
                        FROM site s
                            left join building b on b.site_key = s.site_key
                            left join room r on r.building_key = b.building_key
                            left join room_type rt on rt.room_type_key = r.room_type_key
                            left join air_handler ah on r.air_handler_key = ah.air_handler_key
                            left join terminal_unit tu on tu.terminal_unit_key = r.terminal_unit_key
                        WHERE s.site_key = " + site_key + @"
                        GROUP BY s.site_name, b.building_name,ah.air_handler_name,ah.oa_fraction, ah.ventilation_efficiency,tu.terminal_unit_key                        
                        ORDER BY b.building_name asc, ah.air_handler_name ,tu.terminal_unit_name;
                        ";
            var data = _db.LoadData<ReportRow, dynamic>(sql, new { });
            return data;
        }

    }
}
