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
    public class PlanoComercialBusiness
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

        public PlanoComercialWebEntity GetValorPlano(int pintIDCampaha, int pintIDPlano, int pintIDLogr)
        {

            try
            {

                if (pintIDCampaha == 0)
                {
                    _erro = -1;
                    _msgErro = "Campanha não pode ser 0";

                    return null;
                }

                if (pintIDPlano == 0)
                {
                    _erro = -1;
                    _msgErro = "Plano não pode ser 0";

                    return null;
                }


                PlanoComercialData ObjData = new PlanoComercialData();

                PlanoComercialWebEntity lcolEnt = new PlanoComercialWebEntity();

                //lcolEnt = ObjData.GetValorPlano(pintIDCampaha, pintIDPlano, pintIDLogr);
                lcolEnt = ObjData.GetValorPlanoSQL(pintIDCampaha, pintIDPlano, pintIDLogr);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {

                    if (lcolEnt == null)
                    {
                        _erro = ObjData.Erro;
                        _msgErro = ObjData.MsgErro;
                        return null;
                    }
                    else if (lcolEnt.VA_PARC_CENT == "-1")
                    {
                        _erro = -1;
                        _msgErro = "Preço não encontrado para Camp/Plano/Logr - " + pintIDCampaha.ToString() + "/" + pintIDPlano.ToString() + "/" + pintIDLogr.ToString() + " Favor entrar em contato com a Central de Atendimento.";
                        return null;
                    }


                    return lcolEnt;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GetValorPlano - " + ex.Message;

                return null;
            }

        }
        public PlanoComercialWebEntity GetParametrosPlano(int pintIDPlano)
        {

            try
            {

                if (pintIDPlano == 0)
                {
                    _erro = -1;
                    _msgErro = "Plano não pode ser 0";

                    return null;
                }


                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetParametrosPlano;" + pintIDPlano.ToString();

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
                    PlanoComercialWebEntity lcolEnt = new PlanoComercialWebEntity();

                    lcolEnt = (PlanoComercialWebEntity)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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
        public List<PlanoComercialWebEntity> GetPlanoCampanha(int pintIDCampanha)
        {
            try
            {

                if (pintIDCampanha == 0)
                {
                    _erro = -1;
                    _msgErro = "Campanha não pode ser 0";

                    return null;
                }


                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetPlanoCampanhaList;" + pintIDCampanha.ToString();

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
                    List<PlanoComercialWebEntity> lcolEnt = new List<PlanoComercialWebEntity>();

                    lcolEnt = (List<PlanoComercialWebEntity>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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
        public PlanoComercialEntity GetPlanoComercial(int pintIDPlano)
        {

            try
            {

                if (pintIDPlano == 0)
                {
                    _erro = -1;
                    _msgErro = "Plano não pode ser 0";

                    return null;
                }


                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetPlanoComercial;" + pintIDPlano.ToString();

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
                    PlanoComercialEntity lcolEnt = new PlanoComercialEntity();

                    lcolEnt = (PlanoComercialEntity)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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
        public PrecoCampanhaPlanoProdutoEntity GetPrecoCampanhaPlanoProduto(int pintIDCampaha, int pintIDPlano, int pintProduto)
        {

            try
            {

                if (pintIDCampaha == 0)
                {
                    _erro = -1;
                    _msgErro = "Campanha não pode ser 0";

                    return null;
                }

                if (pintIDPlano == 0)
                {
                    _erro = -1;
                    _msgErro = "Plano não pode ser 0";

                    return null;
                }

                if (pintProduto == 0)
                {
                    _erro = -1;
                    _msgErro = "Produto não pode ser 0";

                    return null;
                }

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetPrecoCampanhaPlanoProduto;" + pintIDCampaha.ToString() + ";" + pintIDPlano.ToString() + ";" + pintProduto;

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
                    PrecoCampanhaPlanoProdutoEntity lcolEnt = new PrecoCampanhaPlanoProdutoEntity();

                    lcolEnt = (PrecoCampanhaPlanoProdutoEntity)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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
        public string GetPrecoCampanhaPlanoProdutoService (int pintIDCampaha, int pintIDPlano, int pintProduto)
        {
            try
            {

                PlanoComercialData ObjData = new PlanoComercialData();

                PrecoCampanhaPlanoProdutoEntity lcolEnt = new PrecoCampanhaPlanoProdutoEntity();

                lcolEnt = ObjData.GetPrecoCampanhaPlanoProduto(pintIDCampaha, pintIDPlano, pintProduto);

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
        public string GetParametrosPlanoService(int pintIDPlano)
        {

            try
            {

                if (pintIDPlano == 0)
                {
                    _erro = -1;
                    _msgErro = "Plano não pode ser 0";

                    return null;
                }

                PlanoComercialData ObjData = new PlanoComercialData();

                PlanoComercialWebEntity lcolEnt = new PlanoComercialWebEntity();

                //lcolEnt = ObjData.GetParametrosPlano(pintIDPlano);
                lcolEnt = ObjData.GetParametrosPlanoSQL(pintIDPlano);

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
                _msgErro = "GetParametrosPlanoService - " + ex.Message;

                return null;
            }

        }
        public string GetPlanoCampanhaService(int pintIDCampanha)
        {

            try
            {

                PlanoComercialData ObjData = new PlanoComercialData();

                List<PlanoComercialWebEntity> lcolEnt = new List<PlanoComercialWebEntity>();

                lcolEnt = ObjData.GetPlanoCampanha(pintIDCampanha);

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
        public string GetPlanoComercialService(int pintIDPlano)
        {

            try
            {

                if (pintIDPlano == 0)
                {
                    _erro = -1;
                    _msgErro = "Plano não pode ser 0";

                    return null;
                }

                PlanoComercialData ObjData = new PlanoComercialData();

                PlanoComercialEntity lcolEnt = new PlanoComercialEntity();

                lcolEnt = ObjData.GetPlanoComercial(pintIDPlano);

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
    }
}
