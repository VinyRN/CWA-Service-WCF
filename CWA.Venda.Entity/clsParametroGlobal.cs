using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class ParametroGlobalEntity
    {
        private int _ID_PARAMETRO;

        private string _NU_TEL_CONTATO;
        private string _DS_MSG_ERRO;
        private string _DS_MSG_SUCESSO;


        public int ID_PARAMETRO
        {
            get { return _ID_PARAMETRO; }
            set { _ID_PARAMETRO = value; }
        }


        public string NU_TEL_CONTATO
        {
            get { return _NU_TEL_CONTATO; }
            set { _NU_TEL_CONTATO = value; }
        }

        public string DS_MSG_ERRO
        {
            get { return _DS_MSG_ERRO; }
            set { _DS_MSG_ERRO = value; }
        }

        public string DS_MSG_SUCESSO
        {
            get { return _DS_MSG_SUCESSO; }
            set { _DS_MSG_SUCESSO = value; }
        }

    }
}
