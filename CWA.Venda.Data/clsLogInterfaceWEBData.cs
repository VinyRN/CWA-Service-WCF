using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

using System.Data.SqlClient;

using CWA.Venda.Entity;

namespace CWA.Venda.Data
{
    public class LogInterfaceWEBData
    {
        private System.Int32 _erro;
        private string _msgErro;

        private int _IDLogRastreamento;

        public System.Int32 Erro
        {
            get { return _erro; }
        }

        public string MsgErro
        {
            get { return _msgErro; }
        }

        public int IDLogRastreamento
        {
            get { return _IDLogRastreamento; }
        }


        public void SetLogInterfaceWEB(LogInterfaceWEBEntity  ObjEnt,
                                       Boolean pbolAbrirTransacao,
                                       Boolean pbolFecharConexao)
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

                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_LOG_INTERFACE_WEB_I_01";

                    AddParmsLogInterfaceWEB(ref myComando, ObjEnt);

                    DataContext.ExecutarComando(myComando);

                    if (DataContext.Erro != 0)
                    {
                        if (pbolAbrirTransacao)
                        {
                            DataContext.RollbackTransaction();
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        _IDLogRastreamento = 0;
                    }
                    else
                    {
                        if (pbolAbrirTransacao)
                        {
                            DataContext.CommitTransaction();
                        }

                        _IDLogRastreamento = int.Parse(myComando.Parameters["P_ID_LOG"].Value.ToString());
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
                _msgErro = "LogInterfaceWEBData (SetLogInterfaceWEB) - " + ex.Message;
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
                            _msgErro = "LogInterfaceWEBData (SetLogInterfaceWEB) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }


        }


        public void SetUpdLogInterfaceWEB(string pstrRetGateway,
                                          string pstrError,
                                          Int32 pintIDLog, 
                                          Boolean pbolAbrirTransacao,
                                          Boolean pbolFecharConexao)
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

                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_LOG_INTERFACE_WEB_U_01";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "P_DS_OUTPUT_GATEWAY";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    if (pstrRetGateway == "")
                    {
                        myParametro.Value = DBNull.Value;
                    }
                    else
                    {
                        myParametro.Value = pstrRetGateway;
                    }
                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "P_DS_ERRO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    if (pstrError == "")
                    {
                        myParametro.Value = DBNull.Value;
                    }
                    else
                    {
                        myParametro.Value = pstrError;
                    }
                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "P_ID_LOG";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDLog;

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
                        if (pbolAbrirTransacao)
                        {
                            DataContext.CommitTransaction();
                        }
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
                _msgErro = "LogInterfaceWEBData (SetLogInterfaceWEB) - " + ex.Message;
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
                            _msgErro = "LogInterfaceWEBData (SetLogInterfaceWEB) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }


        }

        private void AddParmsLogInterfaceWEB(ref DbCommand pdbCommand,
                                            LogInterfaceWEBEntity  ObjEnt)
        {
            DbParameter ldbParameter;
           
            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_INPUT_GATEWAY";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_INPUT_GATEWAY == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_INPUT_GATEWAY;
            }
            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_METODO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_METODO == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_METODO;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_OBS";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_OBS  == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_OBS;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_OUTPUT_GATEWAY";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_OUTPUT_GATEWAY  == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_OUTPUT_GATEWAY;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_REG_ENDER";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_REG_ENDER  == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_REG_ENDER;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_REG_PESSOA";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_REG_PESSOA  == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_REG_PESSOA;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_REG_PGTO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_REG_PGTO  == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_REG_PGTO;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_URL";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_URL == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_URL;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DT_LOG";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            //ldbParameter.Value = ObjEnt.DT_LOG.ToString("dd/MM/yyyy");
            ldbParameter.Value = ObjEnt.DT_LOG;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_HR_LOG";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjEnt.HR_LOG;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_TP_LOG";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.TP_LOG;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_CHAMADOR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjEnt.DS_CHAMADOR;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_NU_SERIE_CTR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.NU_SERIE_CTR == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.NU_SERIE_CTR;
            }          
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_NU_CTR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.NU_CTR;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_NU_DV_CTR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.NU_DV_CTR;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_ERRO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_ERRO == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_ERRO;
            }           
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_ID_LOG";
            ldbParameter.Direction = ParameterDirection.Output;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.ID_LOG;
            pdbCommand.Parameters.Add(ldbParameter);
        }

        public LogInterfaceWEBEntity GetLogInterfaceWEB(int pintIDLog)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_LOG_INTERFACE_WEB_SEL_01";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_ID_LOG";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDLog;

                    myComando.Parameters.Add(myParametro);


                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        LogInterfaceWEBEntity lEnt = new LogInterfaceWEBEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.ID_LOG = int.Parse(myReader[0].ToString());
                            //lEnt.DT_LOG = DateTime.Parse((myReader[1].ToString()));
                            lEnt.DT_LOG = (myReader[1].ToString());
                            lEnt.HR_LOG = myReader[2].ToString();
                            lEnt.TP_LOG = int.Parse(myReader[3].ToString());
                            lEnt.DS_CHAMADOR = myReader[4].ToString();
                            lEnt.DS_METODO = myReader[5].ToString();
                            lEnt.DS_URL = myReader[6].ToString();

                            lEnt.DS_REG_PESSOA = myReader[7].ToString();
                            lEnt.DS_REG_ENDER = myReader[8].ToString();
                            lEnt.DS_REG_PGTO = myReader[9].ToString();

                            lEnt.DS_INPUT_GATEWAY = myReader[10].ToString();
                            lEnt.DS_OUTPUT_GATEWAY = myReader[11].ToString();
                            lEnt.DS_OBS = myReader[12].ToString();

                            lEnt.NU_SERIE_CTR = myReader[13].ToString();
                            lEnt.NU_CTR = Int32.Parse(myReader[14].ToString());
                            lEnt.NU_DV_CTR = Int32.Parse(myReader[15].ToString());

                            lEnt.DS_ERRO = myReader[16].ToString();

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
                        _msgErro = "GetCampanha (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }


        }

        public void SetLogInterfaceWEBSQL(LogInterfaceWEBEntity ObjEnt,
                                          Boolean pbolAbrirTransacao,
                                          Boolean pbolFecharConexao)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                using (SqlCommand myComando = new SqlCommand())
                {
                    if (DataContextSQL.AbrirConexao())
                    {
                        if (pbolAbrirTransacao)
                        {
                            DataContextSQL.BeginTransaction();
                        }

                        myComando.Connection = DataContextSQL.GetConnection;

                        if (pbolAbrirTransacao)
                        {
                            myComando.Transaction = DataContextSQL.GetTransaction;
                        }

                        myComando.CommandType = CommandType.StoredProcedure;
                        myComando.CommandText = "WSP_LOG_INTERFACE_WEB_I_01";

                        SqlCommand myComandoParms = new SqlCommand();
                        myComandoParms = myComando;

                        AddParmsLogInterfaceWEBSQL(ref myComandoParms, ObjEnt);

                        DataContextSQL.ExecutarComando(myComandoParms);

                        if (DataContextSQL.Erro != 0)
                        {
                            if (pbolAbrirTransacao)
                            {
                                DataContextSQL.RollbackTransaction();
                            }

                            _erro = DataContextSQL.Erro;
                            _msgErro = DataContextSQL.MsgErro;

                            myComandoParms.Dispose();

                            _IDLogRastreamento = 0;
                        }
                        else
                        {
                            if (pbolAbrirTransacao)
                            {
                                DataContextSQL.CommitTransaction();
                            }

                            _IDLogRastreamento = int.Parse(myComandoParms.Parameters["P_ID_LOG"].Value.ToString());

                            myComandoParms.Dispose();
                        }
                    }
                    else
                    {
                        _erro = DataContextSQL.Erro;
                        _msgErro = DataContextSQL.MsgErro;

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "LogInterfaceWEBData (SetLogInterfaceWEB) - " + ex.Message;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContextSQL.ConexaoAberta())
                    {
                        if (!DataContextSQL.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "LogInterfaceWEBData (SetLogInterfaceWEB) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }


        }

        public void SetUpdLogInterfaceWEBSQL(string pstrRetGateway,
                                             string pstrError,
                                             Int32 pintIDLog,
                                             Boolean pbolAbrirTransacao,
                                             Boolean pbolFecharConexao)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                using (SqlCommand myComando = new SqlCommand())
                {
                    if (DataContextSQL.AbrirConexao())
                    {
                        if (pbolAbrirTransacao)
                        {
                            DataContextSQL.BeginTransaction();
                        }

                        myComando.Connection = DataContextSQL.GetConnection;
                        myComando.CommandType = CommandType.StoredProcedure;
                        myComando.CommandText = "WSP_LOG_INTERFACE_WEB_U_01";

                        SqlParameter myParametro;

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "P_DS_OUTPUT_GATEWAY";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.String;
                        if (pstrRetGateway == "")
                        {
                            myParametro.Value = DBNull.Value;
                        }
                        else
                        {
                            myParametro.Value = pstrRetGateway;
                        }
                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "P_DS_ERRO";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.String;
                        if (pstrError == "")
                        {
                            myParametro.Value = DBNull.Value;
                        }
                        else
                        {
                            myParametro.Value = pstrError;
                        }
                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "P_ID_LOG";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintIDLog;

                        myComando.Parameters.Add(myParametro);

                        DataContextSQL.ExecutarComando(myComando);

                        if (DataContextSQL.Erro != 0)
                        {
                            if (pbolAbrirTransacao)
                            {
                                DataContextSQL.RollbackTransaction();
                            }

                            _erro = DataContextSQL.Erro;
                            _msgErro = DataContextSQL.MsgErro;

                        }
                        else
                        {
                            if (pbolAbrirTransacao)
                            {
                                DataContextSQL.CommitTransaction();
                            }
                        }
                    }
                    else
                    {
                        _erro = DataContextSQL.Erro;
                        _msgErro = DataContextSQL.MsgErro;

                        return;
                    }
                }
            }

            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "LogInterfaceWEBData (SetLogInterfaceWEB) - " + ex.Message;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContextSQL.ConexaoAberta())
                    {
                        if (!DataContextSQL.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "LogInterfaceWEBData (SetLogInterfaceWEB) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }


        }

        private void AddParmsLogInterfaceWEBSQL(ref SqlCommand  pdbCommand,
                                                LogInterfaceWEBEntity ObjEnt)
        {
            SqlParameter  ldbParameter;

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_INPUT_GATEWAY";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_INPUT_GATEWAY == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_INPUT_GATEWAY;
            }
            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_METODO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_METODO == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_METODO;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_OBS";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_OBS == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_OBS;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_OUTPUT_GATEWAY";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_OUTPUT_GATEWAY == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_OUTPUT_GATEWAY;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_REG_ENDER";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_REG_ENDER == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_REG_ENDER;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_REG_PESSOA";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_REG_PESSOA == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_REG_PESSOA;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_REG_PGTO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_REG_PGTO == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_REG_PGTO;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_URL";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_URL == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_URL;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DT_LOG";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            //ldbParameter.Value = ObjEnt.DT_LOG.ToString("dd/MM/yyyy");
            ldbParameter.Value = ObjEnt.DT_LOG;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_HR_LOG";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjEnt.HR_LOG;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_TP_LOG";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.TP_LOG;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_CHAMADOR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjEnt.DS_CHAMADOR;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_NU_SERIE_CTR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.NU_SERIE_CTR == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.NU_SERIE_CTR;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_NU_CTR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.NU_CTR;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_NU_DV_CTR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.NU_DV_CTR;
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_ERRO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_ERRO == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = ObjEnt.DS_ERRO;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_ID_LOG";
            ldbParameter.Direction = ParameterDirection.Output;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.ID_LOG;
            pdbCommand.Parameters.Add(ldbParameter);
        }
    }

}
