using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class FormaExpedicaoEntity
    {
        private string _DS_FORMA_EXPEDICAO;
        private int _CD_FORMA_EXPEDICAO;

        public int CD_FORMA_EXPEDICAO
        {
            get { return _CD_FORMA_EXPEDICAO; }
            set { _CD_FORMA_EXPEDICAO = value; }
        }

        public string DS_FORMA_EXPEDICAO
        {
            get { return _DS_FORMA_EXPEDICAO; }
            set { _DS_FORMA_EXPEDICAO = value; }
        }

    }
}
