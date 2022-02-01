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
    public class CampanhaPlanoBusiness
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

        public List<CampanhaPlanoEntity> GetCampanhaPlanoList(int pintIDCampanha, int pintIDCanal)
        {

            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetCampanhaPlanoList;" + pintIDCampanha.ToString() + ";" + pintIDCanal.ToString();

                ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                lstrVetRet = ObjInterface.VertorRetorno;

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        lstrRet = lstrVetRet[2];
                    }

                }

                if (lstrRet != "")
                {
                    List<CampanhaPlanoEntity> lcolEnt = new List<CampanhaPlanoEntity>();

                    lcolEnt = (List<CampanhaPlanoEntity>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

                    return lcolEnt;

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

                    return null;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }

        }

        public string GetCampanhaPlanoListService(int pintIDCampanha, int pintIDCanal)
        {

            try
            {

                CampanhaPlanoData ObjData = new CampanhaPlanoData();

                List<CampanhaPlanoEntity> lcolEnt = new List<CampanhaPlanoEntity>();

                lcolEnt = ObjData.GetCampanhaPlano(pintIDCampanha, pintIDCanal);

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

        public string SetSincronizacaoCampanhaPlanoTerceiro(string pstrVetorCampPlano)
        {
            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,SetSincronizacaoCampanhaPlanoTerceiro;" + pstrVetorCampPlano;

                ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                lstrVetRet = ObjInterface.VertorRetorno;

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        lstrRet = lstrVetRet[2];
                    }
                }

                if (lstrRet != "")
                {
                    return "OK,";
                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

                    return "Erro," + _msgErro;
                }
                
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return "Erro," + _msgErro;
            }
        }

    }
}
