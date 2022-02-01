using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class CampanhaPlanoWEBEntity
    {

        private int _CD_CAMPANHA;
        private int _CD_PLANO;
        private string _CD_IMG_MKT;
        private int _CD_TP_CANAL;
        private DateTime _DT_CADASTRO;
        private int _CD_USUARIO;
        private string _DS_TIT_MKT;
        private string _DS_MKT;
        private int _ST_DESTAQUE;
        private int _NU_ORDEM;

        private string _DS_CAMPANHA;
        private string _DS_PLANO;
        private int _CD_PRODUTO;
        private string _DS_DET_MKT;

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

        public string CD_IMG_MKT
        {
            get { return _CD_IMG_MKT; }
            set { _CD_IMG_MKT = value; }
        }

        public int CD_TP_CANAL
        {
            get { return _CD_TP_CANAL; }
            set { _CD_TP_CANAL = value; }
        }

        public DateTime DT_CADASTRO
        {
            get { return _DT_CADASTRO; }
            set { _DT_CADASTRO = value; }
        }

        public int CD_USUARIO
        {
            get { return _CD_USUARIO; }
            set { _CD_USUARIO = value; }
        }

        public string DS_TIT_MKT
        {
            get { return _DS_TIT_MKT; }
            set { _DS_TIT_MKT = value; }
        }

        public string DS_MKT
        {
            get { return _DS_MKT; }
            set { _DS_MKT = value; }
        }

        public int ST_DESTAQUE
        {
            get { return _ST_DESTAQUE; }
            set { _ST_DESTAQUE = value; }
        }

        public int NU_ORDEM
        {
            get { return _NU_ORDEM; }
            set { _NU_ORDEM = value; }
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

        public int CD_PRODUTO
        {
            get { return _CD_PRODUTO; }
            set { _CD_PRODUTO = value; }
        }

        public string DS_DET_MKT
        {
            get { return _DS_DET_MKT; }
            set { _DS_DET_MKT = value; }
        }


    }
}
