using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class CampanhaPrecoEntity
    {
        private int _CD_CAMPANHA;
        private int _CD_PLANO;
        private int _CD_GRUPO_PRECO_ED;
        private DateTime _DT_VIGOR;
        private double _VA_PARC1_NOVAS;
        private double _VA_DEMAIS_PARC1_NOVAS;
        private double _VA_TOTAL_NOVA;
        private double _VA_PARC1_RENOVA;
        private double _VA_DEMAIS_PARC1_RENOVA;
        private double _VA_TOTAL_RENOVA;

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

        public int CD_GRUPO_PRECO_ED
        {
            get { return _CD_GRUPO_PRECO_ED; }
            set { _CD_GRUPO_PRECO_ED = value; }
        }
        public DateTime DT_VIGOR
        {
            get { return _DT_VIGOR; }
            set { _DT_VIGOR = value; }
        }

        public double VA_PARC1_NOVAS
        {
            get { return _VA_PARC1_NOVAS; }
            set { _VA_PARC1_NOVAS = value; }
        }
        public double VA_DEMAIS_PARC1_NOVAS
        {
            get { return _VA_DEMAIS_PARC1_NOVAS; }
            set { _VA_DEMAIS_PARC1_NOVAS = value; }
        }
        public double VA_TOTAL_NOVA
        {
            get { return _VA_TOTAL_NOVA; }
            set { _VA_TOTAL_NOVA = value; }
        }
        public double VA_PARC1_RENOVA
        {
            get { return _VA_PARC1_RENOVA; }
            set { _VA_PARC1_RENOVA = value; }
        }
        public double VA_DEMAIS_PARC1_RENOVA
        {
            get { return _VA_DEMAIS_PARC1_RENOVA; }
            set { _VA_DEMAIS_PARC1_RENOVA = value; }
        }

        public double VA_TOTAL_RENOVA
        {
            get { return _VA_TOTAL_RENOVA; }
            set { _VA_TOTAL_RENOVA = value; }
        }


    }
}
