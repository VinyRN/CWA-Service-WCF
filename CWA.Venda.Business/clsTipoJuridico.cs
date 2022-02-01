using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CWA.Venda.Data;
using CWA.Venda.Entity;

namespace CWA.Venda.Business
{
    public class TipoJuridicoBusiness
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

        public List<TipoJuridicoEntity> GetTipoJuridicoList()
        {

            try
            {

                TipoJuridicoData ObjData = new TipoJuridicoData();

                List<TipoJuridicoEntity> lcolEnt = new List<TipoJuridicoEntity>();

                lcolEnt = ObjData.GetTipoJuridicoEntityList();

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    return lcolEnt;
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
