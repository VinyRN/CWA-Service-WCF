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
    public class PrecoProdutoAgregData
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

        public List<PrecoProdutoAgregEntity> GetPrecoProdutoAgregado(int pintIDProduto, int pintIDitem)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "SP_BUSCA_PRECO_PRODUTO_AGREGADO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PRODUTO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDProduto;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_ITEM_PRODUTO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDitem;

                    myComando.Parameters.Add(myParametro);


                    DbDataReader myReader = DataContext.ExecutarReader(myComando);


                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<PrecoProdutoAgregEntity> lcolEnt = new List<PrecoProdutoAgregEntity>();
                            PrecoProdutoAgregEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new PrecoProdutoAgregEntity();

                                lEnt.CD_PRODUTO = int.Parse(myReader[0].ToString());
                                lEnt.CD_ITEM_PRODUTO = int.Parse(myReader[1].ToString());
                                lEnt.CD_FORMA_PAG = int.Parse(myReader[2].ToString());
                                lEnt.DT_BASE = DateTime.Parse(myReader[3].ToString());
                                lEnt.CD_GRUPO_CLIENTE = int.Parse(myReader[4].ToString());

                                lEnt.VL_PARC1_ASS = decimal.Parse(myReader[5].ToString());
                                lEnt.VL_DEMAIS_PARC_ASS = decimal.Parse(myReader[6].ToString());

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

    }
}
