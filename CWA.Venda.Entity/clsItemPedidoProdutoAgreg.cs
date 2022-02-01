using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ItemPedidoProdutoAgregEntity
    {
        int _CD_PRODUTO;
        int _CD_ITEM_PRODUTO;
        int _ST_IND_RETIRADO;
        int _QTD_PRODUTO;
        decimal _VA_DESCONTO;
        public int CD_PRODUTO
        {
            get { return _CD_PRODUTO; }
            set { _CD_PRODUTO = value; }
        }
        public int CD_ITEM_PRODUTO
        {
            get { return _CD_ITEM_PRODUTO; }
            set { _CD_ITEM_PRODUTO = value; }
        }
        public int ST_IND_RETIRADO
        {
            get { return _ST_IND_RETIRADO; }
            set { _ST_IND_RETIRADO = value; }
        }
        public int QTD_PRODUTO
        {
            get { return _QTD_PRODUTO; }
            set { _QTD_PRODUTO = value; }
        }
        public decimal VA_DESCONTO
        {
            get { return _VA_DESCONTO; }
            set { _VA_DESCONTO = value; }
        }


    }
}
