using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

using CWA.Venda.Entity;
using CWA.Central.Entity;

namespace CWA.Venda.Data
{
    public class ContratoAssinanteData
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

        public StatusContratoEntity GetStatusContrato(Int32 pintCTR, 
                                                      int pintDVCTR, 
                                                      string pstrSerieCTR, 
                                                      Int32 pintCodigoAssinante)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_STATUS_CONTRATO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_DV_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintDVCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_SERIE_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrSerieCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCodigoAssinante;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        StatusContratoEntity lEnt = new StatusContratoEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.ST_ESTADO_ATUAL = int.Parse(myReader[0].ToString());
                            lEnt.CD_MOTIVO_SUSCANC = int.Parse(myReader[1].ToString());
                            lEnt.DT_VENDA =  DateTime.Parse(myReader[2].ToString());

                            lEnt.NM_PESSOA = myReader[3].ToString();
                            lEnt.ST_ONLINE_PROD1 = int.Parse(myReader[4].ToString());
                            lEnt.ST_ONLINE_PROD2 = int.Parse(myReader[5].ToString());
                            lEnt.ST_TP_PRODUTO = int.Parse(myReader[6].ToString());

                            lEnt.CD_CAMPANHA = int.Parse(myReader[7].ToString());
                            lEnt.CD_PLANO = int.Parse(myReader[8].ToString());


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
                        _msgErro = "ContratoAssinanteData (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        
        }



        public Int32 GetContabilContrato(Int32 pintCTR,
                                         int pintDVCTR,
                                         string pstrSerieCTR)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_CONTABIL_CTR";

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
                    myParametro.Value = pintCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_DV_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintDVCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PESSOA";
                    myParametro.Direction = ParameterDirection.Output;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = 0;

                    myComando.Parameters.Add(myParametro);

                    DataContext.ExecutarComando(myComando);

                    if (DataContext.Erro == 0)
                    {

                        int lintCDPessoa = int.Parse(myComando.Parameters["SP_CD_PESSOA"].Value.ToString());

                        return lintCDPessoa;
                    }
                    else
                    {
                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return 0;

                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return 0;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return 0;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "ContratoAssinanteData (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public void SetSituacaoContrato(Int32 pintCTR,
                                        int pintDVCTR,
                                        string pstrSerieCTR,
                                        Int32 pintCodigoAssinante,
                                        int pintSituacaoAtual,
                                        int pintSituacaoAnterior,
                                        Boolean pbolAbrirTransacao,
                                        Boolean pbolFecharConexao)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "SP_CONTRATO_ASSINANTE_U_14";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_cd_contabil_pessoa";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCodigoAssinante;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_nu_serie_ctr";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrSerieCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_nu_ctr";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_nu_dv_ctr";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintDVCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_st_estado_atual";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintSituacaoAtual;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_st_estado_anterior";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintSituacaoAnterior;

                    myComando.Parameters.Add(myParametro);

                    DataContext.ExecutarComando(myComando);

                    if (DataContext.Erro != 0)
                    {
                        if (pbolAbrirTransacao)
                        {
                            DataContext.RollbackTransaction();
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return;
                }
            }

            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GravaAssinaturaData (SetSituacaoContrato) - " + ex.Message;
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
                            _msgErro = "GravaAssinaturaData (SetSituacaoContrato) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }


        }

        public Venda.Entity.ContratoAssinanteEntity GetDadosContrato(int pintCdPessoa, 
                                                                     string pstrSerieCTR, 
                                                                     int pintNuCTR, 
                                                                     int pintDvCTR)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCA_INF_CONTRATO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCdPessoa;

                    myComando.Parameters.Add(myParametro);

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

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        ContratoAssinanteEntity lEnt = new ContratoAssinanteEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.NU_SERIE_CTR = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                            lEnt.NU_CTR = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                            lEnt.NU_DV_CTR = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                            lEnt.CD_CONTABIL_PESSOA = myReader.IsDBNull(3) ? "" : myReader[3].ToString();
                            lEnt.NM_PESSOA = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                            lEnt.ST_TP_PESSOA = myReader.IsDBNull(5) ? "" : myReader[5].ToString();
                            lEnt.NU_DOC = myReader.IsDBNull(6) ? "" : myReader[6].ToString();
                            lEnt.DS_EMAIL = myReader.IsDBNull(7) ? "" : myReader[7].ToString();
                            lEnt.NM_PRODUTO = myReader.IsDBNull(8) ? "" : myReader[8].ToString();
                            lEnt.ST_SITUACAO = myReader.IsDBNull(9) ? "" : myReader[9].ToString();
                            
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
                        _msgErro = "GetDadosContrato (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public List<Central.Entity.ContratoAssinanteCentralEntity> GetLerContratosCentral(int pintCD_CONTABIL_PESSOA,
                                                                                          string pstrDS_EMAIL,
                                                                                          Int64 plngNU_CPF,
                                                                                          Int64 plngNU_CNPJ)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCARCONTRATOS";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Output;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCD_CONTABIL_PESSOA;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;

                    if (!string.IsNullOrEmpty(pstrDS_EMAIL))
                    {
                        myParametro.Value = pstrDS_EMAIL;
                    }
                    else
                    {
                        myParametro.Value = DBNull.Value;
                    }
                    myComando.Parameters.Add(myParametro);


                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_CPF";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int64;

                    if (plngNU_CPF != 0)
                    {
                        myParametro.Value = plngNU_CPF;
                    }
                    else
                    {
                        myParametro.Value = DBNull.Value;
                    }
                    myComando.Parameters.Add(myParametro);


                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_CNPJ";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int64;

                    if (plngNU_CNPJ != 0)
                    {
                        myParametro.Value = plngNU_CNPJ;
                    }
                    else
                    {
                        myParametro.Value = DBNull.Value;
                    }
                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<ContratoAssinanteCentralEntity> lcolEnt = new List<ContratoAssinanteCentralEntity>();
                            ContratoAssinanteCentralEntity lEnt;

                            while (myReader.Read())
                            {
                                lEnt = new ContratoAssinanteCentralEntity();


                                lEnt.CD_CONTABIL_PESSOA = int.Parse(myReader[0].ToString());
                                lEnt.NU_SERIE_CTR = myReader[1].ToString();
                                lEnt.NU_CTR = int.Parse(myReader[2].ToString());
                                lEnt.NU_DV_CTR = byte.Parse(myReader[3].ToString());
                                lEnt.NM_PRODUTO = myReader[4].ToString();
                                lEnt.DS_PLANO = myReader[5].ToString();
                                lEnt.QTD_PRODUTO = short.Parse(myReader[6].ToString());

                                lEnt.DT_INICIO = DateTime.Parse(myReader[7].ToString());

                                lEnt.DT_TERMINO = DateTime.Parse(myReader[9].ToString());
                                lEnt.ST_ESTADO_ATUAL = byte.Parse(myReader[11].ToString());
                                lEnt.DS_ESTADO_ATUAL = myReader[12].ToString();

                                if (!myReader.IsDBNull(13))
                                {
                                    lEnt.DT_SUSCAN = DateTime.Parse(myReader[13].ToString());
                                }

                                lEnt.NU_PERIODO = short.Parse(myReader[14].ToString());
                                lEnt.ST_CAD_DEB = int.Parse(myReader[16].ToString());



                                lcolEnt.Add(lEnt);
                            }

                            return lcolEnt;

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
                        _msgErro = "GetLerContratosCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public Central.Entity.ContratoAssinanteCentralEntity GetImpressaoContrato(int pintCdPessoa,
                                                                     string pstrSerieCTR,
                                                                     int pintNuCTR,
                                                                     int pintDvCTR)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_PEGA_CONTRATO_ASSINANTE_IMRESSSAO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCdPessoa;

                    myComando.Parameters.Add(myParametro);

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

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        Central.Entity.ContratoAssinanteCentralEntity lEnt = new Central.Entity.ContratoAssinanteCentralEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            //lEnt.NU_SERIE_CTR = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                            //lEnt.NU_CTR = int.Parse(myReader[1].ToString());
                            //lEnt.NU_DV_CTR = byte.Parse(myReader[2].ToString());
                            //lEnt.CD_CONTABIL_PESSOA = int.Parse(myReader[3].ToString());
                            lEnt.DS_CAMPANHA = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                            lEnt.DS_PLANO = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                            lEnt.DS_TP_PAGAMENTO = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                            lEnt.NU_PARCELAS = byte.Parse(myReader[3].ToString());
                            lEnt.NM_REPRESENTANTE_VENDA = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                            lEnt.NM_VENDEDOR = myReader.IsDBNull(5) ? "" : myReader[5].ToString();
                            lEnt.DT_INICIO = DateTime.Parse(myReader[6].ToString());
                            lEnt.DT_TERMINO = DateTime.Parse(myReader[7].ToString());
                            lEnt.VA_PLANO = decimal.Parse(myReader[8].ToString());
                            lEnt.QTD_PRODUTO = Int16.Parse(myReader[9].ToString());
                            lEnt.QTD_PRODUTO_PLANO = Int16.Parse(myReader[10].ToString());
                            lEnt.NM_PRODUTO = myReader.IsDBNull(11) ? "" : myReader[11].ToString();
                            lEnt.VA_PARC1 = decimal.Parse(myReader[12].ToString());
                            lEnt.VA_DEMAIS = decimal.Parse(myReader[13].ToString());
                            lEnt.DS_PAGAMENTO = myReader.IsDBNull(14) ? "" : myReader[14].ToString();

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
                        _msgErro = "GetDadosContrato (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }
    }
}
