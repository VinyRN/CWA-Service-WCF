using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWA.Services
{
    public class ResultIntegracaoLahar
    {
        public string status { get; set; }
        public Dados data { get; set; }
    }
    public class Dados
    {
        public string link { get; set; }
        public string Return { get; set; }
        public Erro error { get; set; }
    }

    public class Erro
    {
        public string code { get; set; }
        public string message { get; set; }
    }

}