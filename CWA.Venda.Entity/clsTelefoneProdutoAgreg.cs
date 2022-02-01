using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class TelefoneProdutoAgregEntity
    {
        int _ST_TP_TELEFONE;
        int _NU_DDD;
        string _NU_TEL;
        string _NU_RAMAL;
        string _DS_OBS;

        public int ST_TP_TELEFONE
        {
            get { return _ST_TP_TELEFONE; }
            set { _ST_TP_TELEFONE = value; }
        }
        public int NU_DDD
        {
            get { return _NU_DDD; }
            set { _NU_DDD = value; }
        }
        public string NU_TEL
        {
            get { return _NU_TEL; }
            set { _NU_TEL = value; }
        }
        public string NU_RAMAL
        {
            get { return _NU_RAMAL; }
            set { _NU_RAMAL = value; }
        }
        public string DS_OBS
        {
            get { return _DS_OBS; }
            set { _DS_OBS = value; }
        }


    }
}
