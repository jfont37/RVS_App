using RVS_App.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RVS_App
{
    public class TerminalUnitService
    {
        private readonly SqlDataAccess _db;

        public TerminalUnitService(SqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<TerminalUnit>> GetBuildingTerminalUnits(int building_key)
        {
            var sql = @"SELECT terminal_unit.*
                            ,air_handler_name                        
                            ,building.building_name
                            ,site.site_name
                        FROM terminal_unit
                            left join air_handler on air_handler.air_handler_key = terminal_unit.air_handler_key                                                    
                            left join building on building.building_key = air_handler.building_key
                            left join site on site.site_key = building.site_key
                        WHERE terminal_unit.building_key = " + building_key +
                        " ORDER by terminal_unit.terminal_unit_name";

            return _db.LoadData<TerminalUnit, dynamic>(sql, new { });
        }

        public Task<List<TerminalUnit>> GetAirHandlerTerminalUnits(int air_handler_key)
        {
            var sql = @"SELECT terminal_unit.*
                        FROM terminal_unit
                        WHERE air_handler_key = " + air_handler_key +
                        " ORDER by terminal_unit.terminal_unit_name";

            return _db.LoadData<TerminalUnit, dynamic>(sql, new { });
        }

        public Task InsertTerminalUnit(TerminalUnit terminalUnit)
        {
            var sql = @"INSERT INTO terminal_unit (building_key, air_handler_key, terminal_unit_name, area)
                        VALUES (@building_key, @air_handler_key, @terminal_unit_name, @area);";

            return _db.SaveData(sql, terminalUnit);
        }

        public Task UpdateTerminalUnit(TerminalUnit terminalUnit)
        {
            var sql = @"UPDATE terminal_unit SET building_key = @building_key
                            , air_handler_key = @air_handler_key
                            , terminal_unit_name = @terminal_unit_name
                            , area = @area
                            , oa_required = @oa_required
                            , cooling_min_airflow = @cooling_min_airflow
                            , cooling_max_airflow = @cooling_max_airflow
                            , heating_min_airflow = @heating_min_airflow
                            , heating_max_airflow = @heating_max_airflow
                            , cooling_min_airflow_unocc = @cooling_min_airflow_unocc
                            , cooling_max_airflow_unocc = @cooling_max_airflow_unocc
                            , heating_min_airflow_unocc = @heating_min_airflow_unocc
                            , heating_max_airflow_unocc = @heating_max_airflow_unocc

                        WHERE terminal_unit_key = @terminal_unit_key";

            return _db.SaveData(sql, terminalUnit);
        }

        public Task<List<TerminalUnit>> GetImportedTerminalUnit(int building_key, string tuName)
        {
            var sql = @"SELECT terminal_unit.*
                        FROM terminal_unit
                        WHERE building_key = " + building_key +
                        " and terminal_unit_name = '" + tuName +
                        "' ORDER by terminal_unit.terminal_unit_name";

            return _db.LoadData<TerminalUnit, dynamic>(sql, new { });
        }

        //public async Task<List<TerminalUnit>> ImportTerminalUnits(FileInfo file, int bldgkey)
        //{
        //    var terminalUnits = new List<TerminalUnit>();
        //    using var package = new ExcelPackage(file);
        //    await package.LoadAsync(file);

        //    var worksheet = package.Workbook.Worksheets[0];

        //    //assuming that terminal unit name is in the 4th column on spreadsheet and starts in row 1
        //    int row = 1;
        //    int col = 4;

        //    while (string.IsNullOrWhiteSpace(worksheet.Cells[row, col].Value?.ToString()) == false)
        //    {
        //        TerminalUnit tu = new TerminalUnit();
        //        tu.terminal_unit_name = worksheet.Cells[row, col].Value.ToString();
        //        tu.building_key = bldgkey;
        //        terminalUnits.Add(tu);
        //        row++;
        //    }

        //    foreach (var tu in terminalUnits)
        //    {
        //        await InsertTerminalUnit(tu);
        //    }

        //    //need to check see if terminalunitkey gets populated after insert above otherwise will have to get them here
        //    return terminalUnits;
        //}

        public Task DeleteTerminalUnit(TerminalUnit terminalUnit)
        {
            var terminalUnitKey = terminalUnit.terminal_unit_key;
            var sql = @"delete from terminal_unit where terminal_unit_key = " + terminalUnitKey;

            return _db.SaveData(sql, terminalUnitKey);
        }
    }
}
