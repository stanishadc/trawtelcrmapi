using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IUserRepository _user;
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        private IUserKeyRepository _userkey;
        public IUserKeyRepository UserKey
        {
            get
            {
                if (_userkey == null)
                {
                    _userkey = new UserKeyRepository(_repoContext);
                }
                return _userkey;
            }
        }
        private IAgentRepository _agent;
        public IAgentRepository Agent
        {
            get
            {
                if (_agent == null)
                {
                    _agent = new AgentRepository(_repoContext);
                }
                return _agent;
            }
        }
        private IClientRepository _client;
        public IClientRepository Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new ClientRepository(_repoContext);
                }
                return _client;
            }
        }
        private ISupplierCodeRepository _suppliercode;
        public ISupplierCodeRepository SupplierCode
        {
            get
            {
                if (_suppliercode == null)
                {
                    _suppliercode = new SupplierCodeRepository(_repoContext);
                }
                return _suppliercode;
            }
        }
        private ISupplierRepository _supplier;
        public ISupplierRepository Supplier
        {
            get
            {
                if (_supplier == null)
                {
                    _supplier = new SupplierRepository(_repoContext);
                }
                return _supplier;
            }
        }
        private IVisaRepository _visa;
        public IVisaRepository Visa
        {
            get
            {
                if (_visa == null)
                {
                    _visa = new VisaRepository(_repoContext);
                }
                return _visa;
            }
        }
        private IVisaCountryRepository _visacountry;
        public IVisaCountryRepository VisaCountry
        {
            get
            {
                if (_visacountry == null)
                {
                    _visacountry = new VisaCountryRepository(_repoContext);
                }
                return _visacountry;
            }
        }
        private IVisaPriceRepository _visaprice;
        public IVisaPriceRepository VisaPrice
        {
            get
            {
                if (_visaprice == null)
                {
                    _visaprice = new VisaPriceRepository(_repoContext);
                }
                return _visaprice;
            }
        }
        private IRequestRepository _request;
        public IRequestRepository Request
        {
            get
            {
                if (_request == null)
                {
                    _request = new RequestRepository(_repoContext);
                }
                return _request;
            }
        }
        private IBookingRepository _booking;
        public IBookingRepository Booking
        {
            get
            {
                if (_booking == null)
                {
                    _booking = new BookingRepository(_repoContext);
                }
                return _booking;
            }
        }
        private ITravelerRepository _traveler;
        public ITravelerRepository Traveler
        {
            get
            {
                if (_traveler == null)
                {
                    _traveler = new TravelerRepository(_repoContext);
                }
                return _traveler;
            }
        }
        private IFlightRepository _flight;
        public IFlightRepository Flight
        {
            get
            {
                if (_flight == null)
                {
                    _flight = new FlightRepository(_repoContext);
                }
                return _flight;
            }
        }
        private IHotelRepository _hotel;
        public IHotelRepository Hotel
        {
            get
            {
                if (_hotel == null)
                {
                    _hotel = new HotelRepository(_repoContext);
                }
                return _hotel;
            }
        }
        private IAirportRepository _airport;
        public IAirportRepository Airport
        {
            get
            {
                if (_airport == null)
                {
                    _airport = new AirportRepository();
                }
                return _airport;
            }
        }
        private ICityRepository _city;
        public ICityRepository City
        {
            get
            {
                if (_city == null)
                {
                    _city = new CityRepository();
                }
                return _city;
            }
        }
        private ICountryRepository _country;
        public ICountryRepository Country
        {
            get
            {
                if (_country == null)
                {
                    _country = new CountryRepository();
                }
                return _country;
            }
        }
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
