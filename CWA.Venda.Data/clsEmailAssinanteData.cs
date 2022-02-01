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
    public class EmailAssinanteData
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

        public string GetEmail(string pstrSerieCTR,  Int32 pintCtr, int pintDV, Boolean pbolAbrirTransacao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {

                    if (pbolAbrirTransacao)
                    {
                        DataContext.BeginTransaction();
                    }

                    string lstrSQL = "";
                    lstrSQL = lstrSQL + " SELECT TOP 1 DS_EMAIL ";
                    lstrSQL = lstrSQL + " FROM CONTRATO_ASSINANTE  CA INNER JOIN EMAIL_ASSINANTE EA ON EA.CD_CONTABIL_PESSOA = CA.CD_CONTABIL_PESSOA ";
                    lstrSQL = lstrSQL + " WHERE ";
                    lstrSQL = lstrSQL + " EA.ST_EMAIL_PRINCIPAL = 1 AND ";
                    lstrSQL = lstrSQL + " CA.NU_SERIE_CTR = '" + pstrSerieCTR + "' AND ";
                    lstrSQL = lstrSQL + " CA.NU_CTR = " + pintCtr.ToString()  + " AND ";
                    lstrSQL = lstrSQL + " CA.NU_DV_CTR = " + pintDV.ToString() ;
                    lstrSQL = lstrSQL + " ORDER BY EA.NU_SEQ DESC ";

                    string lstrEmail = "";

                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.Text;
                    myComando.CommandText = lstrSQL;

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        
                        if (myReader.HasRows)
                        {

                            myReader.Read();

                           lstrEmail =  myReader[0].ToString();
                        }

                        return lstrEmail;
                    }
                    else
                    {

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return "";

                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return "";
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "EmailAssinante (GetEmailAssinante) - " + ex.Message;

                return "";
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "EmailAssinante (GetEmailAssinante) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public List<Central.Entity.EmailDoAssinanteCentralEntity> GetLerEmailDoAssinanteCentral(int? pintCD_CONTABIL_PESSOA)
        {
            //WSP_BUSCAREMAILSDOASSINANTE
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCAREMAILSDOASSINANTE";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCD_CONTABIL_PESSOA;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<EmailDoAssinanteCentralEntity> lcolEnt = new List<EmailDoAssinanteCentralEntity>();
                            EmailDoAssinanteCentralEntity lEnt;

                            while (myReader.Read())
                            {
                                lEnt = new EmailDoAssinanteCentralEntity();

                                lEnt.CD_CONTABIL_PESSOA = Int32.Parse(myReader[0].ToString());
                                lEnt.DS_EMAIL = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                                lEnt.ST_SITUACAO = byte.Parse(myReader[2].ToString());
                                lEnt.ST_EMAIL_PRINCIPAL = byte.Parse(myReader[3].ToString());
                                lEnt.CD_TIPO_EMAIL = byte.Parse(myReader[4].ToString());
                                lEnt.NU_SEQ = byte.Parse(myReader[5].ToString());
                                lEnt.DS_TIPO_EMAIL = myReader.IsDBNull(6) ? "" : myReader[6].ToString();

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
                        _msgErro = "GetLerEmailDoAssinanteCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public void SetIncluirEmailDoAssinanteCentral(int? CD_CONTABIL_PESSOA, 
                                                      byte NU_SEQ, 
                                                      string DS_EMAIL, 
                                                      byte? ST_SITUACAO, 
                                                      byte? ST_EMAIL_PRINCIPAL, 
                                                      int CD_TIPO_EMAIL,
                                                      Boolean pbolAbrirTransacao,
                                                      Boolean pbolFecharConexao)
        {
            //WSP_INSERIREMAIL

            try
            {
                _erro = 0;
                _msgErro = "";

                int lintSEQ = 0;

                if (DataContext.AbrirConexao())
                {

                    //##############################################################################################################################################
                    DbCommand myComandoQuery = DataContext.CriarComando(false);
                    myComandoQuery.CommandType = CommandType.Text;
                    myComandoQuery.CommandText = "select isnull(sum(NU_SEQ),0) as NU_SEQ  from EMAIL_ASSINANTE where CD_CONTABIL_PESSOA = " + CD_CONTABIL_PESSOA;

                    DbDataReader myReaderQuery = DataContext.ExecutarReader(myComandoQuery);

                    if (DataContext.Erro == 0)
                    {
                        if (myReaderQuery.HasRows)
                        {
                            myReaderQuery.Read();

                            lintSEQ = myReaderQuery.IsDBNull(0) ? 0 : myReaderQuery.GetInt32(0);
                            lintSEQ = lintSEQ + 1;

                            if (myReaderQuery != null)
                            {
                                if (!myReaderQuery.IsClosed)
                                {
                                    myReaderQuery.Close();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (myReaderQuery != null)
                        {
                            if (!myReaderQuery.IsClosed)
                            {
                                myReaderQuery.Close();
                            }
                        }

                        lintSEQ = lintSEQ + 1;
                    }
                    //##############################################################################################################################################

                }


                if (DataContext.AbrirConexao())
                {

                    //@CD_CONTABIL_PESSOA INT, @NU_SEQ TINYINT,@DS_EMAIL VARCHAR(50), @ST_SITUACAO TINYINT, @ST_EMAIL_PRINCIPAL TINYINT, @CD_TIPO_EMAIL INT

                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_INSERIREMAIL";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = CD_CONTABIL_PESSOA;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_SEQ";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = lintSEQ;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (DS_EMAIL != null ? DS_EMAIL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "ST_SITUACAO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (ST_SITUACAO != null ? ST_SITUACAO : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "ST_EMAIL_PRINCIPAL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int16;
                    myParametro.Value = (ST_EMAIL_PRINCIPAL != null ? ST_EMAIL_PRINCIPAL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_TIPO_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int16;
                    myParametro.Value = (CD_TIPO_EMAIL != 0 ? CD_TIPO_EMAIL : (object)DBNull.Value);

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
                    else
                    {
                        DataContext.CommitTransaction();
                    }

                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "SetIncluirEmailDoAssinanteCentral (Insert) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public void SetAlterarEmailDoAssinanteCentral(int? CD_CONTABIL_PESSOA,
                                                      byte NU_SEQ,
                                                      string DS_EMAIL,
                                                      byte? ST_SITUACAO,
                                                      byte? ST_EMAIL_PRINCIPAL,
                                                      int CD_TIPO_EMAIL,
                                                      Boolean pbolAbrirTransacao,
                                                      Boolean pbolFecharConexao)
        {
            try
            {
                if (DataContext.AbrirConexao())
                {

                    //@CD_CONTABIL_PESSOA INT, @NU_SEQ TINYINT,@DS_EMAIL VARCHAR(50), @ST_SITUACAO TINYINT, @ST_EMAIL_PRINCIPAL TINYINT, @CD_TIPO_EMAIL INT

                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_ALTERAEMAIL";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = CD_CONTABIL_PESSOA;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_SEQ";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = NU_SEQ;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (DS_EMAIL != null ? DS_EMAIL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "ST_SITUACAO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (ST_SITUACAO != 0 ? ST_SITUACAO : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "ST_EMAIL_PRINCIPAL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int16;
                    myParametro.Value = (ST_EMAIL_PRINCIPAL != 0 ? ST_EMAIL_PRINCIPAL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_TIPO_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int16;
                    myParametro.Value = (CD_TIPO_EMAIL != 0 ? CD_TIPO_EMAIL : (object)DBNull.Value);

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
                    else
                    {
                        DataContext.CommitTransaction();
                    }

                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "SetAlterarEmailDoAssinanteCentral (Insert) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }        }

        public void SetDeletarEmailDoAssinanteCentral(int CD_CONTABIL_PESSOA, byte NU_SEQ,
                                                      Boolean pbolAbrirTransacao,
                                                      Boolean pbolFecharConexao)
        {
            //WSP_DELETAREMAIL
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_DELETAREMAIL";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = CD_CONTABIL_PESSOA;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_SEQ";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = NU_SEQ;

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
                    else
                    {
                        DataContext.CommitTransaction();
                    }

                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "SetDeletarEmailDoAssinanteCentral (Delete) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }


    }
}
