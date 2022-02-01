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
    public class RedeSocialData
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

        public List<Central.Entity.RedeSocialCentralEntity> GetLerRedeSocialCentral(int? pintCD_CONTABIL_PESSOA)
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
                    myComando.CommandText = "WSP_BUSCAREDESOCIAL";

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
                            List<RedeSocialCentralEntity> lcolEnt = new List<RedeSocialCentralEntity>();
                            RedeSocialCentralEntity lEnt;

                            while (myReader.Read())
                            {
                                lEnt = new RedeSocialCentralEntity();

                                lEnt.CD_CONTABIL_PESSOA = Int32.Parse(myReader[0].ToString());
                                lEnt.NU_SEQ = byte.Parse(myReader[1].ToString());
                                lEnt.DS_REDE_SOCIAL = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                                lEnt.ST_SITUACAO = byte.Parse(myReader[3].ToString());
                                lEnt.DS_EMAIL = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                                lEnt.CD_TIPO_REDE_SOCIAL = short.Parse(myReader[5].ToString());
                                lEnt.DS_USUARIO = myReader.IsDBNull(6) ? "" : myReader[6].ToString();
                                lEnt.ID_TOKEN = myReader.IsDBNull(7) ? "" : myReader[7].ToString();

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

                            List<RedeSocialCentralEntity> lcolEnt = new List<RedeSocialCentralEntity>();
                            return lcolEnt;
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
                        _msgErro = "GetLerRedeSocialCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public void SetIncluirRedeSocialCentral(int? CD_CONTABIL_PESSOA, 
                                                byte NU_SEQ, 
                                                string DS_REDE_SOCIAL, 
                                                string DS_EMAIL, 
                                                int CD_TIPO_REDE_SOCIAL, 
                                                string DS_USUARIO, 
                                                string ID_TOKEN,
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
                    myComandoQuery.CommandText = "select isnull(sum(NU_SEQ),0) as NU_SEQ  from REDE_SOCIAL_ASSINANTE where CD_CONTABIL_PESSOA = " + CD_CONTABIL_PESSOA;

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

                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_INSERIRREDESOCIALASSINANTE";

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
                    myParametro.ParameterName = "DS_REDE_SOCIAL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (DS_REDE_SOCIAL != null ? DS_REDE_SOCIAL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (DS_EMAIL != null ? DS_EMAIL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_TIPO_REDE_SOCIAL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int16;
                    myParametro.Value = CD_TIPO_REDE_SOCIAL;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_USUARIO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (DS_USUARIO != null ? DS_USUARIO : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "ID_TOKEN";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (ID_TOKEN != null ? ID_TOKEN : (object)DBNull.Value);

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
                        _msgErro = "SetIncluirRedeSocialCentral (Insert) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public void SetAlterarRedeSocialCentral(int? CD_CONTABIL_PESSOA,
                                                byte NU_SEQ,
                                                string DS_REDE_SOCIAL,
                                                string DS_EMAIL,
                                                int CD_TIPO_REDE_SOCIAL,
                                                string DS_USUARIO,
                                                string ID_TOKEN,
                                                Boolean pbolAbrirTransacao,
                                                Boolean pbolFecharConexao)
        {
            try
            {
                if (DataContext.AbrirConexao())
                {

                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_ALTERARREDESOCIALASSINANTE";

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
                    myParametro.ParameterName = "DS_REDE_SOCIAL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (DS_REDE_SOCIAL != null ? DS_REDE_SOCIAL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (DS_EMAIL != null ? DS_EMAIL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_TIPO_REDE_SOCIAL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int16;
                    myParametro.Value = CD_TIPO_REDE_SOCIAL;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_USUARIO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (DS_USUARIO != null ? DS_USUARIO : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "ID_TOKEN";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (ID_TOKEN != null ? ID_TOKEN : (object)DBNull.Value);

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
                        _msgErro = "SetAlterarRedeSocialCentral (Insert) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public void SetDeletarRedeSocialCentral(int CD_CONTABIL_PESSOA, byte NU_SEQ,
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
                    myComando.CommandText = "WSP_DELETARREDESOCIALASSINANTE";

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
                        _msgErro = "SetDeletarRedeSocialCentral (Delete) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

    }
}
