using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWA.Services
{

    public class PaymentTransacaoESITEF
    {
        public string status { get; set; }
        public string nit { get; set; }
        public string order_id { get; set; }
        public string merchant_usn { get; set; }
        public string amount { get; set; }
    }

    public class TransacaoESITEF
    {
        public string code { get; set; }
        public string message { get; set; }
        public PaymentTransacaoESITEF payment { get; set; }
    }


    public class PaymentPagamentoESITEF
    {
        public string authorizer_code { get; set; }
        public string authorizer_message { get; set; }
        public string status { get; set; }
        public string nit { get; set; }
        public string order_id { get; set; }
        public string customer_receipt { get; set; }
        public string merchant_receipt { get; set; }
        public string authorizer_id { get; set; }
        public string acquirer_id { get; set; }
        public string acquirer_name { get; set; }
        public string authorizer_date { get; set; }
        public string authorization_number { get; set; }
        public string merchant_usn { get; set; }
        public string esitef_usn { get; set; }
        public string sitef_usn { get; set; }
        public string host_usn { get; set; }
        public string payment_date { get; set; }
        public string amount { get; set; }
        public string payment_type { get; set; }
        public string issuer { get; set; }
        public string authorizer_merchant_id { get; set; }
    }

    public class PagamentoESITEF
    {
        public string code { get; set; }
        public string message { get; set; }
        public PaymentPagamentoESITEF payment { get; set; }
    }

}