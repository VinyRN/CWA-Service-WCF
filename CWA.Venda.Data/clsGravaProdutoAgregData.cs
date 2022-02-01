using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

using CWA.Venda.Entity;
using System.Globalization;

namespace CWA.Venda.Data
{
    public class GravaProdutoAgregData
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

        public void SetCriarTabelasTEMP(Boolean pbolAbrirTransacao,
                                        Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.Text;

                string lstrSQL = "";

                lstrSQL = "if object_id('tempdb.dbo.#T_TMP_CP') is not null " ;
                lstrSQL = lstrSQL + "begin " ;
                lstrSQL = lstrSQL + "   drop table dbo.#T_TMP_CP " ;
                lstrSQL = lstrSQL + "end ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }


                lstrSQL = "create table dbo.#T_TMP_CP " ;
                lstrSQL = lstrSQL + "( " ;
                lstrSQL = lstrSQL + " CD_CONTABIL_PESSOA [int]                                        NULL " ;
                lstrSQL = lstrSQL + ",ST_TP_PESSOA       [tinyint]                                    NULL " ;
                lstrSQL = lstrSQL + ",NM_PESSOA          [varchar](50)   COLLATE Latin1_General_CI_AI NULL " ;
                lstrSQL = lstrSQL + ",NM_SOBRENOME       [varchar](50)   COLLATE Latin1_General_CI_AI NULL " ;
                lstrSQL = lstrSQL + ",NU_CPF             [bigint]                                     NULL " ;
                lstrSQL = lstrSQL + ",DT_NASC_FUND       [smalldatetime]                              NULL " ;
                lstrSQL = lstrSQL + ",DS_EMAIL           [varchar](50)   COLLATE Latin1_General_CI_AI NULL " ;
                lstrSQL = lstrSQL + ",NU_CNPJ            [bigint]                                     NULL " ;
                lstrSQL = lstrSQL + ",NM_RESPONSAVEL     [varchar](50)   COLLATE Latin1_General_CI_AI NULL " ;
                lstrSQL = lstrSQL + ",NU_CPF_RESP        [bigint]                                     NULL " ;
                lstrSQL = lstrSQL + ",NM_FANTASIA        [varchar](50)   COLLATE Latin1_General_CI_AI NULL " ;
                lstrSQL = lstrSQL + ",NU_INSCR_MUN       [varchar](20)   COLLATE Latin1_General_CI_AI NULL " ;
                lstrSQL = lstrSQL + ",NU_INSCR_EST       [varchar](20)   COLLATE Latin1_General_CI_AI NULL " ;
                lstrSQL = lstrSQL + ",CD_RAMO            [smallint]                                   NULL " ;
                lstrSQL = lstrSQL + ") ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }

                lstrSQL = "if object_id('tempdb.dbo.#T_TMP_TA') is not null ";
                lstrSQL = lstrSQL + "begin ";
                lstrSQL = lstrSQL + "   drop table dbo.#T_TMP_TA ";
                lstrSQL = lstrSQL + "end ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }

                lstrSQL = "create table dbo.#T_TMP_TA ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + "ST_TP_TELEFONE [tinyint]                                   NOT NULL ";
                lstrSQL = lstrSQL + ",NU_DDD         [smallint]                                 NOT NULL ";
                lstrSQL = lstrSQL + ",NU_TEL         [varchar](20) COLLATE Latin1_General_CI_AI NOT NULL ";
                lstrSQL = lstrSQL + ",NU_RAMAL       [varchar](10) COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",DS_OBS         [varchar](50) COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ") ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }

                lstrSQL = "if object_id('tempdb.dbo.#T_TMP_DA') is not null ";
                lstrSQL = lstrSQL + "begin ";
                lstrSQL = lstrSQL + "   drop table dbo.#T_TMP_DA ";
                lstrSQL = lstrSQL + "end ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }

                lstrSQL = "create table dbo.#T_TMP_DA ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " CD_FONTE_DEBITO     [int]                                        NOT NULL ";
                lstrSQL = lstrSQL + ",DT_ENVIO_COBRANCA   [varchar](10)                                NULL ";
                lstrSQL = lstrSQL + ",DT_RETORNO_COBRANCA [varchar](10)                                NULL ";
                lstrSQL = lstrSQL + ",NM_TITULAR          [varchar](50)   COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",NU_CPF_CNPJ         [bigint]                                     NULL ";
                lstrSQL = lstrSQL + ",NU_DIA_DEBITO       [tinyint]                                    NULL ";
                lstrSQL = lstrSQL + ",NU_CARTAO           [varchar](20)   COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",NU_CVV_CARTAO       [varchar](4)    COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",DT_VALID_CARTAO     [smalldatetime]                              NULL ";
                lstrSQL = lstrSQL + ",ST_TP_PARC_CARTAO   [tinyint]                                    NULL ";
                lstrSQL = lstrSQL + ",COD_AUTORIZACAO     [varchar](10)   COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",COD_NSU             [varchar](10)   COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",NU_AGENCIA          [varchar](10)   COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",NU_DV_AGENCIA       [varchar](2)    COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",NU_CONTA            [varchar](15)   COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",NU_DV_CONTA         [varchar](2)    COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ") ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }

                lstrSQL = "if object_id('tempdb.dbo.#T_TMP_PPA') is not null ";
                lstrSQL = lstrSQL + "begin ";
                lstrSQL = lstrSQL + "   drop table dbo.#T_TMP_PPA ";
                lstrSQL = lstrSQL + "end ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }


                lstrSQL = "create table dbo.#T_TMP_PPA ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " CD_FORMA_PAG           [smallint]                                NOT NULL ";
                lstrSQL = lstrSQL + ",CD_CONTABIL_REPR_VENDA [int]                                     NOT NULL ";
                lstrSQL = lstrSQL + ",CD_CONTABIL_VEND       [int]                                     NOT NULL ";
                lstrSQL = lstrSQL + ",VA_FRETE               [smallmoney]                              NOT NULL ";
                lstrSQL = lstrSQL + ",CD_FORMA_EXPEDICAO     [smallint]                                NOT NULL ";
                lstrSQL = lstrSQL + ",ST_IND_RETIRADO        [varchar](1) COLLATE Latin1_General_CI_AI NOT NULL ";
                lstrSQL = lstrSQL + ") ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }

                lstrSQL = "if object_id('tempdb.dbo.#T_TMP_PPAI') is not null ";
                lstrSQL = lstrSQL + "begin ";
                lstrSQL = lstrSQL + "   drop table dbo.#T_TMP_PPAI ";
                lstrSQL = lstrSQL + "end ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }

                lstrSQL = "create table dbo.#T_TMP_PPAI ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " CD_PRODUTO      [smallint]   NOT NULL ";
                lstrSQL = lstrSQL + ",CD_ITEM_PRODUTO [smallint]   NOT NULL ";
                lstrSQL = lstrSQL + ",ST_IND_RETIRADO [tinyint]    NOT NULL ";
                lstrSQL = lstrSQL + ",QTD_PRODUTO     [smallint]   NOT NULL ";
                lstrSQL = lstrSQL + ",VA_DESCONTO     [smallmoney] NOT NULL ";
                lstrSQL = lstrSQL + ") ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }

                lstrSQL = "if object_id('tempdb.dbo.#T_TMP_EA') is not null ";
                lstrSQL = lstrSQL + "begin ";
                lstrSQL = lstrSQL + "   drop table dbo.#T_TMP_EA ";
                lstrSQL = lstrSQL + "end ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }

                lstrSQL = "create table dbo.#T_TMP_EA ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " ST_TP_ENDERECO   [tinyint]                                   NOT NULL ";
                lstrSQL = lstrSQL + ",CD_LOGRADOURO    [int]                                       NULL ";
                lstrSQL = lstrSQL + ",NU_RESIDENCIA    [int]                                       NOT NULL ";
                lstrSQL = lstrSQL + ",COMPL_RESIDENCIA [varchar] (2)  COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",NU_BLOCO         [varchar] (5)  COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",NU_APARTAMENTO   [varchar] (4)  COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",DS_COMPLEMENTO   [varchar](50)  COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",DS_PONTO_REF     [varchar](50)  COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",CD_LOCAL_ENTREGA [smallint]                                  NULL ";
                lstrSQL = lstrSQL + ",NU_CEP           [varchar](8)   COLLATE Latin1_General_CI_AI NOT NULL ";
                lstrSQL = lstrSQL + ",DS_ENDERECO_EXT  [varchar](100) COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",DS_BAIRRO_EXT    [varchar](50)  COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",DS_MUNICIPIO_EXT [varchar](50)  COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",DS_UF_EXT        [varchar](2)   COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ",NU_CEP_EXT       [varchar](10)  COLLATE Latin1_General_CI_AI NULL ";
                lstrSQL = lstrSQL + ") ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }


                lstrSQL = "if object_id('tempdb.dbo.#T_TMP_RET') is not null ";
                lstrSQL = lstrSQL + "begin ";
                lstrSQL = lstrSQL + "   drop table dbo.#T_TMP_RET ";
                lstrSQL = lstrSQL + "end ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }

                lstrSQL = "create table dbo.#T_TMP_RET ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " DS_RET [varchar](400) COLLATE Latin1_General_CI_AI NOT NULL ";
                lstrSQL = lstrSQL + ") ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }

            }
            catch (Exception ex)
            {

                _erro = -1;
                _msgErro = "GravaProdutoAgregData (SetCriarTabelasTEMP) - " + ex.Message;

                if (pbolAbrirTransacao)
                {
                    DataContext.RollbackTransaction();
                }

            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaProdutoAgregData (SetCriarTabelasTEMP) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        public void SetPessoaTEMP(PessoaProdutoAgregEntity ObjEnt,
                                  Boolean pbolAbrirTransacao,
                                  Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.Text;

                string lstrSQL = "";

                lstrSQL = "insert into dbo.#T_TMP_CP ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " CD_CONTABIL_PESSOA";
                lstrSQL = lstrSQL + ",ST_TP_PESSOA";
                lstrSQL = lstrSQL + ",NM_PESSOA";
                lstrSQL = lstrSQL + ",NM_SOBRENOME";
                lstrSQL = lstrSQL + ",NU_CPF";
                lstrSQL = lstrSQL + ",DT_NASC_FUND";
                lstrSQL = lstrSQL + ",DS_EMAIL";
                lstrSQL = lstrSQL + ",NU_CNPJ";
                lstrSQL = lstrSQL + ",NM_RESPONSAVEL";
                lstrSQL = lstrSQL + ",NU_CPF_RESP";
                lstrSQL = lstrSQL + ",NM_FANTASIA";
                lstrSQL = lstrSQL + ",NU_INSCR_MUN";
                lstrSQL = lstrSQL + ",NU_INSCR_EST";
                lstrSQL = lstrSQL + ",CD_RAMO";
                lstrSQL = lstrSQL + ") values (";
                
                if (ObjEnt.CD_CONTABIL_PESSOA != 0)
                {
                    lstrSQL = lstrSQL + ObjEnt.CD_CONTABIL_PESSOA + ",";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL,";
                }

                lstrSQL = lstrSQL + ObjEnt.ST_TP_PESSOA + ",";
                lstrSQL = lstrSQL + "'" + ObjEnt.NM_PESSOA + "',";
                if (ObjEnt.NM_SOBRENOME != null)
                {
                    lstrSQL = lstrSQL + "'" + ObjEnt.NM_SOBRENOME + "',";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL,";
                }
                if (ObjEnt.NU_CPF != "")
                {
                    lstrSQL = lstrSQL + ObjEnt.NU_CPF + ",";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL,";
                }
                if (!string.IsNullOrEmpty(ObjEnt.DT_NASC_FUND))
                {
                    string lstrDataFormat = DateTime.Parse(ObjEnt.DT_NASC_FUND).ToString("dd/MM/yyyy");

                    lstrSQL = lstrSQL + "'" + lstrDataFormat + "',";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL,";
                }

                if (ObjEnt.DS_EMAIL != "")
                {
                    lstrSQL = lstrSQL + "'" + ObjEnt.DS_EMAIL + "',";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL,";
                }

                if ((ObjEnt.NU_CNPJ != "") && (ObjEnt.NU_CNPJ != null))
                {
                    lstrSQL = lstrSQL + ObjEnt.NU_CNPJ + ",";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL,";
                }

                if ((ObjEnt.NM_RESPONSAVEL != "") && (ObjEnt.NM_RESPONSAVEL != null))
                {
                    lstrSQL = lstrSQL + "'" + ObjEnt.NM_RESPONSAVEL + "',";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL,";
                }
                lstrSQL = lstrSQL + "NULL,";

                if ((ObjEnt.NM_FANTASIA != "") && (ObjEnt.NM_FANTASIA != null))
                {
                    lstrSQL = lstrSQL + "'" + ObjEnt.NM_FANTASIA + "',";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL,";
                }

                if ((ObjEnt.NU_INSCR_MUN != "") && (ObjEnt.NU_INSCR_MUN != null))
                {
                    lstrSQL = lstrSQL + "'" + ObjEnt.NU_INSCR_MUN + "',";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL,";
                }
                if ((ObjEnt.NU_INSCR_EST != "") && (ObjEnt.NU_INSCR_EST != null))
                {
                    lstrSQL = lstrSQL + "'" + ObjEnt.NU_INSCR_EST + "',";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL,";
                }
                lstrSQL = lstrSQL + "NULL)";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }


            }
            catch (Exception ex)
            {

                _erro = -1;
                _msgErro = "GravaProdutoAgregData (SetPessoaTEMP) - " + ex.Message;
            
                if (pbolAbrirTransacao)
                {
                    DataContext.RollbackTransaction();
                }

            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaProdutoAgregData (SetPessoaTEMP) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }
        public void SetEnderecoTEMP(EnderecoProdutoAgregEntity ObjEnt,
                                    Boolean pbolAbrirTransacao,
                                    Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.Text;

                string lstrSQL = "";

                lstrSQL = "insert into #T_TMP_EA ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " ST_TP_ENDERECO ";
                lstrSQL = lstrSQL + ",CD_LOGRADOURO ";
                lstrSQL = lstrSQL + ",NU_RESIDENCIA ";
                lstrSQL = lstrSQL + ",COMPL_RESIDENCIA ";
                lstrSQL = lstrSQL + ",NU_BLOCO ";
                lstrSQL = lstrSQL + ",NU_APARTAMENTO ";
                lstrSQL = lstrSQL + ",DS_COMPLEMENTO ";
                lstrSQL = lstrSQL + ",DS_PONTO_REF ";
                lstrSQL = lstrSQL + ",CD_LOCAL_ENTREGA ";
                lstrSQL = lstrSQL + ",NU_CEP ";
                lstrSQL = lstrSQL + ",DS_ENDERECO_EXT ";
                lstrSQL = lstrSQL + ",DS_BAIRRO_EXT ";
                lstrSQL = lstrSQL + ",DS_MUNICIPIO_EXT ";
                lstrSQL = lstrSQL + ",DS_UF_EXT ";
                lstrSQL = lstrSQL + ",NU_CEP_EXT ";
                lstrSQL = lstrSQL + ") VALUES ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " 1" + " ";
                lstrSQL = lstrSQL + "," + ObjEnt.CD_LOGRADOURO + " ";
                lstrSQL = lstrSQL + "," + ObjEnt.NU_RESIDENCIA + " ";

                if ((ObjEnt.COMPL_RESIDENCIA != "") && (ObjEnt.COMPL_RESIDENCIA != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.COMPL_RESIDENCIA + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }
                if ((ObjEnt.NU_BLOCO != "") && (ObjEnt.NU_BLOCO != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.NU_BLOCO + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }

                if ((ObjEnt.NU_APARTAMENTO != "") && (ObjEnt.NU_APARTAMENTO != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.NU_APARTAMENTO + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }

                if ((ObjEnt.DS_COMPLEMENTO != "") && (ObjEnt.DS_COMPLEMENTO != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.DS_COMPLEMENTO + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }

                if ((ObjEnt.DS_PONTO_REF != "") && (ObjEnt.DS_PONTO_REF != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.DS_PONTO_REF + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }

                lstrSQL = lstrSQL + ",NULL ";

                if ((ObjEnt.NU_CEP!= "") && (ObjEnt.NU_CEP != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.NU_CEP + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }

                if ((ObjEnt.DS_ENDERECO_EXT != "") && (ObjEnt.DS_ENDERECO_EXT != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.DS_ENDERECO_EXT + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }

                if ((ObjEnt.DS_BAIRRO_EXT != "") && (ObjEnt.DS_BAIRRO_EXT != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.DS_BAIRRO_EXT + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }

                if ((ObjEnt.DS_MUNICIPIO_EXT != "") && (ObjEnt.DS_MUNICIPIO_EXT != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.DS_MUNICIPIO_EXT + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }

                if ((ObjEnt.DS_UF_EXT != "") && (ObjEnt.DS_UF_EXT != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.DS_UF_EXT + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }

                if ((ObjEnt.NU_CEP_EXT != "") && (ObjEnt.NU_CEP_EXT != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.NU_CEP_EXT + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }                
                lstrSQL = lstrSQL + ") ";


                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }


            }
            catch (Exception ex)
            {

                _erro = -1;
                _msgErro = "GravaProdutoAgregData (SetEnderecoTEMP) - " + ex.Message;

                if (pbolAbrirTransacao)
                {
                    DataContext.RollbackTransaction();
                }

            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaProdutoAgregData (SetEnderecoTEMP) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }
        public void SetTelefoneTEMP(TelefoneProdutoAgregEntity ObjEnt,
                                    Boolean pbolAbrirTransacao,
                                    Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.Text;

                string lstrSQL = "";

                lstrSQL = "insert into #T_TMP_TA ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " ST_TP_TELEFONE ";
                lstrSQL = lstrSQL + ",NU_DDD ";
                lstrSQL = lstrSQL + ",NU_TEL ";
                lstrSQL = lstrSQL + ",NU_RAMAL ";
                lstrSQL = lstrSQL + ",DS_OBS ";
                lstrSQL = lstrSQL + ") VALUES ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " " + ObjEnt.ST_TP_TELEFONE + " ";
                lstrSQL = lstrSQL + "," + ObjEnt.NU_DDD + " ";
                lstrSQL = lstrSQL + ",'" + ObjEnt.NU_TEL + "' ";

                if ((ObjEnt.NU_RAMAL != "") && (ObjEnt.NU_RAMAL != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.NU_RAMAL + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }

                if ((ObjEnt.DS_OBS != "") &&  (ObjEnt.DS_OBS != null))
                {
                    lstrSQL = lstrSQL + ",'" + ObjEnt.DS_OBS + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + ",NULL ";
                }
               
               
                lstrSQL = lstrSQL + ") ";

                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }


            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = "GravaProdutoAgregData (SetPedidoTEMP) - " + ex.Message;

                if (pbolAbrirTransacao)
                {
                    DataContext.RollbackTransaction();
                }

            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaProdutoAgregData (SetPedidoTEMP) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }
        public void SetDebitoTEMP(DebitoProdutoAgregEntity ObjEnt,
                                  Boolean pbolAbrirTransacao,
                                  Boolean pbolFecharConexao)
        {
            string lstrSQL = "";

            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.Text;

                //string lstrSQL = "";

                lstrSQL = "insert into #T_TMP_DA ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " CD_FONTE_DEBITO ";
                lstrSQL = lstrSQL + ",DT_ENVIO_COBRANCA ";
                lstrSQL = lstrSQL + ",DT_RETORNO_COBRANCA ";
                lstrSQL = lstrSQL + ",NM_TITULAR ";
                lstrSQL = lstrSQL + ",NU_CPF_CNPJ ";
                lstrSQL = lstrSQL + ",NU_DIA_DEBITO ";
                lstrSQL = lstrSQL + ",NU_CARTAO ";
                lstrSQL = lstrSQL + ",NU_CVV_CARTAO ";
                lstrSQL = lstrSQL + ",DT_VALID_CARTAO ";
                lstrSQL = lstrSQL + ",ST_TP_PARC_CARTAO ";
                lstrSQL = lstrSQL + ",COD_AUTORIZACAO ";
                lstrSQL = lstrSQL + ",COD_NSU ";
                lstrSQL = lstrSQL + ",NU_AGENCIA ";
                lstrSQL = lstrSQL + ",NU_DV_AGENCIA ";
                lstrSQL = lstrSQL + ",NU_CONTA ";
                lstrSQL = lstrSQL + ",NU_DV_CONTA ";
                lstrSQL = lstrSQL + ") VALUES ";
                lstrSQL = lstrSQL + "( ";

                lstrSQL = lstrSQL + ObjEnt.CD_FONTE_DEBITO.ToString() + ", ";

                if (!string.IsNullOrEmpty(ObjEnt.DT_ENVIO_COBRANCA))
                {
                    //lstrSQL = lstrSQL + "'" + DateTime.Parse(ObjEnt.DT_ENVIO_COBRANCA).ToString("dd/MM/yyyy") + "'," ;
                    lstrSQL = lstrSQL + "'" + ObjEnt.DT_ENVIO_COBRANCA + "',";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL, ";
                }

                if (!string.IsNullOrEmpty(ObjEnt.DT_RETORNO_COBRANCA))
                {
                    //lstrSQL = lstrSQL + "'" + DateTime.Parse(ObjEnt.DT_RETORNO_COBRANCA).ToString("dd/MM/yyyy") + "',";
                    lstrSQL = lstrSQL + "'" + ObjEnt.DT_RETORNO_COBRANCA + "',";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL, ";
                }

                if (!string.IsNullOrEmpty(ObjEnt.NM_TITULAR))
                {
                    lstrSQL = lstrSQL + "'" + ObjEnt.NM_TITULAR + "', ";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL, ";
                }                

                if (!string.IsNullOrEmpty(ObjEnt.NU_CPF_CNPJ))
                {
                    lstrSQL = lstrSQL + ObjEnt.NU_CPF_CNPJ + ", ";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL, ";
                }

                if (ObjEnt.NU_DIA_DEBITO != 0)
                {
                    lstrSQL = lstrSQL + ObjEnt.NU_DIA_DEBITO + ", ";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL, ";
                }

                if (!string.IsNullOrEmpty(ObjEnt.NU_CARTAO))
                {
                    lstrSQL = lstrSQL + "'" + ObjEnt.NU_CARTAO + "', ";
                    lstrSQL = lstrSQL + "'" + ObjEnt.NU_CVV_CARTAO + "', ";
                    lstrSQL = lstrSQL + "'" + ObjEnt.DT_VALID_CARTAO + "', ";
                    lstrSQL = lstrSQL + ObjEnt.ST_TP_PARC_CARTAO.ToString() + ", ";
                    lstrSQL = lstrSQL + "'" + ObjEnt.COD_AUTORIZACAO + "', ";
                    lstrSQL = lstrSQL + "'" + ObjEnt.COD_NSU + "', ";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL, ";
                    lstrSQL = lstrSQL + "NULL, ";
                    lstrSQL = lstrSQL + "NULL, ";
                    lstrSQL = lstrSQL + "NULL, ";
                    lstrSQL = lstrSQL + "'" + ObjEnt.COD_AUTORIZACAO + "', ";
                    lstrSQL = lstrSQL + "'" + ObjEnt.COD_NSU + "', ";
                }

                if (!string.IsNullOrEmpty(ObjEnt.NU_AGENCIA))
                {
                    lstrSQL = lstrSQL + "'" + ObjEnt.NU_AGENCIA + "', ";
                    lstrSQL = lstrSQL + "'" + ObjEnt.NU_DV_AGENCIA + "', ";
                    lstrSQL = lstrSQL + "'" + ObjEnt.NU_CONTA + "', ";
                    lstrSQL = lstrSQL + "'" + ObjEnt.NU_DV_CONTA + "' ";
                }
                else
                {
                    lstrSQL = lstrSQL + "NULL, ";
                    lstrSQL = lstrSQL + "NULL, ";
                    lstrSQL = lstrSQL + "NULL, ";
                    lstrSQL = lstrSQL + "NULL ";
                }

                lstrSQL = lstrSQL + ") ";

                myComando.CommandText = lstrSQL;

                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }


            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = "GravaProdutoAgregData (SetDebitoTEMP) - " + ex.Message;


                if (pbolAbrirTransacao)
                {
                    DataContext.RollbackTransaction();
                }

            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaProdutoAgregData (SetDebitoTEMP) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }
        public void SetPedidoTEMP(PedidoProdutoAgregEntity ObjEnt,
                                  Boolean pbolAbrirTransacao,
                                  Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.Text;

                string lstrSQL = "";

                lstrSQL = "insert into #T_TMP_PPA ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " CD_FORMA_PAG ";
                lstrSQL = lstrSQL + ",CD_CONTABIL_REPR_VENDA ";
                lstrSQL = lstrSQL + ",CD_CONTABIL_VEND ";
                lstrSQL = lstrSQL + ",VA_FRETE ";
                lstrSQL = lstrSQL + ",CD_FORMA_EXPEDICAO ";
                lstrSQL = lstrSQL + ",ST_IND_RETIRADO ";
                lstrSQL = lstrSQL + ") VALUES ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " " + ObjEnt.CD_FORMA_PAG.ToString() + " ";
                lstrSQL = lstrSQL + "," + ObjEnt.CD_CONTABIL_REPR_VENDA.ToString() + " ";
                lstrSQL = lstrSQL + "," + ObjEnt.CD_CONTABIL_VEND.ToString() + " ";
                lstrSQL = lstrSQL + "," + ObjEnt.VA_FRETE.ToString().Replace(",", ".");
                lstrSQL = lstrSQL + "," + ObjEnt.CD_FORMA_EXPEDICAO.ToString() + " ";
                lstrSQL = lstrSQL + "," + ObjEnt.ST_IND_RETIRADO.ToString() + " ";
                lstrSQL = lstrSQL + ") ";


                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }


            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = "GravaProdutoAgregData (SetPedidoTEMP) - " + ex.Message;

                if (pbolAbrirTransacao)
                {
                    DataContext.RollbackTransaction();
                }

            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaProdutoAgregData (SetPedidoTEMP) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        public void SetItemPedidoTEMP(ItemPedidoProdutoAgregEntity ObjEnt,
                                      Boolean pbolAbrirTransacao,
                                      Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.Text;

                string lstrSQL = "";

                lstrSQL = "insert into #T_TMP_PPAI ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " CD_PRODUTO ";
                lstrSQL = lstrSQL + ",CD_ITEM_PRODUTO ";
                lstrSQL = lstrSQL + ",ST_IND_RETIRADO ";
                lstrSQL = lstrSQL + ",QTD_PRODUTO ";
                lstrSQL = lstrSQL + ",VA_DESCONTO ";
                lstrSQL = lstrSQL + ") VALUES ";
                lstrSQL = lstrSQL + "( ";
                lstrSQL = lstrSQL + " " + ObjEnt.CD_PRODUTO.ToString() + " ";
                lstrSQL = lstrSQL + "," + ObjEnt.CD_ITEM_PRODUTO.ToString() + " ";
                lstrSQL = lstrSQL + "," + ObjEnt.ST_IND_RETIRADO.ToString() + " ";
                lstrSQL = lstrSQL + "," + ObjEnt.QTD_PRODUTO.ToString() + " ";
                lstrSQL = lstrSQL + "," + ObjEnt.VA_DESCONTO.ToString().Replace(",",".") + " ";
                lstrSQL = lstrSQL + ")"; 


                myComando.CommandText = lstrSQL;
                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }


            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = "GravaProdutoAgregData (SetItemPedidoTEMP) - " + ex.Message;

                if (pbolAbrirTransacao)
                {
                    DataContext.RollbackTransaction();
                }

            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaProdutoAgregData (SetItemPedidoTEMP) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        public bool GetCriticaTabela(string pstrTabela, 
                                     string pstrColuna, 
                                     string pstrValor, 
                                     Boolean pbolAbrirTransacao, 
                                     Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                
                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.Text;

                string lstrSQL = "";

                lstrSQL = "Select 1 ";
                lstrSQL = lstrSQL + " from " + pstrTabela + " (nolock) ";
                lstrSQL = lstrSQL + " where " + pstrColuna + " = '" + pstrValor + "'";

                myComando.CommandText = lstrSQL;

                DbDataReader myReader = DataContext.ExecutarReader(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return false;
                }
                else
                {
                    if (myReader.HasRows)
                    {
                        myReader.Read();

                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    
                }


            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = "GravaProdutoAgregData (GetCriticaTabela) - " + ex.Message;

                if (pbolAbrirTransacao)
                {
                    DataContext.RollbackTransaction();
                }

                return false;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaProdutoAgregData (GetCriticaTabela) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }
        }
        public bool GetCriticaTabela(string pstrTabela,
                                    string pstrColuna1,
                                    string pstrValor1,
                                    string pstrColuna2,
                                    string pstrValor2,
                                    Boolean pbolAbrirTransacao,
                                    Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.Text;

                string lstrSQL = "";

                lstrSQL = "Select 1 ";
                lstrSQL = lstrSQL + " from " + pstrTabela + " (nolock) ";
                lstrSQL = lstrSQL + " where " + pstrColuna1 + " = '" + pstrValor1 + "'";
                lstrSQL = lstrSQL + " and " + pstrColuna2 + " = '" + pstrValor2 + "'";

                myComando.CommandText = lstrSQL;

                DbDataReader myReader = DataContext.ExecutarReader(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return false;
                }
                else
                {
                    if (myReader.HasRows)
                    {
                        myReader.Read();

                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }


            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = "GravaProdutoAgregData (GetCriticaTabela) - " + ex.Message;


                if (pbolAbrirTransacao)
                {
                    DataContext.RollbackTransaction();
                }

                return false;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaProdutoAgregData (GetCriticaTabela) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }
        }
        public Int32 GetTipoPagameto(Int32 pintFormaPagamento,
                                     Boolean pbolAbrirTransacao,
                                     Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.Text;

                string lstrSQL = "";

                lstrSQL = "select CD_TP_PAGAMENTO ";
                lstrSQL = lstrSQL + " from FORMA_PAGAMENTO (nolock) ";
                lstrSQL = lstrSQL + " where CD_FORMA_PAG = " + pintFormaPagamento;

                myComando.CommandText = lstrSQL;

                DbDataReader myReader = DataContext.ExecutarReader(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return 0;
                }
                else
                {
                    if (myReader.HasRows)
                    {
                        myReader.Read();

                        Int32 pintTipoPamento;

                        pintTipoPamento = int.Parse(myReader[0].ToString());

                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        return pintTipoPamento;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {

                _erro = -1;
                _msgErro = "GravaProdutoAgregData (GetTipoPagameto) - " + ex.Message;


                if (pbolAbrirTransacao)
                {
                    DataContext.RollbackTransaction();
                }

                return 0;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaProdutoAgregData (GetTipoPagameto) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }
        }

        public void SetGravarPedido(Int32 pintIDEditora, Int32 pintIDUsuario, bool pbolAbrirTransacao)
        {
            try
            {

                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.StoredProcedure;
                myComando.CommandText = "SP_INS_PED_PROD_AGREGADO";

                DbParameter myParametro;

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "p_in_i_cd_contabil_editora";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.Int32;
                myParametro.Value = pintIDEditora;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "p_in_i_cd_contabil_operador";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.Int32;
                myParametro.Value = pintIDUsuario;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_ds_key_encrypt";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.String;
                myParametro.Value = "";

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "p_out_vc_msg";
                myParametro.Direction = ParameterDirection.Output;
                myParametro.DbType = DbType.String;
                myParametro.Value = "";

                myComando.Parameters.Add(myParametro);

                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return;
                }
                else
                {
                    string lstrRet = myComando.Parameters["p_out_vc_msg"].Value.ToString();

                    _erro = 0;
                    _msgErro = lstrRet;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                
            }
        }

        public string GetRetorGravacao(bool pbolAbrirTransacao)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.Text;

                string lstrSQL = "";

                lstrSQL = "select DS_RET ";
                lstrSQL = lstrSQL + " from #T_TMP_RET (nolock) ";

                myComando.CommandText = lstrSQL;

                DbDataReader myReader = DataContext.ExecutarReader(myComando);

                if (DataContext.Erro != 0)
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                    return "";
                }
                else
                {
                    if (myReader.HasRows)
                    {
                        myReader.Read();

                        string lstrRet = "";

                        lstrRet = myReader[0].ToString();

                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        return lstrRet;
                    }
                    else
                    {
                        return "";
                    }
                }
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
