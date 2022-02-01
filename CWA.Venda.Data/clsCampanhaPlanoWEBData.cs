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
    public class CampanhaPlanoWEBData
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

        public void SetCampanhaPlanoWEB(CampanhaPlanoWEBEntity ObjEnt, Boolean pbolAbrirTransacao, Boolean pbolFecharConexao)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.StoredProcedure;
                myComando.CommandText = "WS_VENDA_CAMPANHA_PLANO_WEB_I_01";

                AddParms(ref myComando, ObjEnt, "I");

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

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "SetCampanhaPlanoWEB - " + ex.Message;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "SetCampanhaPlanoWEB - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }
        
        }

        public void UpdCampanhaPlanoWEB(CampanhaPlanoWEBEntity ObjEnt, Boolean pbolAbrirTransacao, Boolean pbolFecharConexao)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.StoredProcedure;
                myComando.CommandText = "WS_VENDA_CAMPANHA_PLANO_WEB_U_01";

                AddParms(ref myComando, ObjEnt, "U");

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

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "UpdCampanhaPlanoWEB - " + ex.Message;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "UpdCampanhaPlanoWEB - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        public void DelCampanhaPlanoWEB(CampanhaPlanoWEBEntity ObjEnt, Boolean pbolAbrirTransacao, Boolean pbolFecharConexao)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.StoredProcedure;
                myComando.CommandText = "WS_VENDA_CAMPANHA_PLANO_WEB_D_01";

                AddParms(ref myComando, ObjEnt, "D");

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

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "DelCampanhaPlanoWEB - " + ex.Message;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContext.ConexaoAberta())
                    {
                        if (!DataContext.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "DelCampanhaPlanoWEB - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        private void AddParms(ref DbCommand pdbCommand, CampanhaPlanoWEBEntity ObjEnt, string pstrOpcao)
        {
            DbParameter ldbParameter;

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_CD_CAMPANHA";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.CD_CAMPANHA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_CD_PLANO";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjEnt.CD_PLANO;

            pdbCommand.Parameters.Add(ldbParameter);

            if (pstrOpcao != "D"  )
            {
                ldbParameter = pdbCommand.CreateParameter();
                ldbParameter.ParameterName = "SP_CD_IMG_MKT";
                ldbParameter.Direction = ParameterDirection.Input;
                ldbParameter.DbType = DbType.String;

                if (ObjEnt.CD_IMG_MKT.Trim() != "")
                {
                    ldbParameter.Value = ObjEnt.CD_IMG_MKT;
                }
                else
                {
                    ldbParameter.Value = DBNull.Value;
                }


                pdbCommand.Parameters.Add(ldbParameter);

                ldbParameter = pdbCommand.CreateParameter();
                ldbParameter.ParameterName = "SP_CD_TP_CANAL";
                ldbParameter.Direction = ParameterDirection.Input;
                ldbParameter.DbType = DbType.Int32;
                ldbParameter.Value = ObjEnt.CD_TP_CANAL;

                pdbCommand.Parameters.Add(ldbParameter);

                ldbParameter = pdbCommand.CreateParameter();
                ldbParameter.ParameterName = "SP_DT_CADASTRO";
                ldbParameter.Direction = ParameterDirection.Input;
                ldbParameter.DbType = DbType.DateTime;
                ldbParameter.Value = ObjEnt.DT_CADASTRO;

                pdbCommand.Parameters.Add(ldbParameter);

                ldbParameter = pdbCommand.CreateParameter();
                ldbParameter.ParameterName = "SP_CD_USUARIO";
                ldbParameter.Direction = ParameterDirection.Input;
                ldbParameter.DbType = DbType.Int32;
                ldbParameter.Value = ObjEnt.CD_USUARIO;

                pdbCommand.Parameters.Add(ldbParameter);

                ldbParameter = pdbCommand.CreateParameter();
                ldbParameter.ParameterName = "SP_DS_TIT_MKT";
                ldbParameter.Direction = ParameterDirection.Input;
                ldbParameter.DbType = DbType.String;
                ldbParameter.Value = ObjEnt.DS_TIT_MKT;

                pdbCommand.Parameters.Add(ldbParameter);

                ldbParameter = pdbCommand.CreateParameter();
                ldbParameter.ParameterName = "SP_DS_MKT";
                ldbParameter.Direction = ParameterDirection.Input;
                ldbParameter.DbType = DbType.String;
                ldbParameter.Value = ObjEnt.DS_MKT;

                pdbCommand.Parameters.Add(ldbParameter);

                ldbParameter = pdbCommand.CreateParameter();
                ldbParameter.ParameterName = "SP_ST_DESTAQUE";
                ldbParameter.Direction = ParameterDirection.Input;
                ldbParameter.DbType = DbType.Int32;
                ldbParameter.Value = ObjEnt.ST_DESTAQUE;

                pdbCommand.Parameters.Add(ldbParameter);

                ldbParameter = pdbCommand.CreateParameter();
                ldbParameter.ParameterName = "SP_NU_ORDEM";
                ldbParameter.Direction = ParameterDirection.Input;
                ldbParameter.DbType = DbType.Int32;
                ldbParameter.Value = ObjEnt.NU_ORDEM;

                pdbCommand.Parameters.Add(ldbParameter);

                ldbParameter = pdbCommand.CreateParameter();
                ldbParameter.ParameterName = "SP_DET_MKT";
                ldbParameter.Direction = ParameterDirection.Input;
                ldbParameter.DbType = DbType.String;
                ldbParameter.Value = ObjEnt.DS_DET_MKT;

                pdbCommand.Parameters.Add(ldbParameter);
                
            }
        }

        public CampanhaPlanoWEBEntity GetCampanhaPlanoWEB(int pintIDCampanha, int pintIDPlano) 
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_PLANO_CAMPANHA_WEB";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_CAMPANHA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDCampanha;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PLANO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDPlano;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        CampanhaPlanoWEBEntity lEnt = new CampanhaPlanoWEBEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.CD_CAMPANHA = int.Parse(myReader[0].ToString());
                            lEnt.CD_PLANO = int.Parse(myReader[1].ToString());
                            lEnt.CD_IMG_MKT = myReader[2].ToString();
                            lEnt.CD_TP_CANAL = int.Parse(myReader[3].ToString());
                            lEnt.DT_CADASTRO = DateTime.Parse(myReader[4].ToString());
                            lEnt.CD_USUARIO = int.Parse(myReader[5].ToString());
                            lEnt.DS_TIT_MKT = myReader[6].ToString();
                            lEnt.DS_MKT = myReader[7].ToString();
                            lEnt.ST_DESTAQUE = int.Parse(myReader[8].ToString());
                            lEnt.NU_ORDEM = int.Parse(myReader[9].ToString());

                            lEnt.DS_CAMPANHA = myReader[10].ToString();
                            lEnt.DS_PLANO = myReader[11].ToString();
                            lEnt.CD_PRODUTO = int.Parse(myReader[12].ToString());
                            lEnt.DS_DET_MKT = myReader[13].ToString();


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
                        _msgErro = "GetCampanhaPlanoWEB (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public List<CampanhaPlanoWEBEntity> GetCampanhaPlanoWEB(int pintIDCampanha)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_PLANO_CAMPANHA_WEB";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_CAMPANHA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDCampanha;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<CampanhaPlanoWEBEntity> lcolEnt = new List<CampanhaPlanoWEBEntity>();
                            CampanhaPlanoWEBEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new CampanhaPlanoWEBEntity();

                                lEnt.CD_CAMPANHA = int.Parse(myReader[0].ToString());
                                lEnt.CD_PLANO = int.Parse(myReader[1].ToString());
                                lEnt.CD_IMG_MKT = myReader[2].ToString();
                                lEnt.CD_TP_CANAL = int.Parse(myReader[3].ToString());
                                lEnt.DT_CADASTRO = DateTime.Parse(myReader[4].ToString());
                                lEnt.CD_USUARIO = int.Parse(myReader[5].ToString());
                                lEnt.DS_TIT_MKT = myReader[6].ToString();
                                lEnt.DS_MKT = myReader[7].ToString();
                                lEnt.ST_DESTAQUE = int.Parse(myReader[8].ToString());
                                lEnt.NU_ORDEM = int.Parse(myReader[9].ToString());

                                lEnt.DS_CAMPANHA = myReader[10].ToString();
                                lEnt.DS_PLANO = myReader[11].ToString();

                                lEnt.CD_PRODUTO = int.Parse(myReader[12].ToString());
                                lEnt.DS_DET_MKT = myReader[13].ToString();

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
                        _msgErro = "GetCampanhaPlanoWEB (Select ALL) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public List<CampanhaPlanoWEBEntity> GetCampanhaPlanoWEBConteudo(int pintDestaque)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_PLANO_CAMPANHA_WEB_CONTEUDO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_DEST";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintDestaque;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<CampanhaPlanoWEBEntity> lcolEnt = new List<CampanhaPlanoWEBEntity>();
                            CampanhaPlanoWEBEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new CampanhaPlanoWEBEntity();

                                lEnt.CD_CAMPANHA = int.Parse(myReader[0].ToString());
                                lEnt.CD_PLANO = int.Parse(myReader[1].ToString());
                                lEnt.CD_IMG_MKT = myReader[2].ToString();
                                lEnt.CD_TP_CANAL = int.Parse(myReader[3].ToString());
                                lEnt.DT_CADASTRO = DateTime.Parse(myReader[4].ToString());
                                lEnt.CD_USUARIO = int.Parse(myReader[5].ToString());
                                lEnt.DS_TIT_MKT = myReader[6].ToString();
                                lEnt.DS_MKT = myReader[7].ToString();
                                lEnt.ST_DESTAQUE = int.Parse(myReader[8].ToString());
                                lEnt.NU_ORDEM = int.Parse(myReader[9].ToString());

                                lEnt.DS_CAMPANHA = myReader[10].ToString();
                                lEnt.DS_PLANO = myReader[11].ToString();

                                lEnt.CD_PRODUTO = int.Parse(myReader[12].ToString());
                                lEnt.DS_DET_MKT = myReader[13].ToString();

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
                        _msgErro = "GetCampanhaPlanoWEBConteudo - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public int GetTotalDestaque()
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_CONTA_DESTAQUE";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_TOTAL_DESTAQUE";
                    myParametro.Direction = ParameterDirection.Output;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = 0;

                    myComando.Parameters.Add(myParametro);

                    DataContext.ExecutarComando(myComando);

                    if (DataContext.Erro == 0)
                    {

                        int lintQTDDestaque = int.Parse(myComando.Parameters["SP_TOTAL_DESTAQUE"].Value.ToString());

                        return lintQTDDestaque;
                    }
                    else
                    {
                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return 0;

                    }

                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return 0;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return 0;

            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetTotalDestaque (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        
        }
    }

}
