using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class PessoaCadastroEntity
    {
        
        public int TP_REG { get; set; }
        public Int32 CD_CONTABIL_PESSOA { get; set; }
        public Int32 NU_CTR { get; set; }
        public string NM_PESSOA { get; set; }
        public DateTime DT_NASC_FUND { get; set; }
        public string ST_TP_PESSOA { get; set; }
        public string NU_CPF_CNPJ { get; set; }
        public string ST_ESTADO_CIVIL { get; set; }
        public string ST_IND_DIVULGACAO { get; set; }
        public string NM_RESPONSAVEL { get; set; }
        public string ST_IND_AUTORIZA_EMAIL { get; set; }

        public List<ContratoPessoaEntity> ListaContrato { get; set; }
        public List<PessoaEmailEntity> ListaEmail { get; set; }
        public List<PessoaTelefoneEntity> ListaTelefone { get; set; }



    }
}
