using System.Globalization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

using CWA.Venda.Entity;

namespace CWA.Venda.Data
{
    public class BoletoData
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

        public BoletoParametroEntity GetBoletoParametro(string[] pstrVetorConVB6 = null, int pintTipoEnvioEmail = 1)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao(pstrVetorConVB6))
                {


                    if (pintTipoEnvioEmail == 1)
                    {
                        DbCommand myComando = DataContext.CriarComando(false);
                        myComando.CommandType = CommandType.StoredProcedure;
                        myComando.CommandText = "WS_BUSCA_INF_PARAMETRO_BOLETO_EMAIL";

                        DbDataReader myReader = DataContext.ExecutarReader(myComando);

                        if (DataContext.Erro == 0)
                        {
                            BoletoParametroEntity lPar = new BoletoParametroEntity();

                            if (myReader.HasRows)
                            {

                                myReader.Read();

                                lPar.DS_EMAIL_PADRAO_BOL_WEB = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                                lPar.DS_SERV_SMTP_BOL_WEB = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                                lPar.DS_SENHA_EMAIL_PADRAO_BOL_WEB = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                                lPar.DS_EMAIL_LOGIN_BOL_WEB = myReader.IsDBNull(3) ? "" : myReader[3].ToString();
                                lPar.NU_PORTA_SMTP_BOL_WEB = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                                lPar.DS_CAMINHO_BOLETO = myReader.IsDBNull(5) ? "" : myReader[5].ToString();
                                lPar.DS_CAMINHO_BOLETO_EMAIL = myReader.IsDBNull(6) ? "" : myReader[6].ToString();
                                lPar.DS_ASSUNTO_EMAIL_BOL = myReader.IsDBNull(7) ? "" : myReader[7].ToString();
                                lPar.DS_CAMINHO_IMAGEM_EMAIL_BOL = myReader.IsDBNull(8) ? "" : myReader[8].ToString();
                                lPar.DS_CAMINHO_MENSAGEM_EMAIL_BOL = myReader.IsDBNull(9) ? "" : myReader[9].ToString();

                                lPar.ST_EMAIL_SSL = myReader.IsDBNull(10) ? "0" : myReader[10].ToString();

                                lPar.ST_REQ_AUTENT_EMAIL_BOL = myReader.IsDBNull(11) ? 0 : int.Parse(myReader[11].ToString());
                                lPar.ST_REQ_AUTENT_EMAIL_WF = myReader.IsDBNull(12) ? 0 : int.Parse(myReader[12].ToString());

                                if (myReader != null)
                                {
                                    if (!myReader.IsClosed)
                                    {
                                        myReader.Close();
                                    }
                                }

                                return lPar;
                            }
                            else
                            {
                                if (myReader != null)
                                {
                                    if (!myReader.IsClosed)
                                    {
                                        myReader.Close();
                                    }
                                }

                                return null;
                            }
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            _erro = DataContext.Erro;
                            _msgErro = DataContext.MsgErro;

                            return null;
                        }
                    }
                    else
                    {
                        //FIXO PARA TODOS OS CLIENTES SÓ PODE UMA PARAMETRIZAÇÃO COM ESSA CHAVE GATEWAY/ACAO
                        string lstrGateway = "MAIL_BOLETO_PDF";
                        string lstrAcao = "ENVIO";

                        DbCommand myComando = DataContext.CriarComando(false);
                        myComando.CommandType = CommandType.StoredProcedure;
                        myComando.CommandText = "WS_BUSCA_INF_PARAMETRO_BOLETO_EMAIL_API";

                        DbParameter myParametro;

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "SP_NM_GATEWAY";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.String;
                        myParametro.Value = lstrGateway;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "SP_NM_TP_ACAO";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.String;
                        myParametro.Value = lstrAcao;

                        myComando.Parameters.Add(myParametro);

                        DbDataReader myReader = DataContext.ExecutarReader(myComando);

                        if (DataContext.Erro == 0)
                        {
                            BoletoParametroEntity lPar = new BoletoParametroEntity();

                            if (myReader.HasRows)
                            {

                                myReader.Read();

                                lPar.NM_INTERFACE_CWA = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                                lPar.NM_METODO_INTEFACE_CWA = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                                lPar.NM_ENDPOINT_CWA = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                                lPar.NM_TP_ENDPOINT_CWA = myReader.IsDBNull(3) ? "" : myReader[3].ToString();
                                lPar.NM_ENDPOINT_CLIENT = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                                lPar.NM_TP_ENDPOINT_CLIENT = myReader.IsDBNull(5) ? "" : myReader[5].ToString();
                                lPar.NM_METODO_ENDPOINT_CLIENT = myReader.IsDBNull(6) ? "" : myReader[6].ToString();
                                lPar.NM_AMBIENTE_ENDPOINT_CLIENT = myReader.IsDBNull(7) ? "" : myReader[7].ToString();
                                lPar.NM_CONTENTTYPE_ENDPOINT_CLIENT = myReader.IsDBNull(8) ? "" : myReader[8].ToString();
                                lPar.NM_MERCHANTID_ENDPOINT_CLIENT = myReader.IsDBNull(9) ? "" : myReader[9].ToString();
                                lPar.NM_MERCHANTKEY_ENDPOINT_CLIENT = myReader.IsDBNull(10) ? "" : myReader[10].ToString();
                                lPar.NM_LISTA_PARMS = myReader.IsDBNull(11) ? "" : myReader[11].ToString();

                                if (myReader != null)
                                {
                                    if (!myReader.IsClosed)
                                    {
                                        myReader.Close();
                                    }
                                }

                                return lPar;
                            }
                            else
                            {
                                if (myReader != null)
                                {
                                    if (!myReader.IsClosed)
                                    {
                                        myReader.Close();
                                    }
                                }

                                return null;
                            }
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            _erro = DataContext.Erro;
                            _msgErro = DataContext.MsgErro;

                            return null;
                        }

                    }


                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetBoletoParametro (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public BoletoReciboEntity GetBoletoRecibo(int pintRecibo, int pintParcela, int pintDV, string[] pstrVetorConVB6 = null)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao(pstrVetorConVB6))
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUCAR_INF_RECIBO_BOLETO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_RECIBO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintRecibo;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_PARCELA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintParcela;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_DV";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintDV;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        BoletoReciboEntity lEnt = new BoletoReciboEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.VA_RECIBO = myReader.IsDBNull(0) ? "" : double.Parse(myReader[0].ToString()).ToString("0.00");
                            lEnt.DT_VENCIMENTO = myReader.IsDBNull(1) ? "" : DateTime.Parse(myReader[1].ToString()).ToString("dd/MM/yyyy");

                            lEnt.NU_SERIE_CTR= myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                            lEnt.NU_CTR= myReader.IsDBNull(3) ? "" : myReader[3].ToString();
                            lEnt.NU_DV_CTR= myReader.IsDBNull(4) ? "" : myReader[4].ToString();

                            lEnt.NM_PESSOA= myReader.IsDBNull(5) ? "" : myReader[5].ToString();

                            lEnt.NU_DOCUMENTO = myReader.IsDBNull(6) ? "" : myReader[6].ToString().Trim();

                            if (lEnt.NU_DOCUMENTO != "")
                            {
                                Int64 lintDoc = Int64.Parse(myReader[6].ToString());

                                if (lEnt.NU_DOCUMENTO.Trim().Length <= 11)
                                {
                                    lEnt.NU_DOCUMENTO = "CPF: " + String.Format(@"{0:000\.000\.000\-00}", lintDoc); 
                                }
                                else
                                {
                                    lEnt.NU_DOCUMENTO = "CNPJ: " +  String.Format(@"{0:00\.000\.000\/0000\-00}", lintDoc);
                                }
                            }

                            lEnt.NU_PARCELAS= myReader.IsDBNull(7) ? "" : Int64.Parse(myReader[7].ToString()).ToString("00");
                            lEnt.NU_PERIODO= myReader.IsDBNull(8) ? "" : myReader[8].ToString();

                            lEnt.DT_INICIO = myReader.IsDBNull(9) ? "" : DateTime.Parse(myReader[9].ToString()).ToString("dd/MM/yyyy");
                            lEnt.DT_TERMINO = myReader.IsDBNull(10) ? "" : DateTime.Parse(myReader[10].ToString()).ToString("dd/MM/yyyy");

                            lEnt.NU_NOSSO_NUMERO = myReader.IsDBNull(11) ? "0" : myReader[11].ToString();

                            lEnt.DS_EMAIL = myReader.IsDBNull(12) ? "" : myReader[12].ToString();
                            

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lEnt;
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetBoletoRecibo (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public BoletoEnderecoEntity GetBoletoEndereco(string pstrSerieCTR, int pintNuCTR, int pintDvCTR, 
                                                      int pintRecibo = 0, int pintParcela = 0, int pintDV = 0,  
                                                      string[] pstrVetorConVB6 = null)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao(pstrVetorConVB6))
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUCAR_INF_ENDERECO_BOLETO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_SERIE_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrSerieCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintNuCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_DV_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintDvCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_RECIBO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintRecibo;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_PARCELA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintParcela;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_DV";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintDV;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        BoletoEnderecoEntity lEnt = new BoletoEnderecoEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.NU_RESIDENCIA = myReader.IsDBNull(0) ? "S/N" : myReader[0].ToString();
                            lEnt.COMPL_RESIDENCIA  = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                            lEnt.NU_BLOCO = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                            lEnt.NU_APARTAMENTO = myReader.IsDBNull(3) ? "" : myReader[3].ToString();
                            lEnt.DS_COMPLEMENTO = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                            lEnt.DS_PONTO_REF = myReader.IsDBNull(5) ? "" : myReader[5].ToString();
                            lEnt.DS_TIPO_ABREV = myReader.IsDBNull(6) ? "" : myReader[6].ToString();
                            lEnt.DS_LOGRADOURO = myReader.IsDBNull(7) ? "" : myReader[7].ToString();
                            lEnt.DS_BAIRRO = myReader.IsDBNull(8) ? "" : myReader[8].ToString();
                            lEnt.DS_MUNICIPIO = myReader.IsDBNull(9) ? "" : myReader[9].ToString();
                            lEnt.DS_UF = myReader.IsDBNull(10) ? "" : myReader[10].ToString();
                            lEnt.NU_CEP = myReader.IsDBNull(11) ? "" : double.Parse(myReader[11].ToString()).ToString("00000-000");

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lEnt;
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetBoletoEndereco (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public BoletoMensagemEntity GetBoletoMensagem(int pintBanco, string[] pstrVetorConVB6 = null)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao(pstrVetorConVB6 ))
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUCAR_INF_MENSAGEM_BOLETO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_BANCO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintBanco;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        BoletoMensagemEntity lEnt = new BoletoMensagemEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.CD_BANCO = myReader.IsDBNull(0) ? 0 : int.Parse(myReader[0].ToString());
                            lEnt.NM_MSG_BOL1 = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                            lEnt.NM_MSG_BOL2 = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                            lEnt.NM_MSG_BOL3 = myReader.IsDBNull(3) ? "" : myReader[3].ToString();
                            lEnt.NM_MSG_BOL4 = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                            lEnt.NM_MSG_BOL5 = myReader.IsDBNull(5) ? "" : myReader[5].ToString();
                            lEnt.NM_MSG_BOL6 = myReader.IsDBNull(6) ? "" : myReader[6].ToString();
                            lEnt.NM_MSG_BOL7 = myReader.IsDBNull(7) ? "" : myReader[7].ToString();

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lEnt;
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetBoletoMensagem (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }    
    
    }
}
