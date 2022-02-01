using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWA.Services
{
    public class Customer
    {
        public string Name { get; set; }
    }

    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string Holder { get; set; }
        public string ExpirationDate { get; set; }
        public bool SaveCard { get; set; }
        public string Brand { get; set; }
        public string CardToken { get; set; }
    }

    public class Link
    {
        public string Method { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
    }

    public class RecurrentPayment
    {
        public string RecurrentPaymentId { get; set; }
        public int ReasonCode { get; set; }
        public string ReasonMessage { get; set; }
        public string NextRecurrency { get; set; }
        public string EndDate { get; set; }
        public string Interval { get; set; }
        public Link Link { get; set; }
        public bool AuthorizeNow { get; set; }
    }

    public class Payment
    {
        public int ServiceTaxAmount { get; set; }
        public int Installments { get; set; }
        public string Interest { get; set; }
        public bool Capture { get; set; }
        public bool Authenticate { get; set; }
        public bool Recurrent { get; set; }
        public CreditCard CreditCard { get; set; }
        public string ProofOfSale { get; set; }
        public string AcquirerTransactionId { get; set; }
        public string AuthorizationCode { get; set; }
        public string PaymentId { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public string ReceivedDate { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string Provider { get; set; }
        public int ReasonCode { get; set; }
        public string ReasonMessage { get; set; }
        public int Status { get; set; }
        public string ProviderReturnCode { get; set; }
        public string ProviderReturnMessage { get; set; }
        public RecurrentPayment RecurrentPayment { get; set; }
        public List<Link> Links { get; set; }
    }

    public class ResultVendaBRASPAG
    {
        public string MerchantOrderId { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
    }

    public class ResultVendaMerchantOrderIdBRASPAG
    {
        public int ReasonCode { get; set; }
        public string ReasonMessage { get; set; }
        public List<Payment> Payments { get; set; }
    }

    public class PaymentMerchantOrderId
    {
        public string PaymentId { get; set; }
        public string ReceveidDate { get; set; }
    }
}