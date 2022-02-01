using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class BoletoEnderecoEntity
    {

        private string _NU_RESIDENCIA;
        private string _COMPL_RESIDENCIA;
        private string _NU_BLOCO;
        private string _NU_APARTAMENTO;
        private string _DS_COMPLEMENTO;
        private string _DS_PONTO_REF;
        private string _DS_TIPO_ABREV;
        private string _DS_LOGRADOURO;
        private string _DS_BAIRRO;
        private string _DS_MUNICIPIO;
        private string _DS_UF;
        private string _NU_CEP;

        public string NU_RESIDENCIA
        {
            get { return _NU_RESIDENCIA; }
            set { _NU_RESIDENCIA = value; }
        }

        public string COMPL_RESIDENCIA
        {
            get { return _COMPL_RESIDENCIA; }
            set { _COMPL_RESIDENCIA = value; }
        }

        public string NU_BLOCO
        {
            get { return _NU_BLOCO; }
            set { _NU_BLOCO = value; }
        }

        public string NU_APARTAMENTO
        {
            get { return _NU_APARTAMENTO; }
            set { _NU_APARTAMENTO = value; }
        }

        public string DS_COMPLEMENTO
        {
            get { return _DS_COMPLEMENTO; }
            set { _DS_COMPLEMENTO = value; }
        }

        public string DS_PONTO_REF
        {
            get { return _DS_PONTO_REF; }
            set { _DS_PONTO_REF = value; }
        }

        public string DS_TIPO_ABREV
        {
            get { return _DS_TIPO_ABREV; }
            set { _DS_TIPO_ABREV = value; }
        }

        public string DS_LOGRADOURO
        {
            get { return _DS_LOGRADOURO; }
            set { _DS_LOGRADOURO = value; }
        }

        public string DS_BAIRRO
        {
            get { return _DS_BAIRRO; }
            set { _DS_BAIRRO = value; }
        }

        public string DS_MUNICIPIO
        {
            get { return _DS_MUNICIPIO; }
            set { _DS_MUNICIPIO = value; }
        }

        public string DS_UF
        {
            get { return _DS_UF; }
            set { _DS_UF = value; }
        }

        public string NU_CEP
        {
            get { return _NU_CEP; }
            set { _NU_CEP = value; }
        }

    }
}
