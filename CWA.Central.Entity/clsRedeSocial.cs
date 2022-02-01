using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Central.Entity
{
    public class RedeSocialCentralEntity
    {
        public int? CD_CONTABIL_PESSOA { get; set; }

        public byte NU_SEQ { get; set; }

        public string DS_REDE_SOCIAL { get; set; }

        public byte ST_SITUACAO { get; set; }

        public string ID_TOKEN { get; set; }

        public string DS_EMAIL { get; set; }

        public Int16 CD_TIPO_REDE_SOCIAL { get; set; }

        public string DS_USUARIO { get; set; }

    }
}
