using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Central.Entity
{
    public class ListaDebitoAssinanteCentralEntity
    {
        public Int32? CD_FONTE_DEBITO { get; set; }
        public byte? ST_TP_DEBITO { get; set; }
        public string NM_TITULAR { get; set; }
        public byte? NU_DIA_DEBITO { get; set; }
        public string NU_CARTAO { get; set; }
        public string NU_CVV_CARTAO { get; set; }
        public DateTime? DT_VALID_CARTAO { get; set; }
        public Int16? NU_BANCO { get; set; }
        public string NU_AGENCIA { get; set; }
        public string NU_CONTA { get; set; }
        public Int64? NU_CPF_CNPJ { get; set; }
        public string NU_DV_AGENCIA { get; set; }
        public string NU_DV_CONTA { get; set; }
        public Int64? NU_CDC { get; set; }
        public byte? NU_DV_CDC { get; set; }
        public Int16? NU_DDD { get; set; }
        public string NU_TELEFONE { get; set; }
        public Int64? NU_PN { get; set; }
    }
}
