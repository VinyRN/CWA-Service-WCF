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
    public class EmailAssinanteCentralBusiness
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


        public List<CWA.Central.Domain.BusinessEntities.EmailDoAssinante> GetLerEmailDoAssinanteCentral(int? CD_CONTABIL_PESSOA)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetLerEmailDoAssinanteCentralJSON((int)CD_CONTABIL_PESSOA);

                if (lstrRet != "")
                {
                    List<CWA.Central.Domain.BusinessEntities.EmailDoAssinante> lcolEnt = new List<CWA.Central.Domain.BusinessEntities.EmailDoAssinante>();

                    lcolEnt = (List<CWA.Central.Domain.BusinessEntities.EmailDoAssinante>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

                    return lcolEnt;

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

        public string GetLerEmailDoAssinanteCentralService(int CD_CONTABIL_PESSOA)
        {
            try
            {

                EmailAssinanteData ObjData = new EmailAssinanteData();
                List<EmailDoAssinanteCentralEntity> lcolEnt = new List<EmailDoAssinanteCentralEntity>();

                lcolEnt = ObjData.GetLerEmailDoAssinanteCentral(CD_CONTABIL_PESSOA);

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

        public void SetIncluirEmailAssinanteCentral(int? CD_CONTABIL_PESSOA, 
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

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.SetIncluirEmailDoAssinanteCentralService(CD_CONTABIL_PESSOA, 
                                                                                    NU_SEQ, 
                                                                                    DS_EMAIL, 
                                                                                    ST_SITUACAO, 
                                                                                    ST_EMAIL_PRINCIPAL, 
                                                                                    CD_TIPO_EMAIL, 
                                                                                    pbolAbrirTransacao, 
                                                                                    pbolFecharConexao);

                if (lstrRet != "")
                {
                    RetornoJSON ObjRetJSON = new RetornoJSON();

                    ObjRetJSON = (RetornoJSON)JsonConvert.DeserializeObject(lstrRet, ObjRetJSON.GetType());

                    if (ObjRetJSON.CodigoRetorno == "0")
                    {
                        _erro = 0;
                        _msgErro = "";
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
                    _msgErro = "Erro ao retornar JSON vazio";

                    return;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return;
            }

        }
        public string SetIncluirEmailAssinanteCentralService(int? CD_CONTABIL_PESSOA,
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
                EmailAssinanteData ObjData = new EmailAssinanteData();

                ObjData.SetIncluirEmailDoAssinanteCentral(CD_CONTABIL_PESSOA, 
                                                          NU_SEQ, 
                                                          DS_EMAIL, 
                                                          ST_SITUACAO, 
                                                          ST_EMAIL_PRINCIPAL, 
                                                          CD_TIPO_EMAIL,
                                                          pbolAbrirTransacao, 
                                                          pbolFecharConexao);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjData.MsgErro, "");
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


        public void SetAlterarEmailAssinanteCentral(int? CD_CONTABIL_PESSOA,
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

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.SetAlterarEmailDoAssinanteCentralService(CD_CONTABIL_PESSOA,
                                                                                    NU_SEQ,
                                                                                    DS_EMAIL,
                                                                                    ST_SITUACAO,
                                                                                    ST_EMAIL_PRINCIPAL,
                                                                                    CD_TIPO_EMAIL,
                                                                                    pbolAbrirTransacao,
                                                                                    pbolFecharConexao);

                if (lstrRet != "")
                {
                    RetornoJSON ObjRetJSON = new RetornoJSON();

                    ObjRetJSON = (RetornoJSON)JsonConvert.DeserializeObject(lstrRet, ObjRetJSON.GetType());

                    if (ObjRetJSON.CodigoRetorno == "0")
                    {
                        _erro = 0;
                        _msgErro = "";
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
                    _msgErro = "Erro ao retornar JSON vazio";

                    return;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return;
            }

        }
        public string SetAlterarEmailAssinanteCentralService(int? CD_CONTABIL_PESSOA,
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
                EmailAssinanteData ObjData = new EmailAssinanteData();

                ObjData.SetAlterarEmailDoAssinanteCentral(CD_CONTABIL_PESSOA,
                                                          NU_SEQ,
                                                          DS_EMAIL,
                                                          ST_SITUACAO,
                                                          ST_EMAIL_PRINCIPAL,
                                                          CD_TIPO_EMAIL,
                                                          pbolAbrirTransacao,
                                                          pbolFecharConexao);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjData.MsgErro, "");
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
        public void SetDeletarEmailAssinanteCentral(int? CD_CONTABIL_PESSOA,
                                                     byte NU_SEQ,
                                                     Boolean pbolAbrirTransacao,
                                                     Boolean pbolFecharConexao)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.SetDeletarEmailDoAssinanteCentralService(CD_CONTABIL_PESSOA, NU_SEQ, pbolAbrirTransacao, pbolFecharConexao);

                if (lstrRet != "")
                {
                    RetornoJSON ObjRetJSON = new RetornoJSON();

                    ObjRetJSON = (RetornoJSON)JsonConvert.DeserializeObject(lstrRet, ObjRetJSON.GetType());

                    if (ObjRetJSON.CodigoRetorno == "0")
                    {
                        _erro = 0;
                        _msgErro = "";
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
                    _msgErro = "Erro ao retornar JSON vazio";

                    return;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return;
            }

        }
        public string SetDeletarEmailAssinanteCentralService(int? CD_CONTABIL_PESSOA,
                                                            byte NU_SEQ,
                                                            Boolean pbolAbrirTransacao,
                                                            Boolean pbolFecharConexao)
        {
            try
            {
                EmailAssinanteData ObjData = new EmailAssinanteData();

                ObjData.SetDeletarEmailDoAssinanteCentral((int)CD_CONTABIL_PESSOA, NU_SEQ, pbolAbrirTransacao, pbolFecharConexao);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return Utility.GetRetornoJSON("-1", ObjData.MsgErro, "");
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
    }
}
