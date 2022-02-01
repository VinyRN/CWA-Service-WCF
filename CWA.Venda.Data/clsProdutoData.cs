using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

using CWA.Venda.Entity;

namespace CWA.Venda.Data
{
    public class ProdutoData
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

        public List<ProdutoEntity> GetProdutoAgregado()
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCA_PRODUTO_AGREGADO";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<ProdutoEntity> lcolEnt = new List<ProdutoEntity>();
                            ProdutoEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new ProdutoEntity();

                                lEnt.CD_PRODUTO = int.Parse(myReader[0].ToString());
                                lEnt.NM_PRODUTO = myReader[1].ToString();

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
                _msgErro = "ProdutoAgregadoData (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "ProdutoAgregadoData (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public List<ProdutoEntity> GetProdutoAgregado(int pintIDProduto, int pintIDItem)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCA_PRODUTO_AGREGADO_ID";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PRODUTO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDProduto;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_ITEM_PRODUTO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDItem;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);


                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<ProdutoEntity> lcolEnt = new List<ProdutoEntity>();
                            ProdutoEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new ProdutoEntity();

                                lEnt.CD_PRODUTO = int.Parse(myReader[0].ToString());
                                lEnt.NM_PRODUTO = myReader[1].ToString();

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
                _msgErro = "ProdutoAgregadoData (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "ProdutoAgregadoData (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public List<ProdutoEntity> GetProduto(int pintIDProduto)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCA_PRODUTO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PRODUTO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDProduto;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<ProdutoEntity> lcolEnt = new List<ProdutoEntity>();
                            ProdutoEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new ProdutoEntity();

                                lEnt.CD_PRODUTO = int.Parse(myReader[0].ToString());
                                lEnt.NM_PRODUTO = myReader[1].ToString();
                                lEnt.ST_IND_ONLINE = int.Parse(myReader[2].ToString());

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
                _msgErro = "GetProdutoData (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GetProdutoData (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public List<ProdutoEntity> GetProdutoSQL(int pintIDProduto)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                using (SqlCommand myComando = new SqlCommand())
                {
                    if (DataContextSQL.AbrirConexao())
                    {
                        myComando.Connection = DataContextSQL.GetConnection;
                        myComando.CommandType = CommandType.StoredProcedure;
                        myComando.CommandText = "WS_BUSCA_PRODUTO";

                        SqlParameter myParametro;

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "SP_CD_PRODUTO";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintIDProduto;

                        myComando.Parameters.Add(myParametro);

                        SqlDataReader myReader = DataContextSQL.ExecutarReader(myComando);

                        if (DataContextSQL.Erro == 0)
                        {
                            if (myReader.HasRows)
                            {

                                List<ProdutoEntity> lcolEnt = new List<ProdutoEntity>();
                                ProdutoEntity lEnt;

                                while (myReader.Read())
                                {

                                    lEnt = new ProdutoEntity();

                                    lEnt.CD_PRODUTO = int.Parse(myReader[0].ToString());
                                    lEnt.NM_PRODUTO = myReader[1].ToString();
                                    lEnt.ST_IND_ONLINE = int.Parse(myReader[2].ToString());

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
                _msgErro = ex.Message;

                return null;
            }
            finally
            {
                if (DataContextSQL.ConexaoAberta())
                {
                    if (!DataContextSQL.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GetProdutoDataSQL - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }
    }
}
