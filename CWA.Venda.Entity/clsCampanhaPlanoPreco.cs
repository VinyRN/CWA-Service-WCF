using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class CampanhaPlanoPrecoEntity
    {

        public string DS_CAMPANHA { get; set; }
        public int CD_CAMPANHA { get; set; }
        public int CD_PRODUTO { get; set; }
        public int CD_PRODUTO_DIGITAL { get; set; }

        public List<CampanhaPlanoEntity> CampanhaPlano { get; set; }
        public List<CampanhaPrecoEntity> CampanhaPreco { get; set; }
        public List<PlanoComercialEntity> PlanoComercial { get; set; }


    }
}
