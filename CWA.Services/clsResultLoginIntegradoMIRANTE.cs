using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWA.Services
{
    public class ResultLoginIntegradoMIRANTE
    {
        public userinfo userinfo { get; set; }
        public bool error { get; set; }
        public string status { get; set; }
        public string description { get; set; }
    }

    public class permissao
    {
        public int central { get; set; }
        public int site { get; set; }
    }

    public class userinfo
    {
        public string codigo_assinante { get; set; }
        public string codigo_contrato { get; set; }
        public permissao Permissao { get; set; }
    }

}