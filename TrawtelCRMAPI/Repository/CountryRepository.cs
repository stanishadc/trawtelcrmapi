using Contracts;
using CsvHelper;
using CsvHelper.Configuration;
using Entities.Models;
using System.Globalization;

namespace Repository
{
    public class CountryRepository : ICountryRepository
    {
        public List<Country> GetCountries()
        {
            try
            {
                return BindCountryCSVData();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Country> SearchCountry(string searchkey)
        {
            try
            {
                var airports = BindCountryCSVData();
                return airports.Where(p => p.CountryCode.ToLower().Contains(searchkey.ToLower()) || p.CountryName.ToLower().Contains(searchkey.ToLower())).ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private List<Country> BindCountryCSVData()
        {
            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = "|",
                    Comment = '%'
                };
                using (var reader = new StreamReader("Assets/countries.csv"))
                using (var csv = new CsvReader(reader, configuration))
                {
                    csv.Context.RegisterClassMap<CountryMap>();
                    var records = csv.GetRecords<Country>().ToList();
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
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Map(p => p.CountryId).Index(0);
            Map(p => p.CountryName).Index(1);
            Map(p => p.CountryCode).Index(2);
        }
    }
}