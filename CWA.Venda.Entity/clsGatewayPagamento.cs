using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class GatewayPagamento
    {
        string _CodAutorizacao;
        string _ComprovanteVenda;
        string _TransacAdquirente;
        string _IDPagamento;
        int _NumeroParcelasEnviadas;
        string _TokenCard;
        string _CVV;

        public string CodAutorizacao
        {
            get { return _CodAutorizacao; }
            set { _CodAutorizacao = value; }
        }
        public string ComprovanteVenda
        {
            get { return _ComprovanteVenda; }
            set { _ComprovanteVenda = value; }
        }
        public string TransacAdquirente
        {
            get { return _TransacAdquirente; }
            set { _TransacAdquirente = value; }
        }
        public string IDPagamento
        {
            get { return _IDPagamento; }
            set { _IDPagamento = value; }
        }
        public int NumeroParcelasEnviadas
        {
            get { return _NumeroParcelasEnviadas; }
            set { _NumeroParcelasEnviadas = value; }
        }
        public string TokenCard
        {
            get { return _TokenCard; }
            set { _TokenCard = value; }
        }

        public string CVV
        {
            get { return _CVV; }
            set { _CVV = value; }
        }
    }
}
