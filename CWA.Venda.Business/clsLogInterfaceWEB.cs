using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

using CWA.Venda.Data;
using CWA.Venda.Entity;

using CWA.Util; 

namespace CWA.Venda.Business
{
    public class LogInterfaceWEBBusiness
    {
        private System.Int32 _erro;
        private string _msgErro;

        private int _IDLogRatreamento;

        public System.Int32 Erro
        {
            get { return _erro; }
        }
        public string MsgErro
        {
            get { return _msgErro; }
        }
        public int IDLogRatreamento
        {
            get { return _IDLogRatreamento; }
        }
        public void SetLogInterface(int pintIDLog,
                                    DadosPessoaEntity pObjPS,
                                    DadosEntregaEntity pObjDE,
                                    DadosPagamentoEntity pObjPG,
                                    PlanoComercialWebEntity pObjPlano,
                                    GatewayPagamento ObjGateway,
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

                //Quando o tipo do log for 1 - venda web 
                //o campo DS_OBS será preenchido com os dados do plano

                string lstrPessoaJSON = "";
                string lstrEntregaJSON = "";
                string lstrPagamentoJSON = "";
                string lstrGateWayJSON = "";

                Utility ObjUtil = new Utility();

                lstrPessoaJSON = ObjUtil.GetObjetoToStringJSON(pObjPS);
                lstrEntregaJSON = ObjUtil.GetObjetoToStringJSON(pObjDE);
                lstrPagamentoJSON = ObjUtil.GetObjetoToStringJSON(pObjPG);

                if (ObjGateway != null)
                {
                    lstrGateWayJSON = ObjUtil.GetObjetoToStringJSON(ObjGateway);
                }

                string lstrValor1ParcelaCentavos = pObjPlano.VA_PARC_CENT;
                string lstrValorTotalCentavos = pObjPlano.VA_TOTAL_CENT;

                string lsParm = "";

                if (pObjPG.CD_TP_FORMA_PAG == 6) //CARTAO
                {
                    
                    lsParm = lsParm + "Venda" + ",";                        //TipoInterface
                    lsParm = lsParm + pObjPG.NM_PESSOA_CARTAO + ",";

                    if (pObjPS.TIPO_PARC_CARTAO == 1)
                    {                                                       //PARCELADO PELA ADM
                        lsParm = lsParm + lstrValorTotalCentavos + ",";     //VALOR TOTAL
                        lsParm = lsParm + pObjPG.NU_PARCELA + ",";           //TOTAL DE PARCELAS 

                    }
                    else
                    {                                                       //PARCELADO PELA JORNAL
                        lsParm = lsParm + lstrValor1ParcelaCentavos + ",";  //VALOR DA PARCELA
                        lsParm = lsParm + "1,";                             //PARCELA = 1 (PRIMEIRA)

                    }

                    if (!string.IsNullOrEmpty(pObjPG.NU_CARTAO))
                    {
                        lsParm = lsParm + pObjPG.NU_CARTAO + ",";
                        lsParm = lsParm + pObjPG.NM_PESSOA_CARTAO + ",";
                        lsParm = lsParm + pObjPG.DT_VALID.Substring(3, 7) + ","; //pegar somente mes/ano
                        lsParm = lsParm + pObjPG.NU_CVV + ",";
                        lsParm = lsParm + pObjPG.NM_BANDEIRA + ",";

                    }

                    lsParm = lsParm + pstrSerieCTR + pintNuCTR.ToString("0000000000") + pintDvCTR.ToString() + ";";

                    lsParm = lsParm.Substring(0, lsParm.Length - 1);
                }

                if (pintIDLog == 0 )
                {

                    LogInterfaceWEBEntity ObjLongEnt = new LogInterfaceWEBEntity();

                    ObjLongEnt.ID_LOG = pintIDLog;

                    if (lsParm != "")
	                {
                        ObjLongEnt.DS_INPUT_GATEWAY = lsParm;
	                }
                    else
                    {
                        ObjLongEnt.DS_INPUT_GATEWAY = null;
                    }

                    ObjLongEnt.DS_METODO = pstrMetodo;

                    if (ObjLongEnt.DS_OBS != "")
	                {
                        ObjLongEnt.DS_OBS = "PC -> " + pObjPlano.CD_PLANO  + " - " + pstrObs; 
	                }

                    if (lstrGateWayJSON != "")
                    {
                        ObjLongEnt.DS_OUTPUT_GATEWAY = lstrGateWayJSON;
                    }
                    else
                    {
                        ObjLongEnt.DS_OUTPUT_GATEWAY = null;
                    }
                    
                    
                    ObjLongEnt.DS_REG_ENDER = lstrEntregaJSON;
                    ObjLongEnt.DS_REG_PESSOA = lstrPessoaJSON;
                    ObjLongEnt.DS_REG_PGTO = lstrPagamentoJSON;
                    if (pstrURL != "")
	                {
                        ObjLongEnt.DS_URL = pstrURL; 
                    }
                    ObjLongEnt.DT_LOG = DateTime.Now.ToString("dd/MM/yyyy");
                    ObjLongEnt.HR_LOG = DateTime.Now.ToString("hh:MM:ss");
                    ObjLongEnt.TP_LOG = 1; //Venda de Assinatura
                    ObjLongEnt.DS_CHAMADOR = pstrChamador;

                    if (pstrSerieCTR != "")
	                {
		                ObjLongEnt.NU_SERIE_CTR = pstrSerieCTR;
	                }

                    ObjLongEnt.NU_CTR = pintNuCTR;
                    ObjLongEnt.NU_DV_CTR = pintDvCTR;

                    if (pstrErro.Trim() != "" )
                    {
                        ObjLongEnt.DS_ERRO = pstrErro;
                    }
                    else
                    {
                        ObjLongEnt.DS_ERRO = null;
                    }
                    
                    LogInterfaceWEBData ObjData = new LogInterfaceWEBData();

                    //ObjData.SetLogInterfaceWEB(ObjLongEnt, true, true);
                    ObjData.SetLogInterfaceWEBSQL(ObjLongEnt, true, true);

                    if (DataContext.Erro == 0)
                    {
                        _IDLogRatreamento = ObjData.IDLogRastreamento;

                    }
                    else
                    {
                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                    }

                }
                else
                {

                }

            }
            catch (Exception ex )
            {
                _erro = -1;
                _msgErro = "SetLogInterface - " + ex.Message + " - " + ex.InnerException;
            }


        }
 
       public void SetUpdLogInterface (string pstrRetGateway,
                                       string pstrError,
                                       Int32 pintIDLog)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                LogInterfaceWEBData ObjData = new LogInterfaceWEBData();

                //ObjData.SetUpdLogInterfaceWEB(pstrRetGateway, pstrError, pintIDLog, true, true);
                ObjData.SetUpdLogInterfaceWEBSQL(pstrRetGateway, pstrError, pintIDLog, true, true);

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;
            }
        }

       public LogInterfaceWEBEntity GetLogInterfaceWEB(int pintIDLog)
       {
           try
           {

               LogInterfaceWEBData ObjData = new LogInterfaceWEBData(); 
               LogInterfaceWEBEntity ObjEnt = new LogInterfaceWEBEntity();

               ObjEnt = ObjData.GetLogInterfaceWEB(pintIDLog);

               if (ObjData.Erro == 0)
               {
                   return ObjEnt;
               }
               else
               {
                   _erro = ObjData.Erro;
                   _msgErro = ObjData.MsgErro ;

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

    }

}
