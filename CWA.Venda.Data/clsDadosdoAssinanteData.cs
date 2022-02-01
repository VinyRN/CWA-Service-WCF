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
    public class DadosdoAssinanteData
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

        public List<Central.Entity.DadosdoAssinanteCentralEntity> GetLerDadosDoAssinanteCentral(int CD_CONTABIL_PESSOA)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {

                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCARDADOSDOASSINANTE";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = CD_CONTABIL_PESSOA;

                    myComando.Parameters.Add(myParametro);


                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<DadosdoAssinanteCentralEntity> lcolEnt = new List<DadosdoAssinanteCentralEntity>();
                            DadosdoAssinanteCentralEntity lEnt;

                            while (myReader.Read())
                            {
                                lEnt = new DadosdoAssinanteCentralEntity();


                                lEnt.ST_TP_PESSOA = byte.Parse(myReader[0].ToString());
                                lEnt.NM_PESSOA = myReader[1].ToString();
                                lEnt.NM_SOBRENOME = myReader[2].ToString();
                                lEnt.NM_RESPONSAVEL = myReader[3].ToString();
                                lEnt.NM_FANTASIA = myReader[4].ToString();
                                lEnt.CD_CONTABIL_PESSOA = int.Parse(myReader[5].ToString());
                                lEnt.NU_CPF = myReader[6].ToString();
                                lEnt.NU_IDENTIDADE = myReader[7].ToString();
                                lEnt.NM_ORGAO_EMISSOR = myReader[8].ToString();

                                if (!myReader.IsDBNull(9))
                                {
                                    lEnt.DT_EMISSAO = DateTime.Parse(myReader[9].ToString());
                                }

                                if (!myReader.IsDBNull(10))
                                {
                                    lEnt.ST_ESTADO_CIVIL = byte.Parse(myReader[10].ToString());
                                }

                                if (!myReader.IsDBNull(11))
                                {
                                    lEnt.DT_NASC_FUND = DateTime.Parse(myReader[11].ToString());
                                }

                                lEnt.NU_CNPJ = myReader[12].ToString();

                                lEnt.NU_INSCR_MUN = myReader[13].ToString();
                                lEnt.NU_INSCR_EST = myReader[14].ToString();

                                if (!myReader.IsDBNull(15))
                                {
                                    lEnt.CD_RAMO = short.Parse(myReader[15].ToString());
                                }

                                lEnt.ST_IND_DIVULGACAO = byte.Parse(myReader[16].ToString());

                                if (!myReader.IsDBNull(17))
                                {
                                    lEnt.CD_TP_TRATAMENTO = byte.Parse(myReader[17].ToString());
                                }
                                lEnt.UF_RG = myReader[18].ToString();

                                if (!myReader.IsDBNull(19))
                                {
                                    lEnt.CD_CARGO = int.Parse(myReader[19].ToString());
                                }
                                if (!myReader.IsDBNull(20))
                                {
                                    lEnt.CD_GRAU_INSTRUCAO = int.Parse(myReader[20].ToString());
                                }
                                if (!myReader.IsDBNull(21))
                                {
                                    lEnt.CD_NACIONALIDADE = int.Parse(myReader[21].ToString());
                                }
                                if (!myReader.IsDBNull(22))
                                {
                                    lEnt.CD_NATURALIDADE = int.Parse(myReader[22].ToString());
                                }
                                if (!myReader.IsDBNull(23))
                                {
                                    lEnt.CD_PROFISSAO = int.Parse(myReader[23].ToString());
                                }

                                if (!myReader.IsDBNull(24))
                                {
                                    lEnt.NU_CPF_RESP = long.Parse(myReader[24].ToString());
                                }
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
                        _msgErro = "GetLerContratosCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public void SetAlterarDadosDoAssinanteCentral(byte? ST_TP_PESSOA, string NM_PESSOA,
                                                      int? CD_CONTABIL_PESSOA, string NM_RESPONSAVEL,
                                                      string NU_IDENTIDADE, string NM_ORGAO_EMISSOR,
                                                      DateTime? DT_EMISSAO, byte? ST_ESTADO_CIVIL,
                                                      DateTime? DT_NASC_FUND, string NM_FANTASIA,
                                                      string NU_INSCR_MUN, string NU_INSCR_EST,
                                                      short? CD_RAMO, string DS_NOME_ABREV,
                                                      int? CD_GRUPO_AFINIDADE, byte? CD_TP_TRATAMENTO,
                                                      int? CD_CARGO, int? CD_GRAU_INSTRUCAO,
                                                      int? CD_NACIONALIDADE, int? CD_NATURALIDADE,
                                                      int? CD_PROFISSAO, string NM_SOBRENOME, long? NU_CPF_RESP,
                                                      Boolean pbolAbrirTransacao,
                                                      Boolean pbolFecharConexao)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {

                    //Seleciona o maior SEQ
                    DbCommand myComandoQuery = DataContext.CriarComando(false);
                    myComandoQuery.CommandType = CommandType.Text;
                    myComandoQuery.CommandText = "WSP_BUSCARTELSDOASSINANTE";


                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_ALTERARDADOSPESSOAIS";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "ST_TP_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (ST_TP_PESSOA != null ? ST_TP_PESSOA : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NM_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NM_PESSOA != null ? NM_PESSOA : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (CD_CONTABIL_PESSOA != null ? CD_CONTABIL_PESSOA : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NM_RESPONSAVEL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NM_RESPONSAVEL != null ? NM_RESPONSAVEL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_IDENTIDADE";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NU_IDENTIDADE != null ? NU_IDENTIDADE : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NM_ORGAO_EMISSOR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NM_ORGAO_EMISSOR != null ? NM_ORGAO_EMISSOR : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    if (DT_EMISSAO == Convert.ToDateTime("01/01/1900"))
                    {
                        DT_EMISSAO = null;
                    }

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DT_EMISSAO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.DateTime;
                    myParametro.Value = (DT_EMISSAO != null ? DT_EMISSAO : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "ST_ESTADO_CIVIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (DT_EMISSAO != null ? DT_EMISSAO : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    if (DT_NASC_FUND == Convert.ToDateTime("01/01/1900"))
                    {
                        DT_NASC_FUND = null;
                    }

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DT_NASC_FUND";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.DateTime;
                    myParametro.Value = (DT_NASC_FUND != null ? DT_NASC_FUND : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NM_FANTASIA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NM_FANTASIA != null ? NM_FANTASIA : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_INSCR_MUN";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NU_INSCR_MUN != null ? NU_INSCR_MUN : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_INSCR_EST";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NU_INSCR_EST != null ? NU_INSCR_EST : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_RAMO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (CD_RAMO != null ? CD_RAMO : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_NOME_ABREV";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (DS_NOME_ABREV != null ? DS_NOME_ABREV : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_GRUPO_AFINIDADE";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (CD_GRUPO_AFINIDADE != null ? CD_GRUPO_AFINIDADE : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_TP_TRATAMENTO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (CD_TP_TRATAMENTO != null ? CD_TP_TRATAMENTO : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CARGO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (CD_CARGO != null ? CD_CARGO : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_GRAU_INSTRUCAO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (CD_GRAU_INSTRUCAO != null ? CD_GRAU_INSTRUCAO : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_NACIONALIDADE";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (CD_NACIONALIDADE != null ? CD_NACIONALIDADE : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_NATURALIDADE";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (CD_NATURALIDADE != null ? CD_NATURALIDADE : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_PROFISSAO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (CD_PROFISSAO != null ? CD_PROFISSAO : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NM_SOBRENOME";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (NM_SOBRENOME != null ? NM_SOBRENOME : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    if (NU_CPF_RESP == 0)
                    {
                        NU_CPF_RESP = null;
                    }

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_CPF_RESP";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int64;
                    myParametro.Value = (NU_CPF_RESP != null ? NU_CPF_RESP : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    DataContext.ExecutarComando(myComando);

                    if (DataContext.Erro != 0)
                    {
                        if (pbolAbrirTransacao)
                        {
                            DataContext.RollbackTransaction();
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;
                    }
                    else
                    {
                        DataContext.CommitTransaction();
                    }
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "SetAlterarDadosDoAssinanteCentral (UpDate) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

    }
}
