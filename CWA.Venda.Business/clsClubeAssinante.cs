using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

using CWA.Venda.Data;
using CWA.Venda.Entity;

namespace CWA.Venda.Business
{
    public class ClubeAssinanteBusiness
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

        public ClubeAssinanteEntity GetDadosCarteiraClube(int pintCdPessoa, string pstrSerieCTR, int pintNuCTR, int pintDvCTR)
        {

            try
            {
                ClubeAssinanteData ObjData = new ClubeAssinanteData();

                ClubeAssinanteEntity lEnt = new ClubeAssinanteEntity();

                lEnt = ObjData.GetDadosCarteiraClube(pintCdPessoa, pstrSerieCTR, pintNuCTR, pintDvCTR);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    return lEnt;
                }


            }
            catch (Exception ex)
            {

                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }
    }
}
