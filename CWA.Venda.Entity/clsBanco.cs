using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class BancoEntity
    {
        private int _CD_BANCO;
        private string _NM_BANCO;
        private int _NU_BANCO;

        public int CD_BANCO
        {
            get { return _CD_BANCO; }
            set { _CD_BANCO = value; }
        }

        public string NM_BANCO
        {
            get { return _NM_BANCO; }
            set { _NM_BANCO = value; }
        }

        public int NU_BANCO
        {
            get { return _NU_BANCO; }
            set { _NU_BANCO = value; }
        }

    }
}
