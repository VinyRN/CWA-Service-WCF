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
    public class TipoPagamentoBusiness
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

        public List<TipoPagamentoEntity> GetTipoPagamentoList(int pintIDProduto, int pintIDCampanha, 
                                                              int pintIDTipoAssinatura, int pintTipoEntrega)
        {

            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetTipoPagamentoList;" + pintIDProduto.ToString() + ";" 
                                                                                               + pintIDCampanha.ToString() + ";" 
                                                                                               + pintIDTipoAssinatura.ToString() + ";" 
                                                                                               + pintTipoEntrega.ToString() ;

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
                    List<TipoPagamentoEntity> lcolEnt = new List<TipoPagamentoEntity>();

                    lcolEnt = (List<TipoPagamentoEntity>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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

        public string GetTipoPagamentoListService(int pintIDProduto, int pintIDCampanha,
                                                  int pintIDTipoAssinatura, int pintTipoEntrega)
        {

            try
            {

                TipoPagamentoData ObjData = new TipoPagamentoData();

                List<TipoPagamentoEntity> lcolEnt = new List<TipoPagamentoEntity>();

                lcolEnt = ObjData.GetTipoPagamentoEntityList(pintIDProduto, pintIDCampanha, pintIDTipoAssinatura, pintTipoEntrega);

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

        public string GetTipoPagamentoListService()
        {

            try
            {

                TipoPagamentoData ObjData = new TipoPagamentoData();

                List<TipoPagamentoEntity> lcolEnt = new List<TipoPagamentoEntity>();

                lcolEnt = ObjData.GetTipoPagamentoEntityList();

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

        public string GetTipoPagamentoEntService(int pintTipoPagto)
        {

            try
            {

                TipoPagamentoData ObjData = new TipoPagamentoData();

                TipoPagamentoEntity lEnt = new TipoPagamentoEntity();

                lEnt = ObjData.GetTipoPagamentoEntity(pintTipoPagto);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lEnt, Formatting.None);
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

        public TipoPagamentoEntity GetTipoPagamentoEnt(int pintTipoPagto)
        {

            try
            {

                TipoPagamentoData ObjData = new TipoPagamentoData();

                TipoPagamentoEntity lEnt = new TipoPagamentoEntity();

                lEnt = ObjData.GetTipoPagamentoEntity(pintTipoPagto);

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
