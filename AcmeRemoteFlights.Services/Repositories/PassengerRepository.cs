using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace AcmeRemoteFlights.Services.Models
{
    public class PassengerRepository : IRepositoryV1<Passenger>
    {
        private IDbConnection dbConnection;
        private readonly string sqlConnectionString;

        public PassengerRepository(string dbConnectionStr)
        {
            sqlConnectionString = dbConnectionStr;
        }

        public int Add(Passenger passenger)
        {
            var result = 0;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspPassengersInsert";

                DynamicParameters param = new DynamicParameters();
                param.Add("@IdentityNumber", passenger.IdentityNumber, DbType.String);
                param.Add("@FirstName", passenger.FirstName, DbType.String);
                param.Add("@LastName", passenger.LastName, DbType.String);
                param.Add("@EmailAddress", passenger.EmailAddress, DbType.String);
                param.Add("@PhoneNumber", passenger.PhoneNumber, DbType.String);

                result = SqlMapper.Execute(dbConnection, sQuery, param, null, null, CommandType.StoredProcedure);
            }

            return result;
        }

        public Passenger Find(int passengerId)
        {
            IList<Passenger> passengerList;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspPassengersSelectById";

                DynamicParameters param = new DynamicParameters();
                param.Add("@PassengerId", passengerId, DbType.Int32);

                passengerList = SqlMapper.Query<Passenger>(dbConnection, sQuery, param, null, true, null, CommandType.StoredProcedure).ToList();
            }

            return passengerList.FirstOrDefault();
        }
    }
}
