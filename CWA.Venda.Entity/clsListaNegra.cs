using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ListaNegraEntity
    {

        private int _CD_PESSSOA;

        private string _NU_CARTAO;

        private int _NU_BANCO;
        private string _NU_AGENCIA;
        private string _NU_CONTA;

        private double _NU_CPF_CNPJ;

        private int _CD_LOGR;
        private int _NU_RESID;
        private string _NU_BLOCO;
        private string _NU_APTO;
        private string _COMPL_RESID;

        public int CD_PESSSOA
        {
            get { return _CD_PESSSOA; }
            set { _CD_PESSSOA = value; }
        }

        public string NU_CARTAO
        {
            get { return _NU_CARTAO; }
            set { _NU_CARTAO = value; }
        }

        public int NU_BANCO
        {
            get { return _NU_BANCO; }
            set { _NU_BANCO = value; }
        }

        public string NU_AGENCIA
        {
            get { return _NU_AGENCIA; }
            set { _NU_AGENCIA = value; }
        }

        public string NU_CONTA
        {
            get { return _NU_CONTA; }
            set { _NU_CONTA = value; }
        }

        public double NU_CPF_CNPJ
        {
            get { return _NU_CPF_CNPJ; }
            set { _NU_CPF_CNPJ = value; }
        }

        public int CD_LOGR
        {
            get { return _CD_LOGR; }
            set { _CD_LOGR = value; }
        }

        public int NU_RESID
        {
            get { return _NU_RESID; }
            set { _NU_RESID = value; }
        }

        public string NU_BLOCO
        {
            get { return _NU_BLOCO; }
            set { _NU_BLOCO = value; }
        }

        public string NU_APTO
        {
            get { return _NU_APTO; }
            set { _NU_APTO = value; }
        }

        public string COMPL_RESID
        {
            get { return _COMPL_RESID; }
            set { _COMPL_RESID = value; }
        }

    }
}
