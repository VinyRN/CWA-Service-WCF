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
    public class ExtraData
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

        public List<ExtraEnt> GetRecibosGraficaSemPDF(string[] pstrVetorConVB6, string pstrQuery)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao(pstrVetorConVB6))
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.Text;
                    myComando.CommandText = pstrQuery;

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<ExtraEnt> lcolEnt = new List<ExtraEnt>();
                            ExtraEnt lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new ExtraEnt();

                                lEnt.TIPO = myReader[0].ToString();
                                lEnt.NU_RECIBO = int.Parse(myReader[1].ToString());
                                lEnt.NU_PARCELA = int.Parse(myReader[2].ToString());
                                lEnt.NU_DV = int.Parse(myReader[3].ToString());
                                lEnt.CD_EDITORA = int.Parse(myReader[4].ToString());
                                lEnt.CD_BANCO = int.Parse(myReader[5].ToString());

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
                        _msgErro = "GetCampanha (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }


    }

}
