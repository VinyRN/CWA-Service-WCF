using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class UsuarioLoginEntity
    {
        private int _CD_USUARIO;
        private string _DS_LOGIN;
        private string _DS_SENHA;

        public int CD_USUARIO
        {
            get { return _CD_USUARIO; }
            set { _CD_USUARIO = value; }
        }

        public string DS_LOGIN
        {
            get { return _DS_LOGIN; }
            set { _DS_LOGIN = value; }
        }

        public string DS_SENHA
        {
            get { return _DS_SENHA; }
            set { _DS_SENHA = value; }
        }

        private string _NU_SERIE_CTR;
        private string _NU_CTR;
        private string _NU_DV_CTR;
        private int _ST_ESTADO_ATUAL;
        private int _CD_MOTIVO;
        private int _TIPO;
        public string NU_SERIE_CTR
        {
            get { return _NU_SERIE_CTR; }
            set { _NU_SERIE_CTR = value; }
        }
        public string NU_CTR
        {
            get { return _NU_CTR; }
            set { _NU_CTR = value; }
        }
        public string NU_DV_CTR
        {
            get { return _NU_DV_CTR; }
            set { _NU_DV_CTR = value; }
        }
        public int ST_ESTADO_ATUAL
        {
            get { return _ST_ESTADO_ATUAL; }
            set { _ST_ESTADO_ATUAL = value; }
        }
        public int CD_MOTIVO
        {
            get { return _CD_MOTIVO; }
            set { _CD_MOTIVO = value; }
        }
        public int TIPO
        {
            get { return _TIPO; }
            set { _TIPO = value; }
        }

    }
}
