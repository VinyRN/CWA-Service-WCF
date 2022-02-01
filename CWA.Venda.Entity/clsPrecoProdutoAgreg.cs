using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class PrecoProdutoAgregEntity
    {
        private int _CD_PRODUTO;
        private int _CD_ITEM_PRODUTO;
        private int _CD_FORMA_PAG;

        private DateTime _DT_BASE;

        private int _CD_GRUPO_CLIENTE;

        private decimal _VL_PARC1_ASS;
        private decimal _VL_DEMAIS_PARC_ASS;

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
        public int CD_FORMA_PAG
        {
            get { return _CD_FORMA_PAG; }
            set { _CD_FORMA_PAG = value; }
        }
        public DateTime DT_BASE
        {
            get { return _DT_BASE; }
            set { _DT_BASE = value; }
        }

        public decimal VL_PARC1_ASS
        {
            get { return _VL_PARC1_ASS; }
            set { _VL_PARC1_ASS = value; }
        }

        public decimal VL_DEMAIS_PARC_ASS
        {
            get { return _VL_DEMAIS_PARC_ASS; }
            set { _VL_DEMAIS_PARC_ASS = value; }
        }

        public int CD_GRUPO_CLIENTE
        {
            get { return _CD_GRUPO_CLIENTE; }
            set { _CD_GRUPO_CLIENTE = value; }
        }

        public FormaPagamentoEntity FormaPagamento { get; set; }

    }
}
