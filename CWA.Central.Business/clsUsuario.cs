using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CWA.Venda.Data;
using CWA.Central.Entity;
using CWA.Central.Domain.BusinessEntities;
using CWA.Util;
using CWA.EngineServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CWA.Central.Business
{
    public class UsuarioCentralBusiness
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

        public string GetAcessoProcedureService(string pstrDS_EMAIL)
        {
            try
            {
                UsuarioLoginData ObjData = new UsuarioLoginData();
                UsuarioCentralEntity lcolEnt = new UsuarioCentralEntity();

                lcolEnt = ObjData.GetAcessoProcedure(pstrDS_EMAIL);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Formatting.None);
                    return lstrRet;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }
        public CWA.Central.Domain.BusinessEntities.UsuarioProcedure GetAcessoProcedure(string pstrDS_EMAIL)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetAcessoProcedureJSON(pstrDS_EMAIL);

                if (lstrRet != "")
                {
                    CWA.Central.Domain.BusinessEntities.UsuarioProcedure ObjEnt = new CWA.Central.Domain.BusinessEntities.UsuarioProcedure();

                    ObjEnt = (CWA.Central.Domain.BusinessEntities.UsuarioProcedure)JsonConvert.DeserializeObject(lstrRet, ObjEnt.GetType());

                    return ObjEnt;

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

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


        public string GetAcessoSenhaProcedureService(string pstrDS_EMAIL, string pstrDS_SENHA)
        {
            try
            {
                UsuarioLoginData ObjData = new UsuarioLoginData();
                UsuarioCentralEntity lcolEnt = new UsuarioCentralEntity();

                lcolEnt = ObjData.GetAcessoSenhaProcedure(pstrDS_EMAIL, pstrDS_SENHA);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Formatting.None);
                    return lstrRet;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }
        public CWA.Central.Domain.BusinessEntities.UsuarioProcedure GetAcessoSenhaProcedure(string pstrDS_EMAIL, string pstrDS_SENHA)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetAcessoSenhaProcedureJSON(pstrDS_EMAIL, pstrDS_SENHA);

                if (lstrRet != "")
                {
                    CWA.Central.Domain.BusinessEntities.UsuarioProcedure ObjEnt = new CWA.Central.Domain.BusinessEntities.UsuarioProcedure();

                    ObjEnt = (CWA.Central.Domain.BusinessEntities.UsuarioProcedure)JsonConvert.DeserializeObject(lstrRet, ObjEnt.GetType());

                    return ObjEnt;

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

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


        public string GetEsqueciSenhaProcedureService(string pstrDS_EMAIL, string pstrNU_CPF, string pstrNU_CNPJ)
        {
            try
            {
                UsuarioLoginData ObjData = new UsuarioLoginData();
                UsuarioCentralEntity lcolEnt = new UsuarioCentralEntity();

                lcolEnt = ObjData.GetEsqueciSenhaProcedure(pstrDS_EMAIL,pstrNU_CPF,pstrNU_CNPJ);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Formatting.None);
                    return lstrRet;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }

        }
        public CWA.Central.Domain.BusinessEntities.UsuarioProcedure GetEsqueciSenhaProcedure(string pstrDS_EMAIL, string pstrNU_CPF, string pstrNU_CNPJ)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetEsqueciSenhaProcedureJSON(pstrDS_EMAIL, pstrNU_CPF, pstrNU_CNPJ);

                if (lstrRet != "")
                {
                    CWA.Central.Domain.BusinessEntities.UsuarioProcedure ObjEnt = new CWA.Central.Domain.BusinessEntities.UsuarioProcedure();

                    ObjEnt = (CWA.Central.Domain.BusinessEntities.UsuarioProcedure)JsonConvert.DeserializeObject(lstrRet, ObjEnt.GetType());

                    return ObjEnt;

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

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


        public void SetAlterarSenhaProcedure(string pstrDS_EMAIL, string pstrDS_SENHA, string pstrNU_CPF, string pstrNU_CNPJ)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.SetAlterarSenhaProcedureJSON(pstrDS_EMAIL, pstrDS_SENHA, pstrNU_CPF, pstrNU_CNPJ);

                if (lstrRet != "")
                {

                    RetornoJSON ObjRetJSON = new RetornoJSON();
                    ObjRetJSON = (RetornoJSON)JsonConvert.DeserializeObject(lstrRet, ObjRetJSON.GetType());

                    if (ObjRetJSON != null)
                    {
                        if (ObjRetJSON.CodigoRetorno == "OK")
                        {
                            _erro = 0;
                            _msgErro = "OK";

                        }
                        else
                        {

                            _erro = -1;
                            _msgErro = ObjRetJSON.DescricaoRetorno;
                        }
                    }
                    else
                    {
                        _erro = -1;
                        _msgErro = "Erro ao retornar JSON vazio - Objeto NULL";

                    }
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";                    
                }

                return;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return;
            }
        }


        public string GetPesquisaParametroEmailService()
        {
            try
            {
                UsuarioLoginData ObjData = new UsuarioLoginData();
                UsuarioCentralEntity lcolEnt = new UsuarioCentralEntity();

                lcolEnt = ObjData.GetPesquisaParametroEmail();

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Formatting.None);
                    return lstrRet;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }
        public CWA.Central.Domain.BusinessEntities.UsuarioProcedure GetPesquisaParametroEmail()
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetPesquisaParametroEmailJSON(); 

                if (lstrRet != "")
                {
                    CWA.Central.Domain.BusinessEntities.UsuarioProcedure ObjEnt = new CWA.Central.Domain.BusinessEntities.UsuarioProcedure();

                    ObjEnt = (CWA.Central.Domain.BusinessEntities.UsuarioProcedure)JsonConvert.DeserializeObject(lstrRet, ObjEnt.GetType());

                    return ObjEnt;

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

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


        public string GetAcessoIntegradoService(int pintCD_CONTABIL_PESSOA, string pstrNU_SERIE_CTR, int pintNU_CTR, int pintNU_DV_CTR)
        {
            try
            {
                UsuarioLoginData ObjData = new UsuarioLoginData();
                UsuarioCentralEntity lcolEnt = new UsuarioCentralEntity();

                lcolEnt = ObjData.GetAcessoIntegrado(pintCD_CONTABIL_PESSOA, pstrNU_SERIE_CTR, pintNU_CTR, pintNU_DV_CTR);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Formatting.None);
                    return lstrRet;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }
        public CWA.Central.Domain.BusinessEntities.UsuarioProcedure GetAcessoIntegrado(int pintCD_CONTABIL_PESSOA, string pstrNU_SERIE_CTR, int pintNU_CTR, int pintNU_DV_CTR)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetAcessoIntegradoJSON(pintCD_CONTABIL_PESSOA, pstrNU_SERIE_CTR, pintNU_CTR, pintNU_DV_CTR);

                if (lstrRet != "")
                {
                    CWA.Central.Domain.BusinessEntities.UsuarioProcedure ObjEnt = new CWA.Central.Domain.BusinessEntities.UsuarioProcedure();

                    ObjEnt = (CWA.Central.Domain.BusinessEntities.UsuarioProcedure)JsonConvert.DeserializeObject(lstrRet, ObjEnt.GetType());

                    return ObjEnt;

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

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


        public string GetPesquisaParametroGlobalWebService()
        {
            try
            {
                UsuarioLoginData ObjData = new UsuarioLoginData();
                UsuarioCentralEntity lcolEnt = new UsuarioCentralEntity();

                lcolEnt = ObjData.GetPesquisaParametroGlobalWeb();

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Formatting.None);
                    return lstrRet;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }       
        public CWA.Central.Domain.BusinessEntities.UsuarioProcedure GetPesquisaParametroGlobalWeb()
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetPesquisaParametroGlobalWebJSON(); 

                if (lstrRet != "")
                {
                    CWA.Central.Domain.BusinessEntities.UsuarioProcedure ObjEnt = new CWA.Central.Domain.BusinessEntities.UsuarioProcedure();

                    ObjEnt = (CWA.Central.Domain.BusinessEntities.UsuarioProcedure)JsonConvert.DeserializeObject(lstrRet, ObjEnt.GetType());

                    return ObjEnt;

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

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


        public string GetAcessoIntegradoSSOJCService(int pintCD_CONTABIL_PESSOA, string pstrNU_DOC, string pstrDS_EMAIL)
        {
            try
            {
                UsuarioLoginData ObjData = new UsuarioLoginData();
                UsuarioCentralEntity lcolEnt = new UsuarioCentralEntity();

                lcolEnt = ObjData.GetAcessoIntegradoSSOJC(pintCD_CONTABIL_PESSOA,pstrNU_DOC,pstrDS_EMAIL);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Formatting.None);
                    return lstrRet;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }
        public CWA.Central.Domain.BusinessEntities.UsuarioProcedure GetAcessoIntegradoSSOJC(int pintCD_CONTABIL_PESSOA, string pstrNU_DOC, string pstrDS_EMAIL)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetAcessoIntegradoSSOJCJSON(pintCD_CONTABIL_PESSOA, pstrNU_DOC, pstrDS_EMAIL);

                if (lstrRet != "")
                {
                    CWA.Central.Domain.BusinessEntities.UsuarioProcedure ObjEnt = new CWA.Central.Domain.BusinessEntities.UsuarioProcedure();

                    ObjEnt = (CWA.Central.Domain.BusinessEntities.UsuarioProcedure)JsonConvert.DeserializeObject(lstrRet, ObjEnt.GetType());

                    return ObjEnt;

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

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

    }
}
