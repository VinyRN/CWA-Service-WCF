using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace CWA.Services
{
    internal class CriarChamadaDinamicaWS
    {
        private static System.Int32 _erro;
        private static string _msgErro;

        public static System.Int32 Erro
        {
            get { return _erro; }
        }
        public static string MsgErro
        {
            get { return _msgErro; }
        }


        public string lstrUrlWSclient { get; set; }
        public string lstrMetodoNomeWSClient { get; set; }
        public string lstrSoapSTR { get; set; }
        public string lstrUriSoap { get; set; }
        public string lstrContentType { get; set; }
        public string lstrAccept { get; set; }
        public string lstrMethod { get; set; }
        public string lstrFormatParametro { get; set; }

        public Dictionary<string, string> ldicParametro = new Dictionary<string, string>();

        public XDocument lxmlResultXML;
        public string lstrResultString;

        public CriarChamadaDinamicaWS()
        {

        }

        public CriarChamadaDinamicaWS(string pstrUrlWSclient,
                                      string pstrMetodoNomeWSClient,
                                      string pstrSoapSTR,
                                      string pstrUriSoap,
                                      string pstrContentType,
                                      string pstrAccept,
                                      string pstrMethod,
                                      string pstrFormatParametro)
        {
            lstrUrlWSclient = pstrUrlWSclient;
            lstrMetodoNomeWSClient = pstrMetodoNomeWSClient;
            lstrSoapSTR = pstrSoapSTR;
            lstrUriSoap = pstrUriSoap;
            lstrContentType = pstrContentType;
            lstrAccept = pstrAccept;
            lstrMethod = pstrMethod;
            lstrFormatParametro = pstrFormatParametro;
        }

     
        public void Chama()
        {
            Chama(false);
        }

      public void Chama(bool encode)
        {
            try
            {
                HttpWebRequest ObjHttpReq = (HttpWebRequest)WebRequest.Create(lstrUrlWSclient);
                ObjHttpReq.Headers.Add("SOAPAction", "\"" + lstrUriSoap + "\"");
                ObjHttpReq.ContentType = lstrContentType;
                ObjHttpReq.Accept = lstrAccept;
                ObjHttpReq.Method = lstrMethod;

                using (Stream stm = ObjHttpReq.GetRequestStream())
                {
                    string lstrPostValues = "";

                    foreach (var ObjParam in ldicParametro)
                    {
                        if (encode)
                            lstrPostValues += string.Format(lstrFormatParametro, HttpUtility.UrlEncode(ObjParam.Key), HttpUtility.UrlEncode(ObjParam.Value));
                        else
                            //postValues += string.Format("<{0}>{1}</{0}>", "tem:" + param.Key, param.Value);
                            lstrPostValues += string.Format(lstrFormatParametro, ObjParam.Key, ObjParam.Value);
                    }

                    lstrSoapSTR = string.Format(lstrSoapSTR, lstrMetodoNomeWSClient, lstrPostValues);
                    using (StreamWriter stmw = new StreamWriter(stm))
                    {
                        stmw.Write(lstrSoapSTR);
                    }
                }

                var ObjhttpResponse = (HttpWebResponse)ObjHttpReq.GetResponse();
                using (StreamReader responseReader = new StreamReader(ObjhttpResponse.GetResponseStream()))
                {
                    string lsrtResult = responseReader.ReadToEnd();
                    lxmlResultXML = XDocument.Parse(lsrtResult);
                    lstrResultString = lsrtResult;
                }
            }
            catch(Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;

                lstrResultString = "";
                CWA.Util.Utility.SetLogXML("CriaChamadaDinamicaWS.Chama", "Erro", ex.Message, false);
            }

        }





    }
}