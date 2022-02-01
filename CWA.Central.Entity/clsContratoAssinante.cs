using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Central.Entity
{
    public class ContratoAssinanteCentralEntity
    {
        public virtual int CD_CONTABIL_PESSOA { get; set; }
        public virtual string NU_SERIE_CTR { get; set; }
        public virtual int NU_CTR { get; set; }
        public virtual byte NU_DV_CTR { get; set; }
        public virtual string NM_PRODUTO { get; set; }
        public virtual string DS_PLANO { get; set; }
        public virtual short QTD_PRODUTO { get; set; }
        public virtual DateTime? DT_INICIO { get; set; }
        public virtual DateTime? DT_TERMINO { get; set; }
        public virtual byte ST_ESTADO_ATUAL { get; set; }
        public virtual string DS_ESTADO_ATUAL { get; set; }
        public virtual DateTime? DT_SUSCAN { get; set; }
        public virtual short NU_PERIODO { get; set; }
        public virtual string NM_PESSOA { get; set; }
        public virtual string ST_TP_PESSOA { get; set; }
        public virtual string NU_DOC { get; set; }
        public virtual string DS_EMAIL { get; set; }
        public virtual string DS_CAMPANHA { get; set; }
        public virtual string DS_TP_PAGAMENTO { get; set; }
        public virtual byte? NU_PARCELAS { get; set; }
        public virtual string NM_REPRESENTANTE_VENDA { get; set; }
        public virtual string NM_VENDEDOR { get; set; }
        public virtual decimal? VA_PLANO { get; set; }
        public virtual Int16? QTD_PRODUTO_PLANO { get; set; }
        public virtual decimal? VA_PARC1 { get; set; }
        public virtual decimal? VA_DEMAIS { get; set; }
        public virtual string DS_PAGAMENTO { get; set; }
        public virtual Int16 CD_PRODUTO { get; set; }
        public virtual int ST_CAD_DEB { get; set; }


    }
}
