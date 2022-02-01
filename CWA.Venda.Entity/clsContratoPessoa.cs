using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ContratoPessoaEntity
    {

        public int TP_REG { get; set; }
        public Int32 CD_CONTABIL_PESSOA { get; set; }
        public Int32 NU_CTR { get; set; }
        public string NU_SERIE_CTR { get; set; }
        public int NU_DV_CTR { get; set; }
        public int ST_ESTADO_ATUAL { get; set; }
        public string DS_ESTADO_ATUAL { get; set; }
        public DateTime DT_SUSCAN { get; set; }
        public int CD_MOTIVO { get; set; }
        public string DS_MOTIVO { get; set; }
        public DateTime DT_VENDA { get; set; }
        public DateTime DT_ADESSAO_CLUBE { get; set; }
        public DateTime DT_ENVIO_CART_CLUBE { get; set; }
        public DateTime DT_VALID_CART_CLUBE { get; set; }
        public Int32 NU_CTR_CORP { get; set; }    
        public string DS_EMAIL { get; set; }

        public List<ContratoPeriodoEntity> ListaPeriodo { get; set; }
        public List<ContratoEnderecoEntity> ListaEndereco { get; set; }
        public List<ContratoDependenteEntity> ListaDependente { get; set; }

    }
}
