using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class RamoAtividadeEntity
    {
        private int _CD_RAMO;
        private string _DS_RAMO;

        public int CD_RAMO
        {
            get { return _CD_RAMO; }
            set { _CD_RAMO = value; }
        }

        public string DS_RAMO
        {
            get { return _DS_RAMO; }
            set { _DS_RAMO = value; }
        }

    }
}
