using RVS_App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVS_App
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

        public Task<List<State>> GetState(int state_key)
        {
            var sql = "select state.* from state where state_key = @state_key";

            return _db.LoadData<State, dynamic>(sql, state_key);
        }

        public Task InsertState(State state)
        {
            var sql = @"insert into state (state_name, abbreviation)
                           values (@state_name, @abbreviation)";

            return _db.SaveData(sql, state);
        }

        public Task UpdateState(State state)
        {
            var sql = @"update state SET state_name = @state_name, abbreviation = @abbreviation";

            return _db.SaveData(sql, state);
        }


        public Task DeleteState(int stateKey)
        {
            var sql = @"delete from state where state_key = @state_key";

            return _db.SaveData(sql, stateKey);
        }


    }
}
