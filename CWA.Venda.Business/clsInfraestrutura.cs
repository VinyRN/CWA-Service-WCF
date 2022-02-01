using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

using CWA.Venda.Data;
using CWA.Venda.Entity;
using CWA.Util;
using CWA.EngineServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CWA.Venda.Business
{
    public class InfraestruturaBusiness
    {
        private System.Int32 _erro;
        private string _msgErro;

        public System.Int32 Erro
        {
            get { return _erro; }
        }

        public string MsgErro
        {
            get { return _msgErro; }
        }

        public void GetStatusConexaoBD()
        {
            try
            {
                bool lbolStatusConexao = false;

                lbolStatusConexao = DataContextSQL.AbrirConexao(null);

                if (lbolStatusConexao == false)
                {
                    _erro = DataContextSQL.Erro;
                    _msgErro = DataContextSQL.MsgErro;

                    DataContextSQL.FecharConexao();
                }
                else
                {
                    _erro = 0;
                    _msgErro = "Conexão com banco de dados OK";

                    DataContextSQL.FecharConexao();
                }

                return;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return;
            }
        }

    }
}
