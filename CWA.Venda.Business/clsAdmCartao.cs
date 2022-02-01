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
    public class AdmCartaoBusiness
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

        public List<BandeiraCartaoEntity> GetBandeiraCartaoList()
        {

            try
            {

                AdmCartaoData ObjData = new AdmCartaoData();

                List<BandeiraCartaoEntity> lcolEnt = new List<BandeiraCartaoEntity>();

                lcolEnt = ObjData.GetBandeiraCartaoEntityList();

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

        public List<AdmCartaoEntity> GetAdmCartaoList(int pintBandeira)
        {

            try
            {

                AdmCartaoData ObjData = new AdmCartaoData();

                List<AdmCartaoEntity> lcolEnt = new List<AdmCartaoEntity>();

                lcolEnt = ObjData.GetAdmCartaoEntityList(pintBandeira);

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

        public List<AnoVenctoCartaoEntity> GetAnoVencimento() 
        {

            try
            {
                string lstrVenctoCartaoAdd = ConfigurationManager.AppSettings["VenctoCartaoAdd"];
                

                if (lstrVenctoCartaoAdd.Trim() != "")
                {

                    string lstrDataBase = DateTime.Now.ToString("dd/MM/yyyy");
                    string lstrAnoTemp = lstrDataBase.Substring(6, 4);

                    List<AnoVenctoCartaoEntity> lcolEnt = new List<AnoVenctoCartaoEntity>();
                    AnoVenctoCartaoEntity ObjEnt = new AnoVenctoCartaoEntity();    

                    int lintAno = 0;
                    lintAno = int.Parse(lstrAnoTemp);

                    ObjEnt.ID_ANO = lstrAnoTemp;
                    ObjEnt.DS_ANO = lstrAnoTemp;

                    lcolEnt.Add(ObjEnt);

                    for (int i = 1; i <= int.Parse(lstrVenctoCartaoAdd); i++)
                    {
                        lintAno = lintAno + 1;

                        ObjEnt = new AnoVenctoCartaoEntity();  

                        ObjEnt.ID_ANO = lintAno.ToString();
                        ObjEnt.DS_ANO = lintAno.ToString();

                        lcolEnt.Add(ObjEnt);                    
                    }


                    return lcolEnt;
                }
                else
                {
                    _erro = -99;
                    _msgErro = "Anos para vencimento do cartão não parametrizado.";

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

        public bool GetAdmCobraGateWay(int pintIDAdmCartao)
        {

            try
            {

                AdmCartaoData ObjData = new AdmCartaoData();
                AdmCartaoEntity ObjEnt = new AdmCartaoEntity();

                ObjEnt = ObjData.GetAdmCartao(pintIDAdmCartao);

                if (ObjEnt != null)
                {

                    if (ObjEnt.ST_COBR_GATEWAY == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {

                _erro = -1;
                _msgErro = ex.Message;

                return false;
            }
        
        }

        public List<BandeiraCartaoEntityNOVO> GetBandeiraCartaoListNovo()
        {

            try
            {

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetBandeiraCartaoListNovo;";

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
                    List<BandeiraCartaoEntityNOVO> lcolEnt = new List<BandeiraCartaoEntityNOVO>();

                    lcolEnt = (List<BandeiraCartaoEntityNOVO>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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
       
        public string GetBandeiraCartaoListService()
        {

            try
            {

                AdmCartaoData ObjData = new AdmCartaoData();

                List<BandeiraCartaoEntity> lcolEnt = new List<BandeiraCartaoEntity>();

                lcolEnt = ObjData.GetBandeiraCartaoEntityList();

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

        public string GetAdmCartaoListService(int pintBandeira)
        {

            try
            {

                AdmCartaoData ObjData = new AdmCartaoData();

                List<AdmCartaoEntity> lcolEnt = new List<AdmCartaoEntity>();

                lcolEnt = ObjData.GetAdmCartaoEntityList(pintBandeira);

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

        public string GetAdmCobraGateWayService(int pintIDAdmCartao)
        {

            try
            {
                string lstrCobraGateWay = "";

                AdmCartaoData ObjData = new AdmCartaoData();
                AdmCartaoEntity ObjEnt = new AdmCartaoEntity();

                ObjEnt = ObjData.GetAdmCartao(pintIDAdmCartao);

                if (ObjEnt != null)
                {

                    

                    if (ObjEnt.ST_COBR_GATEWAY == 1)
                    {
                        lstrCobraGateWay = "true";
                    }
                    else
                    {
                        lstrCobraGateWay = "false";
                    }

                }
                else
                {
                    lstrCobraGateWay = "false";
                }

                string lstrRet = JsonConvert.SerializeObject(lstrCobraGateWay, Formatting.None);
                return lstrRet;

            }
            catch (Exception ex)
            {

                _erro = -1;
                _msgErro = ex.Message;

                return "";
            }

        }

        public string GetBandeiraCartaoListNovoService()
        {

            try
            {

                AdmCartaoData ObjData = new AdmCartaoData();

                List<BandeiraCartaoEntityNOVO> lcolEnt = new List<BandeiraCartaoEntityNOVO>();

                lcolEnt = ObjData.GetBandeiraCartaoEntityListNOVO();

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
