using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.Configuration;

namespace CWA.EngineServices
{
    public class ServiceClientCWA
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


        public static CWAService.ServiceCWAClient GetServiceClintEndPoint(string pstrTipoEndPoint = "", 
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

                if (lstrEndPoint.Trim() == "" )
                {
                    _erro = -1;
                    _msgErro = "EndPoint WSDL não parametrizado.";
                    return null;
                }


                CWAService.ServiceCWAClient client;
                EndpointAddress endpoint = new EndpointAddress(new Uri(lstrEndPoint));

                if (pintTipoConexao == 0)
                {
                    BasicHttpsBinding binding = new BasicHttpsBinding();
                    binding.Security.Mode = BasicHttpsSecurityMode.Transport;
                    client = new CWAService.ServiceCWAClient(binding, endpoint);
                }
                else
                {
                    BasicHttpBinding binding = new BasicHttpBinding();
                    binding.Security.Mode = BasicHttpSecurityMode.None;
                    client = new CWAService.ServiceCWAClient(binding, endpoint);
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
