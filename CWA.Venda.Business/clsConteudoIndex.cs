using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

using CWA.Venda.Entity;
using CWA.Venda.Data;
using CWA.EngineServices;

using CWA.Util;

namespace CWA.Venda.Business
{
    public class ConteudoIndexBusiness
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


        public string GetConteudoDestaque(string pstrPathConteudo, string pstrPathImgMKT)
        {

            try
            {
                string lstrConteudoTemplate = string.Empty;

                Utility.SetLogXML("GetConteudoDestaque", "Erro", pstrPathConteudo, false, 1);

                using (StreamReader streamReader = new StreamReader(pstrPathConteudo, Encoding.UTF8))
                {
                    lstrConteudoTemplate = streamReader.ReadToEnd();
                }

                //MONTA CONTEUDO DESTAQUE

                string lstrConteudoDestaque = ""; 

                if (lstrConteudoTemplate != "")
                {

                    List<CampanhaPlanoWEBEntity> lColEnt = new List<CampanhaPlanoWEBEntity>();

                    CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                    lColEnt = ObjData.GetCampanhaPlanoWEBConteudo(1);

                    if (ObjData.Erro == 0)
                    {
                        int lintControle = 0;

                        foreach (CampanhaPlanoWEBEntity lObjEnt in lColEnt)
                        {

                            lstrConteudoDestaque = lstrConteudoDestaque + lstrConteudoTemplate;


                            lstrConteudoDestaque = lstrConteudoDestaque.Replace("@ContItem_item", lintControle.ToString());
                            if (lintControle == 0 )
	                        {
                                lstrConteudoDestaque = lstrConteudoDestaque.Replace("@ClassItem", "item active");
                            }
                            else
                            {
                                lstrConteudoDestaque = lstrConteudoDestaque.Replace("@ClassItem", "item");
                            }

                            //IMAGEM MKT
                            lstrConteudoDestaque = lstrConteudoDestaque.Replace("@PathImgMKT", pstrPathImgMKT + lObjEnt.CD_IMG_MKT);

                            //TITULO 
                            lstrConteudoDestaque = lstrConteudoDestaque.Replace("@TituloMKT", lObjEnt.DS_TIT_MKT);

                            //VALOR
                            lstrConteudoDestaque = lstrConteudoDestaque.Replace("@ValorMKT", "");

                            //DESCRICAO
                            lstrConteudoDestaque = lstrConteudoDestaque.Replace("@DescricaoMKT", lObjEnt.DS_MKT);

                            //LINK QUERO ASSINAR
                            string lstrLink = "Privacidade.aspx?IDProd=" + lObjEnt.CD_PRODUTO.ToString() + "&IDCamp=" + lObjEnt.CD_CAMPANHA.ToString() + "&IDPlano=" + lObjEnt.CD_PLANO;
                            lstrConteudoDestaque = lstrConteudoDestaque.Replace("@LinkQueroAssinar", lstrLink);

                            //LINK DETALHE
                            lstrConteudoDestaque = lstrConteudoDestaque.Replace("@IDCampanha", lObjEnt.CD_CAMPANHA.ToString());
                            lstrConteudoDestaque = lstrConteudoDestaque.Replace("@IDPlano", lObjEnt.CD_PLANO.ToString());

                            lintControle++;
                        }


                    }
                    else
                    {
                        _erro = ObjData.Erro;
                        _msgErro = ObjData.MsgErro;
                    }

                }

                return lstrConteudoDestaque;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "";
            }

        }

        public string GetConteudoPromocao(string pstrPathConteudo, string pstrPathImgMKT)
        {

            try
            {
                string lstrConteudoTemplate = string.Empty;

                Utility.SetLogXML("GetConteudoPromocao", "Erro", pstrPathConteudo, false, 1);

                using (StreamReader streamReader = new StreamReader(pstrPathConteudo, Encoding.UTF8))
                {
                    lstrConteudoTemplate = streamReader.ReadToEnd();
                }

                //MONTA CONTEUDO PROMOÇÕES

                string lstrConteudoPromocao = "";

                if (lstrConteudoTemplate != "")
                {

                    List<CampanhaPlanoWEBEntity> lColEnt = new List<CampanhaPlanoWEBEntity>();

                    CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                    lColEnt = ObjData.GetCampanhaPlanoWEBConteudo(0);

                    if (ObjData.Erro == 0)
                    {
                        int lintControle = 0;
                        //int lintContDIV = 0;

                        lstrConteudoPromocao = "";

                        foreach (CampanhaPlanoWEBEntity lObjEnt in lColEnt)
                        {


                            lstrConteudoPromocao = lstrConteudoPromocao + lstrConteudoTemplate;


                            lstrConteudoPromocao = lstrConteudoPromocao.Replace("@ContItem_item", lintControle.ToString());

                            //IMAGEM MKT
                            lstrConteudoPromocao = lstrConteudoPromocao.Replace("@PathImgMKT", pstrPathImgMKT + lObjEnt.CD_IMG_MKT);

                            //TITULO 
                            lstrConteudoPromocao = lstrConteudoPromocao.Replace("@TituloMKT", lObjEnt.DS_TIT_MKT);

                            //VALOR
                            lstrConteudoPromocao = lstrConteudoPromocao.Replace("@ValorMKT", "");

                            //DESCRICAO
                            lstrConteudoPromocao = lstrConteudoPromocao.Replace("@DescricaoMKT", lObjEnt.DS_MKT);

                            //LINK QUERO ASSINAR
                            string lstrLink = "Privacidade.aspx?IDProd=" + lObjEnt.CD_PRODUTO.ToString() + "&IDCamp=" + lObjEnt.CD_CAMPANHA.ToString() + "&IDPlano=" + lObjEnt.CD_PLANO;
                            lstrConteudoPromocao = lstrConteudoPromocao.Replace("@LinkQueroAssinar", lstrLink);

                            //LINK DETALHE
                            lstrConteudoPromocao = lstrConteudoPromocao.Replace("@IDCampanha", lObjEnt.CD_CAMPANHA.ToString());
                            lstrConteudoPromocao = lstrConteudoPromocao.Replace("@IDPlano", lObjEnt.CD_PLANO.ToString());

                            lintControle++;
                            //lintContDIV++; 
                          
                        }


                    }
                    else
                    {
                        _erro = ObjData.Erro;
                        _msgErro = ObjData.MsgErro;
                    }

                }

                return lstrConteudoPromocao;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "";
            }

        }

        public string GetConteudoDetalhe(string pstrPathConteudo, string pstrPathImgMKT, 
                                         int pintIDCampanha, int pintIDPlano)
        {

            try
            {
                string lstrConteudoTemplate = string.Empty;

                Utility.SetLogXML("GetConteudoDetalhe", "Erro", pstrPathConteudo, false, 1);

                using (StreamReader streamReader = new StreamReader(pstrPathConteudo, Encoding.UTF8))
                {
                    lstrConteudoTemplate = streamReader.ReadToEnd();
                }

                //MONTA CONTEUDO DETALHE
                string lstrConteudoDetalhe = "";

                if (lstrConteudoTemplate != "")
                {

                    CampanhaPlanoWEBEntity lObjEnt = new CampanhaPlanoWEBEntity();

                    CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                    lObjEnt = ObjData.GetCampanhaPlanoWEB(pintIDCampanha, pintIDPlano);

                    if (ObjData.Erro == 0)
                    {

                        lstrConteudoDetalhe = "";

                        lstrConteudoDetalhe = lstrConteudoDetalhe + lstrConteudoTemplate;

                        //IMAGEM MKT
                        lstrConteudoDetalhe = lstrConteudoDetalhe.Replace("@PathImgMKT", pstrPathImgMKT + lObjEnt.CD_IMG_MKT);

                        //TITULO 
                        lstrConteudoDetalhe = lstrConteudoDetalhe.Replace("@TituloMKT", lObjEnt.DS_TIT_MKT);

                        //VALOR
                        lstrConteudoDetalhe = lstrConteudoDetalhe.Replace("@ValorMKT", "");

                        //DESCRICAO
                        lstrConteudoDetalhe = lstrConteudoDetalhe.Replace("@DetalheMKT", lObjEnt.DS_DET_MKT);

                        //LINK QUERO ASSINAR
                        string lstrLink = "Privacidade.aspx?IDProd=" + lObjEnt.CD_PRODUTO.ToString() + "&IDCamp=" + lObjEnt.CD_CAMPANHA.ToString() + "&IDPlano=" + lObjEnt.CD_PLANO;
                        lstrConteudoDetalhe = lstrConteudoDetalhe.Replace("@LinkQueroAssinar", lstrLink);



                    }
                    else
                    {
                        _erro = ObjData.Erro;
                        _msgErro = ObjData.MsgErro;
                    }

                }

                return lstrConteudoDetalhe;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "";
            }

        }

        public string GetMontarBannerDestaque(){

            try
            {

                    List<CampanhaPlanoWEBEntity> lColEnt = new List<CampanhaPlanoWEBEntity>();
                    CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                    lColEnt = ObjData.GetCampanhaPlanoWEBConteudo(1);

                    string lstrConteudoBanner = "";

                    if (ObjData.Erro == 0)
                    {

                        for (int Index = 0; Index < lColEnt.Count; Index++)
                        {
                            if (Index == 0)
                            {
                                lstrConteudoBanner = lstrConteudoBanner + "<li data-target='#carousel' data-slide-to='" + Index.ToString() + "' class='active'></li>";
                            }
                            else
                            {
                                lstrConteudoBanner = lstrConteudoBanner + "<li data-target='#carousel' data-slide-to='" + Index.ToString() + "'></li>";
                            }
                        }
                    }
                    else
                    {
                        _erro = ObjData.Erro;
                        _msgErro = ObjData.MsgErro;
                    }

                    return lstrConteudoBanner;

            }
            catch (Exception)
            {
                
                return "";
            }

        }

        public string GetConteudo(int pintTipoConteudo,
                                  string pstrPathConteudo = "", 
                                  string pstrPathImgMKT= "",
                                  int pintIDCampanha = 0, 
                                  int pintIDPlano = 0)
        {
            try
            {
                
                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                if (pintTipoConteudo == 1) // ConteudoDestaque
                {
                    lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetConteudoDestaque;" + pstrPathConteudo + "," + pstrPathImgMKT;

                    ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                    lstrVetRet = ObjInterface.VertorRetorno;

                    if (lstrVetRet != null)
                    {
                        if (lstrVetRet[0] == "0")
                        {
                            lstrRet = lstrVetRet[2];
                        }

                    }

                    return lstrRet;
                }
                else if (pintTipoConteudo == 2)
                {
                    lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetConteudoPromocao;" + pstrPathConteudo + "," + pstrPathImgMKT;

                    ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                    lstrVetRet = ObjInterface.VertorRetorno;

                    if (lstrVetRet != null)
                    {
                        if (lstrVetRet[0] == "0")
                        {
                            lstrRet = lstrVetRet[2];
                        }

                    }

                    return lstrRet;
                }                
                else if (pintTipoConteudo == 3)
                {
                    lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetConteudoDetalhe;" + pstrPathConteudo + "," + pstrPathImgMKT + "," + pintIDCampanha + "," + pintIDPlano;

                    ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                    lstrVetRet = ObjInterface.VertorRetorno;

                    if (lstrVetRet != null)
                    {
                        if (lstrVetRet[0] == "0")
                        {
                            lstrRet = lstrVetRet[2];
                        }

                    }

                    return lstrRet;
                }
                else if (pintTipoConteudo == 4)
                {
                    lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetMontarBannerDestaque;";

                    ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                    lstrVetRet = ObjInterface.VertorRetorno;

                    if (lstrVetRet != null)
                    {
                        if (lstrVetRet[0] == "0")
                        {
                            lstrRet = lstrVetRet[2];
                        }

                    }

                    return lstrRet;
                }

                return "";
            }
            catch (Exception ex)
            {
                
                _erro = -99;
                _msgErro = ex.Message;
                return "";
                
            }
        }


    }
}
