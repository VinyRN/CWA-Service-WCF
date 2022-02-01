using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class PedidoProdutoAgregEntity
    {
        int _CD_FORMA_PAG;
        int _CD_CONTABIL_REPR_VENDA;
        int _CD_CONTABIL_VEND;
        decimal _VA_FRETE;
        int _CD_FORMA_EXPEDICAO;
        int _ST_IND_RETIRADO;
        public int CD_FORMA_PAG
        {
            get { return _CD_FORMA_PAG; }
            set { _CD_FORMA_PAG = value; }
        }
        public int CD_CONTABIL_REPR_VENDA
        {
            get { return _CD_CONTABIL_REPR_VENDA; }
            set { _CD_CONTABIL_REPR_VENDA = value; }
        }
        public int CD_CONTABIL_VEND
        {
            get { return _CD_CONTABIL_VEND; }
            set { _CD_CONTABIL_VEND = value; }
        }
        public decimal VA_FRETE
        {
            get { return _VA_FRETE; }
            set { _VA_FRETE = value; }
        }
        public int CD_FORMA_EXPEDICAO
        {
            get { return _CD_FORMA_EXPEDICAO; }
            set { _CD_FORMA_EXPEDICAO = value; }
        }
        public int ST_IND_RETIRADO
        {
            get { return _ST_IND_RETIRADO; }
            set { _ST_IND_RETIRADO = value; }
        }

    }


}
