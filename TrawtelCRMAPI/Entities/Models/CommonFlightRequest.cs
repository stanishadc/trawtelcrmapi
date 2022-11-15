﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CommonFlightRequest
    {
        public List<FlightJourneyRequest>? flightJourneyRequest { get; set; }
        public string? JourneyType { get; set; }
        public int Adults { get; set; }
        public int Kids { get; set; }
        public int Infants { get; set; }
        public string? CabinClass { get; set; }
        public Guid AgentId { get; set; }        
    }
    public class AgentSuppliers
    {
        public Guid SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierKey { get; set; }
        public string? SupplierUserName { get; set; }
        public string? SupplierPassword { get; set; }
        public string? SupplierURL { get; set; }
    }
    public class CommonFlightsResponse
    {
        public CommonFlightRequest? commonFlightRequest { get; set; }
        public List<CommonFlightDetails>? commonFlightDetails { get; set; }
        public bool? status { get; set; }
        public string? ErrorMessage{get;set;}
    }
    public class CommonFlightDetails
    {
        
        public string? JourneyType { get; set; }
        public List<TFLegs>? tFLegs { get; set; }
    }
    public class TFLegs
    {
        public Guid TFId { get; set; }
        public Guid SupplierId { get; set; }
        public string? SupplierLegId { get; set; }
        public string? SupplierSessionId { get; set; }
        public TFDepartureData? tFDepartureData { get; set; }
        public TFArrivalData? tFArrivalData { get; set; }
        public int Stops { get; set; }
        public bool MealType { get; set; }
        public int NoOfSeats { get; set; }
        public int RefundType { get; set; }        
        public TFPriceDetails? tFPriceDetails { get; set; }        
        public List<TFSegments>? tFSegments { get; set; }
    }
    public class TFSegments
    {
        public TFDepartureData? tFDepartureData { get; set; }
        public TFArrivalData? tFArrivalData { get; set; }
        public string? SupplierSegmentId { get; set; }
        public string? Airline { get; set; }
        public string? AirlineCode { get; set; }
        public string? AirlineLogo { get; set; }
        public string? EquimentType { get; set; }        
        public string? FlightNumber { get; set; }
        public int Duration { get; set; }
        public int ConnectingTime { get; set; }
    }
    public class TFPriceDetails
    {
        public TFSupplierPrice? tFSupplierPrice { get; set; }
        public TFAgentPrice? tFAgentPrice { get; set; }
    }
    public class TFSupplierPrice
    {
        public TFPassengerPrice? tFAdults { get; set; }
        public TFPassengerPrice? tFKids { get; set; }
        public TFPassengerPrice? tFInfants { get; set; }
        public double TotalPrice { get; set; }
    }
    public class TFAgentPrice
    {
        public TFPassengerPrice? tFAdults { get; set; }
        public TFPassengerPrice? tFKids { get; set; }
        public TFPassengerPrice? tFInfants { get; set; }        
        public double TotalPrice { get; set; }
    }
    public class TFPassengerPrice
    {
        public double BaseFare { get; set; }
        public double NetFare { get; set; }
        public double Tax { get; set; }
        public double TotalFare { get; set; }
        public string? MealType { get; set; }
        public string? Refundable { get; set; }
        public string? CabinBag { get; set; }
        public string? CheckinBag { get; set; }
        public string? CabinClass { get; set; }
        public TFOtherCharges? tFOtherCharges { get; set; }
    }
    public class TFOtherCharges
    {
        public double OtherCharges { get; set; }
        public double ManagementFee { get; set; }
        public double ManagementFeeTax { get; set; }
        public double AirlineGST { get; set; }
        public double FuelSurcharge { get; set; }
        public double? CarrierMiscFee { get; set; }
    }
    public class TFDepartureData
    {
        public string? DepartureDate { get; set; }
        public string? DepartureTime { get; set; }
        public string? AirportCode { get; set; }
        public string? AirportName { get; set; }
        public string? CityCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? Terminal { get; set; }
    }
    public class TFArrivalData
    {
        public string? ArrivalDate { get; set; }
        public string? ArrivalTime { get; set; }
        public string? AirportCode { get; set; }
        public string? AirportName { get; set; }
        public string? CityCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? Terminal { get; set; }
    }
}
