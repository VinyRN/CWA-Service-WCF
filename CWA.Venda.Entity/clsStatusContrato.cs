using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class StatusContratoEntity
    {

        private int _ST_ESTADO_ATUAL;
        private int _CD_MOTIVO_SUSCANC;
        private DateTime _DT_VENDA;

        private string _NM_PESSOA;
        private int _ST_ONLINE_PROD1;
        private int _ST_ONLINE_PROD2;
        private int _ST_TP_PRODUTO;

        private int _CD_CAMPANHA;
        private int _CD_PLANO;

        public int ST_ESTADO_ATUAL
        {
            get { return _ST_ESTADO_ATUAL; }
            set { _ST_ESTADO_ATUAL = value; }
        }

        public int CD_MOTIVO_SUSCANC
        {
            get { return _CD_MOTIVO_SUSCANC; }
            set { _CD_MOTIVO_SUSCANC = value; }
        }

        public DateTime DT_VENDA
        {
            get { return _DT_VENDA; }
            set { _DT_VENDA = value; }
        }
        public string NM_PESSOA
        {
            get { return _NM_PESSOA; }
            set { _NM_PESSOA = value; }
        }
        public int ST_ONLINE_PROD1
        {
            get { return _ST_ONLINE_PROD1; }
            set { _ST_ONLINE_PROD1 = value; }
        }
        public int ST_ONLINE_PROD2
        {
            get { return _ST_ONLINE_PROD2; }
            set { _ST_ONLINE_PROD2 = value; }
        }
        public int ST_TP_PRODUTO
        {
            get { return _ST_TP_PRODUTO; }
            set { _ST_TP_PRODUTO = value; }
        }

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


    }
}
