using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class RetornoAPIRest
    {
        public int status { get; set; }
        public string descricao { get; set; }

        public MensagemAPI mensagem { get; set; }
    }


    public class MensagemAPI
    {
        public string retorno { get; set; }
    }

}
