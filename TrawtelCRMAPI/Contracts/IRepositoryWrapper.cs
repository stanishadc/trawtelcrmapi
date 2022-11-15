﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IUserKeyRepository UserKey { get; }
        IAgentRepository Agent { get; }
        ISupplierRepository Supplier { get; }
        ISupplierCodeRepository SupplierCode { get; }
        IClientRepository Client { get; }
        IVisaRepository Visa { get; }
        IVisaPriceRepository VisaPrice { get; }
        IVisaCountryRepository VisaCountry { get; }
        IRequestRepository Request { get; }
        IBookingRepository Booking { get; }
        ITravelerRepository Traveler { get; }
        IFlightRepository Flight { get; }
        void Save();
    }
}