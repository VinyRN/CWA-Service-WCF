using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class FormaPagamentoEntity
    {
        private string _DS_FORMA_PAGAMENTO;
        private int _CD_FORMA_PAG;
        private int _NU_PARCELAS;
        private int _CD_TP_PAGAMENTO;

        public string DS_FORMA_PAGAMENTO
        {
            get { return _DS_FORMA_PAGAMENTO; }
            set { _DS_FORMA_PAGAMENTO = value; }
        }

        public int CD_FORMA_PAG
        {
            get { return _CD_FORMA_PAG; }
            set { _CD_FORMA_PAG = value; }
        }

        public int NU_PARCELAS
        {
            get { return _NU_PARCELAS; }
            set { _NU_PARCELAS = value; }
        }

        public int CD_TP_PAGAMENTO
        {
            get { return _CD_TP_PAGAMENTO; }
            set { _CD_TP_PAGAMENTO = value; }
        }

        public TipoPagamentoEntity TipoPagamento { get; set; }

    }
}
