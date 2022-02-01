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
    public class DadosDoAssinanteCentralBusiness
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


        public List<CWA.Central.Domain.BusinessEntities.DadosDoAssinante> GetLerDadosDoAssinanteCentral(int? CD_CONTABIL_PESSOA)
        {

            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetLerDadosDoAssinanteCentralJSON((int)CD_CONTABIL_PESSOA);

                if (lstrRet != "")
                {
                    List<CWA.Central.Domain.BusinessEntities.DadosDoAssinante> lcolEnt = new List<CWA.Central.Domain.BusinessEntities.DadosDoAssinante>();

                    lcolEnt = (List<CWA.Central.Domain.BusinessEntities.DadosDoAssinante>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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
        public string GetLerDadosDoAssinanteCentralService(int CD_CONTABIL_PESSOA)
        {

            try
            {

                DadosdoAssinanteData ObjData = new DadosdoAssinanteData();
                List<DadosdoAssinanteCentralEntity> lcolEnt = new List<DadosdoAssinanteCentralEntity>();

                lcolEnt = ObjData.GetLerDadosDoAssinanteCentral(CD_CONTABIL_PESSOA);


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

        public void SetAlterarDadosDoAssinanteCentral(byte? ST_TP_PESSOA, string NM_PESSOA,
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

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.SetAlterarDadosDoAssinanteCentralJSON(ST_TP_PESSOA, NM_PESSOA, CD_CONTABIL_PESSOA, NM_RESPONSAVEL,
                                                                                  NU_IDENTIDADE, NM_ORGAO_EMISSOR, DT_EMISSAO, ST_ESTADO_CIVIL,
                                                                                  DT_NASC_FUND, NM_FANTASIA, NU_INSCR_MUN, NU_INSCR_EST,
                                                                                  CD_RAMO, DS_NOME_ABREV, CD_GRUPO_AFINIDADE, CD_TP_TRATAMENTO,
                                                                                  CD_CARGO, CD_GRAU_INSTRUCAO, CD_NACIONALIDADE, CD_NATURALIDADE,
                                                                                  CD_PROFISSAO, NM_SOBRENOME, NU_CPF_RESP, pbolAbrirTransacao, pbolFecharConexao);

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



        public string SetAlterarDadosDoAssinanteCentralService(byte? ST_TP_PESSOA, string NM_PESSOA,
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
                DadosdoAssinanteData ObjData = new DadosdoAssinanteData();

                ObjData.SetAlterarDadosDoAssinanteCentral(ST_TP_PESSOA, NM_PESSOA, CD_CONTABIL_PESSOA, NM_RESPONSAVEL,
                                                        NU_IDENTIDADE, NM_ORGAO_EMISSOR, DT_EMISSAO, ST_ESTADO_CIVIL,
                                                        DT_NASC_FUND, NM_FANTASIA, NU_INSCR_MUN, NU_INSCR_EST,
                                                        CD_RAMO, DS_NOME_ABREV, CD_GRUPO_AFINIDADE, CD_TP_TRATAMENTO,
                                                        CD_CARGO, CD_GRAU_INSTRUCAO, CD_NACIONALIDADE, CD_NATURALIDADE,
                                                        CD_PROFISSAO, NM_SOBRENOME, NU_CPF_RESP, 
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

    }
}
