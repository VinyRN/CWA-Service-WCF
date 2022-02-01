using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using CWA.Util;

namespace CWA.Venda.Data
{
    public class DataContextSQL
    {
        private static System.Int32 _erro;
        private static string _msgErro;

        private static string _ConectString;
        private static SqlConnection _Connection;
        private static SqlTransaction  _Transaction;

        public static System.Int32 Erro
        {
            protected set { }
            get { return _erro; }
        }

        public static string MsgErro
        {
            protected set { }
            get { return _msgErro; }
        }

        public static SqlConnection GetConnection
        {
            protected set { }
            get { return _Connection; }

        }

        public static SqlTransaction GetTransaction
        {
            protected set { }
            get { return _Transaction; }

        }

        private static string ObterConectionString(string pstrAmbiente, string[] pstrVetorConVB6 = null)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                string lstrUser = "";
                string lstrPwd = "";
                string lstrBD = "";
                string lstrServer = "";

                if ((pstrVetorConVB6 != null) && (pstrVetorConVB6.Length > 0))
                {
                    //============================================================================================
                    // 0      1        2              3                      4                   5
                    //PRD,CIR2000,VSSMJOSCWA,CIR2000_PADRAO_MIRANTE,192.168.0.80\\SQL2008R2,System.Data.OleDb
                    //============================================================================================

                    lstrUser = pstrVetorConVB6[1];
                    lstrPwd = pstrVetorConVB6[2].ToString().Replace("|@|", ",");
                    lstrBD = pstrVetorConVB6[3];
                    lstrServer = pstrVetorConVB6[4];

                    _ConectString = "Provider=SQLOLEDB;Password=@PWD;User ID=@USER;Initial Catalog=@BD;Data Source=@SERVER;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;";

                }
                else
                {
                    Utility ObjUtil = new Utility();

                    lstrUser = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["USER"]);
                    lstrPwd = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["PWD"]);
                    lstrBD = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["BD"]);
                    lstrServer = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["SERVER"]);

                    _ConectString = ConfigurationManager.ConnectionStrings["Conexao" + pstrAmbiente].ConnectionString.ToString();
                }

                _ConectString = _ConectString.Replace("@PWD", lstrPwd);
                _ConectString = _ConectString.Replace("@USER", lstrUser);
                _ConectString = _ConectString.Replace("@BD", lstrBD);
                _ConectString = _ConectString.Replace("@SERVER", lstrServer);

                Utility.SetLogXML("ObterConectionString", "Erro", _ConectString, false, 1);
                return _ConectString;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "ObterConectionString - " + ex.Message;
                return "";
            }
        }
        private static void CriarConexao(string[] pstrVetorConVB6 = null)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if ((pstrVetorConVB6 != null) && (pstrVetorConVB6.Length > 0))
                {
                    string lstrAmbeinte = pstrVetorConVB6[0];

                    _ConectString = ObterConectionString(lstrAmbeinte, pstrVetorConVB6);

                }
                else
                {
                    string lstrAmbeinte = ConfigurationManager.AppSettings["Ambiente"];

                    _ConectString = ObterConectionString(lstrAmbeinte);

                }

                _Connection = new SqlConnection(); 
                _Connection.ConnectionString = _ConectString;


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "CriarConexao - " + ex.Message;
                return;
            }
        }
        public static Boolean AbrirConexao(string[] pstrVetorConVB6 = null)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                CriarConexao(pstrVetorConVB6);
                if (_erro == 0)
                {
                    _erro = 0;
                    _msgErro = "";

                    if (_Connection != null)
                    {
                        if (_Connection.State == ConnectionState.Closed)
                        {
                            _Connection.Open();
                        }

                    }
                    else
                    {
                        _Connection.Open();
                    }

                    return true;

                }
                else
                {
                    Utility.SetLogXML("Conexao", "Erro", _msgErro, false, 1);
                    return false;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "AbrirConexao - " + ex.Message;
                Utility.SetLogXML("Conexao", "Erro", _msgErro, false, 1);
                return false;
            }
        }
        public static Boolean ConexaoAberta()
        {
            if (_Connection.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean FecharConexao()
        {
            try
            {
                if (_Connection.State != ConnectionState.Closed)
                {
                    _Connection.Dispose();
                    _Connection.Close();
                }
                else
                {
                    _Connection.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "FecharConexao - " + ex.Message;

                return false;
            }
        }
        public static void BeginTransaction()
        {
            try
            {
                if (_Connection.State == ConnectionState.Open)
                {
                    _Transaction = _Connection.BeginTransaction();
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Conexão não está aberta.";
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "BeginTransaction - " + ex.Message;
            }
        }
        public static void CommitTransaction()
        {
            try
            {
                if (_Connection.State == ConnectionState.Open)
                {
                    _Transaction.Commit();
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Conexão não está aberta.";
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "CommitTransaction - " + ex.Message;
            }
        }
        public static void RollbackTransaction()
        {
            try
            {
                if (_Connection.State == ConnectionState.Open)
                {
                    _Transaction.Rollback();
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Conexão não está aberta.";
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "RollbackTransaction - " + ex.Message;
            }
        }

        public static SqlCommand CriarComando(Boolean pbolAbrirTransacao)
        {
            try
            {
                if (_Connection.State == ConnectionState.Open)
                {
                    SqlCommand  myComando = _Connection.CreateCommand();

                    if (pbolAbrirTransacao)
                    {
                        myComando.Transaction = _Transaction;
                    }
                    return myComando;
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Conexão não está aberta.";

                    _Connection = null;

                    return null;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "CriarComando - " + ex.Message;

                return null;
            }
        }
        public static SqlDataReader ExecutarReader(SqlCommand  PmyCommand)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (_Connection.State == ConnectionState.Open)
                {
                    PmyCommand.Connection = _Connection;

                    SqlDataReader myReader = PmyCommand.ExecuteReader(CommandBehavior.CloseConnection);

                    return myReader;
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Conexão não está aberta.";

                    return null;
                }
            }

            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "ExecutarReader - " + ex.Message;

                return null;
            }
        }
        public static void ExecutarComando(SqlCommand PmyCommand)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                PmyCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "ExecutarComando - " + ex.Message;
            }
        }
    }
}
