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
    public class TelefoneAssinanteData
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

        public List<Central.Entity.TelsDoAssinanteCentralEntity> GetLerTelefoneCentral(int pintCD_CONTABIL_PESSOA)
        {
          try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCARTELSDOASSINANTE";

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
                            List<TelsDoAssinanteCentralEntity> lcolEnt = new List<TelsDoAssinanteCentralEntity>();
                            TelsDoAssinanteCentralEntity lEnt;

                            while (myReader.Read())
                            {
                                lEnt = new TelsDoAssinanteCentralEntity();

                                lEnt.CD_CONTABIL_PESSOA = Int32.Parse(myReader[0].ToString());
                                lEnt.ST_TP_TELEFONE = byte.Parse(myReader[1].ToString());
                                lEnt.NU_DDD = myReader.IsDBNull(2) ? short.Parse("0") : short.Parse(myReader[2].ToString());
                                lEnt.NU_TEL = myReader[3].ToString();
                                lEnt.DS_OBS = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                                lEnt.NU_DDI = myReader.IsDBNull(5) ? 0 : Int32.Parse(myReader[5].ToString());
                                lEnt.NU_SEQ = byte.Parse(myReader[6].ToString());
                                lEnt.NU_RAMAL = myReader.IsDBNull(7) ? "" : myReader[7].ToString();

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
                        _msgErro = "GetLerTelefoneCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public void SetIncluirTelefoneCentral(int? CD_CONTABIL_PESSOA,
                                               byte NU_SEQ, 
                                               byte? ST_TP_TELEFONE, 
                                               short? NU_DDD, 
                                               string NU_TEL, 
                                               string NU_RAMAL, 
                                               string DS_OBS, 
                                               int? NU_DDI,
                                               Boolean pbolAbrirTransacao,
                                               Boolean pbolFecharConexao)
        {
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
                    myComandoQuery.CommandText = "select isnull(sum(NU_SEQ),0) as NU_SEQ  from TELEFONE_ASSINANTE where CD_CONTABIL_PESSOA = " + CD_CONTABIL_PESSOA;

                    DbDataReader myReaderQuery = DataContext.ExecutarReader(myComandoQuery);

                    if (DataContext.Erro == 0)
                    {
                        if (myReaderQuery.HasRows)
                        {
                            myReaderQuery.Read();

                            lintSEQ = myReaderQuery.IsDBNull(0) ? 0 :myReaderQuery.GetInt32(0);
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
                    myComando.CommandText = "WSP_INSERIRTELEFONEASSINANTE";

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
                    myParametro.ParameterName = "ST_TP_TELEFONE";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int16;
                    myParametro.Value = (ST_TP_TELEFONE != null ? ST_TP_TELEFONE : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_DDD";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (NU_DDD != null ? NU_DDD : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_TEL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NU_TEL != null ? NU_TEL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_RAMAL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NU_RAMAL != null ? NU_RAMAL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_OBS";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (DS_OBS != null ? DS_OBS : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_DDI";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (NU_DDI != null ? NU_DDI : (object)DBNull.Value);

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
                        _msgErro = "SetIncluirTelefoneCentral (Insert) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }
        public void SetAlterarTelefoneCentral(int CD_CONTABIL_PESSOA, 
                                             byte NU_SEQ, 
                                             byte? ST_TP_TELEFONE, 
                                             short? NU_DDD, 
                                             string NU_TEL, 
                                             string NU_RAMAL, 
                                             string DS_OBS, 
                                             int? NU_DDI,
                                             Boolean pbolAbrirTransacao,
                                             Boolean pbolFecharConexao)
        {

            //CD_CONTABIL_PESSOA, NU_SEQ, ST_TP_TELEFONE, NU_DDD, NU_TEL, NU_RAMAL, DS_OBS, NU_DDI

            try
            {
                if (DataContext.AbrirConexao())
                {
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
                    myParametro.ParameterName = "ST_TP_TELEFONE";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int16;
                    myParametro.Value = (ST_TP_TELEFONE != null ? ST_TP_TELEFONE : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_DDD";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (NU_DDD != null ? NU_DDD : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_TEL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NU_TEL != null ? NU_TEL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_RAMAL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NU_RAMAL != null ? NU_RAMAL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_OBS";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (DS_OBS != null ? DS_OBS : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_DDI";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (NU_DDI != null ? NU_DDI : (object)DBNull.Value);

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
                        _msgErro = "SetAlterarTelefoneCentral (UpDate) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public void SetDeletarTelefoneCentral(int? CD_CONTABIL_PESSOA,
                                              byte NU_SEQ,
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
                    myComando.CommandText = "WSP_DELETARTELEFONEASSINANTE";

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
                        _msgErro = "SetDeletarTelefoneCentral (Delete) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }
    }
}
