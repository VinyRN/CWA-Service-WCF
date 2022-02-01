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
    public class PrecoProdutoAgregadoBusiness
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

        public List<PrecoProdutoAgregEntity> GetPrecoProdutoAgregadoList(int pintIDProduto, int pintItem)
        {

            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetPrecoProdutoAgregadoList;" + pintIDProduto.ToString() + ";" + pintItem.ToString();

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
                    List<PrecoProdutoAgregEntity> lcolEnt = new List<PrecoProdutoAgregEntity>();

                    lcolEnt = (List<PrecoProdutoAgregEntity>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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

        public string GetPrecoProdutoAgregadoListService(int pintIDProduto, int pintItem)
        {
            try
            {
                PrecoProdutoAgregData ObjData = new PrecoProdutoAgregData();

                List<PrecoProdutoAgregEntity> lcolEnt = new List<PrecoProdutoAgregEntity>();

                lcolEnt = ObjData.GetPrecoProdutoAgregado(pintIDProduto, pintItem);

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

        public string GetPrecoProdutoAgregado()
        {
            try
            {
                

                ProdutoBusiness ObjProdBLL = new ProdutoBusiness();
                List<ProdutoEntity> ObjProdList = new List<ProdutoEntity>();

                //Recupera Lista de Produtos Agregado
                //ObjProdList = ObjProdBLL.GetProdutoAgregadoList();

                string lstrRetProdList = ObjProdBLL.GetProdutoAgregadListService();
                if (lstrRetProdList != "")
                {
                    ObjProdList = (List<ProdutoEntity>)JsonConvert.DeserializeObject(lstrRetProdList, ObjProdList.GetType());
                }

                if (ObjProdList != null)
                {

                    List<ProdutoItemPrecoAgregEntity> ObjProdItemPrecoList = new List<ProdutoItemPrecoAgregEntity>();

                    //Percorre Lista de Produtos Agregado
                    foreach (ProdutoEntity ObjItemProd in ObjProdList)
                    {
                        ProdutoItemPrecoAgregEntity ObjProdItemPrecoEnt = new ProdutoItemPrecoAgregEntity();

                        ObjProdItemPrecoEnt.CD_PRODUTO = ObjItemProd.CD_PRODUTO;
                        ObjProdItemPrecoEnt.NM_PRODUTO = ObjItemProd.NM_PRODUTO;

                        //Recupera os itens do produto
                        ItemProdutoBusiness ObjItensProdBLL = new ItemProdutoBusiness();
                        List<ItemProdutoEntity> ObjItensProdList = new List<ItemProdutoEntity>();

                        //ObjItensProdList = ObjItensProdBLL.GetItemProdutoAgregadoList(ObjItemProd.CD_PRODUTO);

                        string lstrRetItensProdList = ObjItensProdBLL.GetItemProdutoAgregadoListService(ObjItemProd.CD_PRODUTO);
                        if (lstrRetItensProdList != "")
                        {
                            ObjItensProdList = (List<ItemProdutoEntity>)JsonConvert.DeserializeObject(lstrRetItensProdList, ObjItensProdList.GetType());
                        }

                        if (ObjItensProdList != null)
                        {

                            List<ItemProdutoEntity> ObjItensProdListRet = new List<ItemProdutoEntity>();

                            //Percorre lista de itens para pegar os dados de preço
                            foreach (ItemProdutoEntity ObjItem in ObjItensProdList)
                            {

                                List<PrecoProdutoAgregEntity> ObjPrecoListRet = new List<PrecoProdutoAgregEntity>();

                                //Recupera preços
                                PrecoProdutoAgregadoBusiness ObjPrecoBLL = new PrecoProdutoAgregadoBusiness();
                                List<PrecoProdutoAgregEntity> ObjPrecoList = new List<PrecoProdutoAgregEntity>();

                                //ObjPrecoList = ObjPrecoBLL.GetPrecoProdutoAgregadoList(ObjItem.CD_PRODUTO, ObjItem.CD_ITEM_PRODUTO);

                                string lstrRetPrecoList = ObjPrecoBLL.GetPrecoProdutoAgregadoListService(ObjItem.CD_PRODUTO, ObjItem.CD_ITEM_PRODUTO);
                                if (lstrRetPrecoList != "")
                                {
                                    ObjPrecoList = (List<PrecoProdutoAgregEntity>)JsonConvert.DeserializeObject(lstrRetPrecoList, ObjPrecoList.GetType());
                                }

                                //Percorre lista de preços para pegar forma / tipo
                                foreach (PrecoProdutoAgregEntity ObjItemPreco in ObjPrecoList)
                                {

                                    //===================================================================================
                                    //Forma pagto
                                    //===================================================================================
                                    FormaPagamentoBusiness ObjFormaPagBLL = new FormaPagamentoBusiness();
                                    FormaPagamentoEntity ObjFormaPagEnt = new FormaPagamentoEntity();

                                    ObjFormaPagEnt = ObjFormaPagBLL.GetFormaPagamentoEnt(ObjItemPreco.CD_FORMA_PAG);

                                    string lstrRetFormaPagEnt = ObjFormaPagBLL.GetFormaPagamentoEntService(ObjItemPreco.CD_FORMA_PAG);
                                    if (lstrRetFormaPagEnt != "")
                                    {
                                        ObjFormaPagEnt = (FormaPagamentoEntity)JsonConvert.DeserializeObject(lstrRetFormaPagEnt, ObjFormaPagEnt.GetType());
                                    }

                                    //===================================================================================

                                    //===================================================================================
                                    //Forma tipo pagto
                                    //===================================================================================
                                    TipoPagamentoBusiness ObjTipoPagPagBLL = new TipoPagamentoBusiness();
                                    TipoPagamentoEntity ObjTipoPagEnt = new TipoPagamentoEntity();

                                    ObjTipoPagEnt = ObjTipoPagPagBLL.GetTipoPagamentoEnt(ObjFormaPagEnt.CD_TP_PAGAMENTO);

                                    string lstrRetTipoPagEnt = ObjTipoPagPagBLL.GetTipoPagamentoEntService(ObjFormaPagEnt.CD_TP_PAGAMENTO);
                                    if (lstrRetTipoPagEnt != "")
                                    {
                                        ObjTipoPagEnt = (TipoPagamentoEntity)JsonConvert.DeserializeObject(lstrRetTipoPagEnt, ObjTipoPagEnt.GetType());
                                    }


                                    ObjFormaPagEnt.TipoPagamento = ObjTipoPagEnt;
                                    //===================================================================================


                                    ObjItemPreco.FormaPagamento = ObjFormaPagEnt;

                                    ObjPrecoListRet.Add(ObjItemPreco);
                                    
                                }

                                ObjItem.PrecoProdutoAgreg = ObjPrecoListRet;

                                ObjItensProdListRet.Add(ObjItem);

                            }

                            //Adiconar o itens
                            ObjProdItemPrecoEnt.ItemProduto = ObjItensProdListRet;

                        }
                        else
                        {
                            return null;
                        }


                        ObjProdItemPrecoList.Add(ObjProdItemPrecoEnt);
                    }

                    //retorna os dados

                    string lstrRet = JsonConvert.SerializeObject(ObjProdItemPrecoList, Formatting.None);
                    return lstrRet;

                }
                else
                {
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

        public string GetPrecoProdutoAgregado(int pIDProduto, int pIDItem = 0)
        {
            try
            {


                ProdutoBusiness ObjProdBLL = new ProdutoBusiness();
                List<ProdutoEntity> ObjProdList = new List<ProdutoEntity>();

                //Recupera Lista de Produtos Agregado
                ObjProdList = ObjProdBLL.GetProdutoAgregadoList(pIDProduto, pIDItem);

                if (ObjProdList != null)
                {

                    List<ProdutoItemPrecoAgregEntity> ObjProdItemPrecoList = new List<ProdutoItemPrecoAgregEntity>();

                    //Percorre Lista de Produtos Agregado
                    foreach (ProdutoEntity ObjItemProd in ObjProdList)
                    {
                        ProdutoItemPrecoAgregEntity ObjProdItemPrecoEnt = new ProdutoItemPrecoAgregEntity();

                        ObjProdItemPrecoEnt.CD_PRODUTO = ObjItemProd.CD_PRODUTO;
                        ObjProdItemPrecoEnt.NM_PRODUTO = ObjItemProd.NM_PRODUTO;

                        //Recupera os itens do produto
                        ItemProdutoBusiness ObjItensProdBLL = new ItemProdutoBusiness();
                        List<ItemProdutoEntity> ObjItensProdList = new List<ItemProdutoEntity>();

                        ObjItensProdList = ObjItensProdBLL.GetItemProdutoAgregadoList(pIDProduto, pIDItem);

                        if (ObjItensProdList != null)
                        {

                            List<ItemProdutoEntity> ObjItensProdListRet = new List<ItemProdutoEntity>();

                            //Percorre lista de itens para pegar os dados de preço
                            foreach (ItemProdutoEntity ObjItem in ObjItensProdList)
                            {

                                List<PrecoProdutoAgregEntity> ObjPrecoListRet = new List<PrecoProdutoAgregEntity>();

                                //Recupera preços
                                PrecoProdutoAgregadoBusiness ObjPrecoBLL = new PrecoProdutoAgregadoBusiness();
                                List<PrecoProdutoAgregEntity> ObjPrecoList = new List<PrecoProdutoAgregEntity>();

                                ObjPrecoList = ObjPrecoBLL.GetPrecoProdutoAgregadoList(ObjItem.CD_PRODUTO, ObjItem.CD_ITEM_PRODUTO);

                                //Percorre lista de preços para pegar forma / tipo
                                foreach (PrecoProdutoAgregEntity ObjItemPreco in ObjPrecoList)
                                {

                                    //===================================================================================
                                    //Forma pagto
                                    //===================================================================================
                                    FormaPagamentoBusiness ObjFormaPagBLL = new FormaPagamentoBusiness();
                                    FormaPagamentoEntity ObjFormaPagEnt = new FormaPagamentoEntity();

                                    ObjFormaPagEnt = ObjFormaPagBLL.GetFormaPagamentoEnt(ObjItemPreco.CD_FORMA_PAG);
                                    //===================================================================================

                                    //===================================================================================
                                    //Forma tipo pagto
                                    //===================================================================================
                                    TipoPagamentoBusiness ObjTipoPagPagBLL = new TipoPagamentoBusiness();
                                    TipoPagamentoEntity ObjTipoPagEnt = new TipoPagamentoEntity();

                                    ObjTipoPagEnt = ObjTipoPagPagBLL.GetTipoPagamentoEnt(ObjFormaPagEnt.CD_TP_PAGAMENTO);

                                    ObjFormaPagEnt.TipoPagamento = ObjTipoPagEnt;
                                    //===================================================================================


                                    ObjItemPreco.FormaPagamento = ObjFormaPagEnt;

                                    ObjPrecoListRet.Add(ObjItemPreco);

                                }

                                ObjItem.PrecoProdutoAgreg = ObjPrecoListRet;

                                ObjItensProdListRet.Add(ObjItem);

                            }

                            //Adiconar o itens
                            ObjProdItemPrecoEnt.ItemProduto = ObjItensProdListRet;

                        }
                        else
                        {
                            return null;
                        }


                        ObjProdItemPrecoList.Add(ObjProdItemPrecoEnt);
                    }

                    //retorna os dados

                    string lstrRet = JsonConvert.SerializeObject(ObjProdItemPrecoList, Formatting.None);
                    return lstrRet;

                }
                else
                {
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
