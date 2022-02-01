using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Central.Entity
{
    public class RetornoJSON
    {
        string _CodigoRetorno;
        string _DescricaoRetorno;
        string _DadosRetorno;

        public string CodigoRetorno
        {
            get { return _CodigoRetorno; }
            set { _CodigoRetorno = value; }
        }

        public string DescricaoRetorno
        {
            get { return _DescricaoRetorno; }
            set { _DescricaoRetorno = value; }
        }

        public string DadosRetorno
        {
            get { return _DadosRetorno; }
            set { _DadosRetorno = value; }
        }
    }
}
