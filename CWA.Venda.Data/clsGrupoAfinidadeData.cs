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
    public class GrupoAfinidadeData
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

        public List<Central.Entity.GrupoAfinidadeCentralEntity> GetLerGrupoAfinidadeCentral()
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCARGRUPOAFINIDADE";
                                    

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<GrupoAfinidadeCentralEntity> lcolEnt = new List<GrupoAfinidadeCentralEntity>();
                            GrupoAfinidadeCentralEntity lEnt;

                            while (myReader.Read())
                            {
                                lEnt = new GrupoAfinidadeCentralEntity();

                                lEnt.DS_GRUPO_AFINIDADE = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                                lEnt.CD_GRUPO_AFINIDADE = Int16.Parse(myReader[1].ToString());
                                

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
                        _msgErro = "GetLerGrupoAfinidadeCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public List<Central.Entity.GrupoAfinidadeCentralEntity> GetLerGrupoAfinidadeCentral(int pintTipo)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCARGRUPOAFINIDADETIPO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "P_CD_TP_AFINIDADE";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintTipo;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<GrupoAfinidadeCentralEntity> lcolEnt = new List<GrupoAfinidadeCentralEntity>();
                            GrupoAfinidadeCentralEntity lEnt;

                            while (myReader.Read())
                            {
                                lEnt = new GrupoAfinidadeCentralEntity();

                                lEnt.DS_GRUPO_AFINIDADE = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                                lEnt.CD_GRUPO_AFINIDADE = Int16.Parse(myReader[1].ToString());


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
                        _msgErro = "GetLerGrupoAfinidadeCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

    }
}
