using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class CuboMovRecJCEntity
    {

        private int _COD_CLIENTE;
        private string _CONTRATO;
        private string _DT_MOV;
        private int _CD_MOTIVO_RECLAMACAO;
        private string _DT_EDICAO;
        
        public int COD_CLIENTE
        {
            get { return _COD_CLIENTE; }
            set { _COD_CLIENTE = value; }
        }

        public string CONTRATO
        {
            get { return _CONTRATO; }
            set { _CONTRATO = value; }
        }


        public string DT_MOV
        {
            get { return _DT_MOV; }
            set { _DT_MOV = value; }
        }

        public int CD_MOTIVO_RECLAMACAO
        {
            get { return _CD_MOTIVO_RECLAMACAO; }
            set { _CD_MOTIVO_RECLAMACAO = value; }
        }

        public string DT_EDICAO
        {
            get { return _DT_EDICAO; }
            set { _DT_EDICAO = value; }
        }


    }
}
