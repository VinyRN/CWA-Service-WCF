using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class SexoEntity
    {
        private int _ID_SEXO;
        private string _DS_SEXO;

        public int ID_SEXO
        {
            get { return _ID_SEXO; }
            set { _ID_SEXO = value; }
        }

        public string DS_SEXO
        {
            get { return _DS_SEXO; }
            set { _DS_SEXO = value; }
        }
    }
}
