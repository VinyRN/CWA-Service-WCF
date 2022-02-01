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
    public class FormaPagamentoData
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

        public List<FormaPagamentoEntity> GetFormaPagamentoEntityList(int pintIDProduto, 
                                                                      int pintIDCampanha,
                                                                      int pintIDTipoAssinatura, 
                                                                      int pintTipoEntrega,
                                                                      int pintIDTipoPag)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_FORMA_PAGAMENTO";

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

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_TP_ENTREGA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintTipoEntrega;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_TP_PAG";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDTipoPag;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);


                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<FormaPagamentoEntity> lcolEnt = new List<FormaPagamentoEntity>();
                            FormaPagamentoEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new FormaPagamentoEntity();

                                lEnt.CD_FORMA_PAG = int.Parse(myReader[0].ToString());
                                lEnt.NU_PARCELAS = int.Parse(myReader[1].ToString());

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
                _erro = -1;
                _msgErro = "FormaPagamentoData (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "FormaPagamentoData (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }



        public List<FormaPagamentoEntity> GetFormaPagamentoEntityList()
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCAR_FORMA_PAGAMENTO";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<FormaPagamentoEntity> lcolEnt = new List<FormaPagamentoEntity>();
                            FormaPagamentoEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new FormaPagamentoEntity();

                                lEnt.CD_FORMA_PAG = int.Parse(myReader[0].ToString());
                                lEnt.DS_FORMA_PAGAMENTO = myReader[1].ToString();
                                lEnt.NU_PARCELAS = int.Parse(myReader[2].ToString());
                                lEnt.CD_TP_PAGAMENTO = int.Parse(myReader[3].ToString());


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
                _erro = -1;
                _msgErro = "GetFormaPagamentoEntityList (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GetFormaPagamentoEntityList (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public FormaPagamentoEntity GetFormaPagamentoEntity(int pintIDFormaPag)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCAR_FORMA_PAGAMENTO_ID";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_FORMA_PAG";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDFormaPag;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        FormaPagamentoEntity lEnt = new FormaPagamentoEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.CD_FORMA_PAG = int.Parse(myReader[0].ToString());
                            lEnt.DS_FORMA_PAGAMENTO = myReader[1].ToString();
                            lEnt.NU_PARCELAS = int.Parse(myReader[2].ToString());
                            lEnt.CD_TP_PAGAMENTO = int.Parse(myReader[3].ToString());


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
                _erro = -1;
                _msgErro = "GetFormaPagamentoEntityList (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GetFormaPagamentoEntityList (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }


    }
}
