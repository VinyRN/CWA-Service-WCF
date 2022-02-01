using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Central.Entity
{
    public class DadosdoAssinanteCentralEntity
    {
        public byte? ST_TP_PESSOA { get; set; }
        public string NM_PESSOA { get; set; }
        public string NM_SOBRENOME { get; set; }
        public string NM_RESPONSAVEL { get; set; }
        public string NM_FANTASIA { get; set; }
        public int? CD_CONTABIL_PESSOA { get; set; }
        public string NU_CPF { get; set; }
        public string NU_IDENTIDADE { get; set; }
        public string NM_ORGAO_EMISSOR { get; set; }
        public DateTime? DT_EMISSAO { get; set; }
        public byte? ST_ESTADO_CIVIL { get; set; }
        public DateTime? DT_NASC_FUND { get; set; }
        public string NU_CNPJ { get; set; }
        public string NU_INSCR_MUN { get; set; }
        public string NU_INSCR_EST { get; set; }
        public short? CD_RAMO { get; set; }
        public string DS_NOME_ABREV { get; set; }
        public byte? ST_IND_DIVULGACAO { get; set; }
        public byte? CD_TP_TRATAMENTO { get; set; }
        public string UF_RG { get; set; }
        public int? CD_CARGO { get; set; }
        public int? CD_GRAU_INSTRUCAO { get; set; }
        public int? CD_GRUPO_AFINIDADE { get; set; }
        public int? CD_NACIONALIDADE { get; set; }
        public int? CD_NATURALIDADE { get; set; }
        public int? CD_PROFISSAO { get; set; }
        public long? NU_CPF_RESP { get; set; }

    }
}
