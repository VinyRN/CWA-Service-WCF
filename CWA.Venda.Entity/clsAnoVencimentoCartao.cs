using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class AnoVenctoCartaoEntity
    {
        private string _ID_ANO;
        private string _DS_ANO;

        public string ID_ANO
        {
            get { return _ID_ANO; }
            set { _ID_ANO = value; }
        }

        public string DS_ANO
        {
            get { return _DS_ANO; }
            set { _DS_ANO = value; }
        }

    }
}


