using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripJack
{
    public class TripJackFareRuleResponse
    {
        public FareRule fareRule { get; set; }
        public Status status { get; set; }
    }
    public class CANCELLATION
    {
        public DEFAULT? DEFAULT { get; set; }
    }

    public class DATECHANGE
    {
        public DEFAULT? DEFAULT { get; set; }
    }

    public class DEFAULT
    {
        public string policyInfo { get; set; }
        public int amount { get; set; }
        public int additionalFee { get; set; }
        public Fcs fcs { get; set; }
    }

    public class DELBOM
    {
        public FareRuleInfo fareRuleInfo { get; set; }
    }

    public class FareRule
    {
        [JsonProperty("DEL-BOM")]
        public DELBOM DELBOM { get; set; }
    }

    public class FareRuleInfo
    {
        public NOSHOW NO_SHOW { get; set; }
        public DATECHANGE DATECHANGE { get; set; }
        public CANCELLATION CANCELLATION { get; set; }
        public SEATCHARGEABLE SEAT_CHARGEABLE { get; set; }
    }

    public class Fcs
    {
        public double ARFT { get; set; }
        public int CRF { get; set; }
        public int ARF { get; set; }
        public int CRFT { get; set; }
        public int ACFT { get; set; }
        public int CCF { get; set; }
        public int CCFT { get; set; }
        public int ACF { get; set; }
    }

    public class NOSHOW
    {
        public DEFAULT? DEFAULT { get; set; }
    }
    public class SEATCHARGEABLE
    {
        public DEFAULT? DEFAULT { get; set; }
    }
}
