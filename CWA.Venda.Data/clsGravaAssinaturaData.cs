using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

using CWA.Venda.Entity;
using System.Globalization;

namespace CWA.Venda.Data
{
    public class GravaAssinaturaData
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

        public void SetDadosPessoa(DadosPessoaEntity ObjPS,
                                   DadosEntregaEntity ObjDE,
                                   DadosPagamentoEntity ObjPG,
                                   string pstrSerieCTR,
                                   int pintNuCTR,
                                   int pintDvCTR,
                                   Boolean pbolAbrirTransacao,
                                   Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.StoredProcedure;
                myComando.CommandText = "SP_CADASTRO_PESSOA_BV_I_W_02";

                AddParmsPessoa(ref myComando, ObjPS, ObjDE, ObjPG, pstrSerieCTR, pintNuCTR, pintDvCTR);

                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    if (pbolAbrirTransacao)
                    {
                        //DataContext.RollbackTransaction();
                    }

                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GravaAssinaturaData (SetDadosPessoa) - " + ex.Message;
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
                            _msgErro = "GravaAssinaturaData (SetDadosPessoa) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        private void AddParmsPessoa(ref DbCommand pdbCommand,
                                    DadosPessoaEntity ObjPS,
                                    DadosEntregaEntity ObjDE,
                                    DadosPagamentoEntity ObjPG,
                                    string pstrSerieCTR,
                                    int pintNuCTR,
                                    int pintDvCTR)
        {
            DbParameter ldbParameter;

            //Dados Pessoais
            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_fonte_origem";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 0;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_entrada";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_registro";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 3;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_contabil_pessoa";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_pessoa";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.TP_PESSOA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_pessoa";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            //PESSOA FISICA
            if (ObjPS.TP_PESSOA == 1 || ObjPS.TP_PESSOA == 2)
            {
                ldbParameter.Value = ObjPS.DS_NOME.Replace("'"," ");
            }
            else
            {
                ldbParameter.Value = ObjPS.DS_NOME_EMPRESA.Replace("'", " ");
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_responsavel";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.DS_NOME_RESP != null)
            {
                ldbParameter.Value = ObjPS.DS_NOME_RESP.Replace("'", " ");
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_cadastro";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_email";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPS.DS_EMAIL.Replace("'", " ");

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cpf";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int64;
            if (ObjPS.NU_CPF != null)
            {
                ldbParameter.Value = long.Parse(ObjPS.NU_CPF.Replace(".", "").Replace("-", ""));
            }
            else
            {
                ldbParameter.Value = DBNull.Value;    
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_identidade";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_orgao_emissor";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_emissao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_estado_civil";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 1; //SOLTEIRO

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_nasc_fund";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            if (string.IsNullOrEmpty(ObjPS.DT_NASC))
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = DateTime.Parse(ObjPS.DT_NASC).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("pt-BR"));
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_fantasia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.NM_FANTASIA != null)
            {
                ldbParameter.Value = ObjPS.NM_FANTASIA.Replace("'", " ");
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cnpj";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int64;

            if (ObjPS.NU_CNPJ != null)
            {
                ldbParameter.Value = ObjPS.NU_CNPJ;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_inscr_mun";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.NU_IM != null)
            {
                ldbParameter.Value = ObjPS.NU_IM;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_inscr_est";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.NU_IE != null)
            {
                ldbParameter.Value = ObjPS.NU_IE;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_classe";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_vip";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_ramo";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            if (ObjPS.CD_RAMO_ATV != 0)
            {
                ldbParameter.Value = ObjPS.CD_RAMO_ATV;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_ind_divulgacao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.ST_IND_DIVULGACAO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_serie_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = pstrSerieCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintNuCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dv_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintDvCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_produto";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.CD_PRODUTO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_campanha";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.CD_CAMPANHA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_plano";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.CD_PLANO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_repr_venda";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.CD_REPR_VENDA;  //544741;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_vend";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.CD_VENDEDOR; //544741;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_corporate";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_qtd_produto";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 1;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_venda";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_inicio";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_logradouro_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;

            if (ObjDE.ST_CEP_UNICO == 0)
            {
                ldbParameter.Value = ObjDE.ID_LOGR;
            }
            else
            {
                ldbParameter.Value = 0;
            }
            

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_residencia_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjDE.NU_RESID;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_compl_residencia_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_bloco_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_apartamento_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_complemento_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjDE.DS_COMPL != null)
            {
                ldbParameter.Value = ObjDE.DS_COMPL.Replace("'", " ");
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }

           

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_ponto_ref_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_local_entrega_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.CD_LOCAL_ENTREGA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_endereco_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if ( ObjDE.ID_LOGR != 0)
            {
                ldbParameter.Value = ObjDE.DS_LOGR.Replace("'", " ");
            }
            else
            {
                ldbParameter.Value = ObjDE.DS_TIPO.Replace("'", " ") + " " + ObjDE.DS_LOGR.Replace("'", " ");
            }
            

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_bairro_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjDE.DS_BAIRRO.Replace("'", " ");

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_municipio_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjDE.DS_MUNICIPIO.Replace("'", " ");

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_uf_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjDE.DS_UF;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cep_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjDE.NU_CEP.Replace("-","");

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_donante";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_pessoa_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cpf_cnpj_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int64;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_nasc_fund_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_email_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_logradouro_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_residencia_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_compl_res_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_bloco_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_apartamento_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_complemento_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_ponto_ref_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_endereco_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_bairro_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_municipio_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_uf_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cep_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_contabil_donante";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_envio_roteirizacao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cod_remessa";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_transf_cadastro";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_ind_erro";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_situacao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_motivo";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_codigo_grupo";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.CD_GRUPO_SELECAO; //5;
            //verificar melhor formas

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cod_terceiro";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_serie_ctr_corp";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ctr_corp";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dv_ctr_corp";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_lote";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_empresa";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.DS_NOME_EMPRESA != null)
            {
                ldbParameter.Value = ObjPS.DS_NOME_EMPRESA.Replace("'", " ");
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }           
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_cargo";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_empresa_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_cargo_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_midia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cod_matr_func";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_va_perc_desc";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Decimal;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_parc_cartao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.TIPO_PARC_CARTAO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_nome_abrev";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_ind_autoriza_email";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.ST_AUTORIZA_EMAIL; // 1;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_site";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_tp_tratamento";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_senha";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.DS_SENHA != null)
            {
                ldbParameter.Value = ObjPS.DS_SENHA;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_senha_lembrete";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = "";

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_facebook_id";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_sobrenome";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.DS_SOBRE_NOME != null)
            {
                ldbParameter.Value = ObjPS.DS_SOBRE_NOME.Replace("'", " ");
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cpf_resp";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int64;

            if (ObjPS.NU_CPF_RESP!= null)
            {
                ldbParameter.Value = ObjPS.NU_CPF_RESP;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_alterar_dados";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;

            if (ObjPS.ALTERAR_DADOS != 0)
            {
                ldbParameter.Value = ObjPS.ALTERAR_DADOS;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }


            pdbCommand.Parameters.Add(ldbParameter);

            

        }


        public void SetDadosTelefone(DadosPessoaEntity ObjPS,
                                    string pstrSerieCTR,
                                    int pintNuCTR,
                                    int pintDvCTR,
                                    int pintSeq,
                                    Boolean pbolAbrirTransacao,
                                    Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.StoredProcedure;
                myComando.CommandText = "SP_TELEFONE_ASSINANTE_BV_I_W_02";

                AddParmsTelefone(ref myComando, ObjPS, pstrSerieCTR, pintNuCTR, pintDvCTR, pintSeq);

                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {
                    if (pbolAbrirTransacao)
                    {
                        //DataContext.RollbackTransaction();
                    }

                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                }

            }
            catch (Exception ex)
            {

                //if (pbolAbrirTransacao)
                //{
                //    DataContext.RollbackTransaction();
                //}

                _erro = -1;
                _msgErro = "GravaAssinaturaData (SetDadosTelefone) - " + ex.Message;
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
                            _msgErro = "GravaAssinaturaData (SetDadosTelefone) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        private void AddParmsTelefone(ref DbCommand pdbCommand,
                                    DadosPessoaEntity ObjPS,
                                    string pstrSerieCTR,
                                    int pintNuCTR,
                                    int pintDvCTR,
                                    int pintSeq)
        {
            DbParameter ldbParameter;

            //Dados Telefone
            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_serie_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = pstrSerieCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintNuCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dv_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintDvCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_seq";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintSeq;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_tp_telefone";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            if (pintSeq == 0)
            {
                ldbParameter.Value = 1; //residencial;
            }
            else
            {
                ldbParameter.Value = 3; //celular;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ddd";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            if (pintSeq == 0)
            {
                ldbParameter.Value = int.Parse(ObjPS.NU_DDDTEL.Replace("(","").Replace(")",""));
            }
            else
            {
                ldbParameter.Value = int.Parse(ObjPS.NU_DDDCEL.Replace("(", "").Replace(")", ""));
            }
            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_tel";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String ;
            if (pintSeq == 0)
            {
                ldbParameter.Value = ObjPS.NU_TEL.Replace("(", "").Replace(")", "");
            }
            else
            {
                ldbParameter.Value = ObjPS.NU_CEL.Replace("(", "").Replace(")", "");
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ramal";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_obs";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ddi";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

        }

        public void SetDadosDebito(DadosPagamentoEntity ObjPG,
                                string pstrSerieCTR,
                                int pintNuCTR,
                                int pintDvCTR,
                                Boolean pbolAbrirTransacao,
                                Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.StoredProcedure;
                myComando.CommandText = "SP_DEBITO_AUTOMATICO_BV_I_W_03";

                if (ObjPG.CD_TP_FORMA_PAG == 1) //DEBITO
                {
                    AddParmsDebConta(ref myComando, ObjPG, pstrSerieCTR, pintNuCTR, pintDvCTR);
                }
                else if (ObjPG.CD_TP_FORMA_PAG == 6) //CARTAO
                {
                    AddParmsDebCartao(ref myComando, ObjPG, pstrSerieCTR, pintNuCTR, pintDvCTR);
                }


                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro != 0)
                {                    
                    if (pbolAbrirTransacao)
                    {
                        //DataContext.RollbackTransaction();
                    }

                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;
                }

            }
            catch (Exception ex)
            {

                //if (pbolAbrirTransacao)
                //{
                //    DataContext.RollbackTransaction();
                //}

                _erro = -1;
                _msgErro = "GravaAssinaturaData (SetDadosDebito) - " + ex.Message;
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
                            _msgErro = "GravaAssinaturaData (SetDadosDebito) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        private void AddParmsDebCartao(ref DbCommand pdbCommand,
                                       DadosPagamentoEntity ObjPG,
                                       string pstrSerieCTR,
                                       int pintNuCTR,
                                       int pintDvCTR)
        {
            DbParameter ldbParameter;

            //Dados Cartao
            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_serie_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = pstrSerieCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintNuCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dv_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintDvCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_fonte_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.CD_FONTE_COBRANCA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 1; //CARTAO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_titular";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.NM_PESSOA_CARTAO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dia_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            if (ObjPG.MELHOR_DIA_CARTAO != 0)
            {
                ldbParameter.Value = ObjPG.MELHOR_DIA_CARTAO;
            }
            else
            {
                ldbParameter.Value = 0;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cartao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjPG.NU_CARTAO != null)
            {
                ldbParameter.Value = ObjPG.NU_CARTAO;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cvv";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjPG.NU_CVV !=  null)
            {
                ldbParameter.Value = ObjPG.NU_CVV;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_valid_cartao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            if (ObjPG.DT_VALID != null)
            {
                ldbParameter.Value = DateTime.Parse(ObjPG.DT_VALID).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("pt-BR"));
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_banco";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_agencia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_DV_agencia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_conta";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_DV_conta";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cpf_cnpj";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_ind_selecao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 1;

            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_DS_KEY_ENCRYPT";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if ( !string.IsNullOrEmpty(ObjPG.CHAVE_CRYPT_CC))
            {
                ldbParameter.Value = ObjPG.CHAVE_CRYPT_CC;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            
            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_DS_TOKENCARD";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (!string.IsNullOrEmpty(ObjPG.CARDTOKEN))
            {
                ldbParameter.Value = ObjPG.CARDTOKEN;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }

            pdbCommand.Parameters.Add(ldbParameter);


        }

        private void AddParmsDebConta(ref DbCommand pdbCommand,
                                      DadosPagamentoEntity ObjPG,
                                      string pstrSerieCTR,
                                      int pintNuCTR,
                                      int pintDvCTR)
        {
            DbParameter ldbParameter;

            //Dados Debito
            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_serie_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = pstrSerieCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintNuCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dv_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintDvCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_fonte_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.CD_FONTE_COBRANCA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 2; //DEBITO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_titular";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.NM_RESP_CONTA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dia_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.MELHOR_DIA_DEBITO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cartao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cvv";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_valid_cartao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_banco";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.NU_BANCO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_agencia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.NU_AGENCIA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_DV_agencia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.DV_AGENCIA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_conta";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.NU_CONTA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_DV_conta";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.DV_CONTA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cpf_cnpj";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.CPFCNPJ_DEB;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_ind_selecao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 1;

            pdbCommand.Parameters.Add(ldbParameter);

        }


        public string GetProximoContrato(string pstrOrigem, int pintIDOperador, Boolean pbolAbrirTransacao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {

                    if (pbolAbrirTransacao)
                    {
                        DataContext.BeginTransaction();
                    }

                    string lstrComposicaoCTR = "";

                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_ULT_CONTRATO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_DS_ORIGEM";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrOrigem;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_CD_CONTABIL_OPERADOR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintIDOperador;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_STR_CONTRATO";
                    myParametro.Direction = ParameterDirection.Output;
                    myParametro.DbType = DbType.String;
                    myParametro.Size = 50;
                    myParametro.Value = lstrComposicaoCTR;

                    myComando.Parameters.Add(myParametro);

                    DataContext.ExecutarComando(myComando);

                    if (DataContext.Erro == 0)
                    {
                        lstrComposicaoCTR = myComando.Parameters["SP_STR_CONTRATO"].Value.ToString();

                        if (pbolAbrirTransacao)
                        {
                            DataContext.CommitTransaction();
                        }

                        return lstrComposicaoCTR;
                    }
                    else
                    {
                        if (pbolAbrirTransacao)
                        {
                            DataContext.RollbackTransaction();
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return "";

                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return "";
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GravaAssinaturaData (GetProximoContrato) - " + ex.Message;

                return "";
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GravaAssinaturaData (GetProximoContrato) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }


        public void SetBaixaParcela(string pstrSerieCTR,
                                    int pintCTR,
                                    int pintDvCTR,
                                    string pstrCodAutorizacao,
                                    string pstrComprovanteVenda,
                                    string pstrTransacAdquirente,
                                    string pstrIDPagamento,
                                    string pstrIDRecorrencia,
                                    int pintQtdParcelaPaga,
                                    int pintQtdParcelaEnviada,
                                    string pstrCardToken,
                                    Boolean pbolAbrirTransacao,
                                    Boolean pbolFecharConexao)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.StoredProcedure;
                myComando.CommandText = "WS_VENDA_REGISTRA_PAGAMENTO";

                DbParameter myParametro;

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_nu_serie_ctr";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.String;
                myParametro.Value = pstrSerieCTR;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_nu_ctr";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.Int32;
                myParametro.Value = pintCTR;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_nu_dv_ctr";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.Int32;
                myParametro.Value = pintDvCTR;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_loja_cod_aut_cartao";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.String;
                myParametro.Value = pstrCodAutorizacao;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_loja_nu_comprovante_venda";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.String;
                myParametro.Value = pstrComprovanteVenda;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_loja_id_trans_adquirente";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.String;
                myParametro.Value = pstrTransacAdquirente;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_loja_id_pagto";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.String;
                myParametro.Value = pstrIDPagamento;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_loja_id_recorrencia";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.String;
                myParametro.Value = pstrIDRecorrencia;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_loja_qtd_parc_paga";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.Int32;
                myParametro.Value = pintQtdParcelaPaga;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_loja_qtd_parc_enviada";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.Int32;
                myParametro.Value = pintQtdParcelaEnviada;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "sp_loja_tokencard";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.String;

                if (!string.IsNullOrEmpty(pstrCardToken))
                {
                    myParametro.Value = pstrCardToken;
                }
                else
                {
                    myParametro.Value = DBNull.Value;
                }

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

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GravaAssinaturaData (SetBaixaParcela) - " + ex.Message;
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
                            _msgErro = "GravaAssinaturaData (SetBaixaParcela) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }


        }


        public string[] GetVerificarAssinante(int pintTipoPessoa, string pstrDOC)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VERIFICAR_ASSINANTE";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_st_tp_pessoa";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintTipoPessoa;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_doc";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrDOC;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            string lstrIDPessoa = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                            string lstrSenhaAtual = myReader.IsDBNull(1) ? "" : myReader[1].ToString();

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return new string[] { lstrIDPessoa, lstrSenhaAtual};
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
                _msgErro = "GravaAssinaturaData (GetVerificarAssinante) - " + ex.Message;

                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GravaAssinaturaData (GetVerificarAssinante) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public string GetProximoContratonNOVO(string pstrOrigem, int pintIDOperador, Boolean pbolAbrirTransacao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";


                string lstrComposicaoCTR = "";

                DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                myComando.CommandType = CommandType.StoredProcedure;
                myComando.CommandText = "WS_VENDA_ULT_CONTRATO";

                DbParameter myParametro;

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "SP_DS_ORIGEM";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.String;
                myParametro.Value = pstrOrigem;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "SP_CD_CONTABIL_OPERADOR";
                myParametro.Direction = ParameterDirection.Input;
                myParametro.DbType = DbType.Int32;
                myParametro.Value = pintIDOperador;

                myComando.Parameters.Add(myParametro);

                myParametro = myComando.CreateParameter();
                myParametro.ParameterName = "SP_STR_CONTRATO";
                myParametro.Direction = ParameterDirection.Output;
                myParametro.DbType = DbType.String;
                myParametro.Size = 50;
                myParametro.Value = lstrComposicaoCTR;

                myComando.Parameters.Add(myParametro);

                DataContext.ExecutarComando(myComando);

                if (DataContext.Erro == 0)
                {
                    lstrComposicaoCTR = myComando.Parameters["SP_STR_CONTRATO"].Value.ToString();

                    return lstrComposicaoCTR;
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return "";

                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GravaAssinaturaData (GetProximoContratoNOVO) - " + ex.Message;

                return "";
            }

        }

        //#######################################################
        //SQL
        //#######################################################
        public string GetProximoContratoSQL(string pstrOrigem, int pintIDOperador, Boolean pbolAbrirTransacao)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                using (SqlCommand myComando = new SqlCommand())
                {
                    if (DataContextSQL.AbrirConexao())
                    {

                        if (pbolAbrirTransacao)
                        {
                            DataContextSQL.BeginTransaction();
                        }

                        string lstrComposicaoCTR = "";

                        myComando.Connection = DataContextSQL.GetConnection;
                        if (pbolAbrirTransacao)
                        {
                            myComando.Transaction = DataContextSQL.GetTransaction; 
                        }
                        myComando.CommandType = CommandType.StoredProcedure;
                        myComando.CommandText = "WS_VENDA_ULT_CONTRATO";

                        SqlParameter  myParametro;

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "SP_DS_ORIGEM";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.String;
                        myParametro.Value = pstrOrigem;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "SP_CD_CONTABIL_OPERADOR";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintIDOperador;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "SP_STR_CONTRATO";
                        myParametro.Direction = ParameterDirection.Output;
                        myParametro.DbType = DbType.String;
                        myParametro.Size = 50;
                        myParametro.Value = lstrComposicaoCTR;

                        myComando.Parameters.Add(myParametro);

                        DataContextSQL.ExecutarComando(myComando);

                        if (DataContextSQL.Erro == 0)
                        {
                            lstrComposicaoCTR = myComando.Parameters["SP_STR_CONTRATO"].Value.ToString();

                            if (pbolAbrirTransacao)
                            {
                                DataContextSQL.CommitTransaction();
                            }

                            return lstrComposicaoCTR;
                        }
                        else
                        {
                            if (pbolAbrirTransacao)
                            {
                                DataContextSQL.RollbackTransaction();
                            }

                            _erro = DataContextSQL.Erro;
                            _msgErro = DataContextSQL.MsgErro;

                            return "";

                        }

                    }
                    else
                    {
                        _erro = DataContextSQL.Erro;
                        _msgErro = DataContextSQL.MsgErro;

                        return "";
                    }

                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GravaAssinaturaData (GetProximoContrato) - " + ex.Message;

                return "";
            }
            finally
            {
                if (DataContextSQL.ConexaoAberta())
                {
                    if (!DataContextSQL.FecharConexao())
                    {
                        _erro = -1;
                        _msgErro = "GravaAssinaturaData (GetProximoContrato) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public void SetDadosPessoaSQL(DadosPessoaEntity ObjPS,
                                       DadosEntregaEntity ObjDE,
                                       DadosPagamentoEntity ObjPG,
                                       string pstrSerieCTR,
                                       int pintNuCTR,
                                       int pintDvCTR,
                                       Boolean pbolAbrirTransacao,
                                       Boolean pbolFecharConexao)
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                using (SqlCommand myComando = new SqlCommand())
                {

                    myComando.Connection = DataContextSQL.GetConnection;

                    if (pbolAbrirTransacao)
                    {
                        myComando.Transaction = DataContextSQL.GetTransaction;
                    }

                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "SP_CADASTRO_PESSOA_BV_I_W_02";

                    SqlCommand myComandoParms = new SqlCommand();
                    myComandoParms = myComando;

                    AddParmsPessoaSQL(ref myComandoParms, ObjPS, ObjDE, ObjPG, pstrSerieCTR, pintNuCTR, pintDvCTR);

                    DataContextSQL.ExecutarComando(myComandoParms);

                    if (DataContextSQL.Erro != 0)
                    {
                        _erro = DataContextSQL.Erro;
                        _msgErro = DataContextSQL.MsgErro;
                    }
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GravaAssinaturaData (SetDadosPessoa) - " + ex.Message;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContextSQL.ConexaoAberta())
                    {
                        if (!DataContextSQL.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaAssinaturaData (SetDadosPessoa) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        private void AddParmsPessoaSQL(ref SqlCommand pdbCommand,
                                        DadosPessoaEntity ObjPS,
                                        DadosEntregaEntity ObjDE,
                                        DadosPagamentoEntity ObjPG,
                                        string pstrSerieCTR,
                                        int pintNuCTR,
                                        int pintDvCTR)
        {
            SqlParameter ldbParameter;

            //Dados Pessoais
            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_fonte_origem";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 0;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_entrada";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_registro";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 3;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_contabil_pessoa";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_pessoa";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.TP_PESSOA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_pessoa";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            //PESSOA FISICA
            if (ObjPS.TP_PESSOA == 1 || ObjPS.TP_PESSOA == 2)
            {
                ldbParameter.Value = ObjPS.DS_NOME;
            }
            else
            {
                ldbParameter.Value = ObjPS.DS_NOME_EMPRESA;
            }

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_responsavel";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.DS_NOME_RESP != null)
            {
                ldbParameter.Value = ObjPS.DS_NOME_RESP;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_cadastro";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_email";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPS.DS_EMAIL;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cpf";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int64;
            if (ObjPS.NU_CPF != null)
            {
                ldbParameter.Value = long.Parse(ObjPS.NU_CPF.Replace(".", "").Replace("-", ""));
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_identidade";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_orgao_emissor";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_emissao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_estado_civil";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 1; //SOLTEIRO

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_nasc_fund";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            if (string.IsNullOrEmpty(ObjPS.DT_NASC))
            {
                ldbParameter.Value = DBNull.Value;
            }
            else
            {
                ldbParameter.Value = DateTime.Parse(ObjPS.DT_NASC).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("pt-BR"));
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_fantasia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.NM_FANTASIA != null)
            {
                ldbParameter.Value = ObjPS.NM_FANTASIA;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cnpj";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int64;

            if (ObjPS.NU_CNPJ != null)
            {
                ldbParameter.Value = ObjPS.NU_CNPJ;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_inscr_mun";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.NU_IM != null)
            {
                ldbParameter.Value = ObjPS.NU_IM;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_inscr_est";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.NU_IE != null)
            {
                ldbParameter.Value = ObjPS.NU_IE;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_classe";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_vip";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_ramo";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            if (ObjPS.CD_RAMO_ATV != 0)
            {
                ldbParameter.Value = ObjPS.CD_RAMO_ATV;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_ind_divulgacao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.ST_IND_DIVULGACAO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_serie_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = pstrSerieCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintNuCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dv_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintDvCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_produto";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.CD_PRODUTO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_campanha";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.CD_CAMPANHA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_plano";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.CD_PLANO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_repr_venda";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.CD_REPR_VENDA;  //544741;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_vend";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.CD_VENDEDOR; //544741;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_corporate";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_qtd_produto";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 1;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_venda";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_inicio";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_logradouro_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;

            if (ObjDE.ST_CEP_UNICO == 0)
            {
                ldbParameter.Value = ObjDE.ID_LOGR;
            }
            else
            {
                ldbParameter.Value = 0;
            }


            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_residencia_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjDE.NU_RESID;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_compl_residencia_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_bloco_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_apartamento_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_complemento_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjDE.DS_COMPL != null)
            {
                ldbParameter.Value = ObjDE.DS_COMPL;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }



            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_ponto_ref_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_local_entrega_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.CD_LOCAL_ENTREGA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_endereco_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjDE.ID_LOGR != 0)
            {
                ldbParameter.Value = ObjDE.DS_LOGR;
            }
            else
            {
                ldbParameter.Value = ObjDE.DS_TIPO + " " + ObjDE.DS_LOGR;
            }


            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_bairro_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjDE.DS_BAIRRO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_municipio_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjDE.DS_MUNICIPIO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_uf_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjDE.DS_UF;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cep_e";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjDE.NU_CEP;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_donante";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_pessoa_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cpf_cnpj_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int64;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_nasc_fund_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_email_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_logradouro_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_residencia_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_compl_res_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_bloco_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_apartamento_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_complemento_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_ponto_ref_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_endereco_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_bairro_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_municipio_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_uf_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cep_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_contabil_donante";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_envio_roteirizacao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cod_remessa";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_transf_cadastro";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_ind_erro";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_situacao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_motivo";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_codigo_grupo";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.CD_GRUPO_SELECAO; //5;
            //verificar melhor formas

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cod_terceiro";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_serie_ctr_corp";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ctr_corp";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dv_ctr_corp";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_lote";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_empresa";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.DS_NOME_EMPRESA != null)
            {
                ldbParameter.Value = ObjPS.DS_NOME_EMPRESA;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_cargo";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_empresa_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_cargo_don";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_midia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cod_matr_func";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_va_perc_desc";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Decimal;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_parc_cartao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.TIPO_PARC_CARTAO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_nome_abrev";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_ind_autoriza_email";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPS.ST_AUTORIZA_EMAIL; // 1;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_site";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_tp_tratamento";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_senha";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.DS_SENHA != null)
            {
                ldbParameter.Value = ObjPS.DS_SENHA;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }


            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_senha_lembrete";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = "";

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_facebook_id";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_sobrenome";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;

            if (ObjPS.DS_SOBRE_NOME != null)
            {
                ldbParameter.Value = ObjPS.DS_SOBRE_NOME;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cpf_resp";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int64;

            if (ObjPS.NU_CPF_RESP != null)
            {
                ldbParameter.Value = ObjPS.NU_CPF_RESP;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_alterar_dados";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;

            if (ObjPS.ALTERAR_DADOS != 0)
            {
                ldbParameter.Value = ObjPS.ALTERAR_DADOS;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }


            pdbCommand.Parameters.Add(ldbParameter);



        }

        public void SetDadosTelefoneSQL(DadosPessoaEntity ObjPS,
                                        string pstrSerieCTR,
                                        int pintNuCTR,
                                        int pintDvCTR,
                                        int pintSeq,
                                        Boolean pbolAbrirTransacao,
                                        Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                using (SqlCommand myComando = new SqlCommand())
                {
                    myComando.Connection = DataContextSQL.GetConnection;

                    if (pbolAbrirTransacao )
                    {
                        myComando.Transaction = DataContextSQL.GetTransaction;
                    }
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "SP_TELEFONE_ASSINANTE_BV_I_W_02";

                    SqlCommand myComandoParms = new SqlCommand();
                    myComandoParms = myComando;

                    AddParmsTelefoneSQL(ref myComandoParms, ObjPS, pstrSerieCTR, pintNuCTR, pintDvCTR, pintSeq);

                    DataContextSQL.ExecutarComando(myComandoParms);

                    if (DataContextSQL.Erro != 0)
                    {
                        _erro = DataContextSQL.Erro;
                        _msgErro = DataContextSQL.MsgErro;
                    }
                }

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = "GravaAssinaturaData (SetDadosTelefone) - " + ex.Message;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContextSQL.ConexaoAberta())
                    {
                        if (!DataContextSQL.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaAssinaturaData (SetDadosTelefone) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        private void AddParmsTelefoneSQL(ref SqlCommand pdbCommand,
                                        DadosPessoaEntity ObjPS,
                                        string pstrSerieCTR,
                                        int pintNuCTR,
                                        int pintDvCTR,
                                        int pintSeq)
        {
            SqlParameter ldbParameter;

            //Dados Telefone
            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_serie_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = pstrSerieCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintNuCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dv_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintDvCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_seq";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintSeq;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_tp_telefone";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            if (pintSeq == 0)
            {
                ldbParameter.Value = 1; //residencial;
            }
            else
            {
                ldbParameter.Value = 3; //celular;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ddd";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            if (pintSeq == 0)
            {
                ldbParameter.Value = int.Parse(ObjPS.NU_DDDTEL.Replace("(", "").Replace(")", ""));
            }
            else
            {
                ldbParameter.Value = int.Parse(ObjPS.NU_DDDCEL.Replace("(", "").Replace(")", ""));
            }
            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_tel";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (pintSeq == 0)
            {
                ldbParameter.Value = ObjPS.NU_TEL.Replace("(", "").Replace(")", "");
            }
            else
            {
                ldbParameter.Value = ObjPS.NU_CEL.Replace("(", "").Replace(")", "");
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ramal";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_ds_obs";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ddi";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

        }

        public void SetDadosDebitoSQL(DadosPagamentoEntity ObjPG,
                                    string pstrSerieCTR,
                                    int pintNuCTR,
                                    int pintDvCTR,
                                    Boolean pbolAbrirTransacao,
                                    Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                using (SqlCommand myComando = new SqlCommand())
                {
                    myComando.Connection = DataContextSQL.GetConnection;

                    if (pbolAbrirTransacao)
                    {
                        myComando.Transaction = DataContextSQL.GetTransaction;
                    }
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "SP_DEBITO_AUTOMATICO_BV_I_W_03";

                    SqlCommand myComandoParms = new SqlCommand();
                    myComandoParms = myComando;

                    if (ObjPG.CD_TP_FORMA_PAG == 1) //DEBITO
                    {
                        AddParmsDebContaSQL(ref myComandoParms, ObjPG, pstrSerieCTR, pintNuCTR, pintDvCTR);
                    }
                    else if (ObjPG.CD_TP_FORMA_PAG == 6) //CARTAO
                    {
                        AddParmsDebCartaoSQL(ref myComandoParms, ObjPG, pstrSerieCTR, pintNuCTR, pintDvCTR);
                    }

                    DataContextSQL.ExecutarComando(myComando);

                    if (DataContextSQL.Erro != 0)
                    {
                        _erro = DataContextSQL.Erro;
                        _msgErro = DataContextSQL.MsgErro;
                    }
                }

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = "GravaAssinaturaData (SetDadosDebito) - " + ex.Message;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContextSQL.ConexaoAberta())
                    {
                        if (!DataContextSQL.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaAssinaturaData (SetDadosDebito) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }

        }

        private void AddParmsDebCartaoSQL(ref SqlCommand pdbCommand,
                                       DadosPagamentoEntity ObjPG,
                                       string pstrSerieCTR,
                                       int pintNuCTR,
                                       int pintDvCTR)
        {
            SqlParameter ldbParameter;

            //Dados Cartao
            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_serie_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = pstrSerieCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintNuCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dv_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintDvCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_fonte_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.CD_FONTE_COBRANCA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 1; //CARTAO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_titular";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.NM_PESSOA_CARTAO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dia_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            if (ObjPG.MELHOR_DIA_CARTAO != 0)
            {
                ldbParameter.Value = ObjPG.MELHOR_DIA_CARTAO;
            }
            else
            {
                ldbParameter.Value = 0;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cartao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjPG.NU_CARTAO != null)
            {
                ldbParameter.Value = ObjPG.NU_CARTAO;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cvv";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (ObjPG.NU_CVV != null)
            {
                ldbParameter.Value = ObjPG.NU_CVV;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_valid_cartao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            if (ObjPG.DT_VALID != null)
            {
                ldbParameter.Value = DateTime.Parse(ObjPG.DT_VALID).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("pt-BR"));
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }
            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_banco";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_agencia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_DV_agencia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_conta";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_DV_conta";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cpf_cnpj";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_ind_selecao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 1;

            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_DS_KEY_ENCRYPT";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (!string.IsNullOrEmpty(ObjPG.CHAVE_CRYPT_CC))
            {
                ldbParameter.Value = ObjPG.CHAVE_CRYPT_CC;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }

            pdbCommand.Parameters.Add(ldbParameter);


            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "SP_DS_TOKENCARD";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            if (!string.IsNullOrEmpty(ObjPG.CARDTOKEN))
            {
                ldbParameter.Value = ObjPG.CARDTOKEN;
            }
            else
            {
                ldbParameter.Value = DBNull.Value;
            }

            pdbCommand.Parameters.Add(ldbParameter);


        }

        private void AddParmsDebContaSQL(ref SqlCommand pdbCommand,
                                          DadosPagamentoEntity ObjPG,
                                          string pstrSerieCTR,
                                          int pintNuCTR,
                                          int pintDvCTR)
        {
            SqlParameter ldbParameter;

            //Dados Debito
            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_serie_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = pstrSerieCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintNuCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dv_ctr";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = pintDvCTR;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_cd_fonte_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.CD_FONTE_COBRANCA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_tp_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 2; //DEBITO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nm_titular";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.NM_RESP_CONTA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_dia_debito";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.MELHOR_DIA_DEBITO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cartao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cvv";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_dt_valid_cartao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.DateTime;
            ldbParameter.Value = DBNull.Value;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_banco";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = ObjPG.NU_BANCO;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_agencia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.NU_AGENCIA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_DV_agencia";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.DV_AGENCIA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_conta";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.NU_CONTA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_DV_conta";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.DV_CONTA;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_nu_cpf_cnpj";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.String;
            ldbParameter.Value = ObjPG.CPFCNPJ_DEB;

            pdbCommand.Parameters.Add(ldbParameter);

            ldbParameter = pdbCommand.CreateParameter();
            ldbParameter.ParameterName = "sp_st_ind_selecao";
            ldbParameter.Direction = ParameterDirection.Input;
            ldbParameter.DbType = DbType.Int32;
            ldbParameter.Value = 1;

            pdbCommand.Parameters.Add(ldbParameter);

        }

        public void SetBaixaParcelaSQL(string pstrSerieCTR,
                                        int pintCTR,
                                        int pintDvCTR,
                                        string pstrCodAutorizacao,
                                        string pstrComprovanteVenda,
                                        string pstrTransacAdquirente,
                                        string pstrIDPagamento,
                                        string pstrIDRecorrencia,
                                        int pintQtdParcelaPaga,
                                        int pintQtdParcelaEnviada,
                                        string pstrCardToken,
                                        Boolean pbolAbrirTransacao,
                                        Boolean pbolFecharConexao)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                using (SqlCommand myComando = new SqlCommand())
                {

                    myComando.Connection = DataContextSQL.GetConnection;

                    if (pbolAbrirTransacao)
                    {
                        myComando.Transaction = DataContextSQL.GetTransaction;
                    }

                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_REGISTRA_PAGAMENTO";

                    SqlParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_nu_serie_ctr";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrSerieCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_nu_ctr";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_nu_dv_ctr";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintDvCTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_loja_cod_aut_cartao";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrCodAutorizacao;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_loja_nu_comprovante_venda";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrComprovanteVenda;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_loja_id_trans_adquirente";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrTransacAdquirente;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_loja_id_pagto";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrIDPagamento;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_loja_id_recorrencia";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrIDRecorrencia;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_loja_qtd_parc_paga";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintQtdParcelaPaga;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_loja_qtd_parc_enviada";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintQtdParcelaEnviada;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "sp_loja_tokencard";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;

                    if (!string.IsNullOrEmpty(pstrCardToken))
                    {
                        myParametro.Value = pstrCardToken;
                    }
                    else
                    {
                        myParametro.Value = DBNull.Value;
                    }

                    myComando.Parameters.Add(myParametro);

                    DataContextSQL.ExecutarComando(myComando);

                    if (DataContextSQL.Erro != 0)
                    {
                        _erro = DataContextSQL.Erro;
                        _msgErro = DataContextSQL.MsgErro;
                    }
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GravaAssinaturaData (SetBaixaParcela) - " + ex.Message;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContextSQL.ConexaoAberta())
                    {
                        if (!DataContextSQL.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaAssinaturaData (SetBaixaParcela) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }


        }

        public void SetStatusBaseValidSQL(string pstrSerieCTR,
                                          int pintCTR,
                                          int pintDvCTR,
                                          int pintIDPessoa,
                                          int pintNuPeriodo,
                                          int pintTipoStatus,
                                          int pintStCorrecao,
                                          string pstrDtCorrecao,
                                          Boolean pbolAbrirTransacao,
                                          Boolean pbolFecharConexao)
        {

            try
            {

                _erro = 0;
                _msgErro = "";

                using (SqlCommand myComando = new SqlCommand())
                {
                    if (DataContextSQL.AbrirConexao())
                    {

                        if (pbolAbrirTransacao)
                        {
                            DataContextSQL.BeginTransaction();
                        }

                        myComando.Connection = DataContextSQL.GetConnection;
                        if (pbolAbrirTransacao)
                        {
                            myComando.Transaction = DataContextSQL.GetTransaction;
                        }

                        myComando.CommandType = CommandType.StoredProcedure;
                        myComando.CommandText = "SP_BASE_VALID_STATUS_Principal_U_01";

                        SqlParameter myParametro;

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_nu_serie_ctr";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.String;
                        myParametro.Value = pstrSerieCTR;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_cd_contabil_pessoa";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintIDPessoa;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_nu_ctr";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintCTR;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_nu_dv_ctr";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintDvCTR;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_nu_periodo";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintNuPeriodo;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_cd_tp_status";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintTipoStatus;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_st_ind_correcao";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.Int32;
                        myParametro.Value = pintStCorrecao;

                        myComando.Parameters.Add(myParametro);

                        myParametro = myComando.CreateParameter();
                        myParametro.ParameterName = "sp_dt_correcao";
                        myParametro.Direction = ParameterDirection.Input;
                        myParametro.DbType = DbType.DateTime;
                        myParametro.Value = pstrDtCorrecao;

                        myComando.Parameters.Add(myParametro);

                        DataContextSQL.ExecutarComando(myComando);

                        if (DataContextSQL.Erro != 0)
                        {
                           _erro = DataContextSQL.Erro;
                           _msgErro = DataContextSQL.MsgErro;

                            DataContextSQL.RollbackTransaction();
                            return;
                        }
                        else
                        {
                            DataContextSQL.CommitTransaction();
                            return;
                        }

                    }
                    else
                    {
                        _erro = DataContextSQL.Erro;
                        _msgErro = DataContextSQL.MsgErro;

                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GravaAssinaturaData (SetStatusBaseValidSQL) - " + ex.Message;
            }
            finally
            {
                if (pbolFecharConexao)
                {
                    if (DataContextSQL.ConexaoAberta())
                    {
                        if (!DataContextSQL.FecharConexao())
                        {
                            _erro = -1;
                            _msgErro = "GravaAssinaturaData (SetStatusBaseValidSQL) - Erro ao fechar conexão com o banco de dados.";
                        }
                    }
                }
            }


        }
    }
}

