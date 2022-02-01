using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Central.Entity
{
    public class EmailDoAssinanteCentralEntity
    {
        public int? CD_CONTABIL_PESSOA { get; set; }

        public string DS_EMAIL { get; set; }

        public byte? ST_SITUACAO { get; set; }

        public byte? ST_EMAIL_PRINCIPAL { get; set; }

        public int CD_TIPO_EMAIL { get; set; }

        public byte NU_SEQ { get; set; }

        public string DS_TIPO_EMAIL { get; set; }
    }
}
