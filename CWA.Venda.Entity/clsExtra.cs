using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ExtraEnt
    {

        private string _TIPO;
        private int _NU_RECIBO;
        private int _NU_PARCELA;
        private int _NU_DV;
        private int _CD_EDITORA;
        private int _CD_BANCO;

        public string TIPO
        {
            get { return _TIPO; }
            set { _TIPO = value; }
        }

        public int NU_RECIBO
        {
            get { return _NU_RECIBO; }
            set { _NU_RECIBO = value; }
        }

        public int NU_PARCELA
        {
            get { return _NU_PARCELA; }
            set { _NU_PARCELA = value; }
        }

        public int NU_DV
        {
            get { return _NU_DV; }
            set { _NU_DV = value; }
        }

        public int CD_EDITORA
        {
            get { return _CD_EDITORA; }
            set { _CD_EDITORA = value; }
        }

        public int CD_BANCO
        {
            get { return _CD_BANCO; }
            set { _CD_BANCO = value; }
        }

    }
}
