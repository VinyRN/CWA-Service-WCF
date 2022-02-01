using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class CuboProdAgregJCEntity
    {
        private int _COD_CLIENTE;
        private string _CONTRATO;
        private string _DS_TP_PAGAMENTO;
        private string _QTD_PRODUTO;
        private string _DT_VENDA;
        private string _ST_ASSINATURA;
        private int _CD_PRODUTO;
        private int _CD_ITEM_PRODUTO;
        private string _NU_RECIBO;
        
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

        public string DS_TP_PAGAMENTO
        {
            get { return _DS_TP_PAGAMENTO; }
            set { _DS_TP_PAGAMENTO = value; }
        }

        public string QTD_PRODUTO
        {
            get { return _QTD_PRODUTO; }
            set { _QTD_PRODUTO = value; }
        }

        public string DT_VENDA
        {
            get { return _DT_VENDA; }
            set { _DT_VENDA = value; }
        }

        public string ST_ASSINATURA
        {
            get { return _ST_ASSINATURA; }
            set { _ST_ASSINATURA = value; }
        }

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

        public string NU_RECIBO
        {
            get { return _NU_RECIBO; }
            set { _NU_RECIBO = value; }
        }

    }
}
