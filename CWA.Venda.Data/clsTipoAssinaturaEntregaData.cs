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
    public class TipoAssinaturaEntregaData
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

        public TipoAssinaturaEntregaEntity GetTipoAssinaturaEntrega(int pintTipoEntrega, int pintTipoAssinatura)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCAR_TIPO_ASSINATURA_ENTREGA";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_TIPO_ENTREGA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintTipoEntrega;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_TIPO_ASSINATURA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintTipoAssinatura;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        TipoAssinaturaEntregaEntity lEnt = new TipoAssinaturaEntregaEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.CD_TIPO_ASSINATURA = int.Parse(myReader[0].ToString());
                            lEnt.CD_TIPO_ENTREGA = int.Parse(myReader[1].ToString());
                            lEnt.NU_QTD_SEMANAS = int.Parse(myReader[2].ToString());
                            lEnt.QTD_DIAS_UTEIS = int.Parse(myReader[3].ToString());
                            lEnt.QTD_DIAS_DOM = int.Parse(myReader[4].ToString());


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
                        _msgErro = "GetTipoAssinaturaEntrega (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

    }
}
