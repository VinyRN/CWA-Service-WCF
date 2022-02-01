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
    public class TelefoneAssinanteCentralBusiness
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


        public List<CWA.Central.Domain.BusinessEntities.TelsDoAssinante> GetLerTelefoneCentral(int? CD_CONTABIL_PESSOA)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetLerTelefoneAssinanteCentralJSON((int)CD_CONTABIL_PESSOA);

                if (lstrRet != "")
                {
                    List<CWA.Central.Domain.BusinessEntities.TelsDoAssinante> lcolEnt = new List<CWA.Central.Domain.BusinessEntities.TelsDoAssinante>();

                    lcolEnt = (List<CWA.Central.Domain.BusinessEntities.TelsDoAssinante>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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

        public string GetLerTelefoneCentralService(int CD_CONTABIL_PESSOA)
        {
            try
            {

                TelefoneAssinanteData ObjData = new TelefoneAssinanteData();
                List<TelsDoAssinanteCentralEntity> lcolEnt = new List<TelsDoAssinanteCentralEntity>();

                lcolEnt = ObjData.GetLerTelefoneCentral(CD_CONTABIL_PESSOA);

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

        public void SetIncluirTelefoneCentral(int? CD_CONTABIL_PESSOA,
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

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.SetIncluirTelefoneAssinanteCentralService(CD_CONTABIL_PESSOA, NU_SEQ,
                                                                                      ST_TP_TELEFONE, NU_DDD,
                                                                                      NU_TEL, NU_RAMAL,
                                                                                      DS_OBS, NU_DDI,
                                                                                      pbolAbrirTransacao, pbolFecharConexao);

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
        public string SetIncluirTelefoneCentralService(int? CD_CONTABIL_PESSOA,
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
                TelefoneAssinanteData ObjData = new TelefoneAssinanteData();

                ObjData.SetIncluirTelefoneCentral(CD_CONTABIL_PESSOA, NU_SEQ, ST_TP_TELEFONE,
                                                  NU_DDD, NU_TEL, NU_RAMAL, DS_OBS, NU_DDI,
                                                  pbolAbrirTransacao, pbolFecharConexao);

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


        public void SetAlterarTelefoneCentral(int? CD_CONTABIL_PESSOA,
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

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.SetAlterarTelefoneAssinanteCentral(CD_CONTABIL_PESSOA, NU_SEQ,
                                                                                ST_TP_TELEFONE, NU_DDD,
                                                                                NU_TEL, NU_RAMAL,
                                                                                DS_OBS, NU_DDI,
                                                                                pbolAbrirTransacao, pbolFecharConexao);

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
        public string SetAlterarTelefoneCentralService(int? CD_CONTABIL_PESSOA,
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
                TelefoneAssinanteData ObjData = new TelefoneAssinanteData();

                ObjData.SetAlterarTelefoneCentral((int)CD_CONTABIL_PESSOA, NU_SEQ, ST_TP_TELEFONE,
                                                  NU_DDD, NU_TEL, NU_RAMAL, DS_OBS, NU_DDI,
                                                  pbolAbrirTransacao, pbolFecharConexao);

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
        public void SetDeletarTelefoneCentral(int? CD_CONTABIL_PESSOA,
                                             byte NU_SEQ,
                                             Boolean pbolAbrirTransacao,
                                             Boolean pbolFecharConexao)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.SetDeletarTelefoneAssinanteCentral(CD_CONTABIL_PESSOA, NU_SEQ, pbolAbrirTransacao, pbolFecharConexao);

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
        public string SetDeletarTelefoneCentralService(int? CD_CONTABIL_PESSOA,
                                                        byte NU_SEQ,
                                                        Boolean pbolAbrirTransacao,
                                                        Boolean pbolFecharConexao)
        {
            try
            {
                TelefoneAssinanteData ObjData = new TelefoneAssinanteData();

                ObjData.SetDeletarTelefoneCentral((int)CD_CONTABIL_PESSOA, NU_SEQ, pbolAbrirTransacao, pbolFecharConexao);

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
