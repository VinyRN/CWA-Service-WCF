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
    public class CuboMovRecJCData
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

        public List<CuboMovRecJCEntity> GetCuboMovRecJCEntityList(string psDtInicioInic = "",
            string psDtInicioFim = "", Int32 psCdcontabil = 0)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_CUBO_MOV_RECLAMACAO_JC";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_DT_INICIO_INI";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = psDtInicioInic;

                    if (!string.IsNullOrEmpty(psDtInicioInic))
                    {
                        myParametro.Value = psDtInicioInic;
                    }
                    else
                    {
                        myParametro.Value = DBNull.Value;
                    }

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_DT_INICIO_FIM";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = psDtInicioFim;

                    if (!string.IsNullOrEmpty(psDtInicioFim))
                    {
                        myParametro.Value = psDtInicioFim;
                    }
                    else
                    {
                        myParametro.Value = DBNull.Value;
                    }

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;

                    if (psCdcontabil != 0)
                    {
                        myParametro.Value = psCdcontabil;
                    }
                    else
                    {
                        myParametro.Value = DBNull.Value;
                    }

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);


                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<CuboMovRecJCEntity> lcolEnt = new List<CuboMovRecJCEntity>();
                            CuboMovRecJCEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new CuboMovRecJCEntity();

                                lEnt.COD_CLIENTE = int.Parse(myReader[0].ToString());
                                lEnt.CONTRATO = myReader[1].ToString();
                                lEnt.DT_MOV = myReader[2].ToString();
                                lEnt.CD_MOTIVO_RECLAMACAO = int.Parse(myReader[3].ToString());
                                lEnt.DT_EDICAO = myReader[4].ToString();
                              

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
                _msgErro = "CuboMovRecJCEntity (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "CuboMovRecJCEntity (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }


    }
}
