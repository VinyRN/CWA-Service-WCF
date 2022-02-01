using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class TipoEntregaEntity
    {
        private int _CD_TIPO_ENTREGA;
        private string _DS_TIPO_ENTREGA;

        private int _ST_ENTREGA_SEG;
        private int _ST_ENTREGA_TER;
        private int _ST_ENTREGA_QUA;
        private int _ST_ENTREGA_QUI;
        private int _ST_ENTREGA_SEX;
        private int _ST_ENTREGA_SAB;
        private int _ST_ENTREGA_DOM;

        public int CD_TIPO_ENTREGA
        {
            get { return _CD_TIPO_ENTREGA; }
            set { _CD_TIPO_ENTREGA = value; }
        }

        public string DS_TIPO_ENTREGA
        {
            get { return _DS_TIPO_ENTREGA; }
            set { _DS_TIPO_ENTREGA = value; }
        }

        public int ST_ENTREGA_SEG
        {
            get { return _ST_ENTREGA_SEG; }
            set { _ST_ENTREGA_SEG = value; }
        }

        public int ST_ENTREGA_TER
        {
            get { return _ST_ENTREGA_TER; }
            set { _ST_ENTREGA_TER = value; }
        }

        public int ST_ENTREGA_QUA
        {
            get { return _ST_ENTREGA_QUA; }
            set { _ST_ENTREGA_QUA = value; }
        }

        public int ST_ENTREGA_QUI
        {
            get { return _ST_ENTREGA_QUI; }
            set { _ST_ENTREGA_QUI = value; }
        }

        public int ST_ENTREGA_SEX
        {
            get { return _ST_ENTREGA_SEX; }
            set { _ST_ENTREGA_SEX = value; }
        }

        public int ST_ENTREGA_SAB
        {
            get { return _ST_ENTREGA_SAB; }
            set { _ST_ENTREGA_SAB = value; }
        }

        public int ST_ENTREGA_DOM
        {
            get { return _ST_ENTREGA_DOM; }
            set { _ST_ENTREGA_DOM = value; }
        }



    }
}
