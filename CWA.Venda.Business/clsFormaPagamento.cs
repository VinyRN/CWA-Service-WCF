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
    public class FormaPagamentoBusiness
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

        public List<FormaPagamentoEntity> GetFormaPagamentoList(int pintIDProduto, int pintIDCampanha,
                                                                int pintIDTipoAssinatura, int pintTipoEntrega,
                                                                int pintIDTipoPag)
        {

            try
            {

                FormaPagamentoData ObjData = new FormaPagamentoData();

                List<FormaPagamentoEntity> lcolEnt = new List<FormaPagamentoEntity>();

                lcolEnt = ObjData.GetFormaPagamentoEntityList(pintIDProduto, pintIDCampanha,
                                                              pintIDTipoAssinatura, pintTipoEntrega,
                                                              pintIDTipoPag);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    return lcolEnt;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }

        }

        public string GetFormaPagamentoListService()
        {

            try
            {

                FormaPagamentoData ObjData = new FormaPagamentoData();

                List<FormaPagamentoEntity> lcolEnt = new List<FormaPagamentoEntity>();

                lcolEnt = ObjData.GetFormaPagamentoEntityList();

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
        
        public string GetFormaPagamentoEntService(int pintIDFormaPag)
        {

            try
            {

                FormaPagamentoData ObjData = new FormaPagamentoData();

                FormaPagamentoEntity lEnt = new FormaPagamentoEntity();

                lEnt = ObjData.GetFormaPagamentoEntity(pintIDFormaPag);

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

        public FormaPagamentoEntity GetFormaPagamentoEnt(int pintIDFormaPag)
        {

            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetFormaPagamento;" + pintIDFormaPag.ToString();

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
                    FormaPagamentoEntity lEnt = new FormaPagamentoEntity();

                    lEnt = (FormaPagamentoEntity)JsonConvert.DeserializeObject(lstrRet, lEnt.GetType());

                    return lEnt;

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
