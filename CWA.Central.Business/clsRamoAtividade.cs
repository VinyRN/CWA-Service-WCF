﻿using System;
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
    public class RamoAtividadeCentralBusiness
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


        public List<CWA.Central.Domain.BusinessEntities.RamoAtividades> GetLerRamoAtividadeCentral()
        {

            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetLerRamoAtividadeCentralJSON();

                if (lstrRet != "")
                {
                    List<CWA.Central.Domain.BusinessEntities.RamoAtividades> lcolEnt = new List<CWA.Central.Domain.BusinessEntities.RamoAtividades>();

                    lcolEnt = (List<CWA.Central.Domain.BusinessEntities.RamoAtividades>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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
        public string GetLerRamoAtividadeCentralService()
        {

            try
            {

                RamoAtividadeData ObjData = new RamoAtividadeData();
                List<RamoAtividadeCentralEntity> lcolEnt = new List<RamoAtividadeCentralEntity>();

                lcolEnt = ObjData.GetRamoAtividadeCentralList();


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
