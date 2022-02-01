using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class DebitoProdutoAgregEntity
    {

        private int _CD_FONTE_DEBITO;
        private string _DT_ENVIO_COBRANCA;
        private string _DT_RETORNO_COBRANCA;
        private string _NM_TITULAR;
        private string _NU_CPF_CNPJ;
        private int _NU_DIA_DEBITO;
        private string _NU_CARTAO;
        private string _NU_CVV_CARTAO;
        private string _DT_VALID_CARTAO;
        private int _ST_TP_PARC_CARTAO;
        private string _COD_AUTORIZACAO;
        private string _COD_NSU;
        private string _NU_AGENCIA;
        private string _NU_DV_AGENCIA;
        private string _NU_CONTA;
        private string _NU_DV_CONTA;

        public int CD_FONTE_DEBITO
        {
            get { return _CD_FONTE_DEBITO; }
            set { _CD_FONTE_DEBITO = value; }
        }
        public string DT_ENVIO_COBRANCA
        {
            get { return _DT_ENVIO_COBRANCA; }
            set { _DT_ENVIO_COBRANCA = value; }
        }
        public string DT_RETORNO_COBRANCA
        {
            get { return _DT_RETORNO_COBRANCA; }
            set { _DT_RETORNO_COBRANCA = value; }
        }
        public string NM_TITULAR
        {
            get { return _NM_TITULAR; }
            set { _NM_TITULAR = value; }
        }
        public string NU_CPF_CNPJ
        {
            get { return _NU_CPF_CNPJ; }
            set { _NU_CPF_CNPJ = value; }
        }
        public int NU_DIA_DEBITO
        {
            get { return _NU_DIA_DEBITO; }
            set { _NU_DIA_DEBITO = value; }
        }
        public string NU_CARTAO
        {
            get { return _NU_CARTAO; }
            set { _NU_CARTAO = value; }
        }
        public string NU_CVV_CARTAO
        {
            get { return _NU_CVV_CARTAO; }
            set { _NU_CVV_CARTAO = value; }
        }
        public string DT_VALID_CARTAO
        {
            get { return _DT_VALID_CARTAO; }
            set { _DT_VALID_CARTAO = value; }
        }
        public int ST_TP_PARC_CARTAO
        {
            get { return _ST_TP_PARC_CARTAO; }
            set { _ST_TP_PARC_CARTAO = value; }
        }
        public string COD_AUTORIZACAO
        {
            get { return _COD_AUTORIZACAO; }
            set { _COD_AUTORIZACAO = value; }
        }
        public string COD_NSU
        {
            get { return _COD_NSU; }
            set { _COD_NSU = value; }
        }
        public string NU_AGENCIA
        {
            get { return _NU_AGENCIA; }
            set { _NU_AGENCIA = value; }
        }
        public string NU_DV_AGENCIA
        {
            get { return _NU_DV_AGENCIA; }
            set { _NU_DV_AGENCIA = value; }
        }
        public string NU_CONTA
        {
            get { return _NU_CONTA; }
            set { _NU_CONTA = value; }
        }
        public string NU_DV_CONTA
        {
            get { return _NU_DV_CONTA; }
            set { _NU_DV_CONTA = value; }
        }
    }
}
