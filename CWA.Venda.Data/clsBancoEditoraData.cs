using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

using CWA.Venda.Entity;

using System.Configuration;

namespace CWA.Venda.Data
{
    public class BancoEditoraData
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

        public BancoEditoraEntity GetBancoEditora(int pintIDBancoPortador, int? pintIDEditora = null, string[] pstrVetorConVB6 = null)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao(pstrVetorConVB6))
                {

                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCARBANCOEDITORA";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_CONTABIL_BANCO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDBancoPortador;

                    myComando.Parameters.Add(myParametro);

                    if(pintIDEditora != null)
                    {
                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "SP_CD_CONTABIL_EDITORA";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintIDEditora;

                        myComando.Parameters.Add(myParametro);
                    }

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        BancoEditoraEntity lEnt = new BancoEditoraEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.CODIGO_BANCO = myReader.IsDBNull(0) ? 0 : int.Parse(myReader[0].ToString());
                            lEnt.NU_AGENCIA_BOL_ED = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                            lEnt.NU_DV_AGENCIA = myReader.IsDBNull(2) ? "" : myReader[2].ToString();

                            lEnt.NU_CONTA_BOL_ED = myReader.IsDBNull(3) ? "" : myReader[3].ToString();
                            lEnt.NU_DV_CONTA = myReader.IsDBNull(4) ? "" : myReader[4].ToString();

                            lEnt.NU_LAYOUT_BOL_ED = myReader.IsDBNull(5) ? 0 : int.Parse(myReader[5].ToString());

                            lEnt.NU_CARTEIRA_ED = myReader.IsDBNull(6) ? "" : myReader[6].ToString();
                            lEnt.NU_CONVENIO_BOL_ED = myReader.IsDBNull(7) ? "" : myReader[7].ToString();
                            lEnt.DS_DIR_ENVIO_BOL_ED = myReader.IsDBNull(8) ? "" : myReader[8].ToString();

                            lEnt.DS_ASS_BOLETO_GERAL_BOL_ED = myReader.IsDBNull(9) ? "" : myReader[9].ToString();
                            lEnt.DS_ASS_BOLETO_MSG1_BOL_ED = myReader.IsDBNull(10) ? "" : myReader[10].ToString();
                            lEnt.DS_ASS_BOLETO_MSG2_BOL_ED = myReader.IsDBNull(11) ? "" : myReader[11].ToString();
                            lEnt.DS_ASS_BOLETO_MSG3_BOL_ED = myReader.IsDBNull(12) ? "" : myReader[12].ToString();
                            lEnt.DS_ASS_BOLETO_MSG4_BOL_ED = myReader.IsDBNull(13) ? "" : myReader[13].ToString();
                            lEnt.DS_ASS_BOLETO_MSG5_BOL_ED = myReader.IsDBNull(14) ? "" : myReader[14].ToString();
                            lEnt.DS_ASS_BOLETO_MSG6_BOL_ED = myReader.IsDBNull(15) ? "" : myReader[15].ToString();
                            lEnt.DS_ASS_BOLETO_MSG7_BOL_ED = myReader.IsDBNull(16) ? "" : myReader[16].ToString();

                            lEnt.NU_BOL_TIPO_REGISTRO_ED = myReader.IsDBNull(17) ? 0 : int.Parse(myReader[17].ToString());
                            lEnt.NM_BENEFICIARIO = myReader.IsDBNull(18) ? "" : myReader[18].ToString();

                            lEnt.NU_CNPJ_BENEFICIARIO = myReader.IsDBNull(19) ? "00000000000000" : String.Format(@"{0:00\.000\.000\/0000\-00}", Int64.Parse(myReader[19].ToString()));
                            lEnt.NU_CNPJ_BENEFICIARIO = "CNPJ: " + lEnt.NU_CNPJ_BENEFICIARIO;

                            lEnt.NM_LOCAL_PAGAMENTO = myReader.IsDBNull(20) ? "" : myReader[20].ToString();

                            lEnt.DS_ENDERECO = myReader.IsDBNull(21) ? "" : myReader[21].ToString();

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
                        _msgErro = "GetBancoEditora (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

    }
}
