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
    public class clsCargaWebCorreioData
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

        public List<CargaWebCorreioEntity> GetCargaWebCorreio(int pintIDProduto, 
                                                              int pintIDCampanha, 
                                                              int pintStIndCortesia)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCA_CARAGAWEB_CORREIO";

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
                    myParametro.ParameterName = "SP_ST_IND_CORTESIA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintStIndCortesia;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                            //Recupera dados da campanha
                            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                            string lstrCampanha = "";
                            string lstrIDCamp = "";

                            if (pintIDCampanha != 0)
                            {
                                CampanhaEntity ObjEntCamp = new CampanhaEntity();
                                CampanhaData ObjDataCamp = new CampanhaData();

                                ObjEntCamp = ObjDataCamp.GetCampanha(pintIDCampanha);

                                if (ObjEntCamp != null)
                                {
                                    lstrCampanha = ObjEntCamp.DS_CAMPANHA;
                                    lstrIDCamp = ObjEntCamp.CD_CAMPANHA.ToString();
                                }

                            }
                            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                            List<CargaWebCorreioEntity> lcolEnt = new List<CargaWebCorreioEntity>();
                            CargaWebCorreioEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new CargaWebCorreioEntity();

                                lEnt.TIPO = int.Parse(myReader[0].ToString());

                                if (lEnt.TIPO == 1)
                                {
                                    lEnt.CD_CONTABIL_PESSOA = int.Parse(myReader[1].ToString());
                                    lEnt.NM_PESSOA = myReader[2].ToString();
                                    lEnt.NM_DEPENDENTE = myReader.IsDBNull(3) ? "" : myReader[3].ToString();
                                    lEnt.NU_CPF = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                                    lEnt.NU_CNPJ = myReader.IsDBNull(5) ? "" : myReader[5].ToString();
                                    lEnt.DS_EMAIL = myReader.IsDBNull(6) ? "" : myReader[6].ToString();
                                    lEnt.DT_TERMINO = myReader.IsDBNull(7) ? "" : DateTime.Parse(myReader[7].ToString()).ToString("dd/MM/yyyy");
                                    lEnt.NM_RESPONSAVEL = myReader.IsDBNull(8) ? "" : myReader[8].ToString();
                                    lEnt.ST_IND_ADESAO_CLUBE = myReader.IsDBNull(9) ? 0 : int.Parse(myReader[9].ToString());

                                    if (lstrCampanha.Trim() != "")
                                    {
                                        lEnt.CD_CAMPANHA = int.Parse(lstrIDCamp);
                                        lEnt.NM_CAMPANHA = lstrCampanha;
                                    }
                                    else
                                    {
                                        lEnt.NM_CAMPANHA = lstrCampanha;
                                    }

                                    lEnt.TIPO_PESSOA = myReader.IsDBNull(4) ? "PJ" : "PF";

                                }
                                else
                                {
                                    lEnt.CD_CONTABIL_PESSOA = int.Parse(myReader[1].ToString());
                                    lEnt.NM_DEPENDENTE = myReader.IsDBNull(3) ? "" : myReader[3].ToString();
                                    lEnt.NM_PESSOA = myReader.IsDBNull(4) ? myReader[2].ToString() : "" ;
                                    lEnt.DT_TERMINO = myReader.IsDBNull(7) ? "" : DateTime.Parse(myReader[7].ToString()).ToString("dd/MM/yyyy");
                                    
                                    if (lstrCampanha.Trim() != "")
                                    {
                                        lEnt.CD_CAMPANHA = int.Parse(lstrIDCamp);
                                        lEnt.NM_CAMPANHA = lstrCampanha;
                                    }
                                    else
                                    {
                                        lEnt.NM_CAMPANHA = lstrCampanha;
                                    }

                                    lEnt.TIPO_PESSOA = myReader.IsDBNull(4) ? "PJ" : "PF";
                                }

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
                        _msgErro = "GetCargaWebCorreio (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public List<CargaWebCorreioEntity> GetCargaWebCorpCorreio(int pintIDProduto,
                                                                  int pintIDCampanha,
                                                                  int pintStIndCortesia)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCA_CARAGAWEBCORP_CORREIO";

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
                    myParametro.ParameterName = "SP_ST_IND_CORTESIA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintStIndCortesia;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                            //Recupera dados da campanha
                            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                            string lstrCampanha = "";
                            string lstrIDCamp = "";

                            if (pintIDCampanha != 0)
                            {
                                CampanhaEntity ObjEntCamp = new CampanhaEntity();
                                CampanhaData ObjDataCamp = new CampanhaData();

                                ObjEntCamp = ObjDataCamp.GetCampanha(pintIDCampanha);

                                if (ObjEntCamp != null)
                                {
                                    lstrCampanha = ObjEntCamp.DS_CAMPANHA;
                                    lstrIDCamp = ObjEntCamp.CD_CAMPANHA.ToString();
                                }

                            }
                            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                            List<CargaWebCorreioEntity> lcolEnt = new List<CargaWebCorreioEntity>();
                            CargaWebCorreioEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new CargaWebCorreioEntity();

                                lEnt.TIPO = 0;
                                lEnt.CD_CONTABIL_PESSOA = int.Parse(myReader[0].ToString());
                                lEnt.NM_PESSOA = myReader[1].ToString();
                                lEnt.NM_DEPENDENTE = "";
                                lEnt.NU_CPF = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                                lEnt.NU_CNPJ = myReader.IsDBNull(3) ? "" : myReader[3].ToString();
                                lEnt.DS_EMAIL = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                                lEnt.DT_TERMINO = myReader.IsDBNull(5) ? "" : DateTime.Parse(myReader[5].ToString()).ToString("dd/MM/yyyy");
                                lEnt.NM_RESPONSAVEL = myReader.IsDBNull(6) ? "" : myReader[6].ToString();
                                lEnt.ST_IND_ADESAO_CLUBE = myReader.IsDBNull(7) ? 0 : int.Parse(myReader[7].ToString());

                                if (lstrCampanha.Trim() != "")
                                {
                                    lEnt.CD_CAMPANHA = int.Parse(lstrIDCamp);
                                    lEnt.NM_CAMPANHA = lstrCampanha;
                                }
                                else
                                {
                                    lEnt.NM_CAMPANHA = lstrCampanha;
                                }
                                lEnt.TIPO_PESSOA = myReader.IsDBNull(2) ? "PJ" : "PF";

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
                        _msgErro = "GetCargaWebCorreio (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }
    }
}
