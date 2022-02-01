using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWA.Services
{
    public class LinkExtorno
    {
        public string Method { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
    }

    public class ResultExtornoBRASPAG
    {
        public int Status { get; set; }
        public int ReasonCode { get; set; }
        public string ReasonMessage { get; set; }
        public string ProviderReturnCode { get; set; }
        public string ProviderReturnMessage { get; set; }
        public List<LinkExtorno> Links { get; set; }
    }
}