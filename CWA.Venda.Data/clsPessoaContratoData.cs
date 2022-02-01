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
    public class PessoaContratoData
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

        public List<PessoaCadastroEntity> GetCadastroPessoaContrato(Int32 pintIDPessoa,
                                                                    string pdtDtCadInicial,
                                                                    string pdtDtCadFinal,
                                                                    int pintSituacao)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_BUSCA_PESSOA_CADASTRO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    if (pintIDPessoa == 0)
                    {
                        myParametro.Value = DBNull.Value;
                    }
                    else
                    {
                        myParametro.Value = pintIDPessoa;
                    }
                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_DT_BASE_INI";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    if (pdtDtCadInicial == "")
                    {
                        myParametro.Value = DBNull.Value;
                    }
                    else
                    {
                        myParametro.Value = pdtDtCadInicial;
                    }
                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_DT_BASE_FIM";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    if (pdtDtCadFinal == "")
                    {
                        myParametro.Value = DBNull.Value;
                    }
                    else
                    {
                        myParametro.Value = pdtDtCadFinal;
                    }
                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_ST_SITUACAO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;                    
                    myParametro.Value = pintSituacao;                   
                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<PessoaCadastroEntity> lcolEnt = new List<PessoaCadastroEntity>();


                            //=================================================================================
                            //criar objetos de preenchimento
                            //=================================================================================
                            PessoaCadastroEntity ObjPS = new PessoaCadastroEntity();
                            ContratoPessoaEntity ObjEntCTR = new ContratoPessoaEntity();

                            List<ContratoPessoaEntity> ObjListCTR = new List<ContratoPessoaEntity>();
                            List<ContratoPeriodoEntity> ObjListPER = new List<ContratoPeriodoEntity>();
                            List<ContratoEnderecoEntity> ObjListEND = new List<ContratoEnderecoEntity>();
                            List<ContratoDependenteEntity> ObjListDEP = new List<ContratoDependenteEntity>();

                            List<PessoaTelefoneEntity> ObjListTEL = new List<PessoaTelefoneEntity>();
                            List<PessoaEmailEntity> ObjListMAIL = new List<PessoaEmailEntity>();
                            //=================================================================================

                            Int32 lintRegP = 0;
                            Int32 lintRegC = 0;
                            Int32 lintIDPessoa = 0;
                            Int32 lintCTR = 0;
                            int lintDV = 0;
                            string lstrSerie = "";

                            while (myReader.Read())
                            {

                                if (lintIDPessoa != int.Parse(myReader[2].ToString()))
                                {
                                    if (lintRegP != 0)
                                    {

                                        ObjEntCTR.ListaDependente = ObjListDEP;
                                        ObjEntCTR.ListaEndereco = ObjListEND;
                                        ObjEntCTR.ListaPeriodo = ObjListPER;

                                        ObjListCTR.Add(ObjEntCTR);

                                        ObjPS.ListaContrato = ObjListCTR;
                                        ObjPS.ListaEmail = ObjListMAIL;
                                        ObjPS.ListaTelefone = ObjListTEL;

                                        lcolEnt.Add(ObjPS);
                                    }

                                    lintRegP++;

                                    lintRegC = 0;
                                    lintCTR = 0;
                                    lintDV = 0;
                                    lstrSerie = "";

                                    lintIDPessoa = Int32.Parse(myReader[2].ToString());

                                    ObjPS = new PessoaCadastroEntity();

                                    //myReader.IsDBNull(0) ? "" : myReader[0].ToString();

                                    if (int.Parse(myReader[1].ToString()) == 1)  
                                    {
                                        ObjPS.TP_REG = int.Parse(myReader[1].ToString());
                                        ObjPS.CD_CONTABIL_PESSOA = Int32.Parse(myReader[2].ToString());
                                        ObjPS.NU_CTR = int.Parse(myReader[3].ToString());
                                        ObjPS.NM_PESSOA = myReader.IsDBNull(27)? "": myReader[27].ToString().Trim();

                                        if (!myReader.IsDBNull(17))
                                        {
                                            ObjPS.DT_NASC_FUND = DateTime.Parse(myReader[17].ToString());
                                        }

                                        ObjPS.ST_TP_PESSOA = myReader.IsDBNull(28)?  "": myReader[28].ToString().Trim();
                                        ObjPS.NU_CPF_CNPJ = myReader.IsDBNull(29)? "": myReader[29].ToString().Trim();
                                        ObjPS.ST_ESTADO_CIVIL = myReader.IsDBNull(30)? "": myReader[30].ToString().Trim();
                                        ObjPS.ST_IND_DIVULGACAO = myReader.IsDBNull(31)? "": myReader[31].ToString().Trim();
                                        ObjPS.NM_RESPONSAVEL = myReader.IsDBNull(32)? "": myReader[32].ToString().Trim();
                                        ObjPS.ST_IND_AUTORIZA_EMAIL = myReader.IsDBNull(33)? "": myReader[33].ToString().Trim();
                                    }

                                    //==========================================================
                                    //Definir coleção de contrato/periodo/endereço/dependente
                                    //==========================================================
                                    ObjListCTR = new List<ContratoPessoaEntity>();
                                    ObjListPER = new List<ContratoPeriodoEntity>();
                                    ObjListEND = new List<ContratoEnderecoEntity>();
                                    ObjListDEP = new List<ContratoDependenteEntity>();

                                    //==========================================================
                                    //Definir coleção de pessoal/telefone
                                    //==========================================================
                                    ObjListTEL = new List<PessoaTelefoneEntity>();
                                    ObjListMAIL = new List<PessoaEmailEntity>();
                                    //==========================================================

                                }
                                else if (lintIDPessoa == int.Parse(myReader[2].ToString()))
                                {
                                    
                                    if (int.Parse(myReader[1].ToString()) == 2) // CONTRATO
                                    {
                                        Int32 iCTR = 0;
                                        int iDV = 0;
                                        string sSerie = "";

                                        iCTR = Int32.Parse(myReader[3].ToString());
                                        iDV = int.Parse(myReader[4].ToString());
                                        sSerie = myReader[27].ToString().Trim();

                                        if ((lintCTR != int.Parse(myReader[3].ToString())) ||
                                            (lintDV != int.Parse(myReader[4].ToString())) ||
                                            (lstrSerie != myReader[27].ToString().Trim())
                                           )
                                        {
                                            if (lintRegC != 0)
                                            {
                                                ObjEntCTR.ListaDependente = ObjListDEP;
                                                ObjEntCTR.ListaEndereco = ObjListEND;
                                                ObjEntCTR.ListaPeriodo = ObjListPER;

                                                ObjListCTR.Add(ObjEntCTR);
                                            }
                                            lintRegC++;

                                            ObjEntCTR = new ContratoPessoaEntity();

                                            lintCTR = int.Parse(myReader[3].ToString());
                                            lintDV = int.Parse(myReader[4].ToString());
                                            lstrSerie = myReader[27].ToString().Trim();

                                            ObjEntCTR.TP_REG = int.Parse(myReader[1].ToString());
                                            ObjEntCTR.CD_CONTABIL_PESSOA = Int32.Parse(myReader[2].ToString());
                                            ObjEntCTR.NU_CTR = int.Parse(myReader[3].ToString());
                                            ObjEntCTR.NU_SERIE_CTR = myReader.IsDBNull(27) ? "" : myReader[27].ToString().Trim();
                                            ObjEntCTR.NU_DV_CTR = myReader.IsDBNull(4) ? 0 : int.Parse(myReader[4].ToString());
                                            ObjEntCTR.ST_ESTADO_ATUAL = myReader.IsDBNull(5) ? 0 : int.Parse(myReader[5].ToString());
                                            ObjEntCTR.DS_ESTADO_ATUAL = myReader.IsDBNull(28) ? "" : myReader[28].ToString().Trim();

                                            if (!myReader.IsDBNull(17))
                                            {
                                                ObjEntCTR.DT_SUSCAN = DateTime.Parse(myReader[17].ToString());
                                            }

                                            ObjEntCTR.CD_MOTIVO = myReader.IsDBNull(6) ? 0 : int.Parse(myReader[6].ToString());
                                            ObjEntCTR.DS_MOTIVO = myReader.IsDBNull(29) ? "" : myReader[29].ToString().Trim();

                                            if (!myReader.IsDBNull(18))
                                            {
                                                ObjEntCTR.DT_SUSCAN = DateTime.Parse(myReader[18].ToString());
                                            }

                                            if (!myReader.IsDBNull(19))
                                            {
                                                ObjEntCTR.DT_ADESSAO_CLUBE = DateTime.Parse(myReader[19].ToString());
                                            }

                                            if (!myReader.IsDBNull(20))
                                            {
                                                ObjEntCTR.DT_ENVIO_CART_CLUBE = DateTime.Parse(myReader[20].ToString());
                                            }

                                            if (!myReader.IsDBNull(21))
                                            {
                                                ObjEntCTR.DT_VALID_CART_CLUBE = DateTime.Parse(myReader[21].ToString());
                                            }

                                            ObjEntCTR.NU_CTR_CORP = myReader.IsDBNull(6) ? 0 : int.Parse(myReader[6].ToString());
                                            ObjEntCTR.DS_EMAIL = myReader.IsDBNull(30) ? "" : myReader[30].ToString().Trim();

                                            //==========================================================
                                            //Definir coleção de periodo/endereço/dependente
                                            //==========================================================
                                            ObjListPER = new List<ContratoPeriodoEntity>();
                                            ObjListEND = new List<ContratoEnderecoEntity>();
                                            ObjListDEP = new List<ContratoDependenteEntity>();
                                            //==========================================================


                                        }

                                    }

                                    //===============================================================================
                                    //Dados do contrato
                                    //===============================================================================

                                    if (int.Parse(myReader[1].ToString()) == 3) // PERIODO
                                    {
                                        ContratoPeriodoEntity ObjEntPER = new ContratoPeriodoEntity();

                                        ObjEntPER.TP_REG = int.Parse(myReader[1].ToString());
                                        ObjEntPER.CD_CONTABIL_PESSOA = Int32.Parse(myReader[2].ToString());
                                        ObjEntPER.NU_CTR = int.Parse(myReader[3].ToString());
                                        ObjEntPER.NU_SERIE_CTR = myReader.IsDBNull(27) ? "" : myReader[27].ToString().Trim();
                                        ObjEntPER.NU_DV_CTR = myReader.IsDBNull(4) ? 0 : int.Parse(myReader[4].ToString());
                                        ObjEntPER.CD_PRODUTO = myReader.IsDBNull(5) ? 0 : int.Parse(myReader[5].ToString());
                                        ObjEntPER.NM_PRODUTO = myReader.IsDBNull(28) ? "" : myReader[28].ToString().Trim();
                                        ObjEntPER.QTD_PRODUTO = myReader.IsDBNull(6) ? 0 : int.Parse(myReader[6].ToString());

                                        if (!myReader.IsDBNull(17))
                                        {
                                            ObjEntPER.DT_INICIO = DateTime.Parse(myReader[17].ToString());
                                        }

                                        if (!myReader.IsDBNull(18))
                                        {
                                            ObjEntPER.DT_TERMINO = DateTime.Parse(myReader[18].ToString());
                                        }

                                        ObjEntPER.CD_CAMPANHA = myReader.IsDBNull(7) ? 0 : int.Parse(myReader[7].ToString());
                                        ObjEntPER.DS_CAMPANHA = myReader.IsDBNull(29) ? "" : myReader[29].ToString().Trim();
                                        ObjEntPER.CD_PLANO = myReader.IsDBNull(8) ? 0 : int.Parse(myReader[8].ToString());
                                        ObjEntPER.DS_PANO = myReader.IsDBNull(30) ? "" : myReader[30].ToString().Trim();
                                        ObjEntPER.CD_CONTABIL_REPR_VENDA = myReader.IsDBNull(9) ? 0 : int.Parse(myReader[9].ToString());
                                        ObjEntPER.NM_REPR_VENDA = myReader.IsDBNull(31) ? "" : myReader[31].ToString().Trim();
                                        ObjEntPER.CD_CONTABIL_VEND = myReader.IsDBNull(10) ? 0 : int.Parse(myReader[10].ToString());
                                        ObjEntPER.NM_VENDEDOR = myReader.IsDBNull(32) ? "" : myReader[32].ToString().Trim();
                                        ObjEntPER.CD_PRODUTO_DIGITAL = myReader.IsDBNull(11) ? 0 : int.Parse(myReader[11].ToString());
                                        ObjEntPER.NM_PRODUTO_DIGITAL = myReader.IsDBNull(33) ? "" : myReader[33].ToString().Trim();
                                        ObjEntPER.DS_TP_PAGAMENTO = myReader.IsDBNull(34) ? "" : myReader[34].ToString().Trim();

                                        ObjListPER.Add(ObjEntPER);

                                    }
                                    else if (int.Parse(myReader[1].ToString()) == 4) // ENDEREÇO
                                    {
                                        ContratoEnderecoEntity ObjEntEND = new ContratoEnderecoEntity();

                                        ObjEntEND.TP_REG = int.Parse(myReader[1].ToString());
                                        ObjEntEND.CD_CONTABIL_PESSOA = Int32.Parse(myReader[2].ToString());
                                        ObjEntEND.NU_CTR = int.Parse(myReader[3].ToString());
                                        ObjEntEND.NU_SERIE_CTR = myReader.IsDBNull(27) ? "" : myReader[27].ToString().Trim();
                                        ObjEntEND.NU_DV_CTR = myReader.IsDBNull(4) ? 0 : int.Parse(myReader[4].ToString());

                                        ObjEntEND.ST_TP_ENDERECO = myReader.IsDBNull(28) ? "" : myReader[28].ToString().Trim();
                                        ObjEntEND.NU_RESIDENCIA = myReader.IsDBNull(29) ? "" : myReader[29].ToString().Trim();
                                        ObjEntEND.NU_BLOCO = myReader.IsDBNull(30) ? "" : myReader[30].ToString().Trim();
                                        ObjEntEND.NU_APARTAMENTO = myReader.IsDBNull(31) ? "" : myReader[31].ToString().Trim();
                                        ObjEntEND.COMPL_RESIDENCIA = myReader.IsDBNull(32) ? "" : myReader[32].ToString().Trim();
                                        ObjEntEND.DS_TIPO_LOGRADOURO = myReader.IsDBNull(33) ? "" : myReader[33].ToString().Trim();
                                        ObjEntEND.DS_LOGRADOURO = myReader.IsDBNull(34) ? "" : myReader[34].ToString().Trim();
                                        ObjEntEND.DS_REGIAO = myReader.IsDBNull(35) ? "" : myReader[35].ToString().Trim();
                                        ObjEntEND.DS_ESTADO_FEDERACAO = myReader.IsDBNull(36) ? "" : myReader[36].ToString().Trim();
                                        ObjEntEND.DS_MUNICIPIO = myReader.IsDBNull(37) ? "" : myReader[37].ToString().Trim();
                                        ObjEntEND.DS_BAIRRO = myReader.IsDBNull(38) ? "" : myReader[38].ToString().Trim();
                                        ObjEntEND.NU_CEP = myReader.IsDBNull(39) ? "" : myReader[39].ToString().Trim();
                                        ObjEntEND.DS_UF = myReader.IsDBNull(40) ? "" : myReader[40].ToString().Trim();

                                        ObjListEND.Add(ObjEntEND);
                                    }
                                    else if (int.Parse(myReader[1].ToString()) == 5) // DEPENDENTE
                                    {
                                        ContratoDependenteEntity ObjEntDEP = new ContratoDependenteEntity();

                                        ObjEntDEP.TP_REG = int.Parse(myReader[1].ToString());
                                        ObjEntDEP.CD_CONTABIL_PESSOA = Int32.Parse(myReader[2].ToString());
                                        ObjEntDEP.NU_CTR = int.Parse(myReader[3].ToString());
                                        ObjEntDEP.NU_SERIE_CTR = myReader.IsDBNull(27) ? "" : myReader[27].ToString().Trim();
                                        ObjEntDEP.NU_DV_CTR = myReader.IsDBNull(4) ? 0 : int.Parse(myReader[4].ToString());

                                        if (!myReader.IsDBNull(17))
                                        {
                                            ObjEntDEP.DT_NASC = myReader[17].ToString();
                                        }
                                        ObjEntDEP.NU_SEQ = myReader.IsDBNull(5) ? 0 : int.Parse(myReader[5].ToString());
                                        ObjEntDEP.NM_DEPENDENTE = myReader.IsDBNull(28) ? "" : myReader[28].ToString().Trim();
                                        ObjEntDEP.ST_PARENTESCO = myReader.IsDBNull(29) ? "" : myReader[29].ToString().Trim();

                                        if (!myReader.IsDBNull(18))
                                        {
                                            ObjEntDEP.DT_ADESSAO_CLUBE = DateTime.Parse(myReader[18].ToString());
                                        }

                                        if (!myReader.IsDBNull(19))
                                        {
                                            ObjEntDEP.DT_ENVIO_CART_CLUBE = DateTime.Parse(myReader[19].ToString());
                                        }

                                        if (!myReader.IsDBNull(20))
                                        {
                                            ObjEntDEP.DT_VALID_CART_CLUBE = DateTime.Parse(myReader[20].ToString());
                                        }

                                        ObjListDEP.Add(ObjEntDEP);
                                    }


                                    //===================================================================
                                    //Dados da pessoa
                                    //===================================================================
                                    if (int.Parse(myReader[1].ToString()) == 6) // TELEFONE
                                    {
                                        PessoaTelefoneEntity ObjEntTEL = new PessoaTelefoneEntity();

                                        ObjEntTEL.TP_REG = int.Parse(myReader[1].ToString());
                                        ObjEntTEL.CD_CONTABIL_PESSOA = Int32.Parse(myReader[2].ToString());

                                        ObjEntTEL.NU_SEQ = myReader.IsDBNull(3) ? 0 : int.Parse(myReader[3].ToString());
                                        ObjEntTEL.NU_DDD = myReader.IsDBNull(4) ? 0 : int.Parse(myReader[4].ToString());
                                        ObjEntTEL.NU_TEL = myReader.IsDBNull(27) ? "" : myReader[27].ToString().Trim();
                                        ObjEntTEL.NU_RAMAL = myReader.IsDBNull(28) ? "" : myReader[28].ToString().Trim();
                                        ObjEntTEL.ST_TP_TELEFONE = myReader.IsDBNull(29) ? "" : myReader[29].ToString().Trim();

                                        ObjListTEL.Add(ObjEntTEL);

                                    }

                                    else if (int.Parse(myReader[1].ToString()) == 7) //EMAIL
                                    {
                                        PessoaEmailEntity ObjEntMAIL = new PessoaEmailEntity();

                                        ObjEntMAIL.TP_REG = int.Parse(myReader[1].ToString());
                                        ObjEntMAIL.CD_CONTABIL_PESSOA = Int32.Parse(myReader[2].ToString());

                                        ObjEntMAIL.DS_EMAIL = myReader.IsDBNull(27) ? "" : myReader[27].ToString().Trim();
                                        ObjEntMAIL.ST_EMAIL_PRINCIPAL = myReader.IsDBNull(28) ? "" : myReader[28].ToString().Trim();
                                        ObjEntMAIL.DS_TIPO_EMAIL = myReader.IsDBNull(29) ? "" : myReader[29].ToString().Trim();

                                        ObjListMAIL.Add(ObjEntMAIL);
                                    }

                                }

                                //lcolEnt.Add(ObjPS);
                            }

                            //============================
                            //Trata último Registro
                            //============================
                            ObjEntCTR.ListaDependente = ObjListDEP;
                            ObjEntCTR.ListaEndereco = ObjListEND;
                            ObjEntCTR.ListaPeriodo = ObjListPER;

                            ObjListCTR.Add(ObjEntCTR);

                            ObjPS.ListaEmail = ObjListMAIL;
                            ObjPS.ListaTelefone = ObjListTEL;
                            ObjPS.ListaContrato = ObjListCTR;

                            lcolEnt.Add(ObjPS);
                            //============================

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
    }
}
