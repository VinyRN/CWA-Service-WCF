using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class CuboReciboJCEntity
    {
        private int _COD_CLIENTE;
        private string _CONTRATO;
        private string _NU_RECIBO;
        private string _NU_PARCELA;
        private string _VA_PARCELA;
        private string _DT_VENCIMENTO;
        private string _DT_BAIXA;
        private string _ST_PARCELA;

        public int COD_CLIENTE
        {
            get { return _COD_CLIENTE; }
            set { _COD_CLIENTE = value; }
        }

        public string CONTRATO
        {
            get { return _CONTRATO; }
            set { _CONTRATO = value; }
        }

        public string NU_RECIBO
        {
            get { return _NU_RECIBO; }
            set { _NU_RECIBO = value; }
        }

        public string NU_PARCELA
        {
            get { return _NU_PARCELA; }
            set { _NU_PARCELA = value; }
        }

        public string VA_PARCELA
        {
            get { return _VA_PARCELA; }
            set { _VA_PARCELA = value; }
        }

        public string DT_VENCIMENTO
        {
            get { return _DT_VENCIMENTO; }
            set { _DT_VENCIMENTO = value; }
        }

        public string DT_BAIXA
        {
            get { return _DT_BAIXA; }
            set { _DT_BAIXA = value; }
        }

        public string ST_PARCELA
        {
            get { return _ST_PARCELA; }
            set { _ST_PARCELA = value; }
        }

    }
}
