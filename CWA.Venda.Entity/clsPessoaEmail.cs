using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class PessoaEmailEntity
    {
        public int TP_REG { get; set; }
        public Int32 CD_CONTABIL_PESSOA { get; set; }
        public string DS_EMAIL { get; set; }
        public string ST_EMAIL_PRINCIPAL { get; set; }
        public string DS_TIPO_EMAIL { get; set; }
    }
}
