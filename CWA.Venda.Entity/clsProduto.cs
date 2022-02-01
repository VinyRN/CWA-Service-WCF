using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ProdutoEntity
    {

        private int _CD_PRODUTO;
        private string _NM_PRODUTO;
        private int _ST_IND_ONLINE;

        public int CD_PRODUTO
        {
            get { return _CD_PRODUTO; }
            set { _CD_PRODUTO = value; }
        }

        public string NM_PRODUTO
        {
            get { return _NM_PRODUTO; }
            set { _NM_PRODUTO = value; }
        }

        public int ST_IND_ONLINE
        {
            get { return _ST_IND_ONLINE; }
            set { _ST_IND_ONLINE = value; }
        }

    }
}
