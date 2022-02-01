using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class TipoPagamentoEntity
    {

        private int _CD_TP_PAGAMENTO;
        private string _DS_TP_PAGAMENTO;

        public int CD_TP_PAGAMENTO
        {
            get { return _CD_TP_PAGAMENTO; }
            set { _CD_TP_PAGAMENTO = value; }
        }

        public string DS_TP_PAGAMENTO
        {
            get { return _DS_TP_PAGAMENTO; }
            set { _DS_TP_PAGAMENTO = value; }
        }


    }
}
