using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLibrary
{
    public class StateService
    {
        private readonly SqlDataAccess _db;

        public StateService(SqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<State>> GetStates()
        {
            var sql = "select state.* from state";

            return _db.LoadData<State, dynamic>(sql, new { });
        }

        public Task<List<State>> GetState()
        {
            var sql = "select state.* from state where state_key = @state.state_key";

            return _db.LoadData<State, dynamic>(sql, new { });
        }

        public Task InsertState(State state)
        {
            var stateName = state.NAME;
            var stateAbbreviation = state.abbreviation;

            var sql = @"insert into state (""NAME"", abbreviation)
                           values ('" + stateName + "', '" + stateAbbreviation + "')" ;

            return _db.SaveData(sql, state);
        }

        public Task UpdateState(State state)
        {
            var stateName = state.NAME;
            var stateAbbreviation = state.abbreviation;

            var sql = @"update state SET ""NAME"" =" + stateName +
                ", abbreviation =" + stateAbbreviation;

            return _db.SaveData(sql, state);
        }


        public Task DeleteState(int stateKey)
        {
            var sql = @"delete from state where state_key = " + stateKey;

            return _db.SaveData(sql, stateKey);
        }


    }
}
