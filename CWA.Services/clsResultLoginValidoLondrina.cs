using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWA.Services
{
    public class ResultLoginValidoLondrina
    {
        public string status { get; set; }
        public string mensagem { get; set; }
        public string acesso_central { get; set; }
        public string acesso_jornal { get; set; }
        public string token { get; set; }
    }

    public class ResultUserMW
    {
        public string cpfCnpj { get; set; }
        public string email { get; set; }
    }
}