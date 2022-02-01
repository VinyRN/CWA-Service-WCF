using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class TipoAssinaturaEntregaEntity
    {
        private int _CD_TIPO_ASSINATURA;
        private int _CD_TIPO_ENTREGA;
        private int _QTD_PRODUTO;
        private int _NU_QTD_SEMANAS;
        private int _QTD_DIAS_UTEIS;
        private int _QTD_DIAS_DOM;

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
        public int QTD_PRODUTO
        {
            get { return _QTD_PRODUTO; }
            set { _QTD_PRODUTO = value; }
        }

        public int NU_QTD_SEMANAS
        {
            get { return _NU_QTD_SEMANAS; }
            set { _NU_QTD_SEMANAS = value; }
        }

        public int QTD_DIAS_UTEIS
        {
            get { return _QTD_DIAS_UTEIS; }
            set { _QTD_DIAS_UTEIS = value; }
        }
        public int QTD_DIAS_DOM
        {
            get { return _QTD_DIAS_DOM; }
            set { _QTD_DIAS_DOM = value; }
        }


    }
}
