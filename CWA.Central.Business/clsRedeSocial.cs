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
    public class RedeSocialCentralBusiness
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


        public List<CWA.Central.Domain.BusinessEntities.RedeSocial> GetLerLerRedeSocialCentral(int? CD_CONTABIL_PESSOA)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetLerRedeSocialCentralJSON((int)CD_CONTABIL_PESSOA);

                if (lstrRet != "")
                {
                    List<CWA.Central.Domain.BusinessEntities.RedeSocial> lcolEnt = new List<CWA.Central.Domain.BusinessEntities.RedeSocial>();

                    lcolEnt = (List<CWA.Central.Domain.BusinessEntities.RedeSocial>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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

        public string GeterLerRedeSocialCentralService(int? CD_CONTABIL_PESSOA)
        {
            try
            {

                RedeSocialData ObjData = new RedeSocialData();
                List<RedeSocialCentralEntity> lcolEnt = new List<RedeSocialCentralEntity>();

                lcolEnt = ObjData.GetLerRedeSocialCentral(CD_CONTABIL_PESSOA);

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

        public void SetIncluirRedeSocialCentral(int? CD_CONTABIL_PESSOA,
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

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.SetIncluirRedeSocialCentralService(CD_CONTABIL_PESSOA,
                                                                               NU_SEQ,
                                                                               DS_REDE_SOCIAL,
                                                                               DS_EMAIL,
                                                                               CD_TIPO_REDE_SOCIAL,
                                                                               DS_USUARIO,
                                                                               ID_TOKEN,
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
                RedeSocialData ObjData = new RedeSocialData();

                ObjData.SetIncluirRedeSocialCentral(CD_CONTABIL_PESSOA,
                                                    NU_SEQ,
                                                    DS_REDE_SOCIAL,
                                                    DS_EMAIL,
                                                    CD_TIPO_REDE_SOCIAL,
                                                    DS_USUARIO,
                                                    ID_TOKEN,
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


        public void SetAlterarRedeSocialCentral(int? CD_CONTABIL_PESSOA,
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

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.SetAlterarRedeSocialCentralService(CD_CONTABIL_PESSOA,
                                                                               NU_SEQ,
                                                                               DS_REDE_SOCIAL,
                                                                               DS_EMAIL,
                                                                               CD_TIPO_REDE_SOCIAL,
                                                                               DS_USUARIO,
                                                                               ID_TOKEN,
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
                RedeSocialData ObjData = new RedeSocialData();

                ObjData.SetAlterarRedeSocialCentral(CD_CONTABIL_PESSOA,
                                                    NU_SEQ,
                                                    DS_REDE_SOCIAL,
                                                    DS_EMAIL,
                                                    CD_TIPO_REDE_SOCIAL,
                                                    DS_USUARIO,
                                                    ID_TOKEN,
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
        public void SetDeletarRedeSocialCentral(int? CD_CONTABIL_PESSOA,
                                                     byte NU_SEQ,
                                                     Boolean pbolAbrirTransacao,
                                                     Boolean pbolFecharConexao)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.SetDeletarRedeSocialCentralService(CD_CONTABIL_PESSOA, NU_SEQ, pbolAbrirTransacao, pbolFecharConexao);

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
        public string SetDeletarRedeSocialCentralService(int? CD_CONTABIL_PESSOA,
                                                            byte NU_SEQ,
                                                            Boolean pbolAbrirTransacao,
                                                            Boolean pbolFecharConexao)
        {
            try
            {
                RedeSocialData ObjData = new RedeSocialData();

                ObjData.SetDeletarRedeSocialCentral((int)CD_CONTABIL_PESSOA, NU_SEQ, pbolAbrirTransacao, pbolFecharConexao);

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
