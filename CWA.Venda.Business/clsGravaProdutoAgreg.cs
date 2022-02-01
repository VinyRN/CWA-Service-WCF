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
    public class GravaProdutoAgregBusiness
    {
        private System.Int32 _erro;
        private string _msgErro;
        private string _lstrCTR;
        private string _lstrInfoPlano;
        private Int32 _IDLogRastreio;
        private Int32 _IDTipoPagamento;

        public System.Int32 Erro
        {
            get { return _erro; }
        }

        public string MsgErro
        {
            get { return _msgErro; }
        }

        public string GravarPedido(PessoaProdutoAgregEntity ObjPS,
                                   List<TelefoneProdutoAgregEntity> ObjTEL,
                                   List<EnderecoProdutoAgregEntity> ObjENDER,
                                   PedidoProdutoAgregEntity ObjPED,
                                   List<ItemPedidoProdutoAgregEntity> ObjITEM)
        {
            try
            {

                return "";
            }
            catch (Exception ex)
            {

                return "";
            }


        }


        public string GravarPedidoIntegracao(string pstrDadosPS,
                                             string pstrDadosTEL,
                                             string pstrDadosENDER,
                                             string pstrDadosPED,
                                             string pstrDadosITEM,
                                             string pstrDadosDEB,
                                             string pstrDadosGateway,
                                             Int32 pintEditora,
                                             Int32 pintUsuario)
        {
            try
            {
                //################################################################################################
                //Valida dos dados JSON passado e transforma em Objeto para Gravar o pedido de produto agregado  #
                //################################################################################################


                PessoaProdutoAgregEntity ObjEntPS = new PessoaProdutoAgregEntity();
                ObjEntPS = (PessoaProdutoAgregEntity)JsonConvert.DeserializeObject(pstrDadosPS, ObjEntPS.GetType());

                TelefoneProdutoAgregEntity ObjEntTEL = new TelefoneProdutoAgregEntity();
                ObjEntTEL = (TelefoneProdutoAgregEntity)JsonConvert.DeserializeObject(pstrDadosTEL, ObjEntTEL.GetType());

                EnderecoProdutoAgregEntity ObjEntENDER = new EnderecoProdutoAgregEntity();
                ObjEntENDER = (EnderecoProdutoAgregEntity)JsonConvert.DeserializeObject(pstrDadosENDER, ObjEntENDER.GetType());

                PedidoProdutoAgregEntity ObjEntPED = new PedidoProdutoAgregEntity();
                ObjEntPED = (PedidoProdutoAgregEntity)JsonConvert.DeserializeObject(pstrDadosPED, ObjEntPED.GetType());

                ItemPedidoProdutoAgregEntity ObjEntITEM = new ItemPedidoProdutoAgregEntity();
                ObjEntITEM = (ItemPedidoProdutoAgregEntity)JsonConvert.DeserializeObject(pstrDadosITEM, ObjEntITEM.GetType());

                DebitoProdutoAgregEntity ObjEntDEB = new DebitoProdutoAgregEntity();
                ObjEntDEB = (DebitoProdutoAgregEntity)JsonConvert.DeserializeObject(pstrDadosDEB, ObjEntDEB.GetType());

                if ((ObjEntPS == null) || (ObjEntTEL == null) || (ObjEntENDER == null) || (ObjEntPED == null) || (ObjEntITEM == null))
                {
                    _erro = -1;
                    _msgErro = "Erro ao converter JSON de Pessoa/Telefone/Endereço/Pedido/Itens Pedido";

                    return Utility.GetRetornoJSON("-1", "Erro" + _msgErro, "");
                }


                //Fazer funçao para trazer o campo certo ==> CD_FORMA_PAG
                GatewayPagamento ObjGateway = new GatewayPagamento();

                //#########################################################################################
                //Gravar dados nas tabelas TEMP 
                //#########################################################################################

                if (!ValidarDadosPessoa(ObjEntPS))
                {
                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }

                if (!ValidarEndereco(ObjEntENDER))
                {
                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }

                if (!ValidarTelefone(ObjEntTEL))
                {
                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }

                if (!ValidarDebito(ObjEntDEB, ObjEntPED, ObjEntPS))
                {
                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }
                else
                {
                    bool lbolUsaCartaoVenda = bool.Parse(ConfigurationManager.AppSettings["UsaCartaoVenda"]);

                    bool lbolGetway = false;

                    if (_IDTipoPagamento == 6) // cartao
                    {
                        if (!string.IsNullOrEmpty(pstrDadosGateway))
                        {
                            ObjGateway = (GatewayPagamento)JsonConvert.DeserializeObject(pstrDadosGateway, ObjGateway.GetType());
                        }
                        else
                        {
                            ObjGateway = null;
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

                                Utility.SetLogXML("GravarPedidoIntegracao", "Error - ", _msgErro, false);

                                return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                            }
                        }

                        //====================================================================
                        //TRATA DADOS DO CARTAO DE CRÉDITO QUANDO O PARAMENTRO GLOBAL PERMITIR
                        //====================================================================

                        if (lbolUsaCartaoVenda == false)
                        {
                            ObjEntDEB.NU_CARTAO = null;
                            ObjEntDEB.NM_TITULAR = ObjEntPS.NM_PESSOA;
                            ObjEntDEB.NU_DIA_DEBITO = 1;
                            ObjEntDEB.NU_CVV_CARTAO = null;
                            ObjEntDEB.DT_VALID_CARTAO = null;
                        }

                        //====================================================================
                    }
                }

                if (!ValidarPedido(ObjEntPED))
                {
                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }

                if (!ValidarItemPedido(ObjEntITEM))
                {
                    return Utility.GetRetornoJSON("-1", "Erro - " + _msgErro, "");
                }


                DataContext.AbrirConexao();
                DataContext.BeginTransaction();

                GravaProdutoAgregData ObjData = new GravaProdutoAgregData();

                ObjData.SetCriarTabelasTEMP(true, false);

                if (ObjData.Erro != 0)
                {
                    return Utility.GetRetornoJSON("-1", "Erro Create Tables - " + ObjData.MsgErro, "");
                }

                ObjData.SetPessoaTEMP(ObjEntPS, true, false);

                if (ObjData.Erro != 0)
                {
                    return Utility.GetRetornoJSON("-1", "Erro Cadastro Pessoa - " + ObjData.MsgErro, "");
                }

                ObjData.SetEnderecoTEMP(ObjEntENDER, true, false);

                if (ObjData.Erro != 0)
                {
                    return Utility.GetRetornoJSON("-1", "Erro Cadastro Endereço - " + ObjData.MsgErro, "");
                }

                ObjData.SetTelefoneTEMP(ObjEntTEL, true, false);

                if (ObjData.Erro != 0)
                {
                    return Utility.GetRetornoJSON("-1", "Erro Cadastro Telefone - " + ObjData.MsgErro, "");
                }

                ObjData.SetDebitoTEMP(ObjEntDEB, true, false);

                if (ObjData.Erro != 0)
                {
                    return Utility.GetRetornoJSON("-1", "Erro Cadastro Debito - " + ObjData.MsgErro, "");
                }

                ObjData.SetPedidoTEMP(ObjEntPED, true, false);

                if (ObjData.Erro != 0)
                {
                    return Utility.GetRetornoJSON("-1", "Erro Cadstro de Pedido - " + ObjData.MsgErro, "");
                }

                ObjData.SetItemPedidoTEMP(ObjEntITEM, true, false);

                if (ObjData.Erro != 0)
                {
                    return Utility.GetRetornoJSON("-1", "Erro Cadstro Item do Pedido - " + ObjData.MsgErro, "");
                }

                //#########################################################################################

                //Finaliza pedido e recupera a mensagem final
                string[] lstrVetorRet = null;
                lstrVetorRet = SetFecharPedido(pintEditora, pintUsuario);

                if (lstrVetorRet != null)
                {
                    if (lstrVetorRet[0].Trim() != "0")
                    {
                        DataContext.RollbackTransaction();
                        DataContext.FecharConexao();

                        return Utility.GetRetornoJSON("-1", "Erro na inclusão do pedido - " + lstrVetorRet[1], "");
                    }
                    else
                    {
                        DataContext.CommitTransaction();

                        string lstrRet = ObjData.GetRetorGravacao(true);

                        if (ObjData.Erro != 0)
                        {
                            return Utility.GetRetornoJSON("-1", "Erro - GetRetorGravacao", ObjData.MsgErro);
                        }

                        string[] lstrVetRetTemp = lstrRet.Split(';');

                        if (lstrVetRetTemp != null)
                        {
                            if (lstrVetRetTemp[0].Trim() == "0")
                            {
                                return Utility.GetRetornoJSON("0", "Sucesso", lstrVetRetTemp[2] + "-" + lstrVetRetTemp[3]);
                            }
                            else
                            {
                                return Utility.GetRetornoJSON("-1", "Erro", lstrVetRetTemp[1]);
                            }
                        }
                        else
                        {
                            return Utility.GetRetornoJSON("-1", "Erro ao pegar retorno do processo - NULL", "");
                        }

                    }
                }
                else
                {
                    DataContext.RollbackTransaction();
                    DataContext.FecharConexao();

                    return Utility.GetRetornoJSON("-1", "Erro SetFecharPedido - " + _msgErro, "");

                }


                //#########################################################################################


            }
            catch (Exception ex)
            {
                DataContext.RollbackTransaction();
                DataContext.FecharConexao();

                _erro = -99;
                _msgErro = ex.Message;

                return Utility.GetRetornoJSON("-99", "Erro - " + _msgErro, "");
                
            }


        }

        private bool ValidarDadosPessoa(PessoaProdutoAgregEntity ObjEnt)
        {
            try
            {

                DataContext.AbrirConexao();
                GravaProdutoAgregData ObjData = new GravaProdutoAgregData();

                if (ObjEnt.CD_CONTABIL_PESSOA != 0)
                {
                    if (!ObjData.GetCriticaTabela("CADASTRO_PESSOA", "CD_CONTABIL_PESSOA", ObjEnt.CD_CONTABIL_PESSOA.ToString(), false,true))
                    {
                        _erro = -1;
                        _msgErro = "Valor não cadastrado para o campo CD_CONTABIL_PESSOA";
                        return false;
                    }
                }

                if ( (ObjEnt.ST_TP_PESSOA == 0) || (ObjEnt.ST_TP_PESSOA > 5))
                {
                    _erro = -1;
                    _msgErro = "Valor inválido para o campo ST_TP_PESSOA";
                    return false;
                }

                if (string.IsNullOrEmpty(ObjEnt.NM_PESSOA))
                {
                    _erro = -1;
                    _msgErro = "NM_PESSOA é obrigatório";
                    return false;
                }

                if ((ObjEnt.NM_PESSOA.Trim().Length > 50 ))
                {
                    _erro = -1;
                    _msgErro = "NM_PESSOA não pode ter mais que 50 caracteres";
                    return false;
                }

                if (!string.IsNullOrEmpty(ObjEnt.NM_SOBRENOME))
                {

                    if (ObjEnt.NM_SOBRENOME.Trim() != "")
                    {
                        if ((ObjEnt.NM_SOBRENOME.Trim().Length > 50))
                        {
                            _erro = -1;
                            _msgErro = "NM_SOBRENOME não pode ter mais que 50 caracteres";
                            return false;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.NM_RESPONSAVEL))
                {
                    if ((ObjEnt.NM_RESPONSAVEL.Trim().Length > 50))
                    {
                        _erro = -1;
                        _msgErro = "NM_RESPONSAVEL não pode ter mais que 50 caracteres";
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(ObjEnt.DS_EMAIL))
                {
                    if (ObjEnt.DS_EMAIL.Trim() != "")
                    {
                        if ((ObjEnt.DS_EMAIL.Trim().Length > 50))
                        {
                            _erro = -1;
                            _msgErro = "DS_EMAIL não pode ter mais que 50 caracteres";
                            return false;
                        }

                        if (!Utility.GetEmailValido(ObjEnt.DS_EMAIL.Trim()))
                        {
                            _erro = -1;
                            _msgErro = "DS_EMAIL não contém um e-mail válido";
                            return false;
                        }
                    }
                }

                if ((ObjEnt.ST_TP_PESSOA != 3) || (ObjEnt.ST_TP_PESSOA != 5))
                {
                    if (ObjEnt.NU_CPF.Trim() == "")
                    {
                        _erro = -1;
                        _msgErro = "NU_CPF é obrigatório para pessoa física";
                        return false;
                    }

                    if (!Utility.IsCpf(Int64.Parse(ObjEnt.NU_CPF).ToString("00000000000")))
                    {
                        _erro = -1;
                        _msgErro = "Valor inválido para o campo NU_CPF";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.DT_NASC_FUND))
                {
                    if (!Utility.GetValidarData(ObjEnt.DT_NASC_FUND))
                    {
                        _erro = -1;
                        _msgErro = "Valor inválido para o campo DT_NASC_FUND";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.NM_FANTASIA))
                {
                    if ((ObjEnt.NM_FANTASIA.Trim().Length > 50))
                    {
                        _erro = -1;
                        _msgErro = "NM_FANTASIA não pode ter mais que 50 caracteres";
                        return false;
                    }
                }

                if ((ObjEnt.ST_TP_PESSOA == 3) || (ObjEnt.ST_TP_PESSOA == 5))
                {
                    if (string.IsNullOrEmpty(ObjEnt.NU_CNPJ))
                    {
                        _erro = -1;
                        _msgErro = "NU_CNPJ é obrigatório para pessoa jurídica";
                        return false;
                    }

                    if (!Utility.IsCnpj(Int64.Parse(ObjEnt.NU_CNPJ).ToString("00000000000000")))
                    {
                        _erro = -1;
                        _msgErro = "Valor inválido para o campo NU_CNPJ";
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return false;
            }
        }
        private bool ValidarEndereco(EnderecoProdutoAgregEntity ObjEnt)
        {
            try
            {

                DataContext.AbrirConexao();
                GravaProdutoAgregData ObjData = new GravaProdutoAgregData();

                if (ObjEnt.CD_LOGRADOURO != 0)
                {
                    if (!ObjData.GetCriticaTabela("LOGRADOURO", "CD_LOGRADOURO", ObjEnt.CD_LOGRADOURO.ToString(), false, true))
                    {
                        _erro = -1;
                        _msgErro = "Valor não cadastrado para o campo CD_LOGRADOURO";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.COMPL_RESIDENCIA))
                {
                    if ((ObjEnt.COMPL_RESIDENCIA.Trim().Length > 2))
                    {
                        _erro = -1;
                        _msgErro = "COMPL_RESIDENCIA não pode ter mais que 2 caracteres";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.NU_BLOCO))
                {
                    if ((ObjEnt.NU_BLOCO.Trim().Length > 5))
                    {
                        _erro = -1;
                        _msgErro = "NU_BLOCO não pode ter mais que 5 caracteres";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.NU_APARTAMENTO))
                {
                    if ((ObjEnt.NU_APARTAMENTO.Trim().Length > 4))
                    {
                        _erro = -1;
                        _msgErro = "NU_APARTAMENTO não pode ter mais que 4 caracteres";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.DS_COMPLEMENTO))
                {
                    if ((ObjEnt.DS_COMPLEMENTO.Trim().Length > 50))
                    {
                        _erro = -1;
                        _msgErro = "DS_COMPLEMENTO não pode ter mais que 50 caracteres";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.DS_PONTO_REF))
                {
                    if ((ObjEnt.DS_PONTO_REF.Trim().Length > 50))
                    {
                        _erro = -1;
                        _msgErro = "DS_PONTO_REF não pode ter mais que 50 caracteres";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.NU_CEP))
                {
                    if ((ObjEnt.NU_CEP.Trim().Length > 8))
                    {
                        _erro = -1;
                        _msgErro = "NU_CEP não pode ter mais que 8 caracteres";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.DS_ENDERECO_EXT))
                {
                    if ((ObjEnt.DS_ENDERECO_EXT.Trim().Length > 100))
                    {
                        _erro = -1;
                        _msgErro = "DS_ENDERECO_EXT não pode ter mais que 100 caracteres";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.DS_BAIRRO_EXT))
                {
                    if ((ObjEnt.DS_BAIRRO_EXT.Trim().Length > 50))
                    {
                        _erro = -1;
                        _msgErro = "DS_BAIRRO_EXT não pode ter mais que 50 caracteres";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.DS_MUNICIPIO_EXT))
                {
                    if ((ObjEnt.DS_MUNICIPIO_EXT.Trim().Length > 50))
                    {
                        _erro = -1;
                        _msgErro = "DS_MUNICIPIO_EXT não pode ter mais que 50 caracteres";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.DS_UF_EXT))
                {
                    if ((ObjEnt.DS_UF_EXT.Trim().Length > 2))
                    {
                        _erro = -1;
                        _msgErro = "DS_UF_EXT não pode ter mais que 2 caracteres";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.NU_CEP_EXT))
                {
                    if ((ObjEnt.NU_CEP_EXT.Trim().Length > 10))
                    {
                        _erro = -1;
                        _msgErro = "NU_CEP_EXT não pode ter mais que 10 caracteres";
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return false;
            }
        }
        private bool ValidarTelefone(TelefoneProdutoAgregEntity ObjEnt)
        {
            try
            {
                if ((ObjEnt.ST_TP_TELEFONE == 0) || (ObjEnt.ST_TP_TELEFONE > 5))
                {
                    _erro = -1;
                    _msgErro = "Valor inválido para o campo ST_TP_TELEFONE";
                    return false;
                }

                if (string.IsNullOrEmpty(ObjEnt.NU_TEL))
                {
                    _erro = -1;
                    _msgErro = "O campo NU_TEL é obrigatorio";
                    return false;
                }

                if ((ObjEnt.NU_TEL.ToString().Length < 7))
                {
                    _erro = -1;
                    _msgErro = "O campo NU_TEL não pode ter menos que 7 caracteres";
                    return false;
                }

                if ((ObjEnt.NU_TEL.ToString().Length > 9))
                {
                    _erro = -1;
                    _msgErro = "O campo NU_TEL não pode ter mais que 8 caracteres";
                    return false;
                }

                if (!string.IsNullOrEmpty(ObjEnt.NU_RAMAL))
                {
                    if ((ObjEnt.NU_RAMAL.ToString().Length > 10))
                    {
                        _erro = -1;
                        _msgErro = "O campo NU_RAMAL não pode ter mais que 10 caracteres";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(ObjEnt.DS_OBS))
                {
                    if ((ObjEnt.DS_OBS.ToString().Length > 50))
                    {
                        _erro = -1;
                        _msgErro = "O campo DS_OBS não pode ter mais que 50 caracteres";
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return false;
            }
        }
        private bool ValidarDebito(DebitoProdutoAgregEntity ObjEnt, PedidoProdutoAgregEntity ObjEntPed, PessoaProdutoAgregEntity ObjEntPS)
        {
            try
            {
                DataContext.AbrirConexao();
                GravaProdutoAgregData ObjData = new GravaProdutoAgregData();

                if (ObjEnt.CD_FONTE_DEBITO != 0)
                {

                    if (!ObjData.GetCriticaTabela("FORMA_PAGAMENTO", "CD_FORMA_PAG", ObjEntPed.CD_FORMA_PAG.ToString(), false, true))
                    {
                        _erro = -1;
                        _msgErro = "Valor não cadastrado para o campo CD_FORMA_PAG";
                        return false;
                    }
                }
                else
                {
                    _erro = -1;
                    _msgErro = "CD_FONTE_DEBITO é obrigatório";
                    return false;
                }

                if ((!string.IsNullOrEmpty(ObjEnt.DT_ENVIO_COBRANCA)))
                {
                    if (!Utility.GetValidarData(ObjEnt.DT_ENVIO_COBRANCA))
                    {
                        _erro = -1;
                        _msgErro = "Valor inválido para o campo DT_ENVIO_COBRANCA";
                        return false;
                    }
                }

                if ((!string.IsNullOrEmpty(ObjEnt.DT_RETORNO_COBRANCA)))
                {
                    if (!Utility.GetValidarData(ObjEnt.DT_RETORNO_COBRANCA))
                    {
                        _erro = -1;
                        _msgErro = "Valor inválido para o campo DT_RETORNO_COBRANCA";
                        return false;
                    }
                }

                //========================================================================================================
                //Pega Tipo de Pagamento 
                //========================================================================================================

                DataContext.AbrirConexao();
                FormaPagamentoData ObjPgtoData = new FormaPagamentoData();

                FormaPagamentoEntity ObjFormaPagtoEnt = ObjPgtoData.GetFormaPagamentoEntity(ObjEntPed.CD_FORMA_PAG);

                if (ObjFormaPagtoEnt == null)
                {
                    _erro = -1;
                    _msgErro = "Tipo de pagamento não encontrado para a CD_FORMA_PAG informado";
                    return false;

                }

                _IDTipoPagamento = ObjFormaPagtoEnt.CD_TP_PAGAMENTO;

                //========================================================================================================

                if ( (_IDTipoPagamento != 6) && (!string.IsNullOrEmpty(ObjEnt.DT_ENVIO_COBRANCA)) && (!string.IsNullOrEmpty(ObjEnt.DT_RETORNO_COBRANCA)))
                {
                    _erro = -1;
                    _msgErro = "Os campos DT_ENVIO_COBRANCA e DT_RETORNO_COBRANCA só podem ser informados para forma de pagamento cartão de crédito";
                    return false;
                }

                if (_IDTipoPagamento == 6)
                {
                    DataContext.AbrirConexao();
                    if (!ObjData.GetCriticaTabela("ADM_CARTAO", "CD_CONTABIL_ADM", ObjEnt.CD_FONTE_DEBITO.ToString(), false, false))
                    {
                        _erro = -1;
                        _msgErro = "Valor não cadastrado para o campo CD_FONTE_DEBITO como Adm de Cartão";
                        return false;
                    }
                }

                if ((_IDTipoPagamento == 1) || (_IDTipoPagamento == 7))
                {
                    DataContext.AbrirConexao();
                    if (!ObjData.GetCriticaTabela("BANCO", "CD_CONTABIL_BANCO", ObjEnt.CD_FONTE_DEBITO.ToString(), false, false))
                    {
                        _erro = -1;
                        _msgErro = "Valor não cadastrado para o campo CD_FONTE_DEBITO como Banco";
                        return false;
                    }
                }


                //if ((!string.IsNullOrEmpty(ObjEnt.DT_ENVIO_COBRANCA)) || (!string.IsNullOrEmpty(ObjEnt.DT_RETORNO_COBRANCA)))
                //{
                    if ((_IDTipoPagamento == 1) || (_IDTipoPagamento == 6))
                    {
                        bool lbolUsaCartaoVenda = bool.Parse(ConfigurationManager.AppSettings["UsaCartaoVenda"]);

                        if (lbolUsaCartaoVenda == true)
                        {
                            if (string.IsNullOrEmpty(ObjEnt.NU_CPF_CNPJ))
                            {
                                _erro = -1;
                                _msgErro = "O campo NU_CPF_CNPJ_TITULAR não pode ser vazio";
                                return false;
                            }

                            if ((ObjEnt.NU_CPF_CNPJ.Trim().Length != 11) && (ObjEnt.NU_CPF_CNPJ.Trim().Length != 14))
                            {
                                _erro = -1;
                                _msgErro = "O campo NU_CPF_CNPJ_TITULAR deve ter 11 ou 14 caracteres";
                                return false;
                            }
                        }
                        else
                        {
                            //====================================================================
                            //TRATA DADOS DO CARTAO DE CRÉDITO QUANDO O PARAMENTRO GLOBAL PERMITIR
                            //====================================================================

                            if (lbolUsaCartaoVenda == false)
                            {
                                ObjEnt.NU_CARTAO = null;
                                ObjEnt.NM_TITULAR = ObjEntPS.NM_PESSOA;
                                ObjEnt.NU_DIA_DEBITO = 1;
                                ObjEnt.NU_CVV_CARTAO = null;
                                ObjEnt.DT_VALID_CARTAO = null;
                            }

                            //====================================================================
                        }

                        if (string.IsNullOrEmpty(ObjEnt.NM_TITULAR))
                        {
                            _erro = -1;
                            _msgErro = "O campo NM_TITULAR é obrigatório";
                            return false;
                        }

                        if (ObjEnt.NM_TITULAR.Trim().Length > 50)
                        {
                            _erro = -1;
                            _msgErro = "O campo NM_TITULAR não pode ter mais que 50 caracteres";
                            return false;
                        }

                        if ( (ObjEnt.NU_DIA_DEBITO  == 0) || (ObjEnt.NU_DIA_DEBITO > 31))
                        {
                            _erro = -1;
                            _msgErro = "Valor inválido para o campo NU_DIA_DEBITO";
                            return false;
                        }

                        if (_IDTipoPagamento == 6)
                        {
                            if (lbolUsaCartaoVenda == true)
                            {
                                if (string.IsNullOrEmpty(ObjEnt.NU_CARTAO))
                                {
                                    _erro = -1;
                                    _msgErro = "Valor inválido para o campo NU_CARTAO";
                                    return false;
                                }
                                if (string.IsNullOrEmpty(ObjEnt.NU_CVV_CARTAO))
                                {
                                    _erro = -1;
                                    _msgErro = "Valor inválido para o campo NU_CVV_CARTAO";
                                    return false;
                                }
                                if (!Utility.GetValidarData(ObjEnt.DT_VALID_CARTAO))
                                {
                                    _erro = -1;
                                    _msgErro = "Valor inválido para o campo DT_VALID_CARTAO";
                                    return false;
                                }
                                if ((ObjEnt.ST_TP_PARC_CARTAO < 1) || (ObjEnt.ST_TP_PARC_CARTAO > 2))
                                {
                                    _erro = -1;
                                    _msgErro = "Valor inválido para o campo ST_TP_PARC_CARTAO";
                                    return false;
                                }
                            }

                            if (ObjEnt.COD_AUTORIZACAO.Trim().Length > 10)
                            {
                                _erro = -1;
                                _msgErro = "O campo COD_AUTORIZACAO não pode ter mais que 10 caracteres";
                                return false;
                            }
                            if (ObjEnt.COD_NSU.Trim().Length > 10)
                            {
                                _erro = -1;
                                _msgErro = "O campo COD_NSU não pode ter mais que 10 caracteres";
                                return false;
                            }

                        }
                        else
                        {
                            if (ObjEnt.NU_AGENCIA.Trim() == "")
                            {
                                _erro = -1;
                                _msgErro = "Valor inválido para o campo NU_AGENCIA";
                                return false;
                            }
                            if (ObjEnt.NU_DV_AGENCIA.Trim() == "")
                            {
                                _erro = -1;
                                _msgErro = "Valor inválido para o campo NU_DV_AGENCIA";
                                return false;
                            }
                            if (ObjEnt.NU_DV_AGENCIA.Trim().Length > 2)
                            {
                                _erro = -1;
                                _msgErro = "O campo NU_DV_AGENCIA pode ter mais que 2 caracteres";
                                return false;
                            }
                            if (ObjEnt.NU_CONTA.Trim() == "")
                            {
                                _erro = -1;
                                _msgErro = "Valor inválido para o campo NU_CONTA";
                                return false;
                            }
                            if (ObjEnt.NU_DV_CONTA.Trim() == "")
                            {
                                _erro = -1;
                                _msgErro = "Valor inválido para o campo NU_DV_CONTA";
                                return false;
                            }
                            if (ObjEnt.NU_DV_CONTA.Trim().Length > 2)
                            {
                                _erro = -1;
                                _msgErro = "O campo NU_DV_CONTA pode ter mais que 2 caracteres";
                                return false;
                            }
                        }

                    }
                //}

                return true;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return false;
            }
        }
        private bool ValidarPedido(PedidoProdutoAgregEntity ObjEnt)
        {
            try
            {
                DataContext.AbrirConexao();
                GravaProdutoAgregData ObjData = new GravaProdutoAgregData();

                if (!ObjData.GetCriticaTabela("REPRESENTANTE_VENDA", "CD_CONTABIL_REPR_VENDA", ObjEnt.CD_CONTABIL_REPR_VENDA.ToString(), false, true))
                {
                    _erro = -1;
                    _msgErro = "Valor não cadastrado para o campo CD_CONTABIL_REPR_VENDA";
                    return false;
                }

                DataContext.AbrirConexao();

                if (!ObjData.GetCriticaTabela("VENDEDOR_ASSINATURA", 
                                              "CD_CONTABIL_REPR_VENDA", ObjEnt.CD_CONTABIL_REPR_VENDA.ToString(),
                                              "CD_CONTABIL_VEND", ObjEnt.CD_CONTABIL_VEND.ToString(),
                                              false, true))
                {
                    _erro = -1;
                    _msgErro = "Valor não cadastrado para o campo CD_CONTABIL_REPR_VENDA/CD_CONTABIL_VEND";
                    return false;
                }

                DataContext.AbrirConexao();
                if (!ObjData.GetCriticaTabela("FORMA_EXPEDICAO", "CD_FORMA_EXPEDICAO", ObjEnt.CD_FORMA_EXPEDICAO.ToString(), false, true))
                {
                    _erro = -1;
                    _msgErro = "Valor não cadastrado para o campo CD_CONTABIL_REPR_VENDA";
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return false;
            }
        }
        private bool ValidarItemPedido(ItemPedidoProdutoAgregEntity ObjEnt)
        {
            try
            {
                DataContext.AbrirConexao();
                GravaProdutoAgregData ObjData = new GravaProdutoAgregData();

                if (!ObjData.GetCriticaTabela("PRODUTO", "CD_PRODUTO", ObjEnt.CD_PRODUTO.ToString(), false, true))
                {
                    _erro = -1;
                    _msgErro = "Valor não cadastrado para o campo CD_PRODUTO";
                    return false;
                }

                DataContext.AbrirConexao();
                if (!ObjData.GetCriticaTabela("ITEM_PRODUTO",
                                              "CD_PRODUTO", ObjEnt.CD_PRODUTO.ToString(),
                                              "CD_ITEM_PRODUTO", ObjEnt.CD_ITEM_PRODUTO.ToString(),
                                              false, true))
                {
                    _erro = -1;
                    _msgErro = "Valor não cadastrado para o campo CD_PRODUTO/CD_ITEM_PRODUTO";
                    return false;
                }

                if (ObjEnt.QTD_PRODUTO == 0)
                {
                    _erro = -1;
                    _msgErro = "A quantidade de produto deve ser maior que 0";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return false;
            }
        }

        private string[] SetFecharPedido(Int32 pintIDEditora, Int32 pintIDUsuario)
        {
            try
            {
                GravaProdutoAgregData ObjData = new GravaProdutoAgregData();

                ObjData.SetGravarPedido(pintIDEditora, pintIDUsuario, true);

                if (ObjData.Erro != 0)
                {
                    return new string[] { "-1", "Erro - SetGravarPedido", ObjData.MsgErro };
                }
                else
                {
                    return new string[] { "0", "OK", "" };
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return new string[] {"-99", "Erro - Geral", _msgErro};
                
            }
        }



    }


}
