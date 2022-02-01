using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using System.Data.SqlClient;

using CWA.Venda.Entity;

namespace CWA.Venda.Data
{
    public class ViewLogrData
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

        public List<DadosEntregaEntity> GetViewLogrEntity(string pstrCEP)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_LOGR_CEP";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_DS_CEP";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrCEP;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<DadosEntregaEntity> lcolEnt = new List<DadosEntregaEntity>();
                            DadosEntregaEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new DadosEntregaEntity();

                                lEnt.ID_LOGR = int.Parse(myReader[0].ToString());
                                lEnt.DS_TIPO = myReader[1].ToString();
                                lEnt.DS_LOGR = myReader[2].ToString();
                                lEnt.DS_MUNICIPIO = myReader[3].ToString();
                                lEnt.DS_BAIRRO = myReader[4].ToString();
                                lEnt.DS_UF = myReader[5].ToString();

                                lEnt.CD_TIPO = myReader[6].ToString();
                                lEnt.NU_CEP = myReader[7].ToString();
                                lEnt.ST_CEP_UNICO = int.Parse(myReader[8].ToString());

                                //Util.Utility.SetLogXML("DadosEntrega-DebugEnder5", "Erro", myReader[8].ToString(), false);

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
                _msgErro = "GetViewLogrEntity (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GetViewLogrEntity (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public List<DadosEntregaEntity> GetViewLogrEntityID(Int32 pintIDLogr)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_LOGR_ID";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_LOGRADOURO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDLogr;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<DadosEntregaEntity> lcolEnt = new List<DadosEntregaEntity>();
                            DadosEntregaEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new DadosEntregaEntity();

                                lEnt.ID_LOGR = int.Parse(myReader[0].ToString());
                                lEnt.DS_TIPO = myReader[1].ToString();
                                lEnt.DS_LOGR = myReader[2].ToString();
                                lEnt.DS_MUNICIPIO = myReader[3].ToString();
                                lEnt.DS_BAIRRO = myReader[4].ToString();
                                lEnt.DS_UF = myReader[5].ToString();

                                lEnt.CD_TIPO = myReader[6].ToString();
                                lEnt.NU_CEP = myReader[7].ToString();

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
                _msgErro = "GetViewLogrEntity (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GetViewLogrEntity (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }
        public string GetCodRoteirizacao(int pintIDProduto, int pintIDLogr, int pintNumeroResid)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);

                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCA_ROTEIRO_LOGRADOURO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PRODUTO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDProduto;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_LOGRADOURO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDLogr;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_NU_RESIDENCIA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintNumeroResid;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_WPRODAGREG";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = DBNull.Value;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_AUXNUCEP";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = DBNull.Value;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_WFORMAEXPEDICAO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = 0;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_WTPENDERECO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = DBNull.Value;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_COD_ROT";
                    myParametro.Direction = ParameterDirection.Output;
                    myParametro.DbType = DbType.String;
                    myParametro.Size = 50;
                    myParametro.Value = "";

                    myComando.Parameters.Add(myParametro);

                   DataContext.ExecutarComando(myComando);


                    if (DataContext.Erro == 0)
                    {

                        string lstrCorRoteirizacao = myComando.Parameters["SP_COD_ROT"].Value.ToString();

                        return lstrCorRoteirizacao;
                    }
                    else
                    {
                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return "-1";

                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return "-1";
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return "-1";
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetCodRoteirizacao (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        

        }

        public List<DadosEntregaEntity> GetViewLogrEntityIDSQL(Int32 pintIDLogr)
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
                        myComando.CommandText = "WS_VENDA_BUSCA_LOGR_ID";

                        SqlParameter myParametro;

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "SP_CD_LOGRADOURO";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintIDLogr;

                        myComando.Parameters.Add(myParametro);

                        SqlDataReader  myReader = DataContextSQL.ExecutarReader(myComando);

                        if (DataContext.Erro == 0)
                        {

                            if (myReader.HasRows)
                            {

                                List<DadosEntregaEntity> lcolEnt = new List<DadosEntregaEntity>();
                                DadosEntregaEntity lEnt;

                                while (myReader.Read())
                                {

                                    lEnt = new DadosEntregaEntity();

                                    lEnt.ID_LOGR = int.Parse(myReader[0].ToString());
                                    lEnt.DS_TIPO = myReader[1].ToString();
                                    lEnt.DS_LOGR = myReader[2].ToString();
                                    lEnt.DS_MUNICIPIO = myReader[3].ToString();
                                    lEnt.DS_BAIRRO = myReader[4].ToString();
                                    lEnt.DS_UF = myReader[5].ToString();

                                    lEnt.CD_TIPO = myReader[6].ToString();
                                    lEnt.NU_CEP = myReader[7].ToString();

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
                _msgErro = "GetViewLogrEntity (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContextSQL.ConexaoAberta())
                {
                    if (!DataContextSQL.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GetViewLogrEntity (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

    }
}
