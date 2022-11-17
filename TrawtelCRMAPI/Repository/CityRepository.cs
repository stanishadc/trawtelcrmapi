using Contracts;
using CsvHelper;
using CsvHelper.Configuration;
using Entities.Models;
using System.Globalization;

namespace Repository
{
    public class CityRepository : ICityRepository
    {
        public List<City> GetCities()
        {
            try
            {
                return BindCityCSVData();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<City> SearchCity(string searchkey)
        {
            try
            {
                var airports = BindCityCSVData();
                return airports.Where(p => p.CityCode.ToLower().Contains(searchkey.ToLower()) || p.CityName.ToLower().Contains(searchkey.ToLower())).ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private List<City> BindCityCSVData()
        {
            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = ",",
                    Comment = '%'
                };
                using (var reader = new StreamReader("Assets/cities.csv"))
                using (var csv = new CsvReader(reader, configuration))
                {
                    csv.Context.RegisterClassMap<CityMap>();
                    var records = csv.GetRecords<City>().ToList();
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
    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            Map(p => p.CityId).Index(0);
            Map(p => p.CityName).Index(1);
            Map(p => p.CityCode).Index(2);
            Map(p => p.CountryCode).Index(3);
            Map(p => p.CountryName).Index(4);
        }
    }
}