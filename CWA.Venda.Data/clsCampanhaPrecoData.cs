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
    public class CampanhaPrecoData
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

        public List<CampanhaPrecoEntity> GetCampanhaPreco(int pintIDCampanha, int pintIDPlano)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCAR_CAMPANHA_PRECO";

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

                        if (myReader.HasRows)
                        {
                            List<CampanhaPrecoEntity> lcolEnt = new List<CampanhaPrecoEntity>();
                            CampanhaPrecoEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new CampanhaPrecoEntity();

                                lEnt.CD_CAMPANHA = int.Parse(myReader[0].ToString());
                                lEnt.CD_PLANO = int.Parse(myReader[1].ToString());
                                lEnt.CD_GRUPO_PRECO_ED = int.Parse(myReader[2].ToString());
                                lEnt.DT_VIGOR = DateTime.Parse(myReader[3].ToString());

                                lEnt.VA_PARC1_NOVAS = double.Parse(myReader[4].ToString());
                                lEnt.VA_DEMAIS_PARC1_NOVAS = double.Parse(myReader[5].ToString());
                                lEnt.VA_TOTAL_NOVA = double.Parse(myReader[6].ToString());

                                lEnt.VA_PARC1_RENOVA = double.Parse(myReader[7].ToString());
                                lEnt.VA_DEMAIS_PARC1_RENOVA = double.Parse(myReader[8].ToString());
                                lEnt.VA_TOTAL_RENOVA = double.Parse(myReader[9].ToString());


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
                        _msgErro = "GetCampanhaPlano (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

    }
}
