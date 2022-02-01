using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CWA.Venda.Data;
using CWA.Venda.Entity;
using CWA.Util;
using CWA.EngineServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CWA.Venda.Business
{
    public class PessoaContratoBusiness
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

        public string GetCadastroPessoaContrato(Int32 pintIDPessoa, 
                                                string pdtDtCadInicial, 
                                                string pdtDtCadFinal, 
                                                int pintSituacao)
        {
            try
            {
                if ((pintIDPessoa == 0) && (pdtDtCadInicial == null) && (pdtDtCadFinal == null) && (pintSituacao == 0))
                {
                    _erro = -1;
                    _msgErro = "Favor informar um dos critérios de filtro";
                    return "";
                }

                if (pdtDtCadInicial !="")
                {
                    if (pdtDtCadFinal == "")
                    {
                        _erro = -1;
                        _msgErro = "Favor informar a data final (data inicial foi preenchida)";
                        return "";
                    }
                }

                if (pdtDtCadFinal != "")
                {
                    if (pdtDtCadInicial == "")
                    {
                        _erro = -1;
                        _msgErro = "Favor informar a data inicial (data final foi preenchida)";
                        return "";
                    }
                }

                if ((pdtDtCadInicial != "") && (pdtDtCadFinal != ""))
                {
                    DateTime ldtInicio = DateTime.Parse(pdtDtCadInicial);
                    DateTime ldtFim = DateTime.Parse(pdtDtCadFinal);

                    if (ldtInicio > ldtFim)
                    {
                        _erro = -1;
                        _msgErro = "Data inicial deve ser menor que data final.";
                        return "";
                    }
                }


                if (pintSituacao != 0)
                {
                    if ((pintSituacao != 1) && (pintSituacao != 2) && (pintSituacao != 3) && (pintSituacao != 8))
                    {
                        _erro = -1;
                        _msgErro = "a situação deve ser preenchida (1 = Ativos (Diferente de Cancelado/Atendido) | 2 = Suspensos | 3 = Cancelado | 8 = Atendidos";
                        return "";
                    }
                }

                PessoaContratoData ObjData = new PessoaContratoData();

                List<PessoaCadastroEntity> ObjList = ObjData.GetCadastroPessoaContrato(pintIDPessoa, pdtDtCadInicial, pdtDtCadFinal, pintSituacao);

                if (ObjList != null)
                {
                    return JsonConvert.SerializeObject(ObjList, Newtonsoft.Json.Formatting.None);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
    }
}
