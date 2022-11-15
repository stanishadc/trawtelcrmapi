using System.Collections.Generic;

namespace TripJack
{
    public class TripJackSearchResponse
    {
        public SearchResult searchResult { get; set; }
        public Status status { get; set; }
        public MetaInfo metaInfo { get; set; }
    }
    public class AI
    {
        public string code { get; set; }
        public string name { get; set; }
        public bool isLcc { get; set; }
    }
    public class FD
    {
        public AI aI { get; set; }
        public string fN { get; set; }
        public string eT { get; set; }
    }
    public class Da
    {
        public string code { get; set; }
        public string name { get; set; }
        public string cityCode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string terminal { get; set; }
    }
    public class Aa
    {
        public string code { get; set; }
        public string name { get; set; }
        public string cityCode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string terminal { get; set; }
    }
    public class OB
    {
        public string code { get; set; }
        public string name { get; set; }
        public bool isLcc { get; set; }
    }
    public class SI
    {
        public int id { get; set; }
        public FD fD { get; set; }
        public int stops { get; set; }
        public List<object> so { get; set; }
        public int duration { get; set; }
        public int cT { get; set; }
        public Da da { get; set; }
        public Aa aa { get; set; }
        public string dt { get; set; }
        public string at { get; set; }
        public bool iand { get; set; }
        public bool isRs { get; set; }
        public int sN { get; set; }
        public OB oB { get; set; }
    }
    public class FC
    {
        public double BF { get; set; }
        public double TAF { get; set; }
        public double NF { get; set; }
        public double TF { get; set; }
    }
    public class TAF
    {
        public double OT { get; set; }
        public double AGST { get; set; }
        public double MF { get; set; }
        public double MFT { get; set; }
        public double YQ { get; set; }
        public double? YR { get; set; }
    }
    public class AfC
    {
        public TAF TAF { get; set; }
    }
    public class BI
    {
        public string iB { get; set; }
        public string cB { get; set; }
    }
    public class Fd2
    {
        public ADULT ADULT { get; set; }
        public CHILD CHILD { get; set; }
        public INFANT INFANT { get; set; }
    }
    public class ADULT
    {
        public FC fC { get; set; }
        public AfC afC { get; set; }
        public int sR { get; set; }
        public BI bI { get; set; }
        public int rT { get; set; }
        public string cc { get; set; }
        public string cB { get; set; }
        public string fB { get; set; }
        public bool mI { get; set; }
        public string iB { get; set; }
    }
    public class CHILD
    {
        public FC fC { get; set; }
        public AfC afC { get; set; }
        public int sR { get; set; }
        public BI bI { get; set; }
        public int rT { get; set; }
        public string cc { get; set; }
        public string cB { get; set; }
        public string fB { get; set; }
        public bool mI { get; set; }
        public string iB { get; set; }
    }
    public class INFANT
    {
        public FC fC { get; set; }
        public AfC afC { get; set; }
        public int sR { get; set; }
        public BI bI { get; set; }
        public int rT { get; set; }
        public string cc { get; set; }
        public string cB { get; set; }
        public string fB { get; set; }
        public bool mI { get; set; }
        public string iB { get; set; }
    }
    
    public class TotalPriceList
    {
        public Fd2 fd { get; set; }
        public string fareIdentifier { get; set; }
        public string id { get; set; }
        public List<object> msri { get; set; }
        public Tai tai { get; set; }
    }
    public class Tai
    {
        public Tbi tbi { get; set; }
    }
    public class Tbi
    {
        public List<object> list { get; set; }
    }
    public class ONWARD
    {
        public List<SI> sI { get; set; }
        public List<TotalPriceList> totalPriceList { get; set; }
    }
    public class TripInfos
    {
        public List<ONWARD> ONWARD { get; set; }
        public List<COMBO> COMBO { get; set; }
        public List<RETURN> RETURN { get; set; }        
    }
    public class RETURN
    {
        public List<SI> sI { get; set; }
        public List<TotalPriceList> totalPriceList { get; set; }
    }
    public class COMBO
    {
        public List<SI> sI { get; set; }
        public List<TotalPriceList> totalPriceList { get; set; }
    }
    public class SearchResult
    {
        public TripInfos tripInfos { get; set; }
    }
    public class Status
    {
        public bool success { get; set; }
        public int httpStatus { get; set; }
    }
    public class MetaInfo
    {
    }
}
