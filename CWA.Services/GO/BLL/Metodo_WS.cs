using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Reflection;

using System.Configuration;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Activation;

using System.Web;

using System.Net.Http;
using System.Net.Http.Headers;


//using GeraDllIntegracaoGO.DAL;


namespace CWA.Services
{
    public class InterfacesWSGOVA_BLL
    {


        private string lsSQL;
        private string vbCr = " \r ";

        public string Dt_inicio = string.Empty;
        public string Dt_fim = string.Empty;

        public int FlagErro = 0;

        private string retorno_tela = string.Empty;


        public void VoltaTipoDaIs_mov_nf_remessa_devolucao()
        {
            try
            {
                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                lsSQL = "UPDATE" + vbCr;
                lsSQL = lsSQL + "IS_MOV_NF_REMESSA_DEVOLUCAO" + vbCr;
                lsSQL = lsSQL + "SET " + vbCr;
                lsSQL = lsSQL + "ST_SITUACAO = 0" + vbCr;
                lsSQL = lsSQL + "WHERE " + vbCr;
                lsSQL = lsSQL + "ST_SITUACAO = 2";

                VarBanco.lsSQL = lsSQL;
               
                ObjData.ExecutaSQL();


            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public void Cadastra_Retorno_Nota(int NuCDRegistro, 
                                           string Nu_nota, 
                                           string Nota_serie, 
                                           DateTime dt_transacao, 
                                           DateTime dt_mov, 
                                           string ds_user, 
                                           string chave)
        {

            try
            {
                int Nuseq = 0;

                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                lsSQL = "UPDATE" + vbCr;
                lsSQL = lsSQL + "IS_MOV_NF_REMESSA_DEVOLUCAO" + vbCr;
                lsSQL = lsSQL + "SET " + vbCr;
                lsSQL = lsSQL + "ST_SITUACAO = 1" + vbCr;
                lsSQL = lsSQL + "WHERE " + vbCr;
                lsSQL = lsSQL + "CD_REGISTRO = " + NuCDRegistro;

                VarBanco.lsSQL = lsSQL;
                
                ObjData.ExecutaSQL();

                lsSQL = "INSERT INTO IS_MOV_REMESSA_DEVOLUCAO_RETORNO(" + vbCr;
                lsSQL = lsSQL + "CD_REGISTRO," + vbCr;
                lsSQL = lsSQL + "ST_TP_MOVIMENTO," + vbCr;
                lsSQL = lsSQL + "ST_SITUACAO," + vbCr;
                lsSQL = lsSQL + "NU_NOTA_FISCAL," + vbCr;
                lsSQL = lsSQL + "NU_SERIE_NOTA_FISCAL," + vbCr;
                lsSQL = lsSQL + "DT_TRANSACAO," + vbCr;
                lsSQL = lsSQL + "DT_MOV," + vbCr;
                lsSQL = lsSQL + "DS_USR," + vbCr;
                lsSQL = lsSQL + "CD_CHAVE_ACESSO) VALUES (" + vbCr;
                lsSQL = lsSQL + NuCDRegistro + "," + vbCr;
                lsSQL = lsSQL + "1," + vbCr;
                lsSQL = lsSQL + "1," + vbCr;
                lsSQL = lsSQL + "'" + Nu_nota + "'," + vbCr;
                lsSQL = lsSQL + "'" + Nota_serie + "'," + vbCr;
                lsSQL = lsSQL + "'" + dt_transacao.Day.ToString("00") + "/" + dt_transacao.Month.ToString("00") + "/" + dt_transacao.Year.ToString("0000") + "'," + vbCr;
                lsSQL = lsSQL + "'" + dt_mov.Day.ToString("00") + "/" + dt_mov.Month.ToString("00") + "/" + dt_mov.Year.ToString("0000") + "'," + vbCr;
                lsSQL = lsSQL + "'" + ds_user + "'," + vbCr;
                lsSQL = lsSQL + "'" + chave + "')" + vbCr;

                VarBanco.lsSQL = lsSQL;
                ObjData.ExecutaSQL();

            }
            catch (Exception)
            {
                
                throw;
            }


        }

        public void Cadastra_Erro_Nota_sem_resposta(int NuCDRegistro, int OrigemErro, string ds_erro_go, string NomeUsuario)
        {

            try
            {
                int Nuseq = 0;
                int cd_erro = 0;
                int count = 0;
                int cd_CD_REGISTRO = 0;

                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                lsSQL = "SELECT COUNT(*) as cont FROM IS_MOV_NF_REMESSA_DEVOLUCAO WHERE ST_SITUACAO = 2";
                
                VarBanco.lsSQL = lsSQL;

                ObjData.RetornaSQL();

                if (VarBanco.Reader.Read())
                {
                    count = Convert.ToInt32(VarBanco.Reader["cont"].ToString());
                }
                VarBanco.Reader.Close();

                for (int i = 1; i <= count; i++)
                {

                    lsSQL = "SELECT top 1 CD_REGISTRO FROM IS_MOV_NF_REMESSA_DEVOLUCAO WHERE ST_SITUACAO = 2";
                    VarBanco.lsSQL = lsSQL;
                    ObjData.RetornaSQL();
                    if (VarBanco.Reader.Read())
                    {
                        cd_CD_REGISTRO = Convert.ToInt32(VarBanco.Reader["CD_REGISTRO"].ToString());
                    }
                    VarBanco.Reader.Close();


                    lsSQL = "SELECT CASE WHEN MAX(NU_SEQ) IS NULL THEN 0 ELSE MAX(NU_SEQ) END + 1 AS NUSEQ  FROM IS_MOV_REMESSA_DEVOLUCAO_ERROS" + vbCr;
                    VarBanco.lsSQL = lsSQL;
                    ObjData.RetornaSQL();
                    if (VarBanco.Reader.Read())
                    {
                        Nuseq = Convert.ToInt32(VarBanco.Reader["NUSEQ"].ToString());
                    }
                    VarBanco.Reader.Close();

                    lsSQL = "UPDATE" + vbCr;
                    lsSQL = lsSQL + "IS_MOV_NF_REMESSA_DEVOLUCAO" + vbCr;
                    lsSQL = lsSQL + "SET " + vbCr;
                    lsSQL = lsSQL + "ST_SITUACAO = 3" + vbCr;
                    lsSQL = lsSQL + "WHERE " + vbCr;
                    lsSQL = lsSQL + "CD_REGISTRO = " + cd_CD_REGISTRO;
                    VarBanco.lsSQL = lsSQL;
                    ObjData.ExecutaSQL();

                    cd_erro = 2;

                    lsSQL = "INSERT INTO" + vbCr;
                    lsSQL = lsSQL + "IS_MOV_REMESSA_DEVOLUCAO_ERROS" + vbCr;
                    lsSQL = lsSQL + "(CD_REGISTRO," + vbCr;
                    lsSQL = lsSQL + "NU_SEQ," + vbCr;
                    if (OrigemErro == 2)
                    {
                        lsSQL = lsSQL + "CD_ERRO," + vbCr;
                    }
                    else
                    {
                        lsSQL = lsSQL + "CD_ERRO_SEFAZ," + vbCr;
                    }
                    lsSQL = lsSQL + "DS_USR, TP_STATUS_ERRO) VALUES " + vbCr;
                    lsSQL = lsSQL + "(" + cd_CD_REGISTRO + "," + vbCr;
                    lsSQL = lsSQL + Nuseq + "," + vbCr;
                    lsSQL = lsSQL + cd_erro + "," + vbCr;
                    lsSQL = lsSQL + "'" + NomeUsuario + "',1" + vbCr;
                    lsSQL = lsSQL + ")" + vbCr;
                    VarBanco.lsSQL = lsSQL;
                    ObjData.ExecutaSQL();
                }

            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public void Cadastra_Erro_Nota99(int NuCDRegistro, int OrigemErro, string ds_erro_go, string NomeUsuario)
        {
            try
            {
                int Nuseq = 0;
                int cd_erro = 0;

                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                lsSQL = "SELECT CASE WHEN MAX(NU_SEQ) IS NULL THEN 0 ELSE MAX(NU_SEQ) END + 1 AS NUSEQ  FROM IS_MOV_REMESSA_DEVOLUCAO_ERROS" + vbCr;
            
                VarBanco.lsSQL = lsSQL;
                ObjData.RetornaSQL();

                if (VarBanco.Reader.Read())
                {
                    Nuseq = Convert.ToInt32(VarBanco.Reader["NUSEQ"].ToString());
                }
                VarBanco.Reader.Close();

                lsSQL = "UPDATE" + vbCr;
                lsSQL = lsSQL + "IS_MOV_NF_REMESSA_DEVOLUCAO" + vbCr;
                lsSQL = lsSQL + "SET " + vbCr;
                lsSQL = lsSQL + "ST_SITUACAO = 3" + vbCr;
                lsSQL = lsSQL + "WHERE " + vbCr;
                lsSQL = lsSQL + "CD_REGISTRO = " + NuCDRegistro;
                VarBanco.lsSQL = lsSQL;
                ObjData.ExecutaSQL();

                cd_erro = 1;

                lsSQL = "INSERT INTO" + vbCr;
                lsSQL = lsSQL + "IS_MOV_REMESSA_DEVOLUCAO_ERROS" + vbCr;
                lsSQL = lsSQL + "(CD_REGISTRO," + vbCr;
                lsSQL = lsSQL + "NU_SEQ," + vbCr;
                if (OrigemErro == 2)
                {
                    lsSQL = lsSQL + "CD_ERRO," + vbCr;
                }
                else
                {
                    lsSQL = lsSQL + "CD_ERRO_SEFAZ," + vbCr;
                }
                lsSQL = lsSQL + "DS_USR, TP_STATUS_ERRO) VALUES " + vbCr;
                lsSQL = lsSQL + "(" + NuCDRegistro + "," + vbCr;
                lsSQL = lsSQL + Nuseq + "," + vbCr;
                lsSQL = lsSQL + cd_erro + "," + vbCr;
                lsSQL = lsSQL + "'" + NomeUsuario + "', 1" + vbCr;
                lsSQL = lsSQL + ")" + vbCr;
                VarBanco.lsSQL = lsSQL;
                ObjData.ExecutaSQL();

            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public void Cadastra_Erro_Nota(int NuCDRegistro, int OrigemErro, string ds_erro_go, string NomeUsuario)
        {

            try
            {
                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                int Nuseq = 0;
                int cd_erro = 0;

                lsSQL = "SELECT CASE WHEN MAX(NU_SEQ) IS NULL THEN 0 ELSE MAX(NU_SEQ) END + 1 AS NUSEQ  FROM IS_MOV_REMESSA_DEVOLUCAO_ERROS" + vbCr;
                VarBanco.lsSQL = lsSQL;

                ObjData.RetornaSQL();

                if (VarBanco.Reader.Read())
                {
                    Nuseq = Convert.ToInt32(VarBanco.Reader["NUSEQ"].ToString());
                }
                VarBanco.Reader.Close();

                lsSQL = "UPDATE" + vbCr;
                lsSQL = lsSQL + "IS_MOV_NF_REMESSA_DEVOLUCAO" + vbCr;
                lsSQL = lsSQL + "SET " + vbCr;
                lsSQL = lsSQL + "ST_SITUACAO = 3" + vbCr;
                lsSQL = lsSQL + "WHERE " + vbCr;
                lsSQL = lsSQL + "CD_REGISTRO = " + NuCDRegistro;
                VarBanco.lsSQL = lsSQL;

                ObjData.ExecutaSQL();

                lsSQL = "SELECT count(CD_ERRO) + 1 as cd_erro FROM IS_ERROS_FATURA";
                VarBanco.lsSQL = lsSQL;

                ObjData.RetornaSQL();

                if (VarBanco.Reader.Read())
                {

                    cd_erro = Convert.ToInt32(VarBanco.Reader["cd_erro"].ToString());

                    lsSQL = "INSERT INTO IS_ERROS_FATURA" + vbCr;
                    lsSQL = lsSQL + "(CD_ERRO," + vbCr;
                    lsSQL = lsSQL + "DS_ERRO)" + vbCr;
                    lsSQL = lsSQL + "VALUES" + vbCr;
                    lsSQL = lsSQL + "( ' " + cd_erro + "', '" + ds_erro_go.Substring(0, 99) + "'" + vbCr;
                    lsSQL = lsSQL + ")" + vbCr;
                    VarBanco.lsSQL = lsSQL;

                    ObjData.ExecutaSQL();


                }
                VarBanco.Reader.Close();

                lsSQL = "INSERT INTO" + vbCr;
                lsSQL = lsSQL + "IS_MOV_REMESSA_DEVOLUCAO_ERROS" + vbCr;
                lsSQL = lsSQL + "(CD_REGISTRO," + vbCr;
                lsSQL = lsSQL + "NU_SEQ," + vbCr;
                if (OrigemErro == 2)
                {
                    lsSQL = lsSQL + "CD_ERRO," + vbCr;
                }
                else
                {
                    lsSQL = lsSQL + "CD_ERRO_SEFAZ," + vbCr;
                }
                lsSQL = lsSQL + "DS_USR) VALUES " + vbCr;
                lsSQL = lsSQL + "(" + NuCDRegistro + "," + vbCr;
                lsSQL = lsSQL + Nuseq + "," + vbCr;
                lsSQL = lsSQL + cd_erro + "," + vbCr;
                lsSQL = lsSQL + "'" + NomeUsuario + "'" + vbCr;
                lsSQL = lsSQL + ")" + vbCr;
                VarBanco.lsSQL = lsSQL;

                ObjData.ExecutaSQL();

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public string PesquisaTransportadora(string CD_CONTABIL_PESSOA, string dia)
        {
            try
            {
                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                string retorno = string.Empty;
                lsSQL = "SELECT 'CWA' FROM TRANSPORTADORA WHERE CD_CONTABIL_PESSOA = " + CD_CONTABIL_PESSOA;

                VarBanco.lsSQL = lsSQL;
                ObjData.RetornaSQL2();

                if (!VarBanco.Reader2.Read())
                {
                    lsSQL = "SELECT " + vbCr;
                    lsSQL = lsSQL + "RD.CD_CONTABIL_TRANSP" + vbCr;
                    lsSQL = lsSQL + "FROM" + vbCr;
                    lsSQL = lsSQL + "PONTOS_VENDA_DIA PVD" + vbCr;
                    lsSQL = lsSQL + "JOIN ROTA_DISTRIBUICAO RD" + vbCr;
                    lsSQL = lsSQL + "ON RD.CD_ROTA_PRIMARIA = PVD.CD_ROTA_PRIMARIA" + vbCr;
                    lsSQL = lsSQL + "AND RD.CD_ROTA_SECUNDARIA = PVD.CD_ROTA_SECUNDARIA" + vbCr;
                    lsSQL = lsSQL + "AND RD.CD_ROTA_TERCIARIA = PVD.CD_ROTA_TERCIARIA" + vbCr;
                    lsSQL = lsSQL + "WHERE" + vbCr;
                    lsSQL = lsSQL + "PVD.CD_CONTABIL_AGENTE_VA = " + CD_CONTABIL_PESSOA + vbCr;
                    lsSQL = lsSQL + "AND PVD.cd_dia_semana = (DATEPART(DW,'" + dia + "' ))" + vbCr;
                    VarBanco.lsSQL = lsSQL;

                    ObjData.RetornaSQL2();

                    if (VarBanco.Reader2.Read())
                    {
                        retorno = VarBanco.Reader2["CD_CONTABIL_TRANSP"].ToString();
                    }
                    else
                    {
                        retorno = CD_CONTABIL_PESSOA;
                    }
                }
                else
                {
                    retorno = CD_CONTABIL_PESSOA;
                }


                if (!string.IsNullOrEmpty(retorno))
                {
                    lsSQL = "SELECT" + vbCr;
                    lsSQL = lsSQL + "NU_CPF," + vbCr;
                    lsSQL = lsSQL + "NU_CNPJ" + vbCr;
                    lsSQL = lsSQL + "FROM" + vbCr;
                    lsSQL = lsSQL + "CADASTRO_PESSOA" + vbCr;
                    lsSQL = lsSQL + "WHERE " + vbCr;
                    lsSQL = lsSQL + "CD_CONTABIL_PESSOA = " + retorno + vbCr;
                    VarBanco.lsSQL = lsSQL;

                    ObjData.RetornaSQL2();

                    if (VarBanco.Reader2.Read())
                    {
                        if (!string.IsNullOrEmpty(VarBanco.Reader2["NU_CPF"].ToString()))
                        {
                            retorno = VarBanco.Reader2["NU_CPF"].ToString();
                        }
                        else if (!string.IsNullOrEmpty(VarBanco.Reader2["NU_CNPJ"].ToString()))
                        {
                            retorno = VarBanco.Reader2["NU_CNPJ"].ToString();
                        }
                        else
                        {
                            retorno = "0";
                        }

                    }
                    else
                    {
                        retorno = "0";
                    }

                }

                return retorno;

            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public string PesquisaInformacoesRemessaParaNotaEncalhe(string p_DT_REMESSA, string p_CD_CONTABIL_PESSOA, int p_CD_PRODUTO)
        {

            lsSQL = "select sum(round((hv.va_preco * (100 - hv.va_comissao_venda))/100,2) * hv.qt_historico) as VA_RECIBO" + vbCr;
            lsSQL = lsSQL + ",sum(hv.qt_historico) as QT_HISTORICO" + vbCr;
            lsSQL = lsSQL + "from HISTORICO_VENDAS hv" + vbCr;
            lsSQL = lsSQL + "where hv.dt_historico = '" + p_DT_REMESSA + "'" + vbCr;
            lsSQL = lsSQL + "and hv.CD_CONTABIL_AGENTE_VA = " + p_CD_CONTABIL_PESSOA + vbCr;
            lsSQL = lsSQL + "and hv.CD_PRODUTO = " + p_CD_PRODUTO + vbCr;
            lsSQL = lsSQL + "and hv.cd_tp_historico_venda in (1,2)" + vbCr;


            return lsSQL;
        }

        public string PesquisaNotaFiscalRemessaInterior(string p_CD_REGISTRO)
        {

            try
            {

                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                lsSQL = "SELECT NU_NOTA_FISCAL " + vbCr;
                lsSQL = lsSQL + "FROM IS_MOV_REMESSA_DEVOLUCAO_RETORNO" + vbCr;
                lsSQL = lsSQL + "WHERE CD_REGISTRO IN (" + vbCr;
                lsSQL = lsSQL + "       select nfr.CD_REGISTRO" + vbCr;
                lsSQL = lsSQL + "       from IS_MOV_NF_REMESSA_DEVOLUCAO nfe" + vbCr;
                lsSQL = lsSQL + "       inner join IS_MOV_NF_REMESSA_DEVOLUCAO nfr on nfr.DT_REMESSA_DEVOLUCAO = nfe.DT_REMESSA_DEVOLUCAO" + vbCr;
                lsSQL = lsSQL + "											  and nfr.CD_PRODUTO           = nfe.CD_PRODUTO " + vbCr;
                lsSQL = lsSQL + "											  and nfr.ST_TP_NOTA_FISCAL    = 3 " + vbCr;
                lsSQL = lsSQL + "											  and nfr.ST_SITUACAO          = 1 " + vbCr;
                lsSQL = lsSQL + "											  and nfr.CD_CONTABIL_PESSOA = case ISNULL(nfe.CD_CONTABIL_AGENTE_VA, -1) when -1 then nfe.CD_CONTABIL_PESSOA " + vbCr;
                lsSQL = lsSQL + "											                                                                          else nfe.CD_CONTABIL_EDITORA_FI " + vbCr;
                lsSQL = lsSQL + "											                               end " + vbCr;
                lsSQL = lsSQL + "											  and nfr.CD_DESTINO_TIRAGEM = nfe.CD_DESTINO_TIRAGEM " + vbCr;
                //lsSQL = lsSQL + "											  and nfr.CD_DESTINO_TIRAGEM = ( " + vbCr;
                //lsSQL = lsSQL + "											                                select distinct top 1 tt.cd_destino_tiragem " + vbCr;
                //lsSQL = lsSQL + "											                                from TIRAGENS_TRABALHO tt " + vbCr;
                //lsSQL = lsSQL + "											                                inner join TIRAGEM_VA tva on tva.NM_TABELA_TEMP = tt.nm_tabela_temp " + vbCr;
                //lsSQL = lsSQL + "											                                                         and tva.CD_CONTABIL_AGENTE_VA = nfe.CD_CONTABIL_PESSOA " + vbCr;
                //lsSQL = lsSQL + "											                                where tt.CD_PRODUTO = nfr.CD_PRODUTO " + vbCr;
                //lsSQL = lsSQL + "											                                  and tt.st_ind_expedida = 1 " + vbCr;
                //lsSQL = lsSQL + "											                                  and tt.dt_tiragem      = nfr.DT_REMESSA_DEVOLUCAO  " + vbCr;
                //lsSQL = lsSQL + "											                                  ) " + vbCr;
                lsSQL = lsSQL + "       inner join IS_MOV_REMESSA_DEVOLUCAO_RETORNO nfrr on nfrr.CD_REGISTRO = nfr.CD_REGISTRO " + vbCr;
                lsSQL = lsSQL + "       where nfe.CD_REGISTRO in (" + p_CD_REGISTRO + ")" + vbCr;
                lsSQL = lsSQL + "       ) " + vbCr;

                VarBanco.lsSQL = lsSQL;

                ObjData.RetornaSQL2();

                if (VarBanco.Reader2.Read())
                {
                    return VarBanco.Reader2["NU_NOTA_FISCAL"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public string PesquisaDestinoTiragem(string p_Cd_registro)
        {

            try
            {
                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                lsSQL = "select case isnull(x.CD_DESTINO_TIRAGEM, 0) when 0 then 0 " + vbCr;
                lsSQL = lsSQL + "                                when 1 then 1 " + vbCr;
                lsSQL = lsSQL + "                                when 2 then 2 " + vbCr;
                lsSQL = lsSQL + "                                when 3 then 3 " + vbCr;
                lsSQL = lsSQL + "end as clDestino_Tiragem " + vbCr;
                lsSQL = lsSQL + "from IS_MOV_NF_REMESSA_DEVOLUCAO x " + vbCr;
                lsSQL = lsSQL + "where x.CD_DESTINO_TIRAGEM is not null " + vbCr;
                lsSQL = lsSQL + "and x.cd_registro = " + p_Cd_registro;

                VarBanco.lsSQL = lsSQL;

                ObjData.RetornaSQL2();

                if (VarBanco.Reader2.Read())
                {
                    return VarBanco.Reader2["clDestino_Tiragem"].ToString();
                }
                else
                {
                    return "0";
                }

            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public string PesquisaNotaFiscalRemessa(string p_DT_REMESSA, 
                                                 string p_CD_CONTABIL_PESSOA, 
                                                 string p_CD_MATERIAL_ASSOCIADO, 
                                                 string p_Cd_registro)
        {

            try
            {

                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                lsSQL = "select nfr.CD_REGISTRO " + vbCr;
                lsSQL = lsSQL + "      ,nfr.CD_CONTABIL_PESSOA " + vbCr;
                lsSQL = lsSQL + "      ,nfr.CD_PRODUTO " + vbCr;
                lsSQL = lsSQL + "      ,nfrr.NU_NOTA_FISCAL " + vbCr;
                lsSQL = lsSQL + "      ,nfrr.NU_SERIE_NOTA_FISCAL " + vbCr;
                lsSQL = lsSQL + "      ,nfrr.CD_CHAVE_ACESSO " + vbCr;
                lsSQL = lsSQL + "from IS_MOV_NF_REMESSA_DEVOLUCAO nfe " + vbCr;
                lsSQL = lsSQL + "inner join IS_MOV_NF_REMESSA_DEVOLUCAO nfr on nfr.DT_REMESSA_DEVOLUCAO = nfe.DT_REMESSA_DEVOLUCAO " + vbCr;
                lsSQL = lsSQL + "                                          and nfr.CD_PRODUTO = nfe.CD_PRODUTO " + vbCr;
                lsSQL = lsSQL + "                                          and nfr.ST_TP_NOTA_FISCAL = 3 " + vbCr;
                lsSQL = lsSQL + "                                          and nfr.ST_SITUACAO = 1 " + vbCr;
                lsSQL = lsSQL + "                                          and nfr.CD_CONTABIL_PESSOA = case ISNULL(nfe.CD_CONTABIL_AGENTE_VA, -1) when -1 then nfe.CD_CONTABIL_PESSOA " + vbCr;
                lsSQL = lsSQL + "                                                                                                                          else nfe.CD_CONTABIL_EDITORA_FI " + vbCr;
                lsSQL = lsSQL + "                                                                       end " + vbCr;
                lsSQL = lsSQL + "                                          and nfr.CD_DESTINO_TIRAGEM = nfe.CD_DESTINO_TIRAGEM " + vbCr;
                //lsSQL = lsSQL + "                                          and nfr.CD_DESTINO_TIRAGEM = ( " + vbCr;
                //lsSQL = lsSQL + "                                                                        select distinct top 1 tt.cd_destino_tiragem " + vbCr;
                //lsSQL = lsSQL + "                                                                        from TIRAGENS_TRABALHO tt " + vbCr;
                //lsSQL = lsSQL + "                                                                        inner join TIRAGEM_VA tva on tva.NM_TABELA_TEMP = tt.nm_tabela_temp " + vbCr;
                //lsSQL = lsSQL + "                                                                        and tva.CD_CONTABIL_AGENTE_VA = nfe.CD_CONTABIL_PESSOA " + vbCr;
                //lsSQL = lsSQL + "                                                                        where tt.CD_PRODUTO = nfr.CD_PRODUTO " + vbCr;
                //lsSQL = lsSQL + "                                                                          and tt.st_ind_expedida = 1 " + vbCr;
                //lsSQL = lsSQL + "                                                                          and tt.dt_tiragem = nfr.DT_REMESSA_DEVOLUCAO " + vbCr;
                //lsSQL = lsSQL + "                                                                       ) " + vbCr;
                lsSQL = lsSQL + "inner join IS_MOV_REMESSA_DEVOLUCAO_RETORNO nfrr on nfrr.CD_REGISTRO = nfr.CD_REGISTRO " + vbCr;
                lsSQL = lsSQL + "where nfe.CD_REGISTRO = " + p_Cd_registro;


                VarBanco.lsSQL = lsSQL;
                ObjData.RetornaSQL2();

                if (VarBanco.Reader2.Read())
                {
                    return VarBanco.Reader2["NU_NOTA_FISCAL"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public int Ponto_Venda_Fiscal(int Cd_contabil_pessoa)
        {

            try
            {
                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                lsSQL = "SELECT" + vbCr;
                lsSQL = lsSQL + "'CWA'" + vbCr;
                lsSQL = lsSQL + "FROM" + vbCr;
                lsSQL = lsSQL + "PONTO_VENDA_FISCAL" + vbCr;
                lsSQL = lsSQL + "WHERE" + vbCr;
                lsSQL = lsSQL + "CD_CONTABIL_PESSOA = " + Cd_contabil_pessoa + vbCr;
                lsSQL = lsSQL + " AND ST_SITUACAO_ATUAL NOT IN (3)";
                VarBanco.lsSQL = lsSQL;
                ObjData.RetornaSQL2();

                if (VarBanco.Reader2.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                
                throw;
            }


        }

        public string PesquisaAgente_Va(int Cd_contabil_pessoa)
        {

            try
            {

                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                string dCPF = string.Empty;
                string dCNPJ = string.Empty;

                lsSQL = "SELECT" + vbCr;
                lsSQL = lsSQL + "CP.NU_CNPJ," + vbCr;
                lsSQL = lsSQL + "CP.NU_CPF" + vbCr;
                lsSQL = lsSQL + "FROM" + vbCr;
                lsSQL = lsSQL + "PONTOS_VENDA PV" + vbCr;
                lsSQL = lsSQL + "JOIN CADASTRO_PESSOA CP" + vbCr;
                lsSQL = lsSQL + "ON CP.CD_CONTABIL_PESSOA = PV.CD_CONTABIL_AGENTE_VA" + vbCr;
                lsSQL = lsSQL + "WHERE" + vbCr;
                lsSQL = lsSQL + "PV.CD_CONTABIL_PONTO_VA_FISCAL = " + Cd_contabil_pessoa;

                VarBanco.lsSQL = lsSQL;
                ObjData.RetornaSQL2();

                if (VarBanco.Reader2.Read())
                {
                    if (!string.IsNullOrEmpty(VarBanco.Reader2["NU_CPF"].ToString()))
                    {
                        dCPF = VarBanco.Reader2["NU_CPF"].ToString();
                    }

                    if (!string.IsNullOrEmpty(VarBanco.Reader2["NU_CNPJ"].ToString()))
                    {
                        dCNPJ = VarBanco.Reader2["NU_CNPJ"].ToString();
                    }
                }
                else
                {
                    return "0";
                }

                if (!string.IsNullOrEmpty(dCNPJ))
                {
                    return dCNPJ;
                }
                else
                {
                    return dCPF;
                }


            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public string PesquisaDocumento(string Cd_contabil_pessoa)
        {
            try
            {
                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                string dCPF = string.Empty;
                string dCNPJ = string.Empty;

                lsSQL = "SELECT" + vbCr;
                lsSQL = lsSQL + "CP.NU_CNPJ," + vbCr;
                lsSQL = lsSQL + "CP.NU_CPF" + vbCr;
                lsSQL = lsSQL + "FROM" + vbCr;
                lsSQL = lsSQL + "CADASTRO_PESSOA CP" + vbCr;
                lsSQL = lsSQL + "WHERE" + vbCr;
                lsSQL = lsSQL + "CP.CD_CONTABIL_PESSOA = " + Cd_contabil_pessoa;

                VarBanco.lsSQL = lsSQL;
                ObjData.RetornaSQL2();

                if (VarBanco.Reader2.Read())
                {
                    if (!string.IsNullOrEmpty(VarBanco.Reader2["NU_CPF"].ToString()))
                    {
                        dCPF = VarBanco.Reader2["NU_CPF"].ToString();
                    }

                    if (!string.IsNullOrEmpty(VarBanco.Reader2["NU_CNPJ"].ToString()))
                    {
                        dCNPJ = VarBanco.Reader2["NU_CNPJ"].ToString();
                    }
                }
                else
                {
                    return "0";
                }

                if (!string.IsNullOrEmpty(dCNPJ))
                {
                    return dCNPJ;
                }
                else
                {
                    return dCPF;
                }

            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public string SetTratarRegistroSemRetorno()
        {

            try
            {
                InterfacesWSGOVA_Data ObjData = new InterfacesWSGOVA_Data();

                //ATUALIZA STATUS DE PROCESSADO SEM RETORNO  (2) PARA 3 COM ERRO.
                //QUANDO O MESMO NÃO TIVER A NOTA RETORNADA
                lsSQL = "UPDATE IMRD SET IMRD.ST_SITUACAO = 3 " + vbCr;
                lsSQL = lsSQL + "FROM IS_MOV_NF_REMESSA_DEVOLUCAO IMRD " + vbCr;
                lsSQL = lsSQL + "WHERE " + vbCr; 
                lsSQL = lsSQL + "    IMRD.ST_SITUACAO = 2 " + vbCr;
                lsSQL = lsSQL + "AND IMRD.CD_ORIGEM = 2 " + vbCr;
                lsSQL = lsSQL + "AND NOT EXISTS ( SELECT CD_REGISTRO  " + vbCr;
                lsSQL = lsSQL + "				 FROM IS_MOV_REMESSA_DEVOLUCAO_RETORNO IMRDR " + vbCr;
                lsSQL = lsSQL + "				 WHERE IMRDR.CD_REGISTRO = IMRD.CD_REGISTRO " + vbCr;
                lsSQL = lsSQL + "				) " + vbCr;

                VarBanco.lsSQL = lsSQL;

                ObjData.ExecutaSQL();

                //ATUALIZA STATUS DE PROCESSADO SEM RETORNO  (2) PARA 1 COM SUCESSO.
                //QUANDO O MESMO TIVER A NOTA RETORNADA
                lsSQL = "UPDATE IMRD SET IMRD.ST_SITUACAO = 1 " + vbCr;
                lsSQL = lsSQL + "FROM IS_MOV_NF_REMESSA_DEVOLUCAO IMRD " + vbCr;
                lsSQL = lsSQL + "WHERE " + vbCr;
                lsSQL = lsSQL + "    IMRD.ST_SITUACAO = 2 " + vbCr;
                lsSQL = lsSQL + "AND IMRD.CD_ORIGEM = 2 " + vbCr;
                lsSQL = lsSQL + "AND EXISTS ( SELECT CD_REGISTRO  " + vbCr;
                lsSQL = lsSQL + "			  FROM IS_MOV_REMESSA_DEVOLUCAO_RETORNO IMRDR " + vbCr;
                lsSQL = lsSQL + "			  WHERE IMRDR.CD_REGISTRO = IMRD.CD_REGISTRO " + vbCr;
                lsSQL = lsSQL + "			) " + vbCr;

                VarBanco.lsSQL = lsSQL;

                ObjData.ExecutaSQL();

                return "OK,";


            }
            catch (Exception ex)
            {

                return "Erro," + ex.Message;
            }

        }
           

                

    }
}
