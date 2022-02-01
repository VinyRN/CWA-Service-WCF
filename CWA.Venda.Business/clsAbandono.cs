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
    public class AbandonoBusiness
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

        public void SetAbandono(string pstrDOC, string pstrEmail, AbandonoEntity pObjEnt, int pintEtapa, int pintStatus,
                                string pstrNuSerieCTR ="", Int32 pintNuCTR = 0, int pintNuDvCTR = 0)
        {
            try
            {
                AbandonoEntity ObjAbandonoEnt = new AbandonoEntity();

                if (pintEtapa == 1 ) //Dados Pessoa
                {
                    ObjAbandonoEnt.NU_DOC = pstrDOC;
                    ObjAbandonoEnt.DS_EMAIL = pObjEnt.DS_EMAIL;

                    ObjAbandonoEnt.DS_REG_PESSOA = pObjEnt.DS_REG_PESSOA; 
                    ObjAbandonoEnt.TP_ETAPA = pintEtapa;
                    ObjAbandonoEnt.ST_STATUS = 0;

                }
                else if (pintEtapa == 2) //Dados Endereço
                {
                    ObjAbandonoEnt.NU_DOC = pstrDOC;
                    ObjAbandonoEnt.DS_EMAIL = pObjEnt.DS_EMAIL;

                    ObjAbandonoEnt.DS_REG_ENDER = pObjEnt.DS_REG_ENDER;
                    ObjAbandonoEnt.ST_STATUS = 0;
                    ObjAbandonoEnt.TP_ETAPA = pintEtapa;
                    
                }
                else if ((pintEtapa == 3) || (pintEtapa == 99))//Dados Venda
                {
                    ObjAbandonoEnt.NU_DOC = pstrDOC;
                    ObjAbandonoEnt.DS_EMAIL = pObjEnt.DS_EMAIL;

                    ObjAbandonoEnt.DS_REG_VENDA = pObjEnt.DS_REG_VENDA;
                    ObjAbandonoEnt.ST_STATUS = 0;
                    ObjAbandonoEnt.TP_ETAPA = pintEtapa;

                }
                else
                {
                    ObjAbandonoEnt.NU_DOC = pstrDOC;
                    ObjAbandonoEnt.DS_EMAIL = pObjEnt.DS_EMAIL;

                    ObjAbandonoEnt.NU_SERIE_CTR = pstrNuSerieCTR;
                    ObjAbandonoEnt.NU_CTR = pintNuCTR;
                    ObjAbandonoEnt.NU_DV_CTR = pintNuDvCTR;
                    ObjAbandonoEnt.TP_ETAPA = pintEtapa;
                    ObjAbandonoEnt.ST_STATUS = 1;
                }


                AbandonoData ObjData = new AbandonoData();

                ObjData.SetAbandono(ObjAbandonoEnt, true, true);

                if (ObjData.Erro != 0)
                {
                    _erro = -1;
                    _msgErro = ObjData.MsgErro; 
                }

                return; 
            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;
                return; 
            }
        
        }


        public void SetAbandonoService(string pstrDOC, string pstrEmail, object pObjEnt, int pintEtapa, int pintStatus,
                                       string pstrNuSerieCTR = "", Int32 pintNuCTR = 0, int pintNuDvCTR = 0)
        {
            try
            {
                AbandonoEntity ObjAbandonoEnt = new AbandonoEntity();

                if (pintEtapa == 1) //Dados Pessoa
                {
                    ObjAbandonoEnt.NU_DOC = pstrDOC;
                    ObjAbandonoEnt.DS_EMAIL = pstrEmail;

                    ObjAbandonoEnt.DS_REG_PESSOA = JsonConvert.SerializeObject(((DadosPessoaEntity)pObjEnt), Formatting.None);
                    ObjAbandonoEnt.TP_ETAPA = pintEtapa;
                    ObjAbandonoEnt.ST_STATUS = 0;

                }
                else if (pintEtapa == 2) //Dados Endereço
                {
                    ObjAbandonoEnt.NU_DOC = pstrDOC;
                    ObjAbandonoEnt.DS_EMAIL = pstrEmail;

                    ObjAbandonoEnt.DS_REG_ENDER = JsonConvert.SerializeObject(((DadosEntregaEntity)pObjEnt), Formatting.None);
                    ObjAbandonoEnt.ST_STATUS = 0;
                    ObjAbandonoEnt.TP_ETAPA = pintEtapa;

                }
                else if ((pintEtapa == 3) || (pintEtapa == 99))//Dados Venda
                {
                    ObjAbandonoEnt.NU_DOC = pstrDOC;
                    ObjAbandonoEnt.DS_EMAIL = pstrEmail;

                    ObjAbandonoEnt.DS_REG_VENDA = JsonConvert.SerializeObject(((CampanhaPlanoEntity)pObjEnt), Formatting.None);
                    ObjAbandonoEnt.ST_STATUS = 0;
                    ObjAbandonoEnt.TP_ETAPA = pintEtapa;

                }
                else
                {
                    ObjAbandonoEnt.NU_DOC = pstrDOC;
                    ObjAbandonoEnt.DS_EMAIL = pstrEmail;

                    ObjAbandonoEnt.NU_SERIE_CTR = pstrNuSerieCTR;
                    ObjAbandonoEnt.NU_CTR = pintNuCTR;
                    ObjAbandonoEnt.NU_DV_CTR = pintNuDvCTR;
                    ObjAbandonoEnt.TP_ETAPA = pintEtapa;
                    ObjAbandonoEnt.ST_STATUS = 1;
                }

                Utility ObjUtil = new Utility();

                string lstrObjJSONCript = ObjUtil.SetCript(JsonConvert.SerializeObject(((AbandonoEntity)ObjAbandonoEnt), Formatting.None));


                //Fazer a Chamada da EngineService passando os JSON Criptografado.

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GravarAbandono;" + pstrDOC + ";" + pstrEmail + ";" + lstrObjJSONCript + ";" 
                                                                                         + pintEtapa.ToString() + ";" + pintStatus.ToString() + ";"
                                                                                         + pstrNuSerieCTR + ";" + pintNuCTR.ToString() + ";" + pintNuDvCTR.ToString();

                ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                lstrVetRet = ObjInterface.VertorRetorno;

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        //Tratar Retorno e setar as variaveis da classe.
                        lstrRet = lstrVetRet[2];
                    }

                }

                if (lstrRet != "")
                {
                    string[] lstrVetorRet = lstrRet.Split(';');

                    if (lstrVetorRet != null)
                    {

                    }

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao Gravar Abandono - WCF/Engine";

                    return;
                }

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;
                return;
            }

        }

        public string GetAbandonoService(string pstrDOC, int pintTipo)
        {
            try
            {
              
                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetAbandono;" + pstrDOC + ";" + pintTipo.ToString();

                ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                lstrVetRet = ObjInterface.VertorRetorno;

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        //Tratar Retorno e setar as variaveis da classe.
                        lstrRet = lstrVetRet[2];
                    }

                    return lstrRet;

                }
                {
                    _erro = -1;
                    _msgErro = "Erro ao GetAbandonoService - WCF/Engine";

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

        public string GetAbandono(string pstrDOC, int pintTipo)
        {
            try
            {
                AbandonoEntity ObjEnt = new AbandonoEntity(); 
                AbandonoData ObjData = new AbandonoData();

                ObjEnt = ObjData.GetAbandono(pstrDOC);

                if (ObjData.Erro == 0)
                {
                    if (ObjEnt != null)
                    {
                        if (pintTipo == 1)
	                    {
                            return ObjEnt.DS_REG_PESSOA;
                        }
                        else
                        {
                            return ObjEnt.DS_REG_ENDER;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    _erro = -1;
                    _msgErro = ObjData.MsgErro;
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

        public void SetAbandonoMKT(int pintIDLog)
        {
            try
            {
                
                AbandonoData ObjData = new AbandonoData();

                ObjData.SetAbandonoMKT(pintIDLog, true, true);

                if (ObjData.Erro != 0)
                {
                    _erro = -1;
                    _msgErro = ObjData.MsgErro;
                }

                return;
            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;
                return;
            }

        }

        public string GetAbandonoMKTService()
        {
            try
            {
                List<AbandonoEntity> ObjListEnt = new List<AbandonoEntity>();
                AbandonoData ObjData = new AbandonoData();

                ObjListEnt = ObjData.GetAbandonoMKT();

                if (ObjData.Erro == 0)
                {
                    if (ObjListEnt != null)
                    {
                        return JsonConvert.SerializeObject(ObjListEnt, Formatting.None);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    _erro = -1;
                    _msgErro = ObjData.MsgErro;
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
