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
    public class ListaNegraData
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

        public string[] GetValidarListaNegra(ListaNegraEntity ObjListaNegra)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_LISTA_NEGRA";

                    AddParmsValidaLista(ref myComando, ObjListaNegra);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            string[] lstrVetorLista = myReader[0].ToString().Split(',');

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lstrVetorLista;

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
                _msgErro = "GetValidarListaNegra (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GetValidarListaNegra (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        private void AddParmsValidaLista(ref DbCommand pdbCommand, ListaNegraEntity pEnt)
        {
            DbParameter ldbParameter;

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_CARTAO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (pEnt.NU_CARTAO == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = pEnt.NU_CARTAO;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_BANCO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;

            if (pEnt.NU_BANCO == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = pEnt.NU_BANCO;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_AGENCIA";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (pEnt.NU_AGENCIA == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = pEnt.NU_AGENCIA;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_CONTA";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (pEnt.NU_CONTA == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = pEnt.NU_CONTA;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_CPF_CNPJ";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int64;

            if (pEnt.NU_CPF_CNPJ == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = pEnt.NU_CPF_CNPJ;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_CD_LOGR";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;

            if (pEnt.CD_LOGR == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = pEnt.CD_LOGR;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_RESID";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;

            if (pEnt.NU_RESID == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = pEnt.NU_RESID;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_BLOCO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (pEnt.NU_BLOCO == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = pEnt.NU_BLOCO;
            }

            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_NU_APTO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (pEnt.NU_APTO == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = pEnt.NU_APTO;
            }

            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_COMPL_RESID";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (pEnt.COMPL_RESID == null)
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = pEnt.COMPL_RESID;
            }

            pdbCommand.Parameters.Add(ldbParameter);

        }
    }
}
