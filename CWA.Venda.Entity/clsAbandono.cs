using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class AbandonoEntity
    {
        private Int32 _ID_LOG;
        private DateTime _DT_LOG;
        private string _HR_LOG;
        private string _NU_DOC;
        private string _DS_EMAIL;
        private string _DS_REG_PESSOA;
        private string _DS_REG_ENDER;
        private string _DS_REG_VENDA;
        private string _NU_SERIE_CTR;
        private int _NU_CTR;
        private int _NU_DV_CTR;
        private int _TP_ETAPA;
        private int _ST_STATUS;

        private int _ST_MKT;
        private DateTime _DT_MKT;

        public Int32 ID_LOG
        {
            get { return _ID_LOG; }
            set { _ID_LOG = value; }
        }

        public DateTime DT_LOG
        {
            get { return _DT_LOG; }
            set { _DT_LOG = value; }
        }

        public string HR_LOG
        {
            get { return _HR_LOG; }
            set { _HR_LOG = value; }
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

        public string DS_REG_VENDA
        {
            get { return _DS_REG_VENDA; }
            set { _DS_REG_VENDA = value; }
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

        public int TP_ETAPA
        {
            get { return _TP_ETAPA; }
            set { _TP_ETAPA = value; }
        }

        public int ST_STATUS
        {
            get { return _ST_STATUS; }
            set { _ST_STATUS = value; }
        }
        public int ST_MKT
        {
            get { return _ST_MKT; }
            set { _ST_MKT = value; }
        }

        public DateTime DT_MKT
        {
            get { return _DT_MKT; }
            set { _DT_MKT = value; }
        }


    }
}
