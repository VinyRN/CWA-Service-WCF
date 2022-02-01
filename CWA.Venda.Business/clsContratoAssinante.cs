using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

using CWA.Venda.Data;
using CWA.Venda.Entity;
using CWA.Util;
using CWA.EngineServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace CWA.Venda.Business
{
    public class ContratoAssinanteBusiness
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

        
        public StatusContratoEntity GetStatusContrato(Int32 pintCTR, 
                                                     int pintDVCTR, 
                                                     string pstrSerieCTR, 
                                                     Int32 pintCodigoAssinante)
        {
            try
            {
                ContratoAssinanteData ObjData = new ContratoAssinanteData();

                StatusContratoEntity  lEnt = new StatusContratoEntity();

                lEnt = ObjData.GetStatusContrato(pintCTR, pintDVCTR, pstrSerieCTR, pintCodigoAssinante);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    return lEnt;
                }


            }
            catch (Exception ex)
            {
                
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }

        }

        public string[] GetStatusAplicacao(Int32 pintCTR,
                                            int pintDVCTR,
                                            string pstrSerieCTR,
                                            Int32 pintCodigoAssinante)
        {
            try
            {
                ContratoAssinanteData ObjData = new ContratoAssinanteData();

                StatusContratoEntity lEnt = new StatusContratoEntity();

                lEnt = ObjData.GetStatusContrato(pintCTR, pintDVCTR, pstrSerieCTR, pintCodigoAssinante);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return new string[] { "N", "-1", "-1" };
                }
                else if (lEnt == null)
                {
                    return new string[] { "N", "-1", "-1" };
                }
                else
                {
    
                    string lstrListIntegracaoAcesso = ConfigurationManager.AppSettings["ListIntegracaoAcesso"];
                    string[] lstrVetorStatusAcesso = lstrListIntegracaoAcesso.Split('|');

                    string lstrVetorRet = "";

                    foreach (string lstrLinha in lstrVetorStatusAcesso)
                    {

                        string[] lstrDadosAcesso = lstrLinha.Split(',');

                        if (int.Parse(lstrDadosAcesso[0]) == lEnt.ST_ESTADO_ATUAL)
                        {
                            //Verifica se o Status é suspenso para validar o motivo
                            if (lEnt.ST_ESTADO_ATUAL == 2) 
                            {
                                //Verifica se o motivo é o de viajem se sim para o status digital para ativo. 
                                if (int.Parse(lstrDadosAcesso[3]) == lEnt.CD_MOTIVO_SUSCANC )
                                {
                                    lstrVetorRet = "S,0," + lstrDadosAcesso[2] + "," + lEnt.NM_PESSOA + "," + lEnt.ST_TP_PRODUTO + "," + lEnt.CD_CAMPANHA + "," + lEnt.CD_PLANO + "," + lEnt.ST_ESTADO_ATUAL;
                                    break;
                                }

                            }  //Verifica se o Status na BV e valida se esta dentro do período permitido (numero de dias no web.config)
                            else if (lEnt.ST_ESTADO_ATUAL == 14)
                            {

                                int lintDiaAcessoBV = int.Parse(ConfigurationManager.AppSettings["TempoAcessoBV"]);

                                DateTime ldtDataDia = DateTime.Now;
                                DateTime ldtDatavenda = lEnt.DT_VENDA;

                                TimeSpan lintCalcDias = (ldtDataDia - ldtDatavenda);

                                int lintDifDays = lintCalcDias.Days;

                                if (lintDifDays <= lintDiaAcessoBV)
                                {
                                    lstrVetorRet = "S,0,0" + "," + lEnt.NM_PESSOA + "," + lEnt.ST_TP_PRODUTO + "," + lEnt.CD_CAMPANHA + "," + lEnt.CD_PLANO + "," + lEnt.ST_ESTADO_ATUAL;
                                    break;

                                }
                                else
                                {
                                    lstrVetorRet = "S," + lstrDadosAcesso[1] + "," + lstrDadosAcesso[2] + "," + lEnt.NM_PESSOA + "," + lEnt.ST_TP_PRODUTO + "," + lEnt.CD_CAMPANHA + "," + lEnt.CD_PLANO + "," + lEnt.ST_ESTADO_ATUAL;
                                }
                            }
                            else
                            {
                                string lstrAcesso = "";

                                if (lstrDadosAcesso[1].Trim() == "")
                                {
                                    lstrAcesso = lstrAcesso + "1";
                                }
                                else
                                {
                                    lstrAcesso = lstrAcesso + lstrDadosAcesso[1];
                                }

                                if (lstrDadosAcesso[2].Trim() == "")
                                {
                                    lstrAcesso = lstrAcesso + ",1";
                                }
                                else
                                {
                                    lstrAcesso = lstrAcesso + "," + lstrDadosAcesso[2];
                                }

                                lstrVetorRet = "S," + lstrAcesso + "," + lEnt.NM_PESSOA + "," + lEnt.ST_TP_PRODUTO + "," + lEnt.CD_CAMPANHA + "," + lEnt.CD_PLANO + "," + lEnt.ST_ESTADO_ATUAL;
                                break;
                            }
                        }


                    }

                    if (lstrVetorRet == "")
                    {
                        return new string[] { "N", "Não Localizado", "Não Localizado" };
                    }
                    else
                    {
                        return lstrVetorRet.Split(',');
                    }
                    
                }

            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message;

                return new string[] { "N", "-1", "-1" };
            }
        
        
        }

        public Int32 GetContabilContrato(Int32 pintCTR,
                                         int pintDVCTR,
                                         string pstrSerieCTR)
        {
            try
            {
                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetContabilContrato;" + pintCTR.ToString()  + ";" + pintDVCTR.ToString() + ";" + pstrSerieCTR;

                ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                lstrVetRet = ObjInterface.VertorRetorno;

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        lstrRet = lstrVetRet[2];
                    }

                }

                if (lstrRet != "")
                {
                    
                    string lstrRetService = "";

                    lstrRetService = (string)JsonConvert.DeserializeObject(lstrRet, lstrRetService.GetType() );

                    if (lstrRetService != "")
                    {
                        return Int32.Parse(lstrRetService);
                    }
                    else
                    {
                        _erro = -1;
                        _msgErro = "Erro ao retornar JSON não pode ser desfeito";

                        return 0;
                    }
                    

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

                    return 0;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return 0;
            }
        }

        public string GetContabilContratoService(Int32 pintCTR,
                                                int pintDVCTR,
                                                string pstrSerieCTR)
        {
            try
            {

                ContratoAssinanteData ObjData = new ContratoAssinanteData();

                Int32 lintCodigoAssinante = ObjData.GetContabilContrato(pintCTR, pintDVCTR, pstrSerieCTR);

                if (lintCodigoAssinante == 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;
                }

                string lstrRet = JsonConvert.SerializeObject(lintCodigoAssinante.ToString(), Formatting.None);
                return lstrRet;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "0";
            }
        }

        public bool SetSituacaoContrato(Int32 pintCTR,
                                        int pintDVCTR,
                                        string pstrSerieCTR,
                                        Int32 pintCodigoAssinante,
                                        int pintSituacaoAtual,
                                        int pintSituacaoAnterior,
                                        Boolean pbolAbrirTransacao,
                                        Boolean pbolFecharConexao)
        {

            try
            {
                ContratoAssinanteData ObjData = new ContratoAssinanteData();

                ObjData.SetSituacaoContrato(pintCTR, 
                                            pintDVCTR, 
                                            pstrSerieCTR, 
                                            pintCodigoAssinante,
                                            pintSituacaoAtual, 
                                            pintSituacaoAnterior,
                                            pbolAbrirTransacao, 
                                            pbolFecharConexao);

                if (ObjData.Erro != 0)
                {
                    _erro = -99;
                    _msgErro = ObjData.MsgErro;
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return false;
                
            }


        }


        public ContratoAssinanteEntity GetDadosContrato(int pintCdPessoa, 
                                                        string pstrSerieCTR, 
                                                        int pintNuCTR, 
                                                        int pintDvCTR)
        {

            try
            {
                ContratoAssinanteData ObjData = new ContratoAssinanteData();

                ContratoAssinanteEntity lEnt = new ContratoAssinanteEntity();

                lEnt = ObjData.GetDadosContrato(pintCdPessoa, pstrSerieCTR, pintNuCTR, pintDvCTR);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    return lEnt;
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
