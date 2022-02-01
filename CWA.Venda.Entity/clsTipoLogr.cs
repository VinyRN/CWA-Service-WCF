using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class TipoLogradouroEntity
    {
        private int _ID_TIPO_LOGR;
        private string _DS_TIPO_LOGR;

        public int ID_TIPO_LOGR
        {
            get { return _ID_TIPO_LOGR; }
            set { _ID_TIPO_LOGR = value; }
        }

        public string DS_TIPO_LOGR
        {
            get { return _DS_TIPO_LOGR; }
            set { _DS_TIPO_LOGR = value; }
        }
    }
}
