using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class DadosPagamentoEntity
    {

        //DADOS PLANO COMERCIAL
        private int _CD_TP_ASSINATURA;
        private int _CD_TP_ENTREGA;
        private int _CD_TP_FORMA_PAG;
        private int _CD_FONTE_COBRANCA;
        private int _NU_PARCELA;
        private int _CD_FORMA_PAG;

        private int _CD_PRODUTO;
        private int _CD_CAMPANHA;
        private int _CD_PLANO;

        private string _CHAVE_CRYPT_CC;

        public string CHAVE_CRYPT_CC
        {
            get { return _CHAVE_CRYPT_CC; }
            set { _CHAVE_CRYPT_CC = value; }
        }


        public int CD_TP_ASSINATURA
        {
            get { return _CD_TP_ASSINATURA; }
            set { _CD_TP_ASSINATURA = value; }
        }

        public int CD_TP_ENTREGA
        {
            get { return _CD_TP_ENTREGA; }
            set { _CD_TP_ENTREGA = value; }
        }

        public int CD_TP_FORMA_PAG
        {
            get { return _CD_TP_FORMA_PAG; }
            set { _CD_TP_FORMA_PAG = value; }
        }

        public int CD_FONTE_COBRANCA
        {
            get { return _CD_FONTE_COBRANCA; }
            set { _CD_FONTE_COBRANCA = value; }
        }

        public int NU_PARCELA
        {
            get { return _NU_PARCELA; }
            set { _NU_PARCELA = value; }
        }

        public int CD_PRODUTO
        {
            get { return _CD_PRODUTO; }
            set { _CD_PRODUTO = value; }
        }

        public int CD_CAMPANHA
        {
            get { return _CD_CAMPANHA; }
            set { _CD_CAMPANHA = value; }
        }

        public int CD_PLANO
        {
            get { return _CD_PLANO; }
            set { _CD_PLANO = value; }
        }

        public int CD_FORMA_PAG
        {
            get { return _CD_FORMA_PAG; }
            set { _CD_FORMA_PAG = value; }
        }

        //DADOS CARTAO
        private int _CD_TP_BANDEIRA;
        private string _NU_CARTAO;
        private string _NM_PESSOA_CARTAO;
        private string _NU_CVV;
        private string _DT_VALID;
        private int _MELHOR_DIA_CARTAO;
        private string _CARDTOKEN;

        private string _NM_BANDEIRA;

        public int CD_TP_BANDEIRA
        {
            get { return _CD_TP_BANDEIRA; }
            set { _CD_TP_BANDEIRA = value; }
        }

        public string NU_CARTAO
        {
            get { return _NU_CARTAO; }
            set { _NU_CARTAO = value; }
        }

        public string NM_PESSOA_CARTAO
        {
            get { return _NM_PESSOA_CARTAO; }
            set { _NM_PESSOA_CARTAO = value; }
        }

        public string NU_CVV
        {
            get { return _NU_CVV; }
            set { _NU_CVV = value; }
        }

        public string DT_VALID
        {
            get { return _DT_VALID; }
            set { _DT_VALID = value; }
        }

        public int MELHOR_DIA_CARTAO
        {
            get { return _MELHOR_DIA_CARTAO; }
            set { _MELHOR_DIA_CARTAO = value; }
        }

        public string NM_BANDEIRA
        {
            get { return _NM_BANDEIRA; }
            set { _NM_BANDEIRA = value; }
        }

        public string CARDTOKEN
        {
            get { return _CARDTOKEN; }
            set { _CARDTOKEN = value; }
        }

        //DADOS DEBITO
        private string _CPFCNPJ_DEB;
        private string _NU_AGENCIA;
        private string _DV_AGENCIA;
        private string _NU_CONTA;
        private string _DV_CONTA;
        private int _MELHOR_DIA_DEBITO;
        private int _NU_BANCO;
        private string _NM_RESP_CONTA;

        public string CPFCNPJ_DEB
        {
            get { return _CPFCNPJ_DEB; }
            set { _CPFCNPJ_DEB = value; }
        }

        public string NU_AGENCIA
        {
            get { return _NU_AGENCIA; }
            set { _NU_AGENCIA = value; }
        }

        public string DV_AGENCIA
        {
            get { return _DV_AGENCIA; }
            set { _DV_AGENCIA = value; }
        }

        public string NU_CONTA
        {
            get { return _NU_CONTA; }
            set { _NU_CONTA = value; }
        }

        public string DV_CONTA
        {
            get { return _DV_CONTA; }
            set { _DV_CONTA = value; }
        }

        public int MELHOR_DIA_DEBITO
        {
            get { return _MELHOR_DIA_DEBITO; }
            set { _MELHOR_DIA_DEBITO = value; }
        }

        public int NU_BANCO
        {
            get { return _NU_BANCO; }
            set { _NU_BANCO = value; }
        }

        public string NM_RESP_CONTA
        {
            get { return _NM_RESP_CONTA; }
            set { _NM_RESP_CONTA = value; }
        }

    }
}
