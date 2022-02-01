using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ContratoDependenteEntity
    {
        public int TP_REG { get; set; }
        public Int32 CD_CONTABIL_PESSOA { get; set; }
        public Int32 NU_CTR { get; set; }
        public string NU_SERIE_CTR { get; set; }
        public int NU_DV_CTR { get; set; }
        public string DT_NASC { get; set; }
        public int NU_SEQ { get; set; }
        public string NM_DEPENDENTE { get; set; }
        public string ST_PARENTESCO { get; set; }
        public DateTime DT_ADESSAO_CLUBE { get; set; }
        public DateTime DT_ENVIO_CART_CLUBE { get; set; }
        public DateTime DT_VALID_CART_CLUBE { get; set; }
    }
}
