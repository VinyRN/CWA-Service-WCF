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
    public class ParametroGlobalBusiness
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

        public void GravaParametroGlobalWEB(ParametroGlobalEntity pObjEnt)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                ParametroGlobalData ObjData = new ParametroGlobalData();

                DataContext.AbrirConexao();

                ObjData.SetParametroGlobalWEB(pObjEnt, true, true);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return;
                }

                return;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
            }

        }
        public void AtualizaParametroGlobalWEB(ParametroGlobalEntity pObjEnt)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                ParametroGlobalData ObjData = new ParametroGlobalData();

                DataContext.AbrirConexao();

                ObjData.UpdParametroGlobalWEB(pObjEnt, true, true);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return;
                }

                return;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
            }

        }
        public ParametroGlobalEntity GetParametroGlobalWEB()
        {
            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GravaParametroGlobalWEB;";

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
                    ParametroGlobalEntity lEnt = new ParametroGlobalEntity();

                    lEnt = (ParametroGlobalEntity)JsonConvert.DeserializeObject(lstrRet, lEnt.GetType());

                    return lEnt;

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
        public string GetParametroGlobalWEBService()
        {
            try
            {
                ParametroGlobalData ObjData = new ParametroGlobalData();

                ParametroGlobalEntity lEnt = new ParametroGlobalEntity();

                lEnt = ObjData.GetParametroGlobalWEB();

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lEnt, Formatting.None);
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

        public ParametroGlobalEDEntity GetParametroGlobalED()
        {
            try
            {
                _erro = 0;
                _msgErro ="";

                ParametroGlobalEDEntity ObjEnt = new ParametroGlobalEDEntity();
                ParametroGlobalData ObjData = new ParametroGlobalData();

                //ObjEnt = ObjData.GetParametroGlobalED();
                ObjEnt = ObjData.GetParametroGlobalEDSQL();

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    return ObjEnt;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "GetParametroGlobalED - " + ex.Message;

                return null;
            }
        }

    }
}
