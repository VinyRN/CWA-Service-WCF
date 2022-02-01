using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ClubeAssinanteEntity
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
        private string _NM_DEPENDENTE;
        private string _MES_TERMINO;
        private string _ANO_TERMINO;

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

        public string NM_DEPENDENTE
        {
            get { return _NM_DEPENDENTE; }
            set { _NM_DEPENDENTE = value; }
        }

        public string MES_TERMINO
        {
            get { return _MES_TERMINO; }
            set { _MES_TERMINO = value; }
        }

        public string ANO_TERMINO
        {
            get { return _ANO_TERMINO; }
            set { _ANO_TERMINO = value; }
        }


    }
}
