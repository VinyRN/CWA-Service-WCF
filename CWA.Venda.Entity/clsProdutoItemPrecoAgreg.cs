using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ProdutoItemPrecoAgregEntity
    {
        public string NM_PRODUTO { get; set; }
        public int CD_PRODUTO { get; set; }

        public List<ItemProdutoEntity> ItemProduto { get; set; }

    }
}
