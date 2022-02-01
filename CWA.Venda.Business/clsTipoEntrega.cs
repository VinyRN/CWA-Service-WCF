using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CWA.Venda.Data;
using CWA.Venda.Entity;
using CWA.Util;
using CWA.EngineServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CWA.Venda.Business
{
    public class TipoEntregaBusiness
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

        public List<TipoEntregaEntity> GetTipoEntregaList(int pintIDProduto, int pintIDCampanha, int pintIDTipoAssinatura)
        {

            try
            {

                TipoEntregaData ObjData = new TipoEntregaData();

                List<TipoEntregaEntity> lcolEnt = new List<TipoEntregaEntity>();

                lcolEnt = ObjData.GetTipoEntregaEntityList(pintIDProduto, pintIDCampanha, pintIDTipoAssinatura);

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


        public string GetTipoEntregaListService()
        {

            try
            {

                TipoEntregaData ObjData = new TipoEntregaData();

                List<TipoEntregaEntity> lcolEnt = new List<TipoEntregaEntity>();

                lcolEnt = ObjData.GetTipoEntregaEntityList();

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Formatting.None);
                    return lstrRet;
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
