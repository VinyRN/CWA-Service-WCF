using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.Configuration;

namespace CWA.Util
{
    public class ServiceClientCWAAPIMail
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


        public static CWAMailAPIService.ServiceCWAClient GetServiceClintEndPoint(string pstrTipoEndPoint = "",
                                                                                 string pstrEnderecoEndPoint = "")
        {
            try
            {
                //Tipo de Conexao ==> pintTipoConexao
                //=============================================
                //0 = HTTPS
                //1 = HTTP
                int pintTipoConexao;

                string lstrEndPoint = "";
                string lstrTipoEndPoint = "";

                //Caso venha no parametro o endereço será utilizado.
                //====================================================================================
                if ((pstrTipoEndPoint.Trim() == "") && (pstrEnderecoEndPoint.Trim() == ""))
                {
                    lstrEndPoint = ConfigurationManager.AppSettings["EndPointWSDL"];
                    lstrTipoEndPoint = ConfigurationManager.AppSettings["TipoEndPointWSDL"];
                }
                else
                {
                    lstrEndPoint = pstrEnderecoEndPoint;
                    lstrTipoEndPoint = pstrTipoEndPoint;
                }
                //====================================================================================

                if (lstrTipoEndPoint.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "Tipo de conexão do EndPoint WSDL não parametrizado.";
                    return null;
                }
                else
                {
                    if (lstrTipoEndPoint.Trim() == "https")
                    {
                        pintTipoConexao = 0;
                    }
                    else
                    {
                        pintTipoConexao = 1;
                    }

                }

                if (lstrEndPoint.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "EndPoint WSDL não parametrizado.";
                    return null;
                }


                CWAMailAPIService.ServiceCWAClient client;
                EndpointAddress endpoint = new EndpointAddress(new Uri(lstrEndPoint));


                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                if (pintTipoConexao == 0)
                {
                    BasicHttpsBinding binding = new BasicHttpsBinding();
                    binding.Security.Mode = BasicHttpsSecurityMode.Transport;

                    binding.OpenTimeout = new TimeSpan(1, 0, 0);
                    binding.CloseTimeout = new TimeSpan(12, 0, 0);
                    binding.SendTimeout = new TimeSpan(1, 0, 0);
                    binding.ReceiveTimeout = new TimeSpan(12, 0, 0);
                    binding.MaxReceivedMessageSize = 2147483647;
                    binding.MaxBufferSize = 2147483647;

                    client = new CWAMailAPIService.ServiceCWAClient(binding, endpoint);
                }
                else
                {
                    BasicHttpBinding binding = new BasicHttpBinding();
                    binding.Security.Mode = BasicHttpSecurityMode.None;

                    binding.OpenTimeout = new TimeSpan(1, 0, 0);
                    binding.CloseTimeout = new TimeSpan(12, 0, 0);
                    binding.SendTimeout = new TimeSpan(1, 0, 0);
                    binding.ReceiveTimeout = new TimeSpan(12, 0, 0);
                    binding.MaxReceivedMessageSize = 2147483647;
                    binding.MaxBufferSize = 2147483647;

                    client = new CWAMailAPIService.ServiceCWAClient(binding, endpoint);
                }


                return client;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }



        }

    }
}
