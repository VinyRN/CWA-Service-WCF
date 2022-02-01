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
    public class AfinidadeData
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

        public List<Central.Entity.AfinidadeAssinanteCentralEntity> GetLerAfinidadeAssinanteCentral(int pintCD_CONTABIL_PESSOA)
        {
            try
            {
                //exec WSP_BUSCARAFINIDADEASSINANTE @CD_CONTABIL_PESSOA = {0}", CD_CONTABIL_PESSOA
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCARAFINIDADEASSINANTE";

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
                            List<AfinidadeAssinanteCentralEntity> lcolEnt = new List<AfinidadeAssinanteCentralEntity>();
                            AfinidadeAssinanteCentralEntity lEnt;

                            while (myReader.Read())
                            {
                                lEnt = new AfinidadeAssinanteCentralEntity();

                                lEnt.CD_CONTABIL_PESSOA = Int32.Parse(myReader[0].ToString());
                                lEnt.CD_GRUPO_AFINIDADE = Int16.Parse(myReader[1].ToString());
                                lEnt.DS_GRUPO_AFINIDADE = myReader.IsDBNull(2) ? "" : myReader[2].ToString();

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
                        _msgErro = "GetLerAfinidadeAssinanteCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }


        public List<Central.Entity.TipoAfinidadeCentralEntity> GetLerTipoAfinidadeCentral()
        {
            try
            {
                //exec WSP_BUSCARATIPOAFINIDADE
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCARATIPOAFINIDADE";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<TipoAfinidadeCentralEntity> lcolEnt = new List<TipoAfinidadeCentralEntity>();
                            TipoAfinidadeCentralEntity lEnt;

                            while (myReader.Read())
                            {
                                lEnt = new TipoAfinidadeCentralEntity();

                                lEnt.CD_TP_AFINIDADE = Int32.Parse(myReader[0].ToString());
                                lEnt.DS_TP_AFINIDADE = myReader.IsDBNull(1) ? "" : myReader[1].ToString();

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
                        _msgErro = "GetLerTipoAfinidadeCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }


        public void SetIncluirAfinidadeAssinanteCentral(int CD_CONTABIL_PESSOA, 
                                                        int CD_TP_AFINIDADE, 
                                                        int CD_GRUPO_AFINIDADE,
                                                        Boolean pbolAbrirTransacao,
                                                        Boolean pbolFecharConexao)
        {
            try
            {
                //exec WSP_INSERIRAFINIDADEASSINANTE @CD_CONTABIL_PESSOA ={0},@CD_TP_AFINIDADE = {1},@CD_GRUPO_AFINIDADE ={2}", CD_CONTABIL_PESSOA, CD_TP_AFINIDADE, CD_GRUPO_AFINIDADE

                if (DataContext.AbrirConexao())
                {
                    
                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_INSERIRAFINIDADEASSINANTE";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = CD_CONTABIL_PESSOA;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_TP_AFINIDADE";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = CD_TP_AFINIDADE;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_GRUPO_AFINIDADE";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = CD_GRUPO_AFINIDADE;

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
                        _msgErro = "SetIncluirAfinidadeAssinante (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }


        public void SetDeletarAfinidadeAssinanteCentral(int CD_CONTABIL_PESSOA,
                                                        int CD_TP_AFINIDADE,
                                                        int CD_GRUPO_AFINIDADE,
                                                        Boolean pbolAbrirTransacao,
                                                        Boolean pbolFecharConexao)
        {
            try
            {
                //exec WSP_DELETARAFINIDADEASSINANTE @CD_CONTABIL_PESSOA ={0},@CD_TP_AFINIDADE = {1},@CD_GRUPO_AFINIDADE ={2}", CD_CONTABIL_PESSOA, CD_TP_AFINIDADE, CD_GRUPO_AFINIDADE

                if (DataContext.AbrirConexao())
                {

                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_DELETARAFINIDADEASSINANTE";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = CD_CONTABIL_PESSOA;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_TP_AFINIDADE";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = CD_TP_AFINIDADE;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_GRUPO_AFINIDADE";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = CD_GRUPO_AFINIDADE;

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
                        _msgErro = "SetDeletarAfinidadeAssinanteCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

     }
}
