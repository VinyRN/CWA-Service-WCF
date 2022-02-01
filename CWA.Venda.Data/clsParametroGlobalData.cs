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
    public class ParametroGlobalData
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

        public void SetParametroGlobalWEB(ParametroGlobalEntity ObjEnt, Boolean pbolAbrirTransacao, Boolean pbolFecharConexao)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.StoredProcedure;
                myComando.CommandText = "WS_VENDA_PARAMETRO_GLOBAL_WEB_I_01";

                AddParms(ref myComando, ObjEnt, "I");

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
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "SetParametroGlobalWEB - " + ex.Message;
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
                            _msgErro = "SetParametroGlobalWEB - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }
        public void UpdParametroGlobalWEB(ParametroGlobalEntity ObjEnt, Boolean pbolAbrirTransacao, Boolean pbolFecharConexao)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.StoredProcedure;
                myComando.CommandText = "WS_VENDA_PARAMETRO_GLOBAL_WEB_U_01";

                AddParms(ref myComando, ObjEnt, "U");

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
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "UpdParametroGlobalWEB - " + ex.Message;
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
                            _msgErro = "UpdParametroGlobalWEB - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }
        private void AddParms(ref DbCommand pdbCommand, ParametroGlobalEntity ObjEnt, string pstrOpcao)
        {
            DbParameter ldbParameter;

            if (pstrOpcao != "I")
            {
                ldbParameter = pdbCommand.CreateParameter();
                ldbParameter.ParameterName = "P_ID_PARAMETRO";
                ldbParameter.Direction = ParameterDirection.Input;
                ldbParameter.DbType = DbType.Int32;
                ldbParameter.Value = ObjEnt.ID_PARAMETRO;

                pdbCommand.Parameters.Add(ldbParameter);
            }

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_NU_TEL_CONTATO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjEnt.NU_TEL_CONTATO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_MSG_ERRO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjEnt.DS_MSG_ERRO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "P_DS_MSG_SUCESSO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjEnt.DS_MSG_SUCESSO;

            pdbCommand.Parameters.Add(ldbParameter);

        }
        public ParametroGlobalEntity GetParametroGlobalWEB()
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_PARAMETRO_GLOBAL_WEB";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        ParametroGlobalEntity lEnt = new ParametroGlobalEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.ID_PARAMETRO = myReader.IsDBNull(0) ? 0 : int.Parse(myReader[0].ToString());

                            lEnt.NU_TEL_CONTATO = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                            lEnt.DS_MSG_ERRO = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                            lEnt.DS_MSG_SUCESSO = myReader.IsDBNull(3) ? "" : myReader[3].ToString();

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
                        _msgErro = "GetParametroGlobalWEB (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }
        public ParametroGlobalEDEntity GetParametroGlobalED()
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCAR_PARAM_GLOBAL_ED";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        ParametroGlobalEDEntity lEnt = new ParametroGlobalEDEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.ST_IND_REQUER_CARTAO = myReader.IsDBNull(0) ? 0 : int.Parse(myReader[0].ToString());
                            lEnt.CD_LOGR_PADRAO_BRASIL = myReader.IsDBNull(1) ? 0 : int.Parse(myReader[1].ToString());
                            lEnt.CD_LOGR_PADRAO_EXTERIOR = myReader.IsDBNull(2) ? 0 : int.Parse(myReader[2].ToString());

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
                _msgErro = "GetParametroGlobalEDData - " + ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GetParametroGlobalED - Erro ao fechar conexão com o banco de dados.";
                    }
                }

            }
        }

        public ParametroGlobalEDEntity GetParametroGlobalEDSQL()
        {
            try
            {
                using (SqlCommand myComando = new SqlCommand())
                {
                    if (DataContextSQL.AbrirConexao())
                    {
                        myComando.Connection = DataContextSQL.GetConnection;
                        myComando.CommandType = CommandType.StoredProcedure;
                        myComando.CommandText = "WS_BUSCAR_PARAM_GLOBAL_ED";

                        SqlDataReader myReader = DataContextSQL.ExecutarReader(myComando);

                        if (DataContextSQL.Erro == 0)
                        {
                            ParametroGlobalEDEntity lEnt = new ParametroGlobalEDEntity();

                            if (myReader.HasRows)
                            {

                                myReader.Read();

                                lEnt.ST_IND_REQUER_CARTAO = myReader.IsDBNull(0) ? 0 : int.Parse(myReader[0].ToString());
                                lEnt.CD_LOGR_PADRAO_BRASIL = myReader.IsDBNull(1) ? 0 : int.Parse(myReader[1].ToString());
                                lEnt.CD_LOGR_PADRAO_EXTERIOR = myReader.IsDBNull(2) ? 0 : int.Parse(myReader[2].ToString());

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

                            _erro = DataContextSQL.Erro;
                            _msgErro = DataContextSQL.MsgErro;

                            return null;
                        }
                    }
                    else
                    {
                        _erro = DataContextSQL.Erro;
                        _msgErro = DataContextSQL.MsgErro;

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GetParametroGlobalEDDataSQL - " + ex.Message;
                return null;
            }
            finally
            {
                if (DataContextSQL.ConexaoAberta())
                {
                    if (!DataContextSQL.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GetParametroGlobalEDDataSQL - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }
    }
}
