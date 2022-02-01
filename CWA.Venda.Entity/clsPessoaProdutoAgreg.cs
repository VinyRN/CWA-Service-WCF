using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class PessoaProdutoAgregEntity
    {
        int _CD_CONTABIL_PESSOA;
        int _ST_TP_PESSOA;
        string _NM_PESSOA;
        string _NM_SOBRENOME;
        string _NU_CPF;
        string _DT_NASC_FUND;
        string _DS_EMAIL;
        string _NU_CNPJ;
        string _NM_RESPONSAVEL;
        string _NU_CPF_RESP;
        string _NM_FANTASIA;
        string _NU_INSCR_MUN;
        string _NU_INSCR_EST;
        int _CD_RAMO;

        public int CD_CONTABIL_PESSOA
        {
            get { return _CD_CONTABIL_PESSOA; }
            set { _CD_CONTABIL_PESSOA = value; }
        }
        public int ST_TP_PESSOA
        {
            get { return _ST_TP_PESSOA; }
            set { _ST_TP_PESSOA = value; }
        }
        public string NM_PESSOA
        {
            get { return _NM_PESSOA; }
            set { _NM_PESSOA = value; }
        }
        public string NM_SOBRENOME
        {
            get { return _NM_SOBRENOME; }
            set { _NM_SOBRENOME = value; }
        }
        public string NU_CPF
        {
            get { return _NU_CPF; }
            set { _NU_CPF = value; }
        }
        public string DT_NASC_FUND
        {
            get { return _DT_NASC_FUND; }
            set { _DT_NASC_FUND = value; }
        }
        public string DS_EMAIL
        {
            get { return _DS_EMAIL; }
            set { _DS_EMAIL = value; }
        }
        public string NU_CNPJ
        {
            get { return _NU_CNPJ; }
            set { _NU_CNPJ = value; }
        }
        public string NM_RESPONSAVEL
        {
            get { return _NM_RESPONSAVEL; }
            set { _NM_RESPONSAVEL = value; }
        }
        public string NM_FANTASIA
        {
            get { return _NM_FANTASIA; }
            set { _NM_FANTASIA = value; }
        }
        public string NU_INSCR_MUN
        {
            get { return _NU_INSCR_MUN; }
            set { _NU_INSCR_MUN = value; }
        }
        public string NU_INSCR_EST
        {
            get { return _NU_INSCR_EST; }
            set { _NU_INSCR_EST = value; }
        }
        public int CD_RAMO
        {
            get { return _CD_RAMO; }
            set { _CD_RAMO = value; }
        }


    }
}
