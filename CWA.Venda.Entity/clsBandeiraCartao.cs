using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class BandeiraCartaoEntity
    {
        private int _ST_TP_BANDEIRA;
        private string _NM_BANDEIRA;

        public int ST_TP_BANDEIRA
        {
            get { return _ST_TP_BANDEIRA; }
            set { _ST_TP_BANDEIRA = value; }
        }

        public string NM_BANDEIRA
        {
            get { return _NM_BANDEIRA; }
            set { _NM_BANDEIRA = value; }
        }

    }

    public class BandeiraCartaoEntityNOVO
    {
        public string DS_BANDEIRA { get; set; }
        public byte ST_TP_BANDEIRA { get; set; }
        public int CD_CONTABIL_ADM { get; set; }
        public byte NU_MAX_PARC { get; set; }
    }
}
