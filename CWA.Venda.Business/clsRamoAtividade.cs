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
    public class RamoAtividadeBusiness
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

        public List<RamoAtividadeEntity> GetRamoAtividadeList(int pintTipoRamo)
        {

            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetRamoAtividadeList;" + pintTipoRamo.ToString();

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
                    List<RamoAtividadeEntity> lcolEnt = new List<RamoAtividadeEntity>();

                    lcolEnt = (List<RamoAtividadeEntity>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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

        public string GetRamoAtividadeListService(int pintTipoRamo)
        {
            try
            {
                    RamoAtividadeData ObjData = new RamoAtividadeData();

                    List<RamoAtividadeEntity> lcolEnt = new List<RamoAtividadeEntity>();

                    lcolEnt = ObjData.GetRamoAtividadeEntityList(pintTipoRamo);

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
