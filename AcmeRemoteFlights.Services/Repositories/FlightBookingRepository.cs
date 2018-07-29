using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AcmeRemoteFlights.Services.Models;
using Dapper;

namespace AcmeRemoteFlights.Services.Repositories
{
    public class FlightBookingRepository : IRepositoryV1<FlightBooking>
    {
        private IDbConnection dbConnection;
        private readonly string sqlConnectionString;

        public FlightBookingRepository(string dbConnectionStr)
        {
            sqlConnectionString = dbConnectionStr;
        }

        public int Add(FlightBooking flightBooking)
        {
            var result = 0;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspFlightBookingsInsert";

                DynamicParameters param = new DynamicParameters();
                param.Add("@TravelDate", flightBooking.TravelDate, DbType.DateTime);
                param.Add("@FlightId", flightBooking.FlightId, DbType.Int32);
                param.Add("@PassengerCount", flightBooking.PassengerCount, DbType.Int16);

                result = SqlMapper.Execute(dbConnection, sQuery, param, null, null, CommandType.StoredProcedure);
            }

            return result;
        }

        public FlightBooking Find(int flightBookingId)
        {
            throw new NotImplementedException();
        }

        public FlightBooking Find(int flightId, DateTime travelDate)
        {
            IList<FlightBooking> flightBookingList;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspFlightBookingsSelectByTravelDateFlightId";

                DynamicParameters param = new DynamicParameters();
                param.Add("@TravelDate", travelDate, DbType.DateTime);
                param.Add("@FlightId", flightId, DbType.Int32);

                flightBookingList = SqlMapper.Query<FlightBooking>(dbConnection, sQuery, param, null, true, null, CommandType.StoredProcedure).ToList();
            }

            return flightBookingList.FirstOrDefault();
        }

        public IEnumerable<FlightBooking> Find(DateTime startDate, DateTime endDate)
        {
            IList<FlightBooking> flightBookingList;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspFlightBookingsSelectByTravelDate";

                DynamicParameters param = new DynamicParameters();
                param.Add("@StartDate", startDate, DbType.DateTime);
                param.Add("@EndDate", endDate, DbType.DateTime);

                flightBookingList = SqlMapper.Query<FlightBooking>(dbConnection, sQuery, param, null, true, null, CommandType.StoredProcedure).ToList();
            }

            return flightBookingList;
        }

        public int Update(FlightBooking flightBooking)
        {
            var result = 0;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspFlightBookingsUpdate";

                DynamicParameters param = new DynamicParameters();
                param.Add("@TravelDate", flightBooking.TravelDate, DbType.DateTime);
                param.Add("@FlightId", flightBooking.FlightId, DbType.Int32);
                param.Add("@PassengerCount", flightBooking.PassengerCount, DbType.Int16);

                result = SqlMapper.Execute(dbConnection, sQuery, param, null, null, CommandType.StoredProcedure);
            }

            return result;
        }
    }
}
