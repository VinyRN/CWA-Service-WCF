using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class CampanhaEntity
    {

        private int _CD_CAMPANHA;
        private string _DS_CAMPANHA;
        private int _CD_CANAL;
        private int _CD_PRODUTO;
        private int _CD_PRODUTO_DIGITAL;

        public int CD_CAMPANHA
        {
            get { return _CD_CAMPANHA; }
            set { _CD_CAMPANHA = value; }
        }
        public string DS_CAMPANHA
        {
            get { return _DS_CAMPANHA; }
            set { _DS_CAMPANHA = value; }
        }
        public int CD_CANAL
        {
            get { return _CD_CANAL; }
            set { _CD_CANAL = value; }
        }
        public int CD_PRODUTO
        {
            get { return _CD_PRODUTO; }
            set { _CD_PRODUTO = value; }
        }
        public int CD_PRODUTO_DIGITAL
        {
            get { return _CD_PRODUTO_DIGITAL; }
            set { _CD_PRODUTO_DIGITAL = value; }
        }

    }
}
