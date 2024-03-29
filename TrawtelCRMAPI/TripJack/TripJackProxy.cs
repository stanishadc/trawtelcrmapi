﻿using Contracts;
using Entities;
using Entities.Common;
using Entities.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using static Entities.CommonEnums;

namespace TripJack
{
    public interface ITripJackProxy
    {
        TripJackSearchResponse searchAllRequest(string RequestData, FlightRequest commonFlightRequest);
        TripJackReviewResponse reviewRequest(string RequestData, FlightRequest commonFlightRequest);
        TripJackSeatMapResponse seatMapRequest(string RequestData, FlightRequest commonFlightRequest);
        TripJackBookingDetailsResponse bookingDetailsRequest(string RequestData, FlightRequest commonFlightRequest);
        TripJackBookingDetailsResponse confirmHoldBookingRequest(string RequestData, FlightRequest commonFlightRequest);
        TripJackConfirmFareTicketResponse confirmFareTicketRequest(string RequestData, FlightRequest commonFlightRequest);
        TripJackHoldBookResponse holdBookingRequest(string RequestData, FlightRequest commonFlightRequest);
        TripJackBookResponse instantBookingRequest(string RequestData, FlightRequest commonFlightRequest);
    }
    public class TripJackProxy
    {
        //private ILoggerManager _logger;
        public TripJackProxy()
        {
            //_logger = logger;
        }
        //private Response<> CallGetRequest(string requestURL, string APIKey)
        //{
        //    APIResponse _objResponse = new APIResponse();
        //    try
        //    {
        //        var client = new RestClient(requestURL);
        //        var request = new RestRequest();
        //        request.AddHeader("apikey", APIKey);
        //        request.AddHeader("Content-Type", "application/json");
        //        var response = client.Get(request);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            _objResponse = JsonConvert.DeserializeObject<APIResponse>(response.Content);
        //        }
        //        else
        //        {
        //            _objResponse.Status = false;
        //            _objResponse.Data = response.ResponseStatus;
        //            _objResponse.ErrorMessage = response.StatusDescription;                    
        //        }
        //        return _objResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        //_logger.LogError("Get Method Failed " + ex);
        //        _objResponse.Status = false;
        //        _objResponse.ErrorMessage = ex.Message;
        //        return _objResponse;
        //    }
        //}
        private RestResponse CallPostRequest(string requestURL, string RequestData, AgentSuppliers agentSuppliers)
        {
            var client = new RestClient(agentSuppliers.SupplierURL + requestURL);
            var request = new RestRequest();
            request.AddHeader("apikey", agentSuppliers.SupplierKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", RequestData, ParameterType.RequestBody);
            return client.Post(request);
        }

        #region Supplier API Methods
        public TripJackSearchResponse searchAllRequest(string RequestData, AgentSuppliers agentSuppliers)
        {
            try
            {
                var response = CallPostRequest("air-search-all", RequestData, agentSuppliers);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TripJackSearchResponse>(response.Content);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Flights Request Method Failed"+ ex);
                return null;
            }
        }
        public TripJackReviewResponse reviewRequest(string RequestData, AgentSuppliers agentSuppliers)
        {
            try
            {
                var response = CallPostRequest("review", RequestData, agentSuppliers);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TripJackReviewResponse>(response.Content);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Review Request Method Failed"+ ex);
            }
            return null;
        }
        public TripJackFareRuleResponse fareRuleRequest(string RequestData, AgentSuppliers agentSuppliers)
        {
            try
            {
                var response = CallPostRequest("farerule", RequestData, agentSuppliers);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TripJackFareRuleResponse>(response.Content);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Review Request Method Failed"+ ex);
            }
            return null;
        }
        public TripJackSeatMapResponse seatMapRequest(string RequestData, AgentSuppliers agentSuppliers)
        {
            try
            {
                var response = CallPostRequest("seat", RequestData, agentSuppliers);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TripJackSeatMapResponse>(response.Content);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Review Request Method Failed" + ex);
            }
            return null;
        }
        public TripJackBookingDetailsResponse bookingDetailsRequest(string RequestData, AgentSuppliers agentSuppliers)
        {
            try
            {
                var response = CallPostRequest("booking-details", RequestData, agentSuppliers);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TripJackBookingDetailsResponse>(response.Content);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Review Request Method Failed" + ex);
            }
            return null;
        }
        public TripJackBookingDetailsResponse confirmHoldBookingRequest(string RequestData, AgentSuppliers agentSuppliers)
        {
            try
            {
                var response = CallPostRequest("confirm-book", RequestData, agentSuppliers);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TripJackBookingDetailsResponse>(response.Content);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Review Request Method Failed" + ex);
            }
            return null;
        }
        public TripJackConfirmFareTicketResponse confirmFareTicketRequest(string RequestData, AgentSuppliers agentSuppliers)
        {
            try
            {
                var response = CallPostRequest("fare-validate", RequestData, agentSuppliers);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TripJackConfirmFareTicketResponse>(response.Content);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Review Request Method Failed" + ex);
            }
            return null;
        }
        public TripJackHoldBookResponse holdBookingRequest(string RequestData, AgentSuppliers agentSuppliers)
        {
            try
            {
                var response = CallPostRequest("book", RequestData, agentSuppliers);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TripJackHoldBookResponse>(response.Content);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Review Request Method Failed" + ex);
            }
            return null;
        }
        public TripJackBookResponse instantBookingRequest(string RequestData,AgentSuppliers agentSuppliers)
        {
            try
            {
                var response = CallPostRequest("book", RequestData, agentSuppliers);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TripJackBookResponse>(response.Content);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Review Request Method Failed" + ex);
            }
            return null;
        }
        #endregion

        #region Create Search Request with Markup
        public List<CommonFlightDetails> CreateSearchRequest(FlightRequestDTO commonFlightRequest, AgentSuppliers agentSuppliers, List<CommonFlightDetails> commonFlightDetailsList)
        {
            TripJackSearchRequest tripJackSearchRequest = GetTripJackSearchMarkup(commonFlightRequest);
            var data = JsonConvert.SerializeObject(tripJackSearchRequest);
            var Response = LoadJson();//searchAllRequest(data, agentSuppliers);
            //JToken token = JObject.Parse(Response);
            //var searchResult = token["searchResult"];
            //foreach (JToken tripInfos in searchResult.Children())
            //{
            //    foreach (JToken gChild in tripInfos.Children())
            //    {
            //        foreach (JToken grandChild in gChild)
            //        {
            //            var temp = JsonConvert.DeserializeObject<TripInfo>(gChild.ToString());

            //            //
            //        }
            //    }
            //}
            commonFlightDetailsList = ReadFlightDetails(commonFlightRequest, Response, agentSuppliers, commonFlightDetailsList);
            return commonFlightDetailsList;
        }
        private TripJackSearchRequest GetTripJackSearchMarkup(FlightRequestDTO commonFlightRequest)
        {
            TripJackSearchRequest tripJackSearchRequest = new TripJackSearchRequest();
            tripJackSearchRequest.searchQuery = new SearchQuery();
            tripJackSearchRequest.searchQuery.cabinClass = commonFlightRequest.CabinClass.ToUpper();
            tripJackSearchRequest.searchQuery.paxInfo = new PaxInfo();
            tripJackSearchRequest.searchQuery.paxInfo.ADULT = commonFlightRequest.Adults.ToString();
            tripJackSearchRequest.searchQuery.paxInfo.CHILD = commonFlightRequest.Kids.ToString();
            tripJackSearchRequest.searchQuery.paxInfo.INFANT = commonFlightRequest.Infants.ToString();
            tripJackSearchRequest.searchQuery.routeInfos = new List<RouteInfos>();
            if (commonFlightRequest.JourneyType == JourneyTypes.MultiCity.ToString())
            {
                for (int i = 0; i < commonFlightRequest.flightJourneyRequest.Count; i++)
                {
                    RouteInfos routeInfos = new RouteInfos();
                    routeInfos.fromCityOrAirport = new FromCityOrAirport();
                    routeInfos.fromCityOrAirport.code = commonFlightRequest.flightJourneyRequest[i].LocationFrom.AirportCode;
                    routeInfos.toCityOrAirport = new ToCityOrAirport();
                    routeInfos.toCityOrAirport.code = commonFlightRequest.flightJourneyRequest[i].LocationTo.AirportCode;
                    routeInfos.travelDate = commonFlightRequest.flightJourneyRequest[i].DepartureDate.ToString("yyyy-MM-dd");
                    tripJackSearchRequest.searchQuery.routeInfos.Add(routeInfos);
                }
            }
            else if (commonFlightRequest.JourneyType == JourneyTypes.RoundTrip.ToString())
            {
                RouteInfos routeInfos = new RouteInfos();
                routeInfos.fromCityOrAirport = new FromCityOrAirport();
                routeInfos.fromCityOrAirport.code = commonFlightRequest.flightJourneyRequest[0].LocationFrom.AirportCode;
                routeInfos.toCityOrAirport = new ToCityOrAirport();
                routeInfos.toCityOrAirport.code = commonFlightRequest.flightJourneyRequest[0].LocationTo.AirportCode;
                routeInfos.travelDate = commonFlightRequest.flightJourneyRequest[0].DepartureDate.ToString("yyyy-MM-dd");
                tripJackSearchRequest.searchQuery.routeInfos.Add(routeInfos);

                RouteInfos returnRouteInfos = new RouteInfos();
                returnRouteInfos.fromCityOrAirport = new FromCityOrAirport();
                returnRouteInfos.fromCityOrAirport.code = commonFlightRequest.flightJourneyRequest[0].LocationTo.AirportCode;
                returnRouteInfos.toCityOrAirport = new ToCityOrAirport();
                returnRouteInfos.toCityOrAirport.code = commonFlightRequest.flightJourneyRequest[0].LocationFrom.AirportCode;
                returnRouteInfos.travelDate = commonFlightRequest.flightJourneyRequest[0].ReturnDate.ToString("yyyy-MM-dd");
                tripJackSearchRequest.searchQuery.routeInfos.Add(returnRouteInfos);
            }
            else
            {
                RouteInfos routeInfos = new RouteInfos();
                routeInfos.fromCityOrAirport = new FromCityOrAirport();
                routeInfos.fromCityOrAirport.code = commonFlightRequest.flightJourneyRequest[0].LocationFrom.AirportCode;
                routeInfos.toCityOrAirport = new ToCityOrAirport();
                routeInfos.toCityOrAirport.code = commonFlightRequest.flightJourneyRequest[0].LocationTo.AirportCode;
                routeInfos.travelDate = commonFlightRequest.flightJourneyRequest[0].DepartureDate.ToString("yyyy-MM-dd");
                tripJackSearchRequest.searchQuery.routeInfos.Add(routeInfos);
            }
            return tripJackSearchRequest;
        }
        #endregion
        #region Read All Flights Response
        private List<CommonFlightDetails> ReadFlightDetails(FlightRequestDTO commonFlightRequest, TripJackSearchResponse tripJackSearchResponse, AgentSuppliers agentSupplier, List<CommonFlightDetails> commonFlightDetailsList)
        {
            if (tripJackSearchResponse.searchResult != null)
            {
                SearchResult searchResult = tripJackSearchResponse.searchResult;
                if (searchResult != null)
                {
                    if (searchResult.tripInfos != null)
                    {
                        if (searchResult.tripInfos.ONWARD != null)
                        {
                            List<ONWARD> listLegs = searchResult.tripInfos.ONWARD;//-->Supplier Legs Data
                            for (int i = 0; i < listLegs.Count; i++)
                            {
                                CommonFlightDetails tFLegs = ReadLeg("ONWARD", agentSupplier, listLegs[i].sI, listLegs[i].totalPriceList, commonFlightRequest);
                                commonFlightDetailsList.Add(tFLegs);
                            }
                        }
                        if (searchResult.tripInfos.COMBO != null)
                        {
                            List<COMBO> listLegs = searchResult.tripInfos.COMBO;//-->Supplier Legs Data
                            for (int i = 0; i < listLegs.Count; i++)
                            {
                                CommonFlightDetails tFLegs = ReadLeg("COMBO", agentSupplier, listLegs[i].sI, listLegs[i].totalPriceList, commonFlightRequest);
                                commonFlightDetailsList.Add(tFLegs);
                            }
                        }
                        if (searchResult.tripInfos.RETURN != null)
                        {
                            List<RETURN> listLegs = searchResult.tripInfos.RETURN;//-->Supplier Legs Data
                            for (int i = 0; i < listLegs.Count; i++)
                            {                                
                                CommonFlightDetails tFLegs = ReadLeg("RETURN", agentSupplier, listLegs[i].sI, listLegs[i].totalPriceList, commonFlightRequest);
                                commonFlightDetailsList.Add(tFLegs);
                            }
                        }
                    }
                }
            }
            return commonFlightDetailsList;
        }

        private CommonFlightDetails ReadLeg(string journeyType, AgentSuppliers agentSupplier, List<SI> sI, List<TotalPriceList> totalPriceList, FlightRequestDTO commonFlightRequest)
        {
            CommonFlightDetails tFLegs = new CommonFlightDetails();
            tFLegs.Stops = sI.Count - 1;
            tFLegs.SupplierId = agentSupplier.SupplierId;
            tFLegs.SupplierName = agentSupplier.SupplierName;
            tFLegs.TFId = Guid.NewGuid();
            tFLegs.JourneyType = journeyType;
            tFLegs.tFSegments = new List<TFSegments>();
            for (int j = 0; j < sI.Count; j++)
            {
                if (sI[j] != null)
                {                    
                    TFSegments tFSegments = GetSegmentData(sI[j]);                    
                    tFLegs.tFSegments.Add(tFSegments);
                }
                if (totalPriceList != null)
                {
                    if (totalPriceList.Count > 0)
                    {
                        tFLegs.SupplierLegId = totalPriceList[0].id;
                        TFPriceDetails tFPriceDetails = getPriceDetails(totalPriceList[0]);
                        tFLegs.tFPriceDetails = tFPriceDetails;
                    }
                }
            }
            
            return tFLegs;
        }
        private TFPriceDetails getPriceDetails(TotalPriceList totalPriceList)
        {
            TFPriceDetails tFPriceDetails = new TFPriceDetails();
            TFSupplierPrice tFSupplierPrice = new TFSupplierPrice();
            TFAgentPrice tFAgentPrice = new TFAgentPrice();
            double totalPrice = 0;
            //Adult price
            if (totalPriceList.fd.ADULT != null)
            {
                TFPassengerPrice tFAdultPrice = new TFPassengerPrice();
                tFAdultPrice.Tax = totalPriceList.fd.ADULT.fC.TAF;
                tFAdultPrice.BaseFare = totalPriceList.fd.ADULT.fC.BF;
                tFAdultPrice.NetFare = totalPriceList.fd.ADULT.fC.NF;
                tFAdultPrice.TotalFare = totalPriceList.fd.ADULT.fC.TF;
                tFAdultPrice.CabinBag = totalPriceList.fd.ADULT.bI.cB;
                tFAdultPrice.CabinClass = totalPriceList.fd.ADULT.cc;
                tFAdultPrice.CheckinBag = totalPriceList.fd.ADULT.bI.iB;
                tFAdultPrice.MealType = GetMealType(totalPriceList.fd.ADULT.mI);
                tFAdultPrice.Refundable = GetRefundType(totalPriceList.fd.ADULT.rT);
                TFOtherCharges tFAdultOtherCharges = new TFOtherCharges();
                tFAdultOtherCharges.CarrierMiscFee = totalPriceList.fd.ADULT.afC.TAF.YR;
                tFAdultOtherCharges.OtherCharges = totalPriceList.fd.ADULT.afC.TAF.OT;
                tFAdultOtherCharges.ManagementFee = totalPriceList.fd.ADULT.afC.TAF.MF;
                tFAdultOtherCharges.ManagementFeeTax = totalPriceList.fd.ADULT.afC.TAF.MFT;
                tFAdultOtherCharges.AirlineGST = totalPriceList.fd.ADULT.afC.TAF.AGST;
                tFAdultOtherCharges.FuelSurcharge = totalPriceList.fd.ADULT.afC.TAF.YQ;
                tFAdultPrice.tFOtherCharges = tFAdultOtherCharges;
                tFSupplierPrice.tFAdults = tFAdultPrice;
                tFAgentPrice.tFAdults = tFAdultPrice;
                totalPrice = tFAdultPrice.TotalFare;
            }

            //Kid price
            if (totalPriceList.fd.CHILD != null)
            {
                TFPassengerPrice tFKidPrice = new TFPassengerPrice();
                tFKidPrice.Tax = totalPriceList.fd.CHILD.fC.TAF;
                tFKidPrice.BaseFare = totalPriceList.fd.CHILD.fC.BF;
                tFKidPrice.NetFare = totalPriceList.fd.CHILD.fC.NF;
                tFKidPrice.TotalFare = totalPriceList.fd.CHILD.fC.TF;
                tFKidPrice.CabinBag = totalPriceList.fd.CHILD.bI.cB;
                tFKidPrice.CabinClass = totalPriceList.fd.CHILD.cc;
                tFKidPrice.CheckinBag = totalPriceList.fd.CHILD.bI.iB;
                tFKidPrice.MealType = GetMealType(totalPriceList.fd.CHILD.mI);
                tFKidPrice.Refundable = GetRefundType(totalPriceList.fd.CHILD.rT);

                TFOtherCharges tFKidOtherCharges = new TFOtherCharges();
                tFKidOtherCharges.CarrierMiscFee = totalPriceList.fd.CHILD.afC.TAF.YR;
                tFKidOtherCharges.OtherCharges = totalPriceList.fd.CHILD.afC.TAF.OT;
                tFKidOtherCharges.ManagementFee = totalPriceList.fd.CHILD.afC.TAF.MF;
                tFKidOtherCharges.ManagementFeeTax = totalPriceList.fd.CHILD.afC.TAF.MFT;
                tFKidOtherCharges.AirlineGST = totalPriceList.fd.CHILD.afC.TAF.AGST;
                tFKidOtherCharges.FuelSurcharge = totalPriceList.fd.CHILD.afC.TAF.YQ;
                tFKidPrice.tFOtherCharges = tFKidOtherCharges;
                tFSupplierPrice.tFKids = tFKidPrice;
                tFAgentPrice.tFKids = tFKidPrice;
                totalPrice = totalPrice + tFKidPrice.TotalFare;
            }

            //Infant price
            if (totalPriceList.fd.INFANT != null)
            {
                TFPassengerPrice tFInfantPrice = new TFPassengerPrice();
                tFInfantPrice.Tax = totalPriceList.fd.INFANT.fC.TAF;
                tFInfantPrice.BaseFare = totalPriceList.fd.INFANT.fC.BF;
                tFInfantPrice.NetFare = totalPriceList.fd.INFANT.fC.NF;
                tFInfantPrice.TotalFare = totalPriceList.fd.INFANT.fC.TF;
                tFInfantPrice.CabinBag = totalPriceList.fd.INFANT.bI.cB;
                tFInfantPrice.CabinClass = totalPriceList.fd.INFANT.cc;
                tFInfantPrice.CheckinBag = totalPriceList.fd.INFANT.bI.iB;
                tFInfantPrice.MealType = GetMealType(totalPriceList.fd.INFANT.mI);
                tFInfantPrice.Refundable = GetRefundType(totalPriceList.fd.INFANT.rT);

                TFOtherCharges tFInfantOtherCharges = new TFOtherCharges();
                tFInfantOtherCharges.CarrierMiscFee = totalPriceList.fd.INFANT.afC.TAF.YR;
                tFInfantOtherCharges.OtherCharges = totalPriceList.fd.INFANT.afC.TAF.OT;
                tFInfantOtherCharges.ManagementFee = totalPriceList.fd.INFANT.afC.TAF.MF;
                tFInfantOtherCharges.ManagementFeeTax = totalPriceList.fd.INFANT.afC.TAF.MFT;
                tFInfantOtherCharges.AirlineGST = totalPriceList.fd.INFANT.afC.TAF.AGST;
                tFInfantOtherCharges.FuelSurcharge = totalPriceList.fd.INFANT.afC.TAF.YQ;
                tFInfantPrice.tFOtherCharges = tFInfantOtherCharges;
                tFSupplierPrice.tFInfants = tFInfantPrice;
                tFAgentPrice.tFInfants = tFInfantPrice;
                totalPrice = totalPrice + tFInfantPrice.TotalFare;
            }
            tFSupplierPrice.TotalPrice = totalPrice;
            tFAgentPrice.TotalPrice = totalPrice;
            tFPriceDetails.tFSupplierPrice = tFSupplierPrice;
            return tFPriceDetails;
        }

        private string? GetRefundType(int rT)
        {
            if(rT == 2)
            {
                return CommonEnums.RefundType.PartialRefundable.ToString();
            }
            else if(rT == 1)
            {
                return CommonEnums.RefundType.Refundable.ToString();
            }
            else
            {
                return CommonEnums.RefundType.NonRefundable.ToString();
            }
        }

        private string? GetMealType(bool mI)
        {
            if (mI)
            {
                return CommonEnums.MealType.PaidMeal.ToString();
            }
            else
            {
                return CommonEnums.MealType.FreeMeal.ToString();
            }
        }

        private TFSegments GetSegmentData(SI segments)
        {
            TFSegments tFSegments = new TFSegments();
            tFSegments.SupplierSegmentId = segments.id.ToString();
            tFSegments.Airline = segments.fD.aI.name;
            tFSegments.AirlineCode = segments.fD.aI.code;
            tFSegments.FlightNumber = segments.fD.fN;
            tFSegments.EquimentType = segments.fD.eT;
            tFSegments.Duration = segments.duration;
            tFSegments.ConnectingTime = segments.cT;


            TFDepartureData tFDepartureData = new TFDepartureData();
            tFDepartureData.AirportCode = segments.da.code;
            tFDepartureData.AirportName = segments.da.name;
            tFDepartureData.City = segments.da.city;
            tFDepartureData.CityCode = segments.da.cityCode;
            tFDepartureData.Country = segments.da.country;
            tFDepartureData.CountryCode = segments.da.countryCode;
            tFDepartureData.Terminal = segments.da.terminal;
            tFDepartureData.DepartureDateTime = Convert.ToDateTime(segments.dt);
            tFSegments.tFDepartureData = tFDepartureData;

            TFArrivalData tFArrivalData = new TFArrivalData();
            tFArrivalData.AirportCode = segments.aa.code;
            tFArrivalData.AirportName = segments.aa.name;
            tFArrivalData.City = segments.aa.city;
            tFArrivalData.CityCode = segments.aa.cityCode;
            tFArrivalData.Country = segments.aa.country;
            tFArrivalData.CountryCode = segments.aa.countryCode;
            tFArrivalData.Terminal = segments.aa.terminal;
            tFArrivalData.ArrivalDateTime = Convert.ToDateTime(segments.at);
            tFSegments.tFArrivalData = tFArrivalData;
            return tFSegments;
        }
        #endregion

        #region Review Request
        public TripJackReviewResponse CallReviewRequest(List<CommonFlightDetails> commonFlightDetails, AgentSuppliers agentSuppliers)
        {
            try
            {
                List<CommonFlightDetails> tFLegs = new List<CommonFlightDetails>();
                for (int i = 0; i < commonFlightDetails.Count; i++)
                {
                    for (int j = 0; j < tFLegs.Count; j++)
                    {
                        if (tFLegs[j].SupplierId == agentSuppliers.SupplierId)
                        {
                            tFLegs.Add(tFLegs[j]);
                        }
                    }
                }
                var RequestData = CreateReviewRequest(tFLegs);
                var response = CallPostRequest("review", RequestData, agentSuppliers);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TripJackReviewResponse>(response.Content);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Review Request Method Failed"+ ex);
            }
            return null;
        }
        private string CreateReviewRequest(List<CommonFlightDetails> tFLegs)
        {
            TripJackReviewRequest tripJackReviewRequest = new TripJackReviewRequest();
            List<string> list = new List<string>();
            for (int i = 0; i < tFLegs.Count; i++)
            {                
                list.Add(tFLegs[i].SupplierLegId);
            }
            tripJackReviewRequest.priceIds = list.ToArray();
            return JsonConvert.SerializeObject(tripJackReviewRequest);
        }
        public TripJackFareRuleResponse CallFareRuleRequest(string flightId, AgentSuppliers agentSuppliers)
        {
            try
            {
                TripJackFareRuleRequest tripJackFareRuleRequest = new TripJackFareRuleRequest();
                tripJackFareRuleRequest.id = flightId;
                tripJackFareRuleRequest.flowType = "SEARCH";
                var RequestData = JsonConvert.SerializeObject(tripJackFareRuleRequest);
                var response = CallPostRequest("farerule", RequestData, agentSuppliers);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TripJackFareRuleResponse>(response.Content);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Review Request Method Failed"+ ex);
            }
            return null;
        }
        #endregion

        #region Booking Request
        //public TripJackBookingDetailsResponse CallBookingRequest(string RequestData, CommonFlightRequest commonFlightRequest)
        //{
        //    var RequestData = getBookingRequest();
        //}
        #endregion
        //temporary code
        public TripJackSearchResponse LoadJson()
        {
            using (StreamReader r = new StreamReader("tripjackonewaysearch.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<TripJackSearchResponse>(json);
            }
        }
    }
}
