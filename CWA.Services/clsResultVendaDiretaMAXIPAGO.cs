using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWA.Services
{
    public class TransactionResponse
    {
        public string authCode { get; set; }
        public string orderID { get; set; }
        public string referenceNum { get; set; }
        public string transactionID { get; set; }
        public string transactionTimestamp { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string avsResponseCode { get; set; }
        public string cvvResponseCode { get; set; }
        public string processorCode { get; set; }
        public string processorMessage { get; set; }
        public string processorName { get; set; }
        public string creditCardBin { get; set; }
        public string creditCardLast4 { get; set; }
        public object errorMessage { get; set; }
        public string processorTransactionID { get; set; }
        public string processorReferenceNumber { get; set; }
        public string creditCardCountry { get; set; }
        public string creditCardScheme { get; set; }
    }
}