using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class TipoAssinaturaEntity
    {
        private int _CD_TIPO_ASSINATURA;
        private string _DS_TIPO_ASSINATURA;

        public int CD_TIPO_ASSINATURA
        {
            get { return _CD_TIPO_ASSINATURA; }
            set { _CD_TIPO_ASSINATURA = value; }
        }

        public string DS_TIPO_ASSINATURA
        {
            get { return _DS_TIPO_ASSINATURA; }
            set { _DS_TIPO_ASSINATURA = value; }
        }

    }
}
