using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class PrecoCampanhaPlanoProdutoEntity
    {
        private string _DS_PLANO_MKT;
        private string _DS_CAMPANHA;
        private string _DS_PLANO;
        private string _DS_FORMA_PAG;
        private string _VL_VALOR_TOTAL;

        private string _VL_PRIM_PARC;
        private string _VL_DEMAIS_PARC;
        private string _ST_PARC_DIF;

        private string _NU_PARCELA;
        private int _PRODUTO_ONLINE;

        public string DS_PLANO_MKT
        {
            get { return _DS_PLANO_MKT; }
            set { _DS_PLANO_MKT = value; }
        }

        public string DS_CAMPANHA
        {
            get { return _DS_CAMPANHA; }
            set { _DS_CAMPANHA = value; }
        }

        public string DS_PLANO
        {
            get { return _DS_PLANO; }
            set { _DS_PLANO = value; }
        }

        public string DS_FORMA_PAG
        {
            get { return _DS_FORMA_PAG; }
            set { _DS_FORMA_PAG = value; }
        }

        public string VL_VALOR_TOTAL
        {
            get { return _VL_VALOR_TOTAL; }
            set { _VL_VALOR_TOTAL = value; }
        }

        public string VL_PRIM_PARC
        {
            get { return _VL_PRIM_PARC; }
            set { _VL_PRIM_PARC = value; }
        }

        public string VL_DEMAIS_PARC
        {
            get { return _VL_DEMAIS_PARC; }
            set { _VL_DEMAIS_PARC = value; }
        }

        public string ST_PARC_DIF
        {
            get { return _ST_PARC_DIF; }
            set { _ST_PARC_DIF = value; }
        }

        public string NU_PARCELA
        {
            get { return _NU_PARCELA; }
            set { _NU_PARCELA = value; }
        }

        public int PRODUTO_ONLINE
        {
            get { return _PRODUTO_ONLINE; }
            set { _PRODUTO_ONLINE = value; }
        }

        
    }
}
