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
    public class ClubeAssinanteData
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

        public ClubeAssinanteEntity GetDadosCarteiraClube(int pintCdPessoa, string pstrSerieCTR, int pintNuCTR, int pintDvCTR)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_CARTEIRA_CLUBE";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCdPessoa;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_SERIE_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrSerieCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintNuCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_DV_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintDvCTR;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        ClubeAssinanteEntity lEnt = new ClubeAssinanteEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.NU_SERIE_CTR = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                            lEnt.NU_CTR = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                            lEnt.NU_DV_CTR = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                            lEnt.CD_CONTABIL_PESSOA = myReader.IsDBNull(3) ? "" : myReader[3].ToString();
                            lEnt.NM_PESSOA = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                            lEnt.ST_TP_PESSOA = myReader.IsDBNull(5) ? "" : myReader[5].ToString();
                            lEnt.NU_DOC = myReader.IsDBNull(6) ? "" : myReader[6].ToString();
                            lEnt.DS_EMAIL = myReader.IsDBNull(7) ? "" : myReader[7].ToString();
                            lEnt.NM_PRODUTO = myReader.IsDBNull(8) ? "" : myReader[8].ToString();
                            lEnt.NM_DEPENDENTE = myReader.IsDBNull(9) ? "" : myReader[9].ToString();
                            lEnt.MES_TERMINO = myReader.IsDBNull(10) ? "" : myReader[10].ToString();
                            lEnt.ANO_TERMINO = myReader.IsDBNull(11) ? "" : myReader[11].ToString();

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
                        _msgErro = "GetDadosCarteiraClube (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }
    }
}
