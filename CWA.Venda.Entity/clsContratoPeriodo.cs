using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ContratoPeriodoEntity
    {
        public int TP_REG { get; set; }
        public Int32 CD_CONTABIL_PESSOA { get; set; }
        public Int32 NU_CTR { get; set; }
        public string NU_SERIE_CTR { get; set; }
        public int NU_DV_CTR { get; set; }
        public int CD_PRODUTO { get; set; }
        public string NM_PRODUTO { get; set; }
        public int QTD_PRODUTO { get; set; }
        public DateTime DT_INICIO { get; set; }
        public DateTime DT_TERMINO { get; set; }
        public int CD_CAMPANHA { get; set; }
        public string DS_CAMPANHA { get; set; }
        public int CD_PLANO { get; set; }
        public string DS_PANO { get; set; }
        public Int32 CD_CONTABIL_REPR_VENDA { get; set; }
        public string NM_REPR_VENDA { get; set; }
        public Int32 CD_CONTABIL_VEND { get; set; }
        public string NM_VENDEDOR { get; set; }
        public int CD_PRODUTO_DIGITAL { get; set; }
        public string NM_PRODUTO_DIGITAL { get; set; }
        public string DS_TP_PAGAMENTO { get; set; }

    }
}
