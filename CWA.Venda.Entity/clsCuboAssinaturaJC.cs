using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class CuboAssinaturaJCEntity
    {
        private int    _COD_CLIENTE;
        private string _CONTRATO;
        private string _DS_TP_ASS;
        private string _DS_GRUPO_ENTREGA;
        private string _DS_TP_PAGTO;
        private string _QTD_EXEMPLARES;
        private string _DT_INICIO_ASS;
        private string _DT_FINAL_ASS;
        private string _PLANO_COMERCIAL;
        private string _DT_VENDA_ASS;
        private string _ST_ASSINATURA;
        private string _DT_CANC;
        private string _ST_CORPORATIVO;
        private int    _COD_PRODUTO;
        private string _DS_TP_VENDA;
        private string _DT_ATIVACAO;
        private int _NU_PERIODO;

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

        public string DS_TP_ASS
        {
            get { return _DS_TP_ASS; }
            set { _DS_TP_ASS = value; }
        }

        public string DS_GRUPO_ENTREGA
        {
            get { return _DS_GRUPO_ENTREGA; }
            set { _DS_GRUPO_ENTREGA = value; }
        }

        public string DS_TP_PAGTO
        {
            get { return _DS_TP_PAGTO; }
            set { _DS_TP_PAGTO = value; }
        }

        public string QTD_EXEMPLARES
        {
            get { return _QTD_EXEMPLARES; }
            set { _QTD_EXEMPLARES = value; }
        }

        public string DT_INICIO_ASS
        {
            get { return _DT_INICIO_ASS; }
            set { _DT_INICIO_ASS = value; }
        }

        public string DT_FINAL_ASS
        {
            get { return _DT_FINAL_ASS; }
            set { _DT_FINAL_ASS = value; }
        }

        public string PLANO_COMERCIAL
        {
            get { return _PLANO_COMERCIAL; }
            set { _PLANO_COMERCIAL = value; }
        }

        public string DT_VENDA_ASS
        {
            get { return _DT_VENDA_ASS; }
            set { _DT_VENDA_ASS = value; }
        }

        public string ST_ASSINATURA
        {
            get { return _ST_ASSINATURA; }
            set { _ST_ASSINATURA = value; }
        }

        public string DT_CANC
        {
            get { return _DT_CANC; }
            set { _DT_CANC = value; }
        }

        public string ST_CORPORATIVO
        {
            get { return _ST_CORPORATIVO; }
            set { _ST_CORPORATIVO = value; }
        }

        public int COD_PRODUTO
        {
            get { return _COD_PRODUTO; }
            set { _COD_PRODUTO = value; }
        }

        public string DS_TP_VENDA
        {
            get { return _DS_TP_VENDA; }
            set { _DS_TP_VENDA = value; }
        }

        public string DT_ATIVACAO
        {
            get { return _DT_ATIVACAO; }
            set { _DT_ATIVACAO = value; }
        }

        public int NU_PERIODO
        {
            get { return _NU_PERIODO; }
            set { _NU_PERIODO = value; }
        }
    }
}
