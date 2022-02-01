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
    public class AbandonoData
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

        public void SetAbandono(AbandonoEntity ObjEnt, Boolean pbolAbrirTransacao, Boolean pbolFecharConexao)
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
                    myComando.CommandText = "WS_VENDA_ABANDONO_WEB";

                    AddParms(ref myComando, ObjEnt, "I");

                    DataContext.ExecutarComando(myComando);

                    if (DataContext.Erro != 0)
                    {
                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        if (DataContext.ConexaoAberta())
                        {
                            DataContext.RollbackTransaction();
                        }
                    }
                    else
                    {
                        if (DataContext.ConexaoAberta())
                        {
                            DataContext.CommitTransaction();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "SetAbandono - " + ex.Message;
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
                            _msgErro = "SetAbandono - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        private void AddParms(ref DbCommand pdbCommand, AbandonoEntity ObjEnt, string pstrOpcao)
        {
            DbParameter ldbParameter;

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_DOC";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjEnt.NU_DOC;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_DS_EMAIL";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjEnt.DS_EMAIL;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_DS_REG_PESSOA";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_REG_PESSOA != null)
            {
                ldbParameter.Value = ObjEnt.DS_REG_PESSOA;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_DS_REG_ENDER";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if ( ObjEnt.DS_REG_ENDER != null)
            {
                ldbParameter.Value = ObjEnt.DS_REG_ENDER;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_SERIE_CTR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.NU_SERIE_CTR != null)
            {
                ldbParameter.Value = ObjEnt.NU_SERIE_CTR;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_CTR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.NU_CTR != null)
            {
                ldbParameter.Value = ObjEnt.NU_CTR;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_DV_CTR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            if (ObjEnt.NU_DV_CTR != null)
            {
                ldbParameter.Value = ObjEnt.NU_DV_CTR;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_TP_ETAPA";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.TP_ETAPA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_ST_STATUS";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.ST_STATUS;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_DS_REG_VENDA";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjEnt.DS_REG_VENDA != null)
            {
                ldbParameter.Value = ObjEnt.DS_REG_VENDA;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }


            pdbCommand.Parameters.Add(ldbParameter);

        }

        public AbandonoEntity GetAbandono(string pstrDOC)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_ABANDONO_WEB";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_DOC";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String ;
                    myParametro.Value = pstrDOC;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        AbandonoEntity lEnt = new AbandonoEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.ID_LOG = int.Parse(myReader[0].ToString());
                            lEnt.DT_LOG = DateTime.Parse(myReader[1].ToString());
                            lEnt.HR_LOG = myReader[2].ToString();
                            lEnt.NU_DOC = myReader[4].ToString();
                            lEnt.DS_EMAIL = myReader[5].ToString();

                            lEnt.DS_REG_PESSOA = myReader.IsDBNull(6)? "" : myReader[6].ToString();
                            lEnt.DS_REG_ENDER = myReader.IsDBNull(7) ? "" : myReader[7].ToString();
                            lEnt.NU_SERIE_CTR = myReader.IsDBNull(8) ? "" : myReader[8].ToString();

                            lEnt.NU_CTR = myReader.IsDBNull(9) ? 0 : int.Parse(myReader[9].ToString());
                            lEnt.NU_DV_CTR = myReader.IsDBNull(10) ? 0 : int.Parse(myReader[10].ToString());
                            lEnt.TP_ETAPA = myReader.IsDBNull(11) ? 0 : int.Parse(myReader[11].ToString());
                            lEnt.ST_STATUS = myReader.IsDBNull(12) ? 0 : int.Parse(myReader[12].ToString());

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
                        _msgErro = "GetAbandono (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public List<AbandonoEntity> GetAbandonoMKT()
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_ABANDONO_MKT";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<AbandonoEntity> lcolEnt = new List<AbandonoEntity>();
                            AbandonoEntity lEnt;

                            while (myReader.Read())
                            {
                                lEnt = new AbandonoEntity();

                                lEnt.ID_LOG = int.Parse(myReader[0].ToString());
                                lEnt.DT_LOG = DateTime.Parse(myReader[1].ToString());
                                lEnt.HR_LOG = myReader[2].ToString();
                                lEnt.NU_DOC = myReader[4].ToString();
                                lEnt.DS_EMAIL = myReader[5].ToString();

                                lEnt.DS_REG_PESSOA = myReader.IsDBNull(6) ? "" : myReader[6].ToString();
                                lEnt.DS_REG_ENDER = myReader.IsDBNull(7) ? "" : myReader[7].ToString();
                                lEnt.NU_SERIE_CTR = myReader.IsDBNull(8) ? "" : myReader[8].ToString();

                                lEnt.NU_CTR = myReader.IsDBNull(9) ? 0 : int.Parse(myReader[9].ToString());
                                lEnt.NU_DV_CTR = myReader.IsDBNull(10) ? 0 : int.Parse(myReader[10].ToString());
                                lEnt.TP_ETAPA = myReader.IsDBNull(11) ? 0 : int.Parse(myReader[11].ToString());
                                lEnt.ST_STATUS = myReader.IsDBNull(12) ? 0 : int.Parse(myReader[12].ToString());

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
                        _msgErro = "GetAbandonoMKT (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public void SetAbandonoMKT(int pintIDLog, Boolean pbolAbrirTransacao, Boolean pbolFecharConexao)
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
                    myComando.CommandText = "WS_VENDA_ABANDONO_WEB";


                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "@SP_ID_LOG";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDLog;

                    myComando.Parameters.Add(myParametro);


                    DataContext.ExecutarComando(myComando);

                    if (DataContext.Erro != 0)
                    {
                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        if (DataContext.ConexaoAberta())
                        {
                            DataContext.RollbackTransaction();
                        }
                    }
                    else
                    {
                        if (DataContext.ConexaoAberta())
                        {
                            DataContext.CommitTransaction();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "SetAbandonoMKT - " + ex.Message;
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
                            _msgErro = "SetAbandonoMKT - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }
    }


}
