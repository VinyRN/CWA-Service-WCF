using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class TipoPessoaEntity
    {
        private int _ID_TIPO_PESSOA;
        private string _DS_TIPO_PESSOA;

        public int ID_TIPO_PESSOA
        {
            get { return _ID_TIPO_PESSOA; }
            set { _ID_TIPO_PESSOA = value; }
        }

        public string DS_TIPO_PESSOA
        {
            get { return _DS_TIPO_PESSOA; }
            set { _DS_TIPO_PESSOA = value; }
        }
    }
}
