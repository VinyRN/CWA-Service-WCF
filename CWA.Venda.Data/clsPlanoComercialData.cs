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
    public class PlanoComercialData
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

        public PlanoComercialWebEntity GetValorPlano(int pintIDCampaha, int pintIDPlano, int pintIDLogr)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_VALOR_PLANO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_cd_campanha";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDCampaha;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_cd_plano";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDPlano;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_cd_logradouro";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDLogr;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_nu_parcelas";
                    myParametro.Direction = ParameterDirection.Output;
                    myParametro.DbType = DbType.String;
                    myParametro.Size = 50;
                    myParametro.Value = "";

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_va_parc1_cent";
                    myParametro.Direction = ParameterDirection.Output;
                    myParametro.DbType = DbType.String;
                    myParametro.Size = 50;
                    myParametro.Value = "";

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_va_demais_parc_cent";
                    myParametro.Direction = ParameterDirection.Output;
                    myParametro.DbType = DbType.String;
                    myParametro.Size = 50;
                    myParametro.Value = "";

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_va_total_cent";
                    myParametro.Direction = ParameterDirection.Output;
                    myParametro.DbType = DbType.String;
                    myParametro.Size = 50;
                    myParametro.Value = "";

                    myComando.Parameters.Add(myParametro);

                    DataContext.ExecutarComando(myComando);

                    if (DataContext.Erro == 0)
                    {
                        PlanoComercialWebEntity lEnt = new PlanoComercialWebEntity();
                                             
                        string lstrParcela = myComando.Parameters["sp_nu_parcelas"].Value.ToString();
                        string lstrValorParc = myComando.Parameters["sp_va_parc1_cent"].Value.ToString();
                        string lstrValorDemaisParc = myComando.Parameters["sp_va_demais_parc_cent"].Value.ToString();
                        string lstrValorTotal = myComando.Parameters["sp_va_total_cent"].Value.ToString();

                        lEnt.NU_PARCELA = int.Parse(lstrParcela);
                        lEnt.VA_PARC_CENT = lstrValorParc;
                        lEnt.VA_DEMAIS_PARC_CENT = lstrValorDemaisParc;
                        lEnt.VA_TOTAL_CENT = lstrValorTotal;

                        return lEnt;
                    }
                    else
                    {
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
                _msgErro = "GetValorPlanoData - " + ex.Message;

                return null;

            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetValorPlano (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }

            }

        }

        public PlanoComercialWebEntity GetParametrosPlano(int pintIDPlano)
        { 
        
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_PARAMETRO_PLANO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PLANO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDPlano;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        PlanoComercialWebEntity lEnt = new PlanoComercialWebEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.CD_TP_ASSINATURA= int.Parse(myReader[0].ToString());
                            lEnt.DS_TP_ASSINATURA= myReader[1].ToString();
                            lEnt.CD_TP_ENTREGA= int.Parse(myReader[2].ToString());
                            lEnt.DS_TP_ENTREGA= myReader[3].ToString();
                            lEnt.DIAS_ENTREGA= myReader[4].ToString();
                            lEnt.CD_FORMA_PAG= int.Parse(myReader[5].ToString());
                            lEnt.DS_FORMA_PAG= myReader[6].ToString();
                            lEnt.CD_TP_PAG = int.Parse(myReader[7].ToString());
                            lEnt.NU_PARCELA = int.Parse(myReader[8].ToString());

                            lEnt.DS_PLANO = myReader[9].ToString();
                            lEnt.DS_TP_PAG = myReader[10].ToString();

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
                _msgErro = "GetParametrosPlanoData - " + ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetParametroPlano (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        
        }

        public List<PlanoComercialWebEntity> GetPlanoCampanha(int pintIDCampanha)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_PLANO_CAMPANHA";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_ID_CAMPANHA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDCampanha;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<PlanoComercialWebEntity> lcolEnt = new List<PlanoComercialWebEntity>();
                            PlanoComercialWebEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new PlanoComercialWebEntity();

                                lEnt.CD_PLANO = int.Parse(myReader[0].ToString());
                                lEnt.DS_PLANO =  myReader[1].ToString();

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
                        _msgErro = "GetPlanoCampanha (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public PrecoCampanhaPlanoProdutoEntity GetPrecoCampanhaPlanoProduto(int pintIDCampaha, int pintIDPlano, int pintProduto)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_VALOR_CAMPANHA_PLANO_PRODUTO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PRODUTO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintProduto;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_CAMPANHA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDCampaha;

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
                        PrecoCampanhaPlanoProdutoEntity lEnt = new PrecoCampanhaPlanoProdutoEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.DS_PLANO_MKT = myReader[0].ToString();
                            lEnt.DS_CAMPANHA = myReader[1].ToString();
                            lEnt.DS_PLANO = myReader[2].ToString();
                            lEnt.DS_FORMA_PAG = myReader[3].ToString();

                            lEnt.VL_VALOR_TOTAL = double.Parse(myReader[4].ToString()).ToString("0.00");
                            lEnt.VL_PRIM_PARC = double.Parse(myReader[5].ToString()).ToString("0.00");
                            lEnt.VL_DEMAIS_PARC = double.Parse(myReader[6].ToString()).ToString("0.00");
                            lEnt.ST_PARC_DIF = myReader[7].ToString();
                            lEnt.NU_PARCELA = myReader[8].ToString();

                            lEnt.PRODUTO_ONLINE = int.Parse(myReader[9].ToString());


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
                        _msgErro = "GetPrecoCampanhaPlanoProduto (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        
        }

        public PlanoComercialEntity GetPlanoComercial(int pintIDPlano)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCAR_PLANO_COMERCIAL";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PLANO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDPlano;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        PlanoComercialEntity lEnt = new PlanoComercialEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.DS_PLANO = myReader[0].ToString();
                            lEnt.CD_PLANO = int.Parse(myReader[1].ToString());
                            lEnt.CD_TIPO_ASSINATURA= int.Parse(myReader[2].ToString());
                            lEnt.CD_TIPO_ENTREGA = int.Parse(myReader[3].ToString());
                            lEnt.CD_FORMA_PAG = int.Parse(myReader[4].ToString());

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
                        _msgErro = "GetParametroPlano (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public PlanoComercialWebEntity GetValorPlanoSQL(int pintIDCampaha, int pintIDPlano, int pintIDLogr)
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
                        myComando.CommandText = "WS_VENDA_BUSCA_VALOR_PLANO";

                        SqlParameter  myParametro;

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_cd_campanha";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintIDCampaha;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_cd_plano";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintIDPlano;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_cd_logradouro";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintIDLogr;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_nu_parcelas";
                        myParametro.Direction = ParameterDirection.Output;
                        myParametro.DbType = DbType.String;
                        myParametro.Size = 50;
                        myParametro.Value = "";

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_va_parc1_cent";
                        myParametro.Direction = ParameterDirection.Output;
                        myParametro.DbType = DbType.String;
                        myParametro.Size = 50;
                        myParametro.Value = "";

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_va_demais_parc_cent";
                        myParametro.Direction = ParameterDirection.Output;
                        myParametro.DbType = DbType.String;
                        myParametro.Size = 50;
                        myParametro.Value = "";

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_va_total_cent";
                        myParametro.Direction = ParameterDirection.Output;
                        myParametro.DbType = DbType.String;
                        myParametro.Size = 50;
                        myParametro.Value = "";

                        myComando.Parameters.Add(myParametro);

                        DataContextSQL.ExecutarComando(myComando);

                        if (DataContext.Erro == 0)
                        {
                            PlanoComercialWebEntity lEnt = new PlanoComercialWebEntity();

                            string lstrParcela = myComando.Parameters["sp_nu_parcelas"].Value.ToString();
                            string lstrValorParc = myComando.Parameters["sp_va_parc1_cent"].Value.ToString();
                            string lstrValorDemaisParc = myComando.Parameters["sp_va_demais_parc_cent"].Value.ToString();
                            string lstrValorTotal = myComando.Parameters["sp_va_total_cent"].Value.ToString();

                            lEnt.NU_PARCELA = int.Parse(lstrParcela);
                            lEnt.VA_PARC_CENT = lstrValorParc;
                            lEnt.VA_DEMAIS_PARC_CENT = lstrValorDemaisParc;
                            lEnt.VA_TOTAL_CENT = lstrValorTotal;

                            return lEnt;
                        }
                        else
                        {
                            _erro = DataContext.Erro;
                            _msgErro = DataContext.MsgErro;

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
                _msgErro = "GetValorPlanoData - " + ex.Message;

                return null;

            }
            finally
            {
                if (DataContextSQL.ConexaoAberta())
                {
                    if (!DataContextSQL.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GetValorPlanoData - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public PlanoComercialWebEntity GetParametrosPlanoSQL(int pintIDPlano)
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
                        myComando.CommandText = "WS_VENDA_BUSCA_PARAMETRO_PLANO";

                        SqlParameter myParametro;

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "SP_CD_PLANO";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintIDPlano;

                        myComando.Parameters.Add(myParametro);

                        SqlDataReader myReader = DataContextSQL.ExecutarReader(myComando);

                        if (DataContextSQL.Erro == 0)
                        {
                            PlanoComercialWebEntity lEnt = new PlanoComercialWebEntity();

                            if (myReader.HasRows)
                            {

                                myReader.Read();

                                lEnt.CD_TP_ASSINATURA = int.Parse(myReader[0].ToString());
                                lEnt.DS_TP_ASSINATURA = myReader[1].ToString();
                                lEnt.CD_TP_ENTREGA = int.Parse(myReader[2].ToString());
                                lEnt.DS_TP_ENTREGA = myReader[3].ToString();
                                lEnt.DIAS_ENTREGA = myReader[4].ToString();
                                lEnt.CD_FORMA_PAG = int.Parse(myReader[5].ToString());
                                lEnt.DS_FORMA_PAG = myReader[6].ToString();
                                lEnt.CD_TP_PAG = int.Parse(myReader[7].ToString());
                                lEnt.NU_PARCELA = int.Parse(myReader[8].ToString());

                                lEnt.DS_PLANO = myReader[9].ToString();
                                lEnt.DS_TP_PAG = myReader[10].ToString();

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
                _msgErro = "GetParametrosPlanoData - " + ex.Message;
                return null;
            }
            finally
            {
                if (DataContextSQL.ConexaoAberta())
                {
                    if (!DataContextSQL.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetParametroPlano (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }
    }
}
