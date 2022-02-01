using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class CuboTiposRecJCEntity
    {
        private int _CD_MOTIVO_RECLAMACAO;
        private string _DS_MOTIVO_RECLAMACAO;
        
        public int CD_MOTIVO_RECLAMACAO
        {
            get { return _CD_MOTIVO_RECLAMACAO; }
            set { _CD_MOTIVO_RECLAMACAO = value; }
        }

        public string DS_MOTIVO_RECLAMACAO
        {
            get { return _DS_MOTIVO_RECLAMACAO; }
            set { _DS_MOTIVO_RECLAMACAO = value; }
        }

    }
}
