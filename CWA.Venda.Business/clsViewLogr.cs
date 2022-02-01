using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CWA.Venda.Data;
using CWA.Venda.Entity;
using CWA.Util;
using CWA.EngineServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CWA.Venda.Business
{
    public class ViewLogrBusiness
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

        public List<DadosEntregaEntity> GetViewLogrEntity(string pstrCEP)
        {

            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetViewLogrEntity;" + pstrCEP;

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
                    List<DadosEntregaEntity> lcolEnt = new List<DadosEntregaEntity>();

                    lcolEnt = (List<DadosEntregaEntity>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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

        public string GetCodRoteirizacao(int pintIDProduto, int pintIDLogr, int pintNumeroResid)
        {

            try
            {
                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetCodRoteirizacao;" + 
                                       pintIDProduto.ToString() + ";" + pintIDLogr.ToString() + ";" + pintNumeroResid.ToString() ;

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
                    string lstrCodRot;
                    lstrCodRot = JsonConvert.DeserializeObject(lstrRet).ToString();

                    return lstrCodRot;

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

                    return "-1";
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "-1";
            }

        }

        public List<DadosEntregaEntity> GetViewLogrEntityID(Int32 pintIDLogr)
        {

            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetViewLogrEntityID;" + pintIDLogr.ToString();

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
                    List<DadosEntregaEntity> lcolEnt = new List<DadosEntregaEntity>();

                    lcolEnt = (List<DadosEntregaEntity>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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

        public string GetViewLogrEntityService(string pstrCEP)
        {
            try
            {
                
                ViewLogrData ObjData = new ViewLogrData();

                List<DadosEntregaEntity> lcolEnt = new List<DadosEntregaEntity>();

                lcolEnt = ObjData.GetViewLogrEntity(pstrCEP);

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

        public string GetCodRoteirizacaoService(int pintIDProduto, int pintIDLogr, int pintNumeroResid)
        {

            try
            {

                ViewLogrData ObjData = new ViewLogrData();

                string lstrCodRot = ObjData.GetCodRoteirizacao(pintIDProduto, pintIDLogr, pintNumeroResid);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return "-1";
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lstrCodRot, Formatting.None);
                    return lstrRet;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "-1";
            }

        }

        public string GetViewLogrEntityServiceID(Int32 pintIDLogr)
        {
            try
            {

                ViewLogrData ObjData = new ViewLogrData();

                List<DadosEntregaEntity> lcolEnt = new List<DadosEntregaEntity>();

                //lcolEnt = ObjData.GetViewLogrEntityID(pintIDLogr);
                lcolEnt = ObjData.GetViewLogrEntityIDSQL(pintIDLogr);

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
                _msgErro = "GetViewLogrEntityServiceID - " + ex.Message;

                return null;
            }

        }
    }
}
