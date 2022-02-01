using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class CargaWebCorreioEntity
    {
        private int _TIPO;
        private int _CD_CONTABIL_PESSOA;
        private string _NM_PESSOA;
        private string _NM_DEPENDENTE;
        private string _NU_CPF;
        private string _NU_CNPJ;
        private string _DS_EMAIL;
        private string _DT_TERMINO;
        private string _NM_RESPONSAVEL;
        private int _ST_IND_ADESAO_CLUBE;

        private string _TIPO_PESSOA;
        private int _CD_CAMPANHA;
        private string _NM_CAMPANHA;


        public int TIPO
        {
            get { return _TIPO; }
            set { _TIPO = value; }
        }
        public int CD_CONTABIL_PESSOA
        {
            get { return _CD_CONTABIL_PESSOA; }
            set { _CD_CONTABIL_PESSOA = value; }
        }
        public string NM_PESSOA
        {
            get { return _NM_PESSOA; }
            set { _NM_PESSOA = value; }
        }
        public string NM_DEPENDENTE
        {
            get { return _NM_DEPENDENTE; }
            set { _NM_DEPENDENTE = value; }
        }
        public string NU_CPF
        {
            get { return _NU_CPF; }
            set { _NU_CPF = value; }
        }
        public string NU_CNPJ
        {
            get { return _NU_CNPJ; }
            set { _NU_CNPJ = value; }
        }
        public string DS_EMAIL
        {
            get { return _DS_EMAIL; }
            set { _DS_EMAIL = value; }
        }
                public string DT_TERMINO
        {
            get { return _DT_TERMINO; }
            set { _DT_TERMINO = value; }
        }
        public string NM_RESPONSAVEL
        {
            get { return _NM_RESPONSAVEL; }
            set { _NM_RESPONSAVEL = value; }
        }
        public int ST_IND_ADESAO_CLUBE
        {
            get { return _ST_IND_ADESAO_CLUBE; }
            set { _ST_IND_ADESAO_CLUBE = value; }
        }
        public string TIPO_PESSOA
        {
            get { return _TIPO_PESSOA; }
            set { _TIPO_PESSOA = value; }
        }
        public int CD_CAMPANHA
        {
            get { return _CD_CAMPANHA; }
            set { _CD_CAMPANHA = value; }
        }
        public string NM_CAMPANHA
        {
            get { return _NM_CAMPANHA; }
            set { _NM_CAMPANHA = value; }
        }
    }
}
