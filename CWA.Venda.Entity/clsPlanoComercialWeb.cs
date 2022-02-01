using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class PlanoComercialWebEntity
    {

        //USADO PARA PEGAR OS VALORES EM CENTAVOS PARA VENDA.
        private int _NU_PARCELA;
        private string _VA_PARC_CENT;
        private string _VA_DEMAIS_PARC_CENT;
        private string _VA_TOTAL_CENT;

        //DADOS DO PLANO GERAL
        private int _CD_TP_ASSINATURA;
        private string _DS_TP_ASSINATURA;
        private int _CD_TP_ENTREGA;
        private string _DS_TP_ENTREGA;
        private string _DIAS_ENTREGA;
        private int _CD_FORMA_PAG;
        private string _DS_FORMA_PAG;
        private int _CD_TP_PAG;
        private string _DS_TP_PAG;

        //DADOS DO PLANO
        private int _CD_PLANO;
        private string _DS_PLANO;

        public int NU_PARCELA
        {
            get { return _NU_PARCELA; }
            set { _NU_PARCELA = value; }
        }

        public string VA_PARC_CENT
        {
            get { return _VA_PARC_CENT; }
            set { _VA_PARC_CENT = value; }
        }

        public string VA_DEMAIS_PARC_CENT
        {
            get { return _VA_DEMAIS_PARC_CENT; }
            set { _VA_DEMAIS_PARC_CENT = value; }
        }

        public string VA_TOTAL_CENT
        {
            get { return _VA_TOTAL_CENT; }
            set { _VA_TOTAL_CENT = value; }
        }

        public int CD_TP_ASSINATURA
        {
            get { return _CD_TP_ASSINATURA; }
            set { _CD_TP_ASSINATURA = value; }
        }

        public string DS_TP_ASSINATURA
        {
            get { return _DS_TP_ASSINATURA; }
            set { _DS_TP_ASSINATURA = value; }
        }

        public int CD_TP_ENTREGA
        {
            get { return _CD_TP_ENTREGA; }
            set { _CD_TP_ENTREGA = value; }
        }

        public string DS_TP_ENTREGA
        {
            get { return _DS_TP_ENTREGA; }
            set { _DS_TP_ENTREGA = value; }
        }

        public int CD_FORMA_PAG
        {
            get { return _CD_FORMA_PAG; }
            set { _CD_FORMA_PAG = value; }
        }

        public string DS_FORMA_PAG
        {
            get { return _DS_FORMA_PAG; }
            set { _DS_FORMA_PAG = value; }
        }

        public int CD_TP_PAG
        {
            get { return _CD_TP_PAG; }
            set { _CD_TP_PAG = value; }
        }

        public string DIAS_ENTREGA
        {
            get { return _DIAS_ENTREGA; }
            set { _DIAS_ENTREGA = value; }
        }

        public int CD_PLANO
        {
            get { return _CD_PLANO; }
            set { _CD_PLANO = value; }
        }

        public string DS_PLANO
        {
            get { return _DS_PLANO; }
            set { _DS_PLANO = value; }
        }

        public string DS_TP_PAG
        {
            get { return _DS_TP_PAG; }
            set { _DS_TP_PAG = value; }
        }

    }
}
