using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ContratoAssinanteEntity
    {

        private string _NU_SERIE_CTR;
        private string _NU_CTR;
        private string _NU_DV_CTR;
        private string _CD_CONTABIL_PESSOA;
        private string _NM_PESSOA;
        private string _ST_TP_PESSOA;
        private string _NU_DOC;
        private string _DS_EMAIL;
        private string _NM_PRODUTO;
        private string _ST_SITUACAO;

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

        public string CD_CONTABIL_PESSOA
        {
            get { return _CD_CONTABIL_PESSOA; }
            set { _CD_CONTABIL_PESSOA = value; }
        }

        public string NM_PESSOA
        {
            get { return _NM_PESSOA; }
            set { _NM_PESSOA = value; }
        }

        public string ST_TP_PESSOA
        {
            get { return _ST_TP_PESSOA; }
            set { _ST_TP_PESSOA = value; }
        }

        public string NU_DOC
        {
            get { return _NU_DOC; }
            set { _NU_DOC = value; }
        }

        public string DS_EMAIL
        {
            get { return _DS_EMAIL; }
            set { _DS_EMAIL = value; }
        }

        public string NM_PRODUTO
        {
            get { return _NM_PRODUTO; }
            set { _NM_PRODUTO = value; }
        }

        public string ST_SITUACAO
        {
            get { return _ST_SITUACAO; }
            set { _ST_SITUACAO = value; }
        }


    }
}
