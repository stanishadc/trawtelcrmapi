using Contracts;
using CsvHelper;
using CsvHelper.Configuration;
using Entities.Models;
using System.Globalization;

namespace Repository
{
    public class AirlineRepository : IAirlineRepository
    {
        public List<Airline> GetAirlines()
        {
            try
            {
                return BindAirlineCSVData();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Airline> SearchAirlines(string searchkey)
        {
            try
            {
                var airports = BindAirlineCSVData();
                return airports.Where(p => p.AirlineCode.ToLower().Contains(searchkey.ToLower()) || p.AirlineName.ToLower().Contains(searchkey.ToLower())).ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private List<Airline> BindAirlineCSVData()
        {
            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = ",",
                    Comment = '%'
                };
                using (var reader = new StreamReader("Assets/airlines.csv"))
                using (var csv = new CsvReader(reader, configuration))
                {
                    csv.Context.RegisterClassMap<AirlineMap>();
                    var records = csv.GetRecords<Airline>().ToList();
                    records.RemoveAt(0);
                    return records;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
    public class AirlineMap : ClassMap<Airline>
    {
        public AirlineMap()
        {
            Map(p => p.AirlineCode).Index(0);
            Map(p => p.AirlineName).Index(1);
        }
    }
}
