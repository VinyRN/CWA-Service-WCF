using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class TipoJuridicoEntity
    {
        private int _ID_TIPO_JURIDICO;
        private string _DS_TIPO_JURIDICO;

        public int ID_TIPO_JURIDICO
        {
            get { return _ID_TIPO_JURIDICO; }
            set { _ID_TIPO_JURIDICO = value; }
        }

        public string DS_TIPO_JURIDICO
        {
            get { return _DS_TIPO_JURIDICO; }
            set { _DS_TIPO_JURIDICO = value; }
        }
    }
}
