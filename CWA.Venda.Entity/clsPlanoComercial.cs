using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class PlanoComercialEntity
    {

        private string _DS_PLANO;
        private int _CD_PLANO;
        private int _CD_TIPO_ASSINATURA;
        private int _CD_TIPO_ENTREGA;
        private int _CD_FORMA_PAG;

        public string DS_PLANO
        {
            get { return _DS_PLANO; }
            set { _DS_PLANO = value; }
        }

        public int CD_PLANO
        {
            get { return _CD_PLANO; }
            set { _CD_PLANO = value; }
        }

        public int CD_TIPO_ASSINATURA
        {
            get { return _CD_TIPO_ASSINATURA; }
            set { _CD_TIPO_ASSINATURA = value; }
        }

        public int CD_TIPO_ENTREGA
        {
            get { return _CD_TIPO_ENTREGA; }
            set { _CD_TIPO_ENTREGA = value; }
        }

        public int CD_FORMA_PAG
        {
            get { return _CD_FORMA_PAG; }
            set { _CD_FORMA_PAG = value; }
        }

        public TipoAssinaturaEntregaEntity TipoAssinaturaEntrega { get; set; }

    }
}
