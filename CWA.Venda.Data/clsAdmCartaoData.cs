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
    public class AdmCartaoData
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


        public List<BandeiraCartaoEntityNOVO> GetBandeiraCartaoEntityListNOVO()
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCA_BANDEIRA_CARTAO_CREDITO";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);


                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<BandeiraCartaoEntityNOVO> lcolEnt = new List<BandeiraCartaoEntityNOVO>();
                            BandeiraCartaoEntityNOVO lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new BandeiraCartaoEntityNOVO();

                                lEnt.DS_BANDEIRA = myReader[0].ToString();
                                lEnt.ST_TP_BANDEIRA = Convert.ToByte(myReader[1].ToString());
                                lEnt.CD_CONTABIL_ADM = Convert.ToInt32(myReader[2].ToString());
                                lEnt.NU_MAX_PARC = Convert.ToByte(myReader[3].ToString());
                                
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
                _msgErro = "AdmCartaoData (Lista Bandeira) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "AdmCartaoData (Lista Bandeira) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public List<BandeiraCartaoEntity> GetBandeiraCartaoEntityList()
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_BANDEIRA_CARTAO";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);


                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<BandeiraCartaoEntity> lcolEnt = new List<BandeiraCartaoEntity>();
                            BandeiraCartaoEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new BandeiraCartaoEntity();

                                lEnt.ST_TP_BANDEIRA = int.Parse(myReader[0].ToString());
                                lEnt.NM_BANDEIRA = myReader[1].ToString();

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
                _msgErro = "AdmCartaoData (Lista Bandeira) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "AdmCartaoData (Lista Bandeira) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public List<AdmCartaoEntity> GetAdmCartaoEntityList(int pintBandeira)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_ADM_CARTAO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_TP_BANDEIRA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintBandeira;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);


                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<AdmCartaoEntity> lcolEnt = new List<AdmCartaoEntity>();
                            AdmCartaoEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new AdmCartaoEntity();

                                lEnt.CD_ADM_CARTAO = int.Parse(myReader[0].ToString());
                                lEnt.NM_ADM_CARTAO = myReader[1].ToString();

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
                _msgErro = "AdmCartaoData (Lista AdmCartao) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "AdmCartaoData (Lista AdmCartao) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public AdmCartaoEntity GetAdmCartao(int pintIDAdmCartao)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCA_ADM_COBR_GATEWAY";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_CONTABIL_ADM";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDAdmCartao;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        AdmCartaoEntity lEnt = new AdmCartaoEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.CD_ADM_CARTAO = myReader.IsDBNull(0) ? 0 : int.Parse(myReader[0].ToString());
                            lEnt.NM_ADM_CARTAO = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                            lEnt.ST_COBR_GATEWAY = myReader.IsDBNull(2) ? 0 : int.Parse(myReader[2].ToString());
                            

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
                        _msgErro = "GetAdmCartao (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }


        }
    }
}
