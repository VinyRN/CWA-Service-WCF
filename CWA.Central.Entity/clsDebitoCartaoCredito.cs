using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CWA.Central.Entity
{
    public class DebitoCartaoCreditoCentralEntity
    {
        public int CD_CONTABIL_PESSOA { get; set; }
        public string NU_SERIE_CTR { get; set; }
        public int NU_CTR { get; set; }
        public byte NU_DV_CTR { get; set; }
        public int? CD_FONTE_DEBITO { get; set; }
        public string NM_TITULAR { get; set; }
        public Int64? NU_CPF_CNPJ { get; set; }
        public byte NU_DIA_DEBITO { get; set; }
        public string NU_CARTAO { get; set; }
        public string NU_CVV_CARTAO { get; set; }
        public DateTime? DT_VALID_CARTAO { get; set; }

        public string NU_CARTAO_FORMAT { get; set; }
        public string NU_CARTAO_CRIPT { get; set; }


        public string NM_BANDEIRA { get; set; }
        public int ST_TP_PARC_CARTAO { get; set; }

        public int NU_PARCELA { get; set; }
        public string VA_RECIBO { get; set; }

        public Int32 NU_RECIBO { get; set; }

        public int NU_PERIODO { get; set; }

        public ErroCentralEntity Erros { get; set; }
        public List<DebitoCartaoCreditoCentralEntity> BuscarDebitoCartaoCredito { get; set; }
        public List<BandeiraCartaoCentralEntity> BuscaBandeiraCartao { get; set; }
        public List<BancoDebitoCentralEntity> BuscaBancoDebito { get; set; }
        public List<ListaDebitoAssinanteCentralEntity> BuscaDebitoAssinante { get; set; }
    }
}
