using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CWA.Venda.Data;
using CWA.Central.Entity;
using CWA.Central.Domain.BusinessEntities;
using CWA.Util;
using CWA.EngineServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CWA.Central.Business
{
    public class GrupoAfinidadeCentralBusiness
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


        public List<CWA.Central.Domain.BusinessEntities.GrupoAfinidade> GetLerGrupoAfinidadeCentral()
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ""; // ObjService.GetLerEmailDoAssinanteCentralJSON((int)CD_CONTABIL_PESSOA);

                if (lstrRet != "")
                {
                    List<CWA.Central.Domain.BusinessEntities.GrupoAfinidade> lcolEnt = new List<CWA.Central.Domain.BusinessEntities.GrupoAfinidade>();

                    lcolEnt = (List<CWA.Central.Domain.BusinessEntities.GrupoAfinidade>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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

        public string GetLerGrupoAfinidadeCentralService()
        {
            try
            {

                GrupoAfinidadeData ObjData = new GrupoAfinidadeData();
                List<GrupoAfinidadeCentralEntity> lcolEnt = new List<GrupoAfinidadeCentralEntity>();

                lcolEnt = ObjData.GetLerGrupoAfinidadeCentral();

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

        public List<CWA.Central.Domain.BusinessEntities.GrupoAfinidade> GetLerGrupoAfinidadeCentral(int pintTipo)
        {
            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ""; // ObjService.GetLerEmailDoAssinanteCentralJSON((int)CD_CONTABIL_PESSOA);

                if (lstrRet != "")
                {
                    List<CWA.Central.Domain.BusinessEntities.GrupoAfinidade> lcolEnt = new List<CWA.Central.Domain.BusinessEntities.GrupoAfinidade>();

                    lcolEnt = (List<CWA.Central.Domain.BusinessEntities.GrupoAfinidade>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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

        public string GetLerGrupoAfinidadeCentralService(int pintTipo)
        {
            try
            {

                GrupoAfinidadeData ObjData = new GrupoAfinidadeData();
                List<GrupoAfinidadeCentralEntity> lcolEnt = new List<GrupoAfinidadeCentralEntity>();

                lcolEnt = ObjData.GetLerGrupoAfinidadeCentral(pintTipo);

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
