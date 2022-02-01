using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CWA.Venda.Entity
{
    public class CuboProdutoJCEntity
    {
        private int _COD_PRODUTO;
        private string _DESC_PRODUTO;
        private string _DS_TP_PRODUTO;

        public int COD_PRODUTO
        {
            get { return _COD_PRODUTO; }
            set { _COD_PRODUTO = value; }
        }

        public string DESC_PRODUTO
        {
            get { return _DESC_PRODUTO; }
            set { _DESC_PRODUTO = value; }
        }

        public string DS_TP_PRODUTO
        {
            get { return _DS_TP_PRODUTO; }
            set { _DS_TP_PRODUTO = value; }
        }
 

    }
}
