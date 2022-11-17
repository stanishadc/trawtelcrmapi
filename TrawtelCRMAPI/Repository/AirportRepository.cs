using Contracts;
using CsvHelper;
using CsvHelper.Configuration;
using Entities.Models;
using System.Globalization;

namespace Repository
{
    public class AirportRepository : IAirportRepository
    {
        public List<Airport> GetAirports()
        {
            try
            {
                return BindAirportCSVData();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Airport> SearchAirports(string searchkey)
        {
            try
            {
                var airports = BindAirportCSVData();
                return airports.Where(p => p.AirportCode.ToLower().Contains(searchkey.ToLower()) || p.CityName.ToLower().Contains(searchkey.ToLower()) || p.AirportName.ToLower().Contains(searchkey.ToLower())).ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private List<Airport> BindAirportCSVData()
        {
            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = ",",
                    Comment = '%'
                };
                using (var reader = new StreamReader("Assets/airports.csv"))
                using (var csv = new CsvReader(reader, configuration))
                {
                    csv.Context.RegisterClassMap<AirportMap>();
                    var records = csv.GetRecords<Airport>().ToList();
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
    public class AirportMap : ClassMap<Airport>
    {
        public AirportMap()
        {
            Map(p => p.AirportCode).Index(0);
            Map(p => p.AirportName).Index(1);
            Map(p => p.CityCode).Index(2);
            Map(p => p.CityName).Index(3);
            Map(p => p.CountryName).Index(4);
            Map(p => p.CountryCode).Index(5);
        }
    }
}
