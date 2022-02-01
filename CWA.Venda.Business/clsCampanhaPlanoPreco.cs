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

using System.IO;

namespace CWA.Venda.Business
{
    public class CampanhaPlanoPrecoBusiness
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pintIDCamp"></param>
        /// <returns></returns>
        public string GetCampanhaPlanoPreco(int pintIDCamp = 0)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                CampanhaBusiness ObjCampBLL = new CampanhaBusiness();
                List<CampanhaEntity> ObjCampList = new List<CampanhaEntity>();

                //ObjCampList = ObjCampBLL.GetCampanhaList(pintIDCamp);

                //=======================================================================
                //Novo correção memória JC
                //=======================================================================
                string lstrRetCamp = ObjCampBLL.GetCampanhaListService(pintIDCamp);

                if (lstrRetCamp.Trim() != "")
                {
                    ObjCampList = (List<CampanhaEntity>)JsonConvert.DeserializeObject(lstrRetCamp, ObjCampList.GetType());
                } 
                //=======================================================================
                              

                if (ObjCampList != null)
                {

                    CampanhaPlanoPrecoEntity ObjEntConfig = new CampanhaPlanoPrecoEntity();
                    List<CampanhaPlanoPrecoEntity> ObjEntConfigList = new List<CampanhaPlanoPrecoEntity>();

                    foreach (CampanhaEntity ObjItemCamp in ObjCampList)
                    {
                        ObjEntConfig = new CampanhaPlanoPrecoEntity();

                        ObjEntConfig.CD_CAMPANHA = ObjItemCamp.CD_CAMPANHA;
                        ObjEntConfig.DS_CAMPANHA = ObjItemCamp.DS_CAMPANHA;
                        ObjEntConfig.CD_PRODUTO = ObjItemCamp.CD_PRODUTO;
                        ObjEntConfig.CD_PRODUTO_DIGITAL = ObjItemCamp.CD_PRODUTO_DIGITAL;

                        //#################################################################################
                        //Monta lista com a campanha/plano
                        //#################################################################################
                        CampanhaPlanoBusiness ObjCampPlanoBLL = new CampanhaPlanoBusiness();
                        List<CampanhaPlanoEntity> ObjCampPlanoList = new List<CampanhaPlanoEntity>();

                        //ObjCampPlanoList = ObjCampPlanoBLL.GetCampanhaPlanoList(ObjItemCamp.CD_CAMPANHA, ObjItemCamp.CD_CANAL);

                        //=======================================================================
                        //Novo correção memória JC
                        //=======================================================================
                        string lstrRetPlano = ObjCampPlanoBLL.GetCampanhaPlanoListService(ObjItemCamp.CD_CAMPANHA, ObjItemCamp.CD_CANAL);

                        if (lstrRetPlano.Trim() != "")
                        {
                            ObjCampPlanoList = (List<CampanhaPlanoEntity>)JsonConvert.DeserializeObject(lstrRetPlano, ObjCampPlanoList.GetType());
                        }
                        //=======================================================================


                        if (ObjCampPlanoBLL.Erro == 0)
                        {

                            ObjEntConfig.CampanhaPlano = ObjCampPlanoList;

                            //#################################################################################
                            //Monta lista com a campanha/preço
                            //#################################################################################

                            List<CampanhaPrecoEntity> ObjCampPrecoEntList = new List<CampanhaPrecoEntity>();

                            if (ObjCampPlanoList != null)
                            {
                                foreach (CampanhaPlanoEntity ObjItemCampPlano in ObjCampPlanoList)
                                {
                                    CampanhaPrecoBusiness ObjCampPrecoBLL = new CampanhaPrecoBusiness();
                                    List<CampanhaPrecoEntity> ObjCampPrecoList = new List<CampanhaPrecoEntity>();

                                    //ObjCampPrecoList = ObjCampPrecoBLL.GetCampanhaPrecoList(ObjItemCampPlano.CD_CAMPANHA, ObjItemCampPlano.CD_PLANO);

                                    //=======================================================================
                                    //Novo correção memória JC
                                    //=======================================================================
                                    string lstrRetPreco = ObjCampPrecoBLL.GetCampanhaPrecoListService(ObjItemCampPlano.CD_CAMPANHA, ObjItemCampPlano.CD_PLANO);

                                    if (lstrRetPreco.Trim() != "")
                                    {
                                        ObjCampPrecoList = (List<CampanhaPrecoEntity>)JsonConvert.DeserializeObject(lstrRetPreco, ObjCampPrecoList.GetType());
                                    }
                                    //=======================================================================

                                    if (ObjCampPrecoBLL.Erro == 0)
                                    {
                                        if (ObjCampPrecoList != null)
                                        {
                                            CampanhaPrecoEntity ObjCampPrecoEnt = new CampanhaPrecoEntity();

                                            foreach (CampanhaPrecoEntity ObjitemCampPreco in ObjCampPrecoList)
                                            {
                                                ObjCampPrecoEntList.Add(ObjitemCampPreco);

                                            }

                                        }

                                    }

                                }

                                ObjEntConfig.CampanhaPreco = ObjCampPrecoEntList;

                                //#################################################################################
                                //fim do loop de campanha/preço
                                //#################################################################################


                                //#################################################################################
                                //Monta lista com o plano
                                //#################################################################################

                                List<PlanoComercialEntity> ObjPlanoComercialEntList = new List<PlanoComercialEntity>();

                                foreach (CampanhaPlanoEntity ObjItemCampPlano in ObjCampPlanoList)
                                {
                                    PlanoComercialBusiness ObjPlanoComercialoBLL = new PlanoComercialBusiness();
                                    PlanoComercialEntity ObjPlanoComercialEnt = new PlanoComercialEntity();

                                    //ObjPlanoComercialEnt = ObjPlanoComercialoBLL.GetPlanoComercial(ObjItemCampPlano.CD_PLANO);

                                    //=======================================================================
                                    //Novo correção memória JC
                                    //=======================================================================
                                    string lstrRetPC = ObjPlanoComercialoBLL.GetPlanoComercialService(ObjItemCampPlano.CD_PLANO);

                                    if (lstrRetPC.Trim() != "")
                                    {
                                        ObjPlanoComercialEnt = (PlanoComercialEntity)JsonConvert.DeserializeObject(lstrRetPC, ObjPlanoComercialEnt.GetType());
                                    }
                                    //=======================================================================

                                    if (ObjPlanoComercialoBLL.Erro == 0)
                                    {
                                        if (ObjPlanoComercialoBLL != null)
                                        {
                                            //Carregar tipo assinatura x entrega
                                            TipoAssinaturaEntregaBusiness ObjTipoAssinaturaEntregaBLL = new TipoAssinaturaEntregaBusiness();
                                            TipoAssinaturaEntregaEntity ObjTipoAssinaturaEntregaEnta = new TipoAssinaturaEntregaEntity();

                                            //ObjTipoAssinaturaEntregaEnta = ObjTipoAssinaturaEntregaBLL.GetTipoAssinaturaEntrega(ObjPlanoComercialEnt.CD_TIPO_ENTREGA, ObjPlanoComercialEnt.CD_TIPO_ASSINATURA);

                                            //=======================================================================
                                            //Novo correção memória JC
                                            //=======================================================================
                                            string lstrRetTipoAss = ObjTipoAssinaturaEntregaBLL.GetTipoAssinaturaEntregaService(ObjPlanoComercialEnt.CD_TIPO_ENTREGA, ObjPlanoComercialEnt.CD_TIPO_ASSINATURA);

                                            if (lstrRetTipoAss.Trim() != "")
                                            {
                                                ObjTipoAssinaturaEntregaEnta = (TipoAssinaturaEntregaEntity)JsonConvert.DeserializeObject(lstrRetTipoAss, ObjTipoAssinaturaEntregaEnta.GetType());
                                            }
                                            //=======================================================================


                                            if (ObjTipoAssinaturaEntregaEnta != null)
                                            {
                                                ObjPlanoComercialEnt.TipoAssinaturaEntrega = ObjTipoAssinaturaEntregaEnta;

                                            }

                                        }

                                    }

                                    ObjPlanoComercialEntList.Add(ObjPlanoComercialEnt);

                                }

                                ObjEntConfig.PlanoComercial = ObjPlanoComercialEntList;
                            }
                        }
                        else
                        {
                            //Sem campanha plano
                        }
                        //#################################################################################
                        //fim do loop de campanha
                        //#################################################################################

                        ObjEntConfigList.Add(ObjEntConfig);

                    }

                    string lstrRet = JsonConvert.SerializeObject(ObjEntConfigList, Formatting.None);

                    return lstrRet;
                }
                else
                {
                    _erro = -99;
                    _msgErro = "GetCampanhaPlanoPreco - Não a campanha parametrizada para venda na internet.";

                    return null;
                }

                

            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = "GetCampanhaPlanoPreco - " + ex.Message;

                return null;
            }

        }

        public string GetCampanhaPlanoPreco(int pintIDCamp, int pintIDPlano)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                CampanhaPlanoPrecoEntity ObjEntConfig;
                List<CampanhaPlanoPrecoEntity> ObjEntConfigList = new List<CampanhaPlanoPrecoEntity>();

                //Recuperada descrição da campanha validando se é do canal web
                CampanhaBusiness ObjCampBLL = new CampanhaBusiness();
                CampanhaEntity ObjCampEnt = new CampanhaEntity();
               
                string lstrRetCamp = ObjCampBLL.GetCampanhaEntService(pintIDCamp);

                if (lstrRetCamp.Trim() != "")
                {
                    ObjCampEnt = (CampanhaEntity)JsonConvert.DeserializeObject(lstrRetCamp, ObjCampEnt.GetType());
                }

                //===============================================================

                if (ObjCampEnt != null)
                {
                    ObjEntConfig = new CampanhaPlanoPrecoEntity();

                    ObjEntConfig.CD_CAMPANHA = ObjCampEnt.CD_CAMPANHA;
                    ObjEntConfig.DS_CAMPANHA = ObjCampEnt.DS_CAMPANHA;


                    //#################################################################################
                    //Monta lista com a campanha/plano
                    //#################################################################################
                    CampanhaPlanoEntity ObjCampPlanoEnt = new CampanhaPlanoEntity();
                    List<CampanhaPlanoEntity> ObjCampPlanoList = new List<CampanhaPlanoEntity>();

                    ObjCampPlanoEnt.CD_CAMPANHA = pintIDCamp;
                    ObjCampPlanoEnt.CD_PLANO = pintIDPlano;

                    ObjCampPlanoList.Add(ObjCampPlanoEnt);

                    ObjEntConfig.CampanhaPlano = ObjCampPlanoList;
                    //#################################################################################

                    //#################################################################################
                    //Monta lista com a campanha/preço
                    //#################################################################################
                    List<CampanhaPrecoEntity> ObjCampPrecoEntList = new List<CampanhaPrecoEntity>();

                    CampanhaPrecoBusiness ObjCampPrecoBLL = new CampanhaPrecoBusiness();
                    List<CampanhaPrecoEntity> ObjCampPrecoList = new List<CampanhaPrecoEntity>();

                    //ObjCampPrecoList = ObjCampPrecoBLL.GetCampanhaPrecoList(pintIDCamp, pintIDPlano);

                    string lstrRetCampPreco = ObjCampPrecoBLL.GetCampanhaPrecoListService(pintIDCamp, pintIDPlano);

                    if (lstrRetCampPreco.Trim() != "")
                    {
                        ObjCampPrecoList = (List<CampanhaPrecoEntity>)JsonConvert.DeserializeObject(lstrRetCampPreco, ObjCampPrecoList.GetType());
                    }

                    if (ObjCampPrecoBLL.Erro == 0)
                    {
                        if (ObjCampPrecoList != null)
                        {
                            CampanhaPrecoEntity ObjCampPrecoEnt = new CampanhaPrecoEntity();

                            foreach (CampanhaPrecoEntity ObjitemCampPreco in ObjCampPrecoList)
                            {
                                ObjCampPrecoEntList.Add(ObjitemCampPreco);
                            }

                        }
                    }

                    ObjEntConfig.CampanhaPreco = ObjCampPrecoEntList;
                    //#################################################################################
                    //fim do loop de campanha/preço
                    //#################################################################################

                    //#################################################################################
                    //Monta lista com o plano
                    //#################################################################################

                    List<PlanoComercialEntity> ObjPlanoComercialEntList = new List<PlanoComercialEntity>();

                    foreach (CampanhaPlanoEntity ObjItemCampPlano in ObjCampPlanoList)
                    {
                        PlanoComercialBusiness ObjPlanoComercialoBLL = new PlanoComercialBusiness();
                        PlanoComercialEntity ObjPlanoComercialEnt = new PlanoComercialEntity();

                        //ObjPlanoComercialEnt = ObjPlanoComercialoBLL.GetPlanoComercial(ObjItemCampPlano.CD_PLANO);

                        string lstrRetPlanoComercial = ObjPlanoComercialoBLL.GetPlanoComercialService(ObjItemCampPlano.CD_PLANO);

                        if (lstrRetPlanoComercial.Trim() != "")
                        {
                            ObjPlanoComercialEnt = (PlanoComercialEntity)JsonConvert.DeserializeObject(lstrRetPlanoComercial, ObjPlanoComercialEnt.GetType());
                        }

                        if (ObjPlanoComercialoBLL.Erro == 0)
                        {
                            if (ObjPlanoComercialoBLL != null)
                            {
                                //Carregar tipo assinatura x entrega
                                TipoAssinaturaEntregaBusiness ObjTipoAssinaturaEntregaBLL = new TipoAssinaturaEntregaBusiness();
                                TipoAssinaturaEntregaEntity ObjTipoAssinaturaEntregaEnt = new TipoAssinaturaEntregaEntity();

                                //ObjTipoAssinaturaEntregaEnta = ObjTipoAssinaturaEntregaBLL.GetTipoAssinaturaEntrega(ObjPlanoComercialEnt.CD_TIPO_ENTREGA, ObjPlanoComercialEnt.CD_TIPO_ASSINATURA);

                                string lstrTipoAssinaturaEntrega = ObjPlanoComercialoBLL.GetPlanoComercialService(ObjItemCampPlano.CD_PLANO);

                                if (lstrTipoAssinaturaEntrega.Trim() != "")
                                {
                                    ObjTipoAssinaturaEntregaEnt = (TipoAssinaturaEntregaEntity)JsonConvert.DeserializeObject(lstrTipoAssinaturaEntrega, ObjTipoAssinaturaEntregaEnt.GetType());
                                }

                                if (ObjTipoAssinaturaEntregaEnt != null)
                                {
                                    ObjPlanoComercialEnt.TipoAssinaturaEntrega = ObjTipoAssinaturaEntregaEnt;
                                }

                            }

                        }

                        ObjPlanoComercialEntList.Add(ObjPlanoComercialEnt);
                    }

                    ObjEntConfig.PlanoComercial = ObjPlanoComercialEntList;

                }
                else
                {
                    _erro = -99;
                    _msgErro = "Não a campanha/plano parametrizada para venda na internet. ( " + pintIDCamp.ToString() + "/"  + pintIDPlano.ToString() + " )";

                    return null;
                }

                //Monta retorno final
                ObjEntConfigList.Add(ObjEntConfig);


                string lstrRet = JsonConvert.SerializeObject(ObjEntConfigList, Formatting.None);
                return lstrRet;

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
