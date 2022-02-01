using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class BoletoReciboEntity
    {

        private string _VA_RECIBO;
        private string _DT_VENCIMENTO;
        private string _NU_SERIE_CTR;
        private string _NU_CTR;
        private string _NU_DV_CTR;
        private string _NM_PESSOA;
        private string _NU_DOCUMENTO;
        private string _NU_PARCELAS;
        private string _NU_PERIODO;
        private string _DT_INICIO;
        private string _DT_TERMINO;
        private string _NU_NOSSO_NUMERO;
        private string _DS_EMAIL;

        public string VA_RECIBO
        {
            get { return _VA_RECIBO; }
            set { _VA_RECIBO = value; }
        }

        public string DT_VENCIMENTO
        {
            get { return _DT_VENCIMENTO; }
            set { _DT_VENCIMENTO = value; }
        }

        public string NU_SERIE_CTR
        {
            get { return _NU_SERIE_CTR; }
            set { _NU_SERIE_CTR = value; }
        }

        public string NU_CTR
        {
            get { return _NU_CTR; }
            set { _NU_CTR = value; }
        }

        public string NU_DV_CTR
        {
            get { return _NU_DV_CTR; }
            set { _NU_DV_CTR = value; }
        }

        public string NM_PESSOA
        {
            get { return _NM_PESSOA; }
            set { _NM_PESSOA = value; }
        }

        public string NU_DOCUMENTO
        {
            get { return _NU_DOCUMENTO; }
            set { _NU_DOCUMENTO = value; }
        }

        public string NU_PARCELAS
        {
            get { return _NU_PARCELAS; }
            set { _NU_PARCELAS = value; }
        }

        public string NU_PERIODO
        {
            get { return _NU_PERIODO; }
            set { _NU_PERIODO = value; }
        }

        public string DT_INICIO
        {
            get { return _DT_INICIO; }
            set { _DT_INICIO = value; }
        }

        public string DT_TERMINO
        {
            get { return _DT_TERMINO; }
            set { _DT_TERMINO = value; }
        }

        public string NU_NOSSO_NUMERO
        {
            get { return _NU_NOSSO_NUMERO; }
            set { _NU_NOSSO_NUMERO = value; }
        }

        public string DS_EMAIL
        {
            get { return _DS_EMAIL; }
            set { _DS_EMAIL = value; }
        }


    }
}
