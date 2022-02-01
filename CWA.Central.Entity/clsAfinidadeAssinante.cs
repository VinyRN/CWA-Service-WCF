using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Central.Entity
{
    public class AfinidadeAssinanteCentralEntity
    {
        public int? CD_CONTABIL_PESSOA { get; set; }

        public Int16? CD_GRUPO_AFINIDADE { get; set; }

        public int? CD_TP_AFINIDADE { get; set; }

        public string DS_TP_AFINIDADE { get; set; }

        public string DS_GRUPO_AFINIDADE { get; set; }

    }
}
