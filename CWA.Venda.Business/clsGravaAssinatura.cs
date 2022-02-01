using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

using CWA.Venda.Data;
using CWA.Venda.Entity;
using CWA.EngineServices;
using CWA.Util;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CWA.Venda.Business
{
    public class GravaAssinaturaBusiness
    {
        private System.Int32 _erro;
        private string _msgErro;
        private string _lstrCTR;
        private string _lstrInfoPlano;
        private Int32 _IDLogRastreio;

        public System.Int32 Erro
        {
            get { return _erro; }
        }

        public string MsgErro
        {
            get { return _msgErro; }
        }

        public string CTR
        {
            get { return _lstrCTR; }
        }

        public string InfoPlano
        {
            get { return _lstrInfoPlano; }
        }

        public System.Int32 IDLogRastreio
        {
            get { return _IDLogRastreio; }
        }

        public void GravarAssinatura(DadosPessoaEntity ObjPS,
                                     DadosEntregaEntity ObjDE,
                                     DadosPagamentoEntity ObjPG)
        {

            _erro = 0;
            _msgErro = "";
            _lstrCTR = "";
            _lstrInfoPlano = "";
            _IDLogRastreio = 0;


            try
            {

                    //################################################################################################
                    //para produto digital e se os dados do logradouro forem NULL vou pegar os dados do endereço
                    //padrão na parametro global.
                    //################################################################################################

                    List<ProdutoEntity> ObjProdEnt = new List<ProdutoEntity>();
                    ProdutoBusiness ObjProdBLL = new ProdutoBusiness();

                    string lstrRetProdEnt = ObjProdBLL.GetProdutoListService(ObjPG.CD_PRODUTO);

                    if (lstrRetProdEnt != "")
                    {
                        ObjProdEnt = (List<ProdutoEntity>)JsonConvert.DeserializeObject(lstrRetProdEnt, ObjProdEnt.GetType());
                    }

                    if (ObjProdEnt != null)
                    {
                        if (ObjProdEnt[0].ST_IND_ONLINE == 1) //DIGITAL
                        {
                            //LOGRADOURO DE PRODUTO ONLINE SEM PREENCHIMENTO
                            if ((ObjDE.ID_LOGR == 0) && (ObjDE.DS_LOGR == null))
                            {
                                ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                                ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                                ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                                if (ObjParamEnt != null)
                                {
                                    if ((ObjDE.ST_LOGR_EXT != 0) && (ObjDE.ST_LOGR_EXT != 1))
                                    {
                                        _erro = -1;
                                        _msgErro = "Para endereço não informado é obrigatório informar se o mesmo e dentro ou fora do País";

                                        return;
                                    }

                                    ViewLogrBusiness ObjLogrBLL = new ViewLogrBusiness();
                                    List<DadosEntregaEntity> ObjLogrList = new List<DadosEntregaEntity>();

                                    string lstrRetLogrEnt = "";

                                    if (ObjDE.ST_LOGR_EXT == 0)
                                    {
                                        lstrRetLogrEnt = ObjLogrBLL.GetViewLogrEntityServiceID(ObjParamEnt.CD_LOGR_PADRAO_EXTERIOR);
                                    }
                                    else if (ObjDE.ST_LOGR_EXT == 1)
                                    {
                                        lstrRetLogrEnt = ObjLogrBLL.GetViewLogrEntityServiceID(ObjParamEnt.CD_LOGR_PADRAO_BRASIL);
                                    }

                                    if (lstrRetLogrEnt == "")
                                    {
                                        _erro = -1;

                                        if (ObjDE.ST_LOGR_EXT == 0)
                                        {
                                            _msgErro = "Erro ao obter logradouro padrão do Exterior";
                                        }
                                        else
                                        {
                                            _msgErro = "Erro ao obter logradouro padrão do Brasil";
                                        }

                                        return;

                                    }

                                    ObjLogrList = (List<DadosEntregaEntity>)JsonConvert.DeserializeObject(lstrRetLogrEnt, ObjLogrList.GetType());

                                    if (ObjLogrList != null)
                                    {
                                        ObjDE.ID_LOGR = ObjLogrList[0].ID_LOGR;
                                        ObjDE.DS_TIPO = ObjLogrList[0].DS_TIPO;
                                        ObjDE.CD_TIPO = ObjLogrList[0].CD_TIPO;
                                        ObjDE.DS_LOGR = ObjLogrList[0].DS_LOGR;
                                        ObjDE.DS_MUNICIPIO = ObjLogrList[0].DS_MUNICIPIO;
                                        ObjDE.DS_BAIRRO = ObjLogrList[0].DS_BAIRRO;
                                        ObjDE.DS_UF = ObjLogrList[0].DS_UF;
                                        ObjDE.NU_CEP = ObjLogrList[0].NU_CEP;
                                        ObjDE.NU_RESID = "1";
                                    }

                                }
                                else
                                {
                                    _erro = -1;
                                    _msgErro = "Erro ao obter dados do parâmetro goloba ed (1)";

                                    return;
                                }
                            }
                            else
                            {

                                if ((ObjDE.ID_LOGR != 0) && (ObjDE.DS_LOGR != null))
                                {

                                }
                                else
                                {
                                    ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                                    ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                                    ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                                    if (ObjParamEnt != null)
                                    {
                                        if ((ObjDE.ST_LOGR_EXT != 0) && (ObjDE.ST_LOGR_EXT != 1))
                                        {
                                            _erro = -1;
                                            _msgErro = "Para endereço não informado é obrigatório informar se o mesmo e dentro ou fora do País";

                                            return;
                                        }

                                        if (ObjDE.ST_LOGR_EXT == 0)
                                        {
                                            ObjDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_EXTERIOR;
                                        }
                                        else if (ObjDE.ST_LOGR_EXT == 1)
                                        {
                                            ObjDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_BRASIL;
                                        }

                                    }
                                    else
                                    {
                                        _erro = -1;
                                        _msgErro = "Erro ao obter dados do parâmetro goloba ed (2)";

                                        return;
                                    }

                                }
                            }
                        }
                        else //IMPRESSO
                        {
                            if (ObjDE == null)
                            {
                                _erro = -1;
                                _msgErro = "Erro ao converter JSON de Entrega";

                                return;
                            }

                            if ((ObjDE.ID_LOGR == 0) && (ObjDE.DS_LOGR != null))
                            {
                                ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                                ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                                ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                                if (ObjParamEnt != null)
                                {
                                    if ((ObjDE.ST_LOGR_EXT == 0))
                                    {
                                        _erro = -1;
                                        _msgErro = "Não é possível assinar um produto impresso para endereço digitado do exterior.";

                                        return;
                                    }

                                ObjDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_BRASIL;
                                }
                            }
                        }
                    }
                    else
                    {
                        _erro = -1;
                        _msgErro = "Erro ao localizar produto (validação de produto online)";

                        return;
                    }



                    string lstrTipoParcCartao = "";

                   //CARTAO
                   if (ObjPG.CD_TP_FORMA_PAG == 6 )
                   {
                       lstrTipoParcCartao = ConfigurationManager.AppSettings["TipoParcCartao"];

                       if (lstrTipoParcCartao.Trim() == "")
                       {
                           _erro = -1;
                           _msgErro = "Tipo de Parcelamento do Cartão não parametrizado.";

                           Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                           return;
                       }

                       ObjPS.TIPO_PARC_CARTAO = int.Parse(lstrTipoParcCartao);

                   }
                   else
                   {
                       ObjPS.TIPO_PARC_CARTAO = 0;
                   }

                   PlanoComercialBusiness ObjPlanoBLL = new PlanoComercialBusiness();
                   PlanoComercialWebEntity ObjPlanoEnt = new PlanoComercialWebEntity();
                   PlanoComercialWebEntity ObjPlanoEntLog = new PlanoComercialWebEntity();

                   //Retorna na variavel InfoPlano para ser utilizada nas mensagens final.

                   ObjPlanoEnt = ObjPlanoBLL.GetParametrosPlano(ObjPG.CD_PLANO);

                   ObjPlanoEntLog.CD_PLANO  = ObjPG.CD_PLANO;

                   if (ObjPlanoEnt == null)
                   {
                       _erro = ObjPlanoBLL.Erro;
                       _msgErro = ObjPlanoBLL.MsgErro;

                       Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                       return;
                   }

                   _lstrInfoPlano = ObjPlanoEnt.DS_PLANO + "|" + ObjPlanoEnt.DS_TP_PAG + "|" +
                                    ObjPlanoEnt.DIAS_ENTREGA + "|" + ObjPlanoEnt.DS_TP_ASSINATURA + "|" +
                                    ObjPlanoEnt.NU_PARCELA;

                    //Verifica se existe preco parametrizado para a campanha/plano

                    ObjPlanoEnt = ObjPlanoBLL.GetValorPlano(ObjPG.CD_CAMPANHA, ObjPG.CD_PLANO, ObjDE.ID_LOGR);

                    if (ObjPlanoEnt == null)
                    {
                        _erro = ObjPlanoBLL.Erro;
                        _msgErro = ObjPlanoBLL.MsgErro;

                        Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                        return;
                    }

                    ObjPlanoEntLog.VA_TOTAL_CENT = ObjPlanoEnt.VA_TOTAL_CENT;
                    ObjPlanoEntLog.VA_PARC_CENT = ObjPlanoEnt.VA_PARC_CENT;

                    string lstrValor1ParcelaCentavos = ObjPlanoEnt.VA_PARC_CENT;
                    string lstrValorTotalCentavos = ObjPlanoEnt.VA_TOTAL_CENT;

                    //Retorna na valor do plano para ser utilizada nas mensagens final.
                    string lstrValorParc1 = (double.Parse(ObjPlanoEnt.VA_PARC_CENT) / 100).ToString("0.00");
                    string lstrValorParcDemais = (double.Parse(ObjPlanoEnt.VA_DEMAIS_PARC_CENT) / 100).ToString("0.00");
                    string lstrValorTotal = (double.Parse(ObjPlanoEnt.VA_TOTAL_CENT) / 100).ToString("0.00");

                    _lstrInfoPlano = _lstrInfoPlano + "|" + lstrValorTotal + "|" + lstrValorParc1 + "|" + lstrValorParcDemais;
    
                    //10-@VALORTOTAL/11-@VALORPRIMPARC/12-@VALORDEMAISPARC
                    //===========================================================================


                    GravaAssinaturaData ObjData = new GravaAssinaturaData();

                    //===========================================================================
                    //PEGA O ULTIMO CONTRATO
                    //===========================================================================
                    string [] lstrVetorCTR;
                    string lstrCTRTemp;

                    string lstrSerieCTR = "";
                    int lintNuCTR = 0;
                    int lintNuDvCTR = 0;

                    lstrCTRTemp = ObjData.GetProximoContrato("VD", 0, true);

                    if (lstrCTRTemp.Trim() != "")
                    {
                        lstrVetorCTR = lstrCTRTemp.Split(';');

                        if (lstrVetorCTR.Length > 0)
                        {
                            lstrSerieCTR = lstrVetorCTR[0];
                            lintNuCTR = int.Parse(lstrVetorCTR[1]);
                            lintNuDvCTR = int.Parse(lstrVetorCTR[2]);

                            _lstrCTR = lstrCTRTemp;
                        }
                    }
                    else
                    {
                        _erro = -1;
                        _msgErro = "Erro Novo Contrato - Não foi possível obter um contrato para essa solicitação.";

                        Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                        return;
                    }
                    //===========================================================================

                    bool lbolGetway = bool.Parse(ConfigurationManager.AppSettings["UsaGetwayPagamento"]);
                    string lstrRetGetway = "";

                    //Grava Primeira parte do Log de Rastreio.

                    LogInterfaceWEBBusiness ObjLogInterfaceBLL = new LogInterfaceWEBBusiness();
                    ObjLogInterfaceBLL.SetLogInterface(0, ObjPS, ObjDE, ObjPG, ObjPlanoEntLog,null, "GravarVenda", "", "Venda", lstrSerieCTR, lintNuCTR, lintNuDvCTR, lbolGetway, "", "");

                    _IDLogRastreio = ObjLogInterfaceBLL.IDLogRatreamento;

                    //===========================================================================


                    bool lbolAdmCobraGateWay = false;

                    if (lbolGetway == true)
                    {

                        if (ObjPG.CD_TP_FORMA_PAG == 6)
                        {
                            AdmCartaoBusiness ObjBLLAdmCartao = new AdmCartaoBusiness();
                            lbolAdmCobraGateWay = ObjBLLAdmCartao.GetAdmCobraGateWay(ObjPG.CD_FONTE_COBRANCA);
                        }
                        
                    }


                    DataContext.AbrirConexao();
                    DataContext.BeginTransaction();

                    //GRAVA DADOS PESSOAIS
                    ObjData.SetDadosPessoa(ObjPS, ObjDE, ObjPG, lstrSerieCTR, lintNuCTR, lintNuDvCTR, true, false);
                    if (ObjData.Erro != 0)
                    {
                        _erro = ObjData.Erro;
                        _msgErro = ObjData.MsgErro;

                        DataContext.RollbackTransaction();
                        DataContext.FecharConexao();

                        return;
                    }
                    //===========================================================================

                    //GRAVA DADOS TELEFONE RESIDENCIAL
                    if ( (ObjPS.NU_TEL != null) && (ObjPS.NU_TEL.Trim() != ""))
                    {
                        ObjData.SetDadosTelefone(ObjPS, lstrSerieCTR, lintNuCTR, lintNuDvCTR, 0, true, false);
                        if (ObjData.Erro != 0)
                        {
                            _erro = ObjData.Erro;
                            _msgErro = ObjData.MsgErro;

                            DataContext.RollbackTransaction();
                            DataContext.FecharConexao();

                            Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                            return;
                        }

                    }
                    //===========================================================================

                    //GRAVA DADOS TELEFONE CELUAR SE EXISTIR
                    if ((ObjPS.NU_CEL != null) && (ObjPS.NU_CEL.Trim() != ""))
                    {
                        ObjData.SetDadosTelefone(ObjPS, lstrSerieCTR, lintNuCTR, lintNuDvCTR, 1, true, false);

                        if (ObjData.Erro != 0)
                        {
                            _erro = ObjData.Erro;
                            _msgErro = ObjData.MsgErro;

                            DataContext.RollbackTransaction();
                            DataContext.FecharConexao();

                            Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                            return;
                        }
                    }
                    //============================================================================================


                    //Valida se é um plano que grava tabela de DEBITO_AUTOMATICO_CTR
                    if (IsGravaDebito(ObjPG.CD_TP_FORMA_PAG) == true)
                    {
                        //====================================================================
                        //RECUPERA A CHAVE DE CRYPT DO CARTAO CREDITO
                        //====================================================================
                        Utility ObjUtilCript = new Utility();

                        string lstrChaveCryptCC = ObjUtilCript.GetCriptCartaoCIR2000();

                        ObjPG.CHAVE_CRYPT_CC = lstrChaveCryptCC;

                        //====================================================================

                        //GRAVA DADOS DEBITO
                        ObjData.SetDadosDebito(ObjPG, lstrSerieCTR, lintNuCTR, lintNuDvCTR, true, false);

                        if (ObjData.Erro != 0)
                        {
                            _erro = ObjData.Erro;
                            _msgErro = ObjData.MsgErro;

                            DataContext.RollbackTransaction();
                            DataContext.FecharConexao();

                            Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                            return;
                        }
                        //============================================================================================
                    }

                    //INTERFACE COM GETWAY DE PAGAMENTO
                    int lintTotalParcelasPagas = 0;
                    int lintTotalParcelasEnviadas = 0;                     
    
                    if (lbolGetway == true)
                    {
                        if (ObjPG.CD_TP_FORMA_PAG == 6) //CARTAO
                        {
                            if (lbolAdmCobraGateWay == true)
                            {

                               //Valida se a fonte de cobrança carta envia para o gateway
                                string lstrEmpresaGetway = ConfigurationManager.AppSettings["EmpresaGetway"];
                                if (string.IsNullOrEmpty(lstrEmpresaGetway))
                                {
                                    _erro = -1;
                                    _msgErro = "Empresa responsavel por processar o pagamento de cartão não localizada.";

                                    Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                                    return;
                                }
                            

                                string lsParm = "";

                                if (lstrEmpresaGetway.Trim().ToUpper() == "MAXIPAGO")
                                {
                                    lsParm = lsParm + "Venda" + ",";                        //TipoInterface
                                    lsParm = lsParm + lstrSerieCTR + lintNuCTR.ToString("0000000000") + lintNuDvCTR.ToString() + ",";
                                    lsParm = lsParm + ObjPG.NU_CARTAO + ",";
                                    lsParm = lsParm + ObjPG.DT_VALID.Substring(3, 7) + ","; //pegar somente mes/ano
                                    lsParm = lsParm + ObjPG.NU_CVV + ",";

                                    if (lstrTipoParcCartao.Trim() == "1")
                                    {                                                       //PARCELADO PELA ADM
                                        lsParm = lsParm + lstrValorTotal.Replace(',','.') + ",";             //VALOR TOTAL
                                        lsParm = lsParm + ObjPG.NU_PARCELA + ",";           //TOTAL DE PARCELAS 

                                        lintTotalParcelasPagas = ObjPG.NU_PARCELA;
                                        lintTotalParcelasEnviadas = 0;
                                    }
                                    else
                                    {                                                       //PARCELADO PELA JORNAL
                                        lsParm = lsParm + lstrValorParc1.Replace(',', '.') + ",";             //VALOR DA PARCELA
                                        lsParm = lsParm + "1,";                             //PARCELA = 1 (PRIMEIRA)

                                        lintTotalParcelasPagas = 1;
                                        lintTotalParcelasEnviadas = 0;
                                    }

                                    lsParm = lsParm + lstrTipoParcCartao;

                                }
                                else //BRASPAG & ESITEF
                                {
                                    lsParm = lsParm + "Venda" + ",";                        //TipoInterface
                                    lsParm = lsParm + ObjPG.NM_PESSOA_CARTAO + ",";

                                    if (lstrTipoParcCartao.Trim() == "1")
                                    {                                                       //PARCELADO PELA ADM
                                        lsParm = lsParm + lstrValorTotalCentavos + ",";     //VALOR TOTAL
                                        lsParm = lsParm + ObjPG.NU_PARCELA + ",";           //TOTAL DE PARCELAS 

                                        lintTotalParcelasPagas = ObjPG.NU_PARCELA;
                                        lintTotalParcelasEnviadas = 0;
                                    }
                                    else
                                    {                                                       //PARCELADO PELA JORNAL
                                        lsParm = lsParm + lstrValor1ParcelaCentavos + ",";  //VALOR DA PARCELA
                                        lsParm = lsParm + "1,";                             //PARCELA = 1 (PRIMEIRA)

                                        lintTotalParcelasPagas = 1;
                                        lintTotalParcelasEnviadas = 0;
                                    }

                                    lsParm = lsParm + ObjPG.NU_CARTAO + ",";
                                    lsParm = lsParm + ObjPG.NM_PESSOA_CARTAO + ",";
                                    lsParm = lsParm + ObjPG.DT_VALID.Substring(3, 7) + ","; //pegar somente mes/ano
                                    lsParm = lsParm + ObjPG.NU_CVV + ",";
                                    lsParm = lsParm + ObjPG.NM_BANDEIRA + ",";
                                    lsParm = lsParm + lstrSerieCTR + lintNuCTR.ToString("0000000000") + lintNuDvCTR.ToString() + ";";

                                    lsParm = lsParm.Substring(0, lsParm.Length - 1);


                                 }

                                Utility.SetLogXML("GravarAssinatura-InterfacePagto-Envio", "Info", lsParm, false);

                                GerenciaInterface ObjInterface = new GerenciaInterface();
                                ObjInterface.ExecutarInterface("Venda", "Cartao", lsParm);

                                if (ObjInterface.Erro != 0)
                                {
                                    _erro = ObjInterface.Erro;
                                    _msgErro = ObjInterface.MsgErro;

                                    DataContext.RollbackTransaction();
                                    DataContext.FecharConexao();

                                    Utility.SetLogXML("GravarAssinatura-InterfacePagto-Erro-Rollback", "Error", lsParm + " - " + _msgErro, false);

                                    //Atualiza Log com dados de retorno InterfacePagto
                                    if (_IDLogRastreio != 0)
                                    {
                                        if (lbolGetway == true)
                                        {                                            
                                            ObjLogInterfaceBLL.SetUpdLogInterface(lstrRetGetway, _msgErro, _IDLogRastreio);

                                            if (ObjLogInterfaceBLL.Erro != 0)
                                            {
                                                Utility.SetLogXML("GravarAssinatura-GravaLogInterface", "Error", ObjLogInterfaceBLL.MsgErro, false);
                                            }
                                        }
                                    }

                                    return;
                                }

                                //ATUALIZA DADOS PARA UTILIZAR NA BAIXA DAS PARCELAS JÁ APROVADAS.
                                string[] lstrVetRet = ObjInterface.VertorRetorno;

                                lstrRetGetway = "";
                                lstrRetGetway = lstrSerieCTR + "," +
                                                lintNuCTR.ToString() + "," +
                                                lintNuDvCTR.ToString() + "," +
                                                lstrVetRet[2] + "," +
                                                lstrVetRet[3] + "," +
                                                lstrVetRet[4] + "," +
                                                lstrVetRet[5] + "," +
                                                lintTotalParcelasPagas.ToString() + "," +
                                                lintTotalParcelasEnviadas.ToString();

                                Utility.SetLogXML("GravarAssinatura-InterfacePagto-Retorno", "Info", lstrRetGetway, false);

                                ObjData.SetBaixaParcela(lstrSerieCTR,
                                                        lintNuCTR,
                                                        lintNuDvCTR,
                                                        lstrVetRet[2],
                                                        lstrVetRet[3],
                                                        lstrVetRet[4],
                                                        lstrVetRet[5],
                                                        "",
                                                        lintTotalParcelasPagas,
                                                        lintTotalParcelasEnviadas,
                                                        "CardToken",
                                                        true, false);

                                if (ObjData.Erro != 0)
                                {
                                    _erro = DataContext.Erro;
                                    _msgErro = DataContext.MsgErro;

                                    DataContext.RollbackTransaction();
                                    DataContext.FecharConexao();

                                    Utility.SetLogXML("GravarAssinatura-InterfacePagto-SetBaixaParcela", "Error", _msgErro, false);

                                    return;
                                }                                
                            }

                            //============================================================================================

                        }


                    }
                    //============================================================================================

                    //commit 
                    if (Erro == 0)
                    {
                        DataContext.CommitTransaction();
                        DataContext.FecharConexao();

                    }

                    //Atualiza Log com dados de retorno InterfacePagto
                    if (_IDLogRastreio != 0)
                    {
                        if (lbolGetway == true)
                        {
                            ObjLogInterfaceBLL.SetUpdLogInterface(lstrRetGetway, _msgErro, _IDLogRastreio);

                            if (ObjLogInterfaceBLL.Erro != 0 )
                            {
                                Utility.SetLogXML("GravarAssinatura-GravaLogInterface-Log", "Error", ObjLogInterfaceBLL.MsgErro, false);
                            }
                        }
                    }  


                    //Finaliza registro do abandono de carrinho
                    string lstrDOCAbandono = "";
                    string lstrEmailAbandono = "";

                    if ( (ObjPS.TP_PESSOA == 1) || (ObjPS.TP_PESSOA == 2))
                    {
                        lstrDOCAbandono = ObjPS.NU_CPF;
                        lstrEmailAbandono = ObjPS.DS_EMAIL;
                    }
                    else
                    {
                        lstrDOCAbandono = ObjPS.NU_CNPJ;
                        lstrEmailAbandono = ObjPS.DS_EMAIL;
                    }

                    AbandonoBusiness ObjAbandono = new AbandonoBusiness();
                    ObjAbandono.SetAbandonoService(lstrDOCAbandono, lstrEmailAbandono, null, 4, 1, lstrSerieCTR, lintNuCTR, lintNuDvCTR);

                    if (ObjAbandono.Erro != 0)
                    {
                        Utility.SetLogXML("DadosPagtoFinalizar-SetAbandonoService", "Erro", ObjAbandono.MsgErro, false);
                    } 
                
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                Utility.SetLogXML("GravarAssinatura-InterfacePagto", "Error", _msgErro, false);

                DataContext.RollbackTransaction();
                DataContext.FecharConexao();

                if (_IDLogRastreio != 0)
                {
                    LogInterfaceWEBBusiness ObjLogInterfaceBLL = new LogInterfaceWEBBusiness();

                    ObjLogInterfaceBLL.SetUpdLogInterface("", _msgErro, _IDLogRastreio);

                    if (ObjLogInterfaceBLL.Erro != 0)
                    {
                        Utility.SetLogXML("GravarAssinatura-GravaLogInterfaceErro-Ex", "Error", ObjLogInterfaceBLL.MsgErro, false);
                    }
                }


            }

        }


        private bool IsGravaDebito(int pintTipoPagto) 
        {
            try
            {
                
                //3=recibo 4=cort.  5=fatura  6=cartão  7=boleto  8=perm.  9=degust.  10=cheque  11=quitado 
                if ( (pintTipoPagto == 3) || (pintTipoPagto == 4) || 
                     (pintTipoPagto == 5) || (pintTipoPagto == 7) ||
                     (pintTipoPagto == 8) || (pintTipoPagto == 9) ||
                     (pintTipoPagto == 10) ||(pintTipoPagto == 11) 
                   )
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Utility.SetLogXML("IsGravaDebito", "Error", ex.Message, false);

                return false;
            }

        }

        public void GravarAssinaturaService(DadosPessoaEntity ObjPS,
                                            DadosEntregaEntity ObjDE,
                                            DadosPagamentoEntity ObjPG)
        {

            string lstrLogErro = "";

            try
            {

                if (ObjPS == null)
                {
                    _erro = -1;
                    _msgErro = "Objeto de Dados da Pessoa não pode ser nulo";

                    return;
                }

                if (ObjDE == null)
                {
                    _erro = -1;
                    _msgErro = "Objeto de Dados de Endereço não pode ser nulo";

                    return;
                }

                if (ObjPG == null)
                {
                    _erro = -1;
                    _msgErro = "Objeto de Dados de Pagamento não pode ser nulo";

                    return;
                }


                string lstrDadosPessoa = JsonConvert.SerializeObject(ObjPS, Formatting.None);
                string lstrDadosEndereco = JsonConvert.SerializeObject(ObjDE, Formatting.None);
                string lstrDadosPagamento = JsonConvert.SerializeObject(ObjPG, Formatting.None);

                lstrLogErro = lstrLogErro + "SerializeObject - ";

                Utility ObjUtil = new Utility();

                string lstrDadosPessoaCript = ObjUtil.SetCript(lstrDadosPessoa);
                string lstrDadosEnderecoCript = ObjUtil.SetCript(lstrDadosEndereco);
                string lstrDadosPagamentoCript = ObjUtil.SetCript(lstrDadosPagamento);

                lstrLogErro = lstrLogErro + "Encriptação - ";

                //Fazer a Chamada da EngineService passando os JSON Criptografado.

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GravarAssinatura;" + lstrDadosPessoaCript + ";" + lstrDadosEnderecoCript + ";" + lstrDadosPagamentoCript;

                lstrLogErro = lstrLogErro + "Chamada do  - ObjInterface.ExecutarInterfac -  " + lstrParametroMetros;

                ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                lstrVetRet = ObjInterface.VertorRetorno;

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        //Tratar Retorno e setar as variaveis da classe.
                        lstrRet = lstrVetRet[2];

                        lstrLogErro = lstrLogErro + "Tratar Retorno e setar as variaveis da classe. - " + lstrRet;
                    }

                }

                if (lstrRet != "")
                {
                    string[] lstrVetorRet = lstrRet.Split(';');

                    lstrLogErro = lstrLogErro + "Retorno 1 - " + lstrRet;

                    if (lstrVetorRet != null)
                    {
                        _erro = int.Parse(lstrVetorRet[0]);
                        _msgErro = lstrVetorRet[1];

                        if (lstrVetorRet.Length >= 5 )
                        {
                            _lstrCTR = lstrVetorRet[2] + ";" + lstrVetorRet[3] + ";" + lstrVetorRet[4];
                            _lstrInfoPlano = lstrVetorRet[5];

                        }

                        lstrLogErro = lstrLogErro + "Retorno 1.1 - " + _erro.ToString() + " - " + _msgErro + " - " + _lstrCTR + " - " + _lstrInfoPlano;
                    }

                    return;

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao Gravar Assinatura - WCF/Engine";

                    lstrLogErro = lstrLogErro + "Erro ao Gravar Assinatura - WCF/Engine";

                    return;
                }

            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = "GravarAssinaturaService - Erro ao preparar dados JSON - " + " - " + lstrLogErro  + " - " + ex.Message;  
            }

        }

        public string GravarAssinaturaIntegracao(string pstrDadosPessoa,
                                                 string pstrDadosEndereco,
                                                 string pstrDadosPagamento,
                                                 string pstrDadosGateway)
        {

            _erro = 0;
            _msgErro = "";
            _lstrCTR = "";
            _lstrInfoPlano = "";
            _IDLogRastreio = 0;


            try
            {

                //################################################################################################
                //Valida dos dados JSON passado e transforma em Objeto para Gravar a Assinatura
                //################################################################################################

                DadosPessoaEntity ObjEntPS = new DadosPessoaEntity();
                ObjEntPS = (DadosPessoaEntity)JsonConvert.DeserializeObject(pstrDadosPessoa, ObjEntPS.GetType());

                DadosEntregaEntity ObjEntDE = new DadosEntregaEntity();
                ObjEntDE = (DadosEntregaEntity)JsonConvert.DeserializeObject(pstrDadosEndereco, ObjEntDE.GetType());

                DadosPagamentoEntity ObjEntPG = new DadosPagamentoEntity();
                ObjEntPG = (DadosPagamentoEntity)JsonConvert.DeserializeObject(pstrDadosPagamento, ObjEntPG.GetType());

                if ((ObjEntPS == null) || (ObjEntPG == null))
                {
                    _erro = -1;
                    _msgErro = "Erro ao converter JSON de Pessoa/Pagamento";

                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }

                //################################################################################################
                //para produto digital e se os dados do logradouro forem NULL vou pegar os dados do endereço
                //padrão na parametro global.
                //################################################################################################

                List<ProdutoEntity> ObjProdEnt = new List<ProdutoEntity>();
                ProdutoBusiness ObjProdBLL = new ProdutoBusiness();

                string lstrRetProdEnt = ObjProdBLL.GetProdutoListService(ObjEntPG.CD_PRODUTO);

                if (lstrRetProdEnt != "")
                {
                    ObjProdEnt = (List<ProdutoEntity>)JsonConvert.DeserializeObject(lstrRetProdEnt, ObjProdEnt.GetType());
                }

                if (ObjProdEnt != null)
                {
                    if (ObjProdEnt[0].ST_IND_ONLINE == 1) //DIGITAL
                    {
                        //LOGRADOURO DE PRODUTO ONLINE SEM PREENCHIMENTO
                        if ((ObjEntDE.ID_LOGR == 0) && (ObjEntDE.DS_LOGR == null))
                        {
                            ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                            ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                            ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                            if (ObjParamEnt != null)
                            {
                                if ((ObjEntDE.ST_LOGR_EXT != 0) && (ObjEntDE.ST_LOGR_EXT != 1))
                                {
                                    _erro = -1;
                                    _msgErro = "Para endereço não informado é obrigatório informar se o mesmo e dentro ou fora do País";

                                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                                }

                                ViewLogrBusiness ObjLogrBLL = new ViewLogrBusiness();
                                List<DadosEntregaEntity> ObjLogrList = new List<DadosEntregaEntity>();

                                string lstrRetLogrEnt = "";

                                if (ObjEntDE.ST_LOGR_EXT == 0)
                                {
                                    lstrRetLogrEnt = ObjLogrBLL.GetViewLogrEntityServiceID(ObjParamEnt.CD_LOGR_PADRAO_EXTERIOR);
                                }
                                else if (ObjEntDE.ST_LOGR_EXT == 1)
                                {
                                    lstrRetLogrEnt = ObjLogrBLL.GetViewLogrEntityServiceID(ObjParamEnt.CD_LOGR_PADRAO_BRASIL);
                                }

                                if (lstrRetLogrEnt == "")
                                {
                                    _erro = -1;

                                    if (ObjEntDE.ST_LOGR_EXT == 0)
                                    {
                                        _msgErro = "Erro ao obter logradouro padrão do Exterior";
                                    }
                                    else
                                    {
                                        _msgErro = "Erro ao obter logradouro padrão do Brasil";
                                    }

                                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");

                                }

                                ObjLogrList = (List<DadosEntregaEntity>)JsonConvert.DeserializeObject(lstrRetLogrEnt, ObjLogrList.GetType());

                                if (ObjLogrList != null)
                                {
                                    ObjEntDE.ID_LOGR = ObjLogrList[0].ID_LOGR;
                                    ObjEntDE.DS_TIPO = ObjLogrList[0].DS_TIPO;
                                    ObjEntDE.CD_TIPO = ObjLogrList[0].CD_TIPO;
                                    ObjEntDE.DS_LOGR = ObjLogrList[0].DS_LOGR;
                                    ObjEntDE.DS_MUNICIPIO = ObjLogrList[0].DS_MUNICIPIO;
                                    ObjEntDE.DS_BAIRRO = ObjLogrList[0].DS_BAIRRO;
                                    ObjEntDE.DS_UF = ObjLogrList[0].DS_UF;
                                    ObjEntDE.NU_CEP = ObjLogrList[0].NU_CEP;
                                    ObjEntDE.NU_RESID = "1";
                                }

                            }
                            else
                            {
                                _erro = -1;
                                _msgErro = "Erro ao obter dados do parâmetro goloba ed (1)";

                                return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                            }
                        }
                        else
                        {

                            if ((ObjEntDE.ID_LOGR != 0) && (ObjEntDE.DS_LOGR != null))
                            {

                            }
                            else
                            {
                                ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                                ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                                ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                                if (ObjParamEnt != null)
                                {
                                    if ((ObjEntDE.ST_LOGR_EXT != 0) && (ObjEntDE.ST_LOGR_EXT != 1))
                                    {
                                        _erro = -1;
                                        _msgErro = "Para endereço não informado é obrigatório informar se o mesmo e dentro ou fora do País";

                                        return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                                    }

                                    if (ObjEntDE.ST_LOGR_EXT == 0)
                                    {
                                        ObjEntDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_EXTERIOR;
                                    }
                                    else if (ObjEntDE.ST_LOGR_EXT == 1)
                                    {
                                        ObjEntDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_BRASIL;
                                    }

                                }
                                else
                                {
                                    _erro = -1;
                                    _msgErro = "Erro ao obter dados do parâmetro goloba ed (2)";

                                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                                }

                            }
                        }    
                    }
                    else //IMPRESSO
                    {
                        if (ObjEntDE == null)
                        {
                            _erro = -1;
                            _msgErro = "Erro ao converter JSON de Entrega";

                            return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                        }

                        if ((ObjEntDE.ID_LOGR == 0) && (ObjEntDE.DS_LOGR != null))
                        {
                            ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                            ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                            ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                            if (ObjParamEnt != null)
                            {
                                if ((ObjEntDE.ST_LOGR_EXT == 0))
                                {
                                    _erro = -1;
                                    _msgErro = "Não é possível assinar um produto impresso para endereço digitado do exterior.";

                                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                                }

                                ObjEntDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_BRASIL;
                            }
                        }
                    }
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao localizar produto (validação de produto online)";

                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }

                bool lbolUsaCartaoVenda = bool.Parse(ConfigurationManager.AppSettings["UsaCartaoVenda"]);

                bool lbolGetway = false;
                GatewayPagamento ObjGateway = new GatewayPagamento();


                PlanoComercialBusiness ObjPlanoBLL = new PlanoComercialBusiness();
                PlanoComercialWebEntity ObjPlanoEnt = new PlanoComercialWebEntity();
                PlanoComercialWebEntity ObjPlanoEntLog = new PlanoComercialWebEntity();

                //==========================================================================
                //SOMENTE PARA ASSINATURAS PAGAS
                //==========================================================================
                if ((ObjEntPG.CD_TP_FORMA_PAG != 4) && (ObjEntPG.CD_TP_FORMA_PAG != 9))
                {
                    if (ObjEntPG.CD_TP_FORMA_PAG == 6) //CARTAO
                    {
                        if (!string.IsNullOrEmpty(pstrDadosGateway))
                        {
                            ObjGateway = (GatewayPagamento)JsonConvert.DeserializeObject(pstrDadosGateway, ObjGateway.GetType());
                        }

                        if (ObjGateway != null)
                        {
                            //Testa se todos os campos do gatewya estao ok 
                            if ((!string.IsNullOrEmpty(ObjGateway.CodAutorizacao)) && (!string.IsNullOrEmpty(ObjGateway.ComprovanteVenda)) &&
                                 (!string.IsNullOrEmpty(ObjGateway.IDPagamento)) && (!string.IsNullOrEmpty(ObjGateway.TransacAdquirente)) &&
                                 (ObjGateway.NumeroParcelasEnviadas > 0))
                            {
                                lbolGetway = true;
                            }
                            else
                            {
                                _erro = -1;
                                _msgErro = "Dados do gateway de pagamento não informado corretamente.";

                                Utility.SetLogXML("GravarAssinaturaIntegracao", "Error", _msgErro, false);

                                //return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");

                                lbolGetway = false;
                            }
                        }

                    }

                    //################################################################################################
                    //Retorna na variavel InfoPlano para gravar o log de rastreio da venda
                    //################################################################################################
                    
                    ObjPlanoEnt = ObjPlanoBLL.GetParametrosPlano(ObjEntPG.CD_PLANO);
                    ObjPlanoEntLog.CD_PLANO = ObjEntPG.CD_PLANO;

                    ObjPlanoEnt = ObjPlanoBLL.GetValorPlano(ObjEntPG.CD_CAMPANHA, ObjEntPG.CD_PLANO, ObjEntDE.ID_LOGR);

                    if (ObjPlanoEnt == null)
                    {
                        _erro = ObjPlanoBLL.Erro;
                        _msgErro = ObjPlanoBLL.MsgErro;

                        Utility.SetLogXML("GravarAssinaturaIntegracao", "Error", _msgErro, false);

                        return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                    }

                    ObjPlanoEntLog.VA_TOTAL_CENT = ObjPlanoEnt.VA_TOTAL_CENT;
                    ObjPlanoEntLog.VA_PARC_CENT = ObjPlanoEnt.VA_PARC_CENT;

                    //################################################################################################


                    //====================================================================
                    //TRATA DADOS DO CARTAO DE CRÉDITO QUANDO O PARAMENTRO GLOBAL PERMITIR
                    //====================================================================

                    if (ObjEntPG.CD_TP_FORMA_PAG == 6)
                    {
                        if (lbolUsaCartaoVenda == false)
                        {
                            ObjEntPG.NU_CARTAO = "99999999999999999";
                            ObjEntPG.NM_PESSOA_CARTAO = ObjEntPS.DS_NOME;
                            ObjEntPG.MELHOR_DIA_CARTAO = 0;
                            ObjEntPG.NU_CVV = null;
                            ObjEntPG.DT_VALID = null;
                        }
                    }

                    //====================================================================
                }
                else
                {
                    ObjPlanoEnt = ObjPlanoBLL.GetParametrosPlano(ObjEntPG.CD_PLANO);
                    ObjPlanoEntLog.CD_PLANO = ObjEntPG.CD_PLANO;

                    ObjPlanoEntLog.VA_TOTAL_CENT = ObjPlanoEnt.VA_TOTAL_CENT;
                    ObjPlanoEntLog.VA_PARC_CENT = ObjPlanoEnt.VA_PARC_CENT;
                }
                //==========================================================================
                //FIM SOMENTE ASSINATURAS PAGAS
                //==========================================================================

                //################################################################################################
                //Validação de campos obrigatorios
                //################################################################################################
                string lstrRetValid = GetValidarDadosIntegracao(ObjEntPS, ObjEntDE, ObjEntPG, ObjGateway);

                if (_erro == 0)
                {
                    if (lstrRetValid.Trim() != "")
                    {
                        return Utility.GetRetornoJSON("-1", lstrRetValid, "");
                    }
                }
                else
                {
                    return Utility.GetRetornoJSON("-1", _msgErro, "");
                }
                //################################################################################################



                GravaAssinaturaData ObjData = new GravaAssinaturaData();

                //===========================================================================
                //PEGA O ULTIMO CONTRATO
                //===========================================================================
                string[] lstrVetorCTR;
                string lstrCTRTemp;

                string lstrSerieCTR = "";
                int lintNuCTR = 0;
                int lintNuDvCTR = 0;

                lstrCTRTemp = ObjData.GetProximoContrato("VD", 0, true);

                if (lstrCTRTemp.Trim() != "")
                {
                    lstrVetorCTR = lstrCTRTemp.Split(';');

                    if (lstrVetorCTR.Length > 0)
                    {
                        lstrSerieCTR = lstrVetorCTR[0];
                        lintNuCTR = int.Parse(lstrVetorCTR[1]);
                        lintNuDvCTR = int.Parse(lstrVetorCTR[2]);

                        _lstrCTR = lstrCTRTemp;
                    }
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro Novo Contrato - Não foi possível obter um contrato para essa solicitação.";

                    Utility.SetLogXML("GravarAssinaturaIntegracao", "Error", _msgErro, false);

                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");

                }
                //===========================================================================


                //Grava Primeira parte do Log de Rastreio.

                LogInterfaceWEBBusiness ObjLogInterfaceBLL = new LogInterfaceWEBBusiness();

                ObjLogInterfaceBLL.SetLogInterface(0, ObjEntPS, ObjEntDE, ObjEntPG, ObjPlanoEntLog, null, "GravarVendaTerceiro", "", "GravarAssinaturaIntegracao", lstrSerieCTR, lintNuCTR, lintNuDvCTR, lbolGetway, "", "");

                if (ObjLogInterfaceBLL.Erro != 0)
                {
                    Utility.SetLogXML("GravarAssinaturaIntegracao-GravaLogInterface-LogI", "Error", ObjLogInterfaceBLL.MsgErro, false,1);
                }

                _IDLogRastreio = ObjLogInterfaceBLL.IDLogRatreamento;

                //===========================================================================


                DataContext.AbrirConexao();
                DataContext.BeginTransaction();

                //GRAVA DADOS PESSOAIS
                ObjData.SetDadosPessoa(ObjEntPS, ObjEntDE, ObjEntPG, lstrSerieCTR, lintNuCTR, lintNuDvCTR, true, false);
                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    DataContext.RollbackTransaction();
                    DataContext.FecharConexao();

                    return Utility.GetRetornoJSON("-1", _msgErro, "");
                }
                //===========================================================================

                //GRAVA DADOS TELEFONE RESIDENCIAL
                ObjData.SetDadosTelefone(ObjEntPS, lstrSerieCTR, lintNuCTR, lintNuDvCTR, 0, true, false);
                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    DataContext.RollbackTransaction();
                    DataContext.FecharConexao();

                    Utility.SetLogXML("GravarAssinaturaIntegracao", "Error", _msgErro, false);

                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }
                //===========================================================================

                //GRAVA DADOS TELEFONE CELUAR SE EXISTIR
                if (ObjEntPS.NU_CEL != null)
                {
                    if (ObjEntPS.NU_CEL.Trim() != "")
                    {
                        ObjData.SetDadosTelefone(ObjEntPS, lstrSerieCTR, lintNuCTR, lintNuDvCTR, 1, true, false);

                        if (ObjData.Erro != 0)
                        {
                            _erro = ObjData.Erro;
                            _msgErro = ObjData.MsgErro;

                            DataContext.RollbackTransaction();
                            DataContext.FecharConexao();

                            Utility.SetLogXML("GravarAssinaturaIntegracao", "Error", _msgErro, false);

                            return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                        }

                    }
                }
                //============================================================================================


                //==========================================================================
                //SOMENTE PARA ASSINATURAS PAGAS
                //==========================================================================
                if ((ObjEntPG.CD_TP_FORMA_PAG != 4) && (ObjEntPG.CD_TP_FORMA_PAG != 9))
                {
                    //Valida se é um plano que grava tabela de DEBITO_AUTOMATICO_CTR
                    if (IsGravaDebito(ObjEntPG.CD_TP_FORMA_PAG) == true)
                    {                    
                        //====================================================================
                        //RECUPERA A CHAVE DE CRYPT DO CARTAO CREDITO
                        //====================================================================
                        Utility ObjUtilCript = new Utility();

                        string lstrChaveCryptCC = ObjUtilCript.GetCriptCartaoCIR2000();

                        ObjEntPG.CHAVE_CRYPT_CC = lstrChaveCryptCC;

                        //====================================================================

                        //====================================================================
                        //Seta os dados do token quando enviado.
                        //====================================================================

                        if (!string.IsNullOrEmpty(ObjGateway.TokenCard))
                        {
                            if ((ObjGateway.TokenCard.Trim() != ""))
                            {
                                ObjEntPG.CARDTOKEN = ObjGateway.TokenCard;
                                ObjEntPG.NU_CVV = ObjGateway.CVV;
                            }

                        }
                        else
                        {
                            if ((ObjEntPG.CHAVE_CRYPT_CC.Trim() != ""))
                            {
                                ObjEntPG.CARDTOKEN = ObjEntPG.CHAVE_CRYPT_CC.Trim();
                                ObjEntPG.NU_CVV = null;
                            }
                        }

                        //GRAVA DADOS DEBITO

                        ObjData.SetDadosDebito(ObjEntPG, lstrSerieCTR, lintNuCTR, lintNuDvCTR, true, false);

                        if (ObjData.Erro != 0)
                        {
                            _erro = ObjData.Erro;
                            _msgErro = ObjData.MsgErro;

                            DataContext.RollbackTransaction();
                            DataContext.FecharConexao();

                            Utility.SetLogXML("GravarAssinaturaIntegracao", "Error", _msgErro, false);

                            return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                        }
                        //============================================================================================
                    }

                    if (lbolGetway == true)
                    {

                        if (ObjEntPG.CD_TP_FORMA_PAG == 6) //CARTAO
                        {
                            ObjData.SetBaixaParcela(lstrSerieCTR,
                                                    lintNuCTR,
                                                    lintNuDvCTR,
                                                    ObjGateway.CodAutorizacao,
                                                    ObjGateway.ComprovanteVenda,
                                                    ObjGateway.TransacAdquirente,
                                                    ObjGateway.IDPagamento,
                                                    "",
                                                    ObjGateway.NumeroParcelasEnviadas,
                                                    ObjGateway.NumeroParcelasEnviadas,
                                                    ObjGateway.TokenCard,
                                                    true, false);

                            if (ObjData.Erro != 0)
                            {
                                _erro = DataContext.Erro;
                                _msgErro = DataContext.MsgErro;

                                DataContext.RollbackTransaction();
                                DataContext.FecharConexao();

                                Utility.SetLogXML("GravarAssinaturaIntegracao-InterfacePagto-SetBaixaParcela", "Error", _msgErro, false);

                                return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                            }
                        }

                    }
                    //============================================================================================

                }
                //==========================================================================
                //FIM SOMENTE ASSINATURAS PAGAS
                //==========================================================================

                //commit 
                if (Erro == 0)
                {
                    DataContext.CommitTransaction();
                    DataContext.FecharConexao();
                }

                //==========================================================================

                //Atualiza Log com dados de retorno InterfacePagto
                if (_IDLogRastreio != 0)
                {
                    if (lbolGetway == true)
                    {
                        if (!string.IsNullOrEmpty(pstrDadosGateway))
                        {
                            ObjLogInterfaceBLL.SetUpdLogInterface(pstrDadosGateway, _msgErro, _IDLogRastreio);
                        }
                        else
                        {
                            ObjLogInterfaceBLL.SetUpdLogInterface("", _msgErro, _IDLogRastreio);
                        }
                        

                        if (ObjLogInterfaceBLL.Erro != 0)
                        {
                            Utility.SetLogXML("GravarAssinaturaIntegracao-GravaLogInterface-Log", "Error", ObjLogInterfaceBLL.MsgErro, false);
                        }
                    }
                }

                return Utility.GetRetornoJSON("0", "Sucesso", lstrSerieCTR + "|" + lintNuCTR.ToString() + "|" + lintNuDvCTR.ToString());

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                DataContext.RollbackTransaction();
                DataContext.FecharConexao();

                if (_IDLogRastreio != 0)
                {
                    LogInterfaceWEBBusiness ObjLogInterfaceBLL = new LogInterfaceWEBBusiness();

                    ObjLogInterfaceBLL.SetUpdLogInterface("", _msgErro, _IDLogRastreio);

                    if (ObjLogInterfaceBLL.Erro != 0)
                    {
                        Utility.SetLogXML("GravarAssinaturaIntegracao-GravaLogInterface-LogI", "Error", ObjLogInterfaceBLL.MsgErro, false, 1);
                    }
                }

                return Utility.GetRetornoJSON("-99", "Error - " + _msgErro, "");

            }

        }

        public string GetValidarDadosIntegracao(DadosPessoaEntity ObjPS,
                                                DadosEntregaEntity ObjDE,
                                                DadosPagamentoEntity ObjPG,
                                                GatewayPagamento ObjGateway)
        {
            try
            {

                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //Validar dados da pessoa
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                if (ObjPS.TP_PESSOA == 0)
                {
                    return "Tipo de pessoa invalido";
                }

                if ( (ObjPS.DS_NOME == "") || ( string.IsNullOrEmpty(ObjPS.DS_NOME)) )
                {
                    return "Nome da pessoa não pode ser vazio";
                }

                if ((ObjPS.DS_EMAIL == "") || (string.IsNullOrEmpty(ObjPS.DS_EMAIL)))
                {
                    return "Email não pode ser vazio";
                }

                if (ObjPS.TP_PESSOA == 1)
                {
                    if ((ObjPS.NU_CPF == "") || (string.IsNullOrEmpty(ObjPS.NU_CPF)))
                    {
                        return "CPF não pode ser vazio";
                    }
                }
                else
                {
                    if ((ObjPS.NU_CNPJ == "") || (string.IsNullOrEmpty(ObjPS.NU_CNPJ)))
                    {
                        return "CNPJ não pode ser vazio";
                    }

                }

                if ((ObjPS.NU_DDDTEL == "") || (string.IsNullOrEmpty(ObjPS.NU_DDDTEL)))
                {
                    return "DDD do telefone não pode ser vazio";
                }

                if ((ObjPS.NU_TEL == "") || (string.IsNullOrEmpty(ObjPS.NU_TEL)))
                {
                    return "Numero do telefone não pode ser vazio";
                }

                if ((ObjPS.ST_AUTORIZA_EMAIL != 0) && (ObjPS.ST_AUTORIZA_EMAIL != 1))
                {
                    return "O valor se autoriza ou não email deve ser informado 0=NAO/1=SIM";
                }

                if ((ObjPS.ST_IND_DIVULGACAO != 0) && (ObjPS.ST_IND_DIVULGACAO != 1))
                {
                    return "O valor se autoriza ou não divulgação dos dados deve ser informado 0=NAO/1=SIM";
                }

                if (ObjPS.CD_GRUPO_SELECAO == 0)
                {
                    return "O valor do grupo de seleção deve ser maior que 0";
                }

                if (ObjPS.CD_LOCAL_ENTREGA == 0)
                {
                    return "O valor do local de entrega deve ser maior que 0";
                }

                if (ObjPS.CD_REPR_VENDA == 0)
                {
                    return "O codigo do representante de venda deve ser maior que 0 e um ID valido na base";
                }

                if (ObjPS.CD_VENDEDOR == 0)
                {
                    return "O codigo do vendedor deve ser maior que 0 e um ID valido na base";
                }

                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //Validar dados de entrega
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                if ((ObjDE.DS_TIPO == "") || (string.IsNullOrEmpty(ObjDE.DS_TIPO)))
                {
                    return "tipo do logradouro não pode ser vazio";
                }

                if ((ObjDE.DS_LOGR == "") || (string.IsNullOrEmpty(ObjDE.DS_LOGR)))
                {
                    return "Logradouro não pode ser vazio";
                }

                if ((ObjDE.DS_BAIRRO == "") || (string.IsNullOrEmpty(ObjDE.DS_BAIRRO)))
                {
                    return "Bairro não pode ser vazio";
                }

                if ((ObjDE.DS_MUNICIPIO == "") || (string.IsNullOrEmpty(ObjDE.DS_MUNICIPIO)))
                {
                    return "Municipio não pode ser vazio";
                }

                if ((ObjDE.DS_UF == "") || (string.IsNullOrEmpty(ObjDE.DS_UF)))
                {
                    return "UF não pode ser vazio";
                }

                if ((ObjDE.NU_RESID == "") || (string.IsNullOrEmpty(ObjDE.NU_RESID)))
                {
                    return "Numero da residencia não pode ser vazio";
                }
                else
                {
                    Int32 lintOutNumber;
                    bool lbOK = Int32.TryParse(ObjDE.NU_RESID, out lintOutNumber);
                    if (lbOK == false)
                    {
                        return "Numero da residencia tem que ser numérico, para locais sem número informar 0 (zero);";
                    }
                }

                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //Validar dados de pagamento
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                if ((ObjPG.CD_TP_FORMA_PAG != 4) && (ObjPG.CD_TP_FORMA_PAG != 9))
                {
                    if ((ObjPG.CD_TP_ASSINATURA == 0))
                    {
                        return "Tipo de assinatura não pode ser 0";
                    }

                    if ((ObjPG.CD_TP_ENTREGA == 0))
                    {
                        return "Tipo de entrega não pode ser 0";
                    }

                    if ((ObjPG.CD_TP_FORMA_PAG == 0))
                    {
                        return "Tipo da forma de pagamento não pode ser 0";
                    }

                    if ((ObjPG.NU_PARCELA == 0))
                    {
                        return "Numero de parcelas não pode ser 0";
                    }

                    if ((ObjPG.CD_CAMPANHA == 0))
                    {
                        return "Campanha não pode ser 0";
                    }

                    if ((ObjPG.CD_PLANO == 0))
                    {
                        return "Plano não pode ser 0";
                    }

                    if ((ObjPG.CD_FORMA_PAG == 0))
                    {
                        return "Forma de Pagamento não pode ser 0";
                    }

                    if ((ObjPG.CD_FONTE_COBRANCA == 0))
                    {
                        return "Fonte de cobrança não pode ser 0";
                    }

                    if (ObjPG.CD_TP_FORMA_PAG == 6)
                    {

                        bool lbolUsaCartaoVenda = bool.Parse(ConfigurationManager.AppSettings["UsaCartaoVenda"]);

                        if (lbolUsaCartaoVenda == true)
                        {
                            if ((ObjPG.NM_PESSOA_CARTAO == "") || (string.IsNullOrEmpty(ObjPG.NM_PESSOA_CARTAO)))
                            {
                                return "Nome do Responsável do cartão não pode ser vazio.";
                            }

                            if ((ObjPG.MELHOR_DIA_CARTAO == 0) || (ObjPG.MELHOR_DIA_CARTAO < 0) || (ObjPG.MELHOR_DIA_CARTAO > 31))
                            {
                                return "Melhor dia deve esta entre 1 e 31";
                            }

                            if ((ObjPG.NU_CARTAO == "") || (string.IsNullOrEmpty(ObjPG.NU_CARTAO)))
                            {
                                return "Numero do cartão não pode ser vazio.";
                            }

                            if ((ObjPG.NU_CVV == "") || (string.IsNullOrEmpty(ObjPG.NU_CVV)))
                            {
                                return "Numero do cartão não pode ser vazio.";
                            }

                            if ((ObjPG.DT_VALID == "") || (string.IsNullOrEmpty(ObjPG.DT_VALID)))
                            {
                                return "Data de Validade do cartão não pode ser vazia.";
                            }
                        }
                        else
                        {
                            if (ObjGateway != null)
                            {
                                if ((ObjGateway.TokenCard == "") || (string.IsNullOrEmpty(ObjGateway.TokenCard)))
                                {
                                    if ((ObjPG.CHAVE_CRYPT_CC == "") || (string.IsNullOrEmpty(ObjPG.CHAVE_CRYPT_CC)))
                                    {
                                        return "Token não informado para a venda em cartão de crédito.";
                                    }
                                }
                            }
                            else
                            {
                                return "Dados de pagamento para um plano de cartão deve ser informado (null).";
                            }

                        }

                    }
                }
                else
                {
                    if ((ObjPG.CD_TP_ASSINATURA == 0))
                    {
                        return "Tipo de assinatura não pode ser 0";
                    }

                    if ((ObjPG.CD_TP_ENTREGA == 0))
                    {
                        return "Tipo de entrega não pode ser 0";
                    }

                    if ((ObjPG.CD_TP_FORMA_PAG == 0))
                    {
                        return "Tipo da forma de pagamento não pode ser 0";
                    }

                    if ((ObjPG.CD_CAMPANHA == 0))
                    {
                        return "Campanha não pode ser 0";
                    }

                    if ((ObjPG.CD_PLANO == 0))
                    {
                        return "Plano não pode ser 0";
                    }

                    if ((ObjPG.CD_FORMA_PAG == 0))
                    {
                        return "Forma de Pagamento não pode ser 0";
                    }
                }
               

                return "";
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return _erro.ToString() + "-" + _msgErro;
            }

        }

        public string[] GetVerificarAssinante(int pintTipoPessoa, string pstrDOC)
        {
            try
            {
                GravaAssinaturaData ObjData = new GravaAssinaturaData();
                string[] lstrVetRet = ObjData.GetVerificarAssinante(pintTipoPessoa, pstrDOC);

                if (lstrVetRet != null)
                {

                    if (lstrVetRet[1].Trim() != "")
                    {
                        return new string[] { lstrVetRet[0].Trim(), lstrVetRet[1].Trim(), CriptLogin.Desencriptar(lstrVetRet[1].Trim())};
                    }
                    else
                    {
                        return null;
                    }
                    
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
                return new string[] { _erro.ToString(), _msgErro };
            }
        }

        public string GravarAssinaturaIntegracaoNOVO(string pstrDadosPessoa,
                                                     string pstrDadosEndereco,
                                                     string pstrDadosPagamento,
                                                     string pstrDadosGateway)
        {

            _erro = 0;
            _msgErro = "";
            _lstrCTR = "";
            _lstrInfoPlano = "";
            _IDLogRastreio = 0;


            try
            {


                //################################################################################################
                //Valida dos dados JSON passado e transforma em Objeto para Gravar a Assinatura
                //################################################################################################


                DadosPessoaEntity ObjEntPS = new DadosPessoaEntity();
                ObjEntPS = (DadosPessoaEntity)JsonConvert.DeserializeObject(pstrDadosPessoa, ObjEntPS.GetType());

                DadosEntregaEntity ObjEntDE = new DadosEntregaEntity();
                ObjEntDE = (DadosEntregaEntity)JsonConvert.DeserializeObject(pstrDadosEndereco, ObjEntDE.GetType());

                DadosPagamentoEntity ObjEntPG = new DadosPagamentoEntity();
                ObjEntPG = (DadosPagamentoEntity)JsonConvert.DeserializeObject(pstrDadosPagamento, ObjEntPG.GetType());


                if ((ObjEntPS == null) || (ObjEntPG == null))
                {
                    _erro = -1;
                    _msgErro = "Erro ao converter JSON de Pessoa/Pagamento";

                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }

                //################################################################################################
                //para produto digital e se os dados do logradouro forem NULL vou pegar os dados do endereço
                //padrão na parametro global.
                //################################################################################################

                List<ProdutoEntity> ObjProdEnt = new List<ProdutoEntity>();
                ProdutoBusiness ObjProdBLL = new ProdutoBusiness();

                //OKSQL
                string lstrRetProdEnt = ObjProdBLL.GetProdutoListService(ObjEntPG.CD_PRODUTO);

                if (!string.IsNullOrEmpty(lstrRetProdEnt))
                {
                    if (lstrRetProdEnt != "")
                    {
                        ObjProdEnt = (List<ProdutoEntity>)JsonConvert.DeserializeObject(lstrRetProdEnt, ObjProdEnt.GetType());
                    }
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao recuperar dados do produto";

                    Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "GetProdutoListService - Erro ao recuperar dados do produto", false, 1);

                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }


                if (ObjProdEnt != null)
                {
                    if (ObjProdEnt[0].ST_IND_ONLINE == 1) //DIGITAL
                    {
                        //LOGRADOURO DE PRODUTO ONLINE SEM PREENCHIMENTO
                        if ((ObjEntDE.ID_LOGR == 0) && (ObjEntDE.DS_LOGR == null))
                        {
                            ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                            ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                            //SQLOK
                            ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                            if (ObjParamEnt != null)
                            {
                                if ((ObjEntDE.ST_LOGR_EXT != 0) && (ObjEntDE.ST_LOGR_EXT != 1))
                                {
                                    _erro = -1;
                                    _msgErro = "Para endereço não informado é obrigatório informar se o mesmo e dentro ou fora do País";

                                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                                }

                                ViewLogrBusiness ObjLogrBLL = new ViewLogrBusiness();
                                List<DadosEntregaEntity> ObjLogrList = new List<DadosEntregaEntity>();

                                string lstrRetLogrEnt = "";

                                if (ObjEntDE.ST_LOGR_EXT == 0)
                                {
                                    //OKSQL
                                    lstrRetLogrEnt = ObjLogrBLL.GetViewLogrEntityServiceID(ObjParamEnt.CD_LOGR_PADRAO_EXTERIOR);
                                }
                                else if (ObjEntDE.ST_LOGR_EXT == 1)
                                {
                                    //OKSQL
                                    lstrRetLogrEnt = ObjLogrBLL.GetViewLogrEntityServiceID(ObjParamEnt.CD_LOGR_PADRAO_BRASIL);
                                }

                                if (lstrRetLogrEnt == "")
                                {
                                    _erro = -1;

                                    if (ObjEntDE.ST_LOGR_EXT == 0)
                                    {
                                        _msgErro = "Erro ao obter logradouro padrão do Exterior";
                                    }
                                    else
                                    {
                                        _msgErro = "Erro ao obter logradouro padrão do Brasil";
                                    }

                                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");

                                }

                                ObjLogrList = (List<DadosEntregaEntity>)JsonConvert.DeserializeObject(lstrRetLogrEnt, ObjLogrList.GetType());

                                if (ObjLogrList != null)
                                {
                                    ObjEntDE.ID_LOGR = ObjLogrList[0].ID_LOGR;
                                    ObjEntDE.DS_TIPO = ObjLogrList[0].DS_TIPO;
                                    ObjEntDE.CD_TIPO = ObjLogrList[0].CD_TIPO;
                                    ObjEntDE.DS_LOGR = ObjLogrList[0].DS_LOGR;
                                    ObjEntDE.DS_MUNICIPIO = ObjLogrList[0].DS_MUNICIPIO;
                                    ObjEntDE.DS_BAIRRO = ObjLogrList[0].DS_BAIRRO;
                                    ObjEntDE.DS_UF = ObjLogrList[0].DS_UF;
                                    ObjEntDE.NU_CEP = ObjLogrList[0].NU_CEP;
                                    ObjEntDE.NU_RESID = "1";
                                }

                            }
                            else
                            {
                                _erro = -1;
                                _msgErro = "Erro ao obter dados do parâmetro goloba ed (1)";

                                return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                            }
                        }
                        else
                        {

                            if ((ObjEntDE.ID_LOGR != 0) && (ObjEntDE.DS_LOGR != null))
                            {

                            }
                            else
                            {
                                ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                                ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                                //OKSQL
                                ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                                if (ObjParamEnt != null)
                                {
                                    if ((ObjEntDE.ST_LOGR_EXT != 0) && (ObjEntDE.ST_LOGR_EXT != 1))
                                    {
                                        _erro = -1;
                                        _msgErro = "Para endereço não informado é obrigatório informar se o mesmo e dentro ou fora do País";

                                        return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                                    }

                                    if (ObjEntDE.ST_LOGR_EXT == 0)
                                    {
                                        ObjEntDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_EXTERIOR;
                                    }
                                    else if (ObjEntDE.ST_LOGR_EXT == 1)
                                    {
                                        ObjEntDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_BRASIL;
                                    }

                                }
                                else
                                {
                                    _erro = -1;
                                    _msgErro = "Erro ao obter dados do parâmetro goloba ed (2)";

                                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                                }

                            }
                        }
                    }
                    else //IMPRESSO
                    {
                        if (ObjEntDE == null)
                        {
                            _erro = -1;
                            _msgErro = "Erro ao converter JSON de Entrega";

                            return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                        }

                        if ((ObjEntDE.ID_LOGR == 0) && (ObjEntDE.DS_LOGR != null))
                        {
                            ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                            ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                            //OKSQL
                            ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                            if (ObjParamEnt != null)
                            {
                                if ((ObjEntDE.ST_LOGR_EXT == 0))
                                {
                                    _erro = -1;
                                    _msgErro = "Não é possível assinar um produto impresso para endereço digitado do exterior.";

                                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                                }

                                ObjEntDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_BRASIL;
                            }
                        }
                    }
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao localizar produto (validação de produto online)";

                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }

                bool lbolUsaCartaoVenda = bool.Parse(ConfigurationManager.AppSettings["UsaCartaoVenda"]);

                bool lbolGetway = false;
                GatewayPagamento ObjGateway = new GatewayPagamento();


                PlanoComercialBusiness ObjPlanoBLL = new PlanoComercialBusiness();
                PlanoComercialWebEntity ObjPlanoEnt = new PlanoComercialWebEntity();
                PlanoComercialWebEntity ObjPlanoEntLog = new PlanoComercialWebEntity();

                //==========================================================================
                //SOMENTE PARA ASSINATURAS PAGAS
                //==========================================================================
                if ((ObjEntPG.CD_TP_FORMA_PAG != 4) && (ObjEntPG.CD_TP_FORMA_PAG != 9))
                {
                    if (ObjEntPG.CD_TP_FORMA_PAG == 6) //CARTAO
                    {
                        if (!string.IsNullOrEmpty(pstrDadosGateway))
                        {
                            ObjGateway = (GatewayPagamento)JsonConvert.DeserializeObject(pstrDadosGateway, ObjGateway.GetType());
                        }

                        if (ObjGateway != null)
                        {
                            //Testa se todos os campos do gatewya estao ok 
                            if ((!string.IsNullOrEmpty(ObjGateway.CodAutorizacao)) && (!string.IsNullOrEmpty(ObjGateway.ComprovanteVenda)) &&
                                 (!string.IsNullOrEmpty(ObjGateway.IDPagamento)) && (!string.IsNullOrEmpty(ObjGateway.TransacAdquirente)) &&
                                 (ObjGateway.NumeroParcelasEnviadas > 0))
                            {
                                lbolGetway = true;
                            }
                            else
                            {
                                _erro = -1;
                                _msgErro = "Dados do gateway de pagamento não informado corretamente.";

                                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Dados do Gateway - " + _msgErro, false, 1);

                                lbolGetway = false;
                            }
                        }

                    }

                    //################################################################################################
                    //Retorna na variavel InfoPlano para gravar o log de rastreio da venda
                    //################################################################################################

                    Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "GetValorPlano PG Inicio", false, 1);

                    ObjPlanoEntLog.CD_PLANO = ObjEntPG.CD_PLANO;
                    //SQLOK
                    ObjPlanoEnt = ObjPlanoBLL.GetValorPlano(ObjEntPG.CD_CAMPANHA, ObjEntPG.CD_PLANO, ObjEntDE.ID_LOGR);

                    if (ObjPlanoEnt == null)
                    {
                        _erro = ObjPlanoBLL.Erro;
                        _msgErro = ObjPlanoBLL.MsgErro;

                        Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "GetValorPlano PG Erro - " + _msgErro, false, 1);

                        return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                    }

                    ObjPlanoEntLog.VA_TOTAL_CENT = ObjPlanoEnt.VA_TOTAL_CENT;
                    ObjPlanoEntLog.VA_PARC_CENT = ObjPlanoEnt.VA_PARC_CENT;

                    Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "GetValorPlano PG Fim", false, 1);

                    //################################################################################################

                    //====================================================================
                    //TRATA DADOS DO CARTAO DE CRÉDITO QUANDO O PARAMENTRO GLOBAL PERMITIR
                    //====================================================================

                    if (ObjEntPG.CD_TP_FORMA_PAG == 6)
                    {
                        if (lbolUsaCartaoVenda == false)
                        {
                            ObjEntPG.NU_CARTAO = "9999999999999999";
                            ObjEntPG.NM_PESSOA_CARTAO = ObjEntPS.DS_NOME;
                            ObjEntPG.MELHOR_DIA_CARTAO = 0;
                            ObjEntPG.NU_CVV = null;
                            ObjEntPG.DT_VALID = null;
                        }
                    }

                    //====================================================================
                }
                else
                {

                    //ObjPlanoEnt = ObjPlanoBLL.GetParametrosPlano(ObjEntPG.CD_PLANO);
                    //SQLOK
                    string lstrRetPlanoEnt = ObjPlanoBLL.GetParametrosPlanoService(ObjEntPG.CD_PLANO);

                    if (lstrRetPlanoEnt != "")
                    {
                        ObjPlanoEnt = (PlanoComercialWebEntity)JsonConvert.DeserializeObject(lstrRetPlanoEnt, ObjPlanoEnt.GetType());
                    }
                    ObjPlanoEntLog.CD_PLANO = ObjEntPG.CD_PLANO;

                    ObjPlanoEntLog.CD_PLANO = ObjEntPG.CD_PLANO;

                    ObjPlanoEntLog.VA_TOTAL_CENT = ObjPlanoEnt.VA_TOTAL_CENT;
                    ObjPlanoEntLog.VA_PARC_CENT = ObjPlanoEnt.VA_PARC_CENT;
                }
                //==========================================================================
                //FIM SOMENTE ASSINATURAS PAGAS
                //==========================================================================



                //################################################################################################
                //Validação de campos obrigatorios
                //################################################################################################
                string lstrRetValid = GetValidarDadosIntegracao(ObjEntPS, ObjEntDE, ObjEntPG, ObjGateway);

                if (_erro == 0)
                {
                    if (lstrRetValid.Trim() != "")
                    {
                        return Utility.GetRetornoJSON("-1", lstrRetValid, "");
                    }
                }
                else
                {
                    return Utility.GetRetornoJSON("-1", _msgErro, "");
                }

                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Validação de Dados OK", false, 1);

                //################################################################################################

                GravaAssinaturaData ObjData = new GravaAssinaturaData();

                //===========================================================================
                //PEGA O ULTIMO CONTRATO
                //===========================================================================
                string[] lstrVetorCTR;
                string lstrCTRTemp;

                string lstrSerieCTR = "";
                int lintNuCTR = 0;
                int lintNuDvCTR = 0;

                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "GetProximoContrato Inicio", false, 1);

                lstrCTRTemp = ObjData.GetProximoContratoSQL("VD", 0, true);

                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "GetProximoContrato Fim - " + lstrCTRTemp, false, 1);

                if (lstrCTRTemp.Trim() != "")
                {
                    lstrVetorCTR = lstrCTRTemp.Split(';');

                    if (lstrVetorCTR.Length > 0)
                    {
                        lstrSerieCTR = lstrVetorCTR[0];
                        lintNuCTR = int.Parse(lstrVetorCTR[1]);
                        lintNuDvCTR = int.Parse(lstrVetorCTR[2]);

                        _lstrCTR = lstrCTRTemp;
                    }
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro Novo Contrato - Não foi possível obter um contrato para essa solicitação.";

                    Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "GetProximoContrato Erro - " + _msgErro, false, 1);

                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }

                //===========================================================================


                //Grava Primeira parte do Log de Rastreio.

                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "SetLogInterface Inicio", false, 1);

                LogInterfaceWEBBusiness ObjLogInterfaceBLL = new LogInterfaceWEBBusiness();
                //SQLOK
                ObjLogInterfaceBLL.SetLogInterface(0, ObjEntPS, ObjEntDE, ObjEntPG, ObjPlanoEntLog, ObjGateway, "GravarVendaTerceiro", "", "GravarAssinaturaIntegracao", lstrSerieCTR, lintNuCTR, lintNuDvCTR, lbolGetway, "", "");

                if (ObjLogInterfaceBLL.Erro != 0)
                {
                    Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "SetLogInterface Erro - " + ObjLogInterfaceBLL.MsgErro, false, 1);
                }

                _IDLogRastreio = ObjLogInterfaceBLL.IDLogRatreamento;

                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "SetLogInterface Fim - " + _IDLogRastreio.ToString(), false, 1);

                //===========================================================================

                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Gravar Abrir Transacao - " + lstrSerieCTR + "|" + lintNuCTR.ToString() + "|" + lintNuDvCTR.ToString(), false,1);

                DataContextSQL.AbrirConexao();
                DataContextSQL.BeginTransaction();

                //GRAVA DADOS PESSOAIS
                //ObjData.SetDadosPessoa(ObjEntPS, ObjEntDE, ObjEntPG, lstrSerieCTR, lintNuCTR, lintNuDvCTR, true, false);

                //SQLOK
                ObjData.SetDadosPessoaSQL(ObjEntPS, ObjEntDE, ObjEntPG, lstrSerieCTR, lintNuCTR, lintNuDvCTR, true, false);
                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    DataContextSQL.RollbackTransaction();
                    DataContextSQL.FecharConexao();

                    Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Dados da Pessoa Erro - " + _msgErro, false, 1);

                    return Utility.GetRetornoJSON("-1", _msgErro, "");
                }

                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Dados da Pessoa OK", false, 1);

                //===========================================================================

                //GRAVA DADOS TELEFONE RESIDENCIAL
                //ObjData.SetDadosTelefone(ObjEntPS, lstrSerieCTR, lintNuCTR, lintNuDvCTR, 0, true, false);

                //SQLOK
                ObjData.SetDadosTelefoneSQL(ObjEntPS, lstrSerieCTR, lintNuCTR, lintNuDvCTR, 0, true, false);
                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    DataContextSQL.RollbackTransaction();
                    DataContextSQL.FecharConexao();

                    Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Dados do Telefone 1 Erro " + _msgErro, false, 1);

                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }

                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Dados de Telefone 1 OK", false, 1);

                //===========================================================================

                //GRAVA DADOS TELEFONE CELUAR SE EXISTIR
                if (ObjEntPS.NU_CEL != null)
                {
                    if (ObjEntPS.NU_CEL.Trim() != "")
                    {
                        //ObjData.SetDadosTelefone(ObjEntPS, lstrSerieCTR, lintNuCTR, lintNuDvCTR, 1, true, false);
                        //SQLOK
                        ObjData.SetDadosTelefoneSQL(ObjEntPS, lstrSerieCTR, lintNuCTR, lintNuDvCTR, 1, true, false);
                        if (ObjData.Erro != 0)
                        {
                            _erro = ObjData.Erro;
                            _msgErro = ObjData.MsgErro;

                            DataContextSQL.RollbackTransaction();
                            DataContextSQL.FecharConexao();

                            Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Dados do Telefone 2 Erro " + _msgErro, false, 1);

                            return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                        }

                        Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Dados de Telefone 2 OK", false, 1);
                    }
                }
                //============================================================================================


                //==========================================================================
                //SOMENTE PARA ASSINATURAS PAGAS
                //==========================================================================
                if ((ObjEntPG.CD_TP_FORMA_PAG != 4) && (ObjEntPG.CD_TP_FORMA_PAG != 9))
                {
                    //Valida se é um plano que grava tabela de DEBITO_AUTOMATICO_CTR
                    if (IsGravaDebito(ObjEntPG.CD_TP_FORMA_PAG) == true)
                    {
                        //====================================================================
                        //RECUPERA A CHAVE DE CRYPT DO CARTAO CREDITO
                        //====================================================================
                        Utility ObjUtilCript = new Utility();

                        string lstrChaveCryptCC = ObjUtilCript.GetCriptCartaoCIR2000();

                        string lstrTokenTemp = "";

                        if (!string.IsNullOrEmpty(ObjEntPG.CHAVE_CRYPT_CC))
                        {
                            lstrTokenTemp = ObjEntPG.CHAVE_CRYPT_CC;
                        }

                        ObjEntPG.CHAVE_CRYPT_CC = lstrChaveCryptCC;
                        //====================================================================

                        //====================================================================
                        //Seta os dados do token quando enviado.
                        //====================================================================

                        if (!string.IsNullOrEmpty(ObjGateway.TokenCard))
                        {
                            if ((ObjGateway.TokenCard.Trim() != ""))
                            {
                                ObjEntPG.CARDTOKEN = ObjGateway.TokenCard;
                                ObjEntPG.NU_CVV = ObjGateway.CVV;
                            }

                        }
                        else
                        {
                            if ((lstrTokenTemp.Trim() != ""))
                            {
                                ObjGateway.TokenCard = lstrTokenTemp.Trim();

                                ObjEntPG.CARDTOKEN = lstrTokenTemp.Trim();
                                ObjEntPG.NU_CVV = null;
                            }
                        }

                        //GRAVA DADOS DEBITO
                        //ObjData.SetDadosDebito(ObjEntPG, lstrSerieCTR, lintNuCTR, lintNuDvCTR, true, false);
                        ObjData.SetDadosDebitoSQL(ObjEntPG, lstrSerieCTR, lintNuCTR, lintNuDvCTR, true, false);

                        if (ObjData.Erro != 0)
                        {
                            _erro = ObjData.Erro;
                            _msgErro = ObjData.MsgErro;

                            DataContextSQL.RollbackTransaction();
                            DataContextSQL.FecharConexao();

                            Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Dados de Debito Erro - " + _erro, false, 1);

                            return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                        }

                        Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Dados de Debito OK", false, 1);
                        //============================================================================================
                    }

                    if (lbolGetway == true)
                    {

                        if (ObjEntPG.CD_TP_FORMA_PAG == 6) //CARTAO
                        {

                            ObjData.SetBaixaParcelaSQL(lstrSerieCTR,
                                                    lintNuCTR,
                                                    lintNuDvCTR,
                                                    ObjGateway.CodAutorizacao,
                                                    ObjGateway.ComprovanteVenda,
                                                    ObjGateway.TransacAdquirente,
                                                    ObjGateway.IDPagamento,
                                                    "",
                                                    ObjGateway.NumeroParcelasEnviadas,
                                                    ObjGateway.NumeroParcelasEnviadas,
                                                    ObjGateway.TokenCard,
                                                    true, false);

                            if (ObjData.Erro != 0)
                            {
                                _erro = DataContextSQL.Erro;
                                _msgErro = DataContextSQL.MsgErro;

                                DataContextSQL.RollbackTransaction();
                                DataContextSQL.FecharConexao();

                                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "SetBaixaParcela Erro - " + _erro, false, 1);

                                return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                            }

                            Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "SetBaixaParcela OK" , false, 1);
                        }

                    }
                    //============================================================================================

                }
                //==========================================================================
                //FIM SOMENTE ASSINATURAS PAGAS
                //==========================================================================

                //commit 
                if (Erro == 0)
                {
                    DataContextSQL.CommitTransaction();
                    DataContextSQL.FecharConexao();
                }

                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Gravar Fechar Transacao - " + lstrSerieCTR + "|" + lintNuCTR.ToString() + "|" + lintNuDvCTR.ToString(), false,1);

                //==========================================================================

                //Atualiza Log com dados de retorno InterfacePagto
                if (_IDLogRastreio != 0)
                {
                    if (lbolGetway == true)
                    {
                        if (!string.IsNullOrEmpty(pstrDadosGateway))
                        {
                            ObjLogInterfaceBLL.SetUpdLogInterface(pstrDadosGateway, _msgErro, _IDLogRastreio);
                        }
                        else
                        {
                            ObjLogInterfaceBLL.SetUpdLogInterface("", _msgErro, _IDLogRastreio);
                        }


                        if (ObjLogInterfaceBLL.Erro != 0)
                        {
                            Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "SetUpdLogInterface - " + ObjLogInterfaceBLL.MsgErro, false);
                        }
                    }
                }

                return Utility.GetRetornoJSON("0", "Sucesso", lstrSerieCTR + "|" + lintNuCTR.ToString() + "|" + lintNuDvCTR.ToString());

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                DataContextSQL.RollbackTransaction();
                DataContextSQL.FecharConexao();

                Utility.SetLogXML("GravarAssinaturaIntegracao", "LogIntegracao", "Gravar Voltar Transacao - " + _msgErro, false,1);

                if (_IDLogRastreio != 0)
                {
                    LogInterfaceWEBBusiness ObjLogInterfaceBLL = new LogInterfaceWEBBusiness();

                    ObjLogInterfaceBLL.SetUpdLogInterface("", _msgErro, _IDLogRastreio);

                    if (ObjLogInterfaceBLL.Erro != 0)
                    {
                        Utility.SetLogXML("GravarAssinaturaIntegracao-GravaLogInterface-LogI", "Error", ObjLogInterfaceBLL.MsgErro, false, 1);
                    }
                }

                return Utility.GetRetornoJSON("-99", "GravarAssinaturaIntegracao99 - Error - " + _msgErro, "");

            }

        }

        public string GetProximoCTRJSON()
        {

            try
            {
                GravaAssinaturaData ObjData = new GravaAssinaturaData();

                DataContext.AbrirConexao();
                DataContext.BeginTransaction();

                string lstrRetCTR = ObjData.GetProximoContratonNOVO("VD",0,true);

                if (ObjData.Erro == 0 )
                {
                    if (lstrRetCTR != "")
                    {
                        DataContext.CommitTransaction();
                        DataContext.FecharConexao();

                    }
                    else
                    {
                        DataContext.RollbackTransaction();
                        DataContext.FecharConexao();
                    }

                }
                else
                {
                    DataContext.RollbackTransaction();
                    DataContext.FecharConexao(); 
                }

                return lstrRetCTR;

            }
            catch (Exception ex)
            {
                DataContext.RollbackTransaction();
                DataContext.FecharConexao();

                _erro = -99;
                _msgErro = ex.Message;
                return _erro.ToString() + "-" + _msgErro;
            }

        }


        public void GravarAssinaturaNOVO(DadosPessoaEntity ObjPS,
                                        DadosEntregaEntity ObjDE,
                                        DadosPagamentoEntity ObjPG)
        {

            _erro = 0;
            _msgErro = "";
            _lstrCTR = "";
            _lstrInfoPlano = "";
            _IDLogRastreio = 0;


            try
            {
                Utility.SetLogXML("GravarAssinatura", "Error", "0", false);

                //################################################################################################
                //para produto digital e se os dados do logradouro forem NULL vou pegar os dados do endereço
                //padrão na parametro global.
                //################################################################################################

                List<ProdutoEntity> ObjProdEnt = new List<ProdutoEntity>();
                ProdutoBusiness ObjProdBLL = new ProdutoBusiness();

                string lstrRetProdEnt = ObjProdBLL.GetProdutoListService(ObjPG.CD_PRODUTO);

                if (lstrRetProdEnt != "")
                {
                    ObjProdEnt = (List<ProdutoEntity>)JsonConvert.DeserializeObject(lstrRetProdEnt, ObjProdEnt.GetType());
                }

                if (ObjProdEnt != null)
                {
                    if (ObjProdEnt[0].ST_IND_ONLINE == 1) //DIGITAL
                    {
                        //LOGRADOURO DE PRODUTO ONLINE SEM PREENCHIMENTO
                        if ((ObjDE.ID_LOGR == 0) && (ObjDE.DS_LOGR == null))
                        {
                            ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                            ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                            ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                            if (ObjParamEnt != null)
                            {
                                if ((ObjDE.ST_LOGR_EXT != 0) && (ObjDE.ST_LOGR_EXT != 1))
                                {
                                    _erro = -1;
                                    _msgErro = "Para endereço não informado é obrigatório informar se o mesmo e dentro ou fora do País";

                                    return;
                                }

                                ViewLogrBusiness ObjLogrBLL = new ViewLogrBusiness();
                                List<DadosEntregaEntity> ObjLogrList = new List<DadosEntregaEntity>();

                                string lstrRetLogrEnt = "";

                                if (ObjDE.ST_LOGR_EXT == 0)
                                {
                                    lstrRetLogrEnt = ObjLogrBLL.GetViewLogrEntityServiceID(ObjParamEnt.CD_LOGR_PADRAO_EXTERIOR);
                                }
                                else if (ObjDE.ST_LOGR_EXT == 1)
                                {
                                    lstrRetLogrEnt = ObjLogrBLL.GetViewLogrEntityServiceID(ObjParamEnt.CD_LOGR_PADRAO_BRASIL);
                                }

                                if (lstrRetLogrEnt == "")
                                {
                                    _erro = -1;

                                    if (ObjDE.ST_LOGR_EXT == 0)
                                    {
                                        _msgErro = "Erro ao obter logradouro padrão do Exterior";
                                    }
                                    else
                                    {
                                        _msgErro = "Erro ao obter logradouro padrão do Brasil";
                                    }

                                    return;

                                }

                                ObjLogrList = (List<DadosEntregaEntity>)JsonConvert.DeserializeObject(lstrRetLogrEnt, ObjLogrList.GetType());

                                if (ObjLogrList != null)
                                {
                                    ObjDE.ID_LOGR = ObjLogrList[0].ID_LOGR;
                                    ObjDE.DS_TIPO = ObjLogrList[0].DS_TIPO;
                                    ObjDE.CD_TIPO = ObjLogrList[0].CD_TIPO;
                                    ObjDE.DS_LOGR = ObjLogrList[0].DS_LOGR;
                                    ObjDE.DS_MUNICIPIO = ObjLogrList[0].DS_MUNICIPIO;
                                    ObjDE.DS_BAIRRO = ObjLogrList[0].DS_BAIRRO;
                                    ObjDE.DS_UF = ObjLogrList[0].DS_UF;
                                    ObjDE.NU_CEP = ObjLogrList[0].NU_CEP;
                                    ObjDE.NU_RESID = "1";
                                }

                            }
                            else
                            {
                                _erro = -1;
                                _msgErro = "Erro ao obter dados do parâmetro goloba ed (1)";

                                return;
                            }
                        }
                        else
                        {

                            if ((ObjDE.ID_LOGR != 0) && (ObjDE.DS_LOGR != null))
                            {

                            }
                            else
                            {
                                ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                                ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                                ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                                if (ObjParamEnt != null)
                                {
                                    if ((ObjDE.ST_LOGR_EXT != 0) && (ObjDE.ST_LOGR_EXT != 1))
                                    {
                                        _erro = -1;
                                        _msgErro = "Para endereço não informado é obrigatório informar se o mesmo e dentro ou fora do País";

                                        return;
                                    }

                                    if (ObjDE.ST_LOGR_EXT == 0)
                                    {
                                        ObjDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_EXTERIOR;
                                    }
                                    else if (ObjDE.ST_LOGR_EXT == 1)
                                    {
                                        ObjDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_BRASIL;
                                    }

                                }
                                else
                                {
                                    _erro = -1;
                                    _msgErro = "Erro ao obter dados do parâmetro goloba ed (2)";

                                    return;
                                }

                            }
                        }
                    }
                    else //IMPRESSO
                    {
                        if (ObjDE == null)
                        {
                            _erro = -1;
                            _msgErro = "Erro ao converter JSON de Entrega";

                            return;
                        }

                        if ((ObjDE.ID_LOGR == 0) && (ObjDE.DS_LOGR != null))
                        {
                            ParametroGlobalEDEntity ObjParamEnt = new ParametroGlobalEDEntity();
                            ParametroGlobalBusiness ObjParamBLL = new ParametroGlobalBusiness();

                            ObjParamEnt = ObjParamBLL.GetParametroGlobalED();

                            if (ObjParamEnt != null)
                            {
                                if ((ObjDE.ST_LOGR_EXT == 0))
                                {
                                    _erro = -1;
                                    _msgErro = "Não é possível assinar um produto impresso para endereço digitado do exterior.";

                                    return;
                                }

                                ObjDE.ID_LOGR = ObjParamEnt.CD_LOGR_PADRAO_BRASIL;
                            }
                        }
                    }
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao localizar produto (validação de produto online)";

                    return;
                }

                Utility.SetLogXML("GravarAssinatura-Debug", "Error", "1", false);

                string lstrTipoParcCartao = "";

                //CARTAO
                if (ObjPG.CD_TP_FORMA_PAG == 6)
                {
                    lstrTipoParcCartao = ConfigurationManager.AppSettings["TipoParcCartao"];

                    if (lstrTipoParcCartao.Trim() == "")
                    {
                        _erro = -1;
                        _msgErro = "Tipo de Parcelamento do Cartão não parametrizado.";

                        Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                        return;
                    }

                    ObjPS.TIPO_PARC_CARTAO = int.Parse(lstrTipoParcCartao);

                }
                else
                {
                    ObjPS.TIPO_PARC_CARTAO = 0;
                }

                PlanoComercialBusiness ObjPlanoBLL = new PlanoComercialBusiness();
                PlanoComercialWebEntity ObjPlanoEnt = new PlanoComercialWebEntity();
                PlanoComercialWebEntity ObjPlanoEntLog = new PlanoComercialWebEntity();

                //Retorna na variavel InfoPlano para ser utilizada nas mensagens final.

                Utility.SetLogXML("GravarAssinatura-DEBUG", "Error", "1.0", false);

                ObjPlanoEnt = ObjPlanoBLL.GetParametrosPlano(ObjPG.CD_PLANO);

                ObjPlanoEntLog.CD_PLANO = ObjPG.CD_PLANO;

                if (ObjPlanoEnt == null)
                {
                    _erro = ObjPlanoBLL.Erro;
                    _msgErro = ObjPlanoBLL.MsgErro;

                    Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                    return;
                }

                _lstrInfoPlano = ObjPlanoEnt.DS_PLANO + "|" + ObjPlanoEnt.DS_TP_PAG + "|" +
                                 ObjPlanoEnt.DIAS_ENTREGA + "|" + ObjPlanoEnt.DS_TP_ASSINATURA + "|" +
                                 ObjPlanoEnt.NU_PARCELA;

                Utility.SetLogXML("GravarAssinatura", "Error", "1.1", false);

                //Verifica se existe preco parametrizado para a campanha/plano

                ObjPlanoEnt = ObjPlanoBLL.GetValorPlano(ObjPG.CD_CAMPANHA, ObjPG.CD_PLANO, ObjDE.ID_LOGR);

                if (ObjPlanoEnt == null)
                {
                    _erro = ObjPlanoBLL.Erro;
                    _msgErro = ObjPlanoBLL.MsgErro;

                    Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                    return;
                }

                ObjPlanoEntLog.VA_TOTAL_CENT = ObjPlanoEnt.VA_TOTAL_CENT;
                ObjPlanoEntLog.VA_PARC_CENT = ObjPlanoEnt.VA_PARC_CENT;

                string lstrValor1ParcelaCentavos = ObjPlanoEnt.VA_PARC_CENT;
                string lstrValorTotalCentavos = ObjPlanoEnt.VA_TOTAL_CENT;

                //Retorna na valor do plano para ser utilizada nas mensagens final.
                string lstrValorParc1 = (double.Parse(ObjPlanoEnt.VA_PARC_CENT) / 100).ToString("0.00");
                string lstrValorParcDemais = (double.Parse(ObjPlanoEnt.VA_DEMAIS_PARC_CENT) / 100).ToString("0.00");
                string lstrValorTotal = (double.Parse(ObjPlanoEnt.VA_TOTAL_CENT) / 100).ToString("0.00");

                _lstrInfoPlano = _lstrInfoPlano + "|" + lstrValorTotal + "|" + lstrValorParc1 + "|" + lstrValorParcDemais;

                //10-@VALORTOTAL/11-@VALORPRIMPARC/12-@VALORDEMAISPARC
                //===========================================================================

                Utility.SetLogXML("GravarAssinatura", "Error", "2", false);


                GravaAssinaturaData ObjData = new GravaAssinaturaData();

                //===========================================================================
                //PEGA O ULTIMO CONTRATO
                //===========================================================================
                string[] lstrVetorCTR;
                string lstrCTRTemp;

                string lstrSerieCTR = "";
                int lintNuCTR = 0;
                int lintNuDvCTR = 0;

                Utility.SetLogXML("GravarAssinatura", "Error", "Pegar CTR - 1", false);
                lstrCTRTemp = ObjData.GetProximoContrato("VD", 0, true);
                Utility.SetLogXML("GravarAssinatura", "Error", "Pegar CTR - 2", false);

                if (lstrCTRTemp.Trim() != "")
                {
                    lstrVetorCTR = lstrCTRTemp.Split(';');

                    if (lstrVetorCTR.Length > 0)
                    {
                        lstrSerieCTR = lstrVetorCTR[0];
                        lintNuCTR = int.Parse(lstrVetorCTR[1]);
                        lintNuDvCTR = int.Parse(lstrVetorCTR[2]);

                        _lstrCTR = lstrCTRTemp;
                    }
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro Novo Contrato - Não foi possível obter um contrato para essa solicitação.";

                    Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                    return;
                }
                //===========================================================================

                bool lbolGetway = bool.Parse(ConfigurationManager.AppSettings["UsaGetwayPagamento"]);
                string lstrRetGetway = "";

                //Grava Primeira parte do Log de Rastreio.

                LogInterfaceWEBBusiness ObjLogInterfaceBLL = new LogInterfaceWEBBusiness();
                ObjLogInterfaceBLL.SetLogInterface(0, ObjPS, ObjDE, ObjPG, ObjPlanoEntLog, null, "GravarVenda", "", "Venda", lstrSerieCTR, lintNuCTR, lintNuDvCTR, lbolGetway, "", "");

                _IDLogRastreio = ObjLogInterfaceBLL.IDLogRatreamento;

                //===========================================================================


                bool lbolAdmCobraGateWay = false;

                if (lbolGetway == true)
                {

                    if (ObjPG.CD_TP_FORMA_PAG == 6)
                    {
                        AdmCartaoBusiness ObjBLLAdmCartao = new AdmCartaoBusiness();
                        lbolAdmCobraGateWay = ObjBLLAdmCartao.GetAdmCobraGateWay(ObjPG.CD_FONTE_COBRANCA);
                    }

                }

                Utility.SetLogXML("GravarAssinatura", "Error", "3", false);

                DataContext.AbrirConexao();
                DataContext.BeginTransaction();

                //GRAVA DADOS PESSOAIS
                ObjData.SetDadosPessoa(ObjPS, ObjDE, ObjPG, lstrSerieCTR, lintNuCTR, lintNuDvCTR, true, false);
                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    DataContext.RollbackTransaction();
                    DataContext.FecharConexao();

                    return;
                }

                Utility.SetLogXML("GravarAssinatura", "Error", "4", false);
                //===========================================================================

                //GRAVA DADOS TELEFONE RESIDENCIAL
                if ((ObjPS.NU_TEL != null) && (ObjPS.NU_TEL.Trim() != ""))
                {
                    ObjData.SetDadosTelefone(ObjPS, lstrSerieCTR, lintNuCTR, lintNuDvCTR, 0, true, false);
                    if (ObjData.Erro != 0)
                    {
                        _erro = ObjData.Erro;
                        _msgErro = ObjData.MsgErro;

                        DataContext.RollbackTransaction();
                        DataContext.FecharConexao();

                        Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                        return;
                    }

                }
                //===========================================================================

                Utility.SetLogXML("GravarAssinatura", "Error", "5", false);

                //GRAVA DADOS TELEFONE CELUAR SE EXISTIR
                if ((ObjPS.NU_CEL != null) && (ObjPS.NU_CEL.Trim() != ""))
                {
                    ObjData.SetDadosTelefone(ObjPS, lstrSerieCTR, lintNuCTR, lintNuDvCTR, 1, true, false);

                    if (ObjData.Erro != 0)
                    {
                        _erro = ObjData.Erro;
                        _msgErro = ObjData.MsgErro;

                        DataContext.RollbackTransaction();
                        DataContext.FecharConexao();

                        Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                        return;
                    }
                }
                //============================================================================================
                Utility.SetLogXML("GravarAssinatura", "Error", "6", false);

                //Valida se é um plano que grava tabela de DEBITO_AUTOMATICO_CTR
                if (IsGravaDebito(ObjPG.CD_TP_FORMA_PAG) == true)
                {
                    //====================================================================
                    //RECUPERA A CHAVE DE CRYPT DO CARTAO CREDITO
                    //====================================================================
                    Utility ObjUtilCript = new Utility();

                    string lstrChaveCryptCC = ObjUtilCript.GetCriptCartaoCIR2000();

                    ObjPG.CHAVE_CRYPT_CC = lstrChaveCryptCC;

                    //====================================================================

                    //GRAVA DADOS DEBITO
                    ObjData.SetDadosDebito(ObjPG, lstrSerieCTR, lintNuCTR, lintNuDvCTR, true, false);

                    if (ObjData.Erro != 0)
                    {
                        _erro = ObjData.Erro;
                        _msgErro = ObjData.MsgErro;

                        DataContext.RollbackTransaction();
                        DataContext.FecharConexao();

                        Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                        return;
                    }
                    //============================================================================================
                }

                Utility.SetLogXML("GravarAssinatura", "Error", "7", false);

                //commit dados da venda.
                if (Erro == 0)
                {
                    DataContext.CommitTransaction();
                    DataContext.FecharConexao();

                }



                //Begin dados de pagamento
                DataContext.AbrirConexao();
                DataContext.BeginTransaction();

                Utility.SetLogXML("GravarAssinatura", "Error", "8", false);

                //INTERFACE COM GETWAY DE PAGAMENTO
                int lintTotalParcelasPagas = 0;
                int lintTotalParcelasEnviadas = 0;
                bool lbolErro54 = false;

                if (lbolGetway == true)
                {
                    if (ObjPG.CD_TP_FORMA_PAG == 6) //CARTAO
                    {
                        if (lbolAdmCobraGateWay == true)
                        {


                            //Valida se a fonte de cobrança carta envia para o gateway
                            string lstrEmpresaGetway = ConfigurationManager.AppSettings["EmpresaGetway"];
                            if (string.IsNullOrEmpty(lstrEmpresaGetway))
                            {
                                _erro = -1;
                                _msgErro = "Empresa responsavel por processar o pagamento de cartão não localizada.";

                                Utility.SetLogXML("GravarAssinatura", "Error", _msgErro, false);

                                return;
                            }


                            string lsParm = "";

                            if (lstrEmpresaGetway.Trim().ToUpper() == "MAXIPAGO")
                            {
                                lsParm = lsParm + "Venda" + ",";                        //TipoInterface
                                lsParm = lsParm + lstrSerieCTR + lintNuCTR.ToString("0000000000") + lintNuDvCTR.ToString() + ",";
                                lsParm = lsParm + ObjPG.NU_CARTAO + ",";
                                lsParm = lsParm + ObjPG.DT_VALID.Substring(3, 7) + ","; //pegar somente mes/ano
                                lsParm = lsParm + ObjPG.NU_CVV + ",";

                                if (lstrTipoParcCartao.Trim() == "1")
                                {                                                       //PARCELADO PELA ADM
                                    lsParm = lsParm + lstrValorTotal.Replace(',', '.') + ",";             //VALOR TOTAL
                                    lsParm = lsParm + ObjPG.NU_PARCELA + ",";           //TOTAL DE PARCELAS 

                                    lintTotalParcelasPagas = ObjPG.NU_PARCELA;
                                    lintTotalParcelasEnviadas = 0;
                                }
                                else
                                {                                                       //PARCELADO PELA JORNAL
                                    lsParm = lsParm + lstrValorParc1.Replace(',', '.') + ",";             //VALOR DA PARCELA
                                    lsParm = lsParm + "1,";                             //PARCELA = 1 (PRIMEIRA)

                                    lintTotalParcelasPagas = 1;
                                    lintTotalParcelasEnviadas = 0;
                                }

                                lsParm = lsParm + lstrTipoParcCartao;

                            }
                            else //BRASPAG & ESITEF
                            {
                                lsParm = lsParm + "Venda" + ",";                        //TipoInterface
                                lsParm = lsParm + ObjPG.NM_PESSOA_CARTAO + ",";

                                if (lstrTipoParcCartao.Trim() == "1")
                                {                                                       //PARCELADO PELA ADM
                                    lsParm = lsParm + lstrValorTotalCentavos + ",";     //VALOR TOTAL
                                    lsParm = lsParm + ObjPG.NU_PARCELA + ",";           //TOTAL DE PARCELAS 

                                    lintTotalParcelasPagas = ObjPG.NU_PARCELA;
                                    lintTotalParcelasEnviadas = 0;
                                }
                                else
                                {                                                       //PARCELADO PELA JORNAL
                                    lsParm = lsParm + lstrValor1ParcelaCentavos + ",";  //VALOR DA PARCELA
                                    lsParm = lsParm + "1,";                             //PARCELA = 1 (PRIMEIRA)

                                    lintTotalParcelasPagas = 1;
                                    lintTotalParcelasEnviadas = 0;
                                }

                                lsParm = lsParm + ObjPG.NU_CARTAO + ",";
                                lsParm = lsParm + ObjPG.NM_PESSOA_CARTAO + ",";
                                lsParm = lsParm + ObjPG.DT_VALID.Substring(3, 7) + ","; //pegar somente mes/ano
                                lsParm = lsParm + ObjPG.NU_CVV + ",";
                                lsParm = lsParm + ObjPG.NM_BANDEIRA + ",";
                                lsParm = lsParm + lstrSerieCTR + lintNuCTR.ToString("0000000000") + lintNuDvCTR.ToString() + ",";
                                lsParm = lsParm + "S" + ";"; //Gerar TOKEN

                                lsParm = lsParm.Substring(0, lsParm.Length - 1);


                            }

                            Utility.SetLogXML("GravarAssinatura-InterfacePagto-Envio", "Info", lsParm, false);

                            GerenciaInterface ObjInterface = new GerenciaInterface();
                            ObjInterface.ExecutarInterface("Venda", "Cartao", lsParm);

                            if (ObjInterface.Erro != 0)
                            {
                                _erro = ObjInterface.Erro;
                                _msgErro = ObjInterface.MsgErro;

                                DataContext.RollbackTransaction();
                                DataContext.FecharConexao();

                                Utility.SetLogXML("GravarAssinatura-InterfacePagto-Erro-Rollback", "Error", lsParm + " - " + _msgErro, false);

                                //Atualiza Log com dados de retorno InterfacePagto
                                if (_IDLogRastreio != 0)
                                {
                                    if (lbolGetway == true)
                                    {
                                        ObjLogInterfaceBLL.SetUpdLogInterface(lstrRetGetway, _msgErro, _IDLogRastreio);

                                        if (ObjLogInterfaceBLL.Erro != 0)
                                        {
                                            Utility.SetLogXML("GravarAssinatura-GravaLogInterface", "Error", ObjLogInterfaceBLL.MsgErro, false);
                                        }
                                    }
                                }
                                return;
                            }

                            //ATUALIZA DADOS PARA UTILIZAR NA BAIXA DAS PARCELAS JÁ APROVADAS.
                            string[] lstrVetRet = ObjInterface.VertorRetorno;

                            lstrRetGetway = "";
                            lstrRetGetway = lstrSerieCTR + "," +
                                            lintNuCTR.ToString() + "," +
                                            lintNuDvCTR.ToString() + "," +
                                            lstrVetRet[2] + "," +
                                            lstrVetRet[3] + "," +
                                            lstrVetRet[4] + "," +
                                            lstrVetRet[5] + "," +
                                            lintTotalParcelasPagas.ToString() + "," +
                                            lintTotalParcelasEnviadas.ToString();

                            Utility.SetLogXML("GravarAssinatura-InterfacePagto-Retorno", "Info", lstrRetGetway, false);

                            ObjData.SetBaixaParcela(lstrSerieCTR,
                                                    lintNuCTR,
                                                    lintNuDvCTR,
                                                    lstrVetRet[2],
                                                    lstrVetRet[3],
                                                    lstrVetRet[4],
                                                    lstrVetRet[5],
                                                    "",
                                                    lintTotalParcelasPagas,
                                                    lintTotalParcelasEnviadas,
                                                    "CardToken",
                                                    true, false);

                            if (ObjData.Erro != 0)
                            {
                                _erro = DataContext.Erro;
                                _msgErro = DataContext.MsgErro;

                                DataContext.RollbackTransaction();
                                DataContext.FecharConexao();

                                Utility.SetLogXML("GravarAssinatura-InterfacePagto-SetBaixaParcela", "Error", _msgErro, false);

                                return;
                            }

                            lbolErro54 = true;

                            //Atualiza Log com dados de retorno InterfacePagto
                            if (_IDLogRastreio != 0)
                            {
                                if (lbolGetway == true)
                                {
                                    ObjLogInterfaceBLL.SetUpdLogInterface(lstrRetGetway, _msgErro, _IDLogRastreio);

                                    if (ObjLogInterfaceBLL.Erro != 0)
                                    {
                                        Utility.SetLogXML("GravarAssinatura-GravaLogInterface-Log", "Error", ObjLogInterfaceBLL.MsgErro, false);
                                    }
                                }
                            }

                        }

                        //============================================================================================
                    }


                }
                //============================================================================================

                Utility.SetLogXML("GravarAssinatura", "Error", "9", false);
                //commit dados de pagamento.
                if (Erro == 0)
                {
                    DataContext.CommitTransaction();
                    DataContext.FecharConexao();

                }

                //CORRIGIR O ERRO 54 PARA ASSINATURAS DE CARTAO DE CREDITO COM PAGAMENTO ONLINE.
                if (lbolErro54 == true)
                {
                    SetStatusBaseValidErro(lstrSerieCTR, lintNuCTR, lintNuDvCTR, 0, 54, 1);

                    if (_erro != 0)
                    {
                        Utility.SetLogXML("DadosPagtoFinalizar-SetStatusBaseValidErro", "Erro", _msgErro, false);
                    }
                }

                //Finaliza registro do abandono de carrinho
                string lstrDOCAbandono = "";
                string lstrEmailAbandono = "";

                if ((ObjPS.TP_PESSOA == 1) || (ObjPS.TP_PESSOA == 2))
                {
                    lstrDOCAbandono = ObjPS.NU_CPF;
                    lstrEmailAbandono = ObjPS.DS_EMAIL;
                }
                else
                {
                    lstrDOCAbandono = ObjPS.NU_CNPJ;
                    lstrEmailAbandono = ObjPS.DS_EMAIL;
                }

                AbandonoBusiness ObjAbandono = new AbandonoBusiness();
                ObjAbandono.SetAbandonoService(lstrDOCAbandono, lstrEmailAbandono, null, 4, 1, lstrSerieCTR, lintNuCTR, lintNuDvCTR);

                if (ObjAbandono.Erro != 0)
                {
                    Utility.SetLogXML("DadosPagtoFinalizar-SetAbandonoService", "Erro", ObjAbandono.MsgErro, false);
                }

                Utility.SetLogXML("GravarAssinatura", "Error", "10", false);
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                Utility.SetLogXML("GravarAssinatura-InterfacePagto", "Error", _msgErro, false);

                DataContext.RollbackTransaction();
                DataContext.FecharConexao();

                if (_IDLogRastreio != 0)
                {
                    LogInterfaceWEBBusiness ObjLogInterfaceBLL = new LogInterfaceWEBBusiness();

                    ObjLogInterfaceBLL.SetUpdLogInterface("", _msgErro, _IDLogRastreio);

                    if (ObjLogInterfaceBLL.Erro != 0)
                    {
                        Utility.SetLogXML("GravarAssinatura-GravaLogInterfaceErro-Ex", "Error", ObjLogInterfaceBLL.MsgErro, false);
                    }
                }

                Utility.SetLogXML("GravarAssinatura", "Error", "99", false);

            }

        }


        public void SetStatusBaseValidErro(string pstrSerieCTR,
                                           int pintCTR,
                                           int pintDvCTR,                                             
                                           int pintNuPeriodo,
                                           int pintTipoStatus,
                                           int pintStCorrecao)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //PEGA O CONTABIL DA PESSOA
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Int32 lintIDPessoa;
                ContratoAssinanteData ObjDataCTR = new ContratoAssinanteData();

                lintIDPessoa = ObjDataCTR.GetContabilContrato(pintCTR, pintDvCTR, pstrSerieCTR);

                if (lintIDPessoa == 0)
                {
                    _erro = ObjDataCTR.Erro;
                    _msgErro = ObjDataCTR.MsgErro;
                    return;
                }
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //SETA STATUS NA BASE VALID STATUS ( ERRO 54 )
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


                GravaAssinaturaData ObjData = new GravaAssinaturaData();

                ObjData.SetStatusBaseValidSQL(pstrSerieCTR,
                                              pintCTR,
                                              pintDvCTR,
                                              lintIDPessoa,
                                              pintNuPeriodo,
                                              pintTipoStatus,
                                              pintStCorrecao,
                                              DateTime.Now.ToString("dd/MM/yyyy"),
                                              true,
                                              false);


                return;

            }
            catch (Exception ex)
            {
                DataContext.RollbackTransaction();
                DataContext.FecharConexao();

                _erro = -99;
                _msgErro = ex.Message;
                return;
            }

        }

        public void SetDadosLAHAR(DadosPessoaEntity pObjEntPS,
                                   SessionParmPlanoEntity pObjConfigParmPlano,
                                   string pstrEtapa)
        {
            try
            {
                GravaAssinaturaBusiness ObjBusinnes = new GravaAssinaturaBusiness();

                string lstrNomeEmpresa = ConfigurationManager.AppSettings["EMPRESA"];

                if (lstrNomeEmpresa.ToUpper() == "JORNAL DA CIDADE")
                {
                    //Exemplo de geração da linha
                    //nome_contato | VINICIUS NUNES - email_contato | vinicius_tst1@cwa.com.br";

                    string lstrDados = "";

                    if (pstrEtapa == "1")
                    {
                        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                        //DADOS PESSOAL
                        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                        if (pObjEntPS.TP_PESSOA == 1)
                        {
                            lstrDados = lstrDados + "9M6DRlsGtR|F #"; // TIPO PESSOA
                            lstrDados = lstrDados + "nome_contato|" + pObjEntPS.DS_NOME + "#";

                            if (pObjEntPS.DS_SOBRE_NOME.Trim() != "")
                            {
                                lstrDados = lstrDados + "sobrenome|" + pObjEntPS.DS_SOBRE_NOME + "#";
                            }
                            else
                            {
                                lstrDados = lstrDados + "sobrenome| #";
                            }
                        }
                        else
                        {
                            lstrDados = lstrDados + "9M6DRlsGtR|J #"; // TIPO PESSOA
                            lstrDados = lstrDados + "nome_contato|" + pObjEntPS.DS_NOME_EMPRESA + "#";
                            lstrDados = lstrDados + "sobrenome| #";
                        }

                        if (pObjEntPS.DS_EMAIL.Trim() != "")
                        {
                            lstrDados = lstrDados + "email_contato|" + pObjEntPS.DS_EMAIL + "#";
                        }
                        else
                        {
                            lstrDados = lstrDados + "email_contato| #";
                        }

                        lstrDados = lstrDados + "sexo|" + pObjEntPS.TP_PESSOA + "#";
                        lstrDados = lstrDados + "dtaniversario|" + pObjEntPS.DT_NASC + "#";
                        lstrDados = lstrDados + "tel_celular|" + pObjEntPS.NU_DDDCEL + pObjEntPS.NU_CEL + "#";
                        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                        //DADOS CAMPANHA/PLANO/PRODUTO
                        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                        lstrDados = lstrDados + "HFPo9NmlBP|" + pObjConfigParmPlano.CD_CAMPANHA.ToString("00000") + "-" + pObjConfigParmPlano.DS_CAMPANHA + "#"; //CAMPANHA
                        lstrDados = lstrDados + "wSn0Fj7Goo|" + pObjConfigParmPlano.CD_PLANO.ToString("00000") + "-" + pObjConfigParmPlano.DS_PLANO + "#"; //PLANO
                        lstrDados = lstrDados + "TcfuaAwF7Q|" + pObjConfigParmPlano.CD_PRODUTO.ToString("00000") + "#"; //PRODUTO
                        lstrDados = lstrDados + "0M7ZT1hbPI|1"; //ETAPA
                        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    }
                    else if (pstrEtapa == "2")
                    {

                        if (pObjEntPS.DS_EMAIL.Trim() != "")
                        {
                            lstrDados = lstrDados + "email_contato|" + pObjEntPS.DS_EMAIL + "#";
                        }
                        else
                        {
                            lstrDados = lstrDados + "email_contato| #";
                        }
                        lstrDados = lstrDados + "0M7ZT1hbPI|2"; //ETAPA
                    }
                    else if (pstrEtapa == "3")
                    {

                        if (pObjEntPS.DS_EMAIL.Trim() != "")
                        {
                            lstrDados = lstrDados + "email_contato|" + pObjEntPS.DS_EMAIL + "#";
                        }
                        else
                        {
                            lstrDados = lstrDados + "email_contato| #";
                        }
                        lstrDados = lstrDados + "0M7ZT1hbPI|3"; //ETAPA
                    }
                    else if (pstrEtapa == "4")
                    {

                        if (pObjEntPS.DS_EMAIL.Trim() != "")
                        {
                            lstrDados = lstrDados + "email_contato|" + pObjEntPS.DS_EMAIL + "#";
                        }
                        else
                        {
                            lstrDados = lstrDados + "email_contato| #";
                        }
                        lstrDados = lstrDados + "0M7ZT1hbPI|4"; //ETAPA
                    }
                    else if (pstrEtapa == "5")
                    {
                        //Erro para pagamento de cartao de crédito
                        if (pObjEntPS.DS_EMAIL.Trim() != "")
                        {
                            lstrDados = lstrDados + "email_contato|" + pObjEntPS.DS_EMAIL + "#";
                        }
                        else
                        {
                            lstrDados = lstrDados + "email_contato| #";
                        }
                        lstrDados = lstrDados + "0M7ZT1hbPI|5"; //ETAPA
                    }

                    ObjBusinnes.SetGravarIntegracaoLahar(lstrDados);
                }

            }
            catch (Exception ex)
            {
                Utility.SetLogXML("DadosPessoa-SetDadosLAHAR", "Erro", ex.Message, false);
            }
        }
        public void SetGravarIntegracaoLahar(string pstrCampos)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrToken = ConfigurationManager.AppSettings["TokenLahar"];

                if (lstrToken.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "Token não parametrizado para este processo.";
                }

                string lstrUrlPostLahar = ConfigurationManager.AppSettings["UrlPostLahar"];

                if (lstrUrlPostLahar.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "URL para conexão não parametrizada para este processo.";
                }

                string lstrMethodLahar = ConfigurationManager.AppSettings["MethodLahar"];

                if (lstrMethodLahar.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "Metodo não parametrizado para este processo.";
                }

                string lstrContentTypeLahar = ConfigurationManager.AppSettings["ContentTypeLahar"];

                if (lstrContentTypeLahar.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "ContentType não parametrizado para este processo.";
                }

                string lstrFormularioLahar = ConfigurationManager.AppSettings["FormularioLahar"];

                if (lstrFormularioLahar.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "Formulario não parametrizado para este processo.";
                }

                string lstrNomeEmpresa = ConfigurationManager.AppSettings["EMPRESA"];

                string lstrRet = ObjService.SetIntegracaoLahar(lstrToken, 
                                                               lstrUrlPostLahar, 
                                                               lstrMethodLahar, 
                                                               lstrContentTypeLahar, 
                                                               lstrFormularioLahar, 
                                                               pstrCampos, 
                                                               lstrNomeEmpresa);

                string[] lstrVetRet = lstrRet.Split('|');

                if (lstrVetRet != null)
                {
                    _erro = int.Parse(lstrVetRet[0]);
                    _msgErro = lstrVetRet[1];
                }
                else
                {
                    _erro = -90;
                    _msgErro = "Informação do retorno NULL";
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return;
            }
        }

    }
}
