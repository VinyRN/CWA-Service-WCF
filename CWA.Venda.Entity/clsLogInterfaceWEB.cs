using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class LogInterfaceWEBEntity
    {
        private int _ID_LOG;
        private string  _DT_LOG;
        private string _HR_LOG;
        private int _TP_LOG;
        private string _DS_CHAMADOR;
        private string _DS_METODO;
        private string _DS_URL;
        private string _DS_REG_PESSOA;
        private string _DS_REG_ENDER;
        private string _DS_REG_PGTO;
        private string _DS_INPUT_GATEWAY;
        private string _DS_OUTPUT_GATEWAY;
        private string _DS_OBS;

        private string _NU_SERIE_CTR;
        private int _NU_CTR;
        private int _NU_DV_CTR;
        private string _DS_ERRO;

        public int ID_LOG
        {
            get { return _ID_LOG; }
            set { _ID_LOG = value; }
        }

        public string DT_LOG
        {
            get { return _DT_LOG; }
            set { _DT_LOG = value; }
        }

        public string HR_LOG
        {
            get { return _HR_LOG; }
            set { _HR_LOG = value; }
        }

        public int TP_LOG
        {
            get { return _TP_LOG; }
            set { _TP_LOG = value; }
        }

        public string DS_CHAMADOR
        {
            get { return _DS_CHAMADOR; }
            set { _DS_CHAMADOR = value; }
        }

        public string DS_METODO
        {
            get { return _DS_METODO; }
            set { _DS_METODO = value; }
        }

        public string DS_URL
        {
            get { return _DS_URL; }
            set { _DS_URL = value; }
        }

        public string DS_REG_PESSOA
        {
            get { return _DS_REG_PESSOA; }
            set { _DS_REG_PESSOA = value; }
        }

        public string DS_REG_ENDER
        {
            get { return _DS_REG_ENDER; }
            set { _DS_REG_ENDER = value; }
        }

        public string DS_REG_PGTO
        {
            get { return _DS_REG_PGTO; }
            set { _DS_REG_PGTO = value; }
        }

        public string DS_INPUT_GATEWAY
        {
            get { return _DS_INPUT_GATEWAY; }
            set { _DS_INPUT_GATEWAY = value; }
        }

        public string DS_OUTPUT_GATEWAY
        {
            get { return _DS_OUTPUT_GATEWAY; }
            set { _DS_OUTPUT_GATEWAY = value; }
        }

        public string DS_OBS
        {
            get { return _DS_OBS; }
            set { _DS_OBS = value; }
        }

        public string NU_SERIE_CTR
        {
            get { return _NU_SERIE_CTR; }
            set { _NU_SERIE_CTR = value; }
        }

        public int NU_CTR
        {
            get { return _NU_CTR; }
            set { _NU_CTR = value; }
        }

        public int NU_DV_CTR
        {
            get { return _NU_DV_CTR; }
            set { _NU_DV_CTR = value; }
        }

        public string DS_ERRO
        {
            get { return _DS_ERRO; }
            set { _DS_ERRO = value; }
        }
    }
}
