using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class CampanhaPlanoIntegracaoEntity
    {
        private int _CD_CAMPANHA;
        private int _CD_PLANO;
        private int _ST_ACAO;

        public int CD_CAMPANHA
        {
            get { return _CD_CAMPANHA; }
            set { _CD_CAMPANHA = value; }
        }

        public int CD_PLANO
        {
            get { return _CD_PLANO; }
            set { _CD_PLANO = value; }
        }

        public int ST_ACAO
        {
            get { return _ST_ACAO; }
            set { _ST_ACAO = value; }
        }


    }
}
