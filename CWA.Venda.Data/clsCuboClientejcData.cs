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
    public class CuboClientejcData
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

        public List<CuboClienteJCEntity> GetCuboClienteJCEntityList()
        {
            try 
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_CUBO_CLIENTE_JC";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);


                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            List<CuboClienteJCEntity> lcolEnt = new List<CuboClienteJCEntity>();
                            CuboClienteJCEntity lEnt;

                            while (myReader.Read())
                            {

                                lEnt = new CuboClienteJCEntity();

                                lEnt.COD_CLIENTE = int.Parse(myReader[0].ToString());
                                lEnt.NOME_CLIENTE = myReader[1].ToString();
                                lEnt.NOME_FANTASIA = myReader[2].ToString();
                                lEnt.TIPO_CLIENTE = myReader[3].ToString();
                                lEnt.DS_ESTADO_CIVIL = myReader[4].ToString();
                                lEnt.DS_PROFISSAO = myReader[5].ToString();
                                lEnt.CPF = myReader[6].ToString();
                                lEnt.CNPJ = myReader[7].ToString();
                                lEnt.DT_NASCIMENTO = myReader[8].ToString();
                                lEnt.TP_LOGRAD = myReader[9].ToString();
                                lEnt.NOME_LOGRAD = myReader[10].ToString();
                                lEnt.NUM_RESID = myReader[11].ToString();
                                lEnt.NU_APARTAMENTO = myReader[12].ToString();
                                lEnt.NU_BLOCO = myReader[13].ToString();
                                lEnt.COMPL_ENDERECO = myReader[14].ToString();
                                lEnt.NOME_BAIRRO = myReader[15].ToString();
                                lEnt.NOME_MUNICIPIO = myReader[16].ToString();
                                lEnt.CEP = myReader[17].ToString();
                                lEnt.ESTADO_UF = myReader[18].ToString();
                                lEnt.EMAIL = myReader[19].ToString();
                                lEnt.DDD_RES = myReader[20].ToString();
                                lEnt.FONE_RES = myReader[21].ToString();
                                lEnt.DDD_COM = myReader[22].ToString();
                                lEnt.FONE_COM = myReader[23].ToString();
                                lEnt.DDD_CEL = myReader[24].ToString();
                                lEnt.FONE_CEL = myReader[25].ToString();
                                lEnt.DT_CADASTRO = myReader[26].ToString();

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
                _msgErro = "CuboClientejcData (Lista) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "CuboClientejcData (Lista) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }


    }
}