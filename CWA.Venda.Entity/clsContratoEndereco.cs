using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ContratoEnderecoEntity
    {
        public int TP_REG { get; set; }
        public Int32 CD_CONTABIL_PESSOA { get; set; }
        public Int32 NU_CTR { get; set; }
        public string NU_SERIE_CTR { get; set; }
        public int NU_DV_CTR { get; set; }
        public string ST_TP_ENDERECO { get; set; }
        public string NU_RESIDENCIA { get; set; }
        public string NU_BLOCO { get; set; }
        public string NU_APARTAMENTO { get; set; }
        public string COMPL_RESIDENCIA { get; set; }
        public string DS_TIPO_LOGRADOURO { get; set; }
        public string DS_LOGRADOURO { get; set; }
        public string DS_REGIAO { get; set; }
        public string DS_ESTADO_FEDERACAO { get; set; }
        public string DS_MUNICIPIO { get; set; }
        public string DS_BAIRRO { get; set; }
        public string NU_CEP { get; set; }
        public string DS_UF { get; set; }

    }
}
