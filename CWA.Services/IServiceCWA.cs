
using System;
using System.Collections.Generic;
using System.ServiceModel;

using System.ServiceModel.Web;

using CWA.Venda.Entity;

using RestSharp;


namespace CWA.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceCWA" in both code and config file together.
    [ServiceContract]
    public interface IServiceCWA
    {
        [OperationContract]
        string[] BRASPAG_VendaSimplesCartao(string pstrUrlPost,
                                          string pstrContentType,
                                          string pstrMethod,
                                          string pstrMerchantID,
                                          string pstrMerchantKey,
                                          string pstrRequestId,
                                          string pstrProviderPagamento,
                                          string pstrNomeComprador,
                                          string pstrValor,
                                          string pstrParcela,
                                          string pstrCartao,
                                          string pstrNomeCompradorImpressoCartao,
                                          string pstrValidade,
                                          string pstrCVV,
                                          string pstrBandeira,
                                          string pstrIdentificadorVenda,
                                          string pstrSetToken = "N");

        [OperationContract]
        string[] BRASPAG_VendaSimplesCartaoToken(string pstrUrlPost,
                                                 string pstrContentType,
                                                string pstrMethod,
                                                string pstrMerchantID,
                                                string pstrMerchantKey,
                                                string pstrRequestId,
                                                string pstrProviderPagamento,
                                                string pstrNomeComprador,
                                                string pstrValor,
                                                string pstrParcela,
                                                string pstrCardToken,
                                                string pstrCVV,
                                                string pstrBandeira,
                                                string pstrIdentificadorVenda);

        [OperationContract]
        string[] BRASPAG_SetExtornoCartao(string pstrUrlPost,
                                          string pstrContentType,
                                          string pstrMethod,
                                          string pstrMerchantID,
                                          string pstrMerchantKey,
                                          string pstrRequestId,
                                          string pstrPaymentId,
                                          string pstrValor);


        [OperationContract]
        string [] GetStatusContrato(string pstrCodAssinante, string pstrCodContrato);

        [OperationContract]
        string GetStatusContratoJSON(string pstrCodAssinante, string pstrCodContrato);

        [OperationContract]
        [WebInvoke(Method = "POST",
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Xml,
                   BodyStyle = WebMessageBodyStyle.Bare,
                   UriTemplate = "AutenticarAcessoMirante/{token}")]
        string [] GetLoginValidoMirante(string Token);

        [OperationContract]
        string GetCarteiraClubeMiranteJSON(string pstrCodAssinante, string pstrCodContrato);

        [OperationContract]
        ClubeAssinanteEntity GetCarteiraClubeMirante(string pstrCodAssinante, string pstrCodContrato);

        [OperationContract]
        string ConsomeWebServiceDinamico(string pstrSoapSTR,
                                                string pstrFormatParametro,
                                                string pstrUrlWSclient,
                                                string pstrMetodoNomeWSClient,
                                                string pstrUriSoap,
                                                string pstrContentType,
                                                string pstrAccept,
                                                string pstrMethod,
                                                Dictionary<string, string> pdicParametros);

        [OperationContract]
        string ConsomeApiQualityGoiania(string ptrsURL);

        [OperationContract]
        string GetVersaoWS();

        [OperationContract]
        string[] BRASPAG_GetVendaMerchantOrderId(string pstrUrlPost,
                                                        string pstrContentType,
                                                        string pstrMethod,
                                                        string pstrMerchantID,
                                                        string pstrMerchantKey,
                                                        string pstrRequestId,
                                                        string pstrMerchantOrderId);
        [OperationContract]
        string[] BRASPAG_GetVendaPaymentId(string pstrUrlPost,
                                           string pstrContentType,
                                           string pstrMethod,
                                           string pstrMerchantID,
                                           string pstrMerchantKey,
                                           string pstrRequestId,
                                           string pstrPaymentId);

        [OperationContract]
        string SetGeraNotaVA_GO(string pstrTipoEndPointClient,
                                string pstrEndPointClient,
                                string pstrBanco,
                                string pstrServidor,
                                string pstrUsuario,
                                string pstrDataInicio,
                                string pstrDataFim,
                                string pstrPathLog);


        [OperationContract]
        string GetDadosContratoJSON(string pstrCodAssinante, string pstrCodContrato);

        [OperationContract]
        string GetRamoAtividadeJSON(int pintTipo, int pintSituacao);

        [OperationContract]
        List<RamoAtividadeEntity> GetRamoAtividade(int pintTipo, int pintSituacao);

        [OperationContract]
        string GetCuboClientejcJSON();

        [OperationContract]
        List<CuboClienteJCEntity> GetCuboClientejc();

        [OperationContract]
        string GetCuboAssinaturajcJSON(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0);

        [OperationContract]
        List<CuboAssinaturaJCEntity> GetCuboAssinaturajc(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0);

        [OperationContract]
        string GetCuboRecibojcJSON(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0);

        [OperationContract]
        List<CuboReciboJCEntity> GetCuboRecibojc(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0);

        [OperationContract]
        string GetCuboProdutojcJSON();

        [OperationContract]
        List<CuboProdutoJCEntity> GetCuboProdutojc();

        [OperationContract]
        string GetCuboProdAgregjcJSON(Int32 psCdcontabil = 0);

        [OperationContract]
        List<CuboProdAgregJCEntity> GetCuboProdAgregjc(Int32 psCdcontabil = 0);

        [OperationContract]
        string GetCuboTipoRecJSON();

        [OperationContract]
        List<CuboTiposRecJCEntity> GetCuboTiposRecjc();

        [OperationContract]
        string GetCuboMovRecJSON(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0);

        [OperationContract]
        List<CuboMovRecJCEntity> GetCuboMovRecjc(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0);

        [OperationContract]
        string GetConteudoDestaque(string pstrPathConteudo, string pstrPathImgMKT);

        [OperationContract]
        string GetConteudoPromocao(string pstrPathConteudo, string pstrPathImgMKT);

        [OperationContract]
        string GetConteudoDetalhe(string pstrPathConteudo, string pstrPathImgMKT, int pintIDCampanha, int pintIDPlano);

        [OperationContract]
        string GetMontarBannerDestaque();

        [OperationContract]
        string GetRamoAtividadeListJSON(int pintTipoRamo);

        [OperationContract]
        string GetPrecoCampanhaPlanoProdutoJSON(int pintIDCampaha, int pintIDPlano, int pintProduto);

        [OperationContract]
        string GetTipoLogradouroListJSON();

        [OperationContract]
        string GetViewLogrEntityJSON(string pstrCEP);

        [OperationContract]
        string GetCodRoteirizacaoJSON(int pintIDProduto, int pintIDLogr, int pintNumeroResid);

        [OperationContract]
        string GetParametroGlobalWEBJSON();

        [OperationContract]
        string GetBandeiraCartaoListJSON();

        [OperationContract]
        string GetAdmCartaoListJSON(int pintBandeira);

        [OperationContract]
        string GetAdmCobraGateWayJSON(int pintIDAdmCartao);

        [OperationContract]
        string GetBandeiraCartaoListNovoJSON();

        [OperationContract]
        string GetValidarListaNegraJSON(ListaNegraEntity pObjEnt);

        [OperationContract]
        string GetContabilContratoJSON(Int32 pintCTR, int pintDVCTR, string pstrSerieCTR);

        [OperationContract]
        string GetParametrosPlanoJSON(int pintIDPlano);

        [OperationContract]
        string GetTipoAssinaturaListJSON(int pintIDProduto, int pintIDCampanha);

        [OperationContract]
        string GetTipoPagamentoListJSON(int pintIDProduto, int pintIDCampanha,int pintIDTipoAssinatura, int pintTipoEntrega);

        [OperationContract]
        string GetBancoDebitoListJSON(int pintIDProdudo);

        [OperationContract]
        string GravarAssinaturaService(DadosPessoaEntity ObjPS,
                                       DadosEntregaEntity ObjDE,
                                       DadosPagamentoEntity ObjPG);
        [OperationContract]
        string GravarAbandonoService(string pstrDOC, string pstrEmail, AbandonoEntity pObjEnt, int pintEtapa, int pintStatus,
                                     string pstrNuSerieCTR = "", Int32 pintNuCTR = 0, int pintNuDvCTR = 0);

        [OperationContract]
        string GetAbandonoLojaService(string pstrDOC, int pintTipo);

        [OperationContract]
        string SendMailAPI(string pstrUrlBasePOST,
                                  string pstrAPIKey,
                                  string pstrDomain,
                                  string pstrFromMail,
                                  string pstrToMail,
                                  string pstrSubject,
                                  string pstrText,
                                  int pintTipoEmail = 2,
                                  string pstrPathAnexo = "",
                                  string pstrFileAnexo = "");


        [OperationContract]
        string GetLoginUsuarioJSON(string pstrUsuario, string pstrSenha);

        [OperationContract]
        string GetCampanhaListJSON(int pintIDCamp = 0);

        [OperationContract]
        string GetPlanoCampanhaJSON(int pintIDCampanha);

        [OperationContract]
        string GetCampanhaPlanoWEBJSON(int pintIDCampanha);

        [OperationContract]
        string GravaCampanhaPlanoWEB(CampanhaPlanoWEBEntity ObjEnt);

        [OperationContract]
        string AtualizaCampanhaPlanoWEB(CampanhaPlanoWEBEntity ObjEnt);

        [OperationContract]
        string DeleteCampanhaPlanoWEB(CampanhaPlanoWEBEntity ObjEnt);

        [OperationContract]
        string GetTotalDestaque();

        [OperationContract]
        string SetGravarRegistroBoletoBradesco(BoletoRegistroOnLineEntity pObjRegistro,
                                               string pstrPathCertificado,
                                               string pstrSenhaCertificado,
                                               string pstrURLEndPointBanco);


        [OperationContract]

        string[] ESITEF_VendaAutomaticaTransacao(string pstrUrlPost,
                                                 string pstrContentType,
                                                 string pstrMethod,
                                                 string pstrMerchantID,
                                                 string pstrMerchantKey,
                                                 string pstrMerchantUsn, //guid
                                                 string pstrTipoParcelamento, //3 parcelado pela ADM com juros - 4 parcelado pelo Jornal sem juros
                                                 string pstrValor,
                                                 string pstrParcela,
                                                 string pstrBandeira,
                                                 string pstrIdentificadorVenda);


        [OperationContract]
        string[] ESITEF_VendaAutomaticaPagamento(string pstrUrlPost,
                                                 string pstrContentType,
                                                 string pstrMethod,
                                                 string pstrMerchantID,
                                                 string pstrMerchantKey,
                                                 string pstrnit,
                                                 string pstrNumeroCartao,
                                                 string pstrDataValidade,
                                                 string pstrCVV
                                                );

        [OperationContract]
        string GetLerContratosCentralJSON(int pintCD_CONTABIL_PESSOA,
                                                 string pstrDS_EMAIL,
                                                 Int64 plngNU_CPF,
                                                 Int64 plngNU_CNPJ);
        [OperationContract]
        string GetImprimirContratosCentralJSON(int pintCD_CONTABIL_PESSOA,
                                                         string pstrNUSERIECTR,
                                                         int pintNUCTR,
                                                         byte pbyteNUDVCTR);

        [OperationContract]
        string GetCargaFormaPagamentoListJSON();

        [OperationContract]
        string GetCargaTipoPagamentoListJSON();

        [OperationContract]
        string GetCargaTipoEntregaListJSON();

        [OperationContract]
        string GetCargaTipoAssinaturaListJSON();

        [OperationContract]
        string GetCampanhaPlanoListJSON(int pintIDCampanha, int pintIDCanal);

        [OperationContract]
        string GetCampanhaPrecoListJSON(int pintIDCampanha, int pintIDPlano);

        [OperationContract]
        string GetPlanoComercialJSON(int pintIDPlano);

        [OperationContract]
        string GetTipoAssinaturaEntregaJSON(int pintTipoEntrega, int pintTipoAssinatura);

        [OperationContract]
        string GetSincronizacaoCampanhaPlanoPrecoFULL(int pintIDCamp = 0);

        [OperationContract]
        string GetSincronizacaoCampanhaPlanoTerceiro(int pintIDCamp, int pintIDPlano);

        [OperationContract]
        string GetViewLogrEntityIDJSON(Int32 pintIDLogr);

        [OperationContract]
        string GetCampanhaEntJSON(int pintIDCamp);

        [OperationContract]
        string GetTipoPessoaJSON();

        [OperationContract]
        string GetSexoPessoaJSON();

        [OperationContract]
        string GetTipoPessoaJuridicaJSON();

        [OperationContract]
        string GetBancoBoletoJSON(int pintIDProduto);

        [OperationContract]
        string GetProdutoAgregadoJSON();

        [OperationContract]
        string GetProdutoAgregadoIDJSON(int pintIDProduto, int pintIDItem);

        [OperationContract]
        string GetItemProdutoIDJSON(int pintIDProduto, int pintIDItem);

        [OperationContract]
        string GetItemProdutoJSON(int pintIDProduto);

        [OperationContract]
        string GetPrecoProdutoAgregadoJSON(int pintIDProduto, int pintItem);

        [OperationContract]
        string GetFormaPagamentoJSON(int pintIDFormaPag);

        [OperationContract]
        string GetTipoPagamentoJSON(int pintIDTipoPagto);

        [OperationContract]
        string GetSincronizacaoProdutoAgregadoFULL();

        [OperationContract]
        string GetSincronizacaoProdutoAgregadoTerceiro(int pintIDProduto, int pintIDItem);

        [OperationContract]
        string[] MAXIPAGO_VendaSimplesCartao(string pstrUrlPost,
                                            string pstrContentType,
                                            string pstrMethod,
                                            string pstrMerchantID,
                                            string pstrMerchantKey,
                                            string pstrVersion,
                                            string pstrProcessorID,
                                            string pstrfraudCheck,
                                            string pstrReferenceNum,
                                            string pstrCartao,
                                            string pstrValidade,
                                            string pstrCVV,
                                            string pstrValor,
                                            string pstrParcela,
                                            string pstrTipoPacelamento);

        [OperationContract]
        string[] MAXIPAGO_GetVendaPaymentId(string pstrUrlPost,
                                                           string pstrContentType,
                                                           string pstrMethod,
                                                           string pstrMerchantID,
                                                           string pstrMerchantKey,
                                                           string pstrRequestId,
                                                           string pstrPaymentId);

        [OperationContract]
        string GravarAssinaturaIntegracao(string pstrDadosPessoa,
                                          string pstrDadosEndereco,
                                          string pstrDadosPagamento,
                                          string pstrDadosGateway);

        [OperationContract]
        string GravarPedidoIntegracao(string pstrDadosPS,
                                    string pstrDadosTEL,
                                    string pstrDadosENDER,
                                    string pstrDadosPED,
                                    string pstrDadosITEM,
                                    string pstrDadosDEB,
                                    string pstrDadosGateway,
                                    Int32 pintEditora,
                                    Int32 pintUsuario);

        [OperationContract]
        string GetLerDadosDoAssinanteCentralJSON(int pintCD_CONTABIL_PESSOA);

        [OperationContract]
        string GetLerCargoCentralJSON();

        [OperationContract]
        string GetLerEstadoCivilCentralJSON();

        [OperationContract]
        string GetLerGrauInstrucaoCentralJSON();

        [OperationContract]
        string GetLerNacionalidadeCentralJSON();

        [OperationContract]
        string GetLerNaturalidadeCentralJSON();

        [OperationContract]
        string GetLerProfissaoCentralJSON();

        [OperationContract]
        string GetLerRamoAtividadeCentralJSON();

        [OperationContract]
        string GetValidarLoginCentral(string pstruser, string pstrpwd);

        [OperationContract]
        string SetAlterarDadosDoAssinanteCentralJSON(byte? ST_TP_PESSOA, string NM_PESSOA,
                                                                    int? CD_CONTABIL_PESSOA, string NM_RESPONSAVEL,
                                                                    string NU_IDENTIDADE, string NM_ORGAO_EMISSOR,
                                                                    DateTime? DT_EMISSAO, byte? ST_ESTADO_CIVIL,
                                                                    DateTime? DT_NASC_FUND, string NM_FANTASIA,
                                                                    string NU_INSCR_MUN, string NU_INSCR_EST,
                                                                    short? CD_RAMO, string DS_NOME_ABREV,
                                                                    int? CD_GRUPO_AFINIDADE, byte? CD_TP_TRATAMENTO,
                                                                    int? CD_CARGO, int? CD_GRAU_INSTRUCAO,
                                                                    int? CD_NACIONALIDADE, int? CD_NATURALIDADE,
                                                                    int? CD_PROFISSAO, string NM_SOBRENOME, long? NU_CPF_RESP,
                                                                    Boolean pbolAbrirTransacao,                                                                         
                                                                    Boolean pbolFecharConexao);

        [OperationContract]
        string GetLerTelefoneAssinanteCentralJSON(int CD_CONTABIL_PESSOA);

        [OperationContract]
        string SetIncluirTelefoneAssinanteCentralService(int? CD_CONTABIL_PESSOA,
                                                               byte NU_SEQ,
                                                               byte? ST_TP_TELEFONE,
                                                               short? NU_DDD,
                                                               string NU_TEL,
                                                               string NU_RAMAL,
                                                               string DS_OBS,
                                                               int? NU_DDI,
                                                               Boolean pbolAbrirTransacao,
                                                               Boolean pbolFecharConexao);
       [OperationContract]
       string SetAlterarTelefoneAssinanteCentral(int? CD_CONTABIL_PESSOA,
                                                byte NU_SEQ,
                                                byte? ST_TP_TELEFONE,
                                                short? NU_DDD,
                                                string NU_TEL,
                                                string NU_RAMAL,
                                                string DS_OBS,
                                                int? NU_DDI,
                                                Boolean pbolAbrirTransacao,
                                                Boolean pbolFecharConexao);

        [OperationContract]
        string SetDeletarTelefoneAssinanteCentral(int? CD_CONTABIL_PESSOA,
                                                  byte NU_SEQ,
                                                  Boolean pbolAbrirTransacao,
                                                  Boolean pbolFecharConexao);

        [OperationContract]
        string GetLerEmailDoAssinanteCentralJSON(int CD_CONTABIL_PESSOA);

        [OperationContract]
        string SetIncluirEmailDoAssinanteCentralService(int? CD_CONTABIL_PESSOA,
                                                    byte NU_SEQ,
                                                    string DS_EMAIL,
                                                    byte? ST_SITUACAO,
                                                    byte? ST_EMAIL_PRINCIPAL,
                                                    int CD_TIPO_EMAIL,
                                                    Boolean pbolAbrirTransacao,
                                                    Boolean pbolFecharConexao);

        [OperationContract]
        string SetAlterarEmailDoAssinanteCentralService(int? CD_CONTABIL_PESSOA,
                                                byte NU_SEQ,
                                                string DS_EMAIL,
                                                byte? ST_SITUACAO,
                                                byte? ST_EMAIL_PRINCIPAL,
                                                int CD_TIPO_EMAIL,
                                                Boolean pbolAbrirTransacao,
                                                Boolean pbolFecharConexao);

        [OperationContract]
        string SetDeletarEmailDoAssinanteCentralService(int? CD_CONTABIL_PESSOA,
                                                byte NU_SEQ,
                                                Boolean pbolAbrirTransacao,
                                                Boolean pbolFecharConexao);
        [OperationContract]
        string GetLerTipoEmailCentralJSON();

        [OperationContract(Name="GetLerGrupoAfinidadeCentralJSON")]
        string GetLerGrupoAfinidadeCentralJSON();

        [OperationContract(Name = "GetLerGrupoAfinidadeTipoCentralJSON")]
        string GetLerGrupoAfinidadeCentralJSON(int pintTipo);

        [OperationContract]
        string GetReadTextFile(string pstrPathFile);

        [OperationContract]
        string GetLerRedeSocialCentralJSON(int CD_CONTABIL_PESSOA);

        [OperationContract]
        string SetIncluirRedeSocialCentralService(int? CD_CONTABIL_PESSOA,
                                                        byte NU_SEQ,
                                                        string DS_REDE_SOCIAL,
                                                        string DS_EMAIL,
                                                        int CD_TIPO_REDE_SOCIAL,
                                                        string DS_USUARIO,
                                                        string ID_TOKEN,
                                                        Boolean pbolAbrirTransacao,
                                                        Boolean pbolFecharConexao);
        [OperationContract]
        string SetAlterarRedeSocialCentralService(int? CD_CONTABIL_PESSOA,
                                                  byte NU_SEQ,
                                                  string DS_REDE_SOCIAL,
                                                  string DS_EMAIL,
                                                  int CD_TIPO_REDE_SOCIAL,
                                                  string DS_USUARIO,
                                                  string ID_TOKEN,
                                                  Boolean pbolAbrirTransacao,
                                                  Boolean pbolFecharConexao);

        [OperationContract]
        string SetDeletarRedeSocialCentralService(int? CD_CONTABIL_PESSOA,
                                                  byte NU_SEQ,
                                                  Boolean pbolAbrirTransacao,
                                                  Boolean pbolFecharConexao);

        [OperationContract]
        string SetSincronizacaoCampanhaPlanoPrecoCorreio(string pstrURLPost, string pstrVetorCampPlano);

        [OperationContract]
        string GetCargaWebCorreioJSON(int pintTipo, int pintProduto, int pintCampanha, int pintCortesia);

        [OperationContract]
        string GetProdutoJSON(int pintIDProduto);

        [OperationContract]
        string SetSincronizacaoCampanhaPlanoPrecoJC(string pstrURLPost, string pstrToken, string pstrVetorCampPlano);

        [OperationContract]
        string GetFormaExpedicaoProdutoAgregadoJSON();

        [OperationContract]
        string GetAcessoProcedureJSON(string pstrDS_EMAIL);

        [OperationContract]
        string GetAcessoSenhaProcedureJSON(string pstrDS_EMAIL, string pstrDS_SENHA);

        [OperationContract]
        string GetEsqueciSenhaProcedureJSON(string pstrDS_EMAIL, string pstrNU_CPF, string pstrNU_CNPJ);

        [OperationContract]
        string SetAlterarSenhaProcedureJSON(string pstrDS_EMAIL, string pstrDS_SENHA, string pstrNU_CPF, string pstrNU_CNPJ);

        [OperationContract]
        string GetPesquisaParametroEmailJSON();

        [OperationContract]
        string GetPesquisaParametroGlobalWebJSON();

        [OperationContract]
        string GetAcessoIntegradoJSON(int pintCD_CONTABIL_PESSOA, string pstrNU_SERIE_CTR, int pintNU_CTR, int pintNU_DV_CTR);

        [OperationContract]
        string GetAcessoIntegradoSSOJCJSON(int pintCD_CONTABIL_PESSOA, string pstrNU_DOC, string pstrDS_EMAIL);

        [OperationContract]
        string GetReadCerticado(string pstrFileName);

        [OperationContract]
        string SetGravaLogInterfaceWEB(int pintIDLog,
                                    string pstrDadosPessoa,
                                    string pstrDadosEntrega,
                                    string pstrDadosPagamento,
                                    string pstrDadosPlano,
                                    string pstrChamador,
                                    string pstrURL,
                                    string pstrMetodo,
                                    string pstrSerieCTR,
                                    int pintNuCTR,
                                    int pintDvCTR,
                                    bool pbolGetway,
                                    string pstrObs,
                                    string pstrErro);
        [OperationContract]
        string GetAbandonoMKTJSON();

        [OperationContract]
        string SetAbandonoMKT(int pintIDLog);

        [OperationContract]
        string GetStatusConexaoBD();

        [OperationContract]
        string GetLerAfinidadeAssinanteCentralJSON(int pintCD_CONTABIL_PESSOA);

        [OperationContract]
        string GetProximoCTRJSON();

        [OperationContract]
        string SetIntegracaoLahar(string pstrToken,
                                         string pstrUrlPost,
                                         string pstrMethod,
                                         string pstrContentType,
                                         string pstrNomeFormulario,
                                         string pstrDados,
                                         string pstrEmpresa);
        [OperationContract]
        string GetLoginValidoLondrina(string pstrToken);
    }

}
