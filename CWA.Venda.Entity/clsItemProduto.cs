using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ItemProdutoEntity
    {

        private int _CD_ITEM_PRODUTO;
        private int _CD_PRODUTO;
        private string _NM_ITEM_PRODUTO;

        public int CD_ITEM_PRODUTO
        {
            get { return _CD_ITEM_PRODUTO; }
            set { _CD_ITEM_PRODUTO = value; }
        }

        public int CD_PRODUTO
        {
            get { return _CD_PRODUTO; }
            set { _CD_PRODUTO = value; }
        }

        public string NM_ITEM_PRODUTO
        {
            get { return _NM_ITEM_PRODUTO; }
            set { _NM_ITEM_PRODUTO = value; }
        }


        public List<PrecoProdutoAgregEntity> PrecoProdutoAgreg { get; set; }

    }

}
