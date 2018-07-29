using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace AcmeRemoteFlights.Services.Models
{
    public class FlightRepository : IRepositoryV1<Flight>
    {
        private IDbConnection dbConnection;
        private readonly string sqlConnectionString;

        public FlightRepository(string dbConnectionStr)
        {
            sqlConnectionString = dbConnectionStr;
        }

        public int Add(Flight flight)
        {
            var result = 0;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspFlightsInsert";

                DynamicParameters param = new DynamicParameters();
                param.Add("@FlightNumber", flight.Number, DbType.String);
                param.Add("@FlightName", flight.Name, DbType.String);
                param.Add("@DepartureCity", flight.DepartureCity, DbType.String);
                param.Add("@DepartureTime", flight.DepartureTime, DbType.DateTime);
                param.Add("@ArrivalCity", flight.ArrivalCity, DbType.String);
                param.Add("@ArrivalTime", flight.ArrivalTime, DbType.DateTime);
                param.Add("@PassengerCapacity", flight.PassengerCapacity, DbType.Int16);

                result = SqlMapper.Execute(dbConnection, sQuery, param, null, null, CommandType.StoredProcedure);
            }

            return result;
        }

        public IEnumerable<Flight> GetAll()
        {
            IList<Flight> flightList;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspFlightsSelectAll";
                flightList = SqlMapper.Query<Flight>(dbConnection, sQuery, null, null, true, null, CommandType.StoredProcedure).ToList();
            }

            return flightList;
        }

        public Flight Find(int flightId)
        {
            IList<Flight> flightList;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspFlightsSelectById";

                DynamicParameters param = new DynamicParameters();
                param.Add("@FlightId", flightId, DbType.Int32);

                flightList = SqlMapper.Query<Flight>(dbConnection, sQuery, param, null, true, null, CommandType.StoredProcedure).ToList();
            }

            return flightList.FirstOrDefault();
        }
    }
}
