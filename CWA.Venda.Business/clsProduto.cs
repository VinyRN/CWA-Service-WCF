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
    public class ProdutoBusiness
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

        public List<ProdutoEntity> GetProdutoAgregadoList()
        {

            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetProdutoAgregadoList;";

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
                    List<ProdutoEntity> lcolEnt = new List<ProdutoEntity>();

                    lcolEnt = (List<ProdutoEntity>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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

        public string GetProdutoAgregadListService()
        {
            try
            {
                ProdutoData ObjData = new ProdutoData();

                List<ProdutoEntity> lcolEnt = new List<ProdutoEntity>();

                lcolEnt = ObjData.GetProdutoAgregado();

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


        public List<ProdutoEntity> GetProdutoAgregadoList( int pintIDProduto, int pintIDItem)
        {

            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetProdutoAgregadoIDList;" + pintIDProduto.ToString() + ";" + pintIDItem.ToString();

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
                    List<ProdutoEntity> lcolEnt = new List<ProdutoEntity>();

                    lcolEnt = (List<ProdutoEntity>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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

        public string GetProdutoAgregadListService(int pintIDProduto, int pintIDItem)
        {
            try
            {
                ProdutoData ObjData = new ProdutoData();

                List<ProdutoEntity> lcolEnt = new List<ProdutoEntity>();

                lcolEnt = ObjData.GetProdutoAgregado(pintIDProduto, pintIDItem);

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

        public string GetProdutoListService(int pintIDProduto)
        {
            try
            {
                ProdutoData ObjData = new ProdutoData();

                List<ProdutoEntity> lcolEnt = new List<ProdutoEntity>();

                //lcolEnt = ObjData.GetProduto(pintIDProduto);
                lcolEnt = ObjData.GetProdutoSQL(pintIDProduto);

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
                _msgErro = "GetProdutoListService - " + ex.Message;

                return null;

            }


        }
    }
}
