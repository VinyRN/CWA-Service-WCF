using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Net;
using System.Xml;

using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

using CWA.Venda.Business;
using CWA.Venda.Entity;
using CWA.Util;
using CWA.Central.Business;


//=============================================================================
//Boleto Online
//=============================================================================
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs; //incluir System.Security.dll
using System.ServiceModel;
//=============================================================================

namespace CWA.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceCWA" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceCWA.svc or ServiceCWA.svc.cs at the Solution Explorer and start debugging.
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ServiceCWA : IServiceCWA
    {
        private System.Int32 _erro;
        private string _msgErro;

        public System.Int32 Erro
        {
            get { return _erro; }
        }

        public string MsgErro
        {
            get { return _msgErro; }
        }

        private System.Int32 _contErroGO_VA;
        private System.Int32 _contOKGO_VA;
        private System.Int32 _contErroNota;

        public System.Int32 ContErroGO_VA
        {
            get { return _contErroGO_VA; }
        }

        public System.Int32 ContOKGO_VA
        {
            get { return _contOKGO_VA; }
        }

        public System.Int32 ContErroNota
        {
            get { return _contErroNota; }
        }


        public string[] BRASPAG_GetVendaPaymentId(string pstrUrlPost,
                                                        string pstrContentType,
                                                        string pstrMethod,
                                                        string pstrMerchantID,
                                                        string pstrMerchantKey,
                                                        string pstrRequestId,
                                                        string pstrPaymentId)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrUrlPost + pstrPaymentId);
                httpWebRequest.ContentType = pstrContentType; // "application/json";
                httpWebRequest.Method = pstrMethod;          //  "POST";
                httpWebRequest.Headers.Add("MerchantId", pstrMerchantID);
                httpWebRequest.Headers.Add("MerchantKey", pstrMerchantKey);
                httpWebRequest.Headers.Add("RequestId", pstrRequestId);

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string[] lstrRetorno = null;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    ResultVendaBRASPAG ObjResult = JsonConvert.DeserializeObject<ResultVendaBRASPAG>(result);

                    if (ObjResult != null)
                    {

                        lstrRetorno = TratarRetornoVendaSimplesBRASPAG(ObjResult);
                    }

                }

                return lstrRetorno;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }

        }

        

        public string[] BRASPAG_GetVendaMerchantOrderId(string pstrUrlPost,
                                                        string pstrContentType,
                                                        string pstrMethod,
                                                        string pstrMerchantID,
                                                        string pstrMerchantKey,
                                                        string pstrRequestId,
                                                        string pstrMerchantOrderId)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrUrlPost + pstrMerchantOrderId);
                httpWebRequest.ContentType = pstrContentType; // "application/json";
                httpWebRequest.Method = pstrMethod;          //  "POST";
                httpWebRequest.Headers.Add("MerchantId", pstrMerchantID);
                httpWebRequest.Headers.Add("MerchantKey", pstrMerchantKey);
                httpWebRequest.Headers.Add("RequestId", pstrRequestId);

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string[] lstrRetorno = null;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    ResultVendaMerchantOrderIdBRASPAG ObjResult = JsonConvert.DeserializeObject<ResultVendaMerchantOrderIdBRASPAG>(result);

                    if (ObjResult != null)
                    {
                        if (ObjResult.ReasonCode == 0)
                        {
                            lstrRetorno = new string[] { ObjResult.Payments[0].PaymentId };
                        }
                        else
                        {
                            _erro = -99;
                            _msgErro = ObjResult.ReasonMessage;
                            return null;
                        }                        
                    }

                }

                return lstrRetorno;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }

        }

        public string[] BRASPAG_VendaSimplesCartao(string pstrUrlPost,
                                                 string pstrContentType,
                                                 string pstrMethod,
                                                 string pstrMerchantID,
                                                 string pstrMerchantKey,
                                                 string pstrRequestId,
                                                 string pstrProviderPagamento,
                                                 string pstrNomeComprador,
                                                 string pstrValor,
                                                 string pstrParcela,
                                                 string pstrCartao,
                                                 string pstrNomeCompradorImpressoCartao,
                                                 string pstrValidade,
                                                 string pstrCVV,
                                                 string pstrBandeira,
                                                 string pstrIdentificadorVenda,
                                                 string pstrSetToken = "N")
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrUrlPost);

                httpWebRequest.ContentType = pstrContentType; // "application/json";
                httpWebRequest.Method = pstrMethod;          //  "POST";
                httpWebRequest.Headers.Add("MerchantId", pstrMerchantID);
                httpWebRequest.Headers.Add("MerchantKey", pstrMerchantKey);
                httpWebRequest.Headers.Add("RequestId", pstrRequestId);

                string lstrMerchantOrderId = pstrIdentificadorVenda;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{";
                    json = json + "\"MerchantOrderId\":\"" + lstrMerchantOrderId + "\",";
                    json = json + "\"Customer\":{";
                    json = json + "\"Name\":\"" + pstrNomeComprador + "\"";
                    json = json + "},";
                    json = json + "\"Payment\":{";

                    json = json + "\"Provider\":\"" + pstrProviderPagamento + "\",";

                    json = json + "\"Type\":\"CreditCard\",";
                    json = json + "\"Amount\":" + pstrValor + ",";

                    //json = json + "\"Capture\":\"true\",";
                    json = json + "\"Capture\":true,";

                    json = json + "\"Installments\":" + pstrParcela + ",";
                    
                    json = json + "\"CreditCard\":{";
                    json = json + "\"CardNumber\":\"" + pstrCartao + "\",";
                    json = json + "\"Holder\":\"" + pstrNomeCompradorImpressoCartao + "\",";
                    json = json + "\"ExpirationDate\":\"" + pstrValidade + "\",";
                    json = json + "\"SecurityCode\":\"" + pstrCVV + "\",";

                    if (pstrSetToken.Trim() == "N")
                    {
                        json = json + "\"Brand\":\"" + pstrBandeira + "\"";
                    }
                    else
                    {
                        json = json + "\"Brand\":\"" + pstrBandeira + "\",";
                        //json = json + "\"SaveCard\":\"true\",";
                        json = json + "\"SaveCard\":true,";
                        json = json + "\"Alias\":\"\"";
                    }

                    json = json + "}";
                    json = json + "}";
                    json = json + "}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string[] lstrRetorno = null;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    ResultVendaBRASPAG ObjResult = JsonConvert.DeserializeObject<ResultVendaBRASPAG>(result);

                    if (ObjResult != null)
                    {
                        lstrRetorno = TratarRetornoVendaSimplesBRASPAG(ObjResult);
                    }

                }

                return lstrRetorno;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                string[] lstrRetErro = { Erro.ToString(), MsgErro };
                return lstrRetErro;
            }
        }


        public string[] BRASPAG_VendaSimplesCartaoToken(string pstrUrlPost,
                                                         string pstrContentType,
                                                         string pstrMethod,
                                                         string pstrMerchantID,
                                                         string pstrMerchantKey,
                                                         string pstrRequestId,
                                                         string pstrProviderPagamento,
                                                         string pstrNomeComprador,
                                                         string pstrValor,
                                                         string pstrParcela,
                                                         string pstrCardToken,
                                                         string pstrCVV,
                                                         string pstrBandeira,
                                                         string pstrIdentificadorVenda)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrUrlPost);

                httpWebRequest.ContentType = pstrContentType; // "application/json";
                httpWebRequest.Method = pstrMethod;          //  "POST";
                httpWebRequest.Headers.Add("MerchantId", pstrMerchantID);
                httpWebRequest.Headers.Add("MerchantKey", pstrMerchantKey);
                httpWebRequest.Headers.Add("RequestId", pstrRequestId);

                string lstrMerchantOrderId = pstrIdentificadorVenda;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{";
                    json = json + "\"MerchantOrderId\":\"" + lstrMerchantOrderId + "\",";
                    json = json + "\"Customer\":{";
                    json = json + "\"Name\":\"" + pstrNomeComprador + "\"";
                    json = json + "},";
                    json = json + "\"Payment\":{";

                    json = json + "\"Provider\":\"" + pstrProviderPagamento + "\",";

                    json = json + "\"Type\":\"CreditCard\",";
                    json = json + "\"Amount\":" + pstrValor + ",";

                    json = json + "\"Capture\":\"true\",";

                    json = json + "\"Installments\":" + pstrParcela + ",";

                    json = json + "\"CreditCard\":{";
                    json = json + "\"CardToken\":\"" + pstrCardToken + "\",";
                    if (pstrCVV.Trim() != "")
                    {
                        json = json + "\"SecurityCode\":\"" + pstrCVV + "\",";
                    }
                    else
                    {
                        json = json + "\"SecurityCode\":null,";
                    }
                    
                    json = json + "\"Brand\":\"" + pstrBandeira + "\"";
                    json = json + "}";
                    json = json + "}";
                    json = json + "}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string[] lstrRetorno = null;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    ResultVendaBRASPAG ObjResult = JsonConvert.DeserializeObject<ResultVendaBRASPAG>(result);

                    if (ObjResult != null)
                    {
                        lstrRetorno = TratarRetornoVendaSimplesBRASPAG(ObjResult);
                    }

                }

                return lstrRetorno;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                string[] lstrRetErro = { Erro.ToString(), MsgErro };
                return lstrRetErro;
            }
        }




        public string[] BRASPAG_SetExtornoCartao(string pstrUrlPost,
                                                 string pstrContentType,
                                                 string pstrMethod,
                                                 string pstrMerchantID,
                                                 string pstrMerchantKey,
                                                 string pstrRequestId,
                                                 string pstrPaymentId,
                                                 string pstrValor)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                //https://apiquerysandbox.braspag.com.br/sales/{PaymentId}/void?amount=
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrUrlPost + pstrPaymentId + "/void?amount=" + pstrValor);
                httpWebRequest.ContentType = pstrContentType; // "application/json";
                httpWebRequest.Method = pstrMethod;          //  "POST";
                httpWebRequest.Headers.Add("MerchantId", pstrMerchantID);
                httpWebRequest.Headers.Add("MerchantKey", pstrMerchantKey);
                httpWebRequest.Headers.Add("RequestId", pstrRequestId);

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string[] lstrRetorno = null;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    ResultExtornoBRASPAG ObjResult = JsonConvert.DeserializeObject<ResultExtornoBRASPAG>(result);

                    if (ObjResult != null)
                    {

                        lstrRetorno = TratarRetornoExtornoBRASPAG(ObjResult);
                    }

                }

                return lstrRetorno;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return new string[] { Erro.ToString(), MsgErro };
            }

        }

        public string[] ESITEF_VendaAutomaticaTransacao(string pstrUrlPost,
                                                        string pstrContentType,
                                                        string pstrMethod,
                                                        string pstrMerchantID,
                                                        string pstrMerchantKey,
                                                        string pstrMerchantUsn, //guid
                                                        string pstrTipoParcelamento, //3 parcelado pela ADM com juros - 4 parcelado pelo Jornal sem juros
                                                        string pstrValor,
                                                        string pstrParcela,
                                                        string pstrBandeira,
                                                        string pstrIdentificadorVenda)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrUrlPost);
                httpWebRequest.ContentType = pstrContentType; // "application/json";
                httpWebRequest.Method = pstrMethod;          //  "POST";
                httpWebRequest.Headers.Add("merchant_id", pstrMerchantID);
                httpWebRequest.Headers.Add("merchant_key", pstrMerchantKey);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{";
                    json = json + "\"merchant_usn\":\"" + pstrMerchantUsn + "\",";
                    json = json + "\"order_id\":\"" + pstrIdentificadorVenda + "\",";
                    json = json + "\"installments\":" + pstrParcela + ",";
                    json = json + "\"installment_type\":" + pstrTipoParcelamento + ",";
                    json = json + "\"authorizer_id\":" + pstrBandeira + ",";
                    json = json + "\"amount\":" + pstrValor;
                    json = json + "}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string[] lstrRetorno = null;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    object ObjResult = JsonConvert.DeserializeObject<TransacaoESITEF>(result);

                    if (ObjResult != null)
                    {
                        lstrRetorno = TratarRetornoVendaESITEF(ObjResult, 1);
                    }

                }

                return lstrRetorno;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                string[] lstrRetErro = { Erro.ToString(), MsgErro };
                return lstrRetErro;
            }
        }


        public string[] ESITEF_VendaAutomaticaPagamento(string pstrUrlPost,
                                                        string pstrContentType,
                                                        string pstrMethod,
                                                        string pstrMerchantID,
                                                        string pstrMerchantKey,
                                                        string pstrnit, 
                                                        string pstrNumeroCartao,
                                                        string pstrDataValidade,
                                                        string pstrCVV
                                                        )
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                string pstrUrlPostNit = pstrUrlPost + "/" + pstrnit;


                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrUrlPostNit);
                httpWebRequest.ContentType = pstrContentType; // "application/json";
                httpWebRequest.Method = pstrMethod;          //  "POST";
                httpWebRequest.Headers.Add("merchant_id", pstrMerchantID);
                httpWebRequest.Headers.Add("merchant_key", pstrMerchantKey);
                
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{";
                    json = json + "\"card\":{";
                    json = json + "\"number\":\"" + pstrNumeroCartao + "\",";
                    json = json + "\"expiry_date\":\"" + pstrDataValidade + "\",";
                    json = json + "\"security_code\":" + pstrCVV;
                    json = json + "}";
                    json = json + "}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string[] lstrRetorno = null;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    object ObjResult = JsonConvert.DeserializeObject<PagamentoESITEF>(result);

                    if (ObjResult != null)
                    {
                        lstrRetorno = TratarRetornoVendaESITEF(ObjResult,2);
                    }

                }

                return lstrRetorno;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                string[] lstrRetErro = { Erro.ToString(), MsgErro };
                return lstrRetErro;
            }
        }

  
        public string[] MAXIPAGO_VendaSimplesCartao(string pstrUrlPost,
                                                    string pstrContentType,
                                                    string pstrMethod,
                                                    string pstrMerchantID,
                                                    string pstrMerchantKey,
                                                    string pstrVersion,
                                                    string pstrProcessorID,
                                                    string pstrfraudCheck,
                                                    string pstrReferenceNum,
                                                    string pstrCartao,
                                                    string pstrValidade,
                                                    string pstrCVV,
                                                    string pstrValor,
                                                    string pstrParcela,
                                                    string pstrTipoPacelamento
                                                    )
        {
            try
            {
                string lstrXML = "";

                lstrXML = lstrXML + "<?xml version='1.0' encoding='UTF-8'?>";
                lstrXML = lstrXML + "<transaction-request>";
                lstrXML = lstrXML + "<version>" + pstrVersion + "</version>";

                lstrXML = lstrXML + "<verification>";
                lstrXML = lstrXML + "<merchantId>" + pstrMerchantID + "</merchantId>";
                lstrXML = lstrXML + "<merchantKey>" + pstrMerchantKey + "</merchantKey>";
                lstrXML = lstrXML + "</verification>";
                lstrXML = lstrXML + "<order>";
                lstrXML = lstrXML + "<sale>";
                lstrXML = lstrXML + "<processorID>" + pstrProcessorID + "</processorID>";
                lstrXML = lstrXML + "<fraudCheck>" + pstrfraudCheck + "</fraudCheck>";
                lstrXML = lstrXML + "<referenceNum>" + pstrReferenceNum + "</referenceNum>";

                lstrXML = lstrXML + "<transactionDetail>";
                lstrXML = lstrXML + "<payType>";
                lstrXML = lstrXML + "<creditCard>";
                lstrXML = lstrXML + "<number>" + pstrCartao + "</number>";
                lstrXML = lstrXML + "<expMonth>" + pstrValidade.Substring(0, 2) + "</expMonth>";
                lstrXML = lstrXML + "<expYear>" + pstrValidade.Substring(3, 4) + "</expYear>";
                lstrXML = lstrXML + "<cvvNumber>" + pstrCVV + "</cvvNumber>";
                lstrXML = lstrXML + "</creditCard>";
                lstrXML = lstrXML + "</payType>";
                lstrXML = lstrXML + "</transactionDetail>";

                lstrXML = lstrXML + "<payment>";
                lstrXML = lstrXML + "<chargeTotal>" + pstrValor + "</chargeTotal>";
                lstrXML = lstrXML + "<currencyCode>BRL</currencyCode>";

                lstrXML = lstrXML + "<creditInstallment>";
                lstrXML = lstrXML + "<numberOfInstallments>" + pstrParcela + "</numberOfInstallments>";
                lstrXML = lstrXML + "<chargeInterest>N</chargeInterest>";
                lstrXML = lstrXML + "</creditInstallment>";

                lstrXML = lstrXML + "</payment>";

                lstrXML = lstrXML + "</sale>";
                lstrXML = lstrXML + "</order>";
                lstrXML = lstrXML + "</transaction-request>";

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                HttpWebRequest req = null;
                WebResponse rsp = null;

                req = (System.Net.HttpWebRequest)HttpWebRequest.Create(pstrUrlPost);
                req.Method = pstrMethod; //"POST";
                req.ContentType = pstrContentType;  //"text/xml; charset=UTF-8";

                req.Timeout = 99999;

                using (StreamWriter writer = new StreamWriter(req.GetRequestStream()))
                {
                    writer.Write(lstrXML);
                }

              
                rsp = req.GetResponse();

       
                string responseContent = null;
                using (System.IO.StreamReader reader = new System.IO.StreamReader(rsp.GetResponseStream()))
                {
                    responseContent = reader.ReadToEnd();
                }

                Utility.SetLogXML("Service-MAXIPAGO_VendaSimplesCartao", "Info", "Fim do Processamento de Retorno MAXIPAGO", false);

                string[] lstrVetorRet;
                lstrVetorRet = TratarRetornoVendaSimplesMAXIPAGO(responseContent);

                return lstrVetorRet;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                Utility.SetLogXML("Service-MAXIPAGO_VendaSimplesCartao-ErrorGeral", "Error", ex.Message + "-" + ex.InnerException, false);

                return null;
            }
        }

        public string[] MAXIPAGO_GetVendaPaymentId(string pstrUrlPost,
                                                   string pstrContentType,
                                                   string pstrMethod,
                                                   string pstrMerchantID,
                                                   string pstrMerchantKey,
                                                   string pstrRequestId,
                                                   string pstrPaymentId)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                string url = pstrUrlPost;

                string xml = "";

                xml = xml + "<?xml version='1.0' encoding='UTF-8'?>";
                xml = xml + "<rapi-request>";
                xml = xml + "<verification>";
                xml = xml + "<merchantId>" + pstrMerchantID + "</merchantId>";
                xml = xml + "<merchantKey>" + pstrMerchantKey + "</merchantKey>";
                xml = xml + "</verification>";

                xml = xml + "<command>transactionDetailReport</command>";
                xml = xml + "<request>";
                xml = xml + "<filterOptions>";
                //xml = xml + "<transactionId>" + pstrPaymentId + "</transactionId>";
                xml = xml + "<referenceNum > " + pstrPaymentId + "</referenceNum>";
                xml = xml + "</filterOptions>";
                xml = xml + "</request>";

                xml = xml + "</rapi-request>";

                HttpWebRequest req = null;


                WebResponse rsp = null;

                req = (System.Net.HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = pstrMethod; //"POST";
                req.ContentType = pstrContentType; //"text/xml; charset=UTF-8";

                req.Timeout = 99999;

                using (StreamWriter writer = new StreamWriter(req.GetRequestStream()))
                {
                    writer.Write(xml);
                }

                rsp = req.GetResponse();

                string responseContent = null;
                using (System.IO.StreamReader reader = new System.IO.StreamReader(rsp.GetResponseStream()))
                {
                    responseContent = reader.ReadToEnd();
                }

                return new string[] { "0", responseContent };
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }

        }

        private string[] TratarRetornoVendaSimplesMAXIPAGO(string pXMLResult)
        {

            try
            {
                string lstrResult = "";

                XmlDocument ObjDocXML = new XmlDocument();
                ObjDocXML.LoadXml(pXMLResult);

                XmlNodeList nodelist = ObjDocXML.SelectNodes("transaction-response");

                foreach (XmlNode node in nodelist)
                {
                    lstrResult = lstrResult + node.SelectSingleNode("orderID").InnerText + ";";
                    lstrResult = lstrResult + node.SelectSingleNode("transactionID").InnerText + ";";
                    lstrResult = lstrResult + node.SelectSingleNode("authCode").InnerText + ";";
                    lstrResult = lstrResult + node.SelectSingleNode("referenceNum").InnerText + ";";
                    lstrResult = lstrResult + node.SelectSingleNode("responseCode").InnerText + ";";
                    lstrResult = lstrResult + node.SelectSingleNode("responseMessage").InnerText + ";";
                    lstrResult = lstrResult + node.SelectSingleNode("errorMessage").InnerText + ";";

                    if (node.SelectSingleNode("responseCode").InnerText.Trim() == "0")
                    {
                        lstrResult = lstrResult + node.SelectSingleNode("processorTransactionID").InnerText + ";";
                        lstrResult = lstrResult + node.SelectSingleNode("processorReferenceNumber").InnerText + ";";
                    }
                }

                Utility.SetLogXML("Service-TratarRetornoVendaSimplesMAXIPAGO-Retorno", "Info", lstrResult, false);

                return lstrResult.Split(';');

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                string[] lstrRetErro = { Erro.ToString(), MsgErro };
                return lstrRetErro;
            }
        }

        private string[] TratarRetornoVendaSimplesBRASPAG(ResultVendaBRASPAG pEntResult)
        {

            try
            {
                string lstrResult = "";

                //Dados Gerais    
                lstrResult = lstrResult + pEntResult.Payment.ServiceTaxAmount + ";";
                lstrResult = lstrResult + pEntResult.Payment.Installments + ";";
                lstrResult = lstrResult + pEntResult.Payment.Interest + ";";
                lstrResult = lstrResult + pEntResult.Payment.Capture + ";";
                lstrResult = lstrResult + pEntResult.Payment.Authenticate + ";";
                lstrResult = lstrResult + pEntResult.Payment.Recurrent + ";";
                lstrResult = lstrResult + pEntResult.Payment.ProofOfSale + ";";
                lstrResult = lstrResult + pEntResult.Payment.AcquirerTransactionId + ";";
                lstrResult = lstrResult + pEntResult.Payment.AuthorizationCode + ";";
                lstrResult = lstrResult + pEntResult.Payment.PaymentId + ";";
                lstrResult = lstrResult + pEntResult.Payment.Type + ";";
                lstrResult = lstrResult + pEntResult.Payment.Amount + ";";
                lstrResult = lstrResult + pEntResult.Payment.ReceivedDate + ";";
                lstrResult = lstrResult + pEntResult.Payment.Currency + ";";
                lstrResult = lstrResult + pEntResult.Payment.Country + ";";
                lstrResult = lstrResult + pEntResult.Payment.Provider + ";";
                lstrResult = lstrResult + pEntResult.Payment.ReasonCode + ";";
                lstrResult = lstrResult + pEntResult.Payment.ReasonMessage + ";";
                lstrResult = lstrResult + pEntResult.Payment.Status + ";";
                lstrResult = lstrResult + pEntResult.Payment.ProviderReturnCode + ";";
                lstrResult = lstrResult + pEntResult.Payment.ProviderReturnMessage + ";";
                lstrResult = lstrResult + pEntResult.Payment.Status + ";";
                lstrResult = lstrResult + pEntResult.Payment.RecurrentPayment + ";";
                //=============================================================================================

                //Dados do Cartão
                lstrResult = lstrResult + pEntResult.Payment.CreditCard.CardNumber + ";";
                lstrResult = lstrResult + pEntResult.Payment.CreditCard.Holder + ";";
                lstrResult = lstrResult + pEntResult.Payment.CreditCard.ExpirationDate + ";";
                lstrResult = lstrResult + pEntResult.Payment.CreditCard.SaveCard + ";";
                lstrResult = lstrResult + pEntResult.Payment.CreditCard.Brand + ";";
                lstrResult = lstrResult + pEntResult.Payment.CreditCard.CardToken + ";";
                //=============================================================================================

                if (pEntResult.Payment.ReasonCode == 0)
                {
                    //Dados do Links
                    lstrResult = lstrResult + pEntResult.Payment.Links[0].Method + ";";
                    lstrResult = lstrResult + pEntResult.Payment.Links[0].Rel + ";";
                    lstrResult = lstrResult + pEntResult.Payment.Links[0].Href + ";";

                    if (pEntResult.Payment.Links.Count > 1)
                    {
                        lstrResult = lstrResult + pEntResult.Payment.Links[1].Method + ";";
                        lstrResult = lstrResult + pEntResult.Payment.Links[1].Rel + ";";
                        lstrResult = lstrResult + pEntResult.Payment.Links[1].Href + ";";
                    }

                    if (pEntResult.Payment.Links.Count > 2)
                    {
                        lstrResult = lstrResult + pEntResult.Payment.Links[2].Method + ";";
                        lstrResult = lstrResult + pEntResult.Payment.Links[2].Rel + ";";
                        lstrResult = lstrResult + pEntResult.Payment.Links[2].Href;
                    }
                    //=============================================================================================

                }

                return lstrResult.Split(';');

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                string[] lstrRetErro = { Erro.ToString(), MsgErro };
                return lstrRetErro;
            }
        }

        private string[] TratarRetornoVendaESITEF(object pEntResult, int pintTipoRetorno)
        {

            try
            {
                string lstrResult = "";

                if (pintTipoRetorno == 1 )
                {

                    TransacaoESITEF ObjTransacao = new TransacaoESITEF();
                    ObjTransacao = (TransacaoESITEF)pEntResult;

                    //Dados Gerais    
                    lstrResult = lstrResult + ObjTransacao.code + ";";
                    lstrResult = lstrResult + ObjTransacao.message + ";";

                    if (ObjTransacao.code == "0")
                    {
                        lstrResult = lstrResult + ObjTransacao.payment.status + ";";
                        lstrResult = lstrResult + ObjTransacao.payment.nit + ";";
                        lstrResult = lstrResult + ObjTransacao.payment.order_id + ";";
                        lstrResult = lstrResult + ObjTransacao.payment.merchant_usn + ";";
                        lstrResult = lstrResult + ObjTransacao.payment.amount;
                    }

                }
                else
                {
                    PagamentoESITEF ObjPagamento = new PagamentoESITEF();
                    ObjPagamento = (PagamentoESITEF)pEntResult;

                    //Dados Gerais    
                    lstrResult = lstrResult + ObjPagamento.code + ";";
                    lstrResult = lstrResult + ObjPagamento.message + ";";

                    if (ObjPagamento.code == "0")
                    {
                        lstrResult = lstrResult + ObjPagamento.payment.authorizer_code + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.authorizer_message + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.status + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.nit + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.order_id + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.customer_receipt + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.merchant_receipt + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.authorizer_id + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.acquirer_id + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.acquirer_name + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.authorizer_date + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.authorization_number + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.merchant_usn + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.esitef_usn + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.sitef_usn + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.host_usn + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.payment_date + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.amount + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.payment_type + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.issuer + ";";
                        lstrResult = lstrResult + ObjPagamento.payment.authorizer_merchant_id;
                    }
                }

                return lstrResult.Split(';');

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                string[] lstrRetErro = { Erro.ToString(), MsgErro };
                return lstrRetErro;
            }
        }

        private string[] TratarRetornoExtornoBRASPAG(ResultExtornoBRASPAG pEntResult)
        {

            try
            {
                string lstrResult = "";

                //Dados Gerais    
                lstrResult = lstrResult + Erro.ToString() + ";";
                lstrResult = lstrResult + MsgErro + ";";

                lstrResult = lstrResult + pEntResult.Status + ";";
                lstrResult = lstrResult + pEntResult.ReasonCode + ";";
                lstrResult = lstrResult + pEntResult.ReasonMessage + ";";
                lstrResult = lstrResult + pEntResult.ProviderReturnCode + ";";
                lstrResult = lstrResult + pEntResult.ProviderReturnMessage + ";";
                //=============================================================================================


                return lstrResult.Split(';');

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                string[] lstrRetErro = { Erro.ToString(), MsgErro };
                return lstrRetErro;
            }
        }

        public string[] GetStatusContrato(string pstrCodAssinante, string pstrCodContrato)
        {

            try
            {

                string[] lstrRetStatus = null;

                if (pstrCodContrato.Trim().Length == 11)
                {
                    string lstrSerie;
                    string lstrContrato;
                    string lstrDV;

                    lstrSerie = pstrCodContrato.Substring(0, 1).ToUpper();
                    lstrContrato = pstrCodContrato.Substring(1, 9);
                    lstrDV = pstrCodContrato.Substring(10, 1);

                    ContratoAssinanteBusiness ObjBLL = new ContratoAssinanteBusiness();

                    lstrRetStatus = ObjBLL.GetStatusAplicacao(int.Parse(lstrContrato),
                                                                       int.Parse(lstrDV),
                                                                       lstrSerie,
                                                                       int.Parse(pstrCodAssinante));
                }
                else
                {
                    lstrRetStatus = new string[] { "N", "-1", "Contrato fora do formato esperado" };
                }

                return lstrRetStatus;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return new string[] { "N", _erro.ToString(), _msgErro };
            }


        }

        public string GetStatusContratoJSON(string pstrCodAssinante, string pstrCodContrato)
        {

            try
            {
                string[] lstrVetRetStatus = null;

                if (pstrCodContrato.Trim().Length == 11 )
                {
                    string lstrSerie;
                    string lstrContrato;
                    string lstrDV;
                    //string lstrRetStatus;

                    lstrSerie = pstrCodContrato.Substring(0, 1).ToUpper();
                    lstrContrato = pstrCodContrato.Substring(1, 9);
                    lstrDV = pstrCodContrato.Substring(10, 1);

                    ContratoAssinanteBusiness ObjBLL = new ContratoAssinanteBusiness();

                    lstrVetRetStatus = ObjBLL.GetStatusAplicacao(int.Parse(lstrContrato),
                                                                          int.Parse(lstrDV),
                                                                          lstrSerie,
                                                                          int.Parse(pstrCodAssinante));


                }
                else
                {
                    lstrVetRetStatus = new string[] { "N", "-1", "Contrato fora do formato esperado" };
                }

                ResultUsuario ObjEnt = new ResultUsuario()
                {
                    Suscesso = lstrVetRetStatus[0],
                    Pos1 = lstrVetRetStatus[1],
                    Pos2 = lstrVetRetStatus[2],
                };

                return JsonConvert.SerializeObject(ObjEnt);

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                ResultUsuario ObjEntErr = new ResultUsuario()
                {
                    Suscesso = "N",
                    Pos1 = _erro.ToString(),
                    Pos2 = _msgErro,
                };

                return JsonConvert.SerializeObject(ObjEntErr);
            }


        }

        public string[] GetLoginValidoMirante(string pstrToken)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (pstrToken.Trim() == "")
                {
                    _erro = -99;
                    _msgErro = "Token de acesso não informado.";
                    return null;

                }

                string pstrUrlPost = ConfigurationManager.AppSettings["UrlPostLoginIntegrado"];

                if (pstrUrlPost.Trim() == "")
                {
                    _erro = -99;
                    _msgErro = "Url de Integração não configurada";
                    return null;
                }

                string pstrContentType = ConfigurationManager.AppSettings["ContentTypeLogin"];

                if (pstrUrlPost.Trim() == "")
                {
                    _erro = -99;
                    _msgErro = "ContentType de Integração não configurado";
                    return null;
                }

                string pstrMethod = ConfigurationManager.AppSettings["MethodLogin"];

                if (pstrMethod.Trim() == "")
                {
                    _erro = -99;
                    _msgErro = "Method de Integração não configurado";
                    return null;
                }

                string pstrMerchantId = ConfigurationManager.AppSettings["MerchantIdLogin"];

                if (pstrMerchantId.Trim() == "")
                {
                    _erro = -99;
                    _msgErro = "MerchantId de Integração não configurado";
                    return null;
                }

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrUrlPost);
                httpWebRequest.ContentType = pstrContentType;
                httpWebRequest.Method = pstrMethod;
                httpWebRequest.Headers.Add("oem_access_token", pstrMerchantId);


                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "access_token=" + pstrToken;

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    ResultLoginIntegradoMIRANTE ObjResult = JsonConvert.DeserializeObject<ResultLoginIntegradoMIRANTE>(result);

                    if (ObjResult != null)
                    {
                        if (ObjResult.error == false)
                        {

                            if (ObjResult.userinfo.Permissao.central == 0)
                            {
                                return new string[] { ObjResult.userinfo.codigo_contrato.ToString(), ObjResult.userinfo.codigo_assinante.ToString() };
                            }

                        }
                        else
                        {
                            _erro = -99;
                            _msgErro = ObjResult.description;

                            return null;
                        }

                    }

                }

                return null;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }


        }

        public string GetCarteiraClubeMiranteJSON(string pstrCodAssinante, string pstrCodContrato)
        {


            try
            {

                if (pstrCodContrato.Trim().Length == 11)
                {
                    string lstrSerie;
                    string lstrContrato;
                    string lstrDV;
                    //string lstrRetStatus;

                    lstrSerie = pstrCodContrato.Substring(0, 1).ToUpper();
                    lstrContrato = pstrCodContrato.Substring(1, 9);
                    lstrDV = pstrCodContrato.Substring(10, 1);

                    ClubeAssinanteBusiness ObjBLL = new ClubeAssinanteBusiness();
                    ClubeAssinanteEntity ObjEnt = new ClubeAssinanteEntity();

                    ObjEnt = ObjBLL.GetDadosCarteiraClube(int.Parse(pstrCodAssinante),
                                                          lstrSerie,
                                                          int.Parse(lstrContrato),
                                                          int.Parse(lstrDV)
                                                         );

                    if (ObjEnt == null)
                    {
                        ResultCarteiraClubeMIRANTE ObjEntResultErr = new ResultCarteiraClubeMIRANTE()
                        {
                            Sucesso = "N",
                            Descricao = "Dados não encontrato",
                        };

                        return JsonConvert.SerializeObject(ObjEntResultErr);
                    }
                    else
                    {
                        ResultCarteiraClubeMIRANTE ObjEntResult = new ResultCarteiraClubeMIRANTE()
                        {
                            Sucesso = "S",
                            Descricao = "",
                            serie_ctr = ObjEnt.NU_SERIE_CTR,
                            nu_ctr = ObjEnt.NU_CTR,
                            dv_ctr = ObjEnt.NU_DV_CTR,
                            ctr_formatado = ObjEnt.NU_SERIE_CTR + Int64.Parse(ObjEnt.NU_CTR).ToString("000000000") + ObjEnt.NU_DV_CTR,
                            cd_pessoa = ObjEnt.CD_CONTABIL_PESSOA,
                            nm_pessoa = ObjEnt.NM_PESSOA,
                            tp_pessoa = ObjEnt.ST_TP_PESSOA,
                            nu_doc = ObjEnt.NU_DOC,
                            ds_email = ObjEnt.DS_EMAIL, 
                            nm_produto = ObjEnt.NM_PRODUTO,
                            nm_dependente = ObjEnt.NM_DEPENDENTE,
                            ano_termino = ObjEnt.MES_TERMINO + "/" + ObjEnt.ANO_TERMINO,
                        };

                        return JsonConvert.SerializeObject(ObjEntResult);

                    }

                }
                else
                {
                    ResultCarteiraClubeMIRANTE ObjEntResultErr = new ResultCarteiraClubeMIRANTE()
                    {
                        Sucesso = "N",
                        Descricao = "Contrato fora do formato esperado",
                    };

                    return JsonConvert.SerializeObject(ObjEntResultErr);
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                ResultCarteiraClubeMIRANTE ObjEntErr = new ResultCarteiraClubeMIRANTE()
                {
                    Sucesso = "N",
                    Descricao =_msgErro,
                };
                
                return JsonConvert.SerializeObject(ObjEntErr);
            }


        }

        public ClubeAssinanteEntity GetCarteiraClubeMirante(string pstrCodAssinante, string pstrCodContrato)
        {


            try
            {

                if (pstrCodContrato.Trim().Length == 11)
                {
                    string lstrSerie;
                    string lstrContrato;
                    string lstrDV;
                    //string lstrRetStatus;

                    lstrSerie = pstrCodContrato.Substring(0, 1).ToUpper();
                    lstrContrato = pstrCodContrato.Substring(1, 9);
                    lstrDV = pstrCodContrato.Substring(10, 1);

                    ClubeAssinanteBusiness ObjBLL = new ClubeAssinanteBusiness();
                    ClubeAssinanteEntity ObjEnt = new ClubeAssinanteEntity();

                    ObjEnt = ObjBLL.GetDadosCarteiraClube(int.Parse(pstrCodAssinante),
                                                          lstrSerie,
                                                          int.Parse(lstrContrato),
                                                          int.Parse(lstrDV)
                                                         );
                    return ObjEnt;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }


        }

        public string ConsomeWebServiceDinamico(string pstrSoapSTR, 
                                                string pstrFormatParametro, 
                                                string pstrUrlWSclient, 
                                                string pstrMetodoNomeWSClient, 
                                                string pstrUriSoap, 
                                                string pstrContentType, 
                                                string pstrAccept, 
                                                string pstrMethod, 
                                                Dictionary<string, string> pdicParametros)
        {
            try
            {
                CriarChamadaDinamicaWS ObjWSDinamico = new CriarChamadaDinamicaWS(pstrUrlWSclient, 
                                                                                  pstrMetodoNomeWSClient, 
                                                                                  pstrSoapSTR, 
                                                                                  pstrUriSoap, 
                                                                                  pstrContentType, 
                                                                                  pstrAccept, 
                                                                                  pstrMethod, 
                                                                                  pstrFormatParametro);
                ObjWSDinamico.ldicParametro = pdicParametros;
                ObjWSDinamico.Chama();
                return ObjWSDinamico.lstrResultString;
            }
            catch(Exception ex)
            {
                CWA.Util.Utility.SetLogXML("ServiceCWA.ConsomeWebServiceDinamico", "Erro", ex.Message, false);
                return "";
            }
        }

        public string ConsomeApiQualityGoiania(string ptrsURL)
        {
            try
            {
                string lstrResult = string.Empty;

                HttpWebRequest ObjHttpReq = (HttpWebRequest)WebRequest.Create(ptrsURL);
                var httpResponse = (HttpWebResponse)ObjHttpReq.GetResponse();
                using (StreamReader responseReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    lstrResult = responseReader.ReadToEnd();
                }
                return lstrResult;
            }
            catch(Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;
 
                CWA.Util.Utility.SetLogXML("ServiceCWA.ConsomeApiQualityGoiania", "Erro", ex.Message, false);
                return "";
            }
        }

        public string GetVersaoWS()
        {
            try
            {
                //Retorna versao
                return "WCF-" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;
                return null; 
            }
            
        }

        public string SetGeraNotaVA_GO(string pstrTipoEndPointClient,
                                       string pstrEndPointClient,
                                       string pstrBanco,
                                       string pstrServidor,
                                       string pstrUsuario,
                                       string pstrDataInicio,
                                       string pstrDataFim,
                                       string pstrPathLog)
        {
            bool lbolLogOK = false;

            if (pstrPathLog.Trim() != "")
            {
                lbolLogOK = true;
            }

            try
            {

                InterfacesWSGOVA_BLL ObjBLL = new InterfacesWSGOVA_BLL();

                List<WSGO_VA.mensagemVO> Retorno = new List<WSGO_VA.mensagemVO>();
                List<WSGO_VA.cabecalhoNotaVO> CaNotaVOList = new List<WSGO_VA.cabecalhoNotaVO>();

                VarBanco.Banco = pstrBanco;
                VarBanco.Servidor = pstrServidor;
                VarBanco.Usuario = pstrUsuario;

                ObjBLL.Dt_inicio = pstrDataInicio;
                ObjBLL.Dt_fim = pstrDataFim;

                if (lbolLogOK == true)
                {
                    Utility.SetLogTXT(pstrPathLog, "----------------------------------------------------");
                    Utility.SetLogTXT(pstrPathLog, "Serviço iniciado em: " + DateTime.Now);
                    Utility.SetLogTXT(pstrPathLog, "Versão: " + GetVersaoWS());
                    Utility.SetLogTXT(pstrPathLog, "----------------------------------------------------");
                }

                int lintcontreg = 0;

                bool lbolOK = true;

                while (lbolOK == true)
                {
                    InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                    ObjData.Conectar();
                    ObjData.Conectar2();

                    CaNotaVOList.AddRange(GerarNotaFiscalGO_VA(pstrDataInicio, pstrDataFim, pstrPathLog, ObjData));

                    lintcontreg++;

                    if (Erro != 0)
                    {
                        lbolOK = false;
                    }

                    ObjData.Close_Conection();
                    ObjData.Close_Conection2();
                    

                }

                if (CaNotaVOList.Count != 0)
                {
                    if (lbolLogOK == true)
                    {
                        Utility.SetLogTXT(pstrPathLog, "----------------------------------------------------");
                        Utility.SetLogTXT(pstrPathLog, "INICIANDO COMUNICAÇÃO COM O WEBSERVICE");
                    }

                    List<WSGO_VA.mensagemVO> ObjRetorno = new List<WSGO_VA.mensagemVO>();
                    WSGO_VA.cabecalhoNotaVO[] CaNotaVO;

                    CaNotaVO = CaNotaVOList.ToArray();

                    WSGO_VA.gerarNota cabecalho = new WSGO_VA.gerarNota(CaNotaVO);

                    WSGO_VA.NotaFiscalServiceBeanClient ObjService = ServiceClientGO.GetServiceClintEndPointGO_VA(pstrTipoEndPointClient, pstrEndPointClient);

                    Retorno = ObjService.gerarNota(cabecalho.cabecalhoNotaVOList).ToList<WSGO_VA.mensagemVO>();


                    foreach (WSGO_VA.mensagemVO ObjRetornoTemp in Retorno)
                    {
                        InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                        ObjData.Conectar();
                        ObjData.Conectar2();

                        if (ObjRetornoTemp.nota != null)
                        {
                            RetornarNotaGO_VA(ObjRetornoTemp, ObjRetornoTemp.nota.idNota, pstrPathLog);
                        }
                        else
                        {
                            RetornarNotaGO_VA(ObjRetornoTemp, 0, pstrPathLog);
                        }

                        ObjData.Close_Conection();
                        ObjData.Close_Conection2();
                    }

                    if (lbolLogOK == true)
                    {
                        Utility.SetLogTXT(pstrPathLog, "----------------------------------------------------");
                        Utility.SetLogTXT(pstrPathLog, "----------------------------------------------------");
                        Utility.SetLogTXT(pstrPathLog, "Serviço finalizado em: " + DateTime.Now);
                        Utility.SetLogTXT(pstrPathLog, "----------------------------------------------------");
                    }


                    string lstrRet = "";

                    if (lbolLogOK == true)
                    {

                        string lstrLine = "";

                        lstrRet = "";
                        lstrRet = lstrRet + "02RETORNOS COM OK: " + _contOKGO_VA.ToString() + (char)13 + (char)10;
                        lstrRet = lstrRet + "RETORNOS COM ERRO: " + _contErroGO_VA.ToString() + (char)13 + (char)10 + (char)13 + (char)10 + (char)13 + (char)10;
                        lstrRet = lstrRet + "O ARQUIVO LOG SE ENCONTRA EM: " + pstrPathLog + (char)13 + (char)10;
                        lstrRet = lstrRet + "----------------------------------------------------" + (char)13 + (char)10 + (char)13 + (char)10;

                        StreamReader ObjFile = new StreamReader(pstrPathLog);
                        while ((lstrLine = ObjFile.ReadLine()) != null)
                        {
                            lstrRet = lstrRet + lstrLine + (char)13 + (char)10;
                        }

                        ObjFile.Close();

                    }

                    return "00," + lstrRet.Replace(",", ".");

                }
                else
                {
                    if (Erro == 1)
                    {
                        return "01,Não existem dados para integrar";
                    }
                    else
                    {
                        return "-1," + MsgErro;
                    }
                }

            }
            catch (Exception ex)
            {
                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                ObjData.Conectar();
                ObjData.Conectar2();

                InterfacesWSGOVA_BLL ObjBLLErro = new InterfacesWSGOVA_BLL();

                ObjBLLErro.Cadastra_Erro_Nota_sem_resposta(0, 0, "", "cwa");

                _erro = -1;
                _msgErro = ex.Message;

                if (lbolLogOK == true)
                {
                    Utility.SetLogTXT(pstrPathLog, "ERRO INTERNO DLL - SetGeraNotaVA_GO " + _msgErro);
                }

                ObjData.Close_Conection();
                ObjData.Close_Conection2();

                return _erro.ToString() + "," + _msgErro;

            }

        }


        private List<WSGO_VA.cabecalhoNotaVO> GerarNotaFiscalGO_VA(string pstrDataInicio, 
                                                                   string pstrDataFim, 
                                                                   string pstrPathLog, 
                                                                   InterfacesWSGOVA_Data ObjData)
        {

            bool lbolLogOK = false;

            if (pstrPathLog.Trim() != "")
            {
                lbolLogOK = true;
            }


            try
            {

                List<WSGO_VA.cabecalhoNotaVO> CaNotaVOList = new List<WSGO_VA.cabecalhoNotaVO>();

                _erro = 0;
                _msgErro = "";


                int cont = 0;
                int Nuseq = 0;
                int NuCDRegistro = 0;

                long DOCUMENTO_EDITORA = 0;
                long DOCUMENTO_REPRESENTANTE = 0;
                long DOCUMENTO_TRANSPORTADORA = 0;
                string CD_ESTABELECIMENTO_FI = string.Empty;
                string USUARIO = string.Empty;
                string TIPO_NOTA_FISCAL = string.Empty;
                string TIPO_VENDA = string.Empty;
                int ID_NOTA = 0;
                string CD_MATERIAL_ASSOCIADO = string.Empty;
                int CD_ITEM_NOTA_REMESSA = 0;
                decimal VA_UNITARIO_LIQUIDO = 0;
                decimal VA_PRECO_UNITARIO = 0;
                int NU_RECIBO_VA = 0;
                int NU_PARCELA = 0;
                DateTime DT_VENCIMENTO;
                decimal VA_RECIBO = 0;
                decimal QTD_ITEM_PRODUTO = 0;
                DateTime DT_REMESSA_DEVOLUCAO;
                decimal Va_Saldo = 0;
                decimal Qtd_Saldo = 0;
                int CD_PRODUTO = 0;
                int CD_DESTINO_TIRAGEM = 0;

                int ST_TP_NOTA_FISCAL = 0;

                int Flag = 0;
                int FlagErro = 0;

                string NotaFiscalOrigem = string.Empty;

                string lsSQL = "";
                string vbCr = " \r ";

                WSGO_VA.cabecalhoNotaVO CaNota = new WSGO_VA.cabecalhoNotaVO();
                List<WSGO_VA.duplicataNotaVO> DupliVOList = new List<WSGO_VA.duplicataNotaVO>();
                List<WSGO_VA.itemNotaVO> CaItemNotaList = new List<WSGO_VA.itemNotaVO>();

                //InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                if (lbolLogOK == true)
                {
                    Utility.SetLogTXT(pstrPathLog, "--------------------------------------------------------");
                    Utility.SetLogTXT(pstrPathLog, "SELECIONANDO REGISTROS PARA INTEGRAÇÃO");
                }

                //ObjData.Conectar();
                //ObjData.Conectar2();


                //==========================================================================================
                //TRATA RETORNOS PENDENTES STATUS 2 (VALIDA SE JÁ RETORNOU A NOTA OU NÃO COLOCANDO (1 OU 3)
                //==========================================================================================
                InterfacesWSGOVA_BLL ObjBLL = new InterfacesWSGOVA_BLL();
                ObjBLL.SetTratarRegistroSemRetorno();
                //==========================================================================================


                lsSQL = "SELECT" + vbCr;
                lsSQL = lsSQL + "TOP 1" + vbCr;
                lsSQL = lsSQL + "CD_REGISTRO" + vbCr;
                lsSQL = lsSQL + "FROM" + vbCr;
                lsSQL = lsSQL + "IS_MOV_NF_REMESSA_DEVOLUCAO" + vbCr;
                lsSQL = lsSQL + "WHERE " + vbCr;
                lsSQL = lsSQL + "   ST_SITUACAO in (0) AND " + vbCr;

                //lsSQL = lsSQL + "  CD_REGISTRO = 953657 and " + vbCr;

                lsSQL = lsSQL + "   CD_ORIGEM = 2" + vbCr; //SOMENTE VENDA AVULSA

                if (!string.IsNullOrEmpty(pstrDataInicio) && !string.IsNullOrEmpty(pstrDataFim))
                {
                    lsSQL = lsSQL + " AND DT_REMESSA_DEVOLUCAO BETWEEN '" + pstrDataInicio + " 00:00:00" + "' AND '" + pstrDataFim + " 23:59:00" + "'" + vbCr;
                }
                lsSQL = lsSQL + "ORDER BY" + vbCr;
                lsSQL = lsSQL + "CD_REGISTRO";
                VarBanco.lsSQL = lsSQL;

                ObjData.RetornaSQL();


                if (VarBanco.Reader.Read())
                {
                    NuCDRegistro = Convert.ToInt32(VarBanco.Reader["CD_REGISTRO"].ToString());

                    Utility.SetLogTXT(pstrPathLog, "SELECIONADO O REGISTRO: " + NuCDRegistro.ToString());

                    lsSQL = "UPDATE" + vbCr;
                    lsSQL = lsSQL + "IS_MOV_NF_REMESSA_DEVOLUCAO" + vbCr;
                    lsSQL = lsSQL + "SET " + vbCr;
                    lsSQL = lsSQL + "ST_SITUACAO = 2" + vbCr;
                    lsSQL = lsSQL + "WHERE " + vbCr;
                    lsSQL = lsSQL + "    CD_REGISTRO = " + VarBanco.Reader["CD_REGISTRO"].ToString();
                    lsSQL = lsSQL + "AND CD_ORIGEM = 2" + vbCr; //SOMENTE VENDA AVULSA
                    VarBanco.lsSQL = lsSQL;

                    ObjData.ExecutaSQL();

                    Flag = 0;

                    if (lbolLogOK == true)
                    {
                        Utility.SetLogTXT(pstrPathLog, "ALTERADO A SITUAÇÃO DE AGUARDANDO PROCESSAMENTO PARA EM PROCESSAMENTO");
                    }

                }
                else
                {
                    Flag = 1;

                    if (lbolLogOK == true)
                    {
                        Utility.SetLogTXT(pstrPathLog, "SEM REGISTROS PARA INTEGRAR");
                    }

                }

                VarBanco.Reader.Close();

                if (Flag == 0)
                {
                    VarBanco.ErroSQL = string.Empty;

                    lsSQL = "SELECT " + vbCr;
                    lsSQL = lsSQL + "CASE WHEN CP1.NU_CNPJ IS NULL THEN CASE WHEN CP1.NU_CPF IS NULL THEN 0 ELSE CP1.NU_CPF END ELSE CP1.NU_CNPJ END  AS DOCUMENTO_EDITORA," + vbCr;
                    lsSQL = lsSQL + "CASE WHEN CP.NU_CNPJ IS NULL THEN  CASE WHEN CP.NU_CPF IS NULL THEN 0 ELSE CP.NU_CPF END ELSE CP.NU_CNPJ END  AS DOCUMENTO_REPRESENTANTE," + vbCr;
                    lsSQL = lsSQL + "E.CD_CONTABIL_EDITORA_FI AS CD_ESTABELECIMENTO_FI," + vbCr;
                    lsSQL = lsSQL + "'1' AS USUARIO," + vbCr;
                    lsSQL = lsSQL + "'3' AS TIPO_NOTA_FISCAL," + vbCr;
                    lsSQL = lsSQL + "'3' AS TIPO_VENDA," + vbCr;
                    lsSQL = lsSQL + "IMNRD.CD_REGISTRO AS ID_NOTA," + vbCr;
                    lsSQL = lsSQL + "IMNRD.CD_MATERIAL_ASSOCIADO," + vbCr;
                    lsSQL = lsSQL + "IMNRI.CD_ITEM_NOTA_REMESSA," + vbCr;
                    lsSQL = lsSQL + "IMNRI.VA_UNITARIO_ITEM AS VA_UNITARIO_LIQUIDO," + vbCr;
                    lsSQL = lsSQL + "IMNRI.VA_PRECO_UNITARIO," + vbCr;
                    lsSQL = lsSQL + "IMNRD.CD_REGISTRO AS NU_RECIBO_VA," + vbCr;
                    lsSQL = lsSQL + "1 AS NU_PARCELA," + vbCr;
                    lsSQL = lsSQL + "IMNRD.DT_REMESSA_DEVOLUCAO AS DT_VENCIMENTO," + vbCr;
                    lsSQL = lsSQL + "IMNRD.VA_TOTAL AS VA_RECIBO," + vbCr;
                    lsSQL = lsSQL + "IMNRI.QTD_PRODUTOS_ITEM, IMNRD.CD_CONTABIL_PESSOA, IMNRD.ST_TP_NOTA_FISCAL, IMNRD.DT_REMESSA_DEVOLUCAO, P.CD_PRODUTO, IMNRD.CD_CONTABIL_TRANSP" + vbCr;
                    lsSQL = lsSQL + "FROM " + vbCr;
                    lsSQL = lsSQL + "IS_MOV_NF_REMESSA_DEVOLUCAO IMNRD" + vbCr;
                    lsSQL = lsSQL + "JOIN IS_MOV_NF_REMESSA_ITENS IMNRI" + vbCr;
                    lsSQL = lsSQL + "ON IMNRI.CD_REGISTRO = IMNRD.CD_REGISTRO" + vbCr;
                    lsSQL = lsSQL + "JOIN CADASTRO_PESSOA CP" + vbCr;
                    lsSQL = lsSQL + "ON CP.CD_CONTABIL_PESSOA = IMNRD.CD_CONTABIL_PESSOA" + vbCr;
                    lsSQL = lsSQL + "JOIN PRODUTO P" + vbCr;
                    lsSQL = lsSQL + "ON P.CD_PRODUTO = IMNRD.CD_PRODUTO" + vbCr;
                    lsSQL = lsSQL + "JOIN EDITORA E" + vbCr;
                    lsSQL = lsSQL + "ON P.CD_CONTABIL_EDITORA = E.CD_CONTABIL_EDITORA" + vbCr;
                    lsSQL = lsSQL + "JOIN CADASTRO_PESSOA CP1" + vbCr;
                    lsSQL = lsSQL + "ON CP1.CD_CONTABIL_PESSOA = E.CD_CONTABIL_PESSOA" + vbCr;
                    lsSQL = lsSQL + "WHERE " + vbCr;
                    lsSQL = lsSQL + "IMNRD.CD_REGISTRO = " + NuCDRegistro + vbCr;
                    lsSQL = lsSQL + "GROUP BY" + vbCr;
                    lsSQL = lsSQL + "CP1.NU_CNPJ, CP1.NU_CPF," + vbCr;
                    lsSQL = lsSQL + "CP.NU_CNPJ, CP.NU_CPF," + vbCr;
                    lsSQL = lsSQL + "E.CD_CONTABIL_EDITORA_FI," + vbCr;
                    lsSQL = lsSQL + "IMNRD.CD_REGISTRO," + vbCr;
                    lsSQL = lsSQL + "IMNRD.CD_MATERIAL_ASSOCIADO," + vbCr;
                    lsSQL = lsSQL + "IMNRI.CD_ITEM_NOTA_REMESSA," + vbCr;
                    lsSQL = lsSQL + "IMNRI.VA_UNITARIO_ITEM," + vbCr;
                    lsSQL = lsSQL + "IMNRI.VA_PRECO_UNITARIO," + vbCr;
                    lsSQL = lsSQL + "IMNRD.DT_REMESSA_DEVOLUCAO," + vbCr;
                    lsSQL = lsSQL + "IMNRD.VA_TOTAL, " + vbCr;
                    lsSQL = lsSQL + "IMNRI.QTD_PRODUTOS_ITEM, IMNRD.CD_CONTABIL_PESSOA, IMNRD.ST_TP_NOTA_FISCAL, P.CD_PRODUTO,IMNRD.CD_CONTABIL_TRANSP" + vbCr;
                    VarBanco.lsSQL = lsSQL;

                    ObjData.RetornaSQL();

                    if (VarBanco.Reader.HasRows)
                    {
                        cont = 1;
                    }
                    else
                    {

                        if (lbolLogOK == true)
                        {
                            Utility.SetLogTXT(pstrPathLog, "ALTERADO A SITUAÇÃO DE EM PROCESSAMENTO PARA COM ERRO");
                        }

                        if (!string.IsNullOrEmpty(VarBanco.ErroSQL))
                        {
                            if (lbolLogOK == true)
                            {
                                Utility.SetLogTXT(pstrPathLog, "DESCRIÇÃO DO ERRO: " + VarBanco.ErroSQL);
                            }
                        }

                        lsSQL = "UPDATE" + vbCr;
                        lsSQL = lsSQL + "IS_MOV_NF_REMESSA_DEVOLUCAO" + vbCr;
                        lsSQL = lsSQL + "SET " + vbCr;
                        lsSQL = lsSQL + "ST_SITUACAO = 3" + vbCr;
                        lsSQL = lsSQL + "WHERE " + vbCr;
                        lsSQL = lsSQL + "    ST_SITUACAO = 2";
                        lsSQL = lsSQL + "AND CD_ORIGEM = 2" + vbCr; //SOMENTE VENDA AVULSA

                        VarBanco.lsSQL = lsSQL;

                        ObjData.ExecutaSQL();

                        lsSQL = "SELECT CASE WHEN MAX(NU_SEQ) IS NULL THEN 0 ELSE MAX(NU_SEQ) END + 1 AS NUSEQ  FROM IS_MOV_REMESSA_DEVOLUCAO_ERROS" + vbCr;
                        VarBanco.lsSQL = lsSQL;

                        ObjData.RetornaSQL();

                        if (VarBanco.Reader.Read())
                        {
                            Nuseq = Convert.ToInt32(VarBanco.Reader["NUSEQ"].ToString());
                        }
                        VarBanco.Reader.Close();

                        lsSQL = "SELECT 'CWA' FROM IS_ERROS_FATURA WHERE CD_ERRO = '999'";
                        VarBanco.lsSQL = lsSQL;

                        ObjData.RetornaSQL();

                        if (!VarBanco.Reader.HasRows)
                        {
                            lsSQL = "INSERT INTO IS_ERROS_FATURA" + vbCr;
                            lsSQL = lsSQL + "(CD_ERRO," + vbCr;
                            lsSQL = lsSQL + "DS_ERRO)" + vbCr;
                            lsSQL = lsSQL + "VALUES" + vbCr;
                            lsSQL = lsSQL + "( '999', 'Erro de parametrização'" + vbCr;
                            lsSQL = lsSQL + ")" + vbCr;
                            VarBanco.lsSQL = lsSQL;

                            ObjData.ExecutaSQL();
                        }

                        VarBanco.Reader.Close();

                        lsSQL = "INSERT INTO" + vbCr;
                        lsSQL = lsSQL + "IS_MOV_REMESSA_DEVOLUCAO_ERROS" + vbCr;
                        lsSQL = lsSQL + "(CD_REGISTRO," + vbCr;
                        lsSQL = lsSQL + "NU_SEQ," + vbCr;
                        lsSQL = lsSQL + "CD_ERRO," + vbCr;
                        lsSQL = lsSQL + "DS_USR,TP_STATUS_ERRO) VALUES " + vbCr;
                        lsSQL = lsSQL + "(" + NuCDRegistro + "," + vbCr;
                        lsSQL = lsSQL + Nuseq + "," + vbCr;
                        lsSQL = lsSQL + "999," + vbCr;
                        lsSQL = lsSQL + "'CWA', 1" + vbCr;
                        lsSQL = lsSQL + ")" + vbCr;
                        VarBanco.lsSQL = lsSQL;

                        ObjData.ExecutaSQL();

                        FlagErro = 1;
                    }

                    if (FlagErro == 0)
                    {
                        if (lbolLogOK == true)
                        {
                            Utility.SetLogTXT(pstrPathLog, "REALIZANDO LEITURA DAS INFORMAÇÕES DE REMESSA / DEVOLUÇÃO");
                        }

                        //InterfacesWSGOVA_BLL ObjBLL = new InterfacesWSGOVA_BLL();

                        while (VarBanco.Reader.Read())
                        {

                            if (ObjBLL.Ponto_Venda_Fiscal(Convert.ToInt32(VarBanco.Reader["CD_CONTABIL_PESSOA"].ToString())) == 0)
                            {
                                DOCUMENTO_EDITORA = Convert.ToInt64(VarBanco.Reader["DOCUMENTO_EDITORA"].ToString());
                            }
                            else
                            {
                                DOCUMENTO_EDITORA = Convert.ToInt64(VarBanco.Reader["DOCUMENTO_REPRESENTANTE"].ToString());
                            }

                            CD_ESTABELECIMENTO_FI = VarBanco.Reader["CD_ESTABELECIMENTO_FI"].ToString();
                            USUARIO = VarBanco.Reader["USUARIO"].ToString();
                            TIPO_NOTA_FISCAL = VarBanco.Reader["TIPO_NOTA_FISCAL"].ToString();
                            TIPO_VENDA = VarBanco.Reader["TIPO_VENDA"].ToString();
                            ID_NOTA = Convert.ToInt32(VarBanco.Reader["ID_NOTA"].ToString());
                            CD_MATERIAL_ASSOCIADO = VarBanco.Reader["CD_MATERIAL_ASSOCIADO"].ToString();
                            CD_ITEM_NOTA_REMESSA = Convert.ToInt32(VarBanco.Reader["CD_ITEM_NOTA_REMESSA"].ToString());
                            VA_UNITARIO_LIQUIDO = Convert.ToDecimal(VarBanco.Reader["VA_UNITARIO_LIQUIDO"].ToString());
                            VA_PRECO_UNITARIO = Convert.ToDecimal(VarBanco.Reader["VA_PRECO_UNITARIO"].ToString());
                            NU_PARCELA = Convert.ToInt32(VarBanco.Reader["NU_PARCELA"].ToString());
                            DT_VENCIMENTO = Convert.ToDateTime(VarBanco.Reader["DT_VENCIMENTO"].ToString());
                            VA_RECIBO = Convert.ToDecimal(VarBanco.Reader["VA_RECIBO"].ToString());
                            QTD_ITEM_PRODUTO = Convert.ToDecimal(VarBanco.Reader["QTD_PRODUTOS_ITEM"].ToString());
                            ST_TP_NOTA_FISCAL = Convert.ToInt32(VarBanco.Reader["ST_TP_NOTA_FISCAL"].ToString());

                            DT_REMESSA_DEVOLUCAO = Convert.ToDateTime(VarBanco.Reader["DT_REMESSA_DEVOLUCAO"].ToString());

                            CD_PRODUTO = Convert.ToInt32(VarBanco.Reader["CD_PRODUTO"].ToString());


                            if (ST_TP_NOTA_FISCAL == 5)
                            {
                                NotaFiscalOrigem = ObjBLL.PesquisaNotaFiscalRemessa(DT_VENCIMENTO.Day.ToString("00") + "/" +
                                                                                    DT_VENCIMENTO.Month.ToString("00") + "/" +
                                                                                    DT_VENCIMENTO.Year.ToString("0000"),
                                                                                    VarBanco.Reader["CD_CONTABIL_PESSOA"].ToString(),
                                                                                    CD_MATERIAL_ASSOCIADO, NuCDRegistro.ToString());

                                if (NotaFiscalOrigem == "")
                                {
                                    NotaFiscalOrigem = ObjBLL.PesquisaNotaFiscalRemessaInterior(NuCDRegistro.ToString());
                                }

                                TIPO_NOTA_FISCAL = "5";

                                VarBanco.lsSQL = ObjBLL.PesquisaInformacoesRemessaParaNotaEncalhe(DT_VENCIMENTO.Day.ToString("00") + "/" +
                                                                                                  DT_VENCIMENTO.Month.ToString("00") + "/" +
                                                                                                  DT_VENCIMENTO.Year.ToString("0000"),
                                                                                                  VarBanco.Reader["CD_CONTABIL_PESSOA"].ToString(),
                                                                                                  CD_PRODUTO);
                                ObjData.RetornaSQL2();

                                if (VarBanco.Reader2.Read())
                                {
                                    if (!string.IsNullOrEmpty(VarBanco.Reader2["VA_RECIBO"].ToString()))
                                    {
                                        Va_Saldo = Convert.ToDecimal(VarBanco.Reader2["VA_RECIBO"].ToString());
                                        Qtd_Saldo = Convert.ToDecimal(VarBanco.Reader2["QT_HISTORICO"].ToString());
                                    }
                                    else
                                    {
                                        Va_Saldo = 0;
                                        Qtd_Saldo = 0;
                                    }
                                }
                                else
                                {
                                    Va_Saldo = 0;
                                    Qtd_Saldo = 0;
                                }

                            }


                            DOCUMENTO_REPRESENTANTE = Convert.ToInt64(ObjBLL.PesquisaAgente_Va(Convert.ToInt32(VarBanco.Reader["CD_CONTABIL_PESSOA"].ToString())));

                            if (DOCUMENTO_REPRESENTANTE == 0)
                            {
                                DOCUMENTO_REPRESENTANTE = Convert.ToInt64(VarBanco.Reader["DOCUMENTO_REPRESENTANTE"].ToString());
                            }

                            if (ObjBLL.Ponto_Venda_Fiscal(Convert.ToInt32(VarBanco.Reader["CD_CONTABIL_PESSOA"].ToString())) == 0)
                            {
                                DOCUMENTO_TRANSPORTADORA = Convert.ToInt64(ObjBLL.PesquisaTransportadora(VarBanco.Reader["CD_CONTABIL_PESSOA"].ToString(),
                                                                                                        DT_VENCIMENTO.Day.ToString("00") + "/" +
                                                                                                        DT_VENCIMENTO.Month.ToString("00") + "/" +
                                                                                                        DT_VENCIMENTO.Year.ToString("0000")));
                            }
                            else
                            {
                                DOCUMENTO_TRANSPORTADORA = DOCUMENTO_REPRESENTANTE;
                            }

                            if (!string.IsNullOrEmpty(VarBanco.Reader["CD_CONTABIL_TRANSP"].ToString()))
                            {
                                DOCUMENTO_TRANSPORTADORA = Convert.ToInt64(ObjBLL.PesquisaDocumento(VarBanco.Reader["CD_CONTABIL_TRANSP"].ToString()));
                            }

                            CD_DESTINO_TIRAGEM = Convert.ToInt32(ObjBLL.PesquisaDestinoTiragem(NuCDRegistro.ToString()));


                            if (cont == 1)
                            {
                                if (lbolLogOK == true)
                                {
                                    Utility.SetLogTXT(pstrPathLog, "DOCUMENTO_EDITORA: " + DOCUMENTO_EDITORA.ToString());
                                    Utility.SetLogTXT(pstrPathLog, "DOCUMENTO_TRANSPORTADORA: " + DOCUMENTO_TRANSPORTADORA.ToString());
                                    Utility.SetLogTXT(pstrPathLog, "CD_ESTABELECIMENTO_FI: " + CD_ESTABELECIMENTO_FI);
                                    Utility.SetLogTXT(pstrPathLog, "USUARIO: " + USUARIO);
                                    Utility.SetLogTXT(pstrPathLog, "TIPO_NOTA_FISCAL: " + TIPO_NOTA_FISCAL);
                                    Utility.SetLogTXT(pstrPathLog, "TIPO_VENDA: " + TIPO_VENDA);
                                    Utility.SetLogTXT(pstrPathLog, "ID_NOTA: " + ID_NOTA.ToString());
                                    Utility.SetLogTXT(pstrPathLog, "CD_MATERIAL_ASSOCIADO: " + CD_MATERIAL_ASSOCIADO);
                                    Utility.SetLogTXT(pstrPathLog, "NU_PARCELA: " + NU_PARCELA.ToString());
                                    Utility.SetLogTXT(pstrPathLog, "DT_VENCIMENTO: " + DT_VENCIMENTO.ToString());
                                    Utility.SetLogTXT(pstrPathLog, "VA_RECIBO: " + VA_RECIBO.ToString());
                                    Utility.SetLogTXT(pstrPathLog, "NOTA_FISCAL_ORIGEM: " + NotaFiscalOrigem);
                                    Utility.SetLogTXT(pstrPathLog, "DOCUMENTO_REPRESENTANTE: " + DOCUMENTO_REPRESENTANTE.ToString());
                                    Utility.SetLogTXT(pstrPathLog, "DT_EDICAO: " + DT_REMESSA_DEVOLUCAO.ToString());
                                    Utility.SetLogTXT(pstrPathLog, "VA_SALDO: " + Va_Saldo.ToString());
                                    Utility.SetLogTXT(pstrPathLog, "QTD_SALDO: " + Qtd_Saldo.ToString());
                                    Utility.SetLogTXT(pstrPathLog, "CD_DESTINO_TIRAGEM: " + CD_DESTINO_TIRAGEM.ToString());
                                }

                                CaNota.cdCNPJCliente = (DOCUMENTO_EDITORA.ToString().Length <= 11 ? DOCUMENTO_EDITORA.ToString("00000000000") : DOCUMENTO_EDITORA.ToString("00000000000000"));
                                CaNota.cdCNPJTransportador = (DOCUMENTO_TRANSPORTADORA.ToString().Length <= 11 ? DOCUMENTO_TRANSPORTADORA.ToString("00000000000") : DOCUMENTO_TRANSPORTADORA.ToString("00000000000000"));
                                CaNota.cdCodigoEstabelecimento = CD_ESTABELECIMENTO_FI;
                                CaNota.cdCodigoUsuario = USUARIO;
                                CaNota.clTipoNotaFiscal = Convert.ToInt32(TIPO_NOTA_FISCAL);
                                CaNota.clTipoNotaFiscalSpecified = true;
                                CaNota.clTipoVenda = Convert.ToInt32(TIPO_VENDA);
                                CaNota.clTipoVendaSpecified = true;
                                CaNota.dsSerie = "5";
                                CaNota.idNota = ID_NOTA;
                                CaNota.idNotaSpecified = true;
                                if (ST_TP_NOTA_FISCAL == 5)
                                {
                                    CaNota.nrNotaOrig = NotaFiscalOrigem;
                                }
                                else
                                {
                                    CaNota.nrNotaOrig = "";
                                }

                                CaNota.dtEdicao = DT_REMESSA_DEVOLUCAO;
                                CaNota.dtEdicaoSpecified = true;

                                CaNota.clDestinoTiragem = CD_DESTINO_TIRAGEM;
                                CaNota.clDestinoTiragemSpecified = true;

                                WSGO_VA.representanteVO RepreVO = new WSGO_VA.representanteVO();
                                RepreVO.cdClassificador = 0;
                                RepreVO.cdClassificadorSpecified = true;
                                RepreVO.cdCodigoRepresentante = (DOCUMENTO_REPRESENTANTE.ToString().Length <= 11 ? DOCUMENTO_REPRESENTANTE.ToString("00000000000") : DOCUMENTO_REPRESENTANTE.ToString("00000000000000"));

                                RepreVO.dsSequencia = 1;
                                RepreVO.dsSequenciaSpecified = true;
                                RepreVO.idNota = ID_NOTA;
                                RepreVO.idNotaSpecified = true;
                                RepreVO.pcComissao = 0;
                                RepreVO.pcComissaoSpecified = true;
                                RepreVO.vlComissao = 0;
                                RepreVO.vlComissaoSpecified = true;

                                CaNota.representante = RepreVO;

                                WSGO_VA.duplicataNotaVO DupliVO = new WSGO_VA.duplicataNotaVO();

                                DupliVO.dsParcela = NU_PARCELA.ToString("00");
                                DupliVO.dtVencimento = DT_VENCIMENTO;
                                DupliVO.dtVencimentoSpecified = true;
                                DupliVO.vlParcelar = VA_RECIBO;
                                DupliVO.vlParcelarSpecified = true;
                                DupliVO.cdCodigoEspecie = "DP";
                                DupliVO.cdCodigoVencimento = 7;
                                DupliVO.cdCodigoVencimentoSpecified = true;
                                DupliVO.codSeqDocto = 1;
                                DupliVO.codSeqDoctoSpecified = true;
                                DupliVO.idNota = ID_NOTA;
                                DupliVO.idNotaSpecified = true;

                                DupliVO.vlAcumulado = 0;
                                DupliVO.vlAcumuladoSpecified = true;

                                DupliVO.vlComissao = 0;
                                DupliVO.vlComissaoSpecified = true;

                                DupliVO.vlDesconto = 0;
                                DupliVO.vlDescontoSpecified = true;

                                DupliVO.dtDesconto = DateTime.Now;
                                DupliVO.dtDescontoSpecified = true;

                                DupliVO.qtdSaldo = Qtd_Saldo;
                                DupliVO.qtdSaldoSpecified = true;

                                DupliVO.vlSaldo = Va_Saldo;
                                DupliVO.vlSaldoSpecified = true;


                                DupliVOList.Add(DupliVO);

                            }
                            cont++;

                            if (lbolLogOK == true)
                            {
                                Utility.SetLogTXT(pstrPathLog, "CD_ITEM_NOTA_REMESSA: " + CD_ITEM_NOTA_REMESSA.ToString());
                                Utility.SetLogTXT(pstrPathLog, "VA_UNITARIO_LIQUIDO: " + VA_UNITARIO_LIQUIDO.ToString());
                                Utility.SetLogTXT(pstrPathLog, "QTD_ITEM_PRODUTO: " + QTD_ITEM_PRODUTO.ToString());
                            }


                            WSGO_VA.itemNotaVO CaItemNota = new WSGO_VA.itemNotaVO();

                            CaItemNota.cdCodigoItem = CD_MATERIAL_ASSOCIADO;
                            CaItemNota.cdNumeroSequencia = CD_ITEM_NOTA_REMESSA;
                            CaItemNota.cdNumeroSequenciaSpecified = true;
                            CaItemNota.dsUnidade = "UN";
                            CaItemNota.vlValorLiquido = VA_UNITARIO_LIQUIDO;
                            CaItemNota.vlValorLiquidoSpecified = true;
                            CaItemNota.vlValorOriginal = VA_UNITARIO_LIQUIDO;
                            CaItemNota.vlValorOriginalSpecified = true;
                            CaItemNota.vlValorTabela = VA_UNITARIO_LIQUIDO;
                            CaItemNota.vlValorTabelaSpecified = true;
                            CaItemNota.quantidade = QTD_ITEM_PRODUTO;
                            CaItemNota.quantidadeSpecified = true;
                            CaItemNota.idNota = ID_NOTA;
                            CaItemNota.idNotaSpecified = true;
                            CaItemNota.codTabelaPreco = "";
                            CaItemNotaList.Add(CaItemNota);

                        }

                        VarBanco.Reader.Close();
                        if (cont > 0)
                        {
                            WSGO_VA.itemNotaVO[] item = CaItemNotaList.ToArray();
                            CaNota.itemNota = item;

                            WSGO_VA.duplicataNotaVO[] duplicata = DupliVOList.ToArray();
                            CaNota.duplicataNota = duplicata;
                        }
                        try
                        {
                            if (CaNota.idNota != 0)
                            {
                                CaNotaVOList.Add(CaNota);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (lbolLogOK == true)
                            {
                                Utility.SetLogTXT(pstrPathLog, "ERRO INTERNO DA DLL: " + ex.Message);
                            }
                        }

                    }

                    _erro = Flag;
                    _msgErro = "OK";

                }
                else
                {
                    _erro = Flag;
                    _msgErro = "OK";

                }

                return CaNotaVOList;
            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;

                if (lbolLogOK == true)
                {
                    Utility.SetLogTXT(pstrPathLog, "ERRO INTERNO DA DLL GRAVE: " + ex.Message);
                }

                return null;
            }

        }

        private void RetornarNotaGO_VA(WSGO_VA.mensagemVO Retorno, int NuCDRegistro, string pstrPathLog)
        {
            bool lbolLogOK = false;

            if (pstrPathLog.Trim() != "")
            {
                lbolLogOK = true;
            }

            if (lbolLogOK == true)
            {
                Utility.SetLogTXT(pstrPathLog, "----------------------------------------------------");
                Utility.SetLogTXT(pstrPathLog, "cltipo: " + Retorno.clTipo.ToString());
                Utility.SetLogTXT(pstrPathLog, "clSituacao: " + Retorno.nota.clSituacao.ToString());
            }

            InterfacesWSGOVA_BLL ObjBLL = new InterfacesWSGOVA_BLL();

            if (Retorno.clTipo == 1 && Retorno.nota.clSituacao == 99)
            {

                _contErroGO_VA++;

                Utility.SetLogTXT(pstrPathLog, "NÃO REALIZADO INTEGRAÇÃO PARA O ID DA NOTA: " + Retorno.nota.idNota.ToString());
                Utility.SetLogTXT(pstrPathLog, "DESCRIÇÃO DO ERRO: " + Retorno.dsDescricao);

                ObjBLL.Cadastra_Erro_Nota99(Retorno.nota.idNota, Convert.ToInt32(Retorno.dsOrigem), Retorno.dsDescricao, Retorno.nota.dsUsr);

            }
            else
            {
                if (Retorno.clTipo == 1)
                {
                    _contErroGO_VA++;

                    Utility.SetLogTXT(pstrPathLog, "RETORNO REALIZADO COM ERROS PARA O ID DA NOTA: " + NuCDRegistro.ToString());
                    Utility.SetLogTXT(pstrPathLog, "DESCRIÇÃO DO ERRO: " + Retorno.dsDescricao);

                    ObjBLL.Cadastra_Erro_Nota(NuCDRegistro, Convert.ToInt32(Retorno.dsOrigem), Retorno.dsDescricao, Retorno.nota.dsUsr);
                }
                else if (Retorno.clTipo == 3)
                {
                    _contErroGO_VA++;

                    Utility.SetLogTXT(pstrPathLog, "RETORNO REALIZADO COM ERROS PARA O ID DA NOTA: " + NuCDRegistro.ToString());
                    Utility.SetLogTXT(pstrPathLog, "DESCRIÇÃO DO ERRO: " + Retorno.dsDescricao);

                    ObjBLL.Cadastra_Erro_Nota(NuCDRegistro, Convert.ToInt32(Retorno.dsOrigem), Retorno.dsDescricao, "WS");
                }
                else
                {
                    if (!string.IsNullOrEmpty(Retorno.nota.nrNotaFis))
                    {
                        _contOKGO_VA++;

                        Utility.SetLogTXT(pstrPathLog, "RETORNO REALIZADO COM SUCESSO PARA O ID DA NOTA: " + NuCDRegistro.ToString());
                        Utility.SetLogTXT(pstrPathLog, "NUMERO DA NOTA FISCAL: " + Retorno.nota.nrNotaFis);
                        Utility.SetLogTXT(pstrPathLog, "SERIE DA NOTA FISCAL: " + Retorno.nota.dsSerie);

                        ObjBLL.Cadastra_Retorno_Nota(NuCDRegistro,
                                                     Retorno.nota.nrNotaFis,
                                                     Retorno.nota.dsSerie,
                                                     Retorno.nota.dtTransacao,
                                                     Retorno.nota.dtMov,
                                                     Retorno.nota.dsUsr,
                                                     Retorno.nota.cdChaveAcesso);
                    }
                    else
                    {
                        _contErroGO_VA++;
                        Utility.SetLogTXT(pstrPathLog, "ERRO NO RETORNO DA NOTA FISCAL, NÃO FOI RETORNADO O NUMERO DA NOTA FISCAL PELO WEBSERVICE.");
                        //diz que a nota esta nula ou vazia entao eh erro
                        _contErroNota = 1;
                    }
                }
            }


        }

        public string GetDadosContratoJSON(string pstrCodAssinante, string pstrCodContrato)
        {


            try
            {

                if (pstrCodContrato.Trim().Length == 11)
                {
                    string lstrSerie;
                    string lstrContrato;
                    string lstrDV;

                    lstrSerie = pstrCodContrato.Substring(0, 1).ToUpper();
                    lstrContrato = pstrCodContrato.Substring(1, 9);
                    lstrDV = pstrCodContrato.Substring(10, 1);

                    ContratoAssinanteBusiness ObjBLL = new ContratoAssinanteBusiness();
                    ContratoAssinanteEntity ObjEnt = new ContratoAssinanteEntity();

                    ObjEnt = ObjBLL.GetDadosContrato(int.Parse(pstrCodAssinante),
                                                     lstrSerie,
                                                     int.Parse(lstrContrato),
                                                     int.Parse(lstrDV)
                                                    );

                    if (ObjEnt == null)
                    {
                        ResultDadosContrato ObjEntResultErr = new ResultDadosContrato()
                        {
                            Sucesso = "N",
                            Descricao = "Dados não encontrato",
                        };

                        return JsonConvert.SerializeObject(ObjEntResultErr);
                    }
                    else
                    {
                        ResultDadosContrato ObjEntResult = new ResultDadosContrato()
                        {
                            Sucesso = "S",
                            Descricao = "",
                            serie_ctr = ObjEnt.NU_SERIE_CTR,
                            nu_ctr = ObjEnt.NU_CTR,
                            dv_ctr = ObjEnt.NU_DV_CTR,
                            ctr_formatado = ObjEnt.NU_SERIE_CTR + Int64.Parse(ObjEnt.NU_CTR).ToString("000000000") + ObjEnt.NU_DV_CTR,
                            cd_pessoa = ObjEnt.CD_CONTABIL_PESSOA,
                            nm_pessoa = ObjEnt.NM_PESSOA,
                            tp_pessoa = ObjEnt.ST_TP_PESSOA,
                            nu_doc = ObjEnt.NU_DOC,
                            ds_email = ObjEnt.DS_EMAIL,
                            nm_produto = ObjEnt.NM_PRODUTO,
                            st_situacao = ObjEnt.ST_SITUACAO 
                        };

                        return JsonConvert.SerializeObject(ObjEntResult);

                    }

                }
                else
                {
                    ResultDadosContrato ObjEntResultErr = new ResultDadosContrato()
                    {
                        Sucesso = "N",
                        Descricao = "Contrato fora do formato esperado",
                    };

                    return JsonConvert.SerializeObject(ObjEntResultErr);
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                ResultDadosContrato ObjEntErr = new ResultDadosContrato()
                {
                    Sucesso = "N",
                    Descricao = _msgErro,
                };

                return JsonConvert.SerializeObject(ObjEntErr);
            }


        }

        public string GetRamoAtividadeJSON(int pintTipo, int pintSituacao)
        {
            try
            {
                RamoAtividadeBusiness lObjBusinnes = new RamoAtividadeBusiness();
                List<RamoAtividadeEntity> lcolEnt = new List<RamoAtividadeEntity>();

                lcolEnt = lObjBusinnes.GetRamoAtividadeList(pintTipo);

                                
                if (lObjBusinnes.Erro != 0)
                {
                    return "-1;" + lObjBusinnes.MsgErro + ";" ;  
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Newtonsoft.Json.Formatting.None);
                    return "0;;" + lstrRet;
                }

                
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "-1;" + _msgErro + ";";
            }

        }

        public List<RamoAtividadeEntity> GetRamoAtividade(int pintTipo, int pintSituacao)
        {
            try
            {
                RamoAtividadeBusiness lObjBusinnes = new RamoAtividadeBusiness();
                List<RamoAtividadeEntity> lcolEnt = new List<RamoAtividadeEntity>();

                lcolEnt = lObjBusinnes.GetRamoAtividadeList(pintTipo);

                if (lObjBusinnes.Erro != 0)
                {
                    return null;
                }
                else
                {
                    return lcolEnt;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }

        }

        public string GetCuboClientejcJSON()
        {
            try
            {
                CuboClientejcBusiness lObjBusinnes = new CuboClientejcBusiness();
                List<CuboClienteJCEntity> lcolEnt = new List<CuboClienteJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboClienteJCList();


                if (lObjBusinnes.Erro != 0)
                {
                    return "N;" + lObjBusinnes.MsgErro + ";";
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Newtonsoft.Json.Formatting.None);
                    return "S;;" + lstrRet;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "N;" + _msgErro + ";";
            }

        }

        public List<CuboClienteJCEntity> GetCuboClientejc()
        {
            try
            {
                CuboClientejcBusiness lObjBusinnes = new CuboClientejcBusiness();
                List<CuboClienteJCEntity> lcolEnt = new List<CuboClienteJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboClienteJCList();

                if (lObjBusinnes.Erro != 0)
                {
                    return null;
                }
                else
                {
                    return lcolEnt;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }

        }

        public string GetCuboAssinaturajcJSON(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil= 0)
        {
            try
            {
                CuboAssinaturajcBusiness lObjBusinnes = new CuboAssinaturajcBusiness();
                List<CuboAssinaturaJCEntity> lcolEnt = new List<CuboAssinaturaJCEntity>();

                lcolEnt = lObjBusinnes.GetAssinaturaJCList(psDtInicioInic, psDtInicioFim, psCdcontabil);


                if (lObjBusinnes.Erro != 0)
                {
                    return "N;" + lObjBusinnes.MsgErro + ";";
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Newtonsoft.Json.Formatting.None);
                    return "S;;" + lstrRet;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "N;" + _msgErro + ";";
            }

        }

        public List<CuboAssinaturaJCEntity> GetCuboAssinaturajc(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0)
        {
            try
            {
                CuboAssinaturajcBusiness lObjBusinnes = new CuboAssinaturajcBusiness();
                List<CuboAssinaturaJCEntity> lcolEnt = new List<CuboAssinaturaJCEntity>();

                lcolEnt = lObjBusinnes.GetAssinaturaJCList(psDtInicioInic, psDtInicioFim, psCdcontabil);

                if (lObjBusinnes.Erro != 0)
                {
                    return null;
                }
                else
                {
                    return lcolEnt;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }

        }

        public string GetCuboRecibojcJSON(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0)
        {
            try
            {
                CuboRecibojcBusiness lObjBusinnes = new CuboRecibojcBusiness();
                List<CuboReciboJCEntity> lcolEnt = new List<CuboReciboJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboReciboJCList(psDtInicioInic, psDtInicioFim, psCdcontabil);
                

                if (lObjBusinnes.Erro != 0)
                {
                    return "N;" + lObjBusinnes.MsgErro + ";";
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Newtonsoft.Json.Formatting.None);
                    return "S;;" + lstrRet;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "N;" + _msgErro + ";";
            }

        }

        public List<CuboReciboJCEntity> GetCuboRecibojc(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0)
        {
            try
            {
                CuboRecibojcBusiness lObjBusinnes = new CuboRecibojcBusiness();
                List<CuboReciboJCEntity> lcolEnt = new List<CuboReciboJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboReciboJCList(psDtInicioInic, psDtInicioFim, psCdcontabil);

                if (lObjBusinnes.Erro != 0)
                {
                    return null;
                }
                else
                {
                    return lcolEnt;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }

        public string GetCuboProdutojcJSON()
        {
            try
            {
                CuboProdutojcBusiness lObjBusinnes = new CuboProdutojcBusiness();
                List<CuboProdutoJCEntity> lcolEnt = new List<CuboProdutoJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboProdutoJCList();
                

                if (lObjBusinnes.Erro != 0)
                {
                    return "N;" + lObjBusinnes.MsgErro + ";";
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Newtonsoft.Json.Formatting.None);
                    return "S;;" + lstrRet;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "N;" + _msgErro + ";";
            }

        }

        public List<CuboProdutoJCEntity> GetCuboProdutojc()
        {
            try
            {
                CuboProdutojcBusiness lObjBusinnes = new CuboProdutojcBusiness();
                List<CuboProdutoJCEntity> lcolEnt = new List<CuboProdutoJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboProdutoJCList();

                if (lObjBusinnes.Erro != 0)
                {
                    return null;
                }
                else
                {
                    return lcolEnt;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }

        public string GetCuboProdAgregjcJSON(Int32 psCdcontabil = 0)
        {
            try
            {
                CuboProdAgregjcBusiness lObjBusinnes = new CuboProdAgregjcBusiness();
                List<CuboProdAgregJCEntity> lcolEnt = new List<CuboProdAgregJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboProdAgregJCList(psCdcontabil);


                if (lObjBusinnes.Erro != 0)
                {
                    return "N;" + lObjBusinnes.MsgErro + ";";
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Newtonsoft.Json.Formatting.None);
                    return "S;;" + lstrRet;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "N;" + _msgErro + ";";
            }

        }

        public List<CuboProdAgregJCEntity> GetCuboProdAgregjc(Int32 psCdcontabil = 0)
        {
            try
            {
                CuboProdAgregjcBusiness lObjBusinnes = new CuboProdAgregjcBusiness();
                List<CuboProdAgregJCEntity> lcolEnt = new List<CuboProdAgregJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboProdAgregJCList(psCdcontabil);

                if (lObjBusinnes.Erro != 0)
                {
                    return null;
                }
                else
                {
                    return lcolEnt;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }

        public string GetCuboTipoRecJSON()
        {
            try
            {
                CuboTiposRecjcBusiness lObjBusinnes = new CuboTiposRecjcBusiness();
                List<CuboTiposRecJCEntity> lcolEnt = new List<CuboTiposRecJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboTiposRecJC();


                if (lObjBusinnes.Erro != 0)
                {
                    return "N;" + lObjBusinnes.MsgErro + ";";
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Newtonsoft.Json.Formatting.None);
                    return "S;;" + lstrRet;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "N;" + _msgErro + ";";
            }

        }

        public List<CuboTiposRecJCEntity> GetCuboTiposRecjc()
        {
            try
            {
                CuboTiposRecjcBusiness lObjBusinnes = new CuboTiposRecjcBusiness();
                List<CuboTiposRecJCEntity> lcolEnt = new List<CuboTiposRecJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboTiposRecJC();

                if (lObjBusinnes.Erro != 0)
                {
                    return null;
                }
                else
                {
                    return lcolEnt;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }
        
        public string GetCuboMovRecJSON(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0)
        {
            try
            {
                CuboMovRecjcBusiness lObjBusinnes = new CuboMovRecjcBusiness();
                List<CuboMovRecJCEntity> lcolEnt = new List<CuboMovRecJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboMovRecJCList(psDtInicioInic, psDtInicioFim, psCdcontabil);


                if (lObjBusinnes.Erro != 0)
                {
                    return "N;" + lObjBusinnes.MsgErro + ";";
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Newtonsoft.Json.Formatting.None);
                    return "S;;" + lstrRet;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "N;" + _msgErro + ";";
            }

        }

        public List<CuboMovRecJCEntity> GetCuboMovRecjc(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0)
        {
            try
            {
                CuboMovRecjcBusiness lObjBusinnes = new CuboMovRecjcBusiness();
                List<CuboMovRecJCEntity> lcolEnt = new List<CuboMovRecJCEntity>();

                lcolEnt = lObjBusinnes.GetCuboMovRecJCList(psDtInicioInic, psDtInicioFim, psCdcontabil);

                if (lObjBusinnes.Erro != 0)
                {
                    return null;
                }
                else
                {
                    return lcolEnt;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }

        //METODOS PARA ATENDER A ARQUITETURA POR SERVICO

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Metodos da pagina default
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            public string GetConteudoDestaque(string pstrPathConteudo, string pstrPathImgMKT)
            {
                try
                {
                    ConteudoIndexBusiness ObjBusiness = new ConteudoIndexBusiness();

                    string lstrRet = ObjBusiness.GetConteudoDestaque(pstrPathConteudo, pstrPathImgMKT);

                    return "S;;" + lstrRet.Replace(";","##");
                
                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return "N;" + _msgErro + ";";
                }
            }

            public string GetConteudoPromocao(string pstrPathConteudo, string pstrPathImgMKT)
            {
                try
                {
                    ConteudoIndexBusiness ObjBusiness = new ConteudoIndexBusiness();

                    string lstrRet = ObjBusiness.GetConteudoPromocao(pstrPathConteudo, pstrPathImgMKT);

                    return "S;;" + lstrRet.Replace(";", "##");
                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return "N;" + _msgErro + ";";
                }
            }

            public string GetConteudoDetalhe(string pstrPathConteudo, string pstrPathImgMKT, 
                                                int pintIDCampanha, int pintIDPlano)
            {
                try
                {
                    ConteudoIndexBusiness ObjBusiness = new ConteudoIndexBusiness();

                    string lstrRet = ObjBusiness.GetConteudoDetalhe(pstrPathConteudo, pstrPathImgMKT, pintIDCampanha, pintIDPlano);

                    return "S;;" + lstrRet.Replace(";", "##");

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return "N;" + _msgErro + ";";
                }
            }

            public string GetMontarBannerDestaque() 
            {
                try
                {
                    ConteudoIndexBusiness ObjBusiness = new ConteudoIndexBusiness();

                    string lstrRet = ObjBusiness.GetMontarBannerDestaque();

                    return "S;;" + lstrRet.Replace(";", "##");
                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return "N;" + _msgErro + ";";
                }
            }
           //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

           //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
           //Metodos da pagina Dados Pessoais
           //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            public string GetRamoAtividadeListJSON(int pintTipoRamo)
            {

                try
                {

                    RamoAtividadeBusiness ObjBusiness = new RamoAtividadeBusiness();

                    string lstrRetJSON = ObjBusiness.GetRamoAtividadeListService(pintTipoRamo);

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }


                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }

            }
            public string GetPrecoCampanhaPlanoProdutoJSON(int pintIDCampaha, int pintIDPlano, int pintProduto) 
            {
                try
                {

                    PlanoComercialBusiness ObjBusiness = new PlanoComercialBusiness();

                    string lstrRetJSON = ObjBusiness.GetPrecoCampanhaPlanoProdutoService(pintIDCampaha, pintIDPlano, pintProduto);

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
            }


            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Metodos da pagina Dados Entrega
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            public string GetTipoLogradouroListJSON()
            {
                try 
	            {

                    TipoLogradouroBusiness ObjBusiness = new TipoLogradouroBusiness();

                    string lstrRetJSON = ObjBusiness.GetTipoLogradouroListService();

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
            }
            public string GetViewLogrEntityJSON(string pstrCEP)
            {
                try
                {
                    ViewLogrBusiness  ObjBusiness = new ViewLogrBusiness();

                    string lstrRetJSON = ObjBusiness.GetViewLogrEntityService(pstrCEP);

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex) 
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
            }

            public string GetCodRoteirizacaoJSON(int pintIDProduto, int pintIDLogr, int pintNumeroResid)
            {

                try
                {

                    ViewLogrBusiness ObjBusinnes = new ViewLogrBusiness();

                    string lstrCodRot = ObjBusinnes.GetCodRoteirizacaoService(pintIDProduto, pintIDLogr, pintNumeroResid);

                    if (ObjBusinnes.Erro != 0)
                    {
                        _erro = ObjBusinnes.Erro;
                        _msgErro = ObjBusinnes.MsgErro;

                        return null; //"-1"
                    }
                    else
                    {
                        return lstrCodRot;
                    }


                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return "-1";
                }

            }

            public string GetParametroGlobalWEBJSON()
            {
                try
                {

                    ParametroGlobalBusiness ObjBusiness = new ParametroGlobalBusiness();

                    string lstrRetJSON = ObjBusiness.GetParametroGlobalWEBService();

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
            }

            public string GetViewLogrEntityIDJSON(Int32 pintIDLogr)
            {
                try
                {
                    ViewLogrBusiness ObjBusiness = new ViewLogrBusiness();

                    string lstrRetJSON = ObjBusiness.GetViewLogrEntityServiceID(pintIDLogr);

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
            }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da pagina Dados Pagamento
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            public string GetBandeiraCartaoListJSON()
            {
                try
                {

                    AdmCartaoBusiness ObjBusiness = new AdmCartaoBusiness();

                    string lstrRetJSON = ObjBusiness.GetBandeiraCartaoListService();

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
            }
            public string GetAdmCartaoListJSON(int pintBandeira)
            {
                try
                {

                    AdmCartaoBusiness ObjBusiness = new AdmCartaoBusiness();

                    string lstrRetJSON = ObjBusiness.GetAdmCartaoListService(pintBandeira);

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
            }
            public string GetAdmCobraGateWayJSON(int pintIDAdmCartao)
            {
                try
                {

                    AdmCartaoBusiness ObjBusiness = new AdmCartaoBusiness();

                    string lstrRetJSON = ObjBusiness.GetAdmCobraGateWayService(pintIDAdmCartao);

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
            }
            public string GetBandeiraCartaoListNovoJSON()
            {
                try
                {

                    AdmCartaoBusiness ObjBusiness = new AdmCartaoBusiness();

                    string lstrRetJSON = ObjBusiness.GetBandeiraCartaoListNovoService();

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
            }
            public string GetValidarListaNegraJSON(ListaNegraEntity pObjEnt)
            {
                try
                {

                    ListaNegraBusiness ObjBusiness = new ListaNegraBusiness();

                    string lstrRetJSON = ObjBusiness.GetValidarListaNegraService(pObjEnt);

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
            }
            public string GetContabilContratoJSON(Int32 pintCTR, int pintDVCTR, string pstrSerieCTR)
            {

                try
                {

                    ContratoAssinanteBusiness ObjBusinnes = new ContratoAssinanteBusiness();

                    string lstrRetJSON = ObjBusinnes.GetContabilContratoService(pintCTR, pintDVCTR, pstrSerieCTR);

                    if (ObjBusinnes.Erro != 0)
                    {
                        _erro = ObjBusinnes.Erro;
                        _msgErro = ObjBusinnes.MsgErro;

                        return null; //"-1"
                    }
                    else
                    {
                        return lstrRetJSON;
                    }


                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return "-1";
                }

            }
            public string GetParametrosPlanoJSON(int pintIDPlano)
            {
                try
                {

                    PlanoComercialBusiness ObjBusiness = new PlanoComercialBusiness();

                    string lstrRetJSON = ObjBusiness.GetParametrosPlanoService(pintIDPlano);

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;
                        Utility.SetLogXML("GetParametrosPlanoJSON", "Error", "1", false);

                    return null;
                    }
                    else
                    {
                        Utility.SetLogXML("GetParametrosPlanoJSON", "Error", "2", false);
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;
                    Utility.SetLogXML("GetParametrosPlanoJSON", "Error", "3", false);
                    return null;
                }
            }
            public string GetTipoAssinaturaListJSON(int pintIDProduto, int pintIDCampanha)
            {

                try
                {

                    TipoAssinaturaBusiness ObjBusinnes = new TipoAssinaturaBusiness();

                    string lstrRetJSON = ObjBusinnes.GetTipoAssinaturaListService(pintIDProduto, pintIDCampanha);

                    if (ObjBusinnes.Erro != 0)
                    {
                        _erro = ObjBusinnes.Erro;
                        _msgErro = ObjBusinnes.MsgErro;

                        return null; //"-1"
                    }
                    else
                    {
                        return lstrRetJSON;
                    }


                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }

            }
            public string GetTipoPagamentoListJSON(int pintIDProduto, int pintIDCampanha,
                                                  int pintIDTipoAssinatura, int pintTipoEntrega)
            {

                try
                {

                    TipoPagamentoBusiness ObjBusinnes = new TipoPagamentoBusiness();

                    string lstrRetJSON = ObjBusinnes.GetTipoPagamentoListService(pintIDProduto, pintIDCampanha, pintIDTipoAssinatura, pintTipoEntrega);

                    if (ObjBusinnes.Erro != 0)
                    {
                        _erro = ObjBusinnes.Erro;
                        _msgErro = ObjBusinnes.MsgErro;

                        return null; //"-1"
                    }
                    else
                    {
                        return lstrRetJSON;
                    }


                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }

            }
            public string GetBancoDebitoListJSON(int pintIDProdudo)
            {
                try
                {

                    BancoDebitoBusiness ObjBusiness = new BancoDebitoBusiness();

                    string lstrRetJSON = ObjBusiness.GetBancoDebitoListService(pintIDProdudo);

                    if (ObjBusiness.Erro != 0)
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;

                        return null;
                    }
                    else
                    {
                        return lstrRetJSON;
                    }

                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
            }
            
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Gravar Assinantura
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            public string GravarAssinaturaService(DadosPessoaEntity ObjPS,
                                                  DadosEntregaEntity ObjDE,
                                                  DadosPagamentoEntity ObjPG)
            {

                try
                {

                    GravaAssinaturaBusiness ObjBusiness = new GravaAssinaturaBusiness();
                    //ObjBusiness.GravarAssinatura(ObjPS, ObjDE, ObjPG);
                    ObjBusiness.GravarAssinaturaNOVO(ObjPS, ObjDE, ObjPG);

                    string lstrRet = "";

                    if (ObjBusiness.Erro == 0)
                    {
                        lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro + ";" + ObjBusiness.CTR + ";" + ObjBusiness.InfoPlano;
                    }
                    else
                    {
                        lstrRet = ObjBusiness.Erro.ToString() + ";" +  ObjBusiness.MsgErro; 
                    }

                   Utility.SetLogXML("Erro Gravar assinatura", "GravarAssinaturaService", lstrRet, false, 1);


                return lstrRet;
                }
                catch (Exception ex) 
                {

                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }
                
            }



            public string GravarAssinaturaIntegracao(string pstrDadosPessoa,
                                                     string pstrDadosEndereco,
                                                     string pstrDadosPagamento,
                                                     string pstrDadosGateway)
            {
                try
                {

                    string lstrRetInfra = "";
                    lstrRetInfra = GetStatusConexaoBD();

                    if (lstrRetInfra != "")
                    {
                        RetornoJSON ObjRetJSON = new RetornoJSON();
                        ObjRetJSON = JsonConvert.DeserializeObject<RetornoJSON>(lstrRetInfra);

                        if (ObjRetJSON != null)
                        {
                            if (ObjRetJSON.CodigoRetorno != "0")
                            {

                            Utility.SetLogXML("Teste de Conectividade", "LogIntegracao", ObjRetJSON.DescricaoRetorno, false, 1);
                            return Utility.GetRetornoJSON("-99", "Falta de Conectividade - " + ObjRetJSON.DescricaoRetorno, "");

                            }
                        }
                    }  


                    if (string.IsNullOrEmpty(pstrDadosPessoa))
                    {
                        return Utility.GetRetornoJSON("-1", "Erro - Dados da Pessoa (JSON) não pode ser vazio" , "");
                    }

                    if (string.IsNullOrEmpty(pstrDadosEndereco))
                    {
                        return Utility.GetRetornoJSON("-1", "Erro - Dados de Endereço (JSON) não pode ser vazio", "");
                    }

                    if (string.IsNullOrEmpty(pstrDadosPagamento))
                    {
                        return Utility.GetRetornoJSON("-1", "Erro - Dados de Pagamento (JSON) não pode ser vazio", "");
                    }

                    GravaAssinaturaBusiness ObjBLL = new GravaAssinaturaBusiness();

                    string lstrRetJSON = ObjBLL.GravarAssinaturaIntegracaoNOVO(pstrDadosPessoa,
                                                                               pstrDadosEndereco,
                                                                               pstrDadosPagamento,
                                                                               pstrDadosGateway);

                    return lstrRetJSON;

                }
                catch (Exception ex)
                {
                    _erro = -1;
                    _msgErro = ex.Message;

                    return Utility.GetRetornoJSON("-99", "Erro" + _msgErro, "");
                }
            }

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Gravar Abandono
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            public string GravarAbandonoService(string pstrDOC, string pstrEmail, AbandonoEntity pObjEnt, int pintEtapa, int pintStatus,
                                                string pstrNuSerieCTR = "", Int32 pintNuCTR = 0, int pintNuDvCTR = 0)
            {

                try
                {
                    AbandonoBusiness ObjBusiness = new AbandonoBusiness();
                    ObjBusiness.SetAbandono(pstrDOC, pstrEmail, pObjEnt, pintEtapa, pintStatus, pstrNuSerieCTR, pintNuCTR, pintNuDvCTR);

                    string lstrRet = "";

                    if (ObjBusiness.Erro == 0)
                    {
                        lstrRet = ObjBusiness.Erro.ToString() + ";OK" ;
                    }
                    else
                    {
                        lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                    }

                    return lstrRet;
                }
                catch (Exception ex)
                {
                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }

            }

            public string GetAbandonoLojaService(string pstrDOC, int pintTipo)
            {
                try
                {
                    AbandonoBusiness ObjBusiness = new AbandonoBusiness();
                    string lstrRet = ObjBusiness.GetAbandono(pstrDOC, pintTipo);

                    if (ObjBusiness.Erro == 0)
                    {
                        return lstrRet;
                    }
                    else
                    {
                        _erro = 1;
                        _msgErro = ObjBusiness.MsgErro;
                        return null; 
                    }
                    
                }
                catch (Exception ex)
                {
                    _erro = 1;
                    _msgErro = ex.Message;

                    return null; 
                }
            
            }


            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Metodos da pagina da area administrativa
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            public string GetLoginUsuarioJSON(string pstrUsuario, string pstrSenha)
            {
                try
                {
                    UsuarioLoginBusiness ObjBusiness = new UsuarioLoginBusiness();
                    string lstrRet = ObjBusiness.GetLoginUsuarioService(pstrUsuario, pstrSenha);

                    if (ObjBusiness.Erro == 0) 
                    {
                        return lstrRet;
                    }
                    else
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    _erro = 1;
                    _msgErro = ex.Message;

                    return null;
                }

            }
            public string GetCampanhaListJSON(int pintIDCamp = 0)
            {
                try
                {

                    CampanhaBusiness ObjBusiness = new CampanhaBusiness();
                    string lstrRet = ObjBusiness.GetCampanhaListService(pintIDCamp);

                    if (ObjBusiness.Erro == 0)
                    {
                        return lstrRet;
                    }
                    else
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;
                        return null;
                    }

                }
                catch (Exception ex)
                {

                    _erro = 1;
                    _msgErro = ex.Message;

                    return null;
                }
            }
            public string GetPlanoCampanhaJSON(int pintIDCampanha)
            {
                try
                {

                    PlanoComercialBusiness ObjBusiness = new PlanoComercialBusiness();
                    string lstrRet = ObjBusiness.GetPlanoCampanhaService(pintIDCampanha);

                    if (ObjBusiness.Erro == 0)
                    {
                        return lstrRet;
                    }
                    else
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;
                        return null;
                    }

                }
                catch (Exception ex)
                {

                    _erro = 1;
                    _msgErro = ex.Message;

                    return null;
                }
            }
            public string GetCampanhaPlanoWEBJSON(int pintIDCampanha)
            {
                try
                {

                    CampanhaPlanoWEBBusiness ObjBusiness = new CampanhaPlanoWEBBusiness();
                    string lstrRet = ObjBusiness.GetCampanhaPlanoWEBService(pintIDCampanha);

                    if (ObjBusiness.Erro == 0)
                    {
                        return lstrRet;
                    }
                    else
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;
                        return null;
                    }

                }
                catch (Exception ex)
                {

                    _erro = 1;
                    _msgErro = ex.Message;

                    return null;
                }
            }
            public string GravaCampanhaPlanoWEB(CampanhaPlanoWEBEntity ObjEnt)
            {

                try
                {

                    CampanhaPlanoWEBBusiness ObjBusiness = new CampanhaPlanoWEBBusiness();
                    ObjBusiness.GravaCampanhaPlanoWEB(ObjEnt);

                    string lstrRet = "";

                    if (ObjBusiness.Erro == 0)
                    {
                        lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                    }
                    else
                    {
                        lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                    }

                    return lstrRet;
                }
                catch (Exception ex)
                {

                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }

            }
            public string AtualizaCampanhaPlanoWEB(CampanhaPlanoWEBEntity ObjEnt)
            {

                try
                {

                    CampanhaPlanoWEBBusiness ObjBusiness = new CampanhaPlanoWEBBusiness();
                    ObjBusiness.AtualizaCampanhaPlanoWEB(ObjEnt);

                    string lstrRet = "";

                    if (ObjBusiness.Erro == 0)
                    {
                        lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                    }
                    else
                    {
                        lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                    }

                    return lstrRet;
                }
                catch (Exception ex)
                {

                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }

            }
            public string DeleteCampanhaPlanoWEB(CampanhaPlanoWEBEntity ObjEnt)
            {

                try
                {

                    CampanhaPlanoWEBBusiness ObjBusiness = new CampanhaPlanoWEBBusiness();
                    ObjBusiness.DeleteCampanhaPlanoWEB(ObjEnt);

                    string lstrRet = "";

                    if (ObjBusiness.Erro == 0)
                    {
                        lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                    }
                    else
                    {
                        lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                    }

                    return lstrRet;
                }
                catch (Exception ex)
                {

                    _erro = -99;
                    _msgErro = ex.Message;

                    return null;
                }

            }
            public string GetTotalDestaque()
            {
                try
                {

                    CampanhaPlanoWEBBusiness ObjBusiness = new CampanhaPlanoWEBBusiness();
                    string lstrRet = ObjBusiness.GetTotalDestaqueService();

                    if (ObjBusiness.Erro == 0)
                    {
                        return lstrRet;
                    }
                    else
                    {
                        _erro = ObjBusiness.Erro;
                        _msgErro = ObjBusiness.MsgErro;
                        return null;
                    }

                }
                catch (Exception ex)
                {

                    _erro = 1;
                    _msgErro = ex.Message;

                    return null;
                }
            }


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - contratos
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public string GetLerContratosCentralJSON(int pintCD_CONTABIL_PESSOA,
                                                 string pstrDS_EMAIL,
                                                 Int64 plngNU_CPF,
                                                 Int64 plngNU_CNPJ)
        {
            try
            {

                ContratoAssinanteCentralBusiness ObjBusiness = new ContratoAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.GetLerContratosCentralService(pintCD_CONTABIL_PESSOA, pstrDS_EMAIL, plngNU_CPF, plngNU_CNPJ);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - contratos
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public string GetImprimirContratosCentralJSON(int pintCD_CONTABIL_PESSOA,
                                                 string pstrNUSERIECTR,
                                                 int pintNUCTR,
                                                 byte pbyteNUDVCTR) 
        {
            try
            {

                ContratoAssinanteCentralBusiness ObjBusiness = new ContratoAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.GetImprimirContratosCentralService(pintCD_CONTABIL_PESSOA, pstrNUSERIECTR, pintNUCTR, pbyteNUDVCTR);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }
                
            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - dados do assinante
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerDadosDoAssinanteCentralJSON(int pintCD_CONTABIL_PESSOA)
        {
            try
            {

                DadosDoAssinanteCentralBusiness ObjBusiness = new DadosDoAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.GetLerDadosDoAssinanteCentralService(pintCD_CONTABIL_PESSOA);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - cargo
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerCargoCentralJSON()
        {
            try
            {

                CargoCentralBusiness ObjBusiness = new CargoCentralBusiness();

                string lstrRet = ObjBusiness.GetLerCargoCentralService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - estado civil
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerEstadoCivilCentralJSON()
        {
            try
            {

                EstadoCivilCentralBusiness ObjBusiness = new EstadoCivilCentralBusiness();

                string lstrRet = ObjBusiness.GetLerEstadoCivilCentralService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - grau de instrução
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerGrauInstrucaoCentralJSON()
        {
            try
            {

                GrauInstrucaoCentralBusiness ObjBusiness = new GrauInstrucaoCentralBusiness();

                string lstrRet = ObjBusiness.GetLerGrauInstrucaoCentralService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - nacionalidade
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerNacionalidadeCentralJSON()
        {
            try
            {

                NacionalidadeCentralBusiness ObjBusiness = new NacionalidadeCentralBusiness();

                string lstrRet = ObjBusiness.GetLerNacionalidadeCentralService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - nacionalidade
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerNaturalidadeCentralJSON()
        {
            try
            {

                NaturalidadeCentralBusiness ObjBusiness = new NaturalidadeCentralBusiness();

                string lstrRet = ObjBusiness.GetLerNaturalidadeCentralService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - profissao
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerProfissaoCentralJSON()
        {
            try
            {

                ProfissaoCentralBusiness ObjBusiness = new ProfissaoCentralBusiness();

                string lstrRet = ObjBusiness.GetLerProfissaoCentralService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - ramo de atividade
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerRamoAtividadeCentralJSON()
        {
            try
            {

                RamoAtividadeCentralBusiness ObjBusiness = new RamoAtividadeCentralBusiness();

                string lstrRet = ObjBusiness.GetLerRamoAtividadeCentralService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - Alterar dados Assinatura
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string SetAlterarDadosDoAssinanteCentralJSON(byte? ST_TP_PESSOA, string NM_PESSOA,
                                                            int? CD_CONTABIL_PESSOA, string NM_RESPONSAVEL,
                                                            string NU_IDENTIDADE, string NM_ORGAO_EMISSOR,
                                                            DateTime? DT_EMISSAO, byte? ST_ESTADO_CIVIL,
                                                            DateTime? DT_NASC_FUND, string NM_FANTASIA,
                                                            string NU_INSCR_MUN, string NU_INSCR_EST,
                                                            short? CD_RAMO, string DS_NOME_ABREV,
                                                            int? CD_GRUPO_AFINIDADE, byte? CD_TP_TRATAMENTO,
                                                            int? CD_CARGO, int? CD_GRAU_INSTRUCAO,
                                                            int? CD_NACIONALIDADE, int? CD_NATURALIDADE,
                                                            int? CD_PROFISSAO, string NM_SOBRENOME, long? NU_CPF_RESP,
                                                            Boolean pbolAbrirTransacao,
                                                            Boolean pbolFecharConexao)
        {
            try
            {

                DadosDoAssinanteCentralBusiness ObjBusiness = new DadosDoAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.SetAlterarDadosDoAssinanteCentralService(ST_TP_PESSOA, NM_PESSOA,
                                                                                       CD_CONTABIL_PESSOA, NM_RESPONSAVEL,
                                                                                       NU_IDENTIDADE, NM_ORGAO_EMISSOR,
                                                                                       DT_EMISSAO, ST_ESTADO_CIVIL,
                                                                                       DT_NASC_FUND, NM_FANTASIA,
                                                                                       NU_INSCR_MUN, NU_INSCR_EST,
                                                                                       CD_RAMO, DS_NOME_ABREV,
                                                                                       CD_GRUPO_AFINIDADE, CD_TP_TRATAMENTO,
                                                                                       CD_CARGO, CD_GRAU_INSTRUCAO,
                                                                                       CD_NACIONALIDADE, CD_NATURALIDADE,
                                                                                       CD_PROFISSAO, NM_SOBRENOME, NU_CPF_RESP,
                                                                                       pbolAbrirTransacao,
                                                                                       pbolFecharConexao);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - telefone
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerTelefoneAssinanteCentralJSON(int CD_CONTABIL_PESSOA)
        {
            try
            {

                TelefoneAssinanteCentralBusiness ObjBusiness = new TelefoneAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.GetLerTelefoneCentralService(CD_CONTABIL_PESSOA);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }

        }
        public string SetIncluirTelefoneAssinanteCentralService(int? CD_CONTABIL_PESSOA,
                                                               byte NU_SEQ,
                                                               byte? ST_TP_TELEFONE,
                                                               short? NU_DDD,
                                                               string NU_TEL,
                                                               string NU_RAMAL,
                                                               string DS_OBS,
                                                               int? NU_DDI,
                                                               Boolean pbolAbrirTransacao,
                                                               Boolean pbolFecharConexao)
        {
            try
            {
                TelefoneAssinanteCentralBusiness ObjBusiness = new TelefoneAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.SetIncluirTelefoneCentralService(CD_CONTABIL_PESSOA, NU_SEQ, ST_TP_TELEFONE,
                                                                              NU_DDD, NU_TEL, NU_RAMAL, DS_OBS, NU_DDI,
                                                                              pbolAbrirTransacao, pbolFecharConexao);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjBusiness.MsgErro, "");
                }
                else
                {
                    _erro = 0;
                    _msgErro = "";

                    return Utility.GetRetornoJSON("0", "OK", "");
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", _msgErro, "");
            }

        }
        public string SetAlterarTelefoneAssinanteCentral(int? CD_CONTABIL_PESSOA,
                                                        byte NU_SEQ,
                                                        byte? ST_TP_TELEFONE,
                                                        short? NU_DDD,
                                                        string NU_TEL,
                                                        string NU_RAMAL,
                                                        string DS_OBS,
                                                        int? NU_DDI,
                                                        Boolean pbolAbrirTransacao,
                                                        Boolean pbolFecharConexao)
        {
            try
            {
                TelefoneAssinanteCentralBusiness ObjBusiness = new TelefoneAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.SetAlterarTelefoneCentralService(CD_CONTABIL_PESSOA, NU_SEQ, ST_TP_TELEFONE,
                                                                              NU_DDD, NU_TEL, NU_RAMAL, DS_OBS, NU_DDI,
                                                                              pbolAbrirTransacao, pbolFecharConexao);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjBusiness.MsgErro, "");
                }
                else
                {
                    _erro = 0;
                    _msgErro = "";

                    return Utility.GetRetornoJSON("0", "OK", "");
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", _msgErro, "");
            }

        }
        public string SetDeletarTelefoneAssinanteCentral(int? CD_CONTABIL_PESSOA,
                                                        byte NU_SEQ,
                                                        Boolean pbolAbrirTransacao,
                                                        Boolean pbolFecharConexao)
        {
            try
            {
                TelefoneAssinanteCentralBusiness ObjBusiness = new TelefoneAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.SetDeletarTelefoneCentralService (CD_CONTABIL_PESSOA, NU_SEQ,
                                                                              pbolAbrirTransacao, pbolFecharConexao);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjBusiness.MsgErro, "");
                }
                else
                {
                    _erro = 0;
                    _msgErro = "";

                    return Utility.GetRetornoJSON("0", "OK", "");
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", _msgErro, "");
            }

        }


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - email
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerEmailDoAssinanteCentralJSON(int CD_CONTABIL_PESSOA)
        {
            try
            {

                EmailAssinanteCentralBusiness ObjBusiness = new EmailAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.GetLerEmailDoAssinanteCentralService(CD_CONTABIL_PESSOA);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }

        }

        public string SetIncluirEmailDoAssinanteCentralService(int? CD_CONTABIL_PESSOA,
                                                              byte NU_SEQ,
                                                              string DS_EMAIL,
                                                              byte? ST_SITUACAO,
                                                              byte? ST_EMAIL_PRINCIPAL,
                                                              int CD_TIPO_EMAIL,
                                                              Boolean pbolAbrirTransacao,
                                                              Boolean pbolFecharConexao)
        {
            try
            {
                EmailAssinanteCentralBusiness ObjBusiness = new EmailAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.SetIncluirEmailAssinanteCentralService(CD_CONTABIL_PESSOA, NU_SEQ, DS_EMAIL,
                                                                                    ST_SITUACAO, ST_EMAIL_PRINCIPAL, CD_TIPO_EMAIL,
                                                                                    pbolAbrirTransacao, pbolFecharConexao);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjBusiness.MsgErro, "");
                }
                else
                {
                    _erro = 0;
                    _msgErro = "";

                    return Utility.GetRetornoJSON("0", "OK", "");
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", _msgErro, "");
            }

        }
        public string SetAlterarEmailDoAssinanteCentralService(int? CD_CONTABIL_PESSOA,
                                                    byte NU_SEQ,
                                                    string DS_EMAIL,
                                                    byte? ST_SITUACAO,
                                                    byte? ST_EMAIL_PRINCIPAL,
                                                    int CD_TIPO_EMAIL,
                                                    Boolean pbolAbrirTransacao,
                                                    Boolean pbolFecharConexao)
        {
            try
            {
                EmailAssinanteCentralBusiness ObjBusiness = new EmailAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.SetAlterarEmailAssinanteCentralService(CD_CONTABIL_PESSOA, NU_SEQ, DS_EMAIL,
                                                                                    ST_SITUACAO, ST_EMAIL_PRINCIPAL, CD_TIPO_EMAIL,
                                                                                    pbolAbrirTransacao, pbolFecharConexao);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjBusiness.MsgErro, "");
                }
                else
                {
                    _erro = 0;
                    _msgErro = "";

                    return Utility.GetRetornoJSON("0", "OK", "");
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", _msgErro, "");
            }

        }
        
        public string SetDeletarEmailDoAssinanteCentralService(int? CD_CONTABIL_PESSOA,
                                                        byte NU_SEQ,
                                                        Boolean pbolAbrirTransacao,
                                                        Boolean pbolFecharConexao)
        {
            try
            {
                EmailAssinanteCentralBusiness ObjBusiness = new EmailAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.SetDeletarEmailAssinanteCentralService(CD_CONTABIL_PESSOA, NU_SEQ,
                                                                                    pbolAbrirTransacao, pbolFecharConexao);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjBusiness.MsgErro, "");
                }
                else
                {
                    _erro = 0;
                    _msgErro = "";

                    return Utility.GetRetornoJSON("0", "OK", "");
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", _msgErro, "");
            }

        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - tipo email
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerTipoEmailCentralJSON()
        {
            try
            {

                TipoEmailCentralBusiness ObjBusiness = new TipoEmailCentralBusiness();

                string lstrRet = ObjBusiness.GetLerTipoEmailCentralService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }

        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - Grupo Afinidade
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerAfinidadeAssinanteCentralJSON(int pintCD_CONTABIL_PESSOA)
        {
            try
            {

                AfinidadeAssinanteCentralBusiness ObjBusiness = new AfinidadeAssinanteCentralBusiness();

                string lstrRet = ObjBusiness.GetLerAfinidadeAssinanteCentralService(pintCD_CONTABIL_PESSOA);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }

        }
        public string GetLerGrupoAfinidadeCentralJSON(int pintTipo)
        {
            try
            {

                GrupoAfinidadeCentralBusiness ObjBusiness = new GrupoAfinidadeCentralBusiness();

                string lstrRet = ObjBusiness.GetLerGrupoAfinidadeCentralService(pintTipo);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }

        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - Rede Social
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetLerRedeSocialCentralJSON(int CD_CONTABIL_PESSOA)
        {
            try
            {

                RedeSocialCentralBusiness ObjBusiness = new RedeSocialCentralBusiness();

                string lstrRet = ObjBusiness.GeterLerRedeSocialCentralService(CD_CONTABIL_PESSOA);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }

        }
        public string SetIncluirRedeSocialCentralService(int? CD_CONTABIL_PESSOA,
                                                        byte NU_SEQ,
                                                        string DS_REDE_SOCIAL,
                                                        string DS_EMAIL,
                                                        int CD_TIPO_REDE_SOCIAL,
                                                        string DS_USUARIO,
                                                        string ID_TOKEN,
                                                        Boolean pbolAbrirTransacao,
                                                        Boolean pbolFecharConexao)
        {
            try
            {
                RedeSocialCentralBusiness ObjBusiness = new RedeSocialCentralBusiness();

                string lstrRet = ObjBusiness.SetIncluirRedeSocialCentralService(CD_CONTABIL_PESSOA, NU_SEQ, DS_REDE_SOCIAL,
                                                                               DS_EMAIL, CD_TIPO_REDE_SOCIAL, DS_USUARIO, ID_TOKEN,
                                                                               pbolAbrirTransacao, pbolFecharConexao);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjBusiness.MsgErro, "");
                }
                else
                {
                    _erro = 0;
                    _msgErro = "";

                    return Utility.GetRetornoJSON("0", "OK", "");
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", _msgErro, "");
            }

        }
        public string SetAlterarRedeSocialCentralService(int? CD_CONTABIL_PESSOA,
                                                        byte NU_SEQ,
                                                        string DS_REDE_SOCIAL,
                                                        string DS_EMAIL,
                                                        int CD_TIPO_REDE_SOCIAL,
                                                        string DS_USUARIO,
                                                        string ID_TOKEN,
                                                        Boolean pbolAbrirTransacao,
                                                        Boolean pbolFecharConexao)
        {
            try
            {
                RedeSocialCentralBusiness ObjBusiness = new RedeSocialCentralBusiness();

                string lstrRet = ObjBusiness.SetAlterarRedeSocialCentralService(CD_CONTABIL_PESSOA, NU_SEQ, DS_REDE_SOCIAL,
                                                                               DS_EMAIL, CD_TIPO_REDE_SOCIAL, DS_USUARIO, ID_TOKEN,
                                                                               pbolAbrirTransacao, pbolFecharConexao);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjBusiness.MsgErro, "");
                }
                else
                {
                    _erro = 0;
                    _msgErro = "";

                    return Utility.GetRetornoJSON("0", "OK", "");
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", _msgErro, "");
            }

        }

        public string SetDeletarRedeSocialCentralService(int? CD_CONTABIL_PESSOA,
                                                        byte NU_SEQ,
                                                        Boolean pbolAbrirTransacao,
                                                        Boolean pbolFecharConexao)
        {
            try
            {
                RedeSocialCentralBusiness ObjBusiness = new RedeSocialCentralBusiness();

                string lstrRet = ObjBusiness.SetDeletarRedeSocialCentralService(CD_CONTABIL_PESSOA, NU_SEQ,
                                                                                pbolAbrirTransacao, pbolFecharConexao);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjBusiness.MsgErro, "");
                }
                else
                {
                    _erro = 0;
                    _msgErro = "";

                    return Utility.GetRetornoJSON("0", "OK", "");
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", _msgErro, "");
            }

        }





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos da central web - Usuario
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string GetAcessoProcedureJSON(string pstrDS_EMAIL)
        {
            try
            {

                UsuarioCentralBusiness ObjBusiness = new UsuarioCentralBusiness();

                string lstrRet = ObjBusiness.GetAcessoProcedureService(pstrDS_EMAIL);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetAcessoSenhaProcedureJSON(string pstrDS_EMAIL, string pstrDS_SENHA)
        {
            try
            {
                UsuarioCentralBusiness ObjBusiness = new UsuarioCentralBusiness();

                string lstrRet = ObjBusiness.GetAcessoSenhaProcedureService(pstrDS_EMAIL, pstrDS_SENHA);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }

        }
        public string GetEsqueciSenhaProcedureJSON(string pstrDS_EMAIL, string pstrNU_CPF, string pstrNU_CNPJ)
        {
            try
            {
                UsuarioCentralBusiness ObjBusiness = new UsuarioCentralBusiness();

                string lstrRet = ObjBusiness.GetEsqueciSenhaProcedureService(pstrDS_EMAIL, pstrNU_CPF, pstrNU_CNPJ);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string SetAlterarSenhaProcedureJSON(string pstrDS_EMAIL, string pstrDS_SENHA, string pstrNU_CPF, string pstrNU_CNPJ)
        {
            try
            {
                UsuarioCentralBusiness ObjBusiness = new UsuarioCentralBusiness();

                ObjBusiness.SetAlterarSenhaProcedure(pstrDS_EMAIL, pstrDS_SENHA, pstrNU_CPF, pstrNU_CNPJ);

                if (ObjBusiness.Erro == 0)
                {
                    _erro = 0;
                    _msgErro = "OK";
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                }

                string lstrRet = Utility.GetRetornoJSON(_erro.ToString(), _msgErro, "");

                return lstrRet; 

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;

                string lstrRet = Utility.GetRetornoJSON(_erro.ToString(), _msgErro, "");

                return lstrRet;
            }
        }
        public string GetPesquisaParametroEmailJSON()
        {
            try
            {
                UsuarioCentralBusiness ObjBusiness = new UsuarioCentralBusiness();

                string lstrRet = ObjBusiness.GetPesquisaParametroEmailService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetPesquisaParametroGlobalWebJSON()
        {
            try
            {
                UsuarioCentralBusiness ObjBusiness = new UsuarioCentralBusiness();

                string lstrRet = ObjBusiness.GetPesquisaParametroEmailService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetAcessoIntegradoJSON(int pintCD_CONTABIL_PESSOA, string pstrNU_SERIE_CTR, int pintNU_CTR, int pintNU_DV_CTR)
        {
            try
            {
                UsuarioCentralBusiness ObjBusiness = new UsuarioCentralBusiness();

                string lstrRet = ObjBusiness.GetAcessoIntegradoService(pintCD_CONTABIL_PESSOA, pstrNU_SERIE_CTR, pintNU_CTR, pintNU_DV_CTR);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetAcessoIntegradoSSOJCJSON(int pintCD_CONTABIL_PESSOA, string pstrNU_DOC, string pstrDS_EMAIL)
        {
            try
            {
                UsuarioCentralBusiness ObjBusiness = new UsuarioCentralBusiness();

                string lstrRet = ObjBusiness.GetAcessoIntegradoSSOJCService(pintCD_CONTABIL_PESSOA, pstrNU_DOC, pstrDS_EMAIL);
                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;

                return null;
            }
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodo para Envio de Email (Recuperação de senha Central WEB) - JC
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string SendMailAPI(string pstrUrlBasePOST, 
                                  string pstrAPIKey,
                                  string pstrDomain,
                                  string pstrFromMail,
                                  string pstrToMail,
                                  string pstrSubject,
                                  string pstrText,
                                  int pintTipoEmail = 2,
                                  string pstrPathAnexo = "",
                                  string pstrFileAnexo = "")
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                
                RestClient client = new RestClient();
                client.BaseUrl = new Uri(pstrUrlBasePOST.Trim());
                client.Authenticator = new HttpBasicAuthenticator("api", pstrAPIKey.Trim());
                

                RestRequest request = new RestRequest();

                request.UseDefaultCredentials = true;
                request.AlwaysMultipartFormData = true;
                //request.AddHeader("Content-Type", "multipart/form-data");
                request.Timeout = 200000;


                request.Resource = "{domain}/messages";
                request.AddParameter("domain", pstrDomain.Trim(), ParameterType.UrlSegment);
                request.AddParameter("from", pstrFromMail.Trim());
                request.AddParameter("to", pstrToMail.Trim());
                request.AddParameter("subject", pstrSubject.Trim());
                request.RequestFormat = DataFormat.None;

                if (pintTipoEmail == 1)
                {
                    request.AddParameter("html", pstrText);
                }
                else
                {
                    request.AddParameter("text", pstrText);
                }

                if ((!string.IsNullOrEmpty(pstrPathAnexo)) && (!string.IsNullOrEmpty(pstrFileAnexo)))
                {
                    request.AddFile("attachment", Path.Combine(pstrPathAnexo, pstrFileAnexo));
                }

                request.Method = Method.POST;

                IRestResponse ObjRet = client.Execute(request);

                string lstrVetorRet = "";

                if (ObjRet.StatusCode.ToString()  == "OK")
                {
                    lstrVetorRet = "0;;" + ObjRet.StatusCode + ";" + ObjRet.StatusDescription + ";" + ObjRet.ErrorMessage;
                }
                else
                {
                    lstrVetorRet = "-1;;" + ObjRet.StatusCode + ";" + ObjRet.ErrorException + " - " + ObjRet.StatusDescription + ";" + ObjRet.ErrorMessage;
                }
                
                return lstrVetorRet;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return _erro.ToString() + ";" + _msgErro;
            }

        }

        public string SetGravarRegistroBoletoBradesco(BoletoRegistroOnLineEntity pObjRegistro,
                                                        string pstrPathCertificado,
                                                        string pstrSenhaCertificado,
                                                        string pstrURLEndPointBanco)
        {

            //X509Certificate2 certificado = new X509Certificate2(@"C:\Certificado\Empresa 1.pfx", "2104");
            //string url = "https://cobranca.bradesconetempresa.b.br/ibpjregistrotitulows/registrotitulohomologacao";

            //Foi habilitado o item Process Model -> Load User Profile -> True no IIS (pool)

            if (!File.Exists(pstrPathCertificado))
            {
                return "-1" + "," + "Erro Arquivo não localizado - " + pstrPathCertificado + "," + "0";
            }

            X509Certificate2 certificado = new X509Certificate2(pstrPathCertificado, pstrSenhaCertificado);
            string url = pstrURLEndPointBanco;

            //convertendo classe para JSON
            var data = JsonConvert.SerializeObject(pObjRegistro, Newtonsoft.Json.Formatting.Indented);

            var fileContent = new System.Text.UTF8Encoding().GetBytes(data);

            ContentInfo content = new ContentInfo(fileContent);
            SignedCms signedCms = new SignedCms(content, false);
            CmsSigner signer = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, certificado);
            signedCms.ComputeSignature(signer);

            byte[] signEnv = signedCms.Encode();
            var string64 = Convert.ToBase64String(signEnv);


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 1000 * 60;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Method = "POST";


            // Alteramos o conteúdo para “text/xml”
            request.ContentType = "text/xml";

            var bytes = Encoding.UTF8.GetBytes(string64);

            request.ContentLength = bytes.Length;
            using (var stream = request.GetRequestStream())
            {
                // Write pkcs#7 into the stream
                stream.Write(bytes, 0, bytes.Length);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string result = "";

                using (Stream strm = response.GetResponseStream())
                {
                    result = new StreamReader(strm, Encoding.UTF8).ReadToEnd();
                }

                //tratar retorno
                int stratIndex = result.IndexOf("<return>");
                int endIndex = result.IndexOf("</return>");
                string resultJSON = result.Substring(stratIndex, (endIndex - stratIndex)).Replace("<return>", "").Replace("amp;", "");

                ResultBoletoRegistroOnLine ObjResult = JsonConvert.DeserializeObject<ResultBoletoRegistroOnLine>(resultJSON);
                //==================================

                //Utility.SetLogXML("SetGravarRegistroBoletoBradesco", "Error", ObjResult.cdErro + "-" + ObjResult.msgErro + "-" + ObjResult.nuTituloGerado, false, 1);

                return ObjResult.cdErro + "," + ObjResult.msgErro.Replace(","," ") + "," + ObjResult.nuTituloGerado ;
                

            }
            catch (Exception ex)
            {
                Utility.SetLogXML("SetGravarRegistroBoletoBradesco", "Error", ex.Message, false,1);
                return null;
            }

        }



        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Metodos para atender a Loja de Terceiros (JC)
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //GetCampanhaListJSON já existe
        public string GetCargaFormaPagamentoListJSON()
        {
            try
            {

                FormaPagamentoBusiness ObjBusiness = new FormaPagamentoBusiness();
                string lstrRet = ObjBusiness.GetFormaPagamentoListService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetCargaTipoPagamentoListJSON()
        {
            try
            {

                TipoPagamentoBusiness ObjBusiness = new TipoPagamentoBusiness();
                string lstrRet = ObjBusiness.GetTipoPagamentoListService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetCargaTipoEntregaListJSON()
        {
            try
            {

                TipoEntregaBusiness ObjBusiness = new TipoEntregaBusiness();
                string lstrRet = ObjBusiness.GetTipoEntregaListService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetCargaTipoAssinaturaListJSON()
        {
            try
            {

                TipoAssinaturaBusiness ObjBusiness = new TipoAssinaturaBusiness();
                string lstrRet = ObjBusiness.GetTipoAssinaturaListService();

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetCampanhaPlanoListJSON(int pintIDCampanha, int pintIDCanal)
        {
            try
            {

                CampanhaPlanoBusiness ObjBusiness = new CampanhaPlanoBusiness();
                string lstrRet = ObjBusiness.GetCampanhaPlanoListService(pintIDCampanha, pintIDCanal);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetCampanhaPrecoListJSON(int pintIDCampanha, int pintIDPlano)
        {
            try
            {

                CampanhaPrecoBusiness ObjBusiness = new CampanhaPrecoBusiness();
                string lstrRet = ObjBusiness.GetCampanhaPrecoListService(pintIDCampanha, pintIDPlano);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetPlanoComercialJSON(int pintIDPlano)
        {
            try
            {

                PlanoComercialBusiness ObjBusiness = new PlanoComercialBusiness();

                string lstrRetJSON = ObjBusiness.GetPlanoComercialService(pintIDPlano);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetTipoAssinaturaEntregaJSON(int pintTipoEntrega, int pintTipoAssinatura)
        {
            try
            {

                TipoAssinaturaEntregaBusiness ObjBusiness = new TipoAssinaturaEntregaBusiness();

                string lstrRetJSON = ObjBusiness.GetTipoAssinaturaEntregaService(pintTipoEntrega, pintTipoAssinatura);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetSincronizacaoCampanhaPlanoPrecoFULL(int pintIDCamp = 0)
        {
            try
            {
                string lstrJSON = "";

                CampanhaPlanoPrecoBusiness ObjBusiness = new CampanhaPlanoPrecoBusiness();
                lstrJSON = ObjBusiness.GetCampanhaPlanoPreco(pintIDCamp);

                string lstrRet = "";

                if (ObjBusiness.Erro == 0)
                {
                    lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro + ";" + lstrJSON;
                }
                else
                {
                    lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                }

                return lstrRet;

            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetSincronizacaoCampanhaPlanoTerceiro(int pintIDCamp, int pintIDPlano)
        {
            try
            {
                string lstrJSON = "";

                CampanhaPlanoPrecoBusiness ObjBusiness = new CampanhaPlanoPrecoBusiness();
                lstrJSON = ObjBusiness.GetCampanhaPlanoPreco(pintIDCamp, pintIDPlano);

                string lstrRet = "";

                if (ObjBusiness.Erro == 0)
                {
                    lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro + ";" + lstrJSON;
                }
                else
                {
                    lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                }

                return lstrRet;
            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message;

                return _erro.ToString() + "," + _msgErro;
            }
        }
        public string GetProdutoJSON(int pintIDProduto)
        {
            try
            {

                ProdutoBusiness ObjBusiness = new ProdutoBusiness();

                string lstrRetJSON = ObjBusiness.GetProdutoListService(pintIDProduto);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }

        //FASE 2 LOJA JC

        public string GetCampanhaEntJSON(int pintIDCamp)
        {
            try
            {

                CampanhaBusiness ObjBusiness = new CampanhaBusiness();
                string lstrRet = ObjBusiness.GetCampanhaEntService(pintIDCamp);

                if (ObjBusiness.Erro == 0)
                {
                    return lstrRet;
                }
                else
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;
                    return null;
                }

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetTipoPessoaJSON()
        {
            try
            {

                TipoPessoaEntity ObjEnt;
                List<TipoPessoaEntity> lcolEnt = new List<TipoPessoaEntity>();

                ObjEnt = new TipoPessoaEntity();
                ObjEnt.ID_TIPO_PESSOA = 1;
                ObjEnt.DS_TIPO_PESSOA = "Fisica";

                lcolEnt.Add(ObjEnt);

                ObjEnt = new TipoPessoaEntity();
                ObjEnt.ID_TIPO_PESSOA = 2;
                ObjEnt.DS_TIPO_PESSOA = "Juridica";

                lcolEnt.Add(ObjEnt);

                return JsonConvert.SerializeObject(lcolEnt);


            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetSexoPessoaJSON()
        {
            try
            {
                SexoBusiness ObjBll = new SexoBusiness();
                List<SexoEntity> lcolEnt = new List<SexoEntity>();

                lcolEnt = ObjBll.GetSexoList();

                return JsonConvert.SerializeObject(lcolEnt);

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetTipoPessoaJuridicaJSON()
        {
            try
            {
                TipoJuridicoBusiness ObjBll = new TipoJuridicoBusiness();
                List<TipoJuridicoEntity> lcolEnt = new List<TipoJuridicoEntity>();

                lcolEnt = ObjBll.GetTipoJuridicoList();

                return JsonConvert.SerializeObject(lcolEnt);

            }
            catch (Exception ex)
            {

                _erro = 1;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetBancoBoletoJSON(int pintIDProduto)
        {
            try
            {

                BancoBoletoBusiness ObjBusiness = new BancoBoletoBusiness();

                string lstrRetJSON = ObjBusiness.GetBancoBoletoListService(pintIDProduto);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }


        //FASE 3 LOJA JC - PRODUTO AGREGADO

        public string GetProdutoAgregadoJSON()
        {
            try
            {

                ProdutoBusiness ObjBusiness = new ProdutoBusiness();

                string lstrRetJSON = ObjBusiness.GetProdutoAgregadListService();

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetProdutoAgregadoIDJSON(int pintIDProduto, int pintIDItem)
        {
            try
            {

                ProdutoBusiness ObjBusiness = new ProdutoBusiness();

                string lstrRetJSON = ObjBusiness.GetProdutoAgregadListService(pintIDProduto, pintIDItem);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetItemProdutoJSON(int pintIDProduto)
        {
            try
            {

                ItemProdutoBusiness ObjBusiness = new ItemProdutoBusiness();

                string lstrRetJSON = ObjBusiness.GetItemProdutoAgregadoListService(pintIDProduto);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetItemProdutoIDJSON(int pintIDProduto, int pintIDItem)
        {
            try
            {

                ItemProdutoBusiness ObjBusiness = new ItemProdutoBusiness();

                string lstrRetJSON = ObjBusiness.GetItemProdutoAgregadoListService(pintIDProduto, pintIDItem);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetPrecoProdutoAgregadoJSON(int pintIDProduto, int pintItem)
        {
            try
            {

                PrecoProdutoAgregadoBusiness ObjBusiness = new PrecoProdutoAgregadoBusiness();

                string lstrRetJSON = ObjBusiness.GetPrecoProdutoAgregadoListService(pintIDProduto, pintItem);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetFormaPagamentoJSON(int pintIDFormaPag)
        {
            try
            {

                FormaPagamentoBusiness ObjBusiness = new FormaPagamentoBusiness();

                string lstrRetJSON = ObjBusiness.GetFormaPagamentoEntService(pintIDFormaPag);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetTipoPagamentoJSON(int pintIDTipoPagto)
        {
            try
            {

                TipoPagamentoBusiness ObjBusiness = new TipoPagamentoBusiness();

                string lstrRetJSON = ObjBusiness.GetTipoPagamentoEntService(pintIDTipoPagto);

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetSincronizacaoProdutoAgregadoFULL()
        {
            try
            {
                string lstrJSON = "";

                PrecoProdutoAgregadoBusiness ObjBusiness = new PrecoProdutoAgregadoBusiness();
                lstrJSON = ObjBusiness.GetPrecoProdutoAgregado();

                string lstrRet = "";

                if (ObjBusiness.Erro == 0)
                {
                    lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro + ";" + lstrJSON;
                }
                else
                {
                    lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                }

                return lstrRet;

            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetSincronizacaoProdutoAgregadoTerceiro(int pintIDProduto, int pintIDItem)
        {
            try
            {
                string lstrJSON = "";

                PrecoProdutoAgregadoBusiness ObjBusiness = new PrecoProdutoAgregadoBusiness();
                lstrJSON = ObjBusiness.GetPrecoProdutoAgregado(pintIDProduto, pintIDItem);

                string lstrRet = "";

                if (ObjBusiness.Erro == 0)
                {
                    lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro + ";" + lstrJSON;
                }
                else
                {
                    lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                }

                return lstrRet;

            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GetFormaExpedicaoProdutoAgregadoJSON()
        {
            try
            {

                FormaExpedicaoBusines ObjBusiness = new FormaExpedicaoBusines();

                string lstrRetJSON = ObjBusiness.GetFormaExpedicaoListService();

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string GravarPedidoIntegracao(string pstrDadosPS,
                                             string pstrDadosTEL,
                                             string pstrDadosENDER,
                                             string pstrDadosPED,
                                             string pstrDadosITEM,
                                             string pstrDadosDEB,
                                             string pstrDadosGateway,
                                             Int32 pintEditora,
                                             Int32 pintUsuario)
        {

            try
            {
                GravaProdutoAgregBusiness ObjBll = new GravaProdutoAgregBusiness();

                string lstrRet = ObjBll.GravarPedidoIntegracao(pstrDadosPS,
                                                               pstrDadosTEL,
                                                               pstrDadosENDER,
                                                               pstrDadosPED,
                                                               pstrDadosITEM,
                                                               pstrDadosDEB,
                                                               pstrDadosGateway,
                                                               pintEditora,
                                                               pintUsuario);
                return lstrRet;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return "";
            }
        }
        public string GetValidarLoginCentral(string pstruser, string pstrpwd)
        {
            try
            {

                string lstrpwdCript = CriptLogin.Encriptar(pstrpwd);

                UsuarioLoginBusiness ObjBLL = new UsuarioLoginBusiness();

                string lstrRet = ObjBLL.GetLoginUsuarioCentralJSON(pstruser, lstrpwdCript);

                return lstrRet;
                
            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", ex.Message, "");
                
            }
        }       
        public string GetReadTextFile(string pstrPathFile)
        {

            try
            {

                if (File.Exists(pstrPathFile))
                {
                    string lstrText = "";

                    using (StreamReader streamReader = new StreamReader(pstrPathFile, Encoding.UTF8))
                    {
                        lstrText = streamReader.ReadToEnd();
                    }

                    return lstrText;
                }
                else
                {
                    return "Arquivo não existe no local informado";
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                return ex.Message;                
            }
        }
        public string SetSincronizacaoCampanhaPlanoPrecoCorreio(string pstrURLPost, string pstrVetorCampPlano)
        {
            try
            {
                _erro = 0;
                _msgErro = "";


                //Validações e geração do vetor que vai ser convertido em JSON e ser enviado no Body da mensagem.
                if (pstrURLPost.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "Url do serviço não pode vazia.";
                    return _erro.ToString() + "," + _msgErro;
                }

                if (pstrVetorCampPlano.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "lista de campanha plano não pode ser vazia.";
                    return _erro.ToString() + "," + _msgErro;
                }

                string[] lstrVertor = pstrVetorCampPlano.Split(',');

                if ( (lstrVertor == null) || (lstrVertor.Length == 0))
                {
                    _erro = -1;
                    _msgErro = "lista de campanha plano não contém informações.";
                    return _erro.ToString() + "," + _msgErro;
                }

                CampanhaPlanoIntegracaoEntity ObjCampPlano;
                List<CampanhaPlanoIntegracaoEntity> ObjListCampPlano = new List<CampanhaPlanoIntegracaoEntity>();

                foreach (string lstrItem in lstrVertor)
                {
                    string[] lstrVetItem = lstrItem.Split('-');

                    if ((lstrVetItem != null) && (lstrVetItem.Length != 0))
                    {
                        ObjCampPlano = new CampanhaPlanoIntegracaoEntity();

                        ObjCampPlano.CD_CAMPANHA = int.Parse(lstrVetItem[0]);
                        ObjCampPlano.CD_PLANO = int.Parse(lstrVetItem[1]);
                        ObjCampPlano.ST_ACAO = int.Parse(lstrVetItem[2]);

                        ObjListCampPlano.Add(ObjCampPlano);
                    }

                }

                if ((ObjListCampPlano == null) || (ObjListCampPlano.Count == 0))
                {
                    _erro = -1;
                    _msgErro = "lista de campanha plano não contém informações para gerar o JSON.";
                    return _erro.ToString() + "," + _msgErro;
                }

                string lstrMenssage = JsonConvert.SerializeObject(ObjListCampPlano, Newtonsoft.Json.Formatting.None);

                lstrMenssage = "{\"message\":"  + lstrMenssage + "}";

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrURLPost);

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = lstrMenssage;
              
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                ResultCampPlanoCWorks ObjResultCW = new ResultCampPlanoCWorks();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    if (result.Trim() != "")
                    {
                        ObjResultCW = JsonConvert.DeserializeObject<ResultCampPlanoCWorks>(result);
                    }
                }

                if (ObjResultCW != null)
                {

                    if (ObjResultCW.message.Trim() == "Messagem enviada")
                    {
                        _erro = 0;
                        _msgErro = ObjResultCW.messageId;

                        return _erro.ToString() + "," + _msgErro;
                    }
                    else
                    {
                        _erro = -1;
                        _msgErro = ObjResultCW.message + "-" + ObjResultCW.messageId;

                        return _erro.ToString() + "," + _msgErro;
                    }
                    
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Mensagem do WS do parceiro não enviada.";

                    return _erro.ToString() + "," + _msgErro;
                }
               
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return _erro.ToString() + "," + _msgErro;
            }

        }
        public string GetCargaWebCorreioJSON(int pintTipo, int pintProduto, int pintCampanha, int pintCortesia)
        {
            try
            {
                //+++++++++++++++++++++++++++++++++++
                //pintTipo
                //+++++++++++++++++++++++++++++++++++
                // 1 - Carga Assinante
                // 2 - Carga Corporate
                //+++++++++++++++++++++++++++++++++++

                string lstrJSON = "";

                CargaWebCorreioBusiness ObjBusiness = new CargaWebCorreioBusiness();

                if (pintTipo == 1)
                {
                    lstrJSON = ObjBusiness.GetCargaWebCorreioService(pintProduto, pintCampanha, pintCortesia);
                }
                else
                {
                    lstrJSON = ObjBusiness.GetCargaWebCorpCorreioService(pintProduto, pintCampanha, pintCortesia);
                }                

                string lstrRet = "";

                if (ObjBusiness.Erro == 0)
                {
                    lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro + ";" + lstrJSON;
                }
                else
                {
                    lstrRet = ObjBusiness.Erro.ToString() + ";" + ObjBusiness.MsgErro;
                }

                return lstrRet;
            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message;

                return _erro.ToString() + "," + _msgErro;
            }
        }
        public string SetSincronizacaoCampanhaPlanoPrecoJC(string pstrURLPost, string pstrToken, string pstrVetorCampPlano)
        {
            try
            {
                _erro = 0;
                _msgErro = "";


                if (pstrURLPost.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "Url do serviço não pode vazia.";
                    return _erro.ToString() + "," + _msgErro;
                }

                if (pstrToken.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "Token inválido.";
                    return _erro.ToString() + "," + _msgErro;
                }


                if (pstrVetorCampPlano.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "lista de campanha plano não pode ser vazia.";
                    return _erro.ToString() + "," + _msgErro;
                }

                string[] lstrVertor = pstrVetorCampPlano.Split(',');

                if ((lstrVertor == null) || (lstrVertor.Length == 0))
                {
                    _erro = -1;
                    _msgErro = "lista de campanha plano não contém informações.";
                    return _erro.ToString() + "," + _msgErro;
                }

                string lstrErrorList = "";

                foreach (string lstritem in lstrVertor)
                {
                    string lstrMenssage = lstritem.Trim();                    

                    if (lstrMenssage.Trim() != "")
                    {
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrURLPost + lstrMenssage);

                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        httpWebRequest.Headers.Add("Authorization", pstrToken);

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                        ResultCampPlanoJC ObjResultJC = new ResultCampPlanoJC();

                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();

                            if (result.Trim() != "")
                            {
                                ObjResultJC = JsonConvert.DeserializeObject<ResultCampPlanoJC>(result);
                            }
                        }

                        lstrErrorList = lstrErrorList + lstrMenssage + "-" + ObjResultJC.message + ",";
                    }
                }

                return lstrErrorList;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return _erro.ToString() + "," + _msgErro;
            }

        }
        public string GetReadCerticado(string pstrFileName)
        {
            string lstrPath = "";

            try
            {
                if (pstrFileName.Trim() == "")
                {
                    return "Nome do arquivo não informado";
                }

                string lstPath2 = AppDomain.CurrentDomain.BaseDirectory + @"\Certificado\" + pstrFileName;
                string lstrPathServer = OperationContext.Current.Channel.LocalAddress.Uri.AbsoluteUri;

                //if (File.Exists(lstrPathServer))
                //{

                  lstrPath = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Certificado\" + pstrFileName;

                  X509Certificate2 cert = new X509Certificate2(lstrPath, "2104", X509KeyStorageFlags.DefaultKeySet);

                  return "OK";
                    
                //}
                //else
                //{
                //    return "Arquivo não existe no local informado";
                //}

            }
            catch (Exception ex)
            {
                _erro = -99;
                return ex.Message;
            }
        }
        public string SetGravaLogInterfaceWEB(int pintIDLog,
                                            string pstrDadosPessoa,
                                            string pstrDadosEntrega,
                                            string pstrDadosPagamento,
                                            string pstrDadosPlano,
                                            string pstrChamador,
                                            string pstrURL,
                                            string pstrMetodo,
                                            string pstrSerieCTR,
                                            int pintNuCTR,
                                            int pintDvCTR,
                                            bool pbolGetway,
                                            string pstrObs,
                                            string pstrErro)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                DadosPessoaEntity ObjEntPS = new DadosPessoaEntity();
                ObjEntPS = (DadosPessoaEntity)JsonConvert.DeserializeObject(pstrDadosPessoa, ObjEntPS.GetType());

                DadosEntregaEntity ObjEntDE = new DadosEntregaEntity();
                ObjEntDE = (DadosEntregaEntity)JsonConvert.DeserializeObject(pstrDadosEntrega, ObjEntDE.GetType());

                DadosPagamentoEntity ObjEntPG = new DadosPagamentoEntity();
                ObjEntPG = (DadosPagamentoEntity)JsonConvert.DeserializeObject(pstrDadosPagamento, ObjEntPG.GetType());

                PlanoComercialWebEntity ObjPlano = new PlanoComercialWebEntity();
                ObjPlano = (PlanoComercialWebEntity)JsonConvert.DeserializeObject(pstrDadosPlano, ObjPlano.GetType());

                LogInterfaceWEBBusiness ObjBLL = new LogInterfaceWEBBusiness();

                ObjBLL.SetLogInterface(0, ObjEntPS, ObjEntDE, ObjEntPG, ObjPlano, null,pstrChamador, pstrURL, pstrMetodo, pstrSerieCTR, pintNuCTR, pintDvCTR, pbolGetway, "", "");

                if (ObjBLL.Erro != 0)
                {
                    _erro = -99;
                    _msgErro = ObjBLL.MsgErro;

                    Utility.SetLogXML("WS-SetGravaLogInterfaceWEB", "Error", ObjBLL.MsgErro, false, 1);
                }
                else
                {
                    _erro = 0;
                    _msgErro = "";
                }

                return _erro.ToString() + "-" + _msgErro;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message + " - " + ex.InnerException;

                Utility.SetLogXML("WS-SetGravaLogInterfaceWEB", "Error", _msgErro, false, 1);

                return _erro.ToString() + "-" + _msgErro;
            }
        }
        public string GetAbandonoMKTJSON()
        {
            try
            {

                AbandonoBusiness ObjBusiness = new AbandonoBusiness();

                string lstrRetJSON = ObjBusiness.GetAbandonoMKTService();

                if (ObjBusiness.Erro != 0)
                {
                    _erro = ObjBusiness.Erro;
                    _msgErro = ObjBusiness.MsgErro;

                    return null;
                }
                else
                {
                    return lstrRetJSON;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
        public string SetAbandonoMKT(int pintIDLog)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                AbandonoBusiness ObjBusiness = new AbandonoBusiness();

                ObjBusiness.SetAbandonoMKT(pintIDLog);


                if (ObjBusiness.Erro != 0)
                {
                    _erro = -99;
                    _msgErro = ObjBusiness.MsgErro;

                    //Utility.SetLogXML("WS-SetAbandonoMKT", "Error", ObjBusiness.MsgErro, false, 1);
                }
                else
                {
                    _erro = 0;
                    _msgErro = "";
                }

                return _erro.ToString() + "-" + _msgErro;

            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message + " - " + ex.InnerException;

                //Utility.SetLogXML("WS-SetAbandonoMKT", "Error", _msgErro, false, 1);

                return _erro.ToString() + "-" + _msgErro;
            }
        }
        public string GetLoginSSOJC()
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("");

                httpWebRequest.ContentType = ""; // "application/json";
                httpWebRequest.Method = "";          //  "POST";
                httpWebRequest.Headers.Add("MerchantId", "");
                httpWebRequest.Headers.Add("MerchantKey", "");
                httpWebRequest.Headers.Add("RequestId", "");

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string[] lstrRetorno = null;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    ResultVendaBRASPAG ObjResult = JsonConvert.DeserializeObject<ResultVendaBRASPAG>(result);

                    if (ObjResult != null)
                    {
                        //lstrRetorno = TratarRetornoVendaSimplesBRASPAG(ObjResult);
                    }

                }


                return "";
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }
        public string GetStatusConexaoBD()
        {
            try
            {

                InfraestruturaBusiness ObjInfra = new InfraestruturaBusiness();
                ObjInfra.GetStatusConexaoBD();

                return Utility.GetRetornoJSON(ObjInfra.Erro.ToString(), ObjInfra.MsgErro, "");

            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", _msgErro, "");

            }
        }
        public string GetLerGrupoAfinidadeCentralJSON()
        {
            throw new NotImplementedException();
        }
        public string GetProximoCTRJSON()
        {
            try
            {
                GravaAssinaturaBusiness  ObjBLL = new GravaAssinaturaBusiness();

                string lstrRet = ObjBLL.GetProximoCTRJSON();

                if (ObjBLL.Erro == 0)
                {
                    if (lstrRet != "")
                    {
                        return Utility.GetRetornoJSON("0", "Sucesso", lstrRet);
                    }
                    else
                    {
                        lstrRet = "Erro ao pegar próximo Contrato de Assinatura (WEB) - vazio";
                        return Utility.GetRetornoJSON("0", lstrRet, "");
                    }
                }
                else
                {
                    lstrRet = "Erro ao pegar próximo Contrato de Assinatura (WEB)";
                    return Utility.GetRetornoJSON("0", lstrRet, "");
                }
             

            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", ex.Message, "");

            }
        }

        public string SetIntegracaoLahar(string pstrToken,
                                         string pstrUrlPost,
                                         string pstrMethod,
                                         string pstrContentType,
                                         string pstrNomeFormulario,
                                         string pstrDados,
                                         string pstrEmpresa)
        {
            try
            {
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //ATENÇÃO
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //pstrDados => deve chegar neste metodo conforme abaixo.
                //nome_contato|VINICIUS NUNES-email_contato|vinicius_tst1@cwa.com.br";
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                string lstrDadosPost = "";
                string[] lstrVetDados = pstrDados.Split('#');
                

                //pstrToken = "jornalda9G9rrlJn1W4N8Wu6e6vdog6mmfeDDI5QuJW8VPxqCl0vUuWL1oM1yVjF";
                //pstrUrlPost = "https://scripts.lahar.com.br/api/curl_server.php?";

                //pstrMethod = "GET";
                //pstrContentType = "text/xml";

                if (pstrEmpresa.Trim().ToUpper() == "JORNAL DA CIDADE")
                {

                    //lstrDadosPost = "jsoncallback=redireciona&token_api_lahar=" + pstrToken + "&nome_formulario=" + pstrNomeFormulario +"&url_origem=app_jc&tipo_integracao=conversions&nome_contato=VINICIUS NUNES&email_contato=vinicius_tst1@cwa.com.br";

                    lstrDadosPost = "jsoncallback=redireciona&token_api_lahar=" + pstrToken +
                                   "&nome_formulario=" + pstrNomeFormulario +
                                   "&url_origem=app_jc&tipo_integracao=conversions";

                    foreach (string strItem in lstrVetDados)
                    {
                        if (strItem.Trim() != "")
                        {
                            string[] lsVetItem = strItem.Split('|');

                            lstrDadosPost = lstrDadosPost + "&" + lsVetItem[0] + "=" + lsVetItem[1];
                        }
                    }
                }
                

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrUrlPost + lstrDadosPost);
                httpWebRequest.ContentType = pstrContentType; // "application/json";
                httpWebRequest.Method = pstrMethod;          //  "POST";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                ResultIntegracaoLahar ObjResult = new ResultIntegracaoLahar();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    if (result != "")
                    {
                        ObjResult = JsonConvert.DeserializeObject<ResultIntegracaoLahar>(result);
                    }
                }

                if (ObjResult.status.Trim() == "sucesso")
                {
                    return "0|OK";
                }
                else
                {
                    return "-10|" + ObjResult.data.error.message;
                }

                
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return _erro.ToString() + "|" + _msgErro;
            }
        }

        public string GetLoginValidoLondrina(string pstrToken)
        {
            string lstrJSON = "";
            ResultLoginValidoLondrina ObjRet;

            try
            {
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //Valida Preenchimento do Token
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                if (string.IsNullOrEmpty(pstrToken))
                {
                    ObjRet = new ResultLoginValidoLondrina();

                    ObjRet.status = "-1";
                    ObjRet.mensagem = "O token não pode ser nulo";
                    ObjRet.acesso_central = "";
                    ObjRet.acesso_jornal = "";
                    ObjRet.token = "";

                    lstrJSON = JsonConvert.SerializeObject(ObjRet, Newtonsoft.Json.Formatting.None);

                    return lstrJSON;
                }

                if (pstrToken.Trim() == "")
                {
                    ObjRet = new ResultLoginValidoLondrina();

                    ObjRet.status = "-1";
                    ObjRet.mensagem = "O token não pode ser vazio";
                    ObjRet.acesso_central = "";
                    ObjRet.acesso_jornal = "";
                    ObjRet.token = "";

                    lstrJSON = JsonConvert.SerializeObject(ObjRet, Newtonsoft.Json.Formatting.None);

                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //Verifica token no EndPoint do Cliente
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                string pstrUrlPost = ConfigurationManager.AppSettings["UrlPostLoginIntegrado"];

                if (pstrUrlPost.Trim() == "")
                {
                    _erro = -99;
                    _msgErro = "Url de Integração não configurada";

                    ObjRet = new ResultLoginValidoLondrina();

                    ObjRet.status = "-1";
                    ObjRet.mensagem = _msgErro;
                    ObjRet.acesso_central = "";
                    ObjRet.acesso_jornal = "";
                    ObjRet.token = "";

                    lstrJSON = JsonConvert.SerializeObject(ObjRet, Newtonsoft.Json.Formatting.None);

                    return lstrJSON;
                }

                string pstrContentType = ConfigurationManager.AppSettings["ContentTypeLogin"];

                if (pstrUrlPost.Trim() == "")
                {
                    _erro = -99;
                    _msgErro = "ContentType de Integração não configurado";

                    ObjRet = new ResultLoginValidoLondrina();

                    ObjRet.status = "-1";
                    ObjRet.mensagem = _msgErro;
                    ObjRet.acesso_central = "";
                    ObjRet.acesso_jornal = "";
                    ObjRet.token = "";

                    lstrJSON = JsonConvert.SerializeObject(ObjRet, Newtonsoft.Json.Formatting.None);

                    return lstrJSON;
                }

                string pstrMethod = ConfigurationManager.AppSettings["MethodLogin"];

                if (pstrMethod.Trim() == "")
                {
                    _erro = -99;
                    _msgErro = "Method de Integração não configurado";

                    ObjRet = new ResultLoginValidoLondrina();

                    ObjRet.status = "-1";
                    ObjRet.mensagem = _msgErro;
                    ObjRet.acesso_central = "";
                    ObjRet.acesso_jornal = "";
                    ObjRet.token = "";

                    lstrJSON = JsonConvert.SerializeObject(ObjRet, Newtonsoft.Json.Formatting.None);

                    return lstrJSON;
                }
                //AQUI VAI ENTRAR A CHAMADA DO ENDPOINT da MW


                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pstrUrlPost + pstrToken);
                httpWebRequest.ContentType = pstrContentType;  //"application/json";
                httpWebRequest.Method = pstrMethod;  //"GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                ResultUserMW ObjUserMW = new ResultUserMW();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    if (result != "")
                    {
                        ObjUserMW = JsonConvert.DeserializeObject<ResultUserMW>(result);
                    }

                }

                if (ObjUserMW == null)
                {
                    _erro = -99;
                    _msgErro = "Objeto User da MW retornou NULL";

                    ObjRet = new ResultLoginValidoLondrina();

                    ObjRet.status = "-1";
                    ObjRet.mensagem = _msgErro;
                    ObjRet.acesso_central = "";
                    ObjRet.acesso_jornal = "";
                    ObjRet.token = "";

                    lstrJSON = JsonConvert.SerializeObject(ObjRet, Newtonsoft.Json.Formatting.None);

                    return lstrJSON;
                }


                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //Chama a regra de Login da Folha 
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                UsuarioLoginBusiness ObjLoginBLL = new UsuarioLoginBusiness();

                string lstrRetRegraAcesso = ObjLoginBLL.GetLoginUsuarioCentralFolha(ObjUserMW.cpfCnpj, ObjUserMW.email);
                string[] VetRetRegra = lstrRetRegraAcesso.Split('|');

                ObjRet = new ResultLoginValidoLondrina();

                ObjRet.status = "0";
                ObjRet.mensagem = "";
                ObjRet.acesso_central = VetRetRegra[0];
                ObjRet.acesso_jornal = VetRetRegra[1];
                ObjRet.token = VetRetRegra[2];

                lstrJSON = JsonConvert.SerializeObject(ObjRet, Newtonsoft.Json.Formatting.None);

                return lstrJSON;
            }
            catch (Exception ex)
            {
                ObjRet = new ResultLoginValidoLondrina();

                ObjRet.status = "-1";
                ObjRet.mensagem = ex.Message + " - " + ex.InnerException;
                ObjRet.acesso_central = "";
                ObjRet.acesso_jornal = "";
                ObjRet.token = "";

                lstrJSON = JsonConvert.SerializeObject(ObjRet, Newtonsoft.Json.Formatting.None);

                return lstrJSON;

            }
        }
    }
}
