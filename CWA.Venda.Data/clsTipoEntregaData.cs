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
    public class TipoEntregaData
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

        public List<TipoEntregaEntity> GetTipoEntregaEntityList(int pintIDProduto, int pintIDCampanha, int pintIDTipoAssinatura)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_TIPO_ENTREGA";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PRODUTO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDProduto;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_CAMPANHA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDCampanha;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_TP_ASSINATURA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDTipoAssinatura;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);


                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<TipoEntregaEntity> lcolEnt = new List<TipoEntregaEntity>();
                            TipoEntregaEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new TipoEntregaEntity();

                                lEnt.CD_TIPO_ENTREGA = int.Parse(myReader[0].ToString());
                                lEnt.DS_TIPO_ENTREGA = myReader[1].ToString();

                                lcolEnt.Add(lEnt);
                            }

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
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
                _msgErro = "TipoEntregaData (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "TipoEntregaData (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public List<TipoEntregaEntity> GetTipoEntregaEntityList()
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCAR_TIPO_ENTREGA";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);


                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<TipoEntregaEntity> lcolEnt = new List<TipoEntregaEntity>();
                            TipoEntregaEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new TipoEntregaEntity();

                                lEnt.CD_TIPO_ENTREGA = int.Parse(myReader[0].ToString());
                                lEnt.DS_TIPO_ENTREGA = myReader[1].ToString();
                                lEnt.ST_ENTREGA_SEG = int.Parse(myReader[2].ToString());
                                lEnt.ST_ENTREGA_TER = int.Parse(myReader[3].ToString());
                                lEnt.ST_ENTREGA_QUA = int.Parse(myReader[4].ToString());
                                lEnt.ST_ENTREGA_QUI = int.Parse(myReader[5].ToString());
                                lEnt.ST_ENTREGA_SEX = int.Parse(myReader[6].ToString());
                                lEnt.ST_ENTREGA_SAB = int.Parse(myReader[7].ToString());
                                lEnt.ST_ENTREGA_DOM = int.Parse(myReader[8].ToString());

                                lcolEnt.Add(lEnt);
                            }

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
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
                _msgErro = "TipoEntregaData (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "TipoEntregaData (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }
    }
}
