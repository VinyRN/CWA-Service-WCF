using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ParametroGlobalEDEntity
    {
        private int _ST_IND_REQUER_CARTAO;
        private Int32 _CD_LOGR_PADRAO_BRASIL;
        private Int32 _CD_LOGR_PADRAO_EXTERIOR;

        public int ST_IND_REQUER_CARTAO
        {
            get { return _ST_IND_REQUER_CARTAO; }
            set { _ST_IND_REQUER_CARTAO = value; }
        }

        public int CD_LOGR_PADRAO_BRASIL
        {
            get { return _CD_LOGR_PADRAO_BRASIL; }
            set { _CD_LOGR_PADRAO_BRASIL = value; }
        }

        public int CD_LOGR_PADRAO_EXTERIOR
        {
            get { return _CD_LOGR_PADRAO_EXTERIOR; }
            set { _CD_LOGR_PADRAO_EXTERIOR = value; }
        }
    }
}
