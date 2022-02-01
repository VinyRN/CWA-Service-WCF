using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class DadosEntregaEntity
    {
        private int _ID_LOGR;
        private string _DS_TIPO;
        private string _DS_LOGR;
        private string _DS_BAIRRO;
        private string _DS_MUNICIPIO;
        private string _DS_UF;
        private string _NU_CEP;

        private string _NU_RESID;
        private string _DS_COMPL;

        private string _CD_TIPO;

        private int _ST_LOGR_EXT;
        private int _ST_CEP_UNICO;

        public int ID_LOGR
        {
            get { return _ID_LOGR; }
            set { _ID_LOGR = value; }
        }

        public string DS_TIPO
        {
            get { return _DS_TIPO; }
            set { _DS_TIPO = value; }
        }

        public string DS_LOGR
        {
            get { return _DS_LOGR; }
            set { _DS_LOGR = value; }
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

        public string NU_RESID
        {
            get { return _NU_RESID; }
            set { _NU_RESID = value; }
        }

        public string DS_COMPL
        {
            get { return _DS_COMPL; }
            set { _DS_COMPL = value; }
        }

        public string CD_TIPO
        {
            get { return _CD_TIPO; }
            set { _CD_TIPO = value; }
        }

        public int ST_LOGR_EXT
        {
            get { return _ST_LOGR_EXT; }
            set { _ST_LOGR_EXT = value; }
        }

        public int ST_CEP_UNICO
        {
            get { return _ST_CEP_UNICO; }
            set { _ST_CEP_UNICO = value; }
        }

    }
}
