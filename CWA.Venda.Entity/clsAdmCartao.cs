using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class AdmCartaoEntity
    {

        private int _CD_ADM_CARTAO;
        private string _NM_ADM_CARTAO;
        private int _ST_COBR_GATEWAY;

        public int CD_ADM_CARTAO
        {
            get { return _CD_ADM_CARTAO; }
            set { _CD_ADM_CARTAO = value; }
        }

        public string NM_ADM_CARTAO
        {
            get { return _NM_ADM_CARTAO; }
            set { _NM_ADM_CARTAO = value; }
        }

        public int ST_COBR_GATEWAY
        {
            get { return _ST_COBR_GATEWAY; }
            set { _ST_COBR_GATEWAY = value; }
        }


    }
}
