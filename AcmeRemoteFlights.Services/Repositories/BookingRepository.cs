using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace AcmeRemoteFlights.Services.Models
{
    public class BookingRepository : IRepositoryV1<Booking>
    {
        private IDbConnection dbConnection;
        private readonly string sqlConnectionString;

        public BookingRepository(string dbConnectionStr)
        {
            sqlConnectionString = dbConnectionStr;
        }

        public int Add(Booking booking)
        {
            var result = 0;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspBookingInsert";

                DynamicParameters param = new DynamicParameters();
                param.Add("@TravelDate", booking.TravelDate, DbType.DateTime);
                param.Add("@FlightId", booking.FlightId, DbType.Int32);
                param.Add("@PassengerId", booking.PassengerId, DbType.Int32);
                param.Add("@PassengerCount", booking.PassengerCount, DbType.Int16);
                param.Add("@Notes", booking.Notes, DbType.String);

                result = SqlMapper.Execute(dbConnection, sQuery, param, null, null, CommandType.StoredProcedure);
            }

            return result;
        }

        public Booking Find(DateTime travelDate, int flightId, int passengerId)
        {
            IList<Booking> bookingList;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspBookingsSelectByTravelDateFlightIdPassengerId";

                DynamicParameters param = new DynamicParameters();
                param.Add("@TravelDate", travelDate, DbType.DateTime);
                param.Add("@FlightId", flightId, DbType.Int32);
                param.Add("@PassengerId", passengerId, DbType.Int32);

                bookingList = SqlMapper.Query<Booking>(dbConnection, sQuery, param, null, true, null, CommandType.StoredProcedure).ToList();
            }

            return bookingList.FirstOrDefault();
        }

        public Booking Find(int bookingId)
        {
            IList<Booking> bookingList;

            using (dbConnection = new SqlConnection(sqlConnectionString))
            {
                var sQuery = "dbo.uspBookingsSelectByBookingId";

                DynamicParameters param = new DynamicParameters();
                param.Add("@BookingId", bookingId, DbType.DateTime);

                bookingList = SqlMapper.Query<Booking>(dbConnection, sQuery, param, null, true, null, CommandType.StoredProcedure).ToList();
            }

            return bookingList.FirstOrDefault();
        }
    }
}
