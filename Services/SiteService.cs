using RVS_App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;



namespace RVS_App
{
    public class SiteService
    {
        private readonly SqlDataAccess _db;

        public SiteService(SqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<Site>> GetSites()
        {
            var sql = @"select site.*, state.abbreviation state from site left join state on state.state_key = site.state_key order by site.site_name";

            return _db.LoadData<Site, dynamic>(sql, new { });
        }

        public async Task<Site> GetSite(int site_key)
        {
            var sql = "select site.*, state.abbreviation state from site left join state on state.state_key = site.state_key where site_key = @site.site_key";
            var sites = await _db.LoadData<Site, dynamic>(sql, new { });
            List<Site> sites1 = sites.ToList();
            return sites1.FirstOrDefault();
        }

        public Task InsertSite(Site site)
        {
            var sql = @"INSERT INTO site (site_name, address, city, state_key, zip)
                        VALUES (@site_name, @address, @city, @state_key, @zip);";

            return _db.SaveData(sql, site);
        }

        public Task UpdateSite(Site site)
        {
            var sql = @"UPDATE site SET site_name = @site_name
                            , address = @address
                            , city = @city
                            , state_key = @state_key
                            , zip = @zip
                        WHERE site_key = @site_key";

            return _db.SaveData(sql, site);
        }

        //public Task InsertSite(Site site)
        //{
        //    var siteName = site.NAME;
        //    var siteAddress = site.address;
        //    var siteCity = site.city;
        //    var siteState_key = site.state_key;
        //    var siteZip = site.zip;

        //    var sql = @"insert into site (""NAME"", address, city, state_key, zip)
        //                   values ('" + siteName + "', '" + siteAddress + "', '" + siteCity + "', '" + siteState_key + "', '" + siteZip + "');";

        //    return _db.SaveData(sql, site);
        //}

        //public Task UpdateSite(Site site)
        //{
        //    var siteSite_key = site.site_key;
        //    var siteName = site.NAME;
        //    var siteAddress = site.address;
        //    var siteCity = site.city;
        //    var siteState_key = site.state_key;
        //    var siteZip = site.zip;

        //    var sql = @"update site SET ""NAME"" = '" + siteName +
        //        "', address = '" + siteAddress +
        //        "', city = '" + siteCity +
        //        "', state_key = '" + siteState_key +
        //        "', zip = '" + siteZip + "'" +
        //        " where site_key = " + siteSite_key
        //        ;

        //    return _db.SaveData(sql, site);
        //}

        public Task DeleteSite(Site site)
        {
            var siteKey = site.site_key;
            var sql = @"delete from site where site_key = " + siteKey;

            return _db.SaveData(sql, siteKey);
        }


    }
}
